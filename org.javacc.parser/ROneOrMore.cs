namespace org.javacc.parser;

public class ROneOrMore : RegularExpression
{
	public RegularExpression regexpr;
	
	public ROneOrMore(Token t, RegularExpression re)
	{
		line = t.beginLine;
		column = t.beginColumn;
		regexpr = re;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		Nfa nfa = new Nfa();
		NfaState start = nfa.start;
		NfaState end = nfa.end;
		Nfa nfa2 = regexpr.GenerateNfa(b);
		start.AddMove(nfa2.start);
		nfa2.end.AddMove(nfa2.start);
		nfa2.end.AddMove(end);
		return nfa;
	}
	
	public ROneOrMore()
	{
	}
}
