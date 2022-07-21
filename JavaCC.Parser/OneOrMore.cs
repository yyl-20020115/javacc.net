namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class OneOrMore : Expansion
{
    public Expansion Expansion;

    public OneOrMore(Token token, Expansion expansion)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        Expansion = expansion;
        Expansion.Parent = this;
    }

    public override StringBuilder Dump(int i, HashSet<Expansion> set)
    {
        var builder = base.Dump(i, set);
        if (set.Contains(this))
        {
            return builder;
        }
        set.Add(this);
        builder.Append(EOL).Append(Expansion.Dump(i + 1, set));
        return builder;
    }
}
