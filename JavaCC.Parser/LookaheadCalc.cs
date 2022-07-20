namespace JavaCC.Parser;
using System;
using System.Collections.Generic;

public class LookaheadCalc : JavaCCGlobals
{
    public static int FirstChoice(Choice choice)
    {
        if (Options.ForceLaCheck)
        {
            return 0;
        }
        for (int i = 0; i < choice.Choices.Count; i++)
        {
            if (!ExplicitLA(choice.Choices[i]))
            {
                return i;
            }
        }
        return choice.Choices.Count;
    }


    public static bool JavaCodeCheck(List<MatchInfo> mi)
    {
        for (int i = 0; i < mi.Count; i++)
        {
            if (mi[i].FirstFreeLoc == 0)
            {
                return true;
            }
        }
        return false;
    }


    public static MatchInfo Overlap(List<MatchInfo> left, List<MatchInfo> right)
    {
        for (int i = 0; i < left.Count; i++)
        {
            var matchInfo = left[i];
            for (int j = 0; j < right.Count; j++)
            {
                var matchInfo2 = right[j];
                int firstFreeLoc = matchInfo.FirstFreeLoc;
                var result = matchInfo;
                if (firstFreeLoc > matchInfo2.FirstFreeLoc)
                {
                    firstFreeLoc = matchInfo2.FirstFreeLoc;
                    result = matchInfo2;
                }
                if (firstFreeLoc == 0)
                {
                    return null;
                }
                int num = 0;
                for (int k = 0; k < firstFreeLoc; k++)
                {
                    if (matchInfo.Match[k] != matchInfo2.Match[k])
                    {
                        num = 1;
                        break;
                    }
                }
                if (num == 0)
                {
                    return result;
                }
            }
        }
        return null;
    }


    public static bool ExplicitLA(Expansion exp)
    {
        if (exp is Sequence sequence)
        {
            var obj = sequence.Units[0];
            return obj is Lookahead lookahead && lookahead.IsExplicit;
        }
        return false;
    }


    public static string Image(MatchInfo info)
    {
        string text = "";
        for (int i = 0; i < info.FirstFreeLoc; i++)
        {
            if (info.Match[i] == 0)
            {
                text = (text) + (" <EOF>");
                continue;
            }
            var dict = JavaCCGlobals.RexpsOfTokens;
            
            var regularExpression = dict[info.Match[i]];

            text = ((!(regularExpression is RStringLiteral)) ? ((regularExpression.Label == null || string.Equals(regularExpression.Label, "")) ? (text) + (" <token of kind ") + (i)
                + (">")
                 : (text) + (" <") + (regularExpression.Label)
                + (">")
                ) : (text) + (" \"") + (JavaCCGlobals.AddEscapes(((RStringLiteral)regularExpression).image))
                + ("\"")
                );
        }
        if (info.FirstFreeLoc == 0)
        {
            return "";
        }
        return text.Substring(1);// String.instancehelper_substring(text, 1);
    }

    public static string Image(Expansion exp) => exp switch
    {
        OneOrMore => "(...)+",
        ZeroOrMore => "(...)*",
        _ => "[...]"
    };


    public LookaheadCalc()
    {
    }


