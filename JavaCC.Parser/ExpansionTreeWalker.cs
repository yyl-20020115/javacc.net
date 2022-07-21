namespace JavaCC.Parser;

public class ExpansionTreeWalker
{
    public static void PreOrderWalk(Expansion exp, TreeWalkerOp tree)
    {
        tree.Action(exp);
        if (!tree.GoDeeper(exp))
        {
            return;
        }
        if (exp is Choice c)
        {
            foreach (var choice in c.Choices)
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
            PreOrderWalk(z.Expansion, tree);
        }
        else if (exp is ZeroOrMore o)
        {
            PreOrderWalk(o.Expansion, tree);
        }
        else if (exp is ZeroOrOne m)
        {
            PreOrderWalk(m.Expansion, tree);
        }
        else if (exp is Lookahead l)
        {
            var la_expansion = l.LaExpansion;
            if (la_expansion is not Sequence || (Expansion)((Sequence)la_expansion).Units[0] != exp)
            {
                PreOrderWalk(la_expansion, tree);
            }
        }
        else if (exp is TryBlock b)
        {
            PreOrderWalk(b.Expression, tree);
        }
        else if (exp is RChoice r)
        {
            foreach (Expansion e in r.Choices)
            {
                PreOrderWalk(e, tree);
            }
        }
        else if (exp is RSequence q)
        {
            foreach (var u in q.Units)
            {
                PreOrderWalk(u, tree);
            }
        }
        else if (exp is ROneOrMore ro)
        {
            PreOrderWalk(ro.RegExpr, tree);
        }
        else if (exp is RZeroOrMore rz)
        {
            PreOrderWalk(rz.Regexpr, tree);
        }
        else if (exp is RZeroOrOne rm)
        {
            PreOrderWalk(rm.Regexpr, tree);
        }
        else if (exp is RRepetitionRange rr)
        {
            PreOrderWalk(rr.Regexpr, tree);
        }
    }
    public static void PostOrderWalk(Expansion exp, TreeWalkerOp walker)
    {
        if (walker.GoDeeper(exp))
        {
            if (exp is Choice c)
            {
                foreach (var ch in c.Choices)
                {
                    PostOrderWalk(ch, walker);
                }
            }
            else if (exp is Sequence s)
            {
                foreach (var unit in s.Units)
                {
                    PostOrderWalk(unit, walker);
                }
            }
            else if (exp is OneOrMore o)
            {
                PostOrderWalk(o.Expansion, walker);
            }
            else if (exp is ZeroOrMore z)
            {
                PostOrderWalk(z.Expansion, walker);
            }
            else if (exp is ZeroOrOne m)
            {
                PostOrderWalk(m.Expansion, walker);
            }
            else if (exp is Lookahead l)
            {
                Expansion la_expansion = l.LaExpansion;
                if (la_expansion is not Sequence || (Expansion)((Sequence)la_expansion).Units[0] != exp)
                {
                    PostOrderWalk(la_expansion, walker);
                }
            }
            else if (exp is TryBlock b)
            {
                PostOrderWalk(b.Expression, walker);
            }
            else if (exp is RChoice rc)
            {
                foreach (var ec in rc.Choices)
                {
                    PostOrderWalk(ec, walker);
                }
            }
            else if (exp is RSequence rs)
            {
                foreach (var un in rs.Units)
                {
                    PostOrderWalk(un, walker);
                }
            }
            else if (exp is ROneOrMore ro)
            {
                PostOrderWalk(ro.RegExpr, walker);
            }
            else if (exp is RZeroOrMore rz)
            {
                PostOrderWalk(rz.Regexpr, walker);
            }
            else if (exp is RZeroOrOne rm)
            {
                PostOrderWalk(rm.Regexpr, walker);
            }
            else if (exp is RRepetitionRange rr)
            {
                PostOrderWalk(rr.Regexpr, walker);
            }
        }
        walker.Action(exp);
    }
}
