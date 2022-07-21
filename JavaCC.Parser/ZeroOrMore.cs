namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class ZeroOrMore : Expansion
{
    public Expansion Expansion;

    public ZeroOrMore(Token token, Expansion expansion)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        Expansion = expansion;
        Expansion.Parent = this;
    }

    public ZeroOrMore() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        if (s.Contains(this)) return builder;
        s.Add(this);
        builder.Append(EOL).Append(Expansion.Dump(i + 1, s));
        return builder;
    }
}
