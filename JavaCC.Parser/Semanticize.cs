namespace JavaCC.Parser;
using System;
using System.Collections.Generic;

public class Semanticize : JavaCCGlobals
{
    public class EmptyChecker : JavaCCGlobals, TreeWalkerOp
    {
        public EmptyChecker()
        {
        }

        public virtual bool GoDeeper(Expansion exp) 
            => exp is not RegularExpression;


        public virtual void Action(Expansion exp)
        {
            if (exp is OneOrMore o)
            {
                if (EmptyExpansionExists(o.Expansion))
                {
                    JavaCCErrors.Semantic_Error(exp, "Expansion within \"(...)+\" can be matched by empty string.");
                }
            }
            else if (exp is ZeroOrMore z)
            {
                if (EmptyExpansionExists(z.Expansion))
                {
                    JavaCCErrors.Semantic_Error(exp, "Expansion within \"(...)*\" can be matched by empty string.");
                }
            }
            else if (exp is ZeroOrOne t && EmptyExpansionExists(t.Expansion))
            {
                JavaCCErrors.Semantic_Error(exp, "Expansion within \"(...)?\" can be matched by empty string.");
            }
        }
    }


    public class FixRJustNames : JavaCCGlobals, TreeWalkerOp
    {
        public RegularExpression Root;

        public FixRJustNames()
        {
        }

        public virtual bool GoDeeper(Expansion exp) => true;


