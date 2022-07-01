using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class Choice : Expansion
{
	public List<Expansion> Choices = new();
	
	public Choice() { }
	
	public Choice(Token t)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
	}

	
	public Choice(Expansion e)
	{
		Line = e.Line;
		Column = e.Column;
		Choices.Add(e);
	}

	
	public override StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.Dump(i, s);
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		foreach (var expansion in this.Choices)
		{
			stringBuilder.Append(Expansion.EOL).Append(expansion.Dump(i + 1, s));
		}
		return stringBuilder;
	}
}
