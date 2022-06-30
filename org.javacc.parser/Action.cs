

using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace org.javacc.parser;

public class Action : Expansion
{
	public ArrayList action_tokens = new();

	public Action() { }
	
	public override StringBuilder dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.dump(i, s);
		s.Add(this);
		if (action_tokens.Count > 0)
		{
			stringBuilder.Append(' ').Append(action_tokens[0]);
		}
		return stringBuilder;
	}

}