    public static void ChoiceCalc(Choice c)
    {
        int n = FirstChoice(c);
        var a1 = new List<MatchInfo>[c.Choices.Count];
        var a2 = new List<MatchInfo>[c.Choices.Count];
        var a3 = new int[c.Choices.Count - 1];
        var a4 = new MatchInfo[c.Choices.Count - 1];
        var a5 = new int[c.Choices.Count - 1];
        for (int i = 1; i <= Options.ChoiceAmbiguityCheck; i++)
        {
            MatchInfo.LaLimit = i;
            LookaheadWalk.considerSemanticLA = ((!Options.ForceLaCheck) ? true : false);
            for (int j = n; j < c.Choices.Count - 1; j++)
            {
                LookaheadWalk.sizeLimitedMatches = new ();
                MatchInfo matchInfo = new();
                matchInfo.FirstFreeLoc = 0;
                List<MatchInfo> vector = new()
                {
                    matchInfo
                };
                LookaheadWalk.GenFirstSet(vector, (Expansion)c.Choices[j]);
                a1[j] = LookaheadWalk.sizeLimitedMatches;
            }
            LookaheadWalk.considerSemanticLA = false;
            for (int j = n + 1; j < c.Choices.Count; j++)
            {
                LookaheadWalk.sizeLimitedMatches = new ();
                MatchInfo matchInfo = new();
                matchInfo.FirstFreeLoc = 0;
                List<MatchInfo> vector = new ();
                vector.Add(matchInfo);
                LookaheadWalk.GenFirstSet(vector, (Expansion)c.Choices[j]);
                a2[j] = LookaheadWalk.sizeLimitedMatches;
            }
            if (i == 1)
            {
                for (int j = n; j < c.Choices.Count - 1; j++)
                {
                    Expansion expansion = (Expansion)c.Choices[j];
                    if (Semanticize.EmptyExpansionExists(expansion))
                    {
                        JavaCCErrors.Warning(expansion, "This choice can expand to the empty token sequence and will therefore always be taken in favor of the choices appearing later.");
                        break;
                    }
                    if (JavaCodeCheck(a1[j]))
                    {
                        JavaCCErrors.Warning(expansion, "JAVACODE non-terminal will force this choice to be taken in favor of the choices appearing later.");
                        break;
                    }
                }
            }
            int n2 = 0;
            for (int j = n; j < c.Choices.Count - 1; j++)
            {
                for (int k = j + 1; k < c.Choices.Count; k++)
                {
                    MatchInfo matchInfo;
                    if ((matchInfo = Overlap(a1[j], a2[k])) != null)
                    {
                        a3[j] = i + 1;
                        a4[j] = matchInfo;
                        a5[j] = k;
                        n2 = 1;
                        break;
                    }
                }
            }
            if (n2 == 0)
            {
                break;
            }
        }
        for (int i = n; i < c.Choices.Count - 1; i++)
        {
            if (!ExplicitLA((Expansion)c.Choices[i]) || Options.ForceLaCheck)
            {
                if (a3[i] > Options.ChoiceAmbiguityCheck)
                {
                    JavaCCErrors.Warning("Choice conflict involving two expansions at");
                    Console.Error.Write(("         line ") + (((Expansion)c.Choices[i]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[i]).Column));
                    Console.Error.Write((" and line ") + (((Expansion)c.Choices[(a5[i])]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[(a5[i])]).Column));
                    Console.Error.WriteLine(" respectively.");
                    Console.Error.WriteLine(("         A common prefix is: ") + (Image(a4[i])));
                    Console.Error.WriteLine(("         Consider using a lookahead of ") + (a3[i]) + (" or more for earlier expansion.")
                        );
                }
                else if (a3[i] > 1)
                {
                    JavaCCErrors.Warning("Choice conflict involving two expansions at");
                    Console.Error.Write(("         line ") + (((Expansion)c.Choices[i]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[i]).Column));
                    Console.Error.Write((" and line ") + (((Expansion)c.Choices[(a5[i])]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[(a5[i])]).Column));
                    Console.Error.WriteLine(" respectively.");
                    Console.Error.WriteLine(("         A common prefix is: ") + (Image(a4[i])));
                    Console.Error.WriteLine(("         Consider using a lookahead of ") + (a3[i]) + (" for earlier expansion.")
                        );
                }
            }
        }
    }


    public static void EBNFCalc(Expansion e1, Expansion e2)
    {
        MatchInfo matchInfo = null;
        int i;
        for (i = 1; i <= Options.OtherAmbiguityCheck; i++)
        {
            MatchInfo.LaLimit = i;
            LookaheadWalk.sizeLimitedMatches = new ();
            MatchInfo matchInfo2 = new MatchInfo();
            matchInfo2.FirstFreeLoc = 0;
            List<MatchInfo> vector = new ();
            vector.Add(matchInfo2);
            LookaheadWalk.considerSemanticLA = ((!Options.ForceLaCheck) ? true : false);
            LookaheadWalk.GenFirstSet(vector, e2);
            var sizeLimitedMatches = LookaheadWalk.sizeLimitedMatches;
            LookaheadWalk.sizeLimitedMatches = new ();
            LookaheadWalk.considerSemanticLA = false;
            LookaheadWalk.GenFollowSet(vector, e1, Expansion.NextGenerationIndex++);
            var sizeLimitedMatches2 = LookaheadWalk.sizeLimitedMatches;
            if (i == 1 && JavaCodeCheck(sizeLimitedMatches))
            {
                JavaCCErrors.Warning(e2, ("JAVACODE non-terminal within ") + (Image(e1)) + (" construct will force this construct to be entered in favor of ")
                    + ("expansions occurring after construct.")
                    );
            }
            if ((matchInfo2 = Overlap(sizeLimitedMatches, sizeLimitedMatches2)) == null)
            {
                break;
            }
            matchInfo = matchInfo2;
        }
        if (i > Options.OtherAmbiguityCheck)
        {
            JavaCCErrors.Warning(("Choice conflict in ") + (Image(e1)) + (" construct ")
                + ("at line ")
                + (e1.Line)
                + (", column ")
                + (e1.Column)
                + (".")
                );
            Console.Error.WriteLine("         Expansion nested within construct and expansion following construct");
            Console.Error.WriteLine(("         have common prefixes, one of which is: ") + (Image(matchInfo)));
            Console.Error.WriteLine(("         Consider using a lookahead of ") + (i) + (" or more for nested expansion.")
                );
        }
        else if (i > 1)
        {
            JavaCCErrors.Warning(("Choice conflict in ") + (Image(e1)) + (" construct ")
                + ("at line ")
                + (e1.Line)
                + (", column ")
                + (e1.Column)
                + (".")
                );
            Console.Error.WriteLine("         Expansion nested within construct and expansion following construct");
            Console.Error.WriteLine(("         have common prefixes, one of which is: ") + (Image(matchInfo)));
            Console.Error.WriteLine(("         Consider using a lookahead of ") + (i) + (" for nested expansion.")
                );
        }
    }
}
