namespace org.javacc.jjtree;
public class Token
{
	public class GTToken : Token
	{
		internal int realKind = 126;

		public GTToken(int i, string str)
			: base(i, str) { }
	}

	public int Kind = 0;
	public int BeginLine = 0;
	public int BeginColumn = 0;
	public int EndLine = 0;
	public int EndColumn = 0;
	public string Image = "";
	public Token Next;
	public Token SpecialToken;
	public Token() { }
	public static Token NewToken(int i, string str)
	{
		switch (i)
		{
			default:
					return new Token(i, str);
			case 124:
			case 125:
			case 126:
					return new GTToken(i, str);
		}
	}

	public Token(int i, string str)
	{
		Kind = i;
		Image = str;
	}
    public virtual object Value => null;
    public Token(int i) : this(i, null) { }
    public override string ToString() => Image;
    public static Token NewToken(int i) => NewToken(i, null);
}
