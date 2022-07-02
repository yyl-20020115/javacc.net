using System;
using System.Collections;
using System.IO;
using System.Text;

namespace org.javacc.parser;

public class ParseEngine : JavaCCGlobals
{
	private static TextWriter ostr;

	private static int gensymindex;

	private static int indentamt;

	private static bool jj2LA;

	private static ArrayList phase2list;

	private static ArrayList phase3list;

	private static Hashtable phase3table;

	private static bool[] firstSet;

	internal const int NOOPENSTM = 0;

	internal const int OPENIF = 1;

	internal const int OPENSWITCH = 2;

	private static bool xsp_declared;

	internal static Expansion jj3_expansion;

	internal static Hashtable generated;

	
	public new static void ReInit()
	{
		ostr = null;
		gensymindex = 0;
		indentamt = 0;
		jj2LA = false;
		phase2list = new ArrayList();
		phase3list = new ArrayList();
		phase3table = new Hashtable();
		firstSet = null;
		xsp_declared = false;
		jj3_expansion = null;
	}

	
	private static bool javaCodeCheck(Expansion P_0)
	{
		if (P_0 is RegularExpression)
		{
			return false;
		}
		if (P_0 is NonTerminal)
		{
			NormalProduction prod = ((NonTerminal)P_0).prod;
			if (prod is JavaCodeProduction)
			{
				return true;
			}
			bool result = javaCodeCheck(prod.Expansion);
			
			return result;
		}
		if (P_0 is Choice)
		{
			Choice choice = (Choice)P_0;
			for (int i = 0; i < choice.Choices.Count; i++)
			{
				if (javaCodeCheck((Expansion)choice.Choices[i]))
				{
					return true;
				}
			}
			return false;
		}
		if (P_0 is Sequence)
		{
			Sequence sequence = (Sequence)P_0;
			for (int i = 0; i < sequence.Units.Count; i++)
			{
				Expansion[] array = (Expansion[])sequence.Units.ToArray();
				if (array[i] is Lookahead && ((Lookahead)array[i]).isExplicit)
				{
					return false;
				}
				if (javaCodeCheck(array[i]))
				{
					return true;
				}
				if (!Semanticize.EmptyExpansionExists(array[i]))
				{
					return false;
				}
			}
			return false;
		}
		if (P_0 is OneOrMore)
		{
			OneOrMore oneOrMore = (OneOrMore)P_0;
			bool result2 = javaCodeCheck(oneOrMore.expansion);
			
			return result2;
		}
		if (P_0 is ZeroOrMore)
		{
			ZeroOrMore zeroOrMore = (ZeroOrMore)P_0;
			bool result3 = javaCodeCheck(zeroOrMore.expansion);
			
			return result3;
		}
		if (P_0 is ZeroOrOne)
		{
			ZeroOrOne zeroOrOne = (ZeroOrOne)P_0;
			bool result4 = javaCodeCheck(zeroOrOne.expansion);
			
			return result4;
		}
		if (P_0 is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)P_0;
			bool result5 = javaCodeCheck(tryBlock.exp);
			
			return result5;
		}
		return false;
	}

	
	private static void genFirstSet(Expansion P_0)
	{
		if (P_0 is RegularExpression)
		{
			firstSet[((RegularExpression)P_0).ordinal] = true;
		}
		else if (P_0 is NonTerminal)
		{
			if (!(((NonTerminal)P_0).prod is JavaCodeProduction))
			{
				genFirstSet(((BNFProduction)((NonTerminal)P_0).prod).Expansion);
			}
		}
		else if (P_0 is Choice)
		{
			Choice choice = (Choice)P_0;
			for (int i = 0; i < choice.Choices.Count; i++)
			{
				genFirstSet((Expansion)choice.Choices[i]);
			}
		}
		else if (P_0 is Sequence)
		{
			Sequence sequence = (Sequence)P_0;
			object obj = sequence.Units[0];
			if (obj is Lookahead && ((Lookahead)obj).action_tokens.Count != 0)
			{
				jj2LA = true;
			}
			for (int j = 0; j < sequence.Units.Count; j++)
			{
				Expansion expansion = (Expansion)sequence.Units[j];
				if (expansion is NonTerminal && ((NonTerminal)expansion).prod is JavaCodeProduction)
				{
					if (j > 0 && sequence.Units[j-1] is Lookahead)
					{
						Lookahead lookahead = (Lookahead)sequence.Units[j-1];
						genFirstSet(lookahead.la_expansion);
					}
				}
				else
				{
					genFirstSet((Expansion)sequence.Units[j]);
				}
				if (!Semanticize.EmptyExpansionExists((Expansion)sequence.Units[j]))
				{
					break;
				}
			}
		}
		else if (P_0 is OneOrMore)
		{
			OneOrMore oneOrMore = (OneOrMore)P_0;
			genFirstSet(oneOrMore.expansion);
		}
		else if (P_0 is ZeroOrMore)
		{
			ZeroOrMore zeroOrMore = (ZeroOrMore)P_0;
			genFirstSet(zeroOrMore.expansion);
		}
		else if (P_0 is ZeroOrOne)
		{
			ZeroOrOne zeroOrOne = (ZeroOrOne)P_0;
			genFirstSet(zeroOrOne.expansion);
		}
		else if (P_0 is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)P_0;
			genFirstSet(tryBlock.exp);
		}
	}

	
	internal static void phase1NewLine()
	{
		ostr.WriteLine("");
		for (int i = 0; i < indentamt; i++)
		{
			ostr.Write(" ");
		}
	}

	
	internal static string phase1ExpansionGen(Expansion P_0)
	{
		string text = "";
		Token t;
		if (P_0 is RegularExpression)
		{
			RegularExpression regularExpression = (RegularExpression)P_0;
			text = (text)+("\n");
			if (regularExpression.lhsTokens.Count != 0)
			{
				JavaCCGlobals.PrintTokenSetup((Token)regularExpression.lhsTokens[0]);
				foreach(var t in regularExpression.lhsTokens)
				{
					text = (text)+(JavaCCGlobals.PrintToken(t));
				}
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				text = (text)+(" = ");
			}
			string str = ((regularExpression.rhsToken != null) ? (").")+(regularExpression.rhsToken.image)+(";")
				 : ");");
			if (string.Equals(regularExpression.label, ""))
			{
				Hashtable hashtable = JavaCCGlobals.names_of_tokens;
				;
				object obj = hashtable.get((regularExpression.ordinal));
				text = ((obj == null) ? (text)+("jj_consume_token(")+(regularExpression.ordinal)
					+(str)
					 : (text)+("jj_consume_token(")+((string)obj)
					+(str)
					);
			}
			else
			{
				text = (text)+("jj_consume_token(")+(regularExpression.label)
					+(str)
					;
			}
		}
		else if (P_0 is NonTerminal)
		{
			NonTerminal nonTerminal = (NonTerminal)P_0;
			text = (text)+("\n");
			if (nonTerminal.lhsTokens.Count != 0)
			{
				foreach(Token t in nonTerminal.lhsTokens)
				{
					text = (text)+(JavaCCGlobals.PrintToken(t));
				}
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				text = (text)+(" = ");
			}
			text = (text)+(nonTerminal.name)+("(")
				;
			if (nonTerminal.argument_tokens.Count != 0)
			{
				Token t = nonTerminal.argument_tokens[0];
				JavaCCGlobals.PrintTokenSetup(t);
				foreach (var token in nonTerminal.argument_tokens) 
				{
					text = (text)+(JavaCCGlobals.PrintToken(token));
					t = token;
				}
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
			}
			text = (text)+(");");
		}
		else if (P_0 is Action)
		{
			Action action = (Action)P_0;
			text = (text)+("\u0003\n");
			if (action.ActionTokens.Count != 0)
			{
				JavaCCGlobals.PrintTokenSetup((Token)action.ActionTokens[0]);
				JavaCCGlobals.ccol = 1;
				Enumeration enumeration = action.ActionTokens.elements();
				while (enumeration.hasMoreElements())
				{
					t = (Token)enumeration.nextElement();
					text = (text)+(JavaCCGlobals.PrintToken(t));
				}
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
			}
			text = (text)+("\u0004");
		}
		else if (P_0 is Choice)
		{
			Choice choice = (Choice)P_0;
			Lookahead[] array = new Lookahead[choice.Choices.Count];
			string[] array2 = new string[choice.Choices.Count + 1];
			array2[choice.Choices.Count] = "\njj_consume_token(-1);\nthrow new ParseException();";
			for (int i = 0; i < choice.Choices.Count; i++)
			{
				Sequence sequence = (Sequence)choice.Choices[i];
				array2[i] = phase1ExpansionGen(sequence);
				array[i] = (Lookahead)sequence.Units[0];
			}
			text = buildLookaheadChecker(array, array2);
		}
		else if (P_0 is Sequence)
		{
			Sequence sequence2 = (Sequence)P_0;
			for (int j = 1; j < sequence2.Units.Count; j++)
			{
				text = (text)+(phase1ExpansionGen((Expansion)sequence2.Units[j]));
			}
		}
		else if (P_0 is OneOrMore)
		{
			OneOrMore oneOrMore = (OneOrMore)P_0;
			Expansion expansion = oneOrMore.expansion;
			Lookahead lookahead;
			if (expansion is Sequence)
			{
				lookahead = (Lookahead)((Sequence)expansion).Units[0];
			}
			else
			{
				lookahead = new Lookahead();
				lookahead.amount = Options.Lookahead;
				lookahead.la_expansion = expansion;
			}
			text = (text)+("\n");
			int i2 = ++gensymindex;
			text = (text)+("label_")+(i2)
				+(":\n")
				;
			text = (text)+("while (true) {\u0001");
			text = (text)+(phase1ExpansionGen(expansion));
			Lookahead[] array = new Lookahead[1] { lookahead };
			string[] array2 = new string[2]
			{
				"\n;",
				("\nbreak label_")+(i2)+(";")
					
			};
			text = (text)+(buildLookaheadChecker(array, array2));
			text = (text)+("\u0002\n}");
		}
		else if (P_0 is ZeroOrMore)
		{
			ZeroOrMore zeroOrMore = (ZeroOrMore)P_0;
			Expansion expansion = zeroOrMore.expansion;
			Lookahead lookahead;
			if (expansion is Sequence)
			{
				lookahead = (Lookahead)((Sequence)expansion).Units[0];
			}
			else
			{
				lookahead = new Lookahead();
				lookahead.amount = Options.Lookahead;
				lookahead.la_expansion = expansion;
			}
			text = (text)+("\n");
			int i2 = ++gensymindex;
			text = (text)+("label_")+(i2)
				+(":\n")
				;
			text = (text)+("while (true) {\u0001");
			Lookahead[] array = new Lookahead[1] { lookahead };
			string[] array2 = new string[2]
			{
				"\n;",
				("\nbreak label_")+(i2)+(";")
					
			};
			text = (text)+(buildLookaheadChecker(array, array2));
			text = (text)+(phase1ExpansionGen(expansion));
			text = (text)+("\u0002\n}");
		}
		else if (P_0 is ZeroOrOne)
		{
			ZeroOrOne zeroOrOne = (ZeroOrOne)P_0;
			Expansion expansion = zeroOrOne.expansion;
			Lookahead lookahead;
			if (expansion is Sequence)
			{
				lookahead = (Lookahead)((Sequence)expansion).Units[0];
			}
			else
			{
				lookahead = new Lookahead();
				lookahead.amount = Options.Lookahead;
				lookahead.la_expansion = expansion;
			}
			Lookahead[] array = new Lookahead[1] { lookahead };
			string[] array2 = new string[2]
			{
				phase1ExpansionGen(expansion),
				"\n;"
			};
			text = (text)+(buildLookaheadChecker(array, array2));
		}
		else if (P_0 is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)P_0;
			Expansion expansion = tryBlock.exp;
			text = (text)+("\n");
			text = (text)+("try {\u0001");
			text = (text)+(phase1ExpansionGen(expansion));
			text = (text)+("\u0002\n}");
			for (int i2 = 0; i2 < tryBlock.catchblks.Count; i2++)
			{
				text = (text)+(" catch (");
				ArrayList vector = (ArrayList)tryBlock.types[i2];
				if (vector.Count != 0)
				{
					JavaCCGlobals.PrintTokenSetup((Token)vector[0]);
					Enumeration enumeration2 = vector.elements();
					while (enumeration2.hasMoreElements())
					{
						t = (Token)enumeration2.nextElement();
						text = (text)+(JavaCCGlobals.PrintToken(t));
					}
					text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				}
				text += " ";
				t = (Token)tryBlock.ids[i2];
				JavaCCGlobals.PrintTokenSetup(t);
				text = (text)+(JavaCCGlobals.PrintToken(t));
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				text = (text)+(") {\u0003\n");
				vector = (ArrayList)tryBlock.catchblks[i2];
				if (vector.Count != 0)
				{
					JavaCCGlobals.PrintTokenSetup((Token)vector[0]);
					JavaCCGlobals.ccol = 1;
					Enumeration enumeration2 = vector.elements();
					while (enumeration2.hasMoreElements())
					{
						t = (Token)enumeration2.nextElement();
						text = (text)+(JavaCCGlobals.PrintToken(t));
					}
					text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				}
				text = (text)+("\u0004\n}");
			}
			if (tryBlock.finallyblk != null)
			{
				text = (text)+(" finally {\u0003\n");
				if (tryBlock.finallyblk.Count != 0)
				{
					JavaCCGlobals.PrintTokenSetup((Token)tryBlock.finallyblk[0]);
					JavaCCGlobals.ccol = 1;
					Enumeration enumeration3 = tryBlock.finallyblk.elements();
					while (enumeration3.hasMoreElements())
					{
						t = (Token)enumeration3.nextElement();
						text = (text)+(JavaCCGlobals.PrintToken(t));
					}
					text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				}
				text = (text)+("\u0004\n}");
			}
		}
		return text;
	}

	
	internal static void dumpFormattedString(string P_0)
	{
		int num = 32;
		int num2 = 1;
		for (int i = 0; i < P_0.Length; i++)
		{
			int num3 = num;
			num = P_0[i];
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
					phase1NewLine();
				}
				else
				{
					ostr.WriteLine("");
				}
				break;
			case 1:
				indentamt += 2;
				break;
			case 2:
				indentamt -= 2;
				break;
			case 3:
				num2 = 0;
				break;
			case 4:
				num2 = 1;
				break;
			default:
				ostr.Write((char)num);
				break;
			}
		}
	}

	
	internal static string buildLookaheadChecker(Lookahead[] P_0, string[] P_1)
	{
		int num = 0;
		int num2 = 0;
		bool[] array = new bool[JavaCCGlobals.tokenCount];
		string text = "";
		Token t = null;
		int num3 = (JavaCCGlobals.tokenCount - 1) / 32 + 1;
		int[] array2 = null;
		int num4;
		for (num4 = 0; num4 < (nint)P_0.LongLength; num4++)
		{
			Lookahead lookahead = P_0[num4];
			jj2LA = false;
			if (lookahead.amount == 0 || Semanticize.EmptyExpansionExists(lookahead.la_expansion) || javaCodeCheck(lookahead.la_expansion))
			{
				if (lookahead.action_tokens.Count == 0)
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
						text = (text)+("\njj_la1[")+(JavaCCGlobals.maskindex)
							+("] = jj_gen;")
							;
						JavaCCGlobals.maskindex++;
					}
					JavaCCGlobals.maskVals.Add(array2);
					text = (text)+("\nif (");
					num2++;
					break;
				}
				JavaCCGlobals.PrintTokenSetup((Token)lookahead.action_tokens[0]);
				Enumeration enumeration = lookahead.action_tokens.elements();
				while (enumeration.hasMoreElements())
				{
					t = (Token)enumeration.nextElement();
					text = (text)+(JavaCCGlobals.PrintToken(t));
				}
				text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
				text = (text)+(") {\u0001")+(P_1[num4])
					;
				num = 1;
			}
			else if (lookahead.amount == 1 && lookahead.action_tokens.Count == 0)
			{
				if (firstSet == null)
				{
					firstSet = new bool[JavaCCGlobals.tokenCount];
				}
				for (int i = 0; i < JavaCCGlobals.tokenCount; i++)
				{
					firstSet[i] = false;
				}
				genFirstSet(lookahead.la_expansion);
				if (!jj2LA)
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
					for (int i = 0; i < JavaCCGlobals.tokenCount; i++)
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
				jj2LA = true;
			}
			goto IL_0463;
			IL_0335:
			for (int i = 0; i < JavaCCGlobals.tokenCount; i++)
			{
				if (firstSet[i] && !array[i])
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
					string text2 = (string)JavaCCGlobals.names_of_tokens.get((i));
					text = ((text2 != null) ? (text)+(text2) : (text)+(i));
					text = (text)+(":\u0001");
				}
			}
			text = (text)+(P_1[num4]);
			text = (text)+("\nbreak;");
			num = 2;
			goto IL_0463;
			IL_0463:
			if (jj2LA)
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
						text = (text)+("\njj_la1[")+(JavaCCGlobals.maskindex)
							+("] = jj_gen;")
							;
						JavaCCGlobals.maskindex++;
					}
					JavaCCGlobals.maskVals.Add(array2);
					text = (text)+("\nif (");
					num2++;
					break;
				}
				JavaCCGlobals.jj2index++;
				lookahead.la_expansion.internal_name = ("_")+(JavaCCGlobals.jj2index);
				phase2list.Add(lookahead);
				text = (text)+("jj_2")+(lookahead.la_expansion.internal_name)
					+("(")
					+(lookahead.amount)
					+(")")
					;
				if (lookahead.action_tokens.Count != 0)
				{
					text = (text)+(" && (");
					JavaCCGlobals.PrintTokenSetup((Token)lookahead.action_tokens[0]);
					Enumeration enumeration = lookahead.action_tokens.elements();
					while (enumeration.hasMoreElements())
					{
						t = (Token)enumeration.nextElement();
						text = (text)+(JavaCCGlobals.PrintToken(t));
					}
					text = (text)+(JavaCCGlobals.PrintTrailingComments(t));
					text = (text)+(")");
				}
				text = (text)+(") {\u0001")+(P_1[num4])
					;
				num = 1;
			}
		}
		switch (num)
		{
		case 0:
			text = (text)+(P_1[num4]);
			break;
		case 1:
			text = (text)+("\u0002\n} else {\u0001")+(P_1[num4])
				;
			break;
		case 2:
			text = (text)+("\u0002\ndefault:\u0001");
			if (Options.ErrorReporting)
			{
				text = (text)+("\njj_la1[")+(JavaCCGlobals.maskindex)
					+("] = jj_gen;")
					;
				JavaCCGlobals.maskVals.Add(array2);
				JavaCCGlobals.maskindex++;
			}
			text = (text)+(P_1[num4]);
			break;
		}
		for (int i = 0; i < num2; i++)
		{
			text = (text)+("\u0002\n}");
		}
		return text;
	}

	
	private static void generate3R(Expansion P_0, Phase3Data P_1)
	{
		Expansion expansion = P_0;
		if (string.Equals(P_0.internal_name, ""))
		{
			while (true)
			{
				if (expansion is Sequence && ((Sequence)expansion).Units.Count == 2)
				{
					expansion = (Expansion)((Sequence)expansion).Units.elementAt(1);
					continue;
				}
				if (!(expansion is NonTerminal))
				{
					break;
				}
				NonTerminal nonTerminal = (NonTerminal)expansion;
				NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.production_table.get(nonTerminal.name);
				if (normalProduction is JavaCodeProduction)
				{
					break;
				}
				expansion = normalProduction.Expansion;
			}
			if (expansion is RegularExpression)
			{
				P_0.internal_name = ("jj_scan_token(")+(((RegularExpression)expansion).ordinal)+(")")
					;
				return;
			}
			gensymindex++;
			P_0.internal_name = ("R_")+(gensymindex);
		}
		Phase3Data phase3Data = (Phase3Data)phase3table.get(P_0);
		if (phase3Data == null || phase3Data.count < P_1.count)
		{
			phase3Data = new Phase3Data(P_0, P_1.count);
			phase3list.Add(phase3Data);
			phase3table.Add(P_0, phase3Data);
		}
	}

	
	internal static void setupPhase3Builds(Phase3Data P_0)
	{
		Expansion exp = P_0.exp;
		if (exp is RegularExpression)
		{
			return;
		}
		if (exp is NonTerminal)
		{
			NonTerminal nonTerminal = (NonTerminal)exp;
			NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.production_table.get(nonTerminal.name);
			if (!(normalProduction is JavaCodeProduction))
			{
				generate3R(normalProduction.Expansion, P_0);
			}
		}
		else if (exp is Choice)
		{
			Choice choice = (Choice)exp;
			for (int i = 0; i < choice.Choices.Count; i++)
			{
				generate3R((Expansion)choice.Choices[i], P_0);
			}
		}
		else if (exp is Sequence)
		{
			Sequence sequence = (Sequence)exp;
			int i = P_0.count;
			for (int j = 1; j < sequence.Units.Count; j++)
			{
				Expansion expansion = (Expansion)sequence.Units[j];
				setupPhase3Builds(new Phase3Data(expansion, i));
				i -= minimumSize(expansion);
				if (i <= 0)
				{
					break;
				}
			}
		}
		else if (exp is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)exp;
			setupPhase3Builds(new Phase3Data(tryBlock.exp, P_0.count));
		}
		else if (exp is OneOrMore)
		{
			OneOrMore oneOrMore = (OneOrMore)exp;
			generate3R(oneOrMore.expansion, P_0);
		}
		else if (exp is ZeroOrMore)
		{
			ZeroOrMore zeroOrMore = (ZeroOrMore)exp;
			generate3R(zeroOrMore.expansion, P_0);
		}
		else if (exp is ZeroOrOne)
		{
			ZeroOrOne zeroOrOne = (ZeroOrOne)exp;
			generate3R(zeroOrOne.expansion, P_0);
		}
	}

	
	internal static int minimumSize(Expansion P_0)
	{
		int result = minimumSize(P_0, int.MaxValue);
		
		return result;
	}

	
	internal static string genReturn(bool P_0)
	{
		string str = ((!P_0) ? "false" : "true");
		if (Options.DebugLookahead && jj3_expansion != null)
		{
			string str2 = ("trace_return(\"")+(((NormalProduction)jj3_expansion.parent).lhs)+("(LOOKAHEAD ")
				+((!P_0) ? "SUCCEEDED" : "FAILED")
				+(")\");")
				;
			if (Options.ErrorReporting)
			{
				str2 = ("if (!jj_rescan) ")+(str2);
			}
			string result = ("{ ")+(str2)+(" return ")
				+(str)
				+("; }")
				;
			
			return result;
		}
		string result2 = ("return ")+(str)+(";")
			;
		
		return result2;
	}

	
	private static string genjj_3Call(Expansion P_0)
	{
		if (String.instancehelper_startsWith(P_0.internal_name, "jj_scan_token"))
		{
			return P_0.internal_name;
		}
		string result = ("jj_3")+(P_0.internal_name)+("()")
			;
		
		return result;
	}

	
	internal static void buildPhase3Routine(Phase3Data P_0, bool P_1)
	{
		Expansion exp = P_0.exp;
		Token t = null;
		if (String.instancehelper_startsWith(exp.internal_name, "jj_scan_token"))
		{
			return;
		}
		if (!P_1)
		{
			ostr.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_3")
				+(exp.internal_name)
				+("() {")
				);
			xsp_declared = false;
			if (Options.DebugLookahead && exp.parent is NormalProduction)
			{
				ostr.Write("    ");
				if (Options.ErrorReporting)
				{
					ostr.Write("if (!jj_rescan) ");
				}
				ostr.WriteLine(("trace_call(\"")+(((NormalProduction)exp.parent).lhs)+("(LOOKING AHEAD...)\");")
					);
				jj3_expansion = exp;
			}
			else
			{
				jj3_expansion = null;
			}
		}
		if (exp is RegularExpression)
		{
			RegularExpression regularExpression = (RegularExpression)exp;
			if (string.Equals(regularExpression.label, ""))
			{
				Hashtable hashtable = JavaCCGlobals.names_of_tokens;
				;
				object obj = hashtable.get((regularExpression.ordinal));
				if (obj != null)
				{
					ostr.WriteLine(("    if (jj_scan_token(")+((string)obj)+(")) ")
						+(genReturn(true))
						);
				}
				else
				{
					ostr.WriteLine(("    if (jj_scan_token(")+(regularExpression.ordinal)+(")) ")
						+(genReturn(true))
						);
				}
			}
			else
			{
				ostr.WriteLine(("    if (jj_scan_token(")+(regularExpression.label)+(")) ")
					+(genReturn(true))
					);
			}
		}
		else if (exp is NonTerminal)
		{
			NonTerminal nonTerminal = (NonTerminal)exp;
			NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.production_table.get(nonTerminal.name);
			if (normalProduction is JavaCodeProduction)
			{
				ostr.WriteLine(("    if (true) { jj_la = 0; jj_scanpos = jj_lastpos; ")+(genReturn(false))+("}")
					);
			}
			else
			{
				Expansion expansion = normalProduction.Expansion;
				ostr.WriteLine(("    if (")+(genjj_3Call(expansion))+(") ")
					+(genReturn(true))
					);
			}
		}
		else if (exp is Choice)
		{
			Choice choice = (Choice)exp;
			if (choice.Choices.Count != 1)
			{
				if (!xsp_declared)
				{
					xsp_declared = true;
					ostr.WriteLine("    Token xsp;");
				}
				ostr.WriteLine("    xsp = jj_scanpos;");
			}
			for (int i = 0; i < choice.Choices.Count; i++)
			{
				Sequence sequence = (Sequence)choice.Choices[i];
				Lookahead lookahead = (Lookahead)sequence.Units[0];
				if (lookahead.action_tokens.Count != 0)
				{
					ostr.WriteLine("    jj_lookingAhead = true;");
					ostr.Write("    jj_semLA = ");
					JavaCCGlobals.PrintTokenSetup((Token)lookahead.action_tokens[0]);
					Enumeration enumeration = lookahead.action_tokens.elements();
					while (enumeration.hasMoreElements())
					{
						t = (Token)enumeration.nextElement();
						JavaCCGlobals.PrintToken(t, ostr);
					}
					JavaCCGlobals.PrintTrailingComments(t, ostr);
					ostr.WriteLine(";");
					ostr.WriteLine("    jj_lookingAhead = false;");
				}
				ostr.Write("    if (");
				if (lookahead.action_tokens.Count != 0)
				{
					ostr.Write("!jj_semLA || ");
				}
				if (i != choice.Choices.Count - 1)
				{
					ostr.WriteLine((genjj_3Call(sequence))+(") {"));
					ostr.WriteLine("    jj_scanpos = xsp;");
				}
				else
				{
					ostr.WriteLine((genjj_3Call(sequence))+(") ")+(genReturn(true))
						);
				}
			}
			for (int i = 1; i < choice.Choices.Count; i++)
			{
				ostr.WriteLine("    }");
			}
		}
		else if (exp is Sequence)
		{
			Sequence sequence = (Sequence)exp;
			int num = P_0.count;
			for (int i = 1; i < sequence.Units.Count; i++)
			{
				Expansion expansion2 = (Expansion)sequence.Units[i];
				buildPhase3Routine(new Phase3Data(expansion2, num), true);
				num -= minimumSize(expansion2);
				if (num <= 0)
				{
					break;
				}
			}
		}
		else if (exp is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)exp;
			buildPhase3Routine(new Phase3Data(tryBlock.exp, P_0.count), true);
		}
		else if (exp is OneOrMore)
		{
			if (!xsp_declared)
			{
				xsp_declared = true;
				ostr.WriteLine("    Token xsp;");
			}
			OneOrMore oneOrMore = (OneOrMore)exp;
			Expansion expansion3 = oneOrMore.expansion;
			ostr.WriteLine(("    if (")+(genjj_3Call(expansion3))+(") ")
				+(genReturn(true))
				);
			ostr.WriteLine("    while (true) {");
			ostr.WriteLine("      xsp = jj_scanpos;");
			ostr.WriteLine(("      if (")+(genjj_3Call(expansion3))+(") { jj_scanpos = xsp; break; }")
				);
			ostr.WriteLine("    }");
		}
		else if (exp is ZeroOrMore)
		{
			if (!xsp_declared)
			{
				xsp_declared = true;
				ostr.WriteLine("    Token xsp;");
			}
			ZeroOrMore zeroOrMore = (ZeroOrMore)exp;
			Expansion expansion3 = zeroOrMore.expansion;
			ostr.WriteLine("    while (true) {");
			ostr.WriteLine("      xsp = jj_scanpos;");
			ostr.WriteLine(("      if (")+(genjj_3Call(expansion3))+(") { jj_scanpos = xsp; break; }")
				);
			ostr.WriteLine("    }");
		}
		else if (exp is ZeroOrOne)
		{
			if (!xsp_declared)
			{
				xsp_declared = true;
				ostr.WriteLine("    Token xsp;");
			}
			ZeroOrOne zeroOrOne = (ZeroOrOne)exp;
			Expansion expansion3 = zeroOrOne.expansion;
			ostr.WriteLine("    xsp = jj_scanpos;");
			ostr.WriteLine(("    if (")+(genjj_3Call(expansion3))+(") jj_scanpos = xsp;")
				);
		}
		if (!P_1)
		{
			ostr.WriteLine(("    ")+(genReturn(false)));
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
	}

	
	internal static int minimumSize(Expansion P_0, int P_1)
	{
		int result = 0;
		if (P_0.inMinimumSize)
		{
			return int.MaxValue;
		}
		P_0.inMinimumSize = true;
		if (P_0 is RegularExpression)
		{
			result = 1;
		}
		else if (P_0 is NonTerminal)
		{
			NonTerminal nonTerminal = (NonTerminal)P_0;
			NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.production_table.get(nonTerminal.name);
			if (normalProduction is JavaCodeProduction)
			{
				result = int.MaxValue;
			}
			else
			{
				Expansion expansion = normalProduction.Expansion;
				result = minimumSize(expansion);
			}
		}
		else if (P_0 is Choice)
		{
			int num = P_1;
			Choice choice = (Choice)P_0;
			int num2 = 0;
			while (num > 1 && num2 < choice.Choices.Count)
			{
				Expansion expansion2 = (Expansion)choice.Choices[num2];
				int num3 = minimumSize(expansion2, num);
				if (num > num3)
				{
					num = num3;
				}
				num2++;
			}
			result = num;
		}
		else if (P_0 is Sequence)
		{
			int num = 0;
			Sequence sequence = (Sequence)P_0;
			for (int i = 1; i < sequence.Units.Count; i++)
			{
				Expansion expansion3 = (Expansion)sequence.Units[i];
				int num3 = minimumSize(expansion3);
				if (num == int.MaxValue || num3 == int.MaxValue)
				{
					num = int.MaxValue;
					continue;
				}
				num += num3;
				if (num > P_1)
				{
					break;
				}
			}
			result = num;
		}
		else if (P_0 is TryBlock)
		{
			TryBlock tryBlock = (TryBlock)P_0;
			result = minimumSize(tryBlock.exp);
		}
		else if (P_0 is OneOrMore)
		{
			OneOrMore oneOrMore = (OneOrMore)P_0;
			result = minimumSize(oneOrMore.expansion);
		}
		else if (P_0 is ZeroOrMore)
		{
			result = 0;
		}
		else if (P_0 is ZeroOrOne)
		{
			result = 0;
		}
		else if (P_0 is Lookahead)
		{
			result = 0;
		}
		else if (P_0 is Action)
		{
			result = 0;
		}
		P_0.inMinimumSize = false;
		return result;
	}

	
	internal static void buildPhase1Routine(BNFProduction P_0)
	{
		Token token = (Token)P_0.return_type_tokens[0];
		int num = 0;
		if (token.kind == 77)
		{
			num = 1;
		}
		JavaCCGlobals.PrintTokenSetup(token);
		JavaCCGlobals.ccol = 1;
		JavaCCGlobals.PrintLeadingComments(token, ostr);
		ostr.Write(("  ")+(JavaCCGlobals.StaticOpt())+("final ")
			+((P_0.accessMod == null) ? "public" : P_0.accessMod)
			+(" ")
			);
		JavaCCGlobals.cline = token.BeginLine;
		JavaCCGlobals.ccol = token.BeginColumn;
		JavaCCGlobals.PrintTokenOnly(token, ostr);
		for (int i = 1; i < P_0.return_type_tokens.Count; i++)
		{
			token = (Token)P_0.return_type_tokens[i];
			JavaCCGlobals.PrintToken(token, ostr);
		}
		JavaCCGlobals.PrintTrailingComments(token, ostr);
		ostr.Write((" ")+(P_0.lhs)+("(")
			);
		Enumeration enumeration;
		if (P_0.parameter_list_tokens.Count != 0)
		{
			JavaCCGlobals.PrintTokenSetup((Token)P_0.parameter_list_tokens[0]);
			enumeration = P_0.parameter_list_tokens.elements();
			while (enumeration.hasMoreElements())
			{
				token = (Token)enumeration.nextElement();
				JavaCCGlobals.PrintToken(token, ostr);
			}
			JavaCCGlobals.PrintTrailingComments(token, ostr);
		}
		ostr.Write(") throws ParseException");
		enumeration = P_0.ThrowsList.elements();
		while (enumeration.hasMoreElements())
		{
			ostr.Write(", ");
			ArrayList vector = (ArrayList)enumeration.nextElement();
			Enumeration enumeration2 = vector.elements();
			while (enumeration2.hasMoreElements())
			{
				token = (Token)enumeration2.nextElement();
				ostr.Write(token.image);
			}
		}
		ostr.Write(" {");
		indentamt = 4;
		if (Options.DebugParser)
		{
			ostr.WriteLine("");
			ostr.WriteLine(("    trace_call(\"")+(P_0.lhs)+("\");")
				);
			ostr.Write("    try {");
			indentamt = 6;
		}
		if (P_0.declaration_tokens.Count != 0)
		{
			JavaCCGlobals.PrintTokenSetup((Token)P_0.declaration_tokens[0]);
			JavaCCGlobals.cline--;
			enumeration = P_0.declaration_tokens.elements();
			while (enumeration.hasMoreElements())
			{
				token = (Token)enumeration.nextElement();
				JavaCCGlobals.PrintToken(token, ostr);
			}
			JavaCCGlobals.PrintTrailingComments(token, ostr);
		}
		string text = phase1ExpansionGen(P_0.Expansion);
		dumpFormattedString(text);
		ostr.WriteLine("");
		if (P_0.jumpPatched && num == 0)
		{
			ostr.WriteLine("    throw new System.Exception(\"Missing return statement in function\");");
		}
		if (Options.DebugParser)
		{
			ostr.WriteLine("    } finally {");
			ostr.WriteLine(("      trace_return(\"")+(P_0.lhs)+("\");")
				);
			ostr.WriteLine("    }");
		}
		ostr.WriteLine("  }");
		ostr.WriteLine("");
	}

	
	internal static void buildPhase2Routine(Lookahead P_0)
	{
		Expansion la_expansion = P_0.la_expansion;
		ostr.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_2")
			+(la_expansion.internal_name)
			+("(int xla) {")
			);
		ostr.WriteLine("    jj_la = xla; jj_lastpos = jj_scanpos = token;");
		ostr.WriteLine(("    try { return !jj_3")+(la_expansion.internal_name)+("(); }")
			);
		ostr.WriteLine("    catch(LookaheadSuccess ls) { return true; }");
		if (Options.ErrorReporting)
		{
			ostr.WriteLine(("    finally { jj_save(")+(int.parseInt(String.instancehelper_substring(la_expansion.internal_name, 1)) - 1)+(", xla); }")
				);
		}
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		Phase3Data phase3Data = new Phase3Data(la_expansion, P_0.amount);
		phase3list.Add(phase3Data);
		phase3table.Add(la_expansion, phase3Data);
	}

	
	public ParseEngine()
	{
	}

	
	private static void dumpLookaheads(Lookahead[] P_0, string[] P_1)
	{
		for (int i = 0; i < (nint)P_0.LongLength; i++)
		{
			Console.Error.WriteLine(("Lookahead: ")+(i));
			Console.Error.WriteLine(P_0[i].Dump(0, new()));
			Console.Error.WriteLine();
		}
	}

	
	internal static void build(TextWriter P_0)
	{
		
		ostr = P_0;
		Enumeration enumeration = JavaCCGlobals.bnfproductions.elements();
		Enumeration enumeration2;
		while (enumeration.hasMoreElements())
		{
			NormalProduction normalProduction = (NormalProduction)enumeration.nextElement();
			if (normalProduction is JavaCodeProduction)
			{
				JavaCodeProduction javaCodeProduction = (JavaCodeProduction)normalProduction;
				Token token = (Token)javaCodeProduction.return_type_tokens[0];
				JavaCCGlobals.PrintTokenSetup(token);
				JavaCCGlobals.ccol = 1;
				JavaCCGlobals.PrintLeadingComments(token, ostr);
				ostr.Write(("  ")+(JavaCCGlobals.StaticOpt())+((normalProduction.accessMod == null) ? "" : (normalProduction.accessMod)+(" "))
					);
				JavaCCGlobals.cline = token.BeginLine;
				JavaCCGlobals.ccol = token.BeginColumn;
				JavaCCGlobals.PrintTokenOnly(token, ostr);
				for (int i = 1; i < javaCodeProduction.return_type_tokens.Count; i++)
				{
					token = (Token)javaCodeProduction.return_type_tokens[i];
					JavaCCGlobals.PrintToken(token, ostr);
				}
				JavaCCGlobals.PrintTrailingComments(token, ostr);
				ostr.Write((" ")+(javaCodeProduction.lhs)+("(")
					);
				if (javaCodeProduction.parameter_list_tokens.Count != 0)
				{
					JavaCCGlobals.PrintTokenSetup((Token)javaCodeProduction.parameter_list_tokens[0]);
					enumeration2 = javaCodeProduction.parameter_list_tokens.elements();
					while (enumeration2.hasMoreElements())
					{
						token = (Token)enumeration2.nextElement();
						JavaCCGlobals.PrintToken(token, ostr);
					}
					JavaCCGlobals.PrintTrailingComments(token, ostr);
				}
				ostr.Write(") throws ParseException");
				enumeration2 = javaCodeProduction.ThrowsList.elements();
				while (enumeration2.hasMoreElements())
				{
					ostr.Write(", ");
					ArrayList vector = (ArrayList)enumeration2.nextElement();
					Enumeration enumeration3 = vector.elements();
					while (enumeration3.hasMoreElements())
					{
						token = (Token)enumeration3.nextElement();
						ostr.Write(token.image);
					}
				}
				ostr.Write(" {");
				if (Options.DebugParser)
				{
					ostr.WriteLine("");
					ostr.WriteLine(("    trace_call(\"")+(javaCodeProduction.lhs)+("\");")
						);
					ostr.Write("    try {");
				}
				if (javaCodeProduction.CodeTokens.Count > 0)
				{
					JavaCCGlobals.PrintTokenSetup(javaCodeProduction.CodeTokens[0]);
					JavaCCGlobals.cline--;
					foreach (var t in javaCodeProduction.CodeTokens)
                    {
						JavaCCGlobals.PrintToken(t, ostr);
					}
					JavaCCGlobals.PrintTrailingComments(token, ostr);
				}
				ostr.WriteLine("");
				if (Options.DebugParser)
				{
					ostr.WriteLine("    } finally {");
					ostr.WriteLine(("      trace_return(\"")+(javaCodeProduction.lhs)+("\");")
						);
					ostr.WriteLine("    }");
				}
				ostr.WriteLine("  }");
				ostr.WriteLine("");
			}
			else
			{
				buildPhase1Routine((BNFProduction)normalProduction);
			}
		}
		int j;
		for (j = 0; j < phase2list.Count; j++)
		{
			buildPhase2Routine((Lookahead)phase2list[j]);
		}
		j = 0;
		while (j < phase3list.Count)
		{
			for (; j < phase3list.Count; j++)
			{
				setupPhase3Builds((Phase3Data)phase3list[j]);
			}
		}
		enumeration2 = phase3table.elements();
		while (enumeration2.hasMoreElements())
		{
			buildPhase3Routine((Phase3Data)enumeration2.nextElement(), false);
		}
	}

	static ParseEngine()
	{
		
		gensymindex = 0;
		phase2list = new ArrayList();
		phase3list = new ArrayList();
		phase3table = new Hashtable();
		generated = new Hashtable();
	}
}
