namespace JavaCC.Parser;
using System;
using System.Collections.Generic;
using System.IO;

public class ParseEngine : JavaCCGlobals
{
	protected static TextWriter writer;

	protected static int GenSymindex = 0;

	protected static int Indentamt = 0;

	protected static bool JJ2LA = false;

	protected static readonly List<Lookahead> Phase2List =new();

	protected static readonly List<Phase3Data> Phase3List = new();

	protected static readonly Dictionary<Expansion,Phase3Data> Phase3Table =new();

	protected static bool[] FirstSet = Array.Empty<bool>();

	protected internal const int NOOPENSTM = 0;

	protected internal const int OPENIF = 1;

	protected internal const int OPENSWITCH = 2;

	protected static bool xsp_declared = false;

	protected internal static Expansion jj3_expansion;

	public new static void ReInit()
	{
		writer = null;
		GenSymindex = 0;
		Indentamt = 0;
		JJ2LA = false;
		Phase2List.Clear();
		Phase3List.Clear();
		Phase3Table.Clear();
		FirstSet = null;
		xsp_declared = false;
		jj3_expansion = null;
	}

	
	private static bool JavaCodeCheck(Expansion exp)
	{
        switch (exp)
        {
            case RegularExpression:
                return false;
            case NonTerminal terminal:
                {
                    NormalProduction prod = terminal.Production;
                    if (prod is JavaCodeProduction)
                    {
                        return true;
                    }
                    return JavaCodeCheck(prod.Expansion);
                }

            case Choice choice:
                {
                    for (int i = 0; i < choice.Choices.Count; i++)
                    {
                        if (JavaCodeCheck(choice.Choices[i]))
                        {
                            return true;
                        }
                    }
                    return false;
                }

            case Sequence sequence:
                {
                    for (int i = 0; i < sequence.Units.Count; i++)
                    {
                        var array = sequence.Units.ToArray();
                        if (array[i] is Lookahead lookahead && lookahead.IsExplicit)
                        {
                            return false;
                        }
                        else if (JavaCodeCheck(array[i]))
                        {
                            return true;
                        }
                        else if (!Semanticize.EmptyExpansionExists(array[i]))
                        {
                            return false;
                        }
                    }
                    return false;
                }

            case OneOrMore oneOrMore:
                return JavaCodeCheck(oneOrMore.Expansion);
            case ZeroOrMore zeroOrMore:
                return JavaCodeCheck(zeroOrMore.Expansion);
            case ZeroOrOne zeroOrOne:
                return JavaCodeCheck(zeroOrOne.Expansion);
            case TryBlock tryBlock:
                return JavaCodeCheck(tryBlock.Expression);
            default:
                return false;
        }
    }

	
	private static void GenFirstSet(Expansion exp)
	{
		if (exp is RegularExpression expression)
		{
			FirstSet[expression.Ordinal] = true;
		}
		else if (exp is NonTerminal terminal)
		{
			if (terminal.Production is not JavaCodeProduction)
			{
				GenFirstSet(((BNFProduction)terminal.Production).Expansion);
			}
		}
		else if (exp is Choice choice)
        {
            for (int i = 0; i < choice.Choices.Count; i++)
            {
                GenFirstSet(choice.Choices[i]);
            }
        }
        else if (exp is Sequence sequence)
        {
            var u = sequence.Units[0];
            if (u is Lookahead lookahead && lookahead.ActionTokens.Count != 0)
            {
                JJ2LA = true;
            }
            for (int j = 0; j < sequence.Units.Count; j++)
            {
                var expansion = sequence.Units[j];
                if (expansion is NonTerminal terminal1 && terminal1.Production is JavaCodeProduction)
                {
                    if (j > 0 && sequence.Units[j - 1] is Lookahead)
                    {
                        Lookahead lookahead2 = (Lookahead)sequence.Units[j - 1];
                        GenFirstSet(lookahead2.LaExpansion);
                    }
                }
                else
                {
                    GenFirstSet(sequence.Units[j]);
                }
                if (!Semanticize.EmptyExpansionExists(sequence.Units[j]))
                {
                    break;
                }
            }
        }
        else if (exp is OneOrMore oneOrMore)
        {
            GenFirstSet(oneOrMore.Expansion);
        }
        else if (exp is ZeroOrMore zeroOrMore)
        {
            GenFirstSet(zeroOrMore.Expansion);
        }
        else if (exp is ZeroOrOne zeroOrOne)
        {
            GenFirstSet(zeroOrOne.Expansion);
        }
        else if (exp is TryBlock tryBlock)
        {
            GenFirstSet(tryBlock.Expression);
        }
    }

	
	public static void Phase1NewLine()
	{
		writer.WriteLine("");
		for (int i = 0; i < Indentamt; i++)
		{
			writer.Write(" ");
		}
	}


