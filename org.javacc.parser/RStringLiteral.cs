using javacc.net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace org.javacc.parser;


public class RStringLiteral : RegularExpression
{
	public string image;

	internal static int maxStrKind;

	internal static int maxLen;

	internal static int charCnt;

	internal static ArrayList charPosKind;

	internal static int[] maxLenForActive;

	public static string[] allImages;

	internal static int[][] intermediateKinds;

	internal static int[][] intermediateMatchedPos;

	internal static int startStateCnt;

	internal static bool[] subString;

	internal static bool[] subStringAtPos;

	internal static Hashtable[] statesForPos;

	private static bool boilerPlateDumped;

	
	public RStringLiteral(Token t, string str)
	{
		Line = t.BeginLine;
		Column = t.BeginColumn;
		image = str;
	}

	
	public static void ReInit()
	{
		maxStrKind = 0;
		maxLen = 0;
		charPosKind = new ArrayList();
		maxLenForActive = new int[100];
		intermediateKinds = null;
		intermediateMatchedPos = null;
		startStateCnt = 0;
		subString = null;
		subStringAtPos = null;
		statesForPos = null;
	}

	
	public virtual void GenerateDfa(TextWriter pw, int i)
	{
		if (maxStrKind <= ordinal)
		{
			maxStrKind = ordinal + 1;
		}
		int num;
		if ((num = image.Length) > maxLen)
		{
			maxLen = num;
		}
		for (int j = 0; j < num; j++)
		{
			int num2;
			string key = ((!Options.getIgnoreCase()) ? ("")+((char)(num2 = java.lang.String.instancehelper_charAt(image, j))) : java.lang.String.instancehelper_toLowerCase(("")+((char)(num2 = java.lang.String.instancehelper_charAt(image, j)))));
			if (!NfaState.unicodeWarningGiven && num2 > 255 && !Options.getJavaUnicodeEscape() && !Options.getUserCharStream())
			{
				NfaState.unicodeWarningGiven = true;
				JavaCCErrors.Warning(LexGen.curRE, "Non-ASCII characters used in regular expression.Please make sure you use the correct TextReader when you create the parser, one that can handle your character set.");
			}
			Hashtable hashtable;
			if (j >= charPosKind.Count)
			{
				charPosKind.Add(hashtable = new Hashtable());
			}
			else
			{
				hashtable = (Hashtable)charPosKind[j];
			}
			KindInfo kindInfo;
			if ((kindInfo = (KindInfo)hashtable.get(key)) == null)
			{
				hashtable.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
			}
			if (j + 1 == num)
			{
				kindInfo.InsertFinalKind(ordinal);
			}
			else
			{
				kindInfo.InsertValidKind(ordinal);
			}
			if (!Options.getIgnoreCase() && LexGen.ignoreCase[ordinal] && num2 != Char.ToLower((char)num2))
			{
				key = Char.ToLower(image[j]);
				if (j >= charPosKind.Count)
				{
					charPosKind.Add(hashtable = new Hashtable());
				}
				else
				{
					hashtable = (Hashtable)charPosKind[j];
				}
				if ((kindInfo = (KindInfo)hashtable.get(key)) == null)
				{
					hashtable.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
				}
				if (j + 1 == num)
				{
					kindInfo.InsertFinalKind(ordinal);
				}
				else
				{
					kindInfo.InsertValidKind(ordinal);
				}
			}
			if (!Options.getIgnoreCase() && LexGen.ignoreCase[ordinal] && num2 != char.ToUpper((char)num2))
			{
				key = java.lang.String.instancehelper_toUpperCase(("")+(java.lang.String.instancehelper_charAt(image, j)));
				if (j >= charPosKind.Count)
				{
					charPosKind.Add(hashtable = new Hashtable());
				}
				else
				{
					hashtable = (Hashtable)charPosKind[j];
				}
				if ((kindInfo = (KindInfo)hashtable.get(key)) == null)
				{
					hashtable.Add(key, kindInfo = new KindInfo(LexGen.maxOrdinal));
				}
				if (j + 1 == num)
				{
					kindInfo.InsertFinalKind(ordinal);
				}
				else
				{
					kindInfo.InsertValidKind(ordinal);
				}
			}
		}
		maxLenForActive[ordinal / 64] = Math.Max(maxLenForActive[ordinal / 64], num - 1);
		allImages[ordinal] = image;
	}

	
	internal static void FillSubString()
	{
		subString = new bool[maxStrKind + 1];
		subStringAtPos = new bool[maxLen];
		for (int i = 0; i < maxStrKind; i++)
		{
			subString[i] = false;
			string text;
			if ((text = allImages[i]) == null || LexGen.lexStates[i] != LexGen.lexStateIndex)
			{
				continue;
			}
			if (LexGen.mixed[LexGen.lexStateIndex])
			{
				subString[i] = true;
				subStringAtPos[text.Length - 1] = true;
				continue;
			}
			for (int j = 0; j < maxStrKind; j++)
			{
				if (j != i && LexGen.lexStates[j] == LexGen.lexStateIndex && allImages[j] != null)
				{
					if (java.lang.String.instancehelper_indexOf(allImages[j], text) == 0)
					{
						subString[i] = true;
						subStringAtPos[text.Length - 1] = true;
						break;
					}
					if (Options.getIgnoreCase() && StartsWithIgnoreCase(allImages[j], text))
					{
						subString[i] = true;
						subStringAtPos[text.Length - 1] = true;
						break;
					}
				}
			}
		}
	}

	
	internal static void GenerateNfaStartStates(TextWriter P_0, NfaState P_1)
	{
		bool[] array = new bool[NfaState.generatedStates];
		Hashtable hashtable = new Hashtable();
		string text = "";
		_ = 0;
		int num = maxStrKind / 64 + 1;
		ArrayList vector = new ArrayList();
		ArrayList vector2 = null;
		statesForPos = new Hashtable[maxLen];
		intermediateKinds = new int[maxStrKind + 1][];
		intermediateMatchedPos = new int[maxStrKind + 1][];
		for (int i = 0; i < maxStrKind; i++)
		{
			if (LexGen.lexStates[i] != LexGen.lexStateIndex)
			{
				continue;
			}
			string text2 = allImages[i];
			if (text2 == null || text2.Length < 1)
			{
				continue;
			}
			try
			{
				if ((vector2 = (ArrayList)P_1.epsilonMoves.Clone()) == null || vector2.Count == 0)
				{
					DumpNfaStartStatesCode(statesForPos, P_0);
					return;
				}
			}
			catch (System.Exception x)
			{
				goto IL_00e1;
			}
			goto IL_00f2;
			IL_00e1:
			
			JavaCCErrors.Semantic_Error("Error cloning state vector");
			goto IL_00f2;
			IL_00f2:
			intermediateKinds[i] = new int[text2.Length];
			intermediateMatchedPos[i] = new int[text2.Length];
			int i2 = 0;
			_ = int.MaxValue;
			for (int j = 0; j < text2.Length; j++)
			{
				int num6;
				int num3;
				if (vector2 == null || vector2.Count <= 0)
				{
					int[] obj = intermediateKinds[i];
					int num2 = j;
					num3 = intermediateKinds[i][j - 1];
					int num4 = num2;
					int[] array2 = obj;
					int num5 = num3;
					array2[num4] = num3;
					num6 = num5;
					int[] obj2 = intermediateMatchedPos[i];
					int num7 = j;
					num3 = intermediateMatchedPos[i][j - 1];
					num4 = num7;
					array2 = obj2;
					int num8 = num3;
					array2[num4] = num3;
					i2 = num8;
				}
				else
				{
					num6 = NfaState.MoveFromSet(java.lang.String.instancehelper_charAt(text2, j), vector2, vector);
					vector2.Clear();
					if (j == 0 && num6 != int.MaxValue && LexGen.canMatchAnyChar[LexGen.lexStateIndex] != -1 && num6 > LexGen.canMatchAnyChar[LexGen.lexStateIndex])
					{
						num6 = LexGen.canMatchAnyChar[LexGen.lexStateIndex];
					}
					if (GetStrKind(java.lang.String.instancehelper_substring(text2, 0, j + 1)) < num6)
					{
						num6 = (intermediateKinds[i][j] = int.MaxValue);
						i2 = 0;
					}
					else if (num6 != int.MaxValue)
					{
						intermediateKinds[i][j] = num6;
						int[] obj3 = intermediateMatchedPos[i];
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
						int[] obj4 = intermediateKinds[i];
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
						int[] obj5 = intermediateKinds[i];
						int num13 = j;
						num3 = intermediateKinds[i][j - 1];
						int num4 = num13;
						int[] array2 = obj5;
						int num14 = num3;
						array2[num4] = num3;
						num6 = num14;
						int[] obj6 = intermediateMatchedPos[i];
						int num15 = j;
						num3 = intermediateMatchedPos[i][j - 1];
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
				if (hashtable.get(text) == null)
				{
					hashtable.Add(text, text);
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
				ArrayList vector3 = vector2;
				vector2 = vector;
				(vector = vector3).Clear();
				if (statesForPos[j] == null)
				{
					statesForPos[j] = new Hashtable();
				}
				long[] array3;
				if ((array3 = (long[])statesForPos[j].get((num6)+(", ")+(i2)
					+(", ")
					+(text)
					)) == null)
				{
					array3 = new long[num];
					statesForPos[j].Add((num6)+(", ")+(i2)
						+(", ")
						+(text)
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
		DumpNfaStartStatesCode(statesForPos, P_0);
	}

	
	internal static void DumpDfaCode(TextWriter P_0)
	{
		int num = maxStrKind / 64 + 1;
		LexGen.maxLongsReqd[LexGen.lexStateIndex] = num;
		if (maxLen == 0)
		{
			P_0.WriteLine(((!Options.getStatic()) ? "" : "static ")+("private int ")+("jjMoveStringLiteralDfa0")
				+(LexGen.lexStateSuffix)
				+("()")
				);
			DumpNullStrLiterals(P_0);
			return;
		}
		if (!boilerPlateDumped)
		{
			DumpBoilerPlate(P_0);
			boilerPlateDumped = true;
		}
		if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
		{
			DumpStartWithStates(P_0);
		}
		for (int i = 0; i < maxLen; i++)
		{
			int num2 = 0;
			int num3 = 0;
			Hashtable hashtable = (Hashtable)charPosKind[i];
			string[] array = ReArrange(hashtable);
			P_0.Write(((!Options.getStatic()) ? "" : "static ")+("private int ")+("jjMoveStringLiteralDfa")
				+(i)
				+(LexGen.lexStateSuffix)
				+("(")
				);
			switch (i)
			{
			case 1:
			{
				int j;
				for (j = 0; j < num - 1; j++)
				{
					if (i <= maxLenForActive[j])
					{
						if (num2 != 0)
						{
							P_0.Write(", ");
						}
						else
						{
							num2 = 1;
						}
						P_0.Write(("long active")+(j));
					}
				}
				if (i <= maxLenForActive[j])
				{
					if (num2 != 0)
					{
						P_0.Write(", ");
					}
					P_0.Write(("long active")+(j));
				}
				break;
			}
			default:
			{
				int j;
				for (j = 0; j < num - 1; j++)
				{
					if (i <= maxLenForActive[j] + 1)
					{
						if (num2 != 0)
						{
							P_0.Write(", ");
						}
						else
						{
							num2 = 1;
						}
						P_0.Write(("long old")+(j)+(", long active")
							+(j)
							);
					}
				}
				if (i <= maxLenForActive[j] + 1)
				{
					if (num2 != 0)
					{
						P_0.Write(", ");
					}
					P_0.Write(("long old")+(j)+(", long active")
						+(j)
						);
				}
				break;
			}
			case 0:
				break;
			}
			P_0.WriteLine(")");
			P_0.WriteLine("{");
			if (i != 0)
			{
				if (i > 1)
				{
					num2 = 0;
					P_0.Write("   if ((");
					int j;
					for (j = 0; j < num - 1; j++)
					{
						if (i <= maxLenForActive[j] + 1)
						{
							if (num2 != 0)
							{
								P_0.Write(" | ");
							}
							else
							{
								num2 = 1;
							}
							P_0.Write(("(active")+(j)+(" &= old")
								+(j)
								+(")")
								);
						}
					}
					if (i <= maxLenForActive[j] + 1)
					{
						if (num2 != 0)
						{
							P_0.Write(" | ");
						}
						P_0.Write(("(active")+(j)+(" &= old")
							+(j)
							+(")")
							);
					}
					P_0.WriteLine(") == 0L)");
					if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
					{
						P_0.Write(("      return jjStartNfa")+(LexGen.lexStateSuffix)+("(")
							+(i - 2)
							+(", ")
							);
						for (j = 0; j < num - 1; j++)
						{
							if (i <= maxLenForActive[j] + 1)
							{
								P_0.Write(("old")+(j)+(", ")
									);
							}
							else
							{
								P_0.Write("0L, ");
							}
						}
						if (i <= maxLenForActive[j] + 1)
						{
							P_0.WriteLine(("old")+(j)+("); ")
								);
						}
						else
						{
							P_0.WriteLine("0L);");
						}
					}
					else if (NfaState.generatedStates != 0)
					{
						P_0.WriteLine(("      return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
							+(NfaState.InitStateName())
							+(", ")
							+(i - 1)
							+(");")
							);
					}
					else
					{
						P_0.WriteLine(("      return ")+(i)+(";")
							);
					}
				}
				if (i != 0 && Options.getDebugTokenManager())
				{
					P_0.WriteLine(("   if (jjmatchedKind != 0 && jjmatchedKind != 0x")+(Utils.ToString(int.MaxValue,16))+(")")
						);
					P_0.WriteLine("      debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
					P_0.WriteLine("   debugStream.WriteLine(\"   Possible string literal matches : { \"");
					for (int k = 0; k < maxStrKind / 64 + 1; k++)
					{
						if (i <= maxLenForActive[k])
						{
							P_0.WriteLine(" + ");
							P_0.Write(("         jjKindsForBitVector(")+(k)+(", ")
								);
							P_0.Write(("active")+(k)+(") ")
								);
						}
					}
					P_0.WriteLine(" + \" } \");");
				}
				P_0.WriteLine("   try { curChar = input_stream.readChar(); }");
				P_0.WriteLine("   catch(java.io.IOException e) {");
				if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
				{
					P_0.Write(("      jjStopStringLiteralDfa")+(LexGen.lexStateSuffix)+("(")
						+(i - 1)
						+(", ")
						);
					int l;
					for (l = 0; l < num - 1; l++)
					{
						if (i <= maxLenForActive[l])
						{
							P_0.Write(("active")+(l)+(", ")
								);
						}
						else
						{
							P_0.Write("0L, ");
						}
					}
					if (i <= maxLenForActive[l])
					{
						P_0.WriteLine(("active")+(l)+(");")
							);
					}
					else
					{
						P_0.WriteLine("0L);");
					}
					if (i != 0 && Options.getDebugTokenManager())
					{
						P_0.WriteLine(("      if (jjmatchedKind != 0 && jjmatchedKind != 0x")+(Utils.ToString(int.MaxValue,16))+(")")
							);
						P_0.WriteLine("         debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
					}
					P_0.WriteLine(("      return ")+(i)+(";")
						);
				}
				else if (NfaState.generatedStates != 0)
				{
					P_0.WriteLine(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
						+(NfaState.InitStateName())
						+(", ")
						+(i - 1)
						+(");")
						);
				}
				else
				{
					P_0.WriteLine(("      return ")+(i)+(";")
						);
				}
				P_0.WriteLine("   }");
			}
			if (i != 0 && Options.getDebugTokenManager())
			{
				P_0.WriteLine(("   debugStream.WriteLine(")+((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ")+("\"Current character : \" + ")
					+("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
					+("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
					);
			}
			P_0.WriteLine("   switch(curChar)");
			P_0.WriteLine("   {");
			for (int k = 0; k < (nint)array.LongLength; k++)
			{
				string text = array[k];
				KindInfo kindInfo = (KindInfo)hashtable.get(text);
				int num4 = 0;
				int num5 = java.lang.String.instancehelper_charAt(text, 0);
				int num6;
				if (i == 0 && num5 < 128 && kindInfo.finalKindCnt != 0 && (NfaState.generatedStates == 0 || !NfaState.CanStartNfaUsingAscii((char)num5)))
				{
					int j;
					for (j = 0; j < num && kindInfo.finalKinds[j] == 0; j++)
					{
					}
					for (int l = 0; l < 64; l++)
					{
						if ((kindInfo.finalKinds[j] & (1L << l)) == 0 || subString[num6 = j * 64 + l])
						{
							continue;
						}
						if ((intermediateKinds != null && intermediateKinds[j * 64 + l] != null && intermediateKinds[j * 64 + l][i] < j * 64 + l && intermediateMatchedPos != null && intermediateMatchedPos[j * 64 + l][i] == i) || (LexGen.canMatchAnyChar[LexGen.lexStateIndex] >= 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] < j * 64 + l))
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
				if (Options.getIgnoreCase())
				{
					if (num5 != char.ToUpper((char)num5))
					{
						P_0.WriteLine(("      case ")+((int)char.ToUpper((char)num5))+(":")
							);
					}
					if (num5 != Char.ToLower((char)num5))
					{
						P_0.WriteLine(("      case ")+((int)Char.ToLower((char)num5))+(":")
							);
					}
				}
				P_0.WriteLine(("      case ")+(num5)+(":")
					);
				string str = ((i != 0) ? "            " : "         ");
				if (kindInfo.finalKindCnt != 0)
				{
					for (int j = 0; j < num; j++)
					{
						long num7;
						if ((num7 = kindInfo.finalKinds[j]) == 0)
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
								P_0.Write("         else if ");
							}
							else if (i != 0)
							{
								P_0.Write("         if ");
							}
							num4 = 1;
							if (i != 0)
							{
								P_0.WriteLine(("((active")+(j)+(" & 0x")
									+(Long.toHexString(1L << l))
									+("L) != 0L)")
									);
							}
							int i2;
							if (intermediateKinds != null && intermediateKinds[j * 64 + l] != null && intermediateKinds[j * 64 + l][i] < j * 64 + l && intermediateMatchedPos != null && intermediateMatchedPos[j * 64 + l][i] == i)
							{
								JavaCCErrors.Warning((" \"")+(JavaCCGlobals.AddEscapes(allImages[j * 64 + l]))+("\" cannot be matched as a string literal token ")
									+("at line ")
									+(GetLine(j * 64 + l))
									+(", column ")
									+(GetColumn(j * 64 + l))
									+(". It will be matched as ")
									+(GetLabel(intermediateKinds[j * 64 + l][i]))
									+(".")
									);
								i2 = intermediateKinds[j * 64 + l][i];
							}
							else if (i == 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] >= 0 && LexGen.canMatchAnyChar[LexGen.lexStateIndex] < j * 64 + l)
							{
								JavaCCErrors.Warning((" \"")+(JavaCCGlobals.AddEscapes(allImages[j * 64 + l]))+("\" cannot be matched as a string literal token ")
									+("at line ")
									+(GetLine(j * 64 + l))
									+(", column ")
									+(GetColumn(j * 64 + l))
									+(". It will be matched as ")
									+(GetLabel(LexGen.canMatchAnyChar[LexGen.lexStateIndex]))
									+(".")
									);
								i2 = LexGen.canMatchAnyChar[LexGen.lexStateIndex];
							}
							else
							{
								i2 = j * 64 + l;
							}
							if (!subString[j * 64 + l])
							{
								int stateSetForKind = GetStateSetForKind(i, j * 64 + l);
								if (stateSetForKind != -1)
								{
									P_0.WriteLine((str)+("return jjStartNfaWithStates")+(LexGen.lexStateSuffix)
										+("(")
										+(i)
										+(", ")
										+(i2)
										+(", ")
										+(stateSetForKind)
										+(");")
										);
								}
								else
								{
									P_0.WriteLine((str)+("return jjStopAtPos")+("(")
										+(i)
										+(", ")
										+(i2)
										+(");")
										);
								}
							}
							else if ((LexGen.initMatch[LexGen.lexStateIndex] != 0 && LexGen.initMatch[LexGen.lexStateIndex] != int.MaxValue) || i != 0)
							{
								P_0.WriteLine("         {");
								P_0.WriteLine((str)+("jjmatchedKind = ")+(i2)
									+(";")
									);
								P_0.WriteLine((str)+("jjmatchedPos = ")+(i)
									+(";")
									);
								P_0.WriteLine("         }");
							}
							else
							{
								P_0.WriteLine((str)+("jjmatchedKind = ")+(i2)
									+(";")
									);
							}
						}
					}
				}
				if (kindInfo.validKindCnt != 0)
				{
					num2 = 0;
					int j;
					if (i == 0)
					{
						P_0.Write("         return ");
						P_0.Write(("jjMoveStringLiteralDfa")+(i + 1)+(LexGen.lexStateSuffix)
							+("(")
							);
						for (j = 0; j < num - 1; j++)
						{
							if (i + 1 <= maxLenForActive[j])
							{
								if (num2 != 0)
								{
									P_0.Write(", ");
								}
								else
								{
									num2 = 1;
								}
								P_0.Write(("0x")+(Long.toHexString(kindInfo.validKinds[j]))+("L")
									);
							}
						}
						if (i + 1 <= maxLenForActive[j])
						{
							if (num2 != 0)
							{
								P_0.Write(", ");
							}
							P_0.Write(("0x")+(Long.toHexString(kindInfo.validKinds[j]))+("L")
								);
						}
						P_0.WriteLine(");");
						continue;
					}
					P_0.Write("         return ");
					P_0.Write(("jjMoveStringLiteralDfa")+(i + 1)+(LexGen.lexStateSuffix)
						+("(")
						);
					for (j = 0; j < num - 1; j++)
					{
						if (i + 1 <= maxLenForActive[j] + 1)
						{
							if (num2 != 0)
							{
								P_0.Write(", ");
							}
							else
							{
								num2 = 1;
							}
							if (kindInfo.validKinds[j] != 0)
							{
								P_0.Write(("active")+(j)+(", 0x")
									+(Long.toHexString(kindInfo.validKinds[j]))
									+("L")
									);
							}
							else
							{
								P_0.Write(("active")+(j)+(", 0L")
									);
							}
						}
					}
					if (i + 1 <= maxLenForActive[j] + 1)
					{
						if (num2 != 0)
						{
							P_0.Write(", ");
						}
						if (kindInfo.validKinds[j] != 0)
						{
							P_0.Write(("active")+(j)+(", 0x")
								+(Long.toHexString(kindInfo.validKinds[j]))
								+("L")
								);
						}
						else
						{
							P_0.Write(("active")+(j)+(", 0L")
								);
						}
					}
					P_0.WriteLine(");");
				}
				else if (i == 0 && LexGen.mixed[LexGen.lexStateIndex])
				{
					if (NfaState.generatedStates != 0)
					{
						P_0.WriteLine(("         return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
							+(NfaState.InitStateName())
							+(", 0);")
							);
					}
					else
					{
						P_0.WriteLine("         return 1;");
					}
				}
				else if (i != 0)
				{
					P_0.WriteLine("         break;");
					num3 = 1;
				}
				continue;
				IL_0a09:
				LexGen.AddCharToSkip((char)num5, num6);
				if (Options.getIgnoreCase())
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
			P_0.WriteLine("      default :");
			if (Options.getDebugTokenManager())
			{
				P_0.WriteLine("      debugStream.WriteLine(\"   No string literal matches possible.\");");
			}
			if (NfaState.generatedStates != 0)
			{
				if (i == 0)
				{
					P_0.WriteLine(("         return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
						+(NfaState.InitStateName())
						+(", 0);")
						);
				}
				else
				{
					P_0.WriteLine("         break;");
					num3 = 1;
				}
			}
			else
			{
				P_0.WriteLine(("         return ")+(i + 1)+(";")
					);
			}
			P_0.WriteLine("   }");
			if (i != 0 && num3 != 0)
			{
				if (!LexGen.mixed[LexGen.lexStateIndex] && NfaState.generatedStates != 0)
				{
					P_0.Write(("   return jjStartNfa")+(LexGen.lexStateSuffix)+("(")
						+(i - 1)
						+(", ")
						);
					int l;
					for (l = 0; l < num - 1; l++)
					{
						if (i <= maxLenForActive[l])
						{
							P_0.Write(("active")+(l)+(", ")
								);
						}
						else
						{
							P_0.Write("0L, ");
						}
					}
					if (i <= maxLenForActive[l])
					{
						P_0.WriteLine(("active")+(l)+(");")
							);
					}
					else
					{
						P_0.WriteLine("0L);");
					}
				}
				else if (NfaState.generatedStates != 0)
				{
					P_0.WriteLine(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
						+(NfaState.InitStateName())
						+(", ")
						+(i)
						+(");")
						);
				}
				else
				{
					P_0.WriteLine(("   return ")+(i + 1)+(";")
						);
				}
			}
			P_0.WriteLine("}");
		}
	}

	
	public static void DumpStrLiteralImages(TextWriter pw)
	{
		charCnt = 0;
		pw.WriteLine("");
		pw.WriteLine("/** Token literal values. */");
		pw.WriteLine("public static final String[] jjstrLiteralImages = {");
		if (allImages == null || allImages.Length == 0)
		{
			pw.WriteLine("};");
			return;
		}
		allImages[0] = "";
		int i;
		for (i = 0; i < (nint)allImages.LongLength; i++)
		{
			string @this;
			if ((@this = allImages[i]) != null)
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
					if ((num13 & (num14 << ((64 != -1) ? (num15 % 64) : 0))) == 0 && !LexGen.canReachOnMore[LexGen.lexStates[i]] && ((!Options.getIgnoreCase() && !LexGen.ignoreCase[i]) || (string.Equals(@this, java.lang.String.instancehelper_toLowerCase(@this)) && string.Equals(@this, java.lang.String.instancehelper_toUpperCase(@this)))))
					{
						string str = "\"";
						for (int j = 0; j < @this.Length; j++)
						{
							if (java.lang.String.instancehelper_charAt(@this, j) <= 'ÿ')
							{
								str = (str)+("\\")+(int.toOctalString(java.lang.String.instancehelper_charAt(@this, j)))
									;
								continue;
							}
							string text = Utils.ToHexString(java.lang.String.instancehelper_charAt(@this, j));
							if (java.lang.text.Length == 3)
							{
								text = ("0")+(text);
							}
							str = (str)+("\\u")+(text)
								;
						}
						str = (str)+("\", ");
						if ((charCnt += str.Length) >= 80)
						{
							pw.WriteLine("");
							charCnt = 0;
						}
						pw.Write(str);
						continue;
					}
				}
			}
			goto IL_0157;
			IL_0157:
			allImages[i] = null;
			if ((charCnt += 6) > 80)
			{
				pw.WriteLine("");
				charCnt = 0;
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
			if ((charCnt += 6) > 80)
			{
				pw.WriteLine("");
				charCnt = 0;
			}
			pw.Write("null, ");
		}
		pw.WriteLine("};");
	}

	
	public new static void reInit()
	{
		ReInit();
		charCnt = 0;
		allImages = null;
		boilerPlateDumped = false;
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

	
	internal static void DumpNullStrLiterals(TextWriter P_0)
	{
		P_0.WriteLine("{");
		if (NfaState.generatedStates != 0)
		{
			P_0.WriteLine(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
				+(NfaState.InitStateName())
				+(", 0);")
				);
		}
		else
		{
			P_0.WriteLine("   return 1;");
		}
		P_0.WriteLine("}");
	}

	
	internal static void DumpBoilerPlate(TextWriter P_0)
	{
		P_0.WriteLine(((!Options.getStatic()) ? "" : "static ")+("private int ")+("jjStopAtPos(int pos, int kind)")
			);
		P_0.WriteLine("{");
		P_0.WriteLine("   jjmatchedKind = kind;");
		P_0.WriteLine("   jjmatchedPos = pos;");
		if (Options.getDebugTokenManager())
		{
			P_0.WriteLine("   debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
			P_0.WriteLine("   debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
		}
		P_0.WriteLine("   return pos + 1;");
		P_0.WriteLine("}");
	}

	
	internal static void DumpStartWithStates(TextWriter P_0)
	{
		P_0.WriteLine(((!Options.getStatic()) ? "" : "static ")+("private int ")+("jjStartNfaWithStates")
			+(LexGen.lexStateSuffix)
			+("(int pos, int kind, int state)")
			);
		P_0.WriteLine("{");
		P_0.WriteLine("   jjmatchedKind = kind;");
		P_0.WriteLine("   jjmatchedPos = pos;");
		if (Options.getDebugTokenManager())
		{
			P_0.WriteLine("   debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
			P_0.WriteLine("   debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
		}
		P_0.WriteLine("   try { curChar = input_stream.readChar(); }");
		P_0.WriteLine("   catch(java.io.IOException e) { return pos + 1; }");
		if (Options.getDebugTokenManager())
		{
			P_0.WriteLine(("   debugStream.WriteLine(")+((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ")+("\"Current character : \" + ")
				+("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
				+("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
				);
		}
		P_0.WriteLine(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(state, pos + 1);")
			);
		P_0.WriteLine("}");
	}

	
	internal static string[] ReArrange(Hashtable P_0)
	{
		string[] array = new string[P_0.Count];
		Enumeration enumeration = P_0.keys();
		int num = 0;
		while (enumeration.hasMoreElements())
		{
			int i = 0;
			string text;
			for (int num2 = java.lang.String.instancehelper_charAt(text = (string)enumeration.nextElement(), 0); i < num && java.lang.String.instancehelper_charAt(array[i], 0) < num2; i++)
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

	
	internal static int GetLine(int P_0)
	{
		return LexGen.rexprs[P_0].Line;
	}

	
	internal static int GetColumn(int P_0)
	{
		return LexGen.rexprs[P_0].Column;
	}

	
	internal static string GetLabel(int P_0)
	{
		var regularExpression = LexGen.rexprs[P_0];
		if (regularExpression is RStringLiteral)
		{
			string result = (" \"")+(JavaCCGlobals.AddEscapes(((RStringLiteral)regularExpression).image))+("\"")
				;
			
			return result;
		}
		if (!string.Equals(regularExpression.label, ""))
		{
			string result2 = (" <")+(regularExpression.label)+(">")
				;
			
			return result2;
		}
		string result3 = (" <token of kind ")+(P_0)+(">")
			;
		
		return result3;
	}

	
	private static int GetStateSetForKind(int P_0, int P_1)
	{
		if (LexGen.mixed[LexGen.lexStateIndex] || NfaState.generatedStates == 0)
		{
			return -1;
		}
		Hashtable hashtable = statesForPos[P_0];
		if (hashtable == null)
		{
			return -1;
		}
		Enumeration enumeration = hashtable.keys();
		while (enumeration.hasMoreElements())
		{
			string text = (string)enumeration.nextElement();
			long[] array = (long[])hashtable.get(text);
			text = java.lang.String.instancehelper_substring(text, java.lang.String.instancehelper_indexOf(text, ", ") + 2);
			text = java.lang.String.instancehelper_substring(text, java.lang.String.instancehelper_indexOf(text, ", ") + 2);
			if (!string.Equals(text, "null;") && array != null)
			{
				if ((array[P_1 / 64] & (1L << ((64 != -1) ? (P_1 % 64) : 0))) != 0)
				{
					int result = NfaState.AddStartStateSet(text);
					
					return result;
				}
			}
		}
		return -1;
	}

	
	internal static void DumpNfaStartStatesCode(Hashtable[] P_0, TextWriter P_1)
	{
		if (maxStrKind == 0)
		{
			return;
		}
		int num = maxStrKind / 64 + 1;
		int num2 = 0;
		_ = 0;
		P_1.Write(("private")+((!Options.getStatic()) ? "" : " static")+(" final int jjStopStringLiteralDfa")
			+(LexGen.lexStateSuffix)
			+("(int pos, ")
			);
		int i;
		for (i = 0; i < num - 1; i++)
		{
			P_1.Write(("long active")+(i)+(", ")
				);
		}
		P_1.WriteLine(("long active")+(i)+(")\n{")
			);
		if (Options.getDebugTokenManager())
		{
			P_1.WriteLine("      debugStream.WriteLine(\"   No more string literal token matches are possible.\");");
		}
		P_1.WriteLine("   switch (pos)\n   {");
		for (i = 0; i < maxLen - 1; i++)
		{
			if (P_0[i] == null)
			{
				continue;
			}
			P_1.WriteLine(("      case ")+(i)+(":")
				);
			Enumeration enumeration = P_0[i].keys();
			while (enumeration.hasMoreElements())
			{
				string text = (string)enumeration.nextElement();
				long[] array = (long[])P_0[i].get(text);
				for (int j = 0; j < num; j++)
				{
					if (array[j] != 0)
					{
						if (num2 != 0)
						{
							P_1.Write(" || ");
						}
						else
						{
							P_1.Write("         if (");
						}
						num2 = 1;
						P_1.Write(("(active")+(j)+(" & 0x")
							+(Long.toHexString(array[j]))
							+("L) != 0L")
							);
					}
				}
				if (num2 == 0)
				{
					continue;
				}
				P_1.WriteLine(")");
				int num3;
				string text2 = java.lang.String.instancehelper_substring(text, 0, num3 = java.lang.String.instancehelper_indexOf(text, ", "));
				string @this = java.lang.String.instancehelper_substring(text, num3 + 2);
				int num4 = int.parseInt(java.lang.String.instancehelper_substring(@this, 0, java.lang.String.instancehelper_indexOf(@this, ", ")));
				if (!string.Equals(text2, java.lang.String.valueOf(int.MaxValue)))
				{
					P_1.WriteLine("         {");
				}
				if (!string.Equals(text2, java.lang.String.valueOf(int.MaxValue)))
				{
					if (i == 0)
					{
						P_1.WriteLine(("            jjmatchedKind = ")+(text2)+(";")
							);
						if (LexGen.initMatch[LexGen.lexStateIndex] != 0 && LexGen.initMatch[LexGen.lexStateIndex] != int.MaxValue)
						{
							P_1.WriteLine("            jjmatchedPos = 0;");
						}
					}
					else if (i == num4)
					{
						if (subStringAtPos[i])
						{
							P_1.WriteLine(("            if (jjmatchedPos != ")+(i)+(")")
								);
							P_1.WriteLine("            {");
							P_1.WriteLine(("               jjmatchedKind = ")+(text2)+(";")
								);
							P_1.WriteLine(("               jjmatchedPos = ")+(i)+(";")
								);
							P_1.WriteLine("            }");
						}
						else
						{
							P_1.WriteLine(("            jjmatchedKind = ")+(text2)+(";")
								);
							P_1.WriteLine(("            jjmatchedPos = ")+(i)+(";")
								);
						}
					}
					else
					{
						if (num4 > 0)
						{
							P_1.WriteLine(("            if (jjmatchedPos < ")+(num4)+(")")
								);
						}
						else
						{
							P_1.WriteLine("            if (jjmatchedPos == 0)");
						}
						P_1.WriteLine("            {");
						P_1.WriteLine(("               jjmatchedKind = ")+(text2)+(";")
							);
						P_1.WriteLine(("               jjmatchedPos = ")+(num4)+(";")
							);
						P_1.WriteLine("            }");
					}
				}
				text2 = java.lang.String.instancehelper_substring(text, 0, num3 = java.lang.String.instancehelper_indexOf(text, ", "));
				@this = java.lang.String.instancehelper_substring(text, num3 + 2);
				text = java.lang.String.instancehelper_substring(@this, java.lang.String.instancehelper_indexOf(@this, ", ") + 2);
				if (string.Equals(text, "null;"))
				{
					P_1.WriteLine("            return -1;");
				}
				else
				{
					P_1.WriteLine(("            return ")+(NfaState.AddStartStateSet(text))+(";")
						);
				}
				if (!string.Equals(text2, java.lang.String.valueOf(int.MaxValue)))
				{
					P_1.WriteLine("         }");
				}
				num2 = 0;
			}
			P_1.WriteLine("         return -1;");
		}
		P_1.WriteLine("      default :");
		P_1.WriteLine("         return -1;");
		P_1.WriteLine("   }");
		P_1.WriteLine("}");
		P_1.Write(("private")+((!Options.getStatic()) ? "" : " static")+(" final int jjStartNfa")
			+(LexGen.lexStateSuffix)
			+("(int pos, ")
			);
		for (i = 0; i < num - 1; i++)
		{
			P_1.Write(("long active")+(i)+(", ")
				);
		}
		P_1.WriteLine(("long active")+(i)+(")\n{")
			);
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			if (NfaState.generatedStates != 0)
			{
				P_1.WriteLine(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
					+(NfaState.InitStateName())
					+(", pos + 1);")
					);
			}
			else
			{
				P_1.WriteLine("   return pos + 1;");
			}
			P_1.WriteLine("}");
			return;
		}
		P_1.Write(("   return jjMoveNfa")+(LexGen.lexStateSuffix)+("(")
			+("jjStopStringLiteralDfa")
			+(LexGen.lexStateSuffix)
			+("(pos, ")
			);
		for (i = 0; i < num - 1; i++)
		{
			P_1.Write(("active")+(i)+(", ")
				);
		}
		P_1.Write(("active")+(i)+(")")
			);
		P_1.WriteLine(", pos + 1);");
		P_1.WriteLine("}");
	}

	

	internal static int GetStrKind(string P_0)
	{
		for (int i = 0; i < maxStrKind; i++)
		{
			if (LexGen.lexStates[i] == LexGen.lexStateIndex)
			{
				string text = allImages[i];
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
		if (image.Length == 1)
		{
			RCharacterList rCharacterList = new RCharacterList(image[0]);
			Nfa result = rCharacterList.GenerateNfa(b);
			
			return result;
		}
		NfaState nfaState = new NfaState();
		NfaState nfaState2 = nfaState;
		NfaState nfaState3 = null;
		if (image.Length == 0)
		{
			Nfa result2 = new Nfa(nfaState2, nfaState2);
			
			return result2;
		}
		for (int i = 0; i < image.Length; i++)
		{
			nfaState3 = new NfaState();
			nfaState.charMoves = new char[1];
			nfaState.AddChar(image[i]);
			if (Options.getIgnoreCase() || b)
			{
				nfaState.AddChar(Char.ToLower(image[i]));
				nfaState.AddChar(char.ToUpper(image[i]));
			}
			nfaState.next = nfaState3;
			nfaState = nfaState3;
		}
		Nfa result3 = new Nfa(nfaState2, nfaState3);
		
		return result3;
	}

	
	public override StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		return base.Dump(i, s)+(' ')+(image);
	}

	
	public override string ToString()
	{
		string result = (base)+(" - ")+(image)
			;
		
		return result;
	}

	static RStringLiteral()
	{
		maxStrKind = 0;
		maxLen = 0;
		charCnt = 0;
		charPosKind = new ArrayList();
		maxLenForActive = new int[100];
		startStateCnt = 0;
		boilerPlateDumped = false;
	}
}
