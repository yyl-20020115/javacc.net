namespace JavaCC.Parser;

public class ROneOrMore : RegularExpression
{
    public RegularExpression RegExpr;

    public ROneOrMore(Token token, RegularExpression re)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        RegExpr = re;
    }

    public override Nfa GenerateNfa(bool b)
    {
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        var nfa2 = RegExpr.GenerateNfa(b);
        start.AddMove(nfa2.Start);
        nfa2.End.AddMove(nfa2.Start);
        nfa2.End.AddMove(end);
        return nfa;
    }

}
