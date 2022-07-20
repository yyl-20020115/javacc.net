namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class ZeroOrOne : Expansion
{
    public Expansion Expansion;

    public ZeroOrOne(Token t, Expansion e)
    {
        Line = t.BeginLine;
        Column = t.BeginColumn;
        Expansion = e;
        e.parent = this;
    }

    public ZeroOrOne() { }
    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        if (s.Contains(this))
        {
            return builder;
        }
        s.Add(this);
        builder.Append(EOL).Append(Expansion.Dump(i + 1, s));
        return builder;
    }
}
