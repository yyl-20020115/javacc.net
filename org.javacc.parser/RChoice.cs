using System.Collections;
using System.Text;

namespace org.javacc.parser;

public class RChoice : RegularExpression
{
	public ArrayList choices;
	
	public RChoice()
	{
		choices = new ArrayList();
	}
	
	public virtual void CheckUnmatchability()
	{
		int num = 0;
		for (int i = 0; i < choices.Count; i++)
		{
			RegularExpression regularExpression;
			if (!(regularExpression = (RegularExpression)choices[i]).private_rexp && regularExpression.ordinal > 0 && regularExpression.ordinal < ordinal && LexGen.lexStates[regularExpression.ordinal] == LexGen.lexStates[ordinal])
			{
				if (label != null)
				{
					JavaCCErrors.Warning(this, new StringBuilder().Append("Regular Expression choice : ").Append(regularExpression.label).Append(" can never be matched as : ")
						.Append(label)
						.ToString());
				}
				else
				{
					JavaCCErrors.Warning(this, new StringBuilder().Append("Regular Expression choice : ").Append(regularExpression.label).Append(" can never be matched as token of kind : ")
						.Append(ordinal)
						.ToString());
				}
			}
			if (!regularExpression.private_rexp && regularExpression is RStringLiteral)
			{
				num++;
			}
		}
	}

	
	internal virtual void CompressCharLists()
	{
		CompressChoices();
		RCharacterList rCharacterList = null;
		for (int i = 0; i < choices.Count; i++)
		{
			RegularExpression regularExpression = (RegularExpression)choices[i];
			while (regularExpression is RJustName)
			{
				regularExpression = ((RJustName)regularExpression).regexpr;
			}
			if (regularExpression is RStringLiteral && String.instancehelper_length(((RStringLiteral)regularExpression).image) == 1)
			{
				ArrayList vector = choices;
				vector.setElementAt(regularExpression = new RCharacterList(String.instancehelper_charAt(((RStringLiteral)regularExpression).image, 0)), i);
			}
			if (regularExpression is RCharacterList)
			{
				if (((RCharacterList)regularExpression).negated_list)
				{
					((RCharacterList)regularExpression).RemoveNegation();
				}
				ArrayList descriptors = ((RCharacterList)regularExpression).descriptors;
				if (rCharacterList == null)
				{
					choices.setElementAt(rCharacterList = new RCharacterList(), i);
				}
				else
				{
					choices.removeElementAt(i--);
				}
				int index = descriptors.Count;
				while (index-- > 0)
				{
                    rCharacterList.descriptors.Add(descriptors[index]);
				}
			}
		}
	}

	
	internal virtual void CompressChoices()
	{
		for (int i = 0; i < choices.Count; i++)
		{
			RegularExpression regularExpression = (RegularExpression)choices[i];
			while (regularExpression is RJustName)
			{
				regularExpression = ((RJustName)regularExpression).regexpr;
			}
			if (regularExpression is RChoice)
			{
				choices.removeElementAt(i--);
				int index = ((RChoice)regularExpression).choices.Count;
				while (index-- > 0)
				{
					choices.Add(((RChoice)regularExpression).choices[index]);
				}
			}
		}
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		CompressCharLists();
		if (choices.Count == 1)
		{
			Nfa result = ((RegularExpression)choices[0]).GenerateNfa(b);
			
			return result;
		}
		Nfa nfa = new Nfa();
		NfaState start = nfa.start;
		NfaState end = nfa.end;
		for (int i = 0; i < choices.Count; i++)
		{
			RegularExpression regularExpression = (RegularExpression)choices[i];
			Nfa nfa2 = regularExpression.GenerateNfa(b);
			start.AddMove(nfa2.start);
			nfa2.end.AddMove(end);
		}
		return nfa;
	}
}
