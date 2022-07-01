namespace org.javacc.parser;

public sealed class ExpansionTreeWalker
{
	
	internal static void PreOrderWalk(Expansion exp, TreeWalkerOp tree)
	{
		tree.Action(exp);
		if (!tree.GoDeeper(exp))
		{
			return;
		}
		if (exp is Choice c)
		{
			foreach(var choice in c.Choices)
			{
				PreOrderWalk(choice, tree);
			}
		}
		else if (exp is Sequence s)
		{
			foreach (var unit in s.units)
			{
				PreOrderWalk(unit, tree);
			}
		}
		else if (exp is OneOrMore z)
		{
			PreOrderWalk(z.expansion, tree);
		}
		else if (exp is ZeroOrMore o)
		{
			PreOrderWalk(o.expansion, tree);
		}
		else if (exp is ZeroOrOne m)
		{
			PreOrderWalk(m.expansion, tree);
		}
		else if (exp is Lookahead l)
		{
			var la_expansion = l.la_expansion;
			if (!(la_expansion is Sequence) || (Expansion)((Sequence)la_expansion).units[0] != exp)
			{
				PreOrderWalk(la_expansion, tree);
			}
		}
		else if (exp is TryBlock b)
		{
			PreOrderWalk(b.exp, tree);
		}
		else if (exp is RChoice r)
		{
			foreach(Expansion e in r.Choices)
			{
				PreOrderWalk(e, tree);
			}
		}
		else if (exp is RSequence q)
		{
			Enumeration enumeration = ((RSequence)exp).Units.elements();
			while (enumeration.hasMoreElements())
			{
				PreOrderWalk((Expansion)enumeration.nextElement(), tree);
			}
		}
		else if (exp is ROneOrMore ro)
		{
			PreOrderWalk(((ROneOrMore)exp).RegExpr, tree);
		}
		else if (exp is RZeroOrMore rz)
		{
			PreOrderWalk(((RZeroOrMore)exp).regexpr, tree);
		}
		else if (exp is RZeroOrOne rm)
		{
			PreOrderWalk(((RZeroOrOne)exp).regexpr, tree);
		}
		else if (exp is RRepetitionRange rr)
		{
			PreOrderWalk(((RRepetitionRange)exp).regexpr, tree);
		}
	}

	
	internal static void postOrderWalk(Expansion P_0, TreeWalkerOp P_1)
	{
		if (P_1.GoDeeper(P_0))
		{
			if (P_0 is Choice c)
			{
				Enumeration enumeration = ((Choice)P_0).Choices.elements();
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
				Enumeration enumeration = ((RChoice)P_0).Choices.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is RSequence rs)
			{
				Enumeration enumeration = ((RSequence)P_0).Units.elements();
				while (enumeration.hasMoreElements())
				{
					postOrderWalk((Expansion)enumeration.nextElement(), P_1);
				}
			}
			else if (P_0 is ROneOrMore ro)
			{
				postOrderWalk(((ROneOrMore)P_0).RegExpr, P_1);
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
