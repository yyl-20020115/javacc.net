namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Choice : Expansion
{
    public readonly List<Expansion> Choices = new();
    public Choice() { }
    public Choice(Token token)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
    }

    public Choice(Expansion e)
    {
        Line = e.Line;
        Column = e.Column;
        Choices.Add(e);
    }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        if (s.Contains(this))
        {
            return builder;
        }
        s.Add(this);
        foreach (var expansion in this.Choices)
        {
            builder.Append(Expansion.EOL).Append(expansion.Dump(i + 1, s));
        }
        return builder;
    }
}
