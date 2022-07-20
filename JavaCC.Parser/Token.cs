namespace JavaCC.Parser;

public class Token
{
    public class GTToken : Token
    {
        public int RealKind { get; protected internal set; }
        public GTToken(int i, string str)
            : base(i, str)
        {
            RealKind = 132;
        }
    }

    public int Kind;
    public int BeginLine;
    public int BeginColumn;
    public int EndLine;
    public int EndColumn;
    public string Image;
    public Token Next;
    public Token SpecialToken;

    public Token() { }
    public static Token NewToken(int i, string str = null)
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


    public Token(int i, string str = null)
    {
        Kind = i;
        Image = str;
    }

    public virtual object Value => null;

    public override string ToString() => Image;

}
