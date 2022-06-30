using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class TryBlock : Expansion
{
	public Expansion exp;
	public ArrayList types;
	public ArrayList ids;
	public ArrayList catchblks;
	public ArrayList finallyblk;

	public TryBlock() { }
	
	public override StringBuilder dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = base.dump(i, s);
		if (s.Contains(this))
		{
			return stringBuilder;
		}
		s.Add(this);
		stringBuilder.Append(Expansion.EOL).Append(exp.dump(i + 1, s));
		return stringBuilder;
	}
}