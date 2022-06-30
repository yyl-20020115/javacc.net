namespace org.javacc.parser;

public class RJustName : RegularExpression
{
	public RegularExpression regexpr;
	
	public RJustName(Token t, string str)
	{
		line = t.beginLine;
		column = t.beginColumn;
		label = str;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		Nfa result = regexpr.GenerateNfa(b);
		
		return result;
	}
	public RJustName()
	{
	}
}
