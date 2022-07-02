using System.Collections.Generic;

namespace JavaCC.Parser;

public class RSequence : RegularExpression
{
	public List<RegularExpression> Units = new();
	
	internal RSequence()
	{
	}

	
	internal RSequence(List<RegularExpression> units)
	{
		ordinal = int.MaxValue;
		this.Units = units;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		if (Units.Count == 1)
		{
			Nfa result = Units[0].GenerateNfa(b);
			
			return result;
		}
		Nfa nfa = new Nfa();
		NfaState start = nfa.Start;
		NfaState end = nfa.End;
		Nfa nfa2 = null;
		var regularExpression = Units[0];
		var nfa3 = regularExpression.GenerateNfa(b);
		start.AddMove(nfa3.Start);
		for (int i = 1; i < Units.Count; i++)
		{
			regularExpression = Units[i];
			nfa2 = regularExpression.GenerateNfa(b);
			nfa3.End.AddMove(nfa2.Start);
			nfa3 = nfa2;
		}
		nfa2.End.AddMove(end);
		return nfa;
	}
}
