namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;


public class Lookahead : Expansion
{
    public List<Token> ActionTokens = new();
    public int amount = int.MaxValue;
    public Expansion LaExpansion;
    public bool IsExplicit = false;

    public Lookahead()
    {
    }


    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s).Append((!IsExplicit) ? " implicit" : " explicit");
        if (s.Contains(this))
        {
            return builder;
        }
        s.Add(this);
        builder.Append(Expansion.EOL).Append(LaExpansion.Dump(i + 1, s));
        return builder;
    }

}
