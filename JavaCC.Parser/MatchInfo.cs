namespace Javacc.Parser;

public class MatchInfo
{
	public static int laLimit;
	internal int[] match;
	internal int firstFreeLoc;
	
	public MatchInfo()
	{
		match = new int[laLimit];
	}

	public static void ReInit()
	{
		laLimit = 0;
	}
}
