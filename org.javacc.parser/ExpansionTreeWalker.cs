namespace org.javacc.parser;

public sealed class ExpansionTreeWalker
{
	
	internal static void preOrderWalk(Expansion P_0, TreeWalkerOp P_1)
	{
		P_1.Action(P_0);
		if (!P_1.GoDeeper(P_0))
		{
			return;
		}
		if (P_0 is Choice c)
		{
			foreach(var choice in c.choices)
			{
				preOrderWalk(choice, P_1);
			}
		}
		else if (P_0 is Sequence s)
		{
			foreach (var unit in s.units)
			{
				preOrderWalk(unit, P_1);
			}
		}
		else if (P_0 is OneOrMore z)
		{
			preOrderWalk(z.expansion, P_1);
		}
		else if (P_0 is ZeroOrMore o)
		{
			preOrderWalk(o.expansion, P_1);
		}
		else if (P_0 is ZeroOrOne m)
		{
			preOrderWalk(m.expansion, P_1);
		}
		else if (P_0 is Lookahead)
		{
			Expansion la_expansion = ((Lookahead)P_0).la_expansion;
			if (!(la_expansion is Sequence) || (Expansion)((Sequence)la_expansion).units[0] != P_0)
			{
				preOrderWalk(la_expansion, P_1);
			}
		}
		else if (P_0 is TryBlock b)
		{
			preOrderWalk(b.exp, P_1);
		}
		else if (P_0 is RChoice r)
		{
			Enumeration enumeration = ((RChoice)P_0).choices.elements();
			while (enumeration.hasMoreElements())
			{
				preOrderWalk((Expansion)enumeration.nextElement(), P_1);
			}
		}
		else if (P_0 is RSequence q)
		{
			Enumeration enumeration = ((RSequence)P_0).units.elements();
			while (enumeration.hasMoreElements())
			{
				preOrderWalk((Expansion)enumeration.nextElement(), P_1);
			}
		}
		else if (P_0 is ROneOrMore ro)
		{
			preOrderWalk(((ROneOrMore)P_0).regexpr, P_1);
		}
		else if (P_0 is RZeroOrMore rz)
		{
			preOrderWalk(((RZeroOrMore)P_0).regexpr, P_1);
		}
		else if (P_0 is RZeroOrOne rm)
		{
			preOrderWalk(((RZeroOrOne)P_0).regexpr, P_1);
		}
		else if (P_0 is RRepetitionRange rr)
		{
			preOrderWalk(((RRepetitionRange)P_0).regexpr, P_1);
		}
	}

	
	internal static void postOrderWalk(Expansion P_0, TreeWalkerOp P_1)
	{
		if (P_1.GoDeeper(P_0))
		{
			if (P_0 is Choice c)
			{
				Enumeration enumeration = ((Choice)P_0).choices.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is Sequence s)
			{
				Enumeration enumeration = ((Sequence)P_0).units.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is OneOrMore o)
			{
				postOrderWalk(((OneOrMore)P_0).expansion, P_1);
			}
			else if (P_0 is ZeroOrMore z)
			{
				postOrderWalk(((ZeroOrMore)P_0).expansion, P_1);
			}
			else if (P_0 is ZeroOrOne m)
			{
				postOrderWalk(((ZeroOrOne)P_0).expansion, P_1);
			}
			else if (P_0 is Lookahead l)
			{
				Expansion la_expansion = ((Lookahead)P_0).la_expansion;
				if (!(la_expansion is Sequence) || (Expansion)((Sequence)la_expansion).units[0] != P_0)
				{
					postOrderWalk(la_expansion, P_1);
				}
			}
			else if (P_0 is TryBlock b)
			{
				postOrderWalk(((TryBlock)P_0).exp, P_1);
			}
			else if (P_0 is RChoice rc)
			{
				Enumeration enumeration = ((RChoice)P_0).choices.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is RSequence rs)
			{
				Enumeration enumeration = ((RSequence)P_0).units.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is ROneOrMore ro)
			{
				postOrderWalk(((ROneOrMore)P_0).regexpr, P_1);
			}
			else if (P_0 is RZeroOrMore rz)
			{
				postOrderWalk(((RZeroOrMore)P_0).regexpr, P_1);
			}
			else if (P_0 is RZeroOrOne rm)
			{
				postOrderWalk(((RZeroOrOne)P_0).regexpr, P_1);
			}
			else if (P_0 is RRepetitionRange rr)
			{
				postOrderWalk(((RRepetitionRange)P_0).regexpr, P_1);
			}
		}
		P_1.Action(P_0);
	}

	
	private ExpansionTreeWalker()
	{
	}
}
