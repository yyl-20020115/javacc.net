namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class ZeroOrOne : Expansion
{
    public Expansion expansion;

    public ZeroOrOne(Token t, Expansion e)
    {
        Line = t.BeginLine;
        Column = t.BeginColumn;
        expansion = e;
        e.parent = this;
    }

    public ZeroOrOne() { }
    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var stringBuilder = base.Dump(i, s);
        if (s.Contains(this))
        {
            return stringBuilder;
        }
        s.Add(this);
        stringBuilder.Append(Expansion.EOL).Append(expansion.Dump(i + 1, s));
        return stringBuilder;
    }

}
