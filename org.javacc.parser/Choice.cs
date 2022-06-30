using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace org.javacc.parser;


public class Choice : Expansion
{
	public List<Expansion> choices = new();
	
	public Choice()
	{
	}

	
	public Choice(Token t)
	{
		line = t.beginLine;
		column = t.beginColumn;
	}

	
	public Choice(Expansion e)
	{
		line = e.line;
		column = e.column;
		choices.Add(e);
	}

	
	public override StringBuilder dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.dump(i, s);
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		foreach (var expansion in this.choices)
			stringBuilder.Append(Expansion.EOL).Append(expansion.dump(i + 1, s));
	
		return stringBuilder;
	}
}
