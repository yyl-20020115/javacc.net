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

        public virtual bool GoDeeper(Expansion exp) => exp is not RegularExpression;


        public virtual void Action(Expansion exp)
        {
            if (exp is OneOrMore o)
            {
                if (EmptyExpansionExists(o.expansion))
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

        internal FixRJustNames()
        {
        }

        public virtual bool GoDeeper(Expansion exp) => true;


        public virtual void Action(Expansion exp)
        {
            if (exp is RJustName rJustName)
            {
                if (!JavaCCGlobals.named_tokens_table.TryGetValue(rJustName.label, out var regularExpression))
                {
                    JavaCCErrors.Semantic_Error(exp, ("Undefined lexical token name \"") + (rJustName.label) + ("\".")
                        );
                    return;
                }
                if (rJustName == Root && !rJustName.tpContext.IsExplicit && regularExpression.private_rexp)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Token name \"") + (rJustName.label) + ("\" refers to a private ")
                        + ("(with a #) regular expression.")
                        );
                    return;
                }
                if (rJustName == Root && !rJustName.tpContext.IsExplicit && regularExpression.tpContext.Kind != 0)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Token name \"") + (rJustName.label) + ("\" refers to a non-token ")
                        + ("(SKIP, MORE, IGNORE_IN_BNF) regular expression.")
                        );
                    return;
                }
                rJustName.ordinal = regularExpression.ordinal;
                rJustName.regexpr = regularExpression;
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

        public virtual bool GoDeeper(Expansion exp)
        {
            if (exp is RegularExpression)
            {
                return false;
            }
            if (exp is Lookahead)
            {
                return false;
            }
            return true;
        }


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
                if (Options.ForceLaCheck || (ImplicitLA(oneOrMore.expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.EBNFCalc(oneOrMore, oneOrMore.expansion);
                }
            }
            else if (exp is ZeroOrMore zeroOrMore)
            {
                if (Options.ForceLaCheck || (ImplicitLA(zeroOrMore.Expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.EBNFCalc(zeroOrMore, zeroOrMore.Expansion);
                }
            }
            else if (exp is ZeroOrOne)
            {
                ZeroOrOne zeroOrOne = (ZeroOrOne)exp;
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
            if (exp is not Sequence || exp.parent is Choice || exp.parent is ZeroOrMore || exp.parent is OneOrMore || exp.parent is ZeroOrOne)
            {
                return;
            }
            Sequence sequence = (Sequence)exp;
            Lookahead lookahead = (Lookahead)sequence.Units[0];
            if (!lookahead.IsExplicit)
            {
                return;
            }
            Choice choice = new ()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                parent = sequence
            };
            Sequence sequence2 = new ()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                parent = choice
            };
            sequence2.Units.Add(lookahead);
            lookahead.parent = sequence2;
            Action action = new()
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                parent = sequence2
            };
            sequence2.Units.Add(action);
            choice.Choices.Add(sequence2);
            if (lookahead.amount != 0)
            {
                if (lookahead.action_tokens.Count != 0)
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
                parent = sequence
            };
            lookahead.la_expansion = new REndOfFile();
            lookahead2.la_expansion = new REndOfFile();
            sequence.Units[0] = lookahead2;
            sequence.Units.Insert(1, choice);
        }


        static LookaheadFixer()
        {

        }
    }


    internal class ProductionDefinedChecker : JavaCCGlobals, TreeWalkerOp
    {


        internal ProductionDefinedChecker()
        {
        }

        public virtual bool GoDeeper(Expansion P_0)
        {
            if (P_0 is RegularExpression)
            {
                return false;
            }
            return true;
        }


        public virtual void Action(Expansion exp)
        {
            if (exp is NonTerminal nonTerminal && JavaCCGlobals.Production_table.TryGetValue(nonTerminal.name,out var normalProduction))
            {
                NonTerminal nonTerminal2 = nonTerminal;
                nonTerminal2.prod = normalProduction;
                if (normalProduction == null)
                {
                    JavaCCErrors.Semantic_Error(exp, ("Non-terminal ") + (nonTerminal.name) + (" has not been defined.")
                        );
                }
                else
                {
                    nonTerminal.prod.parents.Add(nonTerminal);
                }
            }
        }


        static ProductionDefinedChecker()
        {

        }
    }

    public static List<List<RegExprSpec>> removeList = new();

    public static List<RegExprSpec> itemList = new();

    public static RegularExpression other;

    private static string loopString;


    public static bool EmptyExpansionExists(Expansion e)
    {
        if (e is NonTerminal terminal)
        {
            return terminal.prod.emptyPossible;
        }
        if (e is Action)
        {
            return true;
        }
        if (e is RegularExpression)
        {
            return false;
        }
        if (e is OneOrMore)
        {
            bool result = EmptyExpansionExists(((OneOrMore)e).expansion);

            return result;
        }
        if (e is ZeroOrMore || e is ZeroOrOne)
        {
            return true;
        }
        if (e is Lookahead)
        {
            return true;
        }
        if (e is Choice c)
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
        if (e is Sequence s)
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
        if (e is TryBlock b)
        {
            return EmptyExpansionExists(b.Expression);
        }
        return false;
    }


    public static void start()
    {
        if (JavaCCErrors._Error_Count != 0)
        {

            throw new MetaParseException();
        }
        if (Options.Lookahead > 1 && !Options.ForceLaCheck && Options.SanityCheck)
        {
            JavaCCErrors.Warning("Lookahead adequacy checking not being performed since option LOOKAHEAD is more than 1.  HashSet<object> option FORCE_LA_CHECK to true to force checking.");
        }
        foreach (var b in JavaCCGlobals.BNFProductions)
        {
            ExpansionTreeWalker.postOrderWalk(b.Expansion, new LookaheadFixer());
        }
        foreach(var normalProduction in JavaCCGlobals.BNFProductions)
        {
            if (JavaCCGlobals.Production_table.ContainsKey(normalProduction.lhs))
            {
                JavaCCErrors.Semantic_Error(normalProduction, (normalProduction.lhs) 
                    + (" occurs on the left hand side of more than one production."));
            }
            JavaCCGlobals.Production_table.Add(normalProduction.lhs, normalProduction);
        }
        foreach (var normalProduction in JavaCCGlobals.BNFProductions)
        {
            ExpansionTreeWalker.PreOrderWalk(normalProduction.Expansion, new ProductionDefinedChecker());
        }
        foreach (var tokenProduction in JavaCCGlobals.rexprlist)
        {
            foreach (var regExprSpec in tokenProduction.Respecs)
            {
                if (regExprSpec.nextState != null && !JavaCCGlobals.lexstate_S2I.TryGetValue(
                    regExprSpec.nextState,out var _))
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.nsTok, ("Lexical state \"") 
                        + (regExprSpec.nextState) + ("\" has not been defined.")
                        );
                }
                if (regExprSpec.rexp is REndOfFile)
                {
                    if (tokenProduction.LexStates != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, "EOF action/state change must be specified for all states, i.e., <*>TOKEN:.");
                    }
                    if (tokenProduction.Kind != 0)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, "EOF action/state change can be specified only in a TOKEN specification.");
                    }
                    if (JavaCCGlobals.nextStateForEof != null || JavaCCGlobals.actForEof != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, "Duplicate action/state change specification for <EOF>.");
                    }
                    JavaCCGlobals.actForEof = regExprSpec.act;
                    JavaCCGlobals.nextStateForEof = regExprSpec.nextState;
                    PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                }
                else if (tokenProduction.IsExplicit && Options.UserTokenManager)
                {
                    JavaCCErrors.Warning(regExprSpec.rexp, "Ignoring regular expression specification since option USER_TOKEN_MANAGER has been set to true.");
                }
                else if (tokenProduction.IsExplicit && !Options.UserTokenManager && regExprSpec.rexp is RJustName)
                {
                    JavaCCErrors.Warning(regExprSpec.rexp, ("Ignoring free-standing regular expression reference.  If you really want this, you must give it a different label as <NEWLABEL:<") + (regExprSpec.rexp.label) + (">>.")
                        );
                    PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                }
                else if (!tokenProduction.IsExplicit && regExprSpec.rexp.private_rexp)
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.rexp, "Private (#) regular expression cannot be defined within grammar productions.");
                }
            }
        }
        RemovePreparedItems();
        foreach(var tokenProduction in JavaCCGlobals.rexprlist)
        {
            var respecs = tokenProduction.Respecs;
            foreach(var regExprSpec in respecs)
            {
                if (regExprSpec.rexp is not RJustName && !string.Equals(regExprSpec.rexp.label, ""))
                {
                    string label = regExprSpec.rexp.label;
                    bool obj = JavaCCGlobals.named_tokens_table.ContainsKey(label);
                    
                    JavaCCGlobals.named_tokens_table.Add(label, regExprSpec.rexp);
                    if (obj)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, ("Multiply defined lexical token name \"") + (label) + ("\".")
                            );
                    }
                    else
                    {
                        JavaCCGlobals.ordered_named_tokens.Add(regExprSpec.rexp);
                    }
                    if (JavaCCGlobals.lexstate_S2I.ContainsKey(label))
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, ("Lexical token name \"") + (label) + ("\" is the same as ")
                            + ("that of a lexical state.")
                            );
                    }
                }
            }
        }
        JavaCCGlobals.tokenCount = 1;

        foreach(var tokenProduction in JavaCCGlobals.rexprlist)
        {
            var respecs = tokenProduction.Respecs;

            if (tokenProduction.LexStates == null)
            {
                tokenProduction.LexStates = new string[JavaCCGlobals.lexstate_I2S.Count];
                int num = 0;
                foreach(var pair in JavaCCGlobals.lexstate_I2S)
                {
                    string[] lexStates = tokenProduction.LexStates;
                    int num2 = num;
                    num++;
                    lexStates[num2] = (string)pair.Value;
                }
            }
            var array = new Dictionary<string, Dictionary<string, RegularExpression>>[tokenProduction.LexStates.Length];
            for (int i = 0; i < tokenProduction.LexStates.Length; i++)
            {
                JavaCCGlobals.simple_tokens_table.TryGetValue(tokenProduction.LexStates[i],out var d);
                array[i] = d;
            }

            foreach(var regExprSpec2 in respecs)
            {
                if (regExprSpec2.rexp is RStringLiteral rStringLiteral)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (!array[j].TryGetValue(rStringLiteral.image.ToUpper(),out var dict))
                        {
                            if (rStringLiteral.ordinal == 0)
                            {
                                rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
                            }
                            dict = new();
                            dict.Add(rStringLiteral.image, rStringLiteral);
                            array[j].Add((rStringLiteral.image).ToUpper(), dict);
                            continue;
                        }
                        if (HasIgnoreCase(dict, rStringLiteral.image))
                        {
                            if (!rStringLiteral.tpContext.IsExplicit)
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("String \"") + (rStringLiteral.image) + ("\" can never be matched ")
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
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.image) + ("\" ")
                                    + ("can never be matched.")
                                    );
                            }
                            continue;
                        }
                        if (rStringLiteral.tpContext.IgnoreCase)
                        {
                            string str = "";
                            int num3 = 0;
                            foreach(var pair in dict)
                            {
                                RegularExpression regularExpression = pair.Value;
                                if (num3 != 0)
                                {
                                    str = (str) + (",");
                                }
                                str = (str) + (" line ") + (regularExpression.Line)
                                    ;
                                num3++;
                            }
                            if (num3 == 1)
                            {
                                JavaCCErrors.Warning(rStringLiteral, ("String with IGNORE_CASE is partially superceded by string at") + (str) + (".")
                                    );
                            }
                            else
                            {
                                JavaCCErrors.Warning(rStringLiteral, ("String with IGNORE_CASE is partially superceded by strings at") + (str) + (".")
                                    );
                            }
                            if (rStringLiteral.ordinal == 0)
                            {
                                rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
                            }
                            dict.Add(rStringLiteral.image, rStringLiteral);
                            continue;
                        }
                        if (!dict.TryGetValue(rStringLiteral.image,out var regularExpression2))
                        {
                            if (rStringLiteral.ordinal == 0)
                            {
                                rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
                            }
                            dict.Add(rStringLiteral.image, rStringLiteral);
                        }
                        else if (tokenProduction.IsExplicit)
                        {
                            if (string.Equals(tokenProduction.LexStates[j], "DEFAULT"))
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.image) + ("\".")
                                    );
                            }
                            else
                            {
                                JavaCCErrors.Semantic_Error(rStringLiteral, ("Duplicate definition of string token \"") + (rStringLiteral.image) + ("\" in lexical state \"")
                                    + (tokenProduction.LexStates[j])
                                    + ("\".")
                                    );
                            }
                        }
                        else if (regularExpression2.tpContext.Kind != 0)
                        {
                            JavaCCErrors.Semantic_Error(rStringLiteral, ("String token \"") + (rStringLiteral.image) + ("\" has been defined as a \"")
                                + (TokenProduction.KindImage[regularExpression2.tpContext.Kind])
                                + ("\" token.")
                                );
                        }
                        else if (regularExpression2.private_rexp)
                        {
                            JavaCCErrors.Semantic_Error(rStringLiteral, ("String token \"") + (rStringLiteral.image) + ("\" has been defined as a private regular expression.")
                                );
                        }
                        else
                        {
                            rStringLiteral.ordinal = regularExpression2.ordinal;
                            PrepareToRemove(respecs, regExprSpec2);
                        }
                    }
                }
                else if (!(regExprSpec2.rexp is RJustName))
                {
                    regExprSpec2.rexp.ordinal = JavaCCGlobals.tokenCount++;
                }
                if (!(regExprSpec2.rexp is RJustName) && !string.Equals(regExprSpec2.rexp.label, ""))
                {
                    var dict2 = JavaCCGlobals.names_of_tokens;
                    ;
                    dict2.Add((regExprSpec2.rexp.ordinal), regExprSpec2.rexp.label);
                }
                if (!(regExprSpec2.rexp is RJustName))
                {
                    var dict3 = JavaCCGlobals.rexps_of_tokens;
                    ;
                    dict3.Add((regExprSpec2.rexp.ordinal), regExprSpec2.rexp);
                }
            }
        }
        RemovePreparedItems();
        if (!Options.UserTokenManager)
        {
            FixRJustNames fixRJustNames = new FixRJustNames();
            foreach(var tokenProduction2 in JavaCCGlobals.rexprlist)
            {
                var respecs2 = tokenProduction2.Respecs;
                foreach(var regExprSpec2 in respecs2)
                {
                    fixRJustNames.Root = regExprSpec2.rexp;
                    ExpansionTreeWalker.PreOrderWalk(regExprSpec2.rexp, fixRJustNames);
                    if (regExprSpec2.rexp is RJustName)
                    {
                        PrepareToRemove(respecs2, regExprSpec2);
                    }
                }
            }
        }
        RemovePreparedItems();
        if (Options.UserTokenManager)
        {
            foreach(var tokenProduction in JavaCCGlobals.rexprlist)
            {
                foreach(var regExprSpec in tokenProduction.Respecs)
                {
                    if (regExprSpec.rexp is RJustName rJustName)
                    {
                        if (!JavaCCGlobals.named_tokens_table.TryGetValue(rJustName.label, out var regularExpression3))
                        {
                            rJustName.ordinal = JavaCCGlobals.tokenCount++;
                            JavaCCGlobals.named_tokens_table.Add(rJustName.label, rJustName);
                            JavaCCGlobals.ordered_named_tokens.Add(rJustName);
                            var dict4 = JavaCCGlobals.names_of_tokens;
                            ;
                            dict4.Add((rJustName.ordinal), rJustName.label);
                        }
                        else
                        {
                            rJustName.ordinal = regularExpression3.ordinal;
                            PrepareToRemove(tokenProduction.Respecs, regExprSpec);
                        }
                    }
                }
            }
        }
        RemovePreparedItems();
        if (Options.UserTokenManager)
        {
            foreach(var tokenProduction in JavaCCGlobals.rexprlist)
            {
                var respecs = tokenProduction.Respecs;
                foreach(var regExprSpec in respecs)
                {
                    int key = (regExprSpec.rexp.ordinal);
                    if (!JavaCCGlobals.names_of_tokens.ContainsKey(key))
                    {
                        JavaCCErrors.Warning(regExprSpec.rexp, 
                            "Unlabeled regular expression cannot be referred to by user generated token manager.");
                    }
                }
            }
        }
        if (JavaCCErrors._Error_Count != 0)
        {

            throw new MetaParseException();
        }
        int num4 = 1;
        while (num4 != 0)
        {
            num4 = 0;
            foreach (var normalProduction2 in JavaCCGlobals.BNFProductions)
            {
                if (EmptyExpansionExists(normalProduction2.Expansion) && !normalProduction2.emptyPossible)
                {
                    NormalProduction normalProduction3 = normalProduction2;
                    int num5 = 1;
                    NormalProduction normalProduction4 = normalProduction3;
                    normalProduction4.emptyPossible = (byte)num5 != 0;
                    num4 = num5;
                }
            }
        }
        if (Options.SanityCheck && JavaCCErrors._Error_Count == 0)
        {
            foreach (var normalProduction2 in JavaCCGlobals.BNFProductions)
            {
                ExpansionTreeWalker.PreOrderWalk(normalProduction2.Expansion, new EmptyChecker());
            }
            foreach (var normalProduction2 in JavaCCGlobals.BNFProductions)
            {
                AddLeftMost(normalProduction2, normalProduction2.Expansion);
            }
            foreach(var normalProduction2 in JavaCCGlobals.BNFProductions)
            {
                if (normalProduction2.walkStatus == 0)
                {
                    ProdWalk(normalProduction2);
                }
            }
            if (!Options.UserTokenManager)
            {
                foreach(var tp in JavaCCGlobals.rexprlist)
                {
                    TokenProduction tokenProduction2 = tp;
                    var respecs2 = tokenProduction2.Respecs;
                    foreach(var rp in respecs2)
                    {
                        RegExprSpec regExprSpec2 = rp;
                        RegularExpression regularExpression3 = regExprSpec2.rexp;
                        if (regularExpression3.walkStatus == 0)
                        {
                            regularExpression3.walkStatus = -1;
                            if (RExpWalk(regularExpression3))
                            {
                                loopString = ("...") + (regularExpression3.label) + ("... --> ")
                                    + (loopString)
                                    ;
                                JavaCCErrors.Semantic_Error(regularExpression3, ("Loop in regular expression detected: \"") + (loopString) + ("\"")
                                    );
                            }
                            regularExpression3.walkStatus = 1;
                        }
                    }
                }
            }
            if (JavaCCErrors._Error_Count == 0)
            {
                foreach(var p in JavaCCGlobals.BNFProductions)
                {
                    ExpansionTreeWalker.PreOrderWalk(p.Expansion, new LookaheadChecker());
                }
            }
        }
        if (JavaCCErrors._Error_Count != 0)
        {

            throw new MetaParseException();
        }
    }


    public new static void ReInit()
    {
        removeList = new();
        itemList = new();
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
        if (dict.TryGetValue(str,out var regularExpression) && !regularExpression.tpContext.IgnoreCase)
        {
            return false;
        }
       
        foreach(var pair in dict)
        {
            regularExpression=pair.Value;
            if (regularExpression.tpContext.IgnoreCase)
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
            for (int i = 0; i < npleft.leIndex; i++)
            {
                if (npleft.leftExpansions[i] == ((NonTerminal)npright).prod)
                {
                    return;
                }
            }
            if (npleft.leIndex == npleft.leftExpansions.Length)
            {
                NormalProduction[] array = new NormalProduction[npleft.leIndex * 2];
                Array.Copy(npleft.leftExpansions, 0, array, 0, npleft.leIndex);
                npleft.leftExpansions = array;
            }
            NormalProduction[] leftExpansions = npleft.leftExpansions;
            int leIndex = npleft.leIndex;
            npleft.leIndex = leIndex + 1;
            leftExpansions[leIndex] = ((NonTerminal)npright).prod;
        }
        else if (npright is OneOrMore)
        {
            AddLeftMost(npleft, ((OneOrMore)npright).expansion);
        }
        else if (npright is ZeroOrMore)
        {
            AddLeftMost(npleft, ((ZeroOrMore)npright).Expansion);
        }
        else if (npright is ZeroOrOne)
        {
            AddLeftMost(npleft, ((ZeroOrOne)npright).Expansion);
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
        else if (npright is TryBlock)
        {
            AddLeftMost(npleft, ((TryBlock)npright).Expression);
        }
    }


    private static bool ProdWalk(NormalProduction np)
    {
        np.walkStatus = -1;
        for (int i = 0; i < np.leIndex; i++)
        {
            if (np.leftExpansions[i].walkStatus == -1)
            {
                np.leftExpansions[i].walkStatus = -2;
                loopString = (np.lhs) + ("... --> ") + (np.leftExpansions[i].lhs)
                    + ("...")
                    ;
                if (np.walkStatus == -2)
                {
                    np.walkStatus = 1;
                    JavaCCErrors.Semantic_Error(np, ("Left recursion detected: \"") + (loopString) + ("\"")
                        );
                    return false;
                }
                np.walkStatus = 1;
                return true;
            }
            if (np.leftExpansions[i].walkStatus == 0 && ProdWalk(np.leftExpansions[i]))
            {
                loopString = (np.lhs) + ("... --> ") + (loopString)
                    ;
                if (np.walkStatus == -2)
                {
                    np.walkStatus = 1;
                    JavaCCErrors.Semantic_Error(np, ("Left recursion detected: \"") + (loopString) + ("\"")
                        );
                    return false;
                }
                np.walkStatus = 1;
                return true;
            }
        }
        np.walkStatus = 1;
        return false;
    }


    private static bool RExpWalk(RegularExpression exp)
    {
        if (exp is RJustName rJustName)
        {
            if (rJustName.regexpr.walkStatus == -1)
            {
                rJustName.regexpr.walkStatus = -2;
                loopString = ("...") + (rJustName.regexpr.label) + ("...")
                    ;
                return true;
            }
            if (rJustName.regexpr.walkStatus == 0)
            {
                rJustName.regexpr.walkStatus = -1;
                if (RExpWalk(rJustName.regexpr))
                {
                    loopString = ("...") + (rJustName.regexpr.label) + ("... --> ")
                        + (loopString)
                        ;
                    if (rJustName.regexpr.walkStatus == -2)
                    {
                        rJustName.regexpr.walkStatus = 1;
                        JavaCCErrors.Semantic_Error(rJustName.regexpr, ("Loop in regular expression detected: \"") + (loopString) + ("\"")
                            );
                        return false;
                    }
                    rJustName.regexpr.walkStatus = 1;
                    return true;
                }
                rJustName.regexpr.walkStatus = 1;
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
            if (exp is RSequence rs)
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
            if (exp is ROneOrMore more)
            {
                return RExpWalk(more.RegExpr);
            }
            if (exp is RZeroOrMore more1)
            {
                return RExpWalk(more1.Regexpr);
            }
            if (exp is RZeroOrOne one)
            {
                return RExpWalk(one.regexpr);
            }
            if (exp is RRepetitionRange range)
            {
                return RExpWalk(range.regexpr);
            }
        }
        return false;
    }


    public Semanticize()
    {
    }
}
