using JavaCC.JJTree;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JavaCC.Parser;
public class Semanticize : JavaCCGlobals
{
    internal class EmptyChecker : JavaCCGlobals, TreeWalkerOp
    {

        internal EmptyChecker()
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
                if (EmptyExpansionExists(z.expansion))
                {
                    JavaCCErrors.Semantic_Error(exp, "Expansion within \"(...)*\" can be matched by empty string.");
                }
            }
            else if (exp is ZeroOrOne t && EmptyExpansionExists(t.expansion))
            {
                JavaCCErrors.Semantic_Error(exp, "Expansion within \"(...)?\" can be matched by empty string.");
            }
        }
    }


    internal class FixRJustNames : JavaCCGlobals, TreeWalkerOp
    {
        public RegularExpression root;

        internal FixRJustNames()
        {
        }

        public virtual bool GoDeeper(Expansion P_0)
        {
            return true;
        }


        public virtual void Action(Expansion P_0)
        {
            if (P_0 is RJustName rJustName)
            {
                if (!JavaCCGlobals.named_tokens_table.TryGetValue(rJustName.label, out var regularExpression))
                {
                    JavaCCErrors.Semantic_Error(P_0, ("Undefined lexical token name \"") + (rJustName.label) + ("\".")
                        );
                    return;
                }
                if (rJustName == root && !rJustName.tpContext.isExplicit && regularExpression.private_rexp)
                {
                    JavaCCErrors.Semantic_Error(P_0, ("Token name \"") + (rJustName.label) + ("\" refers to a private ")
                        + ("(with a #) regular expression.")
                        );
                    return;
                }
                if (rJustName == root && !rJustName.tpContext.isExplicit && regularExpression.tpContext.Kind != 0)
                {
                    JavaCCErrors.Semantic_Error(P_0, ("Token name \"") + (rJustName.label) + ("\" refers to a non-token ")
                        + ("(SKIP, MORE, IGNORE_IN_BNF) regular expression.")
                        );
                    return;
                }
                rJustName.ordinal = regularExpression.ordinal;
                rJustName.regexpr = regularExpression;
            }
        }

    }


    internal class LookaheadChecker : JavaCCGlobals, TreeWalkerOp
    {

        internal LookaheadChecker()
        {
        }


        internal static bool ImplicitLA(Expansion P_0)
        {
            if (P_0 is not Sequence)
            {
                return true;
            }
            Sequence sequence = (Sequence)P_0;
            object obj = sequence.Units[0];
            if (obj is not Lookahead)
            {
                return true;
            }
            Lookahead lookahead = (Lookahead)obj;
            return (!lookahead.isExplicit) ? true : false;
        }

        public virtual bool GoDeeper(Expansion P_0)
        {
            if (P_0 is RegularExpression)
            {
                return false;
            }
            if (P_0 is Lookahead)
            {
                return false;
            }
            return true;
        }


        public virtual void Action(Expansion P_0)
        {
            if (P_0 is Choice choice)
            {
                if (Options.Lookahead == 1 || Options.ForceLaCheck)
                {
                    LookaheadCalc.choiceCalc(choice);
                }
            }
            else if (P_0 is OneOrMore oneOrMore)
            {
                if (Options.ForceLaCheck || (ImplicitLA(oneOrMore.expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.ebnfCalc(oneOrMore, oneOrMore.expansion);
                }
            }
            else if (P_0 is ZeroOrMore zeroOrMore)
            {
                if (Options.ForceLaCheck || (ImplicitLA(zeroOrMore.expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.ebnfCalc(zeroOrMore, zeroOrMore.expansion);
                }
            }
            else if (P_0 is ZeroOrOne)
            {
                ZeroOrOne zeroOrOne = (ZeroOrOne)P_0;
                if (Options.ForceLaCheck || (ImplicitLA(zeroOrOne.expansion) && Options.Lookahead == 1))
                {
                    LookaheadCalc.ebnfCalc(zeroOrOne, zeroOrOne.expansion);
                }
            }
        }


        static LookaheadChecker()
        {

        }
    }


    internal class LookaheadFixer : JavaCCGlobals, TreeWalkerOp
    {



        internal LookaheadFixer()
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


        public virtual void Action(Expansion P_0)
        {
            if (P_0 is not Sequence || P_0.parent is Choice || P_0.parent is ZeroOrMore || P_0.parent is OneOrMore || P_0.parent is ZeroOrOne)
            {
                return;
            }
            Sequence sequence = (Sequence)P_0;
            Lookahead lookahead = (Lookahead)sequence.Units[0];
            if (!lookahead.isExplicit)
            {
                return;
            }
            Choice choice = new Choice
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                parent = sequence
            };
            Sequence sequence2 = new Sequence
            {
                Line = lookahead.Line,
                Column = lookahead.Column,
                parent = choice
            };
            sequence2.Units.Add(lookahead);
            lookahead.parent = sequence2;
            Action action = new Action();
            action.Line = lookahead.Line;
            action.Column = lookahead.Column;
            action.parent = sequence2;
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
            Lookahead lookahead2 = new();
            lookahead2.isExplicit = false;
            lookahead2.Line = lookahead.Line;
            lookahead2.Column = lookahead.Column;
            lookahead2.parent = sequence;
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
            if (exp is NonTerminal nonTerminal)
            {
                NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.Production_table.get(nonTerminal.name);
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

    internal static List<List<RegExprSpec>> removeList = new();

    internal static List<RegExprSpec> itemList = new();

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
            return EmptyExpansionExists(b.exp);
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
        enumeration = JavaCCGlobals.BNFProductions.elements();
        while (enumeration.hasMoreElements())
        {
            NormalProduction normalProduction = (NormalProduction)enumeration.nextElement();
            if (JavaCCGlobals.Production_table.Add(normalProduction.lhs, normalProduction) != null)
            {
                JavaCCErrors.Semantic_Error(normalProduction, (normalProduction.lhs) + (" occurs on the left hand side of more than one production."));
            }
        }
        enumeration = JavaCCGlobals.BNFProductions.elements();
        while (enumeration.hasMoreElements())
        {
            ExpansionTreeWalker.PreOrderWalk(((NormalProduction)enumeration.nextElement()).Expansion, new ProductionDefinedChecker());
        }
        enumeration = JavaCCGlobals.rexprlist.elements();
        while (enumeration.hasMoreElements())
        {
            TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
            var respecs = tokenProduction.Respecs;
            Enumeration enumeration2 = respecs.elements();
            while (enumeration2.hasMoreElements())
            {
                RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
                if (regExprSpec.nextState != null && JavaCCGlobals.lexstate_S2I.get(regExprSpec.nextState) == null)
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.nsTok, ("Lexical state \"") + (regExprSpec.nextState) + ("\" has not been defined.")
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
                    prepareToRemove(respecs, regExprSpec);
                }
                else if (tokenProduction.isExplicit && Options.UserTokenManager)
                {
                    JavaCCErrors.Warning(regExprSpec.rexp, "Ignoring regular expression specification since option USER_TOKEN_MANAGER has been set to true.");
                }
                else if (tokenProduction.isExplicit && !Options.UserTokenManager && regExprSpec.rexp is RJustName)
                {
                    JavaCCErrors.Warning(regExprSpec.rexp, ("Ignoring free-standing regular expression reference.  If you really want this, you must give it a different label as <NEWLABEL:<") + (regExprSpec.rexp.label) + (">>.")
                        );
                    prepareToRemove(respecs, regExprSpec);
                }
                else if (!tokenProduction.isExplicit && regExprSpec.rexp.private_rexp)
                {
                    JavaCCErrors.Semantic_Error(regExprSpec.rexp, "Private (#) regular expression cannot be defined within grammar productions.");
                }
            }
        }
        removePreparedItems();
        enumeration = JavaCCGlobals.rexprlist.elements();
        while (enumeration.hasMoreElements())
        {
            TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
            var respecs = tokenProduction.Respecs;
            Enumeration enumeration2 = respecs.elements();
            while (enumeration2.hasMoreElements())
            {
                RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
                if (!(regExprSpec.rexp is RJustName) && !string.Equals(regExprSpec.rexp.label, ""))
                {
                    string label = regExprSpec.rexp.label;
                    object obj = JavaCCGlobals.named_tokens_table.Add(label, regExprSpec.rexp);
                    if (obj != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, ("Multiply defined lexical token name \"") + (label) + ("\".")
                            );
                    }
                    else
                    {
                        JavaCCGlobals.ordered_named_tokens.Add(regExprSpec.rexp);
                    }
                    if (JavaCCGlobals.lexstate_S2I.get(label) != null)
                    {
                        JavaCCErrors.Semantic_Error(regExprSpec.rexp, ("Lexical token name \"") + (label) + ("\" is the same as ")
                            + ("that of a lexical state.")
                            );
                    }
                }
            }
        }
        JavaCCGlobals.tokenCount = 1;
        enumeration = JavaCCGlobals.rexprlist.elements();
        while (enumeration.hasMoreElements())
        {
            TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
            var respecs = tokenProduction.Respecs;
            Enumeration enumeration3;
            if (tokenProduction.LexStates == null)
            {
                tokenProduction.LexStates = new string[JavaCCGlobals.lexstate_I2S.Count];
                int num = 0;
                enumeration3 = JavaCCGlobals.lexstate_I2S.elements();
                while (enumeration3.hasMoreElements())
                {
                    string[] lexStates = tokenProduction.LexStates;
                    int num2 = num;
                    num++;
                    lexStates[num2] = (string)enumeration3.nextElement();
                }
            }
            Hashtable[] array = new Hashtable[(nint)tokenProduction.LexStates.LongLength];
            for (int i = 0; i < (nint)tokenProduction.LexStates.LongLength; i++)
            {
                array[i] = (Hashtable)JavaCCGlobals.simple_tokens_table.get(tokenProduction.LexStates[i]);
            }
            enumeration3 = respecs.elements();
            while (enumeration3.hasMoreElements())
            {
                RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
                if (regExprSpec2.rexp is RStringLiteral rStringLiteral)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        Hashtable hashtable = (Hashtable)array[j].get((rStringLiteral.image).ToUpper());
                        if (hashtable == null)
                        {
                            if (rStringLiteral.ordinal == 0)
                            {
                                rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
                            }
                            hashtable = new Hashtable();
                            hashtable.Add(rStringLiteral.image, rStringLiteral);
                            array[j].Add((rStringLiteral.image).ToUpper(), hashtable);
                            continue;
                        }
                        if (hasIgnoreCase(hashtable, rStringLiteral.image))
                        {
                            if (!rStringLiteral.tpContext.isExplicit)
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
                        if (rStringLiteral.tpContext.ignoreCase)
                        {
                            string str = "";
                            int num3 = 0;
                            Enumeration enumeration4 = hashtable.elements();
                            while (enumeration4.hasMoreElements())
                            {
                                RegularExpression regularExpression = (RegularExpression)enumeration4.nextElement();
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
                            hashtable.Add(rStringLiteral.image, rStringLiteral);
                            continue;
                        }
                        RegularExpression regularExpression2 = (RegularExpression)hashtable.get(rStringLiteral.image);
                        if (regularExpression2 == null)
                        {
                            if (rStringLiteral.ordinal == 0)
                            {
                                rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
                            }
                            hashtable.Add(rStringLiteral.image, rStringLiteral);
                        }
                        else if (tokenProduction.isExplicit)
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
                            prepareToRemove(respecs, regExprSpec2);
                        }
                    }
                }
                else if (!(regExprSpec2.rexp is RJustName))
                {
                    regExprSpec2.rexp.ordinal = JavaCCGlobals.tokenCount++;
                }
                if (!(regExprSpec2.rexp is RJustName) && !string.Equals(regExprSpec2.rexp.label, ""))
                {
                    var hashtable2 = JavaCCGlobals.names_of_tokens;
                    ;
                    hashtable2.Add((regExprSpec2.rexp.ordinal), regExprSpec2.rexp.label);
                }
                if (!(regExprSpec2.rexp is RJustName))
                {
                    Hashtable hashtable3 = JavaCCGlobals.rexps_of_tokens;
                    ;
                    hashtable3.Add((regExprSpec2.rexp.ordinal), regExprSpec2.rexp);
                }
            }
        }
        removePreparedItems();
        if (!Options.UserTokenManager)
        {
            FixRJustNames fixRJustNames = new FixRJustNames();
            Enumeration enumeration5 = JavaCCGlobals.rexprlist.elements();
            while (enumeration5.hasMoreElements())
            {
                TokenProduction tokenProduction2 = (TokenProduction)enumeration5.nextElement();
                var respecs2 = tokenProduction2.Respecs;
                Enumeration enumeration3 = respecs2.elements();
                while (enumeration3.hasMoreElements())
                {
                    RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
                    fixRJustNames.root = regExprSpec2.rexp;
                    ExpansionTreeWalker.PreOrderWalk(regExprSpec2.rexp, fixRJustNames);
                    if (regExprSpec2.rexp is RJustName)
                    {
                        prepareToRemove(respecs2, regExprSpec2);
                    }
                }
            }
        }
        removePreparedItems();
        if (Options.UserTokenManager)
        {
            enumeration = JavaCCGlobals.rexprlist.elements();
            while (enumeration.hasMoreElements())
            {
                TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
                var respecs = tokenProduction.Respecs;
                Enumeration enumeration2 = respecs.elements();
                while (enumeration2.hasMoreElements())
                {
                    RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
                    if (regExprSpec.rexp is RJustName)
                    {
                        RJustName rJustName = (RJustName)regExprSpec.rexp;
                        RegularExpression regularExpression3 = (RegularExpression)JavaCCGlobals.named_tokens_table.get(rJustName.label);
                        if (regularExpression3 == null)
                        {
                            rJustName.ordinal = JavaCCGlobals.tokenCount++;
                            JavaCCGlobals.named_tokens_table.Add(rJustName.label, rJustName);
                            JavaCCGlobals.ordered_named_tokens.Add(rJustName);
                            Hashtable hashtable4 = JavaCCGlobals.names_of_tokens;
                            ;
                            hashtable4.Add((rJustName.ordinal), rJustName.label);
                        }
                        else
                        {
                            rJustName.ordinal = regularExpression3.ordinal;
                            prepareToRemove(respecs, regExprSpec);
                        }
                    }
                }
            }
        }
        removePreparedItems();
        if (Options.UserTokenManager)
        {
            enumeration = JavaCCGlobals.rexprlist.elements();
            while (enumeration.hasMoreElements())
            {
                TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
                ArrayList respecs = tokenProduction.Respecs;
                Enumeration enumeration2 = respecs.elements();
                while (enumeration2.hasMoreElements())
                {
                    RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
                    ;
                    int key = (regExprSpec.rexp.ordinal);
                    if (JavaCCGlobals.names_of_tokens.get(key) == null)
                    {
                        JavaCCErrors.Warning(regExprSpec.rexp, "Unlabeled regular expression cannot be referred to by user generated token manager.");
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
            Enumeration enumeration5 = JavaCCGlobals.BNFProductions.elements();
            while (enumeration5.hasMoreElements())
            {
                NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
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
            Enumeration enumeration5 = JavaCCGlobals.BNFProductions.elements();
            while (enumeration5.hasMoreElements())
            {
                ExpansionTreeWalker.PreOrderWalk(((NormalProduction)enumeration5.nextElement()).Expansion, new EmptyChecker());
            }
            enumeration5 = JavaCCGlobals.BNFProductions.elements();
            while (enumeration5.hasMoreElements())
            {
                NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
                addLeftMost(normalProduction2, normalProduction2.Expansion);
            }
            enumeration5 = JavaCCGlobals.BNFProductions.elements();
            while (enumeration5.hasMoreElements())
            {
                NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
                if (normalProduction2.walkStatus == 0)
                {
                    ProdWalk(normalProduction2);
                }
            }
            if (!Options.UserTokenManager)
            {
                enumeration5 = JavaCCGlobals.rexprlist.elements();
                while (enumeration5.hasMoreElements())
                {
                    TokenProduction tokenProduction2 = (TokenProduction)enumeration5.nextElement();
                    ArrayList respecs2 = tokenProduction2.Respecs;
                    Enumeration enumeration3 = respecs2.elements();
                    while (enumeration3.hasMoreElements())
                    {
                        RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
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
                enumeration5 = JavaCCGlobals.BNFProductions.elements();
                while (enumeration5.hasMoreElements())
                {
                    ExpansionTreeWalker.PreOrderWalk(((NormalProduction)enumeration5.nextElement()).Expansion, new LookaheadChecker());
                }
            }
        }
        if (JavaCCErrors._Error_Count != 0)
        {

            throw new MetaParseException();
        }
    }


    public new static void ReInitReInit()
    {
        removeList = new();
        itemList = new();
        other = null;
        loopString = null;
    }


    internal static void prepareToRemove(List<RegExprSpec> P_0, RegExprSpec P_1)
    {
        removeList.Add(P_0);
        itemList.Add(P_1);
    }


    internal static void removePreparedItems()
    {
        for (int i = 0; i < removeList.Count; i++)
        {
            var vector = removeList[i];
            vector.Remove(itemList[i]);
        }
        removeList.Clear();
        itemList.Clear();
    }


    public static bool hasIgnoreCase(Dictionary<string, RegularExpression> h, string str)
    {
        RegularExpression regularExpression = (RegularExpression)h.get(str);
        if (regularExpression != null && !regularExpression.tpContext.ignoreCase)
        {
            return false;
        }
        Enumeration enumeration = h.elements();
        while (enumeration.hasMoreElements())
        {
            regularExpression = (RegularExpression)enumeration.nextElement();
            if (regularExpression.tpContext.ignoreCase)
            {
                other = regularExpression;
                return true;
            }
        }
        return false;
    }


    private static void addLeftMost(NormalProduction P_0, Expansion P_1)
    {
        if (P_1 is NonTerminal)
        {
            for (int i = 0; i < P_0.leIndex; i++)
            {
                if (P_0.leftExpansions[i] == ((NonTerminal)P_1).prod)
                {
                    return;
                }
            }
            if ((nint)P_0.leIndex == (nint)P_0.leftExpansions.LongLength)
            {
                NormalProduction[] array = new NormalProduction[P_0.leIndex * 2];
                Array.Copy(P_0.leftExpansions, 0, array, 0, P_0.leIndex);
                P_0.leftExpansions = array;
            }
            NormalProduction[] leftExpansions = P_0.leftExpansions;
            int leIndex = P_0.leIndex;
            P_0.leIndex = leIndex + 1;
            leftExpansions[leIndex] = ((NonTerminal)P_1).prod;
        }
        else if (P_1 is OneOrMore)
        {
            addLeftMost(P_0, ((OneOrMore)P_1).expansion);
        }
        else if (P_1 is ZeroOrMore)
        {
            addLeftMost(P_0, ((ZeroOrMore)P_1).expansion);
        }
        else if (P_1 is ZeroOrOne)
        {
            addLeftMost(P_0, ((ZeroOrOne)P_1).expansion);
        }
        else if (P_1 is Choice pc)
        {
            foreach(var cx in pc.Choices)
            {
                addLeftMost(P_0, cx);
            }
        }
        else if (P_1 is Sequence ps)
        {
            foreach(var expansion in ps.Units)
            {
                addLeftMost(P_0, expansion);
                if (!EmptyExpansionExists(expansion))
                {
                    break;
                }
            }
        }
        else if (P_1 is TryBlock)
        {
            addLeftMost(P_0, ((TryBlock)P_1).exp);
        }
    }


    private static bool ProdWalk(NormalProduction P_0)
    {
        P_0.walkStatus = -1;
        for (int i = 0; i < P_0.leIndex; i++)
        {
            if (P_0.leftExpansions[i].walkStatus == -1)
            {
                P_0.leftExpansions[i].walkStatus = -2;
                loopString = (P_0.lhs) + ("... --> ") + (P_0.leftExpansions[i].lhs)
                    + ("...")
                    ;
                if (P_0.walkStatus == -2)
                {
                    P_0.walkStatus = 1;
                    JavaCCErrors.Semantic_Error(P_0, ("Left recursion detected: \"") + (loopString) + ("\"")
                        );
                    return false;
                }
                P_0.walkStatus = 1;
                return true;
            }
            if (P_0.leftExpansions[i].walkStatus == 0 && ProdWalk(P_0.leftExpansions[i]))
            {
                loopString = (P_0.lhs) + ("... --> ") + (loopString)
                    ;
                if (P_0.walkStatus == -2)
                {
                    P_0.walkStatus = 1;
                    JavaCCErrors.Semantic_Error(P_0, ("Left recursion detected: \"") + (loopString) + ("\"")
                        );
                    return false;
                }
                P_0.walkStatus = 1;
                return true;
            }
        }
        P_0.walkStatus = 1;
        return false;
    }


    private static bool RExpWalk(RegularExpression P_0)
    {
        if (P_0 is RJustName rJustName)
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
            if (P_0 is RChoice rc)
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
            if (P_0 is RSequence rs)
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
            if (P_0 is ROneOrMore more)
            {
                return RExpWalk(more.RegExpr);
            }
            if (P_0 is RZeroOrMore more1)
            {
                return RExpWalk(more1.regexpr);
            }
            if (P_0 is RZeroOrOne one)
            {
                return RExpWalk(one.regexpr);
            }
            if (P_0 is RRepetitionRange range)
            {
                return RExpWalk(range.regexpr);
            }
        }
        return false;
    }


    public Semanticize()
    {
    }

    static Semanticize()
    {

        removeList = new();
        itemList = new();
    }
}
