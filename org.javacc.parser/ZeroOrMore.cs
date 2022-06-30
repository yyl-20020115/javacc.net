using System.Collections.Generic;
using System.Text;
namespace org.javacc.parser;

public class ZeroOrMore : Expansion
{
	public Expansion expansion;
	
	public ZeroOrMore(Token t, Expansion e)
	{
		line = t.beginLine;
		column = t.beginColumn;
		expansion = e;
		expansion.parent = this;
	}

	public ZeroOrMore() { }
	
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