	public static string Phase1ExpansionGen(Expansion exp)
	{
		string text = "";
		Token t = null;
        if (exp is RegularExpression regularExpression)
        {
            text = (text) + ("\n");
            if (regularExpression.LhsTokens.Count != 0)
            {
                PrintTokenSetup(regularExpression.LhsTokens[0]);
                foreach (var _t in regularExpression.LhsTokens)
                {
                    text = (text) + (PrintToken(_t));
                    t = _t;
                }
                text = (text) + (PrintTrailingComments(t));
                text = (text) + (" = ");
            }
            string str = ((regularExpression.RhsToken != null) ? (").") + (regularExpression.RhsToken.Image) + (";")
                 : ");");
            if (string.Equals(regularExpression.Label, ""))
            {
                var dict = NamesOfTokens;
                var obj = dict.ContainsKey((regularExpression.Ordinal));
                text = ((obj) ? (text) + ("jj_consume_token(") + (regularExpression.Ordinal)
                    + (str)
                     : (text) + ("jj_consume_token(") + (dict[regularExpression.Ordinal])
                    + (str)
                    );
            }
            else
            {
                text = (text) + ("jj_consume_token(") + (regularExpression.Label)
                    + (str)
                    ;
            }
        }
        else if (exp is NonTerminal nonTerminal)
        {
            text = (text) + ("\n");
            if (nonTerminal.LhsTokens.Count != 0)
            {
                foreach (Token _t in nonTerminal.LhsTokens)
                {
                    text = (text) + (PrintToken(_t));
                    t = _t;
                }
                text = (text) + (PrintTrailingComments(t));
                text = (text) + (" = ");
            }
            text = (text) + (nonTerminal.Name) + ("(")
                ;
            if (nonTerminal.ArgumentTokens.Count != 0)
            {
                Token t2 = nonTerminal.ArgumentTokens[0];
                PrintTokenSetup(t2);
                foreach (var token in nonTerminal.ArgumentTokens)
                {
                    text = (text) + (PrintToken(token));
                    t = token;
                }
                text = (text) + (PrintTrailingComments(t));
            }
            text = (text) + (");");
        }
        else if (exp is Action action)
        {
            text = (text) + ("\u0003\n");
            if (action.ActionTokens.Count != 0)
            {
                PrintTokenSetup(action.ActionTokens[0]);
                CCol = 1;
                foreach (var _t in action.ActionTokens)
                {
                    text = (text) + (PrintToken(_t));
                    t = _t;
                }
                text = (text) + (PrintTrailingComments(t));
            }
            text = (text) + ("\u0004");
        }
        else if (exp is Choice choice)
        {
            var array = new Lookahead[choice.Choices.Count];
            var array2 = new string[choice.Choices.Count + 1];
            array2[choice.Choices.Count] = "\njj_consume_token(-1);\nthrow new ParseException();";
            for (int i = 0; i < choice.Choices.Count; i++)
            {
                Sequence sequence = (Sequence)choice.Choices[i];
                array2[i] = Phase1ExpansionGen(sequence);
                array[i] = (Lookahead)sequence.Units[0];
            }
            text = BuildLookaheadChecker(array, array2);
        }
        else if (exp is Sequence sequence2)
        {
            for (int j = 1; j < sequence2.Units.Count; j++)
            {
                text = (text) + (Phase1ExpansionGen(sequence2.Units[j]));
            }
        }
        else if (exp is OneOrMore oneOrMore)
        {
            Expansion expansion = oneOrMore.Expansion;
            Lookahead lookahead;
            if (expansion is Sequence sequence)
            {
                lookahead = (Lookahead)sequence.Units[0];
            }
            else
            {
                lookahead = new ();
                lookahead.amount = Options.Lookahead;
                lookahead.LaExpansion = expansion;
            }
            text = (text) + ("\n");
            int i2 = ++GenSymindex;
            text = (text) + ("label_") + (i2)
                + (":\n")
                ;
            text = (text) + ("while (true) {\u0001");
            text = (text) + (Phase1ExpansionGen(expansion));
            var array = new Lookahead[1] { lookahead };
            var array2 = new string[2]
            {
                "\n;",
                ("\nbreak label_")+(i2)+(";")

            };
            text = (text) + (BuildLookaheadChecker(array, array2));
            text = (text) + ("\u0002\n}");
        }
        else if (exp is ZeroOrMore zeroOrMore)
        {
            Expansion expansion = zeroOrMore.Expansion;
            Lookahead lookahead;
            if (expansion is Sequence sequence)
            {
                lookahead = (Lookahead)sequence.Units[0];
            }
            else
            {
                lookahead = new ();
                lookahead.amount = Options.Lookahead;
                lookahead.LaExpansion = expansion;
            }
            text = (text) + ("\n");
            int i2 = ++GenSymindex;
            text = (text) + ("label_") + (i2)
                + (":\n")
                ;
            text = (text) + ("while (true) {\u0001");
            var array = new Lookahead[1] { lookahead };
            string[] array2 = new string[2]
            {
                "\n;",
                ("\nbreak label_")+(i2)+(";")

            };
            text = (text) + (BuildLookaheadChecker(array, array2));
            text = (text) + (Phase1ExpansionGen(expansion));
            text = (text) + ("\u0002\n}");
        }
        else if (exp is ZeroOrOne zeroOrOne)
        {
            Expansion expansion = zeroOrOne.Expansion;
            Lookahead lookahead;
            if (expansion is Sequence sequence)
            {
                lookahead = (Lookahead)sequence.Units[0];
            }
            else
            {
                lookahead = new ();
                lookahead.amount = Options.Lookahead;
                lookahead.LaExpansion = expansion;
            }
            Lookahead[] array = new Lookahead[1] { lookahead };
            string[] array2 = new string[2]
            {
                Phase1ExpansionGen(expansion),
                "\n;"
            };
            text = (text) + (BuildLookaheadChecker(array, array2));
        }
        else if (exp is TryBlock tryBlock)
        {
            Expansion expansion = tryBlock.Expression;
            text = (text) + ("\n");
            text = (text) + ("try {\u0001");
            text = (text) + (Phase1ExpansionGen(expansion));
            text = (text) + ("\u0002\n}");
            for (int i2 = 0; i2 < tryBlock.CatchBlocks.Count; i2++)
            {
                text = (text) + (" catch (");
                var vector = tryBlock.Types[i2];
                if (vector.Count != 0)
                {
                    PrintTokenSetup(vector[0]);

                    foreach (var t2 in vector)
                    {
                        text = (text) + (PrintToken(t2));
                    }
                    text = (text) + (PrintTrailingComments(t));
                }
                text += " ";
                t = tryBlock.Ids[i2];
                PrintTokenSetup(t);
                text = (text) + (PrintToken(t));
                text = (text) + (PrintTrailingComments(t));
                text = (text) + (") {\u0003\n");
                vector = tryBlock.CatchBlocks[i2];
                if (vector.Count != 0)
                {
                    PrintTokenSetup(vector[0]);
                    CCol = 1;
                    foreach (var _t in vector)
                    {
                        text = (text) + (PrintToken(_t));
                        t = _t;
                    }
                    text = (text) + (PrintTrailingComments(t));
                }
                text = (text) + ("\u0004\n}");
            }
            if (tryBlock.FinallyBlock != null)
            {
                text = (text) + (" finally {\u0003\n");
                if (tryBlock.FinallyBlock.Count != 0)
                {
                    PrintTokenSetup(tryBlock.FinallyBlock[0]);
                    CCol = 1;
                    foreach (var _t in tryBlock.FinallyBlock)
                    {
                        text = (text) + (PrintToken(_t));
                        t = _t;
                    }
                    text = (text) + (PrintTrailingComments(t));
                }
                text = (text) + ("\u0004\n}");
            }
        }
        return text;
	}

	
	internal static void DumpFormattedString(string text)
	{
		int num = 32;
		int num2 = 1;
		for (int i = 0; i < text.Length; i++)
		{
			int num3 = num;
			num = text[i];
			if (num == 10 && num3 == 13)
			{
				continue;
			}
			switch (num)
			{
			case 10:
			case 13:
				if (num2 != 0)
				{
					Phase1NewLine();
				}
				else
				{
					writer.WriteLine("");
				}
				break;
			case 1:
				Indentamt += 2;
				break;
			case 2:
				Indentamt -= 2;
				break;
			case 3:
				num2 = 0;
				break;
			case 4:
				num2 = 1;
				break;
			default:
				writer.Write((char)num);
				break;
			}
		}
	}

	
	internal static string BuildLookaheadChecker(Lookahead[] lookaheads, string[] texts)
	{
		int num = 0;
		int num2 = 0;
		bool[] array = new bool[TokenCount];
		string text = "";
		Token t = null;
		int num3 = (TokenCount - 1) / 32 + 1;
		int[] array2 = null;
		int num4;
		for (num4 = 0; num4 < lookaheads.Length; num4++)
		{
			var lookahead = lookaheads[num4];
			JJ2LA = false;
			if (lookahead.amount == 0 || Semanticize.EmptyExpansionExists(lookahead.LaExpansion) || JavaCodeCheck(lookahead.LaExpansion))
			{
				if (lookahead.ActionTokens.Count == 0)
				{
					break;
				}
				switch (num)
				{
				case 0:
					text = (text)+("\nif (");
					num2++;
					break;
				case 1:
					text = (text)+("\u0002\n} else if (");
					break;
				case 2:
					text = (text)+("\u0002\ndefault:\u0001");
					if (Options.ErrorReporting)
					{
						text = (text)+("\njj_la1[")+(MaskIndex)
							+("] = jj_gen;")
							;
						MaskIndex++;
					}
					MaskVals.Add(array2);
					text = (text)+("\nif (");
					num2++;
					break;
				}
				PrintTokenSetup(lookahead.ActionTokens[0]);
				
				foreach(var _t in lookahead.ActionTokens)
				{
					text = (text)+(PrintToken(_t));
					t = _t;
				}
				text = (text)+(PrintTrailingComments(t));
				text = (text)+(") {\u0001")+(texts[num4])
					;
				num = 1;
			}
			else if (lookahead.amount == 1 && lookahead.ActionTokens.Count == 0)
			{
				if (FirstSet == null)
				{
					FirstSet = new bool[TokenCount];
				}
				for (int i = 0; i < TokenCount; i++)
				{
					FirstSet[i] = false;
				}
				GenFirstSet(lookahead.LaExpansion);
				if (!JJ2LA)
				{
					int num5 = num;
					if (num5 != 0)
					{
						if (num5 != 1)
						{
							goto IL_0335;
						}
						text = (text)+("\u0002\n} else {\u0001");
					}
					text = (text)+("\nswitch (");
					text = ((!Options.CacheTokens) ? (text)+("(jj_ntk==-1)?jj_ntk():jj_ntk) {\u0001") : (text)+("jj_nt.kind) {\u0001"));
					for (int i = 0; i < TokenCount; i++)
					{
						array[i] = false;
					}
					num2++;
					array2 = new int[num3];
					for (int i = 0; i < num3; i++)
					{
						array2[i] = 0;
					}
					goto IL_0335;
				}
			}
			else
			{
				JJ2LA = true;
			}
			goto IL_0463;
			IL_0335:
			for (int i = 0; i < TokenCount; i++)
			{
				if (FirstSet[i] && !array[i])
				{
					array[i] = true;
					text = (text)+("\u0002\ncase ");
					int num6 = i / 32;
					int num7 = i;
					int num8 = ((32 != -1) ? (num7 % 32) : 0);
					int[] array3 = array2;
					int num9 = num6;
					int[] array4 = array3;
					array4[num9] |= 1 << num8;
					string text2 = (string)NamesOfTokens[i];
					text = ((text2 != null) ? (text)+(text2) : (text)+(i));
					text = (text)+(":\u0001");
				}
			}
			text = (text)+(texts[num4]);
			text = (text)+("\nbreak;");
			num = 2;
			goto IL_0463;
			IL_0463:
			if (JJ2LA)
			{
				switch (num)
				{
				case 0:
					text = (text)+("\nif (");
					num2++;
					break;
				case 1:
					text = (text)+("\u0002\n} else if (");
					break;
				case 2:
					text = (text)+("\u0002\ndefault:\u0001");
					if (Options.ErrorReporting)
					{
						text = (text)+("\njj_la1[")+(MaskIndex)
							+("] = jj_gen;")
							;
						MaskIndex++;
					}
					MaskVals.Add(array2);
					text = (text)+("\nif (");
					num2++;
					break;
				}
				JJ2Index++;
				lookahead.LaExpansion.InternalName = ("_")+(JJ2Index);
				Phase2List.Add(lookahead);
				text = (text)+("jj_2")+(lookahead.LaExpansion.InternalName)
					+("(")
					+(lookahead.amount)
					+(")")
					;
				if (lookahead.ActionTokens.Count != 0)
				{
					text = (text)+(" && (");
					PrintTokenSetup(lookahead.ActionTokens[0]);
					
					foreach(var _t in lookahead.ActionTokens)
					{
						text = (text)+(PrintToken(t));
						t = _t;
					}
					text = (text)+(PrintTrailingComments(t));
					text = (text)+(")");
				}
				text = (text)+(") {\u0001")+(texts[num4])
					;
				num = 1;
			}
		}
		switch (num)
		{
		case 0:
			text = (text)+(texts[num4]);
			break;
		case 1:
			text = (text)+("\u0002\n} else {\u0001")+(texts[num4])
				;
			break;
		case 2:
			text = (text)+("\u0002\ndefault:\u0001");
			if (Options.ErrorReporting)
			{
				text = (text)+("\njj_la1[")+(MaskIndex)
					+("] = jj_gen;")
					;
				MaskVals.Add(array2);
				MaskIndex++;
			}
			text = (text)+(texts[num4]);
			break;
		}
		for (int i = 0; i < num2; i++)
		{
			text = (text)+("\u0002\n}");
		}
		return text;
	}

	
	private static void Generate3R(Expansion exp, Phase3Data p3)
	{
		Expansion expansion = exp;
		if (string.Equals(exp.InternalName, ""))
		{
			while (true)
			{
				if (expansion is Sequence sequence && sequence.Units.Count == 2)
				{
					expansion = sequence.Units[1];
					continue;
				}
				if (expansion is not NonTerminal)
				{
					break;
				}
				NonTerminal nonTerminal = (NonTerminal)expansion;

				if (ProductionTable.TryGetValue(nonTerminal.Name,out var normalProduction)&& normalProduction is JavaCodeProduction)
				{
					break;
				}
				expansion = normalProduction.Expansion;
			}
			if (expansion is RegularExpression re)
			{
				exp.InternalName = ("jj_scan_token(")+(re.Ordinal)+(")");
				return;
			}
			GenSymindex++;
			exp.InternalName = ("R_")+(GenSymindex);
		}
		var phase3Data = Phase3Table[(exp)];
		if (phase3Data == null || phase3Data.Count < p3.Count)
		{
			phase3Data = new Phase3Data(exp, p3.Count);
			Phase3List.Add(phase3Data);
			Phase3Table.Add(exp, phase3Data);
		}
	}

	
	internal static void SetupPhase3Builds(Phase3Data p3)
	{
		Expansion exp = p3.Expansion;
		if (exp is RegularExpression)
		{
			return;
		}
        if (exp is NonTerminal nonTerminal)
        {
            if (ProductionTable.TryGetValue(nonTerminal.Name, out var normalProduction)
                && normalProduction is not JavaCodeProduction)
            {
                Generate3R(normalProduction.Expansion, p3);
            }
        }
        else if (exp is Choice choice)
        {
            for (int i = 0; i < choice.Choices.Count; i++)
            {
                Generate3R(choice.Choices[i], p3);
            }
        }
        else if (exp is Sequence sequence)
        {
            int i = p3.Count;
            for (int j = 1; j < sequence.Units.Count; j++)
            {
                Expansion expansion = sequence.Units[j];
                SetupPhase3Builds(new Phase3Data(expansion, i));
                i -= MinimumSize(expansion);
                if (i <= 0)
                {
                    break;
                }
            }
        }
        else if (exp is TryBlock tryBlock)
        {
            SetupPhase3Builds(new Phase3Data(tryBlock.Expression, p3.Count));
        }
        else if (exp is OneOrMore oneOrMore)
        {
            Generate3R(oneOrMore.Expansion, p3);
        }
        else if (exp is ZeroOrMore zeroOrMore)
        {
            Generate3R(zeroOrMore.Expansion, p3);
        }
        else if (exp is ZeroOrOne zeroOrOne)
        {
            Generate3R(zeroOrOne.Expansion, p3);
        }
    }

	
	internal static int MinimumSize(Expansion exp)
	{
		return MinimumSize(exp, int.MaxValue);
	}

	
	internal static string GenReturn(bool val)
	{
		string str = ((!val) ? "false" : "true");
		if (Options.DebugLookahead && jj3_expansion != null)
		{
			string text = ("trace_return(\"")+(((NormalProduction)jj3_expansion.Parent).Lhs)+("(LOOKAHEAD ")
				+((!val) ? "SUCCEEDED" : "FAILED")
				+(")\");")
				;
			if (Options.ErrorReporting)
			{
				text = ("if (!jj_rescan) ")+(text);
			}
			return ("{ ")+(text)+(" return ")
				+(str)
				+("; }")
				;
		}
		
		return ("return ") + (str) + (";");
	}


