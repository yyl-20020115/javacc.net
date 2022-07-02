using System.Collections.Generic;
using System.Text;
namespace Javacc.Parser;

public class ZeroOrMore : Expansion
{
	public Expansion expansion;
	
	public ZeroOrMore(Token t, Expansion e)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
		expansion = e;
		expansion.parent = this;
	}

	public ZeroOrMore() { }
	
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
