using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class NonTerminal : Expansion
{
	public ArrayList lhsTokens;

	public string name;

	public ArrayList argument_tokens;

	public NormalProduction prod;
	
	public NonTerminal()
	{
		lhsTokens = new ArrayList();
		argument_tokens = new ArrayList();
	}


    public override StringBuilder dump(int i, HashSet<Expansion> s) => base.dump(i, s).Append(' ').Append(name);

}
