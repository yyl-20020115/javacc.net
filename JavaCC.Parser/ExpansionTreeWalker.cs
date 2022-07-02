namespace Javacc.Parser;

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
			foreach (var unit in s.Units)
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
			if (!(la_expansion is Sequence) || (Expansion)((Sequence)la_expansion).Units[0] != exp)
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
			foreach(var u in q.Units)
			{
				PreOrderWalk(u, tree);
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

	
	internal static void postOrderWalk(Expansion exp, TreeWalkerOp walker)
	{
		if (walker.GoDeeper(exp))
		{
			if (exp is Choice c)
			{
				foreach(var ch in c.Choices)
				{
					postOrderWalk(ch, walker);
				}
			}
			else if (exp is Sequence s)
			{
				foreach(var unit in s.Units)
				{
					postOrderWalk(unit, walker);
				}
			}
			else if (exp is OneOrMore o)
			{
				postOrderWalk(((OneOrMore)exp).expansion, walker);
			}
			else if (exp is ZeroOrMore z)
			{
				postOrderWalk(((ZeroOrMore)exp).expansion, walker);
			}
			else if (exp is ZeroOrOne m)
			{
				postOrderWalk(((ZeroOrOne)exp).expansion, walker);
			}
			else if (exp is Lookahead l)
			{
				Expansion la_expansion = ((Lookahead)exp).la_expansion;
				if (!(la_expansion is Sequence) || (Expansion)((Sequence)la_expansion).Units[0] != exp)
				{
					postOrderWalk(la_expansion, walker);
				}
			}
			else if (exp is TryBlock b)
			{
				postOrderWalk(((TryBlock)exp).exp, walker);
			}
			else if (exp is RChoice rc)
			{
				foreach(var ec in rc.Choices)
                {
					postOrderWalk(ec, walker);
				}
			}
			else if (exp is RSequence rs)
			{
				foreach(var un in rs.Units)
				{
					postOrderWalk(un, walker);
				}
			}
			else if (exp is ROneOrMore ro)
			{
				postOrderWalk(((ROneOrMore)exp).RegExpr, walker);
			}
			else if (exp is RZeroOrMore rz)
			{
				postOrderWalk(((RZeroOrMore)exp).regexpr, walker);
			}
			else if (exp is RZeroOrOne rm)
			{
				postOrderWalk(((RZeroOrOne)exp).regexpr, walker);
			}
			else if (exp is RRepetitionRange rr)
			{
				postOrderWalk(((RRepetitionRange)exp).regexpr, walker);
			}
		}
		walker.Action(exp);
	}

	
	private ExpansionTreeWalker()
	{
	}
}
