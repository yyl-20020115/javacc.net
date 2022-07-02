using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JavaCC.Parser;

public class NonTerminal : Expansion
{
	public List<Token> lhsTokens = new();

	public string name = "";

	public List<Token> argument_tokens = new();

	public NormalProduction prod;

	public NonTerminal() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s) => base.Dump(i, s).Append(' ').Append(name);

}
