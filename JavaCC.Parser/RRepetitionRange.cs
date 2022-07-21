namespace JavaCC.Parser;
using System.Collections.Generic;

public class RRepetitionRange : RegularExpression
{
    public RegularExpression Regexpr;

    public int Min = 0;

    public int Max = -1;

    public bool HasMax = false;


    public RRepetitionRange()
    {
        Min = 0;
        Max = -1;
    }


    public override Nfa GenerateNfa(bool b)
    {
        var vector = new List<RegularExpression>();
        int i;
        for (i = 0; i < Min; i++)
        {
            vector.Add(Regexpr);
        }
        if (HasMax && Max == -1)
        {
            vector.Add(new RZeroOrMore()
            {
                Regexpr = Regexpr
            });
        }
        while (true)
        {
            int n = i;
            i++;
            if (n >= Max)
            {
                break;
            }
            vector.Add(new RZeroOrOne
            {
                Regexpr = Regexpr
            });
        }
        RSequence rSequence = new(vector);
        return rSequence.GenerateNfa(b);
    }
}
