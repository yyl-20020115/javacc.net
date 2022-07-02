using System.Collections;
using System.Collections.Generic;

namespace Javacc.Parser;

public class RRepetitionRange : RegularExpression
{
	public RegularExpression regexpr;

	public int min;

	public int max;

	public bool hasMax;

		
	public RRepetitionRange()
	{
		min = 0;
		max = -1;
	}

	
	public override Nfa GenerateNfa(bool b)
	{
		var vector = new List<RegularExpression>();
		int i;
		for (i = 0; i < min; i++)
		{
			vector.Add(regexpr);
		}
		if (hasMax && max == -1)
		{
			RZeroOrMore rZeroOrMore = new RZeroOrMore();
			rZeroOrMore.regexpr = regexpr;
			vector.Add(rZeroOrMore);
		}
		while (true)
		{
			int num = i;
			i++;
			if (num >= max)
			{
				break;
			}
			RZeroOrOne rZeroOrOne = new RZeroOrOne();
			rZeroOrOne.regexpr = regexpr;
			vector.Add(rZeroOrOne);
		}
		RSequence rSequence = new RSequence(vector);
		Nfa result = rSequence.GenerateNfa(b);
		
		return result;
	}
}
