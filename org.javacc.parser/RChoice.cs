using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class RChoice : RegularExpression
{
	public List<Expansion> Choices = new();

	public RChoice() { }
	
	public virtual void CheckUnmatchability()
	{
		int num = 0;
		for (int i = 0; i < Choices.Count; i++)
		{
			RegularExpression regularExpression;
			if (!(regularExpression = (RegularExpression)Choices[i]).private_rexp && regularExpression.ordinal > 0 && regularExpression.ordinal < ordinal && LexGen.lexStates[regularExpression.ordinal] == LexGen.lexStates[ordinal])
			{
				if (label != null)
				{
					JavaCCErrors.Warning(this, ("Regular Expression choice : ")+(regularExpression.label)+(" can never be matched as : ")
						+(label)
						);
				}
				else
				{
					JavaCCErrors.Warning(this, ("Regular Expression choice : ")+(regularExpression.label)+(" can never be matched as token of kind : ")
						+(ordinal)
						);
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
		for (int i = 0; i < Choices.Count; i++)
		{
			RegularExpression regularExpression = (RegularExpression)Choices[i];
			while (regularExpression is RJustName)
			{
				regularExpression = ((RJustName)regularExpression).regexpr;
			}
			if (regularExpression is RStringLiteral && (((RStringLiteral)regularExpression).image.Length) == 1)
			{
				
				this.Choices[i]=	regularExpression = new RCharacterList(
                    (((RStringLiteral)regularExpression).image[0]));
			}
			if (regularExpression is RCharacterList)
			{
				if (((RCharacterList)regularExpression).negated_list)
				{
					((RCharacterList)regularExpression).RemoveNegation();
				}
				var descriptors = ((RCharacterList)regularExpression).descriptors;
				if (rCharacterList == null)
				{
                    Choices[i]=(rCharacterList = new RCharacterList());
				}
				else
				{
					Choices.RemoveAt(i--);
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
		for (int i = 0; i < Choices.Count; i++)
		{
			RegularExpression regularExpression = (RegularExpression)Choices[i];
			while (regularExpression is RJustName j)
			{
				regularExpression = j.regexpr;
			}
			if (regularExpression is RChoice r)
			{
				Choices.RemoveAt(i--);
				int index = r.Choices.Count;
				while (index-- > 0)
				{
					Choices.Add(r.Choices[index]);
				}
			}
		}
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		CompressCharLists();
		if (Choices.Count == 1)
		{
			return (Choices[0] as RegularExpression).GenerateNfa(b);
		}
		var nfa = new Nfa();
		var start = nfa.Start;
		var end = nfa.End;
		for (int i = 0; i < Choices.Count; i++)
		{
			var regularExpression = Choices[i] as RegularExpression;
			if(regularExpression != null)
            {
				var nfa2 = regularExpression.GenerateNfa(b);
				start.AddMove(nfa2.Start);
				nfa2.End.AddMove(end);
			}
		}
		return nfa;
	}
}
