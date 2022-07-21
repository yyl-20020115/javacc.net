namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public abstract class RegularExpression : Expansion
{
    public string Label = "";

    public new int Ordinal = 0;

    public readonly List<Token> LhsTokens = new();

    public Token RhsToken;

    public bool PrivateRexp = false;

    public TokenProduction TpContext;

    public int WalkStatus = 0;

    public virtual bool CanMatchAnyChar => false;

    public abstract Nfa GenerateNfa(bool b);

    public RegularExpression()
    {
        Label = "";
        LhsTokens = new();
        PrivateRexp = false;
        TpContext = null;
        WalkStatus = 0;
    }


    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        s.Add(this);
        builder.Append(' ').Append(Label);
        return builder;
    }
}
