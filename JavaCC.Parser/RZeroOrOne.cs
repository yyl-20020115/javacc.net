namespace JavaCC.Parser;

public class RZeroOrOne : RegularExpression
{
    public RegularExpression Regexpr;

    public RZeroOrOne() { }

    public override Nfa GenerateNfa(bool b)
    {
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        var nfa2 = Regexpr.GenerateNfa(b);
        start.AddMove(nfa2.Start);
        start.AddMove(end);
        nfa2.End.AddMove(end);
        return nfa;
    }
}