    private static string Genjj_3Call(Expansion exp) => exp.InternalName.StartsWith("jj_scan_token") ? exp.InternalName : ("jj_3") + (exp.InternalName) + ("()");


    internal static void BuildPhase3Routine(Phase3Data p3, bool val)
	{
		Expansion exp = p3.Expansion;
		Token t = null;
		if (exp.InternalName.StartsWith("jj_scan_token"))
		{
			return;
		}
		if (!val)
		{
			writer.WriteLine(("  ")+(StaticOpt())+("private boolean jj_3")
				+(exp.InternalName)
				+("() {")
				);
			xsp_declared = false;
			if (Options.DebugLookahead && exp.Parent is NormalProduction)
			{
				writer.Write("    ");
				if (Options.ErrorReporting)
				{
					writer.Write("if (!jj_rescan) ");
				}
				writer.WriteLine(("trace_call(\"")+(((NormalProduction)exp.Parent).Lhs)+("(LOOKING AHEAD...)\");")
					);
				jj3_expansion = exp;
			}
			else
			{
				jj3_expansion = null;
			}
		}
        if (exp is RegularExpression regularExpression)
        {
            if (string.Equals(regularExpression.Label, ""))
            {
                var dict = NamesOfTokens;
                ;
                var obj = dict.ContainsKey((regularExpression.Ordinal));
                if (obj)
                {
                    writer.WriteLine(("    if (jj_scan_token(") + ((dict[regularExpression.Ordinal]) + (")) ")
                        + (GenReturn(true))
                        ));
                }
                else
                {
                    writer.WriteLine(("    if (jj_scan_token(") + (regularExpression.Ordinal) + (")) ")
                        + (GenReturn(true))
                        );
                }
            }
            else
            {
                writer.WriteLine(("    if (jj_scan_token(") + (regularExpression.Label) + (")) ")
                    + (GenReturn(true))
                    );
            }
        }
        else if (exp is NonTerminal nonTerminal)
        {
            if (ProductionTable.TryGetValue(nonTerminal.Name, out var normalProduction) && normalProduction is JavaCodeProduction)
            {
                writer.WriteLine(("    if (true) { jj_la = 0; jj_scanpos = jj_lastpos; ") + (GenReturn(false)) + ("}")
                    );
            }
            else
            {
                Expansion expansion = normalProduction.Expansion;
                writer.WriteLine(("    if (") + (Genjj_3Call(expansion)) + (") ")
                    + (GenReturn(true))
                    );
            }
        }
        else if (exp is Choice choice)
        {
            if (choice.Choices.Count != 1)
            {
                if (!xsp_declared)
                {
                    xsp_declared = true;
                    writer.WriteLine("    Token xsp;");
                }
                writer.WriteLine("    xsp = jj_scanpos;");
            }
            for (int i = 0; i < choice.Choices.Count; i++)
            {
                Sequence sequence = (Sequence)choice.Choices[i];
                Lookahead lookahead = (Lookahead)sequence.Units[0];
                if (lookahead.ActionTokens.Count != 0)
                {
                    writer.WriteLine("    jj_lookingAhead = true;");
                    writer.Write("    jj_semLA = ");
                    PrintTokenSetup(lookahead.ActionTokens[0]);
                    foreach (var _t in lookahead.ActionTokens)
                    {
                        PrintToken(_t, writer);
                        t = _t;
                    }
                    PrintTrailingComments(t, writer);
                    writer.WriteLine(";");
                    writer.WriteLine("    jj_lookingAhead = false;");
                }
                writer.Write("    if (");
                if (lookahead.ActionTokens.Count != 0)
                {
                    writer.Write("!jj_semLA || ");
                }
                if (i != choice.Choices.Count - 1)
                {
                    writer.WriteLine((Genjj_3Call(sequence)) + (") {"));
                    writer.WriteLine("    jj_scanpos = xsp;");
                }
                else
                {
                    writer.WriteLine((Genjj_3Call(sequence)) + (") ") + (GenReturn(true))
                        );
                }
            }
            for (int i = 1; i < choice.Choices.Count; i++)
            {
                writer.WriteLine("    }");
            }
        }
        else if (exp is Sequence sequence)
        {
            int num = p3.Count;
            for (int i = 1; i < sequence.Units.Count; i++)
            {
                Expansion expansion2 = sequence.Units[i];
                BuildPhase3Routine(new Phase3Data(expansion2, num), true);
                num -= MinimumSize(expansion2);
                if (num <= 0)
                {
                    break;
                }
            }
        }
        else if (exp is TryBlock tryBlock)
        {
            BuildPhase3Routine(new Phase3Data(tryBlock.Expression, p3.Count), true);
        }
        else if (exp is OneOrMore more)
        {
            if (!xsp_declared)
            {
                xsp_declared = true;
                writer.WriteLine("    Token xsp;");
            }
            OneOrMore oneOrMore = more;
            Expansion expansion3 = oneOrMore.Expansion;
            writer.WriteLine(("    if (") + (Genjj_3Call(expansion3)) + (") ")
                + (GenReturn(true))
                );
            writer.WriteLine("    while (true) {");
            writer.WriteLine("      xsp = jj_scanpos;");
            writer.WriteLine(("      if (") + (Genjj_3Call(expansion3)) + (") { jj_scanpos = xsp; break; }")
                );
            writer.WriteLine("    }");
        }
        else if (exp is ZeroOrMore more1)
        {
            if (!xsp_declared)
            {
                xsp_declared = true;
                writer.WriteLine("    Token xsp;");
            }
            ZeroOrMore zeroOrMore = more1;
            Expansion expansion3 = zeroOrMore.Expansion;
            writer.WriteLine("    while (true) {");
            writer.WriteLine("      xsp = jj_scanpos;");
            writer.WriteLine(("      if (") + (Genjj_3Call(expansion3)) + (") { jj_scanpos = xsp; break; }")
                );
            writer.WriteLine("    }");
        }
        else if (exp is ZeroOrOne one)
        {
            if (!xsp_declared)
            {
                xsp_declared = true;
                writer.WriteLine("    Token xsp;");
            }
            ZeroOrOne zeroOrOne = one;
            Expansion expansion3 = zeroOrOne.Expansion;
            writer.WriteLine("    xsp = jj_scanpos;");
            writer.WriteLine(("    if (") + (Genjj_3Call(expansion3)) + (") jj_scanpos = xsp;")
                );
        }
        if (!val)
		{
			writer.WriteLine(("    ")+(GenReturn(false)));
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
	}

	
	internal static int MinimumSize(Expansion exp, int val)
	{
		int result = 0;
		if (exp.InMinimumSize)
		{
			return int.MaxValue;
		}
		exp.InMinimumSize = true;
		if (exp is RegularExpression)
		{
			result = 1;
		}
		else if (exp is NonTerminal nonTerminal)
        {
            if (ProductionTable.TryGetValue(nonTerminal.Name, out var normalProduction)
                && normalProduction is JavaCodeProduction)
            {
                result = int.MaxValue;
            }
            else
            {
                Expansion expansion = normalProduction.Expansion;
                result = MinimumSize(expansion);
            }
        }
        else if (exp is Choice choice1)
        {
            int n = val;
            Choice choice = choice1;
            int num2 = 0;
            while (n > 1 && num2 < choice.Choices.Count)
            {
                Expansion expansion2 = choice.Choices[num2];
                int num3 = MinimumSize(expansion2, n);
                if (n > num3)
                {
                    n = num3;
                }
                num2++;
            }
            result = n;
        }
        else if (exp is Sequence sequence)
        {
            int num = 0;
            for (int i = 1; i < sequence.Units.Count; i++)
            {
                var expansion3 = sequence.Units[i];
                int n = MinimumSize(expansion3);
                if (num == int.MaxValue || n == int.MaxValue)
                {
                    num = int.MaxValue;
                    continue;
                }
                num += n;
                if (num > val)
                {
                    break;
                }
            }
            result = num;
        }
        else if (exp is TryBlock tryBlock)
        {
            result = MinimumSize(tryBlock.Expression);
        }
        else if (exp is OneOrMore oneOrMore)
        {
            result = MinimumSize(oneOrMore.Expansion);
        }
        else if (exp is ZeroOrMore)
        {
            result = 0;
        }
        else if (exp is ZeroOrOne)
        {
            result = 0;
        }
        else if (exp is Lookahead)
        {
            result = 0;
        }
        else if (exp is Action)
        {
            result = 0;
        }
        exp.InMinimumSize = false;
		return result;
	}

	
	internal static void BuildPhase1Routine(BNFProduction production)
	{
		var token = production.ReturnTypeToken[0];
		int num = 0;
		if (token.Kind == 77)
		{
			num = 1;
		}
		PrintTokenSetup(token);
		CCol = 1;
		PrintLeadingComments(token, writer);
		writer.Write(("  ")+(StaticOpt())+("final ")
			+((production.AccessMod == null) ? "public" : production.AccessMod)
			+(" ")
			);
		CLine = token.BeginLine;
		CCol = token.BeginColumn;
		PrintTokenOnly(token, writer);
		for (int i = 1; i < production.ReturnTypeToken.Count; i++)
		{
			token = production.ReturnTypeToken[i];
			PrintToken(token, writer);
		}
		PrintTrailingComments(token, writer);
		writer.Write((" ")+(production.Lhs)+("(")
			);
		
		if (production.ParameterListTokens.Count != 0)
		{
			PrintTokenSetup(production.ParameterListTokens[0]);
			foreach(var _t in production.ParameterListTokens)
			{
				PrintToken(token = _t, writer);
			}
			PrintTrailingComments(token, writer);
		}
		writer.Write(") throws ParseException");
		foreach(var tl in production.ThrowsList)
		{
			writer.Write(", ");
			foreach(var _token in tl)
            {
				writer.Write(_token.Image);
			}
		}
		writer.Write(" {");
		Indentamt = 4;
		if (Options.DebugParser)
		{
			writer.WriteLine("");
			writer.WriteLine(("    trace_call(\"")+(production.Lhs)+("\");")
				);
			writer.Write("    try {");
			Indentamt = 6;
		}
		if (production.DeclarationTokens.Count != 0)
		{
			PrintTokenSetup(production.DeclarationTokens[0]);
			CLine--;

			foreach(var _t in production.DeclarationTokens)
			{
				PrintToken(_t, writer);
				token = _t;
			}
			PrintTrailingComments(token, writer);
		}
		string text = Phase1ExpansionGen(production.Expansion);
		DumpFormattedString(text);
		writer.WriteLine("");
		if (production.JumpPatched && num == 0)
		{
			writer.WriteLine("    throw new System.Exception(\"Missing return statement in function\");");
		}
		if (Options.DebugParser)
		{
			writer.WriteLine("    } finally {");
			writer.WriteLine(("      trace_return(\"")+(production.Lhs)+("\");")
				);
			writer.WriteLine("    }");
		}
		writer.WriteLine("  }");
		writer.WriteLine("");
	}

	
	internal static void BuildPhase2Routine(Lookahead lookahead)
	{
		var la_expansion = lookahead.LaExpansion;
		writer.WriteLine(("  ")+(StaticOpt())+("private boolean jj_2")
			+(la_expansion.InternalName)
			+("(int xla) {")
			);
		writer.WriteLine("    jj_la = xla; jj_lastpos = jj_scanpos = token;");
		writer.WriteLine(("    try { return !jj_3")+(la_expansion.InternalName)+("(); }")
			);
		writer.WriteLine("    catch(LookaheadSuccess ls) { return true; }");
		if (Options.ErrorReporting)
		{
			int.TryParse((la_expansion.InternalName.Substring(1)), out var x);
			writer.WriteLine(("    finally { jj_save(")+(
				x - 1)+(", xla); }")
				);
		}
		writer.WriteLine("  }");
		writer.WriteLine("");
		var phase3Data = new Phase3Data(la_expansion, lookahead.amount);
		Phase3List.Add(phase3Data);
		Phase3Table.Add(la_expansion, phase3Data);
	}

	
	private static void DumpLookaheads(Lookahead[] lookahead, string[] text)
	{
		for (int i = 0; i < lookahead.Length; i++)
		{
			Console.Error.WriteLine(("Lookahead: ")+(i));
			Console.Error.WriteLine(lookahead[i].Dump(0, new()));
			Console.Error.WriteLine();
		}
	}

	
	internal static void Build(TextWriter _writer)
	{
		
		writer = _writer;
		foreach(var normalProduction in BNFProductions)
		{
			if (normalProduction is JavaCodeProduction javaCodeProduction)
			{
				Token token = javaCodeProduction.ReturnTypeToken[0];
				PrintTokenSetup(token);
				CCol = 1;
				PrintLeadingComments(token, writer);
				writer.Write(("  ")+(StaticOpt())+((normalProduction.AccessMod == null) ? "" : (normalProduction.AccessMod)+(" "))
					);
				CLine = token.BeginLine;
				CCol = token.BeginColumn;
				PrintTokenOnly(token, writer);
				for (int i = 1; i < javaCodeProduction.ReturnTypeToken.Count; i++)
				{
					token = javaCodeProduction.ReturnTypeToken[i];
					PrintToken(token, writer);
				}
				PrintTrailingComments(token, writer);
				writer.Write((" ")+(javaCodeProduction.Lhs)+("(")
					);
				if (javaCodeProduction.ParameterListTokens.Count != 0)
				{
					PrintTokenSetup(javaCodeProduction.ParameterListTokens[0]);
					foreach(var _t in javaCodeProduction.ParameterListTokens)
					{
						PrintToken(_t, writer);
						token = _t;
					}
					PrintTrailingComments(token, writer);
				}
				writer.Write(") throws ParseException");
				foreach(var vector in javaCodeProduction.ThrowsList)
				{
					writer.Write(", ");
	
					foreach(var t in vector)
					{		
						writer.Write(t.Image);
					}
				} 
				writer.Write(" {");
				if (Options.DebugParser)
				{
					writer.WriteLine("");
					writer.WriteLine(("    trace_call(\"")+(javaCodeProduction.Lhs)+("\");")
						);
					writer.Write("    try {");
				}
				if (javaCodeProduction.CodeTokens.Count > 0)
				{
					PrintTokenSetup(javaCodeProduction.CodeTokens[0]);
					CLine--;
					foreach (var t in javaCodeProduction.CodeTokens)
                    {
						PrintToken(t, writer);
					}
					PrintTrailingComments(token, writer);
				}
				writer.WriteLine("");
				if (Options.DebugParser)
				{
					writer.WriteLine("    } finally {");
					writer.WriteLine(("      trace_return(\"")+(javaCodeProduction.Lhs)+("\");")
						);
					writer.WriteLine("    }");
				}
				writer.WriteLine("  }");
				writer.WriteLine("");
			}
			else
			{
				BuildPhase1Routine((BNFProduction)normalProduction);
			}
		}
		int j;
		for (j = 0; j < Phase2List.Count; j++)
		{
			BuildPhase2Routine(Phase2List[j]);
		}
		j = 0;
		while (j < Phase3List.Count)
		{
			for (; j < Phase3List.Count; j++)
			{
				SetupPhase3Builds(Phase3List[j]);
			}
		}
		foreach(var p3 in Phase3Table)
		{
			BuildPhase3Routine(p3.Value, false);
		}
	}
}
