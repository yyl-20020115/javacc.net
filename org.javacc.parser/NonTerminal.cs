using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class NonTerminal : Expansion
{
	public ArrayList lhsTokens = new();

	public string name = "";

	public ArrayList argument_tokens = new();

	public NormalProduction prod;

	public NonTerminal() { }

    public override StringBuilder Dump(int i, HashSet<Expansion> s) => base.Dump(i, s).Append(' ').Append(name);

}
