using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class Lookahead : Expansion
{
	public ArrayList action_tokens;
	public int amount;
	public Expansion la_expansion;
	public bool isExplicit;

		
	public Lookahead()
	{
		action_tokens = new ArrayList();
		amount = int.MaxValue;
	}

	
	public override StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.Dump(i, s).Append((!isExplicit) ? " implicit" : " explicit");
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		stringBuilder.Append(Expansion.EOL).Append(la_expansion.Dump(i + 1, s));
		return stringBuilder;
	}

}