        public virtual void Action(Expansion exp)
        {
            if (exp is RJustName rJustName)
            {
                if (!NamedTokensTable.TryGetValue(rJustName.Label, out var regularExpression))
                {
                    JavaCCErrors.Semantic_Error(exp, ("Undefined lexical token name \"") + (rJustName.Label) + ("\".")
                        );
                    return;
                }
                if (rJustName == Root && !rJustName.TpContext.IsExplicit && regularExpression.PrivateRexp)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Token name \"") + (rJustName.Label) + ("\" refers to a private ")
                        + ("(with a #) regular expression.")
                        );
                    return;
                }
                if (rJustName == Root && !rJustName.TpContext.IsExplicit && regularExpression.TpContext.Kind != 0)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Token name \"") + (rJustName.Label) + ("\" refers to a non-token ")
                        + ("(SKIP, MORE, IGNORE_IN_BNF) regular expression.")
                        );
                    return;
                }
                rJustName.Ordinal = regularExpression.Ordinal;
                rJustName.RegExpr = regularExpression;
            }
        }

    }


    public class LookaheadChecker : JavaCCGlobals, TreeWalkerOp
    {
        public LookaheadChecker()
        {
        }

        internal static bool ImplicitLA(Expansion exp) 
            => exp is not Sequence sequence || sequence.Units[0] is not Lookahead lookahead || !lookahead.IsExplicit;

        public virtual bool GoDeeper(Expansion exp) => exp switch
        {
            RegularExpression => false,
            Lookahead => false,
            _ => true
        };


        public virtual void Action(Expansion exp)
        {
            if (exp is Choice choice)
            {
                if (Options.Lookahead == 1 || Options.ForceLaCheck)
                {
                    LookaheadCalc.ChoiceCalc(choice);
                }
            }
            else if (exp is OneOrMore oneOrMore)
            {
                if (Options.ForceLaCheck || (ImplicitLA(oneOrMore.Expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.EBNFCalc(oneOrMore, oneOrMore.Expansion);
                }
            }
            else if (exp is ZeroOrMore zeroOrMore)
            {
                if (Options.ForceLaCheck || (ImplicitLA(zeroOrMore.Expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.EBNFCalc(zeroOrMore, zeroOrMore.Expansion);
                }
            }
            else if (exp is ZeroOrOne zeroOrOne)
            {
                if (Options.ForceLaCheck || (ImplicitLA(zeroOrOne.Expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.EBNFCalc(zeroOrOne, zeroOrOne.Expansion);
                }
            }
        }
    }


    public class LookaheadFixer : JavaCCGlobals, TreeWalkerOp
    {
        public LookaheadFixer()
        {
        }

        public virtual bool GoDeeper(Expansion exp) => exp is not RegularExpression;


        public virtual void Action(Expansion exp)
        {
            if (exp is not Sequence sequence || exp.Parent is Choice || exp.Parent is ZeroOrMore || exp.Parent is OneOrMore || exp.Parent is ZeroOrOne)
            {
                return;
            }
            Lookahead lookahead = (Lookahead)sequence.Units[0];
            if (!lookahead.IsExplicit)
            {
                return;
            }
            Choice choice = new ()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                Parent = sequence
            };
            Sequence sequence2 = new ()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                Parent = choice
            };
            sequence2.Units.Add(lookahead);
            lookahead.Parent = sequence2;
            Action action = new()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                Parent = sequence2
            };
            sequence2.Units.Add(action);
            choice.Choices.Add(sequence2);
            if (lookahead.amount != 0)
            {
                if (lookahead.ActionTokens.Count != 0)
                {
                    JavaCCErrors.Warning(lookahead, "Encountered LOOKAHEAD(...) at a non-choice location.  Only semantic lookahead will be considered here.");
                }
                else
                {
                    JavaCCErrors.Warning(lookahead, "Encountered LOOKAHEAD(...) at a non-choice location.  This will be ignored.");
                }
            }
            Lookahead lookahead2 = new()
            {
                IsExplicit = false,
                Line = lookahead.Line,
                Column = lookahead.Column,
                Parent = sequence
            };
            lookahead.LaExpansion = new REndOfFile();
            lookahead2.LaExpansion = new REndOfFile();
            sequence.Units[0] = lookahead2;
            sequence.Units.Insert(1, choice);
        }
    }


    public class ProductionDefinedChecker : JavaCCGlobals, TreeWalkerOp
    {
        public ProductionDefinedChecker()
        {
        }

        public virtual bool GoDeeper(Expansion exp) => exp is not RegularExpression;

        public virtual void Action(Expansion exp)
        {
            if (exp is NonTerminal nonTerminal && ProductionTable.TryGetValue(nonTerminal.Name,out var normalProduction))
            {
                nonTerminal.Production = normalProduction;
                if (normalProduction == null)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Non-terminal ") + (nonTerminal.Name) + (" has not been defined."));
                }
                else
                {
                    nonTerminal.Production.Parents.Add(nonTerminal);
                }
            }
        }
    }

    public static readonly List<List<RegExprSpec>> removeList = new();

    public static readonly List<RegExprSpec> itemList = new();

    public static RegularExpression other = null;

    protected static string loopString = null;


    public static bool EmptyExpansionExists(Expansion e)
    {
        switch (e)
        {
            case NonTerminal terminal:
                return terminal.Production.EmptyPossible;
            case Action:
                return true;
            case RegularExpression:
                return false;
            case OneOrMore more:
                return EmptyExpansionExists(more.Expansion);
            case ZeroOrMore:
            case ZeroOrOne:
                return true;
            case Lookahead:
                return true;
            case Choice c:
                {
                    foreach (var d in c.Choices)
                    {
                        if (EmptyExpansionExists(d))
                        {
                            return true;
                        }
                    }
                    return false;
                }

            case Sequence s:
                {
                    foreach (var t in s.Units)
                    {
                        if (!EmptyExpansionExists(t))
                        {
                            return false;
                        }
                    }
                    return true;
                }

            case TryBlock b:
                return EmptyExpansionExists(b.Expression);
            default:
                return false;
        }
    }


    public static void Start()
    {
        if (JavaCCErrors.ErrorCount != 0)
        {
            throw new MetaParseException();
        }
        if (Options.Lookahead > 1 && !Options.ForceLaCheck && Options.SanityCheck)
        {
            JavaCCErrors.Warning("Lookahead adequacy checking not being performed since option LOOKAHEAD is more than 1.  HashSet<object> option FORCE_LA_CHECK to true to force checking.");
        }
        foreach (var b in BNFProductions)
        {
            ExpansionTreeWalker.PostOrderWalk(b.Expansion, new LookaheadFixer());
        }
        foreach(var normalProduction in BNFProductions)
        {
            if (ProductionTable.ContainsKey(normalProduction.Lhs))
            {
                JavaCCErrors.Semantic_Error(normalProduction, (normalProduction.Lhs) 
                    + (" occurs on the left hand side of more than one production."));
            }
            ProductionTable.Add(normalProduction.Lhs, normalProduction);
        }
        foreach (var normalProduction in BNFProductions)
        {
            ExpansionTreeWalker.PreOrderWalk(normalProduction.Expansion, new ProductionDefinedChecker());
        }
        foreach (var tokenProduction in RexprList)
        {
            foreach (var regExprSpec in tokenProduction.Respecs)
            {
                if (regExprSpec.NextState != null && !Lexstate_S2I.TryGetValue(
                    regExprSpec.NextState,out var _))
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.NsToken, ("Lexical state \"") 
                        + (regExprSpec.NextState) + ("\" has not been defined.")
                        );
                }
                if (regExprSpec.Rexp is REndOfFile)
                {
                    if (tokenProduction.LexStates != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.Rexp, "EOF action/state change must be specified for all states, i.e., <*>TOKEN:.");
                    }
                    if (tokenProduction.Kind != 0)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.Rexp, "EOF action/state change can be specified only in a TOKEN specification.");
                    }
                    if (NextStateForEof != null || ActForEof != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.Rexp, "Duplicate action/state change specification for <EOF>.");
                    }
                    ActForEof = regExprSpec.Action;
                    NextStateForEof = regExprSpec.NextState;
                    PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                }
                else if (tokenProduction.IsExplicit && Options.UserTokenManager)
                {
                    JavaCCErrors.Warning(regExprSpec.Rexp, "Ignoring regular expression specification since option USER_TOKEN_MANAGER has been set to true.");
                }
                else if (tokenProduction.IsExplicit && !Options.UserTokenManager && regExprSpec.Rexp is RJustName)
                {
                    JavaCCErrors.Warning(regExprSpec.Rexp, ("Ignoring free-standing regular expression reference.  If you really want this, you must give it a different label as <NEWLABEL:<") + (regExprSpec.Rexp.Label) + (">>.")
                        );
                    PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                }
                else if (!tokenProduction.IsExplicit && regExprSpec.Rexp.PrivateRexp)
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.Rexp, "Private (#) regular expression cannot be defined within grammar productions.");
                }
            }
        }
        RemovePreparedItems();
        foreach(var tokenProduction in RexprList)
        {
            var respecs = tokenProduction.Respecs;
            foreach(var regExprSpec in respecs)
            {
                if (regExprSpec.Rexp is not RJustName && !string.Equals(regExprSpec.Rexp.Label, ""))
                {
                    var label = regExprSpec.Rexp.Label;
                    bool b = NamedTokensTable.ContainsKey(label);
                    
                    NamedTokensTable.Add(label, regExprSpec.Rexp);
                    if (b)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.Rexp, ("Multiply defined lexical token name \"") + (label) + ("\"."));
                    }
                    else
                    {
                        OrderedNamedTokens.Add(regExprSpec.Rexp);
                    }
                    if (Lexstate_S2I.ContainsKey(label))
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.Rexp, ("Lexical token name \"") + (label) + ("\" is the same as ")
                            + ("that of a lexical state."));
                    }
                }
            }
        }
        TokenCount = 1;

        foreach(var tokenProduction in RexprList)
        {
            var respecs = tokenProduction.Respecs;

            if (tokenProduction.LexStates == null)
            {
                tokenProduction.LexStates = new string[Lexstate_I2S.Count];
                int n = 0;
                foreach(var pair in Lexstate_I2S)
                {
                    var lexStates = tokenProduction.LexStates;
                    int n2 = n;
                    n++;
                    lexStates[n2] = (string)pair.Value;
                }
            }
            var array = new Dictionary<string, Dictionary<string, RegularExpression>>[tokenProduction.LexStates.Length];
            for (int i = 0; i < tokenProduction.LexStates.Length; i++)
            {
                SimpleTokensTable.TryGetValue(tokenProduction.LexStates[i],out var d);
                array[i] = d;
            }

            foreach(var r2 in respecs)
            {
                if (r2.Rexp is RStringLiteral rStringLiteral)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (!array[j].TryGetValue(rStringLiteral.Image.ToUpper(),out var dict))
                        {
                            if (rStringLiteral.Ordinal == 0)
                            {
                                rStringLiteral.Ordinal = TokenCount++;
                            }
                            dict = new();
                            dict.Add(rStringLiteral.Image, rStringLiteral);
                            array[j].Add((rStringLiteral.Image).ToUpper(), dict);
                            continue;
                        }
                        if (HasIgnoreCase(dict, rStringLiteral.Image))
                        {
                            if (!rStringLiteral.TpContext.IsExplicit)
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("String \"") + (rStringLiteral.Image) + ("\" can never be matched ")
                                    + ("due to presence of more general (IGNORE_CASE) regular expression ")
                                    + ("at line ")
                                    + (other.Line)
                                    + (", column ")
                                    + (other.Column)
                                    + (".")
                                    );
                            }
                            else
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.Image) + ("\" ")
                                    + ("can never be matched.")
                                    );
                            }
                            continue;
                        }
                        if (rStringLiteral.TpContext.IgnoreCase)
                        {
                            var str = "";
                            int n = 0;
                            foreach(var pair in dict)
                            {
                                var regularExpression = pair.Value;
                                if (n != 0)
                                {
                                    str = (str) + (",");
                                }
                                str = (str) + (" line ") + (regularExpression.Line);
                                n++;
                            }
                            if (n == 1)
                            {
                                JavaCCErrors.Warning(rStringLiteral, ("String with IGNORE_CASE is partially superceded by string at") + (str) + ("."));
                            }
                            else
                            {
                                JavaCCErrors.Warning(rStringLiteral, ("String with IGNORE_CASE is partially superceded by strings at") + (str) + ("."));
                            }
                            if (rStringLiteral.Ordinal == 0)
                            {
                                rStringLiteral.Ordinal = TokenCount++;
                            }
                            dict.Add(rStringLiteral.Image, rStringLiteral);
                            continue;
                        }
                        if (!dict.TryGetValue(rStringLiteral.Image,out var regularExpression2))
                        {
                            if (rStringLiteral.Ordinal == 0)
                            {
                                rStringLiteral.Ordinal = TokenCount++;
                            }
                            dict.Add(rStringLiteral.Image, rStringLiteral);
                        }
                        else if (tokenProduction.IsExplicit)
                        {
                            if (string.Equals(tokenProduction.LexStates[j], "DEFAULT"))
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.Image) + ("\"."));
                            }
                            else
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.Image) + ("\" in lexical state \"")
                                    + (tokenProduction.LexStates[j])
                                    + ("\"."));
                            }
                        }
                        else if (regularExpression2.TpContext.Kind != 0)
                        {
                            JavaCCErrors.Semantic_Error(rStringLiteral, ("String token \"") + (rStringLiteral.Image) + ("\" has been defined as a \"")
                                + (TokenProduction.KindImage[regularExpression2.TpContext.Kind])
                                + ("\" token."));
                        }
                        else if (regularExpression2.PrivateRexp)
                        {
                            JavaCCErrors.Semantic_Error(rStringLiteral, ("String token \"") + (rStringLiteral.Image) + ("\" has been defined as a private regular expression."));
                        }
                        else
                        {
                            rStringLiteral.Ordinal = regularExpression2.Ordinal;
                            PrepareToRemove(respecs, r2);
                        }
                    }
                }
                else if (r2.Rexp is not RJustName)
                {
                    r2.Rexp.Ordinal = TokenCount++;
                }
                if (r2.Rexp is not RJustName && !string.Equals(r2.Rexp.Label, ""))
                {
                    NamesOfTokens.Add((r2.Rexp.Ordinal), r2.Rexp.Label);
                }
                if (r2.Rexp is not RJustName)
                {
                    RexpsOfTokens.Add((r2.Rexp.Ordinal), r2.Rexp);
                }
            }
        }
        RemovePreparedItems();
        if (!Options.UserTokenManager)
        {
            var fixRJustNames = new FixRJustNames();
            foreach(var t2 in RexprList)
            {
                var r2 = t2.Respecs;
                foreach(var regExprSpec2 in r2)
                {
                    fixRJustNames.Root = regExprSpec2.Rexp;
                    ExpansionTreeWalker.PreOrderWalk(regExprSpec2.Rexp, fixRJustNames);
                    if (regExprSpec2.Rexp is RJustName)
                    {
                        PrepareToRemove(r2, regExprSpec2);
                    }
                }
            }
        }
        RemovePreparedItems();
        if (Options.UserTokenManager)
        {
            foreach(var tokenProduction in RexprList)
            {
                foreach(var regExprSpec in tokenProduction.Respecs)
                {
                    if (regExprSpec.Rexp is RJustName rJustName)
                    {
                        if (!NamedTokensTable.TryGetValue(rJustName.Label, out var regularExpression3))
                        {
                            rJustName.Ordinal = TokenCount++;
                            NamedTokensTable.Add(rJustName.Label, rJustName);
                            OrderedNamedTokens.Add(rJustName);
                            NamesOfTokens.Add((rJustName.Ordinal), rJustName.Label);
                        }
                        else
                        {
                            rJustName.Ordinal = regularExpression3.Ordinal;
                            PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                        }
                    }
                }
            }
        }
        RemovePreparedItems();
        if (Options.UserTokenManager)
        {
            foreach(var tokenProduction in RexprList)
            {
                var respecs = tokenProduction.Respecs;
                foreach(var regExprSpec in respecs)
                {
                    int key = (regExprSpec.Rexp.Ordinal);
                    if (!NamesOfTokens.ContainsKey(key))
                    {
                        JavaCCErrors.Warning(regExprSpec.Rexp, 
                            "Unlabeled regular expression cannot be referred to by user generated token manager.");
                    }
                }
            }
        }
        if (JavaCCErrors.ErrorCount != 0)
        {
            throw new MetaParseException();
        }
        int c = 1;
        while (c != 0)
        {
            c = 0;
            foreach (var normalProduction2 in BNFProductions)
            {
                if (EmptyExpansionExists(normalProduction2.Expansion) && !normalProduction2.EmptyPossible)
                {
                    normalProduction2.EmptyPossible = true;
                    c = 1;
                }
            }
        }
        if (Options.SanityCheck && JavaCCErrors.ErrorCount == 0)
        {
            foreach (var normalProduction2 in BNFProductions)
            {
                ExpansionTreeWalker.PreOrderWalk(normalProduction2.Expansion, new EmptyChecker());
            }
            foreach (var normalProduction2 in BNFProductions)
            {
                AddLeftMost(normalProduction2, normalProduction2.Expansion);
            }
            foreach(var normalProduction2 in BNFProductions)
            {
                if (normalProduction2.WalkStatus == 0)
                {
                    ProdWalk(normalProduction2);
                }
            }
            if (!Options.UserTokenManager)
            {
                foreach(var tp in RexprList)
                {
                    foreach(var regExprSpec2 in tp.Respecs)
                    {
                        var reg3 = regExprSpec2.Rexp;
                        if (reg3.WalkStatus == 0)
                        {
                            reg3.WalkStatus = -1;
                            if (RExpWalk(reg3))
                            {
                                loopString = ("...") + (reg3.Label) + ("... --> ")
                                    + (loopString)
                                    ;
                                JavaCCErrors.Semantic_Error(reg3, ("Loop in regular expression detected: \"") + (loopString) + ("\"")
                                    );
                            }
                            reg3.WalkStatus = 1;
                        }
                    }
                }
            }
            if (JavaCCErrors.ErrorCount == 0)
            {
                foreach(var p in BNFProductions)
                {
                    ExpansionTreeWalker.PreOrderWalk(p.Expansion, new LookaheadChecker());
                }
            }
        }
        if (JavaCCErrors.ErrorCount != 0)
        {
            throw new MetaParseException();
        }
    }


    public new static void ReInit()
    {
        removeList.Clear();
        itemList.Clear();
        other = null;
        loopString = null;
    }


    internal static void PrepareToRemove(List<RegExprSpec> reslist, RegExprSpec res)
    {
        removeList.Add(reslist);
        itemList.Add(res);
    }


    internal static void RemovePreparedItems()
    {
        for (int i = 0; i < removeList.Count; i++)
        {
            var vector = removeList[i];
            vector.Remove(itemList[i]);
        }
        removeList.Clear();
        itemList.Clear();
    }


    public static bool HasIgnoreCase(Dictionary<string, RegularExpression> dict, string str)
    {
        if (dict.TryGetValue(str,out var regularExpression) && !regularExpression.TpContext.IgnoreCase)
        {
            return false;
        }
       
        foreach(var pair in dict)
        {
            regularExpression=pair.Value;
            if (regularExpression.TpContext.IgnoreCase)
            {
                other = regularExpression;
                return true;
            }
        }
        return false;
    }


    private static void AddLeftMost(NormalProduction npleft, Expansion npright)
    {
        if (npright is NonTerminal)
        {
            for (int i = 0; i < npleft.LeIndex; i++)
            {
                if (npleft.LeftExpansions[i] == ((NonTerminal)npright).Production)
                {
                    return;
                }
            }
            if (npleft.LeIndex == npleft.LeftExpansions.Length)
            {
                var array = new NormalProduction[npleft.LeIndex * 2];
                Array.Copy(npleft.LeftExpansions, 0, array, 0, npleft.LeIndex);
                npleft.LeftExpansions = array;
            }
            var leftExpansions = npleft.LeftExpansions;
            int leIndex = npleft.LeIndex;
            npleft.LeIndex = leIndex + 1;
            leftExpansions[leIndex] = ((NonTerminal)npright).Production;
        }
        else if (npright is OneOrMore more2)
        {
            AddLeftMost(npleft, more2.Expansion);
        }
        else if (npright is ZeroOrMore more3)
        {
            AddLeftMost(npleft, more3.Expansion);
        }
        else if (npright is ZeroOrOne one2)
        {
            AddLeftMost(npleft, one2.Expansion);
        }
        else if (npright is Choice pc)
        {
            foreach (var cx in pc.Choices)
            {
                AddLeftMost(npleft, cx);
            }
        }
        else if (npright is Sequence ps)
        {
            foreach (var expansion in ps.Units)
            {
                AddLeftMost(npleft, expansion);
                if (!EmptyExpansionExists(expansion))
                {
                    break;
                }
            }
        }
        else if (npright is TryBlock block)
        {
            AddLeftMost(npleft, block.Expression);
        }
    }


    private static bool ProdWalk(NormalProduction np)
    {
        np.WalkStatus = -1;
        for (int i = 0; i < np.LeIndex; i++)
        {
            if (np.LeftExpansions[i].WalkStatus == -1)
            {
                np.LeftExpansions[i].WalkStatus = -2;
                loopString = (np.Lhs) + ("... --> ") + (np.LeftExpansions[i].Lhs)
                    + ("...")
                    ;
                if (np.WalkStatus == -2)
                {
                    np.WalkStatus = 1;
                    JavaCCErrors.Semantic_Error(np, ("Left recursion detected: \"") + (loopString) + ("\""));
                    return false;
                }
                np.WalkStatus = 1;
                return true;
            }
            if (np.LeftExpansions[i].WalkStatus == 0 && ProdWalk(np.LeftExpansions[i]))
            {
                loopString = (np.Lhs) + ("... --> ") + (loopString)
                    ;
                if (np.WalkStatus == -2)
                {
                    np.WalkStatus = 1;
                    JavaCCErrors.Semantic_Error(np, ("Left recursion detected: \"") + (loopString) + ("\""));
                    return false;
                }
                np.WalkStatus = 1;
                return true;
            }
        }
        np.WalkStatus = 1;
        return false;
    }


    private static bool RExpWalk(RegularExpression exp)
    {
        if (exp is RJustName rJustName)
        {
            if (rJustName.RegExpr.WalkStatus == -1)
            {
                rJustName.RegExpr.WalkStatus = -2;
                loopString = ("...") + (rJustName.RegExpr.Label) + ("...");
                return true;
            }
            if (rJustName.RegExpr.WalkStatus == 0)
            {
                rJustName.RegExpr.WalkStatus = -1;
                if (RExpWalk(rJustName.RegExpr))
                {
                    loopString = ("...") + (rJustName.RegExpr.Label) + ("... --> ")
                        + (loopString);
                    if (rJustName.RegExpr.WalkStatus == -2)
                    {
                        rJustName.RegExpr.WalkStatus = 1;
                        JavaCCErrors.Semantic_Error(rJustName.RegExpr, ("Loop in regular expression detected: \"") + (loopString) + ("\""));
                        return false;
                    }
                    rJustName.RegExpr.WalkStatus = 1;
                    return true;
                }
                rJustName.RegExpr.WalkStatus = 1;
                return false;
            }
        }
        else
        {
            if (exp is RChoice rc)
            {
                foreach (RegularExpression rx in rc.Choices)
                {
                    if (RExpWalk(rx))
                    {
                        return true;
                    }
                }
                return false;
            }
            switch (exp)
            {
                case RSequence rs:
                    {
                        foreach (var rex in rs.Units)
                        {
                            if (RExpWalk(rex))
                            {
                                return true;
                            }
                        }
                        return false;
                    }

                case ROneOrMore more:
                    return RExpWalk(more.RegExpr);
                case RZeroOrMore more1:
                    return RExpWalk(more1.Regexpr);
                case RZeroOrOne one:
                    return RExpWalk(one.Regexpr);
                case RRepetitionRange range:
                    return RExpWalk(range.Regexpr);
            }
        }
        return false;
    }
}
