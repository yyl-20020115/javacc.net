namespace JavaCC.Parser;

public class MatchInfo
{
    public static int LaLimit = 0;
    public int[] Match;
    public int FirstFreeLoc = 0;

    public MatchInfo()
    {
        Match = new int[LaLimit];
    }

    public static void ReInit()
    {
        LaLimit = 0;
    }
}
