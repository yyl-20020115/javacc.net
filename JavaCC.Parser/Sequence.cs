namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Sequence : Expansion
{
    public List<Expansion> Units = new();

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
            //var builder = base.dump(0, s).insert(0, '[').Append(']');
            //CharSequence s2 = default(CharSequence);
            //object obj = (s2.___003Cref_003E = dumpPrefix(i));
            //StringBuilder result = builder.insert(0, s2);

            return this.DumpPrefix(i).Append("[" + base.Dump(0, s) + "]");
        }
        s.Add(this);
        var builder = base.Dump(i, s);
        //Iterator iterator = units.iterator();
        foreach (var expansion in this.Units)
        {
            builder.Append(EOL).Append(expansion.Dump(i + 1, s));
        }
        return builder;
    }

}
