using System.Collections;
namespace org.javacc.parser;

public class TokenProduction
{
	public const int TOKEN = 0;

	public const int SKIP = 1;

	public const int MORE = 2;

	public const int SPECIAL = 3;

	internal static string[] _KindImage =
		 new string[] { "TOKEN", "SKIP", "MORE", "SPECIAL" };

	public int line;

	public int column;

	public string[] lexStates;

	public int kind;

	public ArrayList respecs;

	public bool isExplicit;

	public bool ignoreCase;

	public Token firstToken;

	public Token lastToken;


    public static string[] KindImage => _KindImage;

    public TokenProduction()
	{
		respecs = new ArrayList();
		isExplicit = true;
		ignoreCase = false;
	}
}
