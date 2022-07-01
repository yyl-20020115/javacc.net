namespace org.javacc.parser;

public class ROneOrMore : RegularExpression
{
	public RegularExpression regexpr;
	
	public ROneOrMore(Token t, RegularExpression re)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
		regexpr = re;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		var nfa = new Nfa();
		var start = nfa.Start;
		var end = nfa.End;
		var nfa2 = regexpr.GenerateNfa(b);
		start.AddMove(nfa2.Start);
		nfa2.End.AddMove(nfa2.Start);
		nfa2.End.AddMove(end);
		return nfa;
	}
	
	public ROneOrMore()
	{
	}
}
