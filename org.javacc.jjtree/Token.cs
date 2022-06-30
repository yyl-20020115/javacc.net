namespace org.javacc.jjtree;

public class Token
{
	public class GTToken : Token
	{
		internal int realKind;

		public GTToken(int i, string str)
			: base(i, str)
		{
			realKind = 126;
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

	public Token()
	{
	}

	public static Token NewToken(int i, string str)
	{
		switch (i)
		{
			default:
				{
					Token result2 = new(i, str);

					return result2;
				}
			case 124:
			case 125:
			case 126:
				{
					GTToken result = new(i, str);

					return result;
				}
		}
	}


	public Token(int i, string str)
	{
		kind = i;
		image = str;
	}

	public virtual object getValue()
	{
		return null;
	}


	public Token(int i)
		: this(i, null) { }

    public override string ToString() => image;
    public static Token newToken(int i) => NewToken(i, null);
}
