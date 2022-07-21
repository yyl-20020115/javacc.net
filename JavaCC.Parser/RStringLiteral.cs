using JavaCC.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JavaCC.Parser;

public class RStringLiteral : RegularExpression
{
    public string Image = "";

    public static int MaxStrKind = 0;

    public static int MaxLen = 0;

    public static int CharCnt = 0;

    public static readonly List<Dictionary<string, KindInfo>> CharPosKind =new();

    public static int[] MaxLenForActive = Array.Empty<int>();

    public static string[] AllImages = Array.Empty<string>();

    public static int[][] IntermediateKinds;

    public static int[][] IntermediateMatchedPos;

    public static int StartStateCnt = 0;

    public static bool[] SubString = Array.Empty<bool>();

    public static bool[] SubStringAtPos = Array.Empty<bool>();

    public static Dictionary<string, long[]>[] StatesForPos = Array.Empty<Dictionary<string, long[]>>();

    public static bool BoilerPlateDumped = false;

    public RStringLiteral(Token token, string text)
    {
        Line = token.BeginLine;
        Column = token.BeginColumn;
        Image = text;
    }

    public static new void ReInit()
    {
        RegularExpression.ReInit();
        CharCnt = 0;
        AllImages = null;
        BoilerPlateDumped = false;
        MaxStrKind = 0;
        MaxLen = 0;
        MaxLenForActive = new int[100];
        IntermediateKinds = null;
        IntermediateMatchedPos = null;
        StartStateCnt = 0;
        SubString = null;
        SubStringAtPos = null;
        StatesForPos = null;
    }

    public virtual void GenerateDfa(TextWriter writer, int i)
    {
        if (MaxStrKind <= Ordinal)
        {
            MaxStrKind = Ordinal + 1;
        }
        int n;
        if ((n = Image.Length) > MaxLen)
        {
            MaxLen = n;
        }
        for (int j = 0; j < n; j++)
        {
            int n2;
            var key = ((!Options.IgnoreCase) ? ("") + ((char)(n2 = Image[j])) : (("") + ((char)(n2 = Image[j]))).ToLower());
            if (!NfaState.unicodeWarningGiven && n2 > 255 && !Options.JavaUnicodeEscape && !Options.UserCharStream)
            {
                NfaState.unicodeWarningGiven = true;
                JavaCCErrors.Warning(LexGen.curRE, "Non-ASCII characters used in regular expression.Please make sure you use the correct TextReader when you create the parser, one that can handle your character set.");
            }
            Dictionary<string, KindInfo> dict;
            if (j >= CharPosKind.Count)
            {
                CharPosKind.Add(dict = new ());
            }
            else
            {
                dict = CharPosKind[j];
            }
            KindInfo kindInfo;
            if (!dict.TryGetValue(key, out kindInfo))
            {
                dict.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
            }
            if (j + 1 == n)
            {
                kindInfo.InsertFinalKind(Ordinal);
            }
            else
            {
                kindInfo.InsertValidKind(Ordinal);
            }
            if (!Options.IgnoreCase && LexGen.ignoreCase[Ordinal] && n2 != Char.ToLower((char)n2))
            {
                key = char.ToLower(Image[j]).ToString();
                if (j >= CharPosKind.Count)
                {
                    CharPosKind.Add(dict = new ());
                }
                else
                {
                    dict = CharPosKind[j];
                }
                if (!dict.TryGetValue(key, out kindInfo))
                {
                    dict.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
                }
                if (j + 1 == n)
                {
                    kindInfo.InsertFinalKind(Ordinal);
                }
                else
                {
                    kindInfo.InsertValidKind(Ordinal);
                }
            }
            if (!Options.IgnoreCase && LexGen.ignoreCase[Ordinal] && n2 != char.ToUpper((char)n2))
            {
                key = (("") + (Image[j])).ToUpper();
                if (j >= CharPosKind.Count)
                {
                    CharPosKind.Add(dict = new());
                }
                else
                {
                    dict = CharPosKind[j];
                }
                if (!dict.TryGetValue(key,out kindInfo))
                {
                    dict.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
                }
                if (j + 1 == n)
                {
                    kindInfo.InsertFinalKind(Ordinal);
                }
                else
                {
                    kindInfo.InsertValidKind(Ordinal);
                }
            }
        }
        MaxLenForActive[Ordinal / 64] = Math.Max(MaxLenForActive[Ordinal / 64], n - 1);
        AllImages[Ordinal] = Image;
    }

    internal static void FillSubString()
    {
        SubString = new bool[MaxStrKind + 1];
        SubStringAtPos = new bool[MaxLen];
        for (int i = 0; i < MaxStrKind; i++)
        {
            SubString[i] = false;
            string text;
            if ((text = AllImages[i]) == null || LexGen.lexStates[i] != LexGen.lexStateIndex)
            {
                continue;
            }
            if (LexGen.mixed[LexGen.lexStateIndex])
            {
                SubString[i] = true;
                SubStringAtPos[text.Length - 1] = true;
                continue;
            }
            for (int j = 0; j < MaxStrKind; j++)
            {
                if (j != i && LexGen.lexStates[j] == LexGen.lexStateIndex && AllImages[j] != null)
                {
                    if ((AllImages[j].IndexOf(text)) == 0)
                    {
                        SubString[i] = true;
                        SubStringAtPos[text.Length - 1] = true;
                        break;
                    }
                    if (Options.IgnoreCase && StartsWithIgnoreCase(AllImages[j], text))
                    {
                        SubString[i] = true;
                        SubStringAtPos[text.Length - 1] = true;
                        break;
                    }
                }
            }
        }
    }


