using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;
public class OneOrMore : Expansion
{
	public Expansion expansion;

	public OneOrMore(Token t, Expansion e)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
		expansion = e;
		expansion.parent = this;
	}
	
	public OneOrMore() { }
	
	public override StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.Dump(i, s);
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		stringBuilder.Append(Expansion.EOL).Append(expansion.Dump(i + 1, s));
		return stringBuilder;
	}
}
