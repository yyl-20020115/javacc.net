namespace org.javacc.parser;

public class Token
{	
	public class GTToken : Token
	{
		internal int realKind;		
		public GTToken(int i, string str)
			: base(i, str)
		{
			realKind = 132;
		}
	}

	public int kind;
	public int beginLine;
	public int beginColumn;
	public int endLine;
	public int endColumn;
	public string image;
	public Token next;
	public Token specialToken;

	public Token() { }

	public static Token NewToken(int i, string str)
	{
		switch (i)
		{
		default:
		{
			return new Token(i, str);
		}
		case 130:
		case 131:
		case 132:
		{
			return new GTToken(i, str);
		}
		}
	}

	
	public Token(int i, string str)
	{
		kind = i;
		image = str;
	}

    public virtual object Value() => null;


    public Token(int i)
		: this(i, null) { }

    public override string ToString() => image;

    public static Token NewToken(int i) => NewToken(i, null);
}
