using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class Sequence : Expansion
{
	public List<Expansion> units = new();
	
	public Sequence() { }

	
	public Sequence(Token t, Lookahead l)
	{
		line = t.beginLine;
		column = t.beginColumn;
		units.Add(l);
	}

	
	public override StringBuilder dump(int i, HashSet<Expansion> s)
	{
		if (s.Contains(this))
		{
			//var stringBuilder = base.dump(0, s).insert(0, '[').Append(']');
			//CharSequence s2 = default(CharSequence);
			//object obj = (s2.___003Cref_003E = dumpPrefix(i));
			//StringBuilder result = stringBuilder.insert(0, s2);
			
			return this.DumpPrefix(i).Append( "["+base.dump(0,s) + "]");
		}
		s.Add(this);
		var stringBuffer2 = base.dump(i, s);
		//Iterator iterator = units.iterator();
		foreach (var expansion in this.units) 
		{
			stringBuffer2.Append(Expansion.EOL).Append(expansion.dump(i + 1, s));
		}
		return stringBuffer2;
	}

}
