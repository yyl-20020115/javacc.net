namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Action : Expansion
{
    public List<Token> ActionTokens = new();

    public Action() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var stringBuilder = base.Dump(i, s);
        s.Add(this);
        if (ActionTokens.Count > 0)
        {
            stringBuilder.Append(' ').Append(ActionTokens[0]);
        }
        return stringBuilder;
    }

}
