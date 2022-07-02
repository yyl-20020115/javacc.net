namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class TryBlock : Expansion
{
    public Expansion exp;
    public List<List<Token>> types = new();
    public List<Token> ids = new();
    public List<List<Token>> catchblks = new();
    public List<Token> finallyblk = new();

    public TryBlock() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var stringBuilder = base.Dump(i, s);
        if (s.Contains(this))
        {
            return stringBuilder;
        }
        s.Add(this);
        stringBuilder.Append(Expansion.EOL).Append(exp.Dump(i + 1, s));
        return stringBuilder;
    }
}