using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public abstract class RegularExpression : Expansion
{
	public string label;

	internal new int ordinal;

	public ArrayList lhsTokens;

	public Token rhsToken;

	public bool private_rexp;

	public TokenProduction tpContext;

	internal int walkStatus;

	public virtual bool CanMatchAnyChar() => false;

	public abstract Nfa GenerateNfa(bool b);
	
	public RegularExpression()
	{
		label = "";
		lhsTokens = new ArrayList();
		private_rexp = false;
		tpContext = null;
		walkStatus = 0;
	}

	
	public override StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.Dump(i, s);
		s.Add(this);
		stringBuilder.Append(' ').Append(label);
		return stringBuilder;
	}

}
