namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class Sequence : Expansion
{
    public List<Expansion> Units = new();

    public Sequence() { }


    public Sequence(Token t, Lookahead l)
    {
        Line = t.BeginLine;
        Column = t.BeginColumn;
        Units.Add(l);
    }


    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        if (s.Contains(this))
        {
            //var stringBuilder = base.dump(0, s).insert(0, '[').Append(']');
            //CharSequence s2 = default(CharSequence);
            //object obj = (s2.___003Cref_003E = dumpPrefix(i));
            //StringBuilder result = stringBuilder.insert(0, s2);

            return this.DumpPrefix(i).Append("[" + base.Dump(0, s) + "]");
        }
        s.Add(this);
        var stringBuffer2 = base.Dump(i, s);
        //Iterator iterator = units.iterator();
        foreach (var expansion in this.Units)
        {
            stringBuffer2.Append(Expansion.EOL).Append(expansion.Dump(i + 1, s));
        }
        return stringBuffer2;
    }

}
