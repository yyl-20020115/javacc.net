namespace JavaCC.Parser;
using System.Collections.Generic;
using System.Text;

public class NonTerminal : Expansion
{
    public readonly List<Token> LhsTokens = new();

    public string Name = "";

    public readonly List<Token> ArgumentTokens = new();

    public NormalProduction Production;

    public NonTerminal() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s) => base.Dump(i, s).Append(' ').Append(Name);
}
