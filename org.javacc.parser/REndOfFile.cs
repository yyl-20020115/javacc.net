namespace org.javacc.parser;

public class REndOfFile : RegularExpression
{	
	public REndOfFile()
	{
	}

	public override Nfa GenerateNfa(bool b)
	{
		return null;
	}
}
