using System.Collections.Generic;
using System.Text;

namespace Javacc.Parser;

public class Lookahead : Expansion
{
	public List<Token> action_tokens = new();
	public int amount = int.MaxValue;
	public Expansion la_expansion;
	public bool isExplicit =false;
		
	public Lookahead()
	{
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
