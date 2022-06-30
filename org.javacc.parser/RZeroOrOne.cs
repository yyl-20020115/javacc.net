namespace org.javacc.parser;

public class RZeroOrOne : RegularExpression
{
	public RegularExpression regexpr;

	public RZeroOrOne() { }
	
	public override Nfa GenerateNfa(bool b)
	{
		Nfa nfa = new Nfa();
		NfaState start = nfa.start;
		NfaState end = nfa.end;
		Nfa nfa2 = regexpr.GenerateNfa(b);
		start.AddMove(nfa2.start);
		start.AddMove(end);
		nfa2.end.AddMove(end);
		return nfa;
	}
}
