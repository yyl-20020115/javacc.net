namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Action : Expansion
{
    public readonly List<Token> ActionTokens = new();

    public Action() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        s.Add(this);
        if (ActionTokens.Count > 0)
        {
            builder.Append(' ').Append(ActionTokens[0]);
        }
        return builder;
    }
}
