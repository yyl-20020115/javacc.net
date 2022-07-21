namespace JavaCC.Parser;
using System.Collections.Generic;

public class RChoice : RegularExpression
{
    public readonly List<Expansion> Choices = new();

    public RChoice() { }

    public virtual void CheckUnmatchability()
    {
        int c = 0;
        for (int i = 0; i < Choices.Count; i++)
        {
            RegularExpression regularExpression;
            if (!(regularExpression = (RegularExpression)Choices[i]).PrivateRexp && regularExpression.Ordinal > 0 && regularExpression.Ordinal < Ordinal && LexGen.lexStates[regularExpression.Ordinal] == LexGen.lexStates[Ordinal])
            {
                if (Label != null)
                {
                    JavaCCErrors.Warning(this, ("Regular Expression choice : ") + (regularExpression.Label) + (" can never be matched as : ")
                        + (Label));
                }
                else
                {
                    JavaCCErrors.Warning(this, ("Regular Expression choice : ") + (regularExpression.Label) + (" can never be matched as token of kind : ")
                        + (Ordinal));
                }
            }
            if (!regularExpression.PrivateRexp && regularExpression is RStringLiteral)
            {
                c++;
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
            while (regularExpression is RJustName name)
            {
                regularExpression = name.RegExpr;
            }
            if (regularExpression is RStringLiteral && (((RStringLiteral)regularExpression).Image.Length) == 1)
            {

                this.Choices[i] = regularExpression = new RCharacterList(
                    (((RStringLiteral)regularExpression).Image[0]));
            }
            if (regularExpression is RCharacterList)
            {
                if (((RCharacterList)regularExpression).NegatedList)
                {
                    ((RCharacterList)regularExpression).RemoveNegation();
                }
                var descriptors = ((RCharacterList)regularExpression).Descriptors;
                if (rCharacterList == null)
                {
                    Choices[i] = (rCharacterList = new RCharacterList());
                }
                else
                {
                    Choices.RemoveAt(i--);
                }
                int index = descriptors.Count;
                while (index-- > 0)
                {
                    rCharacterList.Descriptors.Add(descriptors[index]);
                }
            }
        }
    }


    public virtual void CompressChoices()
    {
        for (int i = 0; i < Choices.Count; i++)
        {
            RegularExpression regularExpression = (RegularExpression)Choices[i];
            while (regularExpression is RJustName j)
            {
                regularExpression = j.RegExpr;
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
            return (Choices[0] is RegularExpression re) ? re.GenerateNfa(b) : null;
        }
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        for (int i = 0; i < Choices.Count; i++)
        {
            if (Choices[i] is RegularExpression regularExpression)
            {
                var nfa2 = regularExpression.GenerateNfa(b);
                start.AddMove(nfa2.Start);
                nfa2.End.AddMove(end);
            }
        }
        return nfa;
    }
}
