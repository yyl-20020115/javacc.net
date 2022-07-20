namespace JavaCC.Parser;

public class RZeroOrMore : RegularExpression
{
    public RegularExpression Regexpr;

    public RZeroOrMore(Token token, RegularExpression regexpr)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        Regexpr = regexpr;
    }

    public RZeroOrMore() { }

    public override Nfa GenerateNfa(bool b)
    {
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        var nfa2 = Regexpr.GenerateNfa(b);
        start.AddMove(nfa2.Start);
        start.AddMove(end);
        nfa2.End.AddMove(end);
        nfa2.End.AddMove(nfa2.Start);
        return nfa;
    }
}
