namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class TryBlock : Expansion
{
    public Expansion Expression;
    public List<List<Token>> Types = new();
    public List<Token> Ids = new();
    public List<List<Token>> CatchBlocks = new();
    public List<Token> FinallyBlock = new();
    public TryBlock() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = base.Dump(i, s);
        if (s.Contains(this)) return builder;
        s.Add(this);
        builder.Append(EOL).Append(Expression.Dump(i + 1, s));
        return builder;
    }
}