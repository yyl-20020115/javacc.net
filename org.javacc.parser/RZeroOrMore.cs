namespace org.javacc.parser;

public class RZeroOrMore : RegularExpression
{
	public RegularExpression regexpr;
	
	public RZeroOrMore(Token t, RegularExpression re)
	{
		line = t.beginLine;
		column = t.beginColumn;
		regexpr = re;
	}
	
	public RZeroOrMore() { }
	
	public override Nfa GenerateNfa(bool b)
	{
		Nfa nfa = new Nfa();
		NfaState start = nfa.start;
		NfaState end = nfa.end;
		Nfa nfa2 = regexpr.GenerateNfa(b);
		start.AddMove(nfa2.start);
		start.AddMove(end);
		nfa2.end.AddMove(end);
		nfa2.end.AddMove(nfa2.start);
		return nfa;
	}
}
