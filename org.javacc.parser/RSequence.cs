using System.Collections.Generic;

namespace org.javacc.parser;

public class RSequence : RegularExpression
{
	public List<RegularExpression> units = new();
	
	internal RSequence()
	{
	}

	
	internal RSequence(List<RegularExpression> units)
	{
		ordinal = int.MaxValue;
		this.units = units;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		if (units.Count == 1)
		{
			Nfa result = units[0].GenerateNfa(b);
			
			return result;
		}
		Nfa nfa = new Nfa();
		NfaState start = nfa.Start;
		NfaState end = nfa.End;
		Nfa nfa2 = null;
		var regularExpression = units[0];
		var nfa3 = regularExpression.GenerateNfa(b);
		start.AddMove(nfa3.Start);
		for (int i = 1; i < units.Count; i++)
		{
			regularExpression = units[i];
			nfa2 = regularExpression.GenerateNfa(b);
			nfa3.End.AddMove(nfa2.Start);
			nfa3 = nfa2;
		}
		nfa2.End.AddMove(end);
		return nfa;
	}
}
