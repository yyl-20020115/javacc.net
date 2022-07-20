namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class ZeroOrMore : Expansion
{
    public Expansion Expansion;

    public ZeroOrMore(Token t, Expansion e)
    {
        Line = t.BeginLine;
        Column = t.BeginColumn;
        Expansion = e;
        Expansion.parent = this;
    }

    public ZeroOrMore() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var stringBuilder = base.Dump(i, s);
        if (s.Contains(this))
        {
            return stringBuilder;
        }
        s.Add(this);
        stringBuilder.Append(Expansion.EOL).Append(Expansion.Dump(i + 1, s));
        return stringBuilder;
    }

}