    internal static void GenerateNfaStartStates(TextWriter writer, NfaState state)
    {
        bool[] array = new bool[NfaState.generatedStates];
        Dictionary<string,string> dict = new ();
        string text = "";
        int num = MaxStrKind / 64 + 1;
        List<NfaState> vector = new();
        List<NfaState> vector2 = null;
        StatesForPos = new Dictionary<string, long[]>[MaxLen];
        IntermediateKinds = new int[MaxStrKind + 1][];
        IntermediateMatchedPos = new int[MaxStrKind + 1][];
        for (int i = 0; i < MaxStrKind; i++)
        {
            if (LexGen.lexStates[i] != LexGen.lexStateIndex)
            {
                continue;
            }
            string text2 = AllImages[i];
            if (text2 == null || text2.Length < 1)
            {
                continue;
            }
            try
            {
                if ((vector2 = state.epsilonMoves.ToList()) == null || vector2.Count == 0)
                {
                    DumpNfaStartStatesCode(StatesForPos, writer);
                    return;
                }
            }
            catch
            {
                goto IL_00e1;
            }
            goto IL_00f2;
        IL_00e1:

            JavaCCErrors.Semantic_Error("Error cloning state vector");
            goto IL_00f2;
        IL_00f2:
            IntermediateKinds[i] = new int[text2.Length];
            IntermediateMatchedPos[i] = new int[text2.Length];
            int i2 = 0;
            _ = int.MaxValue;
            for (int j = 0; j < text2.Length; j++)
            {
                int num6;
                int num3;
                if (vector2 == null || vector2.Count <= 0)
                {
                    int[] obj = IntermediateKinds[i];
                    int num2 = j;
                    num3 = IntermediateKinds[i][j - 1];
                    int num4 = num2;
                    int[] array2 = obj;
                    int num5 = num3;
                    array2[num4] = num3;
                    num6 = num5;
                    int[] obj2 = IntermediateMatchedPos[i];
                    int num7 = j;
                    num3 = IntermediateMatchedPos[i][j - 1];
                    num4 = num7;
                    array2 = obj2;
                    int num8 = num3;
                    array2[num4] = num3;
                    i2 = num8;
                }
                else
                {
                    num6 = NfaState.MoveFromSet(text2[j], vector2, vector);
                    vector2.Clear();
                    if (j == 0 && num6 != int.MaxValue && LexGen.canMatchAnyChar[LexGen.lexStateIndex] != -1 && num6 > LexGen.canMatchAnyChar[LexGen.lexStateIndex])
                    {
                        num6 = LexGen.canMatchAnyChar[LexGen.lexStateIndex];
                    }
                    if (GetStrKind(text2.Substring(0, j + 1)) < num6)
                    {
                        num6 = (IntermediateKinds[i][j] = int.MaxValue);
                        i2 = 0;
                    }
                    else if (num6 != int.MaxValue)
                    {
                        IntermediateKinds[i][j] = num6;
                        int[] obj3 = IntermediateMatchedPos[i];
                        int num9 = j;
                        num3 = j;
                        int num4 = num9;
                        int[] array2 = obj3;
                        int num10 = num3;
                        array2[num4] = num3;
                        i2 = num10;
                    }
                    else if (j == 0)
                    {
                        int[] obj4 = IntermediateKinds[i];
                        int num11 = j;
                        num3 = int.MaxValue;
                        int num4 = num11;
                        int[] array2 = obj4;
                        int num12 = num3;
                        array2[num4] = num3;
                        num6 = num12;
                    }
                    else
                    {
                        int[] obj5 = IntermediateKinds[i];
                        int num13 = j;
                        num3 = IntermediateKinds[i][j - 1];
                        int num4 = num13;
                        int[] array2 = obj5;
                        int num14 = num3;
                        array2[num4] = num3;
                        num6 = num14;
                        int[] obj6 = IntermediateMatchedPos[i];
                        int num15 = j;
                        num3 = IntermediateMatchedPos[i][j - 1];
                        num4 = num15;
                        array2 = obj6;
                        int num16 = num3;
                        array2[num4] = num3;
                        i2 = num16;
                    }
                    text = NfaState.GetStateSetString(vector);
                }
                if (num6 == int.MaxValue && (vector == null || vector.Count == 0))
                {
                    continue;
                }
                if (!dict.ContainsKey(text))
                {
                    dict.Add(text, text);
                    for (int k = 0; k < vector.Count; k++)
                    {
                        if (array[((NfaState)vector[k]).stateName])
                        {
                            ((NfaState)vector[k]).inNextOf++;
                        }
                        else
                        {
                            array[((NfaState)vector[k]).stateName] = true;
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < vector.Count; k++)
                    {
                        array[((NfaState)vector[k]).stateName] = true;
                    }
                }
                var vector3 = vector2;
                vector2 = vector;
                (vector = vector3).Clear();
                if (StatesForPos[j] == null)
                {
                    StatesForPos[j] = new ();
                }
                if (StatesForPos[j].TryGetValue((num6) + (", ") + (i2) + (", ")+ (text),out var array3))
                {
                    array3 = new long[num];
                    StatesForPos[j].Add((num6) + (", ") + (i2)
                        + (", ")
                        + (text)
                        , array3);
                }
                long[] array4 = array3;
                num3 = i / 64;
                long[] array5 = array4;
                int num17 = num3;
                long num18 = array5[num3];
                long num19 = 1L;
                int num20 = i;
                array5[num17] = num18 | (num19 << ((64 != -1) ? (num20 % 64) : 0));
            }
        }
        DumpNfaStartStatesCode(StatesForPos, writer);
    }


    internal static void DumpDfaCode(TextWriter writer)
    {
        int num = MaxStrKind / 64 + 1;
        LexGen.maxLongsReqd[LexGen.lexStateIndex] = num;
        if (MaxLen == 0)
        {
            writer.WriteLine(((!Options.Static) ? "" : "static ") + ("private int ") + ("jjMoveStringLiteralDfa0")
                + (LexGen.lexStateSuffix)
                + ("()")
                );
            DumpNullStrLiterals(writer);
            return;
        }
        if (!BoilerPlateDumped)
        {
            DumpBoilerPlate(writer);
            BoilerPlateDumped = true;
        }
        if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
        {
            DumpStartWithStates(writer);
        }
        for (int i = 0; i < MaxLen; i++)
        {
            int num2 = 0;
            int num3 = 0;
            var dict = CharPosKind[i];
            string[] array = ReArrange(dict);
            writer.Write(((!Options.Static) ? "" : "static ") + ("private int ") + ("jjMoveStringLiteralDfa")
                + (i)
                + (LexGen.lexStateSuffix)
                + ("(")
                );
            switch (i)
            {
                case 1:
                    {
                        int j;
                        for (j = 0; j < num - 1; j++)
                        {
                            if (i <= MaxLenForActive[j])
                            {
                                if (num2 != 0)
                                {
                                    writer.Write(", ");
                                }
                                else
                                {
                                    num2 = 1;
                                }
                                writer.Write(("long active") + (j));
                            }
                        }
                        if (i <= MaxLenForActive[j])
                        {
                            if (num2 != 0)
                            {
                                writer.Write(", ");
                            }
                            writer.Write(("long active") + (j));
                        }
                        break;
                    }
                default:
                    {
                        int j;
                        for (j = 0; j < num - 1; j++)
                        {
                            if (i <= MaxLenForActive[j] + 1)
                            {
                                if (num2 != 0)
                                {
                                    writer.Write(", ");
                                }
                                else
                                {
                                    num2 = 1;
                                }
                                writer.Write(("long old") + (j) + (", long active")
                                    + (j)
                                    );
                            }
                        }
                        if (i <= MaxLenForActive[j] + 1)
                        {
                            if (num2 != 0)
                            {
                                writer.Write(", ");
                            }
                            writer.Write(("long old") + (j) + (", long active")
                                + (j)
                                );
                        }
                        break;
                    }
                case 0:
                    break;
            }
            writer.WriteLine(")");
            writer.WriteLine("{");
            if (i != 0)
            {
                if (i > 1)
                {
                    num2 = 0;
                    writer.Write("   if ((");
                    int j;
                    for (j = 0; j < num - 1; j++)
                    {
                        if (i <= MaxLenForActive[j] + 1)
                        {
                            if (num2 != 0)
                            {
                                writer.Write(" | ");
                            }
                            else
                            {
                                num2 = 1;
                            }
                            writer.Write(("(active") + (j) + (" &= old")
                                + (j)
                                + (")")
                                );
                        }
                    }
                    if (i <= MaxLenForActive[j] + 1)
                    {
                        if (num2 != 0)
                        {
                            writer.Write(" | ");
                        }
                        writer.Write(("(active") + (j) + (" &= old")
                            + (j)
                            + (")")
                            );
                    }
                    writer.WriteLine(") == 0L)");
                    if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
                    {
                        writer.Write(("      return jjStartNfa") + (LexGen.lexStateSuffix) + ("(")
                            + (i - 2)
                            + (", ")
                            );
                        for (j = 0; j < num - 1; j++)
                        {
                            if (i <= MaxLenForActive[j] + 1)
                            {
                                writer.Write(("old") + (j) + (", ")
                                    );
                            }
                            else
                            {
                                writer.Write("0L, ");
                            }
                        }
                        if (i <= MaxLenForActive[j] + 1)
                        {
                            writer.WriteLine(("old") + (j) + ("); ")
                                );
                        }
                        else
                        {
                            writer.WriteLine("0L);");
                        }
                    }
                    else if (NfaState.generatedStates != 0)
                    {
                        writer.WriteLine(("      return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                            + (NfaState.InitStateName())
                            + (", ")
                            + (i - 1)
                            + (");")
                            );
                    }
                    else
                    {
                        writer.WriteLine(("      return ") + (i) + (";")
                            );
                    }
                }
                if (i != 0 && Options.DebugTokenManager)
                {
                    writer.WriteLine(("   if (jjmatchedKind != 0 && jjmatchedKind != 0x") + (Utils.ToString(int.MaxValue, 16)) + (")")
                        );
                    writer.WriteLine("      debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
                    writer.WriteLine("   debugStream.WriteLine(\"   Possible string literal matches : { \"");
                    for (int k = 0; k < MaxStrKind / 64 + 1; k++)
                    {
                        if (i <= MaxLenForActive[k])
                        {
                            writer.WriteLine(" + ");
                            writer.Write(("         jjKindsForBitVector(") + (k) + (", ")
                                );
                            writer.Write(("active") + (k) + (") ")
                                );
                        }
                    }
                    writer.WriteLine(" + \" } \");");
                }
                writer.WriteLine("   try { curChar = input_stream.readChar(); }");
                writer.WriteLine("   catch(java.io.IOException e) {");
                if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
                {
                    writer.Write(("      jjStopStringLiteralDfa") + (LexGen.lexStateSuffix) + ("(")
                        + (i - 1)
                        + (", ")
                        );
                    int l;
                    for (l = 0; l < num - 1; l++)
                    {
                        if (i <= MaxLenForActive[l])
                        {
                            writer.Write(("active") + (l) + (", ")
                                );
                        }
                        else
                        {
                            writer.Write("0L, ");
                        }
                    }
                    if (i <= MaxLenForActive[l])
                    {
                        writer.WriteLine(("active") + (l) + (");")
                            );
                    }
                    else
                    {
                        writer.WriteLine("0L);");
                    }
                    if (i != 0 && Options.DebugTokenManager)
                    {
                        writer.WriteLine(("      if (jjmatchedKind != 0 && jjmatchedKind != 0x") + (Utils.ToString(int.MaxValue, 16)) + (")")
                            );
                        writer.WriteLine("         debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
                    }
                    writer.WriteLine(("      return ") + (i) + (";")
                        );
                }
                else if (NfaState.generatedStates != 0)
                {
                    writer.WriteLine(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                        + (NfaState.InitStateName())
                        + (", ")
                        + (i - 1)
                        + (");")
                        );
                }
                else
                {
                    writer.WriteLine(("      return ") + (i) + (";")
                        );
                }
                writer.WriteLine("   }");
            }
            if (i != 0 && Options.DebugTokenManager)
            {
                writer.WriteLine(("   debugStream.WriteLine(") + ((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ") + ("\"Current character : \" + ")
                    + ("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
                    + ("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
                    );
            }
            writer.WriteLine("   switch(curChar)");
            writer.WriteLine("   {");
            for (int k = 0; k < array.Length; k++)
            {
                string text = array[k];
                dict.TryGetValue(text, out var kindInfo);
                int num4 = 0;
                int num5 = text[0];
                int num6;
                if (i == 0 && num5 < 128 && kindInfo.VinalKindCnt != 0 && (NfaState.generatedStates == 0 || !NfaState.CanStartNfaUsingAscii((char)num5)))
                {
                    int j;
                    for (j = 0; j < num && kindInfo.FinalKinds[j] == 0; j++)
                    {
                    }
                    for (int l = 0; l < 64; l++)
                    {
                        if ((kindInfo.FinalKinds[j] & (1L << l)) == 0 || SubString[num6 = j * 64 + l])
                        {
                            continue;
                        }
                        if ((IntermediateKinds != null && IntermediateKinds[j * 64 + l] != null && IntermediateKinds[j * 64 + l][i] < j * 64 + l && IntermediateMatchedPos != null && IntermediateMatchedPos[j * 64 + l][i] == i) || (LexGen.canMatchAnyChar[LexGen.lexStateIndex] >= 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] < j * 64 + l))
                        {
                            break;
                        }
                        if ((LexGen.toSkip[num6 / 64] & (1L << ((64 != -1) ? (num6 % 64) : 0))) == 0)
                        {
                            continue;
                        }
                        if ((LexGen.toSpecial[num6 / 64] & (1L << ((64 != -1) ? (num6 % 64) : 0))) != 0 || LexGen.actions[num6] != null || LexGen.newLexState[num6] != null)
                        {
                            continue;
                        }
                        goto IL_0a09;
                    }
                }
                if (Options.IgnoreCase)
                {
                    if (num5 != char.ToUpper((char)num5))
                    {
                        writer.WriteLine(("      case ") + ((int)char.ToUpper((char)num5)) + (":")
                            );
                    }
                    if (num5 != Char.ToLower((char)num5))
                    {
                        writer.WriteLine(("      case ") + ((int)Char.ToLower((char)num5)) + (":")
                            );
                    }
                }
                writer.WriteLine(("      case ") + (num5) + (":")
                    );
                string str = ((i != 0) ? "            " : "         ");
                if (kindInfo.VinalKindCnt != 0)
                {
                    for (int j = 0; j < num; j++)
                    {
                        long num7;
                        if ((num7 = kindInfo.FinalKinds[j]) == 0)
                        {
                            continue;
                        }
                        for (int l = 0; l < 64; l++)
                        {
                            if ((num7 & (1L << l)) == 0)
                            {
                                continue;
                            }
                            if (num4 != 0)
                            {
                                writer.Write("         else if ");
                            }
                            else if (i != 0)
                            {
                                writer.Write("         if ");
                            }
                            num4 = 1;
                            if (i != 0)
                            {
                                writer.WriteLine(("((active") + (j) + (" & 0x")
                                    + (Utils.ToHexString(1L << l))
                                    + ("L) != 0L)")
                                    );
                            }
                            int i2;
                            if (IntermediateKinds != null && IntermediateKinds[j * 64 + l] != null && IntermediateKinds[j * 64 + l][i] < j * 64 + l && IntermediateMatchedPos != null && IntermediateMatchedPos[j * 64 + l][i] == i)
                            {
                                JavaCCErrors.Warning((" \"") + (JavaCCGlobals.AddEscapes(AllImages[j * 64 + l])) + ("\" cannot be matched as a string literal token ")
                                    + ("at line ")
                                    + (GetLine(j * 64 + l))
                                    + (", column ")
                                    + (GetColumn(j * 64 + l))
                                    + (". It will be matched as ")
                                    + (GetLabel(IntermediateKinds[j * 64 + l][i]))
                                    + (".")
                                    );
                                i2 = IntermediateKinds[j * 64 + l][i];
                            }
                            else if (i == 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] >= 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] < j * 64 + l)
                            {
                                JavaCCErrors.Warning((" \"") + (JavaCCGlobals.AddEscapes(AllImages[j * 64 + l])) + ("\" cannot be matched as a string literal token ")
                                    + ("at line ")
                                    + (GetLine(j * 64 + l))
                                    + (", column ")
                                    + (GetColumn(j * 64 + l))
                                    + (". It will be matched as ")
                                    + (GetLabel(LexGen.canMatchAnyChar[LexGen.lexStateIndex]))
                                    + (".")
                                    );
                                i2 = LexGen.canMatchAnyChar[LexGen.lexStateIndex];
                            }
                            else
                            {
                                i2 = j * 64 + l;
                            }
                            if (!SubString[j * 64 + l])
                            {
                                int stateSetForKind = GetStateSetForKind(i, j * 64 + l);
                                if (stateSetForKind != -1)
                                {
                                    writer.WriteLine((str) + ("return jjStartNfaWithStates") + (LexGen.lexStateSuffix)
                                        + ("(")
                                        + (i)
                                        + (", ")
                                        + (i2)
                                        + (", ")
                                        + (stateSetForKind)
                                        + (");")
                                        );
                                }
                                else
                                {
                                    writer.WriteLine((str) + ("return jjStopAtPos") + ("(")
                                        + (i)
                                        + (", ")
                                        + (i2)
                                        + (");")
                                        );
                                }
                            }
                            else if ((LexGen.initMatch[LexGen.lexStateIndex] != 0 && LexGen.initMatch[LexGen.lexStateIndex] != int.MaxValue) || i != 0)
                            {
                                writer.WriteLine("         {");
                                writer.WriteLine((str) + ("jjmatchedKind = ") + (i2)
                                    + (";")
                                    );
                                writer.WriteLine((str) + ("jjmatchedPos = ") + (i)
                                    + (";")
                                    );
                                writer.WriteLine("         }");
                            }
                            else
                            {
                                writer.WriteLine((str) + ("jjmatchedKind = ") + (i2)
                                    + (";")
                                    );
                            }
                        }
                    }
                }
                if (kindInfo.ValidKindCnt != 0)
                {
                    num2 = 0;
                    int j;
                    if (i == 0)
                    {
                        writer.Write("         return ");
                        writer.Write(("jjMoveStringLiteralDfa") + (i + 1) + (LexGen.lexStateSuffix)
                            + ("(")
                            );
                        for (j = 0; j < num - 1; j++)
                        {
                            if (i + 1 <= MaxLenForActive[j])
                            {
                                if (num2 != 0)
                                {
                                    writer.Write(", ");
                                }
                                else
                                {
                                    num2 = 1;
                                }
                                writer.Write(("0x") + (Utils.ToHexString(kindInfo.ValidKinds[j])) + ("L")
                                    );
                            }
                        }
                        if (i + 1 <= MaxLenForActive[j])
                        {
                            if (num2 != 0)
                            {
                                writer.Write(", ");
                            }
                            writer.Write(("0x") + (Utils.ToHexString(kindInfo.ValidKinds[j])) + ("L")
                                );
                        }
                        writer.WriteLine(");");
                        continue;
                    }
                    writer.Write("         return ");
                    writer.Write(("jjMoveStringLiteralDfa") + (i + 1) + (LexGen.lexStateSuffix)
                        + ("(")
                        );
                    for (j = 0; j < num - 1; j++)
                    {
                        if (i + 1 <= MaxLenForActive[j] + 1)
                        {
                            if (num2 != 0)
                            {
                                writer.Write(", ");
                            }
                            else
                            {
                                num2 = 1;
                            }
                            if (kindInfo.ValidKinds[j] != 0)
                            {
                                writer.Write(("active") + (j) + (", 0x")
                                    + (Utils.ToHexString(kindInfo.ValidKinds[j]))
                                    + ("L")
                                    );
                            }
                            else
                            {
                                writer.Write(("active") + (j) + (", 0L")
                                    );
                            }
                        }
                    }
                    if (i + 1 <= MaxLenForActive[j] + 1)
                    {
                        if (num2 != 0)
                        {
                            writer.Write(", ");
                        }
                        if (kindInfo.ValidKinds[j] != 0)
                        {
                            writer.Write(("active") + (j) + (", 0x")
                                + (Utils.ToHexString(kindInfo.ValidKinds[j]))
                                + ("L")
                                );
                        }
                        else
                        {
                            writer.Write(("active") + (j) + (", 0L")
                                );
                        }
                    }
                    writer.WriteLine(");");
                }
                else if (i == 0 && LexGen.mixed[LexGen.lexStateIndex])
                {
                    if (NfaState.generatedStates != 0)
                    {
                        writer.WriteLine(("         return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                            + (NfaState.InitStateName())
                            + (", 0);")
                            );
                    }
                    else
                    {
                        writer.WriteLine("         return 1;");
                    }
                }
                else if (i != 0)
                {
                    writer.WriteLine("         break;");
                    num3 = 1;
                }
                continue;
            IL_0a09:
                LexGen.AddCharToSkip((char)num5, num6);
                if (Options.IgnoreCase)
                {
                    if (num5 != char.ToUpper((char)num5))
                    {
                        LexGen.AddCharToSkip(char.ToUpper((char)num5), num6);
                    }
                    if (num5 != Char.ToLower((char)num5))
                    {
                        LexGen.AddCharToSkip(Char.ToLower((char)num5), num6);
                    }
                }
            }
            writer.WriteLine("      default :");
            if (Options.DebugTokenManager)
            {
                writer.WriteLine("      debugStream.WriteLine(\"   No string literal matches possible.\");");
            }
            if (NfaState.generatedStates != 0)
            {
                if (i == 0)
                {
                    writer.WriteLine(("         return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                        + (NfaState.InitStateName())
                        + (", 0);")
                        );
                }
                else
                {
                    writer.WriteLine("         break;");
                    num3 = 1;
                }
            }
            else
            {
                writer.WriteLine(("         return ") + (i + 1) + (";")
                    );
            }
            writer.WriteLine("   }");
            if (i != 0 && num3 != 0)
            {
                if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
                {
                    writer.Write(("   return jjStartNfa") + (LexGen.lexStateSuffix) + ("(")
                        + (i - 1)
                        + (", ")
                        );
                    int l;
                    for (l = 0; l < num - 1; l++)
                    {
                        if (i <= MaxLenForActive[l])
                        {
                            writer.Write(("active") + (l) + (", ")
                                );
                        }
                        else
                        {
                            writer.Write("0L, ");
                        }
                    }
                    if (i <= MaxLenForActive[l])
                    {
                        writer.WriteLine(("active") + (l) + (");")
                            );
                    }
                    else
                    {
                        writer.WriteLine("0L);");
                    }
                }
                else if (NfaState.generatedStates != 0)
                {
                    writer.WriteLine(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                        + (NfaState.InitStateName())
                        + (", ")
                        + (i)
                        + (");")
                        );
                }
                else
                {
                    writer.WriteLine(("   return ") + (i + 1) + (";")
                        );
                }
            }
            writer.WriteLine("}");
        }
    }


    public static void DumpStrLiteralImages(TextWriter pw)
    {
        CharCnt = 0;
        pw.WriteLine("");
        pw.WriteLine("/** Token literal values. */");
        pw.WriteLine("public static final String[] jjstrLiteralImages = {");
        if (AllImages == null || AllImages.Length == 0)
        {
            pw.WriteLine("};");
            return;
        }
        AllImages[0] = "";
        int i;
        for (i = 0; i < AllImages.Length; i++)
        {
            string @this;
            if ((@this = AllImages[i]) != null)
            {
                long num = LexGen.toSkip[i / 64];
                long num2 = 1L;
                int num3 = i;
                if ((num & (num2 << ((64 != -1) ? (num3 % 64) : 0))) == 0)
                {
                    long num4 = LexGen.toMore[i / 64];
                    long num5 = 1L;
                    int num6 = i;
                    if ((num4 & (num5 << ((64 != -1) ? (num6 % 64) : 0))) == 0)
                    {
                        long num7 = LexGen.toToken[i / 64];
                        long num8 = 1L;
                        int num9 = i;
                        if ((num7 & (num8 << ((64 != -1) ? (num9 % 64) : 0))) == 0)
                        {
                            goto IL_0157;
                        }
                    }
                }
                long num10 = LexGen.toSkip[i / 64];
                long num11 = 1L;
                int num12 = i;
                if ((num10 & (num11 << ((64 != -1) ? (num12 % 64) : 0))) == 0)
                {
                    long num13 = LexGen.toMore[i / 64];
                    long num14 = 1L;
                    int num15 = i;
                    if ((num13 & (num14 << ((64 != -1) ? (num15 % 64) : 0))) == 0 && !LexGen.canReachOnMore[LexGen.lexStates[i]] && ((!Options.IgnoreCase && !LexGen.ignoreCase[i]) || (string.Equals(@this, (@this).ToLower()) && string.Equals(@this, (@this.ToUpper())))))
                    {
                        string str = "\"";
                        for (int j = 0; j < @this.Length; j++)
                        {
                            if (@this[j] <= 'ÿ')
                            {
                                str = (str) + ("\\") + (Utils.ToOctString(@this[j]))
                                    ;
                                continue;
                            }
                            string text = Utils.ToHexString(@this[j]);
                            if (text.Length == 3)
                            {
                                text = ("0") + (text);
                            }
                            str = (str) + ("\\u") + (text)
                                ;
                        }
                        str = (str) + ("\", ");
                        if ((CharCnt += str.Length) >= 80)
                        {
                            pw.WriteLine("");
                            CharCnt = 0;
                        }
                        pw.Write(str);
                        continue;
                    }
                }
            }
            goto IL_0157;
        IL_0157:
            AllImages[i] = null;
            if ((CharCnt += 6) > 80)
            {
                pw.WriteLine("");
                CharCnt = 0;
            }
            pw.Write("null, ");
        }
        while (true)
        {
            i++;
            if (i >= LexGen.maxOrdinal)
            {
                break;
            }
            if ((CharCnt += 6) > 80)
            {
                pw.WriteLine("");
                CharCnt = 0;
            }
            pw.Write("null, ");
        }
        pw.WriteLine("};");
    }



    private static bool StartsWithIgnoreCase(string P_0, string P_1)
    {
        if (P_0.Length < P_1.Length)
        {
            return false;
        }
        for (int i = 0; i < P_1.Length; i++)
        {
            int num = P_0[i];
            int num2 = P_1[i];
            if (num != num2 && Char.ToLower((char)num2) != num && char.ToUpper((char)num2) != num)
            {
                return false;
            }
        }
        return true;
    }


    internal static void DumpNullStrLiterals(TextWriter writer)
    {
        writer.WriteLine("{");
        if (NfaState.generatedStates != 0)
        {
            writer.WriteLine(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                + (NfaState.InitStateName())
                + (", 0);")
                );
        }
        else
        {
            writer.WriteLine("   return 1;");
        }
        writer.WriteLine("}");
    }


    internal static void DumpBoilerPlate(TextWriter writer)
    {
        writer.WriteLine(((!Options.Static) ? "" : "static ") + ("private int ") + ("jjStopAtPos(int pos, int kind)")
            );
        writer.WriteLine("{");
        writer.WriteLine("   jjmatchedKind = kind;");
        writer.WriteLine("   jjmatchedPos = pos;");
        if (Options.DebugTokenManager)
        {
            writer.WriteLine("   debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
            writer.WriteLine("   debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
        }
        writer.WriteLine("   return pos + 1;");
        writer.WriteLine("}");
    }


    internal static void DumpStartWithStates(TextWriter writer)
    {
        writer.WriteLine(((!Options.Static) ? "" : "static ") + ("private int ") + ("jjStartNfaWithStates")
            + (LexGen.lexStateSuffix)
            + ("(int pos, int kind, int state)")
            );
        writer.WriteLine("{");
        writer.WriteLine("   jjmatchedKind = kind;");
        writer.WriteLine("   jjmatchedPos = pos;");
        if (Options.DebugTokenManager)
        {
            writer.WriteLine("   debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
            writer.WriteLine("   debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
        }
        writer.WriteLine("   try { curChar = input_stream.readChar(); }");
        writer.WriteLine("   catch(java.io.IOException e) { return pos + 1; }");
        if (Options.DebugTokenManager)
        {
            writer.WriteLine(("   debugStream.WriteLine(") + ((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ") + ("\"Current character : \" + ")
                + ("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
                + ("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
                );
        }
        writer.WriteLine(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(state, pos + 1);")
            );
        writer.WriteLine("}");
    }


    internal static string[] ReArrange(Dictionary<string, KindInfo> P_0)
    {
        string[] array = new string[P_0.Count];
        int num = 0;
        foreach(var pair in P_0)
        {
            int i = 0;
            string text;
            for (int num2 = (text = pair.Key)[0]; i < num && array[i][0] < num2; i++)
            {
            }
            if (i < num)
            {
                for (int j = num - 1; j >= i; j += -1)
                {
                    array[j + 1] = array[j];
                }
            }
            array[i] = text;
            num++;
        }
        return array;
    }


    internal static int GetLine(int index)
    {
        return LexGen.rexprs[index].Line;
    }


    internal static int GetColumn(int index)
    {
        return LexGen.rexprs[index].Column;
    }


    internal static string GetLabel(int P_0)
    {
        var regularExpression = LexGen.rexprs[P_0];
        if (regularExpression is RStringLiteral)
        {
            string result = (" \"") + (JavaCCGlobals.AddEscapes(((RStringLiteral)regularExpression).Image)) + ("\"")
                ;

            return result;
        }
        if (!string.Equals(regularExpression.Label, ""))
        {
            string result2 = (" <") + (regularExpression.Label) + (">")
                ;

            return result2;
        }
        string result3 = (" <token of kind ") + (P_0) + (">")
            ;

        return result3;
    }


    private static int GetStateSetForKind(int pos, int pos2)
    {
        if (LexGen.mixed[LexGen.lexStateIndex] || NfaState.generatedStates == 0)
        {
            return -1;
        }
        var dict = StatesForPos[pos];
        if (dict == null)
        {
            return -1;
        }
        foreach(var pair in dict)
        {
            string text = pair.Key;
            long[] array = pair.Value;
            text = text.Substring(text.IndexOf(", ") + 2);
            text = text.Substring(text.IndexOf(", ") + 2);
            if (!string.Equals(text, "null;") && array != null)
            {
                if ((array[pos2 / 64] & (1L << ((64 != -1) ? (pos2 % 64) : 0))) != 0)
                {
                    int result = NfaState.AddStartStateSet(text);

                    return result;
                }
            }
        }
        return -1;
    }


    internal static void DumpNfaStartStatesCode(Dictionary<string, long[]>[] dict, TextWriter writer)
    {
        if (MaxStrKind == 0)
        {
            return;
        }
        int num = MaxStrKind / 64 + 1;
        int num2 = 0;
        
        writer.Write(("private") + ((!Options.Static) ? "" : " static") + (" final int jjStopStringLiteralDfa")
            + (LexGen.lexStateSuffix)
            + ("(int pos, ")
            );
        int i;
        for (i = 0; i < num - 1; i++)
        {
            writer.Write(("long active") + (i) + (", ")
                );
        }
        writer.WriteLine(("long active") + (i) + (")\n{")
            );
        if (Options.DebugTokenManager)
        {
            writer.WriteLine("      debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
        }
        writer.WriteLine("   switch (pos)\n   {");
        for (i = 0; i < MaxLen - 1; i++)
        {
            if (dict[i] == null)
            {
                continue;
            }
            writer.WriteLine(("      case ") + (i) + (":")
                );
            foreach(var pair in dict[i])
            {
                string text = pair.Key;
                long[] array = pair.Value;
                for (int j = 0; j < num; j++)
                {
                    if (array[j] != 0)
                    {
                        if (num2 != 0)
                        {
                            writer.Write(" || ");
                        }
                        else
                        {
                            writer.Write("         if (");
                        }
                        num2 = 1;
                        writer.Write(("(active") + (j) + (" & 0x")
                            + (Utils.ToHexString(array[j]))
                            + ("L) != 0L")
                            );
                    }
                }
                if (num2 == 0)
                {
                    continue;
                }
                writer.WriteLine(")");
                int num3;
                string text2 = text.Substring(0, num3 = text.IndexOf(", "));
                string @this = text.Substring(num3 + 2);
                int.TryParse((@this.Substring(0, (@this.IndexOf(", ")))),out var num4);
                if (!string.Equals(text2, int.MaxValue.ToString()))
                {
                    writer.WriteLine("         {");
                }
                if (!string.Equals(text2, (int.MaxValue.ToString())))
                {
                    if (i == 0)
                    {
                        writer.WriteLine(("            jjmatchedKind = ") + (text2) + (";")
                            );
                        if (LexGen.initMatch[LexGen.lexStateIndex] != 0 && LexGen.initMatch[LexGen.lexStateIndex] != int.MaxValue)
                        {
                            writer.WriteLine("            jjmatchedPos = 0;");
                        }
                    }
                    else if (i == num4)
                    {
                        if (SubStringAtPos[i])
                        {
                            writer.WriteLine(("            if (jjmatchedPos != ") + (i) + (")")
                                );
                            writer.WriteLine("            {");
                            writer.WriteLine(("               jjmatchedKind = ") + (text2) + (";")
                                );
                            writer.WriteLine(("               jjmatchedPos = ") + (i) + (";")
                                );
                            writer.WriteLine("            }");
                        }
                        else
                        {
                            writer.WriteLine(("            jjmatchedKind = ") + (text2) + (";")
                                );
                            writer.WriteLine(("            jjmatchedPos = ") + (i) + (";")
                                );
                        }
                    }
                    else
                    {
                        if (num4 > 0)
                        {
                            writer.WriteLine(("            if (jjmatchedPos < ") + (num4) + (")")
                                );
                        }
                        else
                        {
                            writer.WriteLine("            if (jjmatchedPos == 0)");
                        }
                        writer.WriteLine("            {");
                        writer.WriteLine(("               jjmatchedKind = ") + (text2) + (";")
                            );
                        writer.WriteLine(("               jjmatchedPos = ") + (num4) + (";")
                            );
                        writer.WriteLine("            }");
                    }
                }
                text2 = text.Substring(0, num3 = text.IndexOf(", "));
                @this = text.Substring(num3 + 2);
                text = (@this.Substring(@this.IndexOf(", ") + 2));
                if (string.Equals(text, "null;"))
                {
                    writer.WriteLine("            return -1;");
                }
                else
                {
                    writer.WriteLine(("            return ") + (NfaState.AddStartStateSet(text)) + (";")
                        );
                }
                if (!string.Equals(text2, (int.MaxValue).ToString()))
                {
                    writer.WriteLine("         }");
                }
                num2 = 0;
            }
            writer.WriteLine("         return -1;");
        }
        writer.WriteLine("      default :");
        writer.WriteLine("         return -1;");
        writer.WriteLine("   }");
        writer.WriteLine("}");
        writer.Write(("private") + ((!Options.Static) ? "" : " static") + (" final int jjStartNfa")
            + (LexGen.lexStateSuffix)
            + ("(int pos, ")
            );
        for (i = 0; i < num - 1; i++)
        {
            writer.Write(("long active") + (i) + (", ")
                );
        }
        writer.WriteLine(("long active") + (i) + (")\n{")
            );
        if (LexGen.mixed[LexGen.lexStateIndex])
        {
            if (NfaState.generatedStates != 0)
            {
                writer.WriteLine(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
                    + (NfaState.InitStateName())
                    + (", pos + 1);")
                    );
            }
            else
            {
                writer.WriteLine("   return pos + 1;");
            }
            writer.WriteLine("}");
            return;
        }
        writer.Write(("   return jjMoveNfa") + (LexGen.lexStateSuffix) + ("(")
            + ("jjStopStringLiteralDfa")
            + (LexGen.lexStateSuffix)
            + ("(pos, ")
            );
        for (i = 0; i < num - 1; i++)
        {
            writer.Write(("active") + (i) + (", ")
                );
        }
        writer.Write(("active") + (i) + (")")
            );
        writer.WriteLine(", pos + 1);");
        writer.WriteLine("}");
    }



    internal static int GetStrKind(string P_0)
    {
        for (int i = 0; i < MaxStrKind; i++)
        {
            if (LexGen.lexStates[i] == LexGen.lexStateIndex)
            {
                string text = AllImages[i];
                if (text != null && string.Equals(text, P_0))
                {
                    return i;
                }
            }
        }
        return int.MaxValue;
    }


    public RStringLiteral()
    {
    }


    public override Nfa GenerateNfa(bool b)
    {
        if (Image.Length == 1)
        {
            RCharacterList rCharacterList = new RCharacterList(Image[0]);
            Nfa result = rCharacterList.GenerateNfa(b);

            return result;
        }
        NfaState nfaState = new NfaState();
        NfaState nfaState2 = nfaState;
        NfaState nfaState3 = null;
        if (Image.Length == 0)
        {
            Nfa result2 = new Nfa(nfaState2, nfaState2);

            return result2;
        }
        for (int i = 0; i < Image.Length; i++)
        {
            nfaState3 = new NfaState();
            nfaState.charMoves = new char[1];
            nfaState.AddChar(Image[i]);
            if (Options.IgnoreCase || b)
            {
                nfaState.AddChar(Char.ToLower(Image[i]));
                nfaState.AddChar(char.ToUpper(Image[i]));
            }
            nfaState.next = nfaState3;
            nfaState = nfaState3;
        }
        Nfa result3 = new Nfa(nfaState2, nfaState3);

        return result3;
    }


    public override StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        return base.Dump(i, s).Append(' ').Append(Image);
    }


    public override string ToString()
    {
        return (base.ToString()) + (" - ") + (Image);
    }

    static RStringLiteral()
    {
        MaxStrKind = 0;
        MaxLen = 0;
        CharCnt = 0;
        CharPosKind = new ();
        MaxLenForActive = new int[100];
        StartStateCnt = 0;
        BoilerPlateDumped = false;
    }
}
