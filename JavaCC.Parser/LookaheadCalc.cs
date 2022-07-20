namespace JavaCC.Parser;
using System;
using System.Collections.Generic;

public class LookaheadCalc : JavaCCGlobals
{
    internal static int FirstChoice(Choice P_0)
    {
        if (Options.ForceLaCheck)
        {
            return 0;
        }
        for (int i = 0; i < P_0.Choices.Count; i++)
        {
            if (!ExplicitLA((Expansion)P_0.Choices[i]))
            {
                return i;
            }
        }
        int result = P_0.Choices.Count;

        return result;
    }


    internal static bool JavaCodeCheck(List<MatchInfo> P_0)
    {
        for (int i = 0; i < P_0.Count; i++)
        {
            if (((MatchInfo)P_0[i]).firstFreeLoc == 0)
            {
                return true;
            }
        }
        return false;
    }


    internal static MatchInfo Overlap(List<MatchInfo> left, List<MatchInfo> right)
    {
        for (int i = 0; i < left.Count; i++)
        {
            MatchInfo matchInfo = (MatchInfo)left[i];
            for (int j = 0; j < right.Count; j++)
            {
                MatchInfo matchInfo2 = (MatchInfo)right[j];
                int firstFreeLoc = matchInfo.firstFreeLoc;
                MatchInfo result = matchInfo;
                if (firstFreeLoc > matchInfo2.firstFreeLoc)
                {
                    firstFreeLoc = matchInfo2.firstFreeLoc;
                    result = matchInfo2;
                }
                if (firstFreeLoc == 0)
                {
                    return null;
                }
                int num = 0;
                for (int k = 0; k < firstFreeLoc; k++)
                {
                    if (matchInfo.match[k] != matchInfo2.match[k])
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


    internal static bool ExplicitLA(Expansion P_0)
    {
        if (P_0 is Sequence sequence)
        {
            var obj = sequence.Units[0];
            return obj is Lookahead lookahead && lookahead.IsExplicit;
        }
        return false;
    }


    internal static string Image(MatchInfo info)
    {
        string text = "";
        for (int i = 0; i < info.firstFreeLoc; i++)
        {
            if (info.match[i] == 0)
            {
                text = (text) + (" <EOF>");
                continue;
            }
            var dict = JavaCCGlobals.rexps_of_tokens;
            
            var regularExpression = dict[info.match[i]];

            text = ((!(regularExpression is RStringLiteral)) ? ((regularExpression.label == null || string.Equals(regularExpression.label, "")) ? (text) + (" <token of kind ") + (i)
                + (">")
                 : (text) + (" <") + (regularExpression.label)
                + (">")
                ) : (text) + (" \"") + (JavaCCGlobals.AddEscapes(((RStringLiteral)regularExpression).image))
                + ("\"")
                );
        }
        if (info.firstFreeLoc == 0)
        {
            return "";
        }
        return text.Substring(1);// String.instancehelper_substring(text, 1);
    }

    private static string Image(Expansion exp)
    {
        if (exp is OneOrMore)
        {
            return "(...)+";
        }
        if (exp is ZeroOrMore)
        {
            return "(...)*";
        }
        return "[...]";
    }


    public LookaheadCalc()
    {
    }


    public static void ChoiceCalc(Choice c)
    {
        int num = FirstChoice(c);
        var array = new List<MatchInfo>[c.Choices.Count];
        var array2 = new List<MatchInfo>[c.Choices.Count];
        int[] array3 = new int[c.Choices.Count - 1];
        MatchInfo[] array4 = new MatchInfo[c.Choices.Count - 1];
        int[] array5 = new int[c.Choices.Count - 1];
        for (int i = 1; i <= Options.ChoiceAmbiguityCheck; i++)
        {
            MatchInfo.laLimit = i;
            LookaheadWalk.considerSemanticLA = ((!Options.ForceLaCheck) ? true : false);
            for (int j = num; j < c.Choices.Count - 1; j++)
            {
                LookaheadWalk.sizeLimitedMatches = new ();
                MatchInfo matchInfo = new MatchInfo();
                matchInfo.firstFreeLoc = 0;
                List<MatchInfo> vector = new ();
                vector.Add(matchInfo);
                LookaheadWalk.GenFirstSet(vector, (Expansion)c.Choices[j]);
                array[j] = LookaheadWalk.sizeLimitedMatches;
            }
            LookaheadWalk.considerSemanticLA = false;
            for (int j = num + 1; j < c.Choices.Count; j++)
            {
                LookaheadWalk.sizeLimitedMatches = new ();
                MatchInfo matchInfo = new MatchInfo();
                matchInfo.firstFreeLoc = 0;
                List<MatchInfo> vector = new ();
                vector.Add(matchInfo);
                LookaheadWalk.GenFirstSet(vector, (Expansion)c.Choices[j]);
                array2[j] = LookaheadWalk.sizeLimitedMatches;
            }
            if (i == 1)
            {
                for (int j = num; j < c.Choices.Count - 1; j++)
                {
                    Expansion expansion = (Expansion)c.Choices[j];
                    if (Semanticize.EmptyExpansionExists(expansion))
                    {
                        JavaCCErrors.Warning(expansion, "This choice can expand to the empty token sequence and will therefore always be taken in favor of the choices appearing later.");
                        break;
                    }
                    if (JavaCodeCheck(array[j]))
                    {
                        JavaCCErrors.Warning(expansion, "JAVACODE non-terminal will force this choice to be taken in favor of the choices appearing later.");
                        break;
                    }
                }
            }
            int num2 = 0;
            for (int j = num; j < c.Choices.Count - 1; j++)
            {
                for (int k = j + 1; k < c.Choices.Count; k++)
                {
                    MatchInfo matchInfo;
                    if ((matchInfo = Overlap(array[j], array2[k])) != null)
                    {
                        array3[j] = i + 1;
                        array4[j] = matchInfo;
                        array5[j] = k;
                        num2 = 1;
                        break;
                    }
                }
            }
            if (num2 == 0)
            {
                break;
            }
        }
        for (int i = num; i < c.Choices.Count - 1; i++)
        {
            if (!ExplicitLA((Expansion)c.Choices[i]) || Options.ForceLaCheck)
            {
                if (array3[i] > Options.ChoiceAmbiguityCheck)
                {
                    JavaCCErrors.Warning("Choice conflict involving two expansions at");
                    Console.Error.Write(("         line ") + (((Expansion)c.Choices[i]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[i]).Column));
                    Console.Error.Write((" and line ") + (((Expansion)c.Choices[(array5[i])]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[(array5[i])]).Column));
                    Console.Error.WriteLine(" respectively.");
                    Console.Error.WriteLine(("         A common prefix is: ") + (Image(array4[i])));
                    Console.Error.WriteLine(("         Consider using a lookahead of ") + (array3[i]) + (" or more for earlier expansion.")
                        );
                }
                else if (array3[i] > 1)
                {
                    JavaCCErrors.Warning("Choice conflict involving two expansions at");
                    Console.Error.Write(("         line ") + (((Expansion)c.Choices[i]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[i]).Column));
                    Console.Error.Write((" and line ") + (((Expansion)c.Choices[(array5[i])]).Line));
                    Console.Error.Write((", column ") + (((Expansion)c.Choices[(array5[i])]).Column));
                    Console.Error.WriteLine(" respectively.");
                    Console.Error.WriteLine(("         A common prefix is: ") + (Image(array4[i])));
                    Console.Error.WriteLine(("         Consider using a lookahead of ") + (array3[i]) + (" for earlier expansion.")
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
            MatchInfo.laLimit = i;
            LookaheadWalk.sizeLimitedMatches = new ();
            MatchInfo matchInfo2 = new MatchInfo();
            matchInfo2.firstFreeLoc = 0;
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
