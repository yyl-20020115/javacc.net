namespace JavaCC.Parser;
using System.Collections.Generic;

public class RSequence : RegularExpression
{
    public readonly List<RegularExpression> Units = new();

    public RSequence()
    {
    }

    public RSequence(List<RegularExpression> units)
    {
        Ordinal = int.MaxValue;
        this.Units = units;
    }


    public override Nfa GenerateNfa(bool b)
    {
        if (Units.Count == 1)
        {
            return Units[0].GenerateNfa(b);
        }
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        Nfa nfa2 = null;
        var regularExpression = Units[0];
        var nfa3 = regularExpression.GenerateNfa(b);
        start.AddMove(nfa3.Start);
        for (int i = 1; i < Units.Count; i++)
        {
            regularExpression = Units[i];
            nfa2 = regularExpression.GenerateNfa(b);
            nfa3.End.AddMove(nfa2.Start);
            nfa3 = nfa2;
        }
        nfa2.End.AddMove(end);
        return nfa;
    }
}
