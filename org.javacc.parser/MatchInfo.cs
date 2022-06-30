namespace org.javacc.parser;

public class MatchInfo
{
	public static int laLimit;
	internal int[] match;
	internal int firstFreeLoc;
	
	public MatchInfo()
	{
		match = new int[laLimit];
	}

	public static void reInit()
	{
		laLimit = 0;
	}
}
