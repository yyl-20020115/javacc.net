namespace JavaCC.Parser;

public class RZeroOrMore : RegularExpression
{
	public RegularExpression regexpr;
	
	public RZeroOrMore(Token t, RegularExpression re)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
		regexpr = re;
	}
	
	public RZeroOrMore() { }
	
	public override Nfa GenerateNfa(bool b)
	{
		var nfa = new Nfa();
		var start = nfa.Start;
		var end = nfa.End;
		var nfa2 = regexpr.GenerateNfa(b);
		start.AddMove(nfa2.Start);
		start.AddMove(end);
		nfa2.End.AddMove(end);
		nfa2.End.AddMove(nfa2.Start);
		return nfa;
	}
}
