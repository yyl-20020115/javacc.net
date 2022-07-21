namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Sequence : Expansion
{
    public readonly List<Expansion> Units = new();

    public Sequence() { }


    public Sequence(Token token, Lookahead lookahead)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        Units.Add(lookahead);
    }


    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        if (s.Contains(this))
        {
            return this.DumpPrefix(i).Append("[" + base.Dump(0, s) + "]");
        }
        s.Add(this);
        var builder = base.Dump(i, s);
        foreach (var unit in this.Units)
        {
            builder.Append(EOL).Append(unit.Dump(i + 1, s));
        }
        return builder;
    }

}
