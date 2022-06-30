using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class ZeroOrOne : Expansion
{
	public Expansion expansion;

	public ZeroOrOne(Token t, Expansion e)
	{
		line = t.beginLine;
		column = t.beginColumn;
		expansion = e;
		e.parent = this;
	}

	public ZeroOrOne() { }	
	public override StringBuilder dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.dump(i, s);
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		stringBuilder.Append(Expansion.EOL).Append(expansion.dump(i + 1, s));
		return stringBuilder;
	}

}
