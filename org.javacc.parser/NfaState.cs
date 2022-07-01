using System;
using System.Collections;
using System.IO;
using System.Text;


namespace org.javacc.parser;


public class NfaState 
{
	public static bool unicodeWarningGiven;

	public static int generatedStates;

	internal static int idCnt;

	internal static int lohiByteCnt;

	internal static int dummyStateIndex;

	internal static bool done;

	internal static bool[] mark;

	internal static bool[] stateDone;

	internal static ArrayList allStates;

	internal static ArrayList indexedAllStates;

	internal static ArrayList nonAsciiTableForMethod;

	internal static Hashtable equivStatesTable;

	internal static Hashtable allNextStates;

	internal static Hashtable lohiByteTab;

	internal static Hashtable stateNameForComposite;

	internal static Hashtable compositeStateTable;

	internal static Hashtable stateBlockTable;

	internal static Hashtable stateSetsToFix;

	internal static bool jjCheckNAddStatesUnaryNeeded;

	internal long[] asciiMoves;

	internal char[] charMoves;

	internal char[] rangeMoves;

	internal NfaState next;

	internal NfaState stateForCase;

	internal ArrayList epsilonMoves;

	internal string epsilonMovesString;

	internal NfaState[] epsilonMoveArray;

	internal int id;

	internal int stateName;

	internal int kind;

	internal int lookingFor;

	internal int usefulEpsilonMoves;

	internal int inNextOf;

	private int lexState;

	internal int nonAsciiMethod;

	internal int kindToPrint;

	internal bool dummy;

	internal bool isComposite;

	internal int[] compositeStates;

	internal bool isFinal;

	public ArrayList loByteVec;

	public int[] nonAsciiMoveIndices;

	internal int round;

	internal int onlyChar;

	internal char matchSingleChar;

	private bool closureDone;

	internal static ArrayList allBitVectors;

	internal static int[] tmpIndices;

	internal static string allBits;

	internal static Hashtable tableToDump;

	internal static ArrayList orderedStateSet;

	internal static int lastIndex;

	internal static int[][] kinds;

	internal static int[][][] statesForState;

	
	
	public static void ___003Cclinit_003E()
	{
	}

	
	internal virtual void AddChar(char P_0)
	{
		onlyChar++;
		matchSingleChar = P_0;
		if (P_0 < '\u0080')
		{
			AddASCIIMove(P_0);
			return;
		}
		if (charMoves == null)
		{
			charMoves = new char[10];
		}
		int num = charMoves.Length;
		if (charMoves[num - 1] != 0)
		{
			charMoves = ExpandCharArr(charMoves, 10);
			num += 10;
		}
		int i;
		for (i = 0; i < num && charMoves[i] != 0 && charMoves[i] <= P_0; i++)
		{
		}
		if (!unicodeWarningGiven && P_0 > 'ÿ' && !Options.getJavaUnicodeEscape() && !Options.getUserCharStream())
		{
			unicodeWarningGiven = true;
			JavaCCErrors.Warning(LexGen.curRE, "Non-ASCII characters used in regular expression.\nPlease make sure you use the correct TextReader when you create the parser, one that can handle your character set.");
		}
		int num2 = charMoves[i];
		charMoves[i] = P_0;
		for (i++; i < num; i++)
		{
			if (num2 == 0)
			{
				break;
			}
			int num3 = charMoves[i];
			charMoves[i] = (char)num2;
			num2 = num3;
		}
	}

	
	public static void ReInit()
	{
		generatedStates = 0;
		idCnt = 0;
		dummyStateIndex = -1;
		done = false;
		mark = null;
		stateDone = null;
		allStates.removeAllElements();
		indexedAllStates.removeAllElements();
		equivStatesTable.Clear();
		allNextStates.Clear();
		compositeStateTable.Clear();
		stateBlockTable.Clear();
		stateNameForComposite.Clear();
		stateSetsToFix.Clear();
	}

	
	internal NfaState()
	{
		asciiMoves = new long[2];
		charMoves = null;
		rangeMoves = null;
		next = null;
		epsilonMoves = new ArrayList();
		stateName = -1;
		kind = int.MaxValue;
		usefulEpsilonMoves = 0;
		nonAsciiMethod = -1;
		kindToPrint = int.MaxValue;
		dummy = false;
		isComposite = false;
		compositeStates = null;
		isFinal = false;
		round = 0;
		onlyChar = 0;
		closureDone = false;
		id = idCnt++;
		allStates.Add(this);
		lexState = LexGen.lexStateIndex;
		lookingFor = LexGen.curKind;
	}

	
	internal virtual void AddMove(NfaState P_0)
	{
		if (!epsilonMoves.Contains(P_0))
		{
			InsertInOrder(epsilonMoves, P_0);
		}
	}

	
	public static void ComputeClosures()
	{
		int index = allStates.Count;
		while (index-- > 0)
		{
			NfaState nfaState = (NfaState)allStates[index];
			if (!nfaState.closureDone)
			{
				nfaState.OptimizeEpsilonMoves(true);
			}
		}
		for (index = 0; index < allStates.Count; index++)
		{
			NfaState nfaState = (NfaState)allStates[index];
			if (!nfaState.closureDone)
			{
				nfaState.OptimizeEpsilonMoves(false);
			}
		}
		for (index = 0; index < allStates.Count; index++)
		{
			NfaState nfaState = (NfaState)allStates[index];
			nfaState.epsilonMoveArray = new NfaState[nfaState.epsilonMoves.Count];
			nfaState.epsilonMoves.CopyInto(nfaState.epsilonMoveArray);
		}
	}

	
	internal virtual void GenerateCode()
	{
		if (stateName != -1)
		{
			return;
		}
		if (next != null)
		{
			next.GenerateCode();
			if (next.kind != int.MaxValue)
			{
				kindToPrint = next.kind;
			}
		}
		if (stateName == -1 && HasTransitions())
		{
			NfaState equivalentRunTimeState = GetEquivalentRunTimeState();
			if (equivalentRunTimeState != null)
			{
				stateName = equivalentRunTimeState.stateName;
				dummy = true;
			}
			else
			{
				stateName = generatedStates++;
				indexedAllStates.Add(this);
				GenerateNextStatesCode();
			}
		}
	}

	
	public virtual void GenerateInitMoves(TextWriter pw)
	{
		GetEpsilonMovesString();
		if (epsilonMovesString == null)
		{
			epsilonMovesString = "null;";
		}
		AddStartStateSet(epsilonMovesString);
	}

	
	public static void DumpMoveNfa(TextWriter pw)
	{
		int[] array = null;
		if (kinds == null)
		{
			kinds = new int[LexGen.maxLexStates][];
			statesForState = new int[LexGen.maxLexStates][][];
		}
		ReArrange();
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (nfaState.lexState == LexGen.lexStateIndex && nfaState.HasTransitions() && !nfaState.dummy && nfaState.stateName != -1)
			{
				if (array == null)
				{
					array = new int[generatedStates];
					statesForState[LexGen.lexStateIndex] = new int[java.lang.Math.max(generatedStates, dummyStateIndex + 1)][];
				}
				array[nfaState.stateName] = nfaState.lookingFor;
				statesForState[LexGen.lexStateIndex][nfaState.stateName] = nfaState.compositeStates;
				nfaState.GenerateNonAsciiMoves(pw);
			}
		}
		Enumeration enumeration = stateNameForComposite.keys();
		while (enumeration.hasMoreElements())
		{
			string key = (string)enumeration.nextElement();
			int num = ((int)stateNameForComposite.get(key)).intValue();
			if (num >= generatedStates)
			{
				statesForState[LexGen.lexStateIndex][num] = (int[])allNextStates.get(key);
			}
		}
		if (stateSetsToFix.Count != 0)
		{
			FixStateSets();
		}
		kinds[LexGen.lexStateIndex] = array;
		pw.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private int ").Append("jjMoveNfa")
			.Append(LexGen.lexStateSuffix)
			.Append("(int startState, int curPos)")
			.ToString());
		pw.WriteLine("{");
		if (generatedStates == 0)
		{
			pw.WriteLine("   return curPos;");
			pw.WriteLine("}");
			return;
		}
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			pw.WriteLine("   int strKind = jjmatchedKind;");
			pw.WriteLine("   int strPos = jjmatchedPos;");
			pw.WriteLine("   int seenUpto;");
			pw.WriteLine("   input_stream.backup(seenUpto = curPos + 1);");
			pw.WriteLine("   try { curChar = input_stream.readChar(); }");
			pw.WriteLine("   catch(java.io.IOException e) { throw new System.Exception(\"Internal Error\"); }");
			pw.WriteLine("   curPos = 0;");
		}
		pw.WriteLine("   //int[] nextStates; // not used");
		pw.WriteLine("   int startsAt = 0;");
		pw.WriteLine(new StringBuilder().Append("   jjnewStateCnt = ").Append(generatedStates).Append(";")
			.ToString());
		pw.WriteLine("   int i = 1;");
		pw.WriteLine("   jjstateSet[0] = startState;");
		if (Options.getDebugTokenManager())
		{
			pw.WriteLine("      debugStream.WriteLine(\"   Starting NFA to match one of : \" + jjKindsForStateVector(curLexState, jjstateSet, 0, 1));");
		}
		if (Options.getDebugTokenManager())
		{
			pw.WriteLine(new StringBuilder().Append("      debugStream.WriteLine(").Append((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ").Append("\"Current character : \" + ")
				.Append("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
				.Append("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
				.ToString());
		}
		pw.WriteLine("   //int j; // not used");
		pw.WriteLine(new StringBuilder().Append("   int kind = 0x").Append(Utils.ToString(int.MaxValue,16)).Append(";")
			.ToString());
		pw.WriteLine("   for (;;)");
		pw.WriteLine("   {");
		pw.WriteLine(new StringBuilder().Append("      if (++jjround == 0x").Append(Utils.ToString(int.MaxValue,16)).Append(")")
			.ToString());
		pw.WriteLine("         ReInitRounds();");
		pw.WriteLine("      if (curChar < 64)");
		pw.WriteLine("      {");
		DumpAsciiMoves(pw, 0);
		pw.WriteLine("      }");
		pw.WriteLine("      else if (curChar < 128)");
		pw.WriteLine("      {");
		DumpAsciiMoves(pw, 1);
		pw.WriteLine("      }");
		pw.WriteLine("      else");
		pw.WriteLine("      {");
		DumpCharAndRangeMoves(pw);
		pw.WriteLine("      }");
		pw.WriteLine(new StringBuilder().Append("      if (kind != 0x").Append(Utils.ToString(int.MaxValue,16)).Append(")")
			.ToString());
		pw.WriteLine("      {");
		pw.WriteLine("         jjmatchedKind = kind;");
		pw.WriteLine("         jjmatchedPos = curPos;");
		pw.WriteLine(new StringBuilder().Append("         kind = 0x").Append(Utils.ToString(int.MaxValue,16)).Append(";")
			.ToString());
		pw.WriteLine("      }");
		pw.WriteLine("      ++curPos;");
		if (Options.getDebugTokenManager())
		{
			pw.WriteLine(new StringBuilder().Append("      if (jjmatchedKind != 0 && jjmatchedKind != 0x").Append(Utils.ToString(int.MaxValue,16)).Append(")")
				.ToString());
			pw.WriteLine("         debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
		}
		pw.WriteLine(new StringBuilder().Append("      if ((i = jjnewStateCnt) == (startsAt = ").Append(generatedStates).Append(" - (jjnewStateCnt = startsAt)))")
			.ToString());
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			pw.WriteLine("         break;");
		}
		else
		{
			pw.WriteLine("         return curPos;");
		}
		if (Options.getDebugTokenManager())
		{
			pw.WriteLine("      debugStream.WriteLine(\"   Possible kinds of longer matches : \" + jjKindsForStateVector(curLexState, jjstateSet, startsAt, i));");
		}
		pw.WriteLine("      try { curChar = input_stream.readChar(); }");
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			pw.WriteLine("      catch(java.io.IOException e) { break; }");
		}
		else
		{
			pw.WriteLine("      catch(java.io.IOException e) { return curPos; }");
		}
		if (Options.getDebugTokenManager())
		{
			pw.WriteLine(new StringBuilder().Append("      debugStream.WriteLine(").Append((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ").Append("\"Current character : \" + ")
				.Append("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
				.Append("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
				.ToString());
		}
		pw.WriteLine("   }");
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			pw.WriteLine("   if (jjmatchedPos > strPos)");
			pw.WriteLine("      return curPos;");
			pw.WriteLine("");
			pw.WriteLine("   int toRet = Math.max(curPos, seenUpto);");
			pw.WriteLine("");
			pw.WriteLine("   if (curPos < toRet)");
			pw.WriteLine("      for (i = toRet - Math.min(curPos, seenUpto); i-- > 0; )");
			pw.WriteLine("         try { curChar = input_stream.readChar(); }");
			pw.WriteLine("         catch(java.io.IOException e) { throw new System.Exception(\"Internal Error : Please send a bug report.\"); }");
			pw.WriteLine("");
			pw.WriteLine("   if (jjmatchedPos < strPos)");
			pw.WriteLine("   {");
			pw.WriteLine("      jjmatchedKind = strKind;");
			pw.WriteLine("      jjmatchedPos = strPos;");
			pw.WriteLine("   }");
			pw.WriteLine("   else if (jjmatchedPos == strPos && jjmatchedKind > strKind)");
			pw.WriteLine("      jjmatchedKind = strKind;");
			pw.WriteLine("");
			pw.WriteLine("   return toRet;");
		}
		pw.WriteLine("}");
		allStates.removeAllElements();
	}

	
	public static void DumpStateSets(TextWriter pw)
	{
		int num = 0;
		pw.Write("static final int[] jjnextStates = {");
		for (int i = 0; i < orderedStateSet.Count; i++)
		{
			int[] array = (int[])orderedStateSet[i];
			for (int j = 0; j < (nint)array.LongLength; j++)
			{
				int num2 = num;
				num++;
				if (16 == -1 || num2 % 16 == 0)
				{
					pw.Write("\n   ");
				}
				pw.Write(new StringBuilder().Append(array[j]).Append(", ").ToString());
			}
		}
		pw.WriteLine("\n};");
	}

	
	public static void DumpNonAsciiMoveMethods(TextWriter pw)
	{
		if ((Options.getJavaUnicodeEscape() || unicodeWarningGiven) && nonAsciiTableForMethod.Count > 0)
		{
			for (int i = 0; i < nonAsciiTableForMethod.Count; i++)
			{
				NfaState nfaState = (NfaState)nonAsciiTableForMethod[i];
				nfaState.DumpNonAsciiMoveMethod(pw);
			}
		}
	}

	
	public static void DumpStatesForKind(TextWriter pw)
	{
		DumpStatesForState(pw);
		int num = 0;
		_ = 0;
		pw.Write("protected static final int[][] kindForState = ");
		if (kinds == null)
		{
			pw.WriteLine("null;");
			return;
		}
		pw.WriteLine("{");
		for (int i = 0; i < (nint)kinds.LongLength; i++)
		{
			if (num != 0)
			{
				pw.WriteLine(", ");
			}
			num = 1;
			if (kinds[i] == null)
			{
				pw.WriteLine("null");
				continue;
			}
			int num2 = 0;
			pw.Write("{ ");
			for (int j = 0; j < (nint)kinds[i].LongLength; j++)
			{
				int num3 = num2;
				num2++;
				if (num3 > 0)
				{
					pw.Write(", ");
				}
				int num4 = num2;
				if (15 == -1 || num4 % 15 == 0)
				{
					pw.Write("\n  ");
				}
				pw.Write(kinds[i][j]);
			}
			pw.Write("}");
		}
		pw.WriteLine("\n};");
	}

	
	internal static void PrintBoilerPlate(TextWriter P_0)
	{
		P_0.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private void ").Append("jjCheckNAdd(int state)")
			.ToString());
		P_0.WriteLine("{");
		P_0.WriteLine("   if (jjrounds[state] != jjround)");
		P_0.WriteLine("   {");
		P_0.WriteLine("      jjstateSet[jjnewStateCnt++] = state;");
		P_0.WriteLine("      jjrounds[state] = jjround;");
		P_0.WriteLine("   }");
		P_0.WriteLine("}");
		P_0.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private void ").Append("jjAddStates(int start, int end)")
			.ToString());
		P_0.WriteLine("{");
		P_0.WriteLine("   do {");
		P_0.WriteLine("      jjstateSet[jjnewStateCnt++] = jjnextStates[start];");
		P_0.WriteLine("   } while (start++ != end);");
		P_0.WriteLine("}");
		P_0.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private void ").Append("jjCheckNAddTwoStates(int state1, int state2)")
			.ToString());
		P_0.WriteLine("{");
		P_0.WriteLine("   jjCheckNAdd(state1);");
		P_0.WriteLine("   jjCheckNAdd(state2);");
		P_0.WriteLine("}");
		P_0.WriteLine("");
		P_0.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private void ").Append("jjCheckNAddStates(int start, int end)")
			.ToString());
		P_0.WriteLine("{");
		P_0.WriteLine("   do {");
		P_0.WriteLine("      jjCheckNAdd(jjnextStates[start]);");
		P_0.WriteLine("   } while (start++ != end);");
		P_0.WriteLine("}");
		P_0.WriteLine("");
		if (jjCheckNAddStatesUnaryNeeded)
		{
			P_0.WriteLine(new StringBuilder().Append((!Options.getStatic()) ? "" : "static ").Append("private void ").Append("jjCheckNAddStates(int start)")
				.ToString());
			P_0.WriteLine("{");
			P_0.WriteLine("   jjCheckNAdd(jjnextStates[start]);");
			P_0.WriteLine("   jjCheckNAdd(jjnextStates[start + 1]);");
			P_0.WriteLine("}");
			P_0.WriteLine("");
		}
	}

	public virtual bool HasTransitions()
	{
		return (asciiMoves[0] != 0 || asciiMoves[1] != 0 || (charMoves != null && charMoves[0] != 0) || (rangeMoves != null && rangeMoves[0] != 0)) ? true : false;
	}

	
	public static void reInit()
	{
		unicodeWarningGiven = false;
		generatedStates = 0;
		idCnt = 0;
		lohiByteCnt = 0;
		dummyStateIndex = -1;
		done = false;
		mark = null;
		stateDone = null;
		allStates = new ArrayList();
		indexedAllStates = new ArrayList();
		nonAsciiTableForMethod = new ArrayList();
		equivStatesTable = new Hashtable();
		allNextStates = new Hashtable();
		lohiByteTab = new Hashtable();
		stateNameForComposite = new Hashtable();
		compositeStateTable = new Hashtable();
		stateBlockTable = new Hashtable();
		stateSetsToFix = new Hashtable();
		allBitVectors = new ArrayList();
		tmpIndices = new int[512];
		allBits = "{\n   0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL\n};";
		tableToDump = new Hashtable();
		orderedStateSet = new ArrayList();
		lastIndex = 0;
		jjCheckNAddStatesUnaryNeeded = false;
		kinds = null;
		statesForState = null;
	}

	
	internal virtual void MergeMoves(NfaState P_0)
	{
		if (asciiMoves == P_0.asciiMoves)
		{
			JavaCCErrors.Semantic_Error("Bug in JavaCC : Please send a report along with the input that caused this. Thank you.");
			
			throw new System.Exception();
		}
		asciiMoves[0] = asciiMoves[0] | P_0.asciiMoves[0];
		asciiMoves[1] = asciiMoves[1] | P_0.asciiMoves[1];
		if (P_0.charMoves != null)
		{
			if (charMoves == null)
			{
				charMoves = P_0.charMoves;
			}
			else
			{
				char[] dest = new char[(nint)charMoves.LongLength + (nint)P_0.charMoves.LongLength];
				Array.Copy(charMoves, 0, dest, 0, charMoves.Length);
				charMoves = dest;
				for (int i = 0; i < (nint)P_0.charMoves.LongLength; i++)
				{
					AddChar(P_0.charMoves[i]);
				}
			}
		}
		if (P_0.rangeMoves != null)
		{
			if (rangeMoves == null)
			{
				rangeMoves = P_0.rangeMoves;
			}
			else
			{
				char[] dest = new char[(nint)rangeMoves.LongLength + (nint)P_0.rangeMoves.LongLength];
				Array.Copy(rangeMoves, 0, dest, 0, rangeMoves.Length);
				rangeMoves = dest;
				for (int i = 0; i < (nint)P_0.rangeMoves.LongLength; i += 2)
				{
					AddRange(P_0.rangeMoves[i], P_0.rangeMoves[i + 1]);
				}
			}
		}
		if (P_0.kind < kind)
		{
			kind = P_0.kind;
		}
		if (P_0.kindToPrint < kindToPrint)
		{
			kindToPrint = P_0.kindToPrint;
		}
		isFinal |= P_0.isFinal;
	}

	
	internal static void InsertInOrder(ArrayList P_0, NfaState P_1)
	{
		int i;
		for (i = 0; i < P_0.Count && ((NfaState)P_0[i]).id <= P_1.id; i++)
		{
			if (((NfaState)P_0[i]).id == P_1.id)
			{
				return;
			}
		}
		P_0.insertElementAt(P_1, i);
	}


	private void AddASCIIMove(char P_0)
	{
		long[] array = asciiMoves;
		int num = (int)P_0 / 64;
		long[] array2 = array;
		array2[num] |= 1L << ((64 != -1) ? ((int)P_0 % 64) : 0);
	}

	private static char[] ExpandCharArr(char[] P_0, int P_1)
	{
		char[] array = new char[(nint)P_0.LongLength + P_1];
		Array.Copy(P_0, 0, array, 0, P_0.Length);
		return array;
	}

	
	private void EpsilonClosure()
	{
		_ = 0;
		if (closureDone || mark[id])
		{
			return;
		}
		mark[id] = true;
		for (int i = 0; i < epsilonMoves.Count; i++)
		{
			((NfaState)epsilonMoves[i]).EpsilonClosure();
		}
		Enumeration enumeration = epsilonMoves.elements();
		while (enumeration.hasMoreElements())
		{
			NfaState nfaState = (NfaState)enumeration.nextElement();
			for (int i = 0; i < nfaState.epsilonMoves.Count; i++)
			{
				NfaState nfaState2 = (NfaState)nfaState.epsilonMoves[i];
				if (nfaState2.UsefulState() && !epsilonMoves.Contains(nfaState2))
				{
					InsertInOrder(epsilonMoves, nfaState2);
					done = false;
				}
			}
			if (kind > nfaState.kind)
			{
				kind = nfaState.kind;
			}
		}
		if (HasTransitions() && !epsilonMoves.Contains(this))
		{
			InsertInOrder(epsilonMoves, this);
		}
	}

	
	private bool UsefulState()
	{
		return (isFinal || HasTransitions()) ? true : false;
	}

	
	internal virtual void AddRange(char P_0, char P_1)
	{
		int num = P_0;
		onlyChar = 2;
		if (num < 128)
		{
			if (P_1 < '\u0080')
			{
				while (num <= P_1)
				{
					AddASCIIMove((char)num);
					num = (ushort)(num + 1);
				}
				return;
			}
			while (num < 128)
			{
				AddASCIIMove((char)num);
				num = (ushort)(num + 1);
			}
		}
		if (!unicodeWarningGiven && (num > 255 || P_1 > 'ÿ') && !Options.getJavaUnicodeEscape() && !Options.getUserCharStream())
		{
			unicodeWarningGiven = true;
			JavaCCErrors.Warning(LexGen.curRE, "Non-ASCII characters used in regular expression.\nPlease make sure you use the correct TextReader when you create the parser, one that can handle your character set.");
		}
		if (rangeMoves == null)
		{
			rangeMoves = new char[20];
		}
		int num2 = rangeMoves.Length;
		if (rangeMoves[num2 - 1] != 0)
		{
			rangeMoves = ExpandCharArr(rangeMoves, 20);
			num2 += 20;
		}
		int i;
		for (i = 0; i < num2 && rangeMoves[i] != 0 && rangeMoves[i] <= num && (rangeMoves[i] != num || rangeMoves[i + 1] <= P_1); i += 2)
		{
		}
		int num3 = rangeMoves[i];
		int num4 = rangeMoves[i + 1];
		rangeMoves[i] = (char)num;
		rangeMoves[i + 1] = P_1;
		for (i += 2; i < num2; i += 2)
		{
			if (num3 == 0)
			{
				break;
			}
			int num5 = rangeMoves[i];
			int num6 = rangeMoves[i + 1];
			rangeMoves[i] = (char)num3;
			rangeMoves[i + 1] = (char)num4;
			num3 = num5;
			num4 = num6;
		}
	}

	
	internal virtual NfaState CreateClone()
	{
		NfaState nfaState = new NfaState();
		nfaState.isFinal = isFinal;
		nfaState.kind = kind;
		nfaState.lookingFor = lookingFor;
		nfaState.lexState = lexState;
		nfaState.inNextOf = inNextOf;
		nfaState.MergeMoves(this);
		return nfaState;
	}

	private static bool EqualCharArr(char[] P_0, char[] P_1)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		if (P_0 != null && P_1 != null && (nint)P_0.LongLength == (nint)P_1.LongLength)
		{
			int num = P_0.Length;
			while (num-- > 0)
			{
				if (P_0[num] != P_1[num])
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	
	private NfaState GetEquivalentRunTimeState()
	{
		int index = allStates.Count;
		while (index-- > 0)
		{
			NfaState nfaState = (NfaState)allStates[index];
			if (this == nfaState || nfaState.stateName == -1 || kindToPrint != nfaState.kindToPrint || asciiMoves[0] != nfaState.asciiMoves[0] || asciiMoves[1] != nfaState.asciiMoves[1] || !EqualCharArr(charMoves, nfaState.charMoves) || !EqualCharArr(rangeMoves, nfaState.rangeMoves))
			{
				continue;
			}
			if (next == nfaState.next)
			{
				return nfaState;
			}
			if (next == null || nfaState.next == null || next.epsilonMoves.Count != nfaState.next.epsilonMoves.Count)
			{
				continue;
			}
			int num = 0;
			while (true)
			{
				if (num < next.epsilonMoves.Count)
				{
					if (next.epsilonMoves[num] != nfaState.next.epsilonMoves[num])
					{
						break;
					}
					num++;
					continue;
				}
				return nfaState;
			}
		}
		return null;
	}

	
	internal virtual void GenerateNextStatesCode()
	{
		if (next.usefulEpsilonMoves > 0)
		{
			next.GetEpsilonMovesString();
		}
	}

	
	internal virtual void OptimizeEpsilonMoves(bool P_0)
	{
		done = false;
		int num;
		while (!done)
		{
			if (mark == null || (nint)mark.LongLength < allStates.Count)
			{
				mark = new bool[allStates.Count];
			}
			num = allStates.Count;
			while (num-- > 0)
			{
				mark[num] = false;
			}
			done = true;
			EpsilonClosure();
		}
		num = allStates.Count;
		while (num-- > 0)
		{
			((NfaState)allStates[num]).closureDone = mark[((NfaState)allStates[num]).id];
		}
		int num2 = 1;
		NfaState nfaState = null;
		ArrayList vector = null;
		while (num2 != 0)
		{
			num2 = 0;
			num = 0;
			while (P_0 && num < epsilonMoves.Count)
			{
				NfaState nfaState2;
				if ((nfaState2 = (NfaState)epsilonMoves[num]).HasTransitions())
				{
					for (int i = num + 1; i < epsilonMoves.Count; i++)
					{
						NfaState nfaState3;
						if ((nfaState3 = (NfaState)epsilonMoves[i]).HasTransitions() && nfaState2.asciiMoves[0] == nfaState3.asciiMoves[0] && nfaState2.asciiMoves[1] == nfaState3.asciiMoves[1] && EqualCharArr(nfaState2.charMoves, nfaState3.charMoves) && EqualCharArr(nfaState2.rangeMoves, nfaState3.rangeMoves))
						{
							if (vector == null)
							{
								vector = new ArrayList();
								vector.Add(nfaState2);
							}
							InsertInOrder(vector, nfaState3);
							epsilonMoves.removeElementAt(i--);
						}
					}
				}
				if (vector != null)
				{
					num2 = 1;
					string text = "";
					for (int j = 0; j < vector.Count; j++)
					{
						text = new StringBuilder().Append(text).Append(java.lang.String.valueOf(((NfaState)vector[j]).id)).Append(", ")
							.ToString();
					}
					if ((nfaState = (NfaState)equivStatesTable.get(text)) == null)
					{
						nfaState = CreateEquivState(vector);
						equivStatesTable.Add(text, nfaState);
					}
					epsilonMoves.removeElementAt(num--);
					epsilonMoves.Add(nfaState);
					vector = null;
					nfaState = null;
				}
				num++;
			}
			for (num = 0; num < epsilonMoves.Count; num++)
			{
				NfaState nfaState2 = (NfaState)epsilonMoves[num];
				for (int i = num + 1; i < epsilonMoves.Count; i++)
				{
					NfaState nfaState3 = (NfaState)epsilonMoves[i];
					if (nfaState2.next == nfaState3.next)
					{
						if (nfaState == null)
						{
							nfaState = nfaState2.CreateClone();
							nfaState.next = nfaState2.next;
							num2 = 1;
						}
						nfaState.MergeMoves(nfaState3);
						epsilonMoves.removeElementAt(i--);
					}
				}
				if (nfaState != null)
				{
					epsilonMoves.removeElementAt(num--);
					epsilonMoves.Add(nfaState);
					nfaState = null;
				}
			}
		}
		if (epsilonMoves.Count <= 0)
		{
			return;
		}
		for (num = 0; num < epsilonMoves.Count; num++)
		{
			if (((NfaState)epsilonMoves[num]).HasTransitions())
			{
				usefulEpsilonMoves++;
			}
			else
			{
				epsilonMoves.removeElementAt(num--);
			}
		}
	}

	
	internal virtual NfaState CreateEquivState(ArrayList P_0)
	{
		NfaState nfaState = ((NfaState)P_0[0]).CreateClone();
		nfaState.next = new NfaState();
		InsertInOrder(nfaState.next.epsilonMoves, ((NfaState)P_0[0]).next);
		for (int i = 1; i < P_0.Count; i++)
		{
			NfaState nfaState2 = (NfaState)P_0[i];
			if (nfaState2.kind < nfaState.kind)
			{
				nfaState.kind = nfaState2.kind;
			}
			nfaState.isFinal |= nfaState2.isFinal;
			InsertInOrder(nfaState.next.epsilonMoves, nfaState2.next);
		}
		return nfaState;
	}

	
	internal virtual string GetEpsilonMovesString()
	{
		int[] array = new int[usefulEpsilonMoves];
		int num = 0;
		if (epsilonMovesString != null)
		{
			return epsilonMovesString;
		}
		if (usefulEpsilonMoves > 0)
		{
			epsilonMovesString = "{ ";
			StringBuilder stringBuilder;
			for (int i = 0; i < epsilonMoves.Count; i++)
			{
				NfaState nfaState;
				if (!(nfaState = (NfaState)epsilonMoves[i]).HasTransitions())
				{
					continue;
				}
				if (nfaState.stateName == -1)
				{
					nfaState.GenerateCode();
				}
				((NfaState)indexedAllStates.elementAt(nfaState.stateName)).inNextOf++;
				array[num] = nfaState.stateName;
				stringBuilder = new StringBuilder();
				epsilonMovesString = stringBuilder.Append(epsilonMovesString).Append(nfaState.stateName).Append(", ")
					.ToString();
				int num2 = num;
				num++;
				if (num2 > 0)
				{
					int num3 = num;
					if (16 == -1 || num3 % 16 == 0)
					{
						stringBuilder = new StringBuilder();
						epsilonMovesString = stringBuilder.Append(epsilonMovesString).Append("\n").ToString();
					}
				}
			}
			stringBuilder = new StringBuilder();
			epsilonMovesString = stringBuilder.Append(epsilonMovesString).Append("};").ToString();
		}
		usefulEpsilonMoves = num;
		if (epsilonMovesString != null && allNextStates.get(epsilonMovesString) == null)
		{
			int[] array2 = new int[usefulEpsilonMoves];
			Array.Copy(array, 0, array2, 0, num);
			allNextStates.Add(epsilonMovesString, array2);
		}
		return epsilonMovesString;
	}

	internal bool CanMoveUsingChar(char P_0)
	{
		if (onlyChar == 1)
		{
			return P_0 == matchSingleChar;
		}
		if (P_0 < '\u0080')
		{
			return (asciiMoves[(int)P_0 / 64] & (1L << ((64 != -1) ? ((int)P_0 % 64) : 0))) != 0;
		}
		if (charMoves != null && charMoves[0] != 0)
		{
			for (int i = 0; i < (nint)charMoves.LongLength; i++)
			{
				if (P_0 == charMoves[i])
				{
					return true;
				}
				if (P_0 < charMoves[i] || charMoves[i] == '\0')
				{
					break;
				}
			}
		}
		if (rangeMoves != null && rangeMoves[0] != 0)
		{
			for (int i = 0; i < (nint)rangeMoves.LongLength; i += 2)
			{
				if (P_0 >= rangeMoves[i] && P_0 <= rangeMoves[i + 1])
				{
					return true;
				}
				if (P_0 < rangeMoves[i] || rangeMoves[i] == '\0')
				{
					break;
				}
			}
		}
		return false;
	}

	
	public virtual int MoveFrom(char ch, ArrayList v)
	{
		if (CanMoveUsingChar(ch))
		{
			int index = next.epsilonMoves.Count;
			while (index-- > 0)
			{
				InsertInOrder(v, (NfaState)next.epsilonMoves[index]);
			}
			return kindToPrint;
		}
		return int.MaxValue;
	}

	
	internal static bool AllBitsSet(string P_0)
	{
		bool result = string.Equals(P_0, allBits);
		
		return result;
	}

	
	private void UpdateDuplicateNonAsciiMoves()
	{
		for (int i = 0; i < nonAsciiTableForMethod.Count; i++)
		{
			NfaState nfaState = (NfaState)nonAsciiTableForMethod[i];
			if (EqualLoByteVectors(loByteVec, nfaState.loByteVec) && EqualNonAsciiMoveIndices(nonAsciiMoveIndices, nfaState.nonAsciiMoveIndices))
			{
				nonAsciiMethod = i;
				return;
			}
		}
		nonAsciiMethod = nonAsciiTableForMethod.Count;
		nonAsciiTableForMethod.Add(this);
	}

	
	private static bool EqualLoByteVectors(ArrayList P_0, ArrayList P_1)
	{
		if (P_0 == null || P_1 == null)
		{
			return false;
		}
		if (P_0 == P_1)
		{
			return true;
		}
		if (P_0.Count != P_1.Count)
		{
			return false;
		}
		for (int i = 0; i < P_0.Count; i++)
		{
			if (((int)P_0[i]).intValue() != ((int)P_1[i]).intValue())
			{
				return false;
			}
		}
		return true;
	}

	private static bool EqualNonAsciiMoveIndices(int[] P_0, int[] P_1)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		if (P_0 == null || P_1 == null)
		{
			return false;
		}
		if ((nint)P_0.LongLength != (nint)P_1.LongLength)
		{
			return false;
		}
		for (int i = 0; i < (nint)P_0.LongLength; i++)
		{
			if (P_0[i] != P_1[i])
			{
				return false;
			}
		}
		return true;
	}

	
	private static int AddCompositeStateSet(string P_0, bool P_1)
	{
		int integer;
		if ((integer = (int)stateNameForComposite.get(P_0)) != null)
		{
			int result = integer.intValue();
			
			return result;
		}
		int i = 0;
		int[] array = (int[])allNextStates.get(P_0);
		if (!P_1)
		{
			stateBlockTable.Add(P_0, P_0);
		}
		if (array == null)
		{
			string message = new StringBuilder().Append("JavaCC Bug: Please send mail to sankar@cs.stanford.edu; nameSet null for : ").Append(P_0).ToString();
			
			throw new System.Exception(message);
		}
		if ((nint)array.LongLength == 1)
		{
			;
			integer = new int(array[0]);
			stateNameForComposite.Add(P_0, integer);
			return array[0];
		}
		for (int j = 0; j < (nint)array.LongLength; j++)
		{
			if (array[j] != -1)
			{
				NfaState nfaState = (NfaState)indexedAllStates.elementAt(array[j]);
				nfaState.isComposite = true;
				nfaState.compositeStates = array;
			}
		}
		for (; i < (nint)array.LongLength; i++)
		{
			if (!P_1)
			{
				break;
			}
			if (((NfaState)indexedAllStates[array[i]]).inNextOf <= 1)
			{
				break;
			}
		}
		Enumeration enumeration = compositeStateTable.keys();
		while (enumeration.hasMoreElements())
		{
			string text = (string)enumeration.nextElement();
			if (!string.Equals(text, P_0) && Intersect(P_0, text))
			{
				for (int[] array2 = (int[])compositeStateTable.get(text); i < (nint)array.LongLength && ((P_1 && ((NfaState)indexedAllStates[array[i]]).inNextOf > 1) || ElemOccurs(array[i], array2) >= 0); i++)
				{
				}
			}
		}
		int num = ((i < (nint)array.LongLength) ? array[i] : ((dummyStateIndex != -1) ? (++dummyStateIndex) : (dummyStateIndex = generatedStates)));
		integer = new int(num);
		stateNameForComposite.Add(P_0, integer);
		compositeStateTable.Add(P_0, array);
		return num;
	}

	
	private static bool Intersect(string P_0, string P_1)
	{
		if (P_0 == null || P_1 == null)
		{
			return false;
		}
		int[] array = (int[])allNextStates.get(P_0);
		int[] array2 = (int[])allNextStates.get(P_1);
		if (array == null || array2 == null)
		{
			return false;
		}
		if (array == array2)
		{
			return true;
		}
		int num = array.Length;
		while (num-- > 0)
		{
			int num2 = array2.Length;
			while (num2-- > 0)
			{
				if (array[num] == array2[num2])
				{
					return true;
				}
			}
		}
		return false;
	}

	private static int ElemOccurs(int P_0, int[] P_1)
	{
		int num = P_1.Length;
		while (num-- > 0)
		{
			if (P_1[num] == P_0)
			{
				return num;
			}
		}
		return -1;
	}

	
	private static int StateNameForComposite(string P_0)
	{
		int result = ((int)stateNameForComposite.get(P_0)).intValue();
		
		return result;
	}

	
	internal static int AddStartStateSet(string P_0)
	{
		int result = AddCompositeStateSet(P_0, true);
		
		return result;
	}

	
	internal static string GetStateSetString(int[] P_0)
	{
		string str = "{ ";
		int num = 0;
		while (num < (nint)P_0.LongLength)
		{
			str = new StringBuilder().Append(str).Append(P_0[num]).Append(", ")
				.ToString();
			int num2 = num;
			num++;
			if (num2 > 0)
			{
				int num3 = num;
				if (16 == -1 || num3 % 16 == 0)
				{
					str = new StringBuilder().Append(str).Append("\n").ToString();
				}
			}
		}
		str = new StringBuilder().Append(str).Append("};").ToString();
		allNextStates.Add(str, P_0);
		return str;
	}


	private void FixNextStates(int[] P_0)
	{
		next.usefulEpsilonMoves = P_0.Length;
	}

	internal static int NumberOfBitsSet(long P_0)
	{
		int num = 0;
		for (int i = 0; i < 63; i++)
		{
			if (((P_0 >> i) & 1) != 0)
			{
				num++;
			}
		}
		return num;
	}

	
	private void DumpAsciiMoveForCompositeState(TextWriter P_0, int P_1, bool P_2)
	{
		int num = (selfLoop() ? 1 : 0);
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.asciiMoves[P_1] != 0 && num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
			{
				num = 1;
				break;
			}
		}
		string str = "";
		if (asciiMoves[P_1] != -1)
		{
			int num2 = OnlyOneBitSet(asciiMoves[P_1]);
			if (num2 != -1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  ").Append((!P_2) ? "" : "else ").Append("if (curChar == ")
					.Append(64 * P_1 + num2)
					.Append(")")
					.ToString());
			}
			else
			{
				P_0.WriteLine(new StringBuilder().Append("                  ").Append((!P_2) ? "" : "else ").Append("if ((0x")
					.Append(Long.toHexString(asciiMoves[P_1]))
					.Append("L & l) != 0L)")
					.ToString());
			}
			str = "   ";
		}
		if (kindToPrint != int.MaxValue)
		{
			if (asciiMoves[P_1] != -1)
			{
				P_0.WriteLine("                  {");
			}
			P_0.WriteLine(new StringBuilder().Append(str).Append("                  if (kind > ").Append(kindToPrint)
				.Append(")")
				.ToString());
			P_0.WriteLine(new StringBuilder().Append(str).Append("                     kind = ").Append(kindToPrint)
				.Append(";")
				.ToString());
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			int[] array = (int[])allNextStates.get(next.epsilonMovesString);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAdd(").Append(i2)
						.Append(");")
						.ToString());
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjstateSet[jjnewStateCnt++] = ").Append(i2)
						.Append(";")
						.ToString());
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAddTwoStates(").Append(array[0])
					.Append(", ")
					.Append(array[1])
					.Append(");")
					.ToString());
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num3 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write(new StringBuilder().Append(str).Append("                  jjCheckNAddStates(").Append(stateSetIndicesForUse[0])
						.ToString());
					if (num3 != 0)
					{
						P_0.Write(new StringBuilder().Append(", ").Append(stateSetIndicesForUse[1]).ToString());
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjAddStates(").Append(stateSetIndicesForUse[0])
						.Append(", ")
						.Append(stateSetIndicesForUse[1])
						.Append(");")
						.ToString());
				}
			}
		}
		if (asciiMoves[P_1] != -1 && kindToPrint != int.MaxValue)
		{
			P_0.WriteLine("                  }");
		}
	}

	

	private void DumpNonAsciiMoveForCompositeState(TextWriter P_0)
	{
		int num = (selfLoop() ? 1 : 0);
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.nonAsciiMethod != -1 && num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
			{
				num = 1;
				break;
			}
		}
		if (!Options.getJavaUnicodeEscape() && !unicodeWarningGiven)
		{
			if (loByteVec != null && loByteVec.Count > 1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  if ((jjbitVec").Append(((int)loByteVec.elementAt(1)).intValue()).Append("[i2")
					.Append("] & l2) != 0L)")
					.ToString());
			}
		}
		else
		{
			P_0.WriteLine(new StringBuilder().Append("                  if (jjCanMove_").Append(nonAsciiMethod).Append("(hiByte, i1, i2, l1, l2))")
				.ToString());
		}
		if (kindToPrint != int.MaxValue)
		{
			P_0.WriteLine("                  {");
			P_0.WriteLine(new StringBuilder().Append("                     if (kind > ").Append(kindToPrint).Append(")")
				.ToString());
			P_0.WriteLine(new StringBuilder().Append("                        kind = ").Append(kindToPrint).Append(";")
				.ToString());
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			int[] array = (int[])allNextStates.get(next.epsilonMovesString);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine(new StringBuilder().Append("                     jjCheckNAdd(").Append(i2).Append(");")
						.ToString());
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append("                     jjstateSet[jjnewStateCnt++] = ").Append(i2).Append(";")
						.ToString());
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine(new StringBuilder().Append("                     jjCheckNAddTwoStates(").Append(array[0]).Append(", ")
					.Append(array[1])
					.Append(");")
					.ToString());
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num2 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write(new StringBuilder().Append("                     jjCheckNAddStates(").Append(stateSetIndicesForUse[0]).ToString());
					if (num2 != 0)
					{
						P_0.Write(new StringBuilder().Append(", ").Append(stateSetIndicesForUse[1]).ToString());
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append("                     jjAddStates(").Append(stateSetIndicesForUse[0]).Append(", ")
						.Append(stateSetIndicesForUse[1])
						.Append(");")
						.ToString());
				}
			}
		}
		if (kindToPrint != int.MaxValue)
		{
			P_0.WriteLine("                  }");
		}
	}

	
	private string PrintNoBreak(TextWriter P_0, int P_1, bool[] P_2)
	{
		if (inNextOf != 1)
		{
			
			throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu");
		}
		P_2[stateName] = true;
		if (P_1 >= 0)
		{
			if (asciiMoves[P_1] != 0)
			{
				P_0.WriteLine(new StringBuilder().Append("               case ").Append(stateName).Append(":")
					.ToString());
				DumpAsciiMoveForCompositeState(P_0, P_1, false);
				return "";
			}
		}
		else if (nonAsciiMethod != -1)
		{
			P_0.WriteLine(new StringBuilder().Append("               case ").Append(stateName).Append(":")
				.ToString());
			DumpNonAsciiMoveForCompositeState(P_0);
			return "";
		}
		string result = new StringBuilder().Append("               case ").Append(stateName).Append(":\n")
			.ToString();
		
		return result;
	}

	
	private void DumpAsciiMove(TextWriter P_0, int P_1, bool[] P_2)
	{
		int num = ((selfLoop() && isComposite) ? 1 : 0);
		int num2 = 1;
		int i;
		for (i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.asciiMoves[P_1] != 0)
			{
				if (num2 != 0 && (asciiMoves[P_1] & nfaState.asciiMoves[P_1]) != 0)
				{
					num2 = 0;
				}
				if (num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
				{
					num = 1;
				}
				if (!P_2[nfaState.stateName] && !nfaState.isComposite && asciiMoves[P_1] == nfaState.asciiMoves[P_1] && kindToPrint == nfaState.kindToPrint && ((object)next.epsilonMovesString == nfaState.next.epsilonMovesString || (next.epsilonMovesString != null && nfaState.next.epsilonMovesString != null && string.Equals(next.epsilonMovesString, nfaState.next.epsilonMovesString))))
				{
					P_2[nfaState.stateName] = true;
					P_0.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
						.ToString());
				}
			}
		}
		i = OnlyOneBitSet(asciiMoves[P_1]);
		string str;
		if (asciiMoves[P_1] != -1 && (next == null || next.usefulEpsilonMoves == 0) && kindToPrint != int.MaxValue)
		{
			str = "";
			if (num2 == 0)
			{
				str = new StringBuilder().Append(" && kind > ").Append(kindToPrint).ToString();
			}
			if (i != -1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  if (curChar == ").Append(64 * P_1 + i).Append(str)
					.Append(")")
					.ToString());
			}
			else
			{
				P_0.WriteLine(new StringBuilder().Append("                  if ((0x").Append(Long.toHexString(asciiMoves[P_1])).Append("L & l) != 0L")
					.Append(str)
					.Append(")")
					.ToString());
			}
			P_0.WriteLine(new StringBuilder().Append("                     kind = ").Append(kindToPrint).Append(";")
				.ToString());
			if (num2 != 0)
			{
				P_0.WriteLine("                  break;");
			}
			else
			{
				P_0.WriteLine("                  break;");
			}
			return;
		}
		str = "";
		if (kindToPrint != int.MaxValue)
		{
			if (i != -1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  if (curChar != ").Append(64 * P_1 + i).Append(")")
					.ToString());
				P_0.WriteLine("                     break;");
			}
			else if (asciiMoves[P_1] != -1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  if ((0x").Append(Long.toHexString(asciiMoves[P_1])).Append("L & l) == 0L)")
					.ToString());
				P_0.WriteLine("                     break;");
			}
			if (num2 != 0)
			{
				P_0.WriteLine(new StringBuilder().Append("                  kind = ").Append(kindToPrint).Append(";")
					.ToString());
			}
			else
			{
				P_0.WriteLine(new StringBuilder().Append("                  if (kind > ").Append(kindToPrint).Append(")")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append("                     kind = ").Append(kindToPrint).Append(";")
					.ToString());
			}
		}
		else if (i != -1)
		{
			P_0.WriteLine(new StringBuilder().Append("                  if (curChar == ").Append(64 * P_1 + i).Append(")")
				.ToString());
			str = "   ";
		}
		else if (asciiMoves[P_1] != -1)
		{
			P_0.WriteLine(new StringBuilder().Append("                  if ((0x").Append(Long.toHexString(asciiMoves[P_1])).Append("L & l) != 0L)")
				.ToString());
			str = "   ";
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			int[] array = (int[])allNextStates.get(next.epsilonMovesString);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAdd(").Append(i2)
						.Append(");")
						.ToString());
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjstateSet[jjnewStateCnt++] = ").Append(i2)
						.Append(";")
						.ToString());
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAddTwoStates(").Append(array[0])
					.Append(", ")
					.Append(array[1])
					.Append(");")
					.ToString());
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num3 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write(new StringBuilder().Append(str).Append("                  jjCheckNAddStates(").Append(stateSetIndicesForUse[0])
						.ToString());
					if (num3 != 0)
					{
						P_0.Write(new StringBuilder().Append(", ").Append(stateSetIndicesForUse[1]).ToString());
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjAddStates(").Append(stateSetIndicesForUse[0])
						.Append(", ")
						.Append(stateSetIndicesForUse[1])
						.Append(");")
						.ToString());
				}
			}
		}
		if (num2 != 0)
		{
			P_0.WriteLine("                  break;");
		}
		else
		{
			P_0.WriteLine("                  break;");
		}
	}

	
	private static ArrayList PartitionStatesSetForAscii(int[] P_0, int P_1)
	{
		int[] array = new int[(nint)P_0.LongLength];
		ArrayList vector = new ArrayList();
		ArrayList vector2 = new ArrayList();
		vector.setSize(P_0.Length);
		int num = 0;
		for (int i = 0; i < (nint)P_0.LongLength; i++)
		{
			NfaState nfaState = (NfaState)allStates.elementAt(P_0[i]);
			if (nfaState.asciiMoves[P_1] != 0)
			{
				int num2 = NumberOfBitsSet(nfaState.asciiMoves[P_1]);
				int j;
				for (j = 0; j < i && array[j] > num2; j++)
				{
				}
				for (int k = i; k > j; k += -1)
				{
					array[k] = array[k - 1];
				}
				array[j] = num2;
				vector.insertElementAt(nfaState, j);
				num++;
			}
		}
		vector.setSize(num);
		while (vector.Count > 0)
		{
			NfaState nfaState = (NfaState)vector[0];
			vector.removeElement(nfaState);
			long num3 = nfaState.asciiMoves[P_1];
			ArrayList vector3 = new ArrayList();
			vector3.Add(nfaState);
			for (int k = 0; k < vector.Count; k++)
			{
				NfaState nfaState2 = (NfaState)vector[k];
				if ((nfaState2.asciiMoves[P_1] & num3) == 0)
				{
					num3 |= nfaState2.asciiMoves[P_1];
					vector3.Add(nfaState2);
					vector.removeElementAt(k--);
				}
			}
			vector2.Add(vector3);
		}
		return vector2;
	}

	
	private bool selfLoop()
	{
		if (next == null || next.epsilonMovesString == null)
		{
			return false;
		}
		int[] array = (int[])allNextStates.get(next.epsilonMovesString);
		return ElemOccurs(stateName, array) >= 0;
	}

	internal static int OnlyOneBitSet(long P_0)
	{
		int num = -1;
		for (int i = 0; i < 64; i++)
		{
			if (((P_0 >> i) & 1) != 0)
			{
				if (num >= 0)
				{
					return -1;
				}
				num = i;
			}
		}
		return num;
	}

	
	private static int[] GetStateSetIndicesForUse(string P_0)
	{
		int[] array = (int[])allNextStates.get(P_0);
		int[] array2;
		if ((array2 = (int[])tableToDump.get(P_0)) == null)
		{
			array2 = new int[2]
			{
				lastIndex,
				(int)(lastIndex + (nint)array.LongLength - 1)
			};
			lastIndex = (int)(lastIndex + (nint)array.LongLength);
			tableToDump.Add(P_0, array2);
			orderedStateSet.Add(array);
		}
		return array2;
	}

	
	private static void DumpHeadForCase(TextWriter P_0, int P_1)
	{
		switch (P_1)
		{
		case 0:
			P_0.WriteLine("         long l = 1L << curChar;");
			break;
		case 1:
			P_0.WriteLine("         long l = 1L << (curChar & 077);");
			break;
		default:
			if (Options.getJavaUnicodeEscape() || unicodeWarningGiven)
			{
				P_0.WriteLine("         int hiByte = (int)(curChar >> 8);");
				P_0.WriteLine("         int i1 = hiByte >> 6;");
				P_0.WriteLine("         long l1 = 1L << (hiByte & 077);");
			}
			P_0.WriteLine("         int i2 = (curChar & 0xff) >> 6;");
			P_0.WriteLine("         long l2 = 1L << (curChar & 077);");
			break;
		}
		P_0.WriteLine("         do");
		P_0.WriteLine("         {");
		P_0.WriteLine("            switch(jjstateSet[--i])");
		P_0.WriteLine("            {");
	}

	
	private static void DumpCompositeStatesAsciiMoves(TextWriter P_0, string P_1, int P_2, bool[] P_3)
	{
		int[] array = (int[])allNextStates.get(P_1);
		if ((nint)array.LongLength == 1 || P_3[StateNameForComposite(P_1)])
		{
			return;
		}
		NfaState nfaState = null;
		int num = 0;
		NfaState nfaState2 = null;
		string text = "";
		int num2 = ((stateBlockTable.get(P_1) != null) ? 1 : 0);
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			NfaState nfaState3 = (NfaState)allStates[array[i]];
			if (nfaState3.asciiMoves[P_2] != 0)
			{
				int num3 = num;
				num++;
				if (num3 == 1)
				{
					break;
				}
				nfaState = nfaState3;
			}
			else
			{
				P_3[nfaState3.stateName] = true;
			}
			if (nfaState3.stateForCase != null)
			{
				if (nfaState2 != null)
				{
					
					throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu : ");
				}
				nfaState2 = nfaState3.stateForCase;
			}
		}
		if (nfaState2 != null)
		{
			text = nfaState2.PrintNoBreak(P_0, P_2, P_3);
		}
		switch (num)
		{
		case 0:
			if (nfaState2 != null && string.Equals(text, ""))
			{
				P_0.WriteLine("                  break;");
			}
			return;
		case 1:
			if (!string.Equals(text, ""))
			{
				P_0.Write(text);
			}
			P_0.WriteLine(new StringBuilder().Append("               case ").Append(StateNameForComposite(P_1)).Append(":")
				.ToString());
			if (!P_3[nfaState.stateName] && num2 == 0 && nfaState.inNextOf > 1)
			{
				P_0.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
					.ToString());
			}
			P_3[nfaState.stateName] = true;
			nfaState.DumpAsciiMove(P_0, P_2, P_3);
			return;
		}
		ArrayList vector = PartitionStatesSetForAscii(array, P_2);
		if (!string.Equals(text, ""))
		{
			P_0.Write(text);
		}
		int num4 = StateNameForComposite(P_1);
		P_0.WriteLine(new StringBuilder().Append("               case ").Append(num4).Append(":")
			.ToString());
		if (num4 < generatedStates)
		{
			P_3[num4] = true;
		}
		for (int i = 0; i < vector.Count; i++)
		{
			ArrayList vector2 = (ArrayList)vector[i];
			for (int j = 0; j < vector2.Count; j++)
			{
				NfaState nfaState3 = (NfaState)vector2[j];
				if (num2 != 0)
				{
					P_3[nfaState3.stateName] = true;
				}
				nfaState3.DumpAsciiMoveForCompositeState(P_0, P_2, (j != 0) ? true : false);
			}
		}
		if (num2 != 0)
		{
			P_0.WriteLine("                  break;");
		}
		else
		{
			P_0.WriteLine("                  break;");
		}
	}

	

	private void DumpNonAsciiMove(TextWriter P_0, bool[] P_1)
	{
		int num = ((selfLoop() && isComposite) ? 1 : 0);
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.nonAsciiMethod != -1)
			{
				if (num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
				{
					num = 1;
				}
				if (!P_1[nfaState.stateName] && !nfaState.isComposite && nonAsciiMethod == nfaState.nonAsciiMethod && kindToPrint == nfaState.kindToPrint && ((object)next.epsilonMovesString == nfaState.next.epsilonMovesString || (next.epsilonMovesString != null && nfaState.next.epsilonMovesString != null && string.Equals(next.epsilonMovesString, nfaState.next.epsilonMovesString))))
				{
					P_1[nfaState.stateName] = true;
					P_0.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
						.ToString());
				}
			}
		}
		string str;
		if (next == null || next.usefulEpsilonMoves <= 0)
		{
			str = new StringBuilder().Append(" && kind > ").Append(kindToPrint).ToString();
			if (!Options.getJavaUnicodeEscape() && !unicodeWarningGiven)
			{
				if (loByteVec != null && loByteVec.Count > 1)
				{
					P_0.WriteLine(new StringBuilder().Append("                  if ((jjbitVec").Append(((int)loByteVec.elementAt(1)).intValue()).Append("[i2")
						.Append("] & l2) != 0L")
						.Append(str)
						.Append(")")
						.ToString());
				}
			}
			else
			{
				P_0.WriteLine(new StringBuilder().Append("                  if (jjCanMove_").Append(nonAsciiMethod).Append("(hiByte, i1, i2, l1, l2)")
					.Append(str)
					.Append(")")
					.ToString());
			}
			P_0.WriteLine(new StringBuilder().Append("                     kind = ").Append(kindToPrint).Append(";")
				.ToString());
			P_0.WriteLine("                  break;");
			return;
		}
		str = "   ";
		if (kindToPrint != int.MaxValue)
		{
			if (!Options.getJavaUnicodeEscape() && !unicodeWarningGiven)
			{
				if (loByteVec != null && loByteVec.Count > 1)
				{
					P_0.WriteLine(new StringBuilder().Append("                  if ((jjbitVec").Append(((int)loByteVec.elementAt(1)).intValue()).Append("[i2")
						.Append("] & l2) == 0L)")
						.ToString());
					P_0.WriteLine("                     break;");
				}
			}
			else
			{
				P_0.WriteLine(new StringBuilder().Append("                  if (!jjCanMove_").Append(nonAsciiMethod).Append("(hiByte, i1, i2, l1, l2))")
					.ToString());
				P_0.WriteLine("                     break;");
			}
			P_0.WriteLine(new StringBuilder().Append("                  if (kind > ").Append(kindToPrint).Append(")")
				.ToString());
			P_0.WriteLine(new StringBuilder().Append("                     kind = ").Append(kindToPrint).Append(";")
				.ToString());
			str = "";
		}
		else if (!Options.getJavaUnicodeEscape() && !unicodeWarningGiven)
		{
			if (loByteVec != null && loByteVec.Count > 1)
			{
				P_0.WriteLine(new StringBuilder().Append("                  if ((jjbitVec").Append(((int)loByteVec.elementAt(1)).intValue()).Append("[i2")
					.Append("] & l2) != 0L)")
					.ToString());
			}
		}
		else
		{
			P_0.WriteLine(new StringBuilder().Append("                  if (jjCanMove_").Append(nonAsciiMethod).Append("(hiByte, i1, i2, l1, l2))")
				.ToString());
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			int[] array = (int[])allNextStates.get(next.epsilonMovesString);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAdd(").Append(i2)
						.Append(");")
						.ToString());
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjstateSet[jjnewStateCnt++] = ").Append(i2)
						.Append(";")
						.ToString());
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjCheckNAddTwoStates(").Append(array[0])
					.Append(", ")
					.Append(array[1])
					.Append(");")
					.ToString());
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num2 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write(new StringBuilder().Append(str).Append("                  jjCheckNAddStates(").Append(stateSetIndicesForUse[0])
						.ToString());
					if (num2 != 0)
					{
						P_0.Write(new StringBuilder().Append(", ").Append(stateSetIndicesForUse[1]).ToString());
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine(new StringBuilder().Append(str).Append("                  jjAddStates(").Append(stateSetIndicesForUse[0])
						.Append(", ")
						.Append(stateSetIndicesForUse[1])
						.Append(");")
						.ToString());
				}
			}
		}
		P_0.WriteLine("                  break;");
	}

	
	private static void DumpCompositeStatesNonAsciiMoves(TextWriter P_0, string P_1, bool[] P_2)
	{
		int[] array = (int[])allNextStates.get(P_1);
		if ((nint)array.LongLength == 1 || P_2[StateNameForComposite(P_1)])
		{
			return;
		}
		NfaState nfaState = null;
		int num = 0;
		NfaState nfaState2 = null;
		string text = "";
		int num2 = ((stateBlockTable.get(P_1) != null) ? 1 : 0);
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			NfaState nfaState3 = (NfaState)allStates[array[i]];
			if (nfaState3.nonAsciiMethod != -1)
			{
				int num3 = num;
				num++;
				if (num3 == 1)
				{
					break;
				}
				nfaState = nfaState3;
			}
			else
			{
				P_2[nfaState3.stateName] = true;
			}
			if (nfaState3.stateForCase != null)
			{
				if (nfaState2 != null)
				{
					
					throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu : ");
				}
				nfaState2 = nfaState3.stateForCase;
			}
		}
		if (nfaState2 != null)
		{
			text = nfaState2.PrintNoBreak(P_0, -1, P_2);
		}
		switch (num)
		{
		case 0:
			if (nfaState2 != null && string.Equals(text, ""))
			{
				P_0.WriteLine("                  break;");
			}
			return;
		case 1:
			if (!string.Equals(text, ""))
			{
				P_0.Write(text);
			}
			P_0.WriteLine(new StringBuilder().Append("               case ").Append(StateNameForComposite(P_1)).Append(":")
				.ToString());
			if (!P_2[nfaState.stateName] && num2 == 0 && nfaState.inNextOf > 1)
			{
				P_0.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
					.ToString());
			}
			P_2[nfaState.stateName] = true;
			nfaState.DumpNonAsciiMove(P_0, P_2);
			return;
		}
		
		if (!string.Equals(text, ""))
		{
			P_0.Write(text);
		}
		int num4 = StateNameForComposite(P_1);
		P_0.WriteLine(new StringBuilder().Append("               case ").Append(num4).Append(":")
			.ToString());
		if (num4 < generatedStates)
		{
			P_2[num4] = true;
		}
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			NfaState nfaState3 = (NfaState)allStates[array[i]];
			if (nfaState3.nonAsciiMethod != -1)
			{
				if (num2 != 0)
				{
					P_2[nfaState3.stateName] = true;
				}
				nfaState3.DumpNonAsciiMoveForCompositeState(P_0);
			}
		}
		if (num2 != 0)
		{
			P_0.WriteLine("                  break;");
		}
		else
		{
			P_0.WriteLine("                  break;");
		}
	}

	
	internal virtual void DumpNonAsciiMoveMethod(TextWriter P_0)
	{
		P_0.WriteLine(new StringBuilder().Append("private static final boolean jjCanMove_").Append(nonAsciiMethod).Append("(int hiByte, int i1, int i2, long l1, long l2)")
			.ToString());
		P_0.WriteLine("{");
		P_0.WriteLine("   switch(hiByte)");
		P_0.WriteLine("   {");
		if (loByteVec != null && loByteVec.Count > 0)
		{
			for (int i = 0; i < loByteVec.Count; i += 2)
			{
				P_0.WriteLine(new StringBuilder().Append("      case ").Append(((int)loByteVec[i]).intValue()).Append(":")
					.ToString());
				if (!AllBitsSet((string)allBitVectors.elementAt(((int)loByteVec.elementAt(i + 1)).intValue())))
				{
					P_0.WriteLine(new StringBuilder().Append("         return ((jjbitVec").Append(((int)loByteVec.elementAt(i + 1)).intValue()).Append("[i2")
						.Append("] & l2) != 0L);")
						.ToString());
				}
				else
				{
					P_0.WriteLine("            return true;");
				}
			}
		}
		P_0.WriteLine("      default : ");
		if (nonAsciiMoveIndices != null)
		{
			IntPtr intPtr = (nint)nonAsciiMoveIndices.LongLength;
			int i = (int)(nint)intPtr;
			if ((nint)intPtr > 0)
			{
				do
				{
					if (!AllBitsSet((string)allBitVectors[(nonAsciiMoveIndices[i - 2])]))
					{
						P_0.WriteLine(new StringBuilder().Append("         if ((jjbitVec").Append(nonAsciiMoveIndices[i - 2]).Append("[i1] & l1) != 0L)")
							.ToString());
					}
					if (!AllBitsSet((string)allBitVectors.elementAt(nonAsciiMoveIndices[i - 1])))
					{
						P_0.WriteLine(new StringBuilder().Append("            if ((jjbitVec").Append(nonAsciiMoveIndices[i - 1]).Append("[i2] & l2) == 0L)")
							.ToString());
						P_0.WriteLine("               return false;");
						P_0.WriteLine("            else");
					}
					P_0.WriteLine("            return true;");
					i += -2;
				}
				while (i > 0);
			}
		}
		P_0.WriteLine("         return false;");
		P_0.WriteLine("   }");
		P_0.WriteLine("}");
	}

	
	private static void ReArrange()
	{
		ArrayList vector = allStates;
		allStates = new ArrayList();
		allStates.setSize(generatedStates);
		for (int i = 0; i < vector.Count; i++)
		{
			NfaState nfaState = (NfaState)vector[i];
			if (nfaState.stateName != -1 && !nfaState.dummy)
			{
				allStates.setElementAt(nfaState, nfaState.stateName);
			}
		}
	}

	
	internal virtual void GenerateNonAsciiMoves(TextWriter P_0)
	{
		_ = 0;
		_ = 0;
		int num = 0;
		int[] array = new int[2];
		int num2 = (array[1] = 4);
		num2 = (array[0] = 256);
		long[][] array2 = (long[][])ByteCodeHelper.multianewarray(typeof(long[][]).TypeHandle, array);
		if ((charMoves == null || charMoves[0] == '\0') && (rangeMoves == null || rangeMoves[0] == '\0'))
		{
			return;
		}
		if (charMoves != null)
		{
			for (int i = 0; i < (nint)charMoves.LongLength && charMoves[i] != 0; i++)
			{
				int num3 = (ushort)((int)charMoves[i] >> 8);
				long[] obj = array2[num3];
				num2 = (charMoves[i] & 0xFF) / 64;
				long[] array3 = obj;
				long[] array4 = array3;
				int num4 = num2;
				long num5 = array3[num2];
				long num6 = 1L;
				int num7 = charMoves[i] & 0xFF;
				array4[num4] = num5 | (num6 << ((64 != -1) ? (num7 % 64) : 0));
			}
		}
		if (rangeMoves != null)
		{
			for (int i = 0; i < (nint)rangeMoves.LongLength && rangeMoves[i] != 0; i += 2)
			{
				int num8 = (ushort)(rangeMoves[i + 1] & 0xFF);
				int num3 = (ushort)((int)rangeMoves[i] >> 8);
				if (num3 == (ushort)((int)rangeMoves[i + 1] >> 8))
				{
					for (int num9 = (ushort)(rangeMoves[i] & 0xFF); num9 <= num8; num9 = (ushort)(num9 + 1))
					{
						long[] obj2 = array2[num3];
						num2 = num9 / 64;
						long[] array3 = obj2;
						long[] array5 = array3;
						int num10 = num2;
						long num11 = array3[num2];
						long num12 = 1L;
						int num13 = num9;
						array5[num10] = num11 | (num12 << ((64 != -1) ? (num13 % 64) : 0));
					}
					continue;
				}
				for (int num9 = (ushort)(rangeMoves[i] & 0xFF); num9 <= 255; num9 = (ushort)(num9 + 1))
				{
					long[] obj3 = array2[num3];
					num2 = num9 / 64;
					long[] array3 = obj3;
					long[] array6 = array3;
					int num14 = num2;
					long num15 = array3[num2];
					long num16 = 1L;
					int num17 = num9;
					array6[num14] = num15 | (num16 << ((64 != -1) ? (num17 % 64) : 0));
				}
				while (true)
				{
					num3 = (ushort)(num3 + 1);
					if (num3 >= (ushort)((int)rangeMoves[i + 1] >> 8))
					{
						break;
					}
					long[] obj4 = array2[num3];
					num2 = 0;
					long[] array3 = obj4;
					array3[num2] |= -1L;
					long[] obj5 = array2[num3];
					num2 = 1;
					array3 = obj5;
					array3[num2] |= -1L;
					long[] obj6 = array2[num3];
					num2 = 2;
					array3 = obj6;
					array3[num2] |= -1L;
					long[] obj7 = array2[num3];
					num2 = 3;
					array3 = obj7;
					array3[num2] |= -1L;
				}
				for (int num9 = 0; num9 <= num8; num9 = (ushort)(num9 + 1))
				{
					long[] obj8 = array2[num3];
					num2 = num9 / 64;
					long[] array3 = obj8;
					long[] array7 = array3;
					int num18 = num2;
					long num19 = array3[num2];
					long num20 = 1L;
					int num21 = num9;
					array7[num18] = num19 | (num20 << ((64 != -1) ? (num21 % 64) : 0));
				}
			}
		}
		long[] array8 = null;
		bool[] array9 = new bool[256];
		for (int i = 0; i <= 255; i++)
		{
			if (array9[i])
			{
				continue;
			}
			int num22 = i;
			num2 = ((array2[i][0] == 0 && array2[i][1] == 0 && array2[i][2] == 0 && array2[i][3] == 0) ? 1 : 0);
			int num23 = num22;
			bool[] array10 = array9;
			int num24 = num2;
			array10[num23] = (byte)num2 != 0;
			if (num24 != 0)
			{
				continue;
			}
			for (int j = i + 1; j < 256; j++)
			{
				if (!array9[j] && array2[i][0] == array2[j][0] && array2[i][1] == array2[j][1] && array2[i][2] == array2[j][2] && array2[i][3] == array2[j][3])
				{
					array9[j] = true;
					long[] array3;
					if (array8 == null)
					{
						array9[i] = true;
						array8 = new long[4];
						long[] array11 = array8;
						num2 = i / 64;
						array3 = array11;
						long[] array12 = array3;
						int num25 = num2;
						long num26 = array3[num2];
						long num27 = 1L;
						int num28 = i;
						array12[num25] = num26 | (num27 << ((64 != -1) ? (num28 % 64) : 0));
					}
					long[] array13 = array8;
					num2 = j / 64;
					array3 = array13;
					long[] array14 = array3;
					int num29 = num2;
					long num30 = array3[num2];
					long num31 = 1L;
					int num32 = j;
					array14[num29] = num30 | (num31 << ((64 != -1) ? (num32 % 64) : 0));
				}
			}
			if (array8 == null)
			{
				continue;
			}
			string text = new StringBuilder().Append("{\n   0x").Append(Long.toHexString(array8[0])).Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array8[1]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array8[2]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array8[3]))
				.Append("L\n};")
				.ToString();
			int integer;
			if ((integer = (int)lohiByteTab.get(text)) == null)
			{
				allBitVectors.Add(text);
				if (!AllBitsSet(text))
				{
					P_0.WriteLine(new StringBuilder().Append("static final long[] jjbitVec").Append(lohiByteCnt).Append(" = ")
						.Append(text)
						.ToString());
				}
				Hashtable hashtable = lohiByteTab;
				string key = text;
				;
				hashtable.Add(key, integer = new int(lohiByteCnt++));
			}
			int[] array15 = tmpIndices;
			int num33 = num;
			num++;
			array15[num33] = integer.intValue();
			text = new StringBuilder().Append("{\n   0x").Append(Long.toHexString(array2[i][0])).Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][1]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][2]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][3]))
				.Append("L\n};")
				.ToString();
			if ((integer = (int)lohiByteTab.get(text)) == null)
			{
				allBitVectors.Add(text);
				if (!AllBitsSet(text))
				{
					P_0.WriteLine(new StringBuilder().Append("static final long[] jjbitVec").Append(lohiByteCnt).Append(" = ")
						.Append(text)
						.ToString());
				}
				Hashtable hashtable2 = lohiByteTab;
				string key2 = text;
				;
				hashtable2.Add(key2, integer = new int(lohiByteCnt++));
			}
			int[] array16 = tmpIndices;
			int num34 = num;
			num++;
			array16[num34] = integer.intValue();
			array8 = null;
		}
		nonAsciiMoveIndices = new int[num];
		Array.Copy(tmpIndices, 0, nonAsciiMoveIndices, 0, num);
		for (int i = 0; i < 256; i++)
		{
			if (array9[i])
			{
				array2[i] = null;
				continue;
			}
			string text2 = new StringBuilder().Append("{\n   0x").Append(Long.toHexString(array2[i][0])).Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][1]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][2]))
				.Append("L, ")
				.Append("0x")
				.Append(Long.toHexString(array2[i][3]))
				.Append("L\n};")
				.ToString();
			int obj9;
			if ((obj9 = (int)lohiByteTab.get(text2)) == null)
			{
				allBitVectors.Add(text2);
				if (!AllBitsSet(text2))
				{
					P_0.WriteLine(new StringBuilder().Append("static final long[] jjbitVec").Append(lohiByteCnt).Append(" = ")
						.Append(text2)
						.ToString());
				}
				Hashtable hashtable3 = lohiByteTab;
				;
				hashtable3.Add(text2, obj9 = new int(lohiByteCnt++));
			}
			if (loByteVec == null)
			{
				loByteVec = new ArrayList();
			}
			loByteVec.Add(new int(i));
			loByteVec.Add(obj9);
		}
		UpdateDuplicateNonAsciiMoves();
	}

	
	private static void FixStateSets()
	{
		Hashtable hashtable = new Hashtable();
		Enumeration enumeration = stateSetsToFix.keys();
		int[] array = new int[generatedStates];
		while (enumeration.hasMoreElements())
		{
			string key;
			int[] array2 = (int[])stateSetsToFix.get(key = (string)enumeration.nextElement());
			int num = 0;
			for (int i = 0; i < (nint)array2.LongLength; i++)
			{
				if (array2[i] != -1)
				{
					int num2 = num;
					num++;
					array[num2] = array2[i];
				}
			}
			int[] array3 = new int[num];
			Array.Copy(array, 0, array3, 0, num);
			hashtable.Add(key, array3);
			allNextStates.Add(key, array3);
		}
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			int[] array2;
			if (nfaState.next != null && nfaState.next.usefulEpsilonMoves != 0 && (array2 = (int[])hashtable.get(nfaState.next.epsilonMovesString)) != null)
			{
				nfaState.FixNextStates(array2);
			}
		}
	}

	
	private static void DumpAsciiMoves(TextWriter P_0, int P_1)
	{
		bool[] array = new bool[java.lang.Math.max(generatedStates, dummyStateIndex + 1)];
		Enumeration enumeration = compositeStateTable.keys();
		DumpHeadForCase(P_0, P_1);
		while (enumeration.hasMoreElements())
		{
			DumpCompositeStatesAsciiMoves(P_0, (string)enumeration.nextElement(), P_1, array);
		}
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (array[nfaState.stateName] || nfaState.lexState != LexGen.lexStateIndex || !nfaState.HasTransitions() || nfaState.dummy || nfaState.stateName == -1)
			{
				continue;
			}
			string text = "";
			if (nfaState.stateForCase != null)
			{
				if (nfaState.inNextOf == 1 || array[nfaState.stateForCase.stateName])
				{
					continue;
				}
				text = nfaState.stateForCase.PrintNoBreak(P_0, P_1, array);
				if (nfaState.asciiMoves[P_1] == 0)
				{
					if (string.Equals(text, ""))
					{
						P_0.WriteLine("                  break;");
					}
					continue;
				}
			}
			if (nfaState.asciiMoves[P_1] != 0)
			{
				if (!string.Equals(text, ""))
				{
					P_0.Write(text);
				}
				array[nfaState.stateName] = true;
				P_0.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
					.ToString());
				nfaState.DumpAsciiMove(P_0, P_1, array);
			}
		}
		P_0.WriteLine("               default : break;");
		P_0.WriteLine("            }");
		P_0.WriteLine("         } while(i != startsAt);");
	}

	
	public static void DumpCharAndRangeMoves(TextWriter pw)
	{
		bool[] array = new bool[java.lang.Math.max(generatedStates, dummyStateIndex + 1)];
		Enumeration enumeration = compositeStateTable.keys();
		DumpHeadForCase(pw, -1);
		while (enumeration.hasMoreElements())
		{
			DumpCompositeStatesNonAsciiMoves(pw, (string)enumeration.nextElement(), array);
		}
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = (NfaState)allStates[i];
			if (nfaState.stateName == -1 || array[nfaState.stateName] || nfaState.lexState != LexGen.lexStateIndex || !nfaState.HasTransitions() || nfaState.dummy)
			{
				continue;
			}
			string text = "";
			if (nfaState.stateForCase != null)
			{
				if (nfaState.inNextOf == 1 || array[nfaState.stateForCase.stateName])
				{
					continue;
				}
				text = nfaState.stateForCase.PrintNoBreak(pw, -1, array);
				if (nfaState.nonAsciiMethod == -1)
				{
					if (string.Equals(text, ""))
					{
						pw.WriteLine("                  break;");
					}
					continue;
				}
			}
			if (nfaState.nonAsciiMethod != -1)
			{
				if (!string.Equals(text, ""))
				{
					pw.Write(text);
				}
				array[nfaState.stateName] = true;
				pw.WriteLine(new StringBuilder().Append("               case ").Append(nfaState.stateName).Append(":")
					.ToString());
				nfaState.DumpNonAsciiMove(pw, array);
			}
		}
		pw.WriteLine("               default : break;");
		pw.WriteLine("            }");
		pw.WriteLine("         } while(i != startsAt);");
	}

	
	public static void DumpStatesForState(TextWriter pw)
	{
		pw.Write("protected static final int[][][] statesForState = ");
		if (statesForState == null)
		{
			pw.WriteLine("null;");
			return;
		}
		pw.WriteLine("{");
		for (int i = 0; i < (nint)statesForState.LongLength; i++)
		{
			if (statesForState[i] == null)
			{
				pw.WriteLine(" null, ");
				continue;
			}
			pw.WriteLine(" {");
			for (int j = 0; j < (nint)statesForState[i].LongLength; j++)
			{
				int[] array = statesForState[i][j];
				if (array == null)
				{
					pw.WriteLine(new StringBuilder().Append("   { ").Append(j).Append(" }, ")
						.ToString());
					continue;
				}
				pw.Write("   { ");
				for (int k = 0; k < (nint)array.LongLength; k++)
				{
					pw.Write(new StringBuilder().Append(array[k]).Append(", ").ToString());
				}
				pw.WriteLine("},");
			}
			pw.WriteLine(" },");
		}
		pw.WriteLine("\n};");
	}

	
	public static bool CanStartNfaUsingAscii(char ch)
	{
		if (ch >= '\u0080')
		{
			
			throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu");
		}
		string text = LexGen.initialState.GetEpsilonMovesString();
		if (text == null || string.Equals(text, "null;"))
		{
			return false;
		}
		int[] array = (int[])allNextStates.get(text);
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			NfaState nfaState = (NfaState)indexedAllStates[array[i]];
			if ((nfaState.asciiMoves[(int)ch / 64] & (1L << ((64 != -1) ? ((int)ch % 64) : 0))) != 0)
			{
				return true;
			}
		}
		return false;
	}

	
	public virtual int getFirstValidPos(string str, int i1, int i2)
	{
		if (onlyChar == 1)
		{
			int num = matchSingleChar;
			while (num != str[i1])
			{
				i1++;
				if (i1 >= i2)
				{
					break;
				}
			}
			return i1;
		}
		do
		{
			if (CanMoveUsingChar(str[i1]))
			{
				return i1;
			}
			i1++;
		}
		while (i1 < i2);
		return i1;
	}

	
	public static int MoveFromSet(char ch, ArrayList v1, ArrayList v2)
	{
		int num = int.MaxValue;
		int index = v1.Count;
		while (index-- > 0)
		{
			int num2;
			if (num > (num2 = ((NfaState)v1[index]).MoveFrom(ch, v2)))
			{
				num = num2;
			}
		}
		return num;
	}

	
	public static int moveFromSetForRegEx(char ch, NfaState[] nsarr1, NfaState[] nsarr2, int i)
	{
		int num = 0;
		int num2 = nsarr1.Length;
		for (int j = 0; j < num2; j++)
		{
			NfaState nfaState;
			if ((nfaState = nsarr1[j]) == null)
			{
				break;
			}
			if (!nfaState.CanMoveUsingChar(ch))
			{
				continue;
			}
			if (nfaState.kindToPrint != int.MaxValue)
			{
				nsarr2[num] = null;
				return 1;
			}
			NfaState[] array = nfaState.next.epsilonMoveArray;
			int num3 = array.Length;
			while (num3-- > 0)
			{
				NfaState nfaState2;
				if ((nfaState2 = array[num3]).round != i)
				{
					nfaState2.round = i;
					int num4 = num;
					num++;
					nsarr2[num4] = nfaState2;
				}
			}
		}
		nsarr2[num] = null;
		return int.MaxValue;
	}

	
	internal static int InitStateName()
	{
		string text = LexGen.initialState.GetEpsilonMovesString();
		if (LexGen.initialState.usefulEpsilonMoves != 0)
		{
			int result = StateNameForComposite(text);
			
			return result;
		}
		return -1;
	}

	
	internal static string GetStateSetString(ArrayList P_0)
	{
		if (P_0 == null || P_0.Count == 0)
		{
			return "null;";
		}
		int[] array = new int[P_0.Count];
		string str = "{ ";
		int num = 0;
		while (num < P_0.Count)
		{
			int num2;
			str = new StringBuilder().Append(str).Append(num2 = ((NfaState)P_0[num]).stateName).Append(", ")
				.ToString();
			array[num] = num2;
			int num3 = num;
			num++;
			if (num3 > 0)
			{
				int num4 = num;
				if (16 == -1 || num4 % 16 == 0)
				{
					str = new StringBuilder().Append(str).Append("\n").ToString();
				}
			}
		}
		str = new StringBuilder().Append(str).Append("};").ToString();
		allNextStates.Add(str, array);
		return str;
	}

	
	private bool FindCommonBlocks()
	{
		if (next == null || next.usefulEpsilonMoves <= 1)
		{
			return false;
		}
		if (stateDone == null)
		{
			stateDone = new bool[generatedStates];
		}
		string key = next.epsilonMovesString;
		int[] array = (int[])allNextStates.get(key);
		if ((nint)array.LongLength <= 2 || compositeStateTable.get(key) != null)
		{
			return false;
		}
		int[] array2 = new int[(nint)array.LongLength];
		bool[] array3 = new bool[(nint)array.LongLength];
		int[] array4 = new int[allNextStates.Count];
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			if (array[i] != -1)
			{
				int num = i;
				int num2 = ((!stateDone[array[i]]) ? 1 : 0);
				int num3 = num;
				bool[] array5 = array3;
				int num4 = num2;
				array5[num3] = (byte)num2 != 0;
				if (num4 != 0)
				{
					num2 = 0;
					int[] array6 = array4;
					array6[num2]++;
				}
			}
		}
		int num5 = 0;
		int num6 = 0;
		Enumeration enumeration = allNextStates.keys();
		int[] array7;
		int num8;
		while (enumeration.hasMoreElements())
		{
			array7 = (int[])allNextStates.get(enumeration.nextElement());
			if (array7 == array)
			{
				continue;
			}
			int num7 = 0;
			for (int j = 0; j < (nint)array.LongLength; j++)
			{
				if (array[j] != -1 && array3[j] && ElemOccurs(array[j], array7) >= 0)
				{
					if (num7 == 0)
					{
						num7 = 1;
						num6++;
					}
					int num2 = array2[j];
					int[] array6 = array4;
					array6[num2]--;
					num2 = num6;
					array6 = array4;
					array6[num2]++;
					array2[j] = num6;
				}
			}
			if (num7 == 0)
			{
				continue;
			}
			num8 = -1;
			num5 = 0;
			for (int j = 0; j <= num6; j++)
			{
				if (array4[j] > num5)
				{
					num8 = j;
					num5 = array4[j];
				}
			}
			if (num5 <= 1)
			{
				return false;
			}
			for (int j = 0; j < (nint)array.LongLength; j++)
			{
				if (array[j] != -1 && array2[j] != num8)
				{
					array3[j] = false;
					int num2 = array2[j];
					int[] array6 = array4;
					array6[num2]--;
				}
			}
		}
		if (num5 <= 1)
		{
			return false;
		}
		array7 = new int[num5];
		num8 = 0;
		for (int i = 0; i < (nint)array.LongLength; i++)
		{
			if (array3[i])
			{
				if (((NfaState)indexedAllStates[array[i]]).isComposite)
				{
					return false;
				}
				stateDone[array[i]] = true;
				int[] array8 = array7;
				int num9 = num8;
				num8++;
				array8[num9] = array[i];
			}
		}
		string stateSetString = GetStateSetString(array7);
		enumeration = allNextStates.keys();
		while (enumeration.hasMoreElements())
		{
			int num10 = 1;
			string key2;
			int[] array9 = (int[])allNextStates.get(key2 = (string)enumeration.nextElement());
			if (array9 == array7)
			{
				continue;
			}
			int num11 = 0;
			while (true)
			{
				if (num11 < num8)
				{
					int num12;
					if ((num12 = ElemOccurs(array7[num11], array9)) < 0)
					{
						break;
					}
					if (num10 == 0)
					{
						array9[num12] = -1;
					}
					num10 = 0;
					num11++;
					continue;
				}
				if (stateSetsToFix.get(key2) == null)
				{
					stateSetsToFix.Add(key2, array9);
				}
				break;
			}
		}
		next.usefulEpsilonMoves -= num5 - 1;
		AddCompositeStateSet(stateSetString, false);
		return true;
	}

	
	private bool CheckNextOccursTogether()
	{
		if (next == null || next.usefulEpsilonMoves <= 1)
		{
			return true;
		}
		string key = next.epsilonMovesString;
		int[] array = (int[])allNextStates.get(key);
		if ((nint)array.LongLength == 1 || compositeStateTable.get(key) != null || stateSetsToFix.get(key) != null)
		{
			return false;
		}
		Hashtable hashtable = new Hashtable();
		NfaState nfaState = (NfaState)allStates[array[0]];
		for (int i = 1; i < (nint)array.LongLength; i++)
		{
			NfaState nfaState2 = (NfaState)allStates[array[i]];
			if (nfaState.inNextOf != nfaState2.inNextOf)
			{
				return false;
			}
		}
		Enumeration enumeration = allNextStates.keys();
		while (enumeration.hasMoreElements())
		{
			string key2;
			int[] array2 = (int[])allNextStates.get(key2 = (string)enumeration.nextElement());
			if (array2 == array)
			{
				continue;
			}
			int num = 0;
			int j;
			for (j = 0; j < (nint)array.LongLength; j++)
			{
				if (ElemOccurs(array[j], array2) >= 0)
				{
					num++;
				}
				else if (num > 0)
				{
					return false;
				}
			}
			if (num == j)
			{
				if ((nint)array2.LongLength > (nint)array.LongLength)
				{
					hashtable.Add(key2, array2);
				}
				if (compositeStateTable.get(key2) != null || stateSetsToFix.get(key2) != null)
				{
					return false;
				}
			}
			else if (num != 0)
			{
				return false;
			}
		}
		enumeration = hashtable.keys();
		while (enumeration.hasMoreElements())
		{
			string key2;
			int[] array2 = (int[])hashtable.get(key2 = (string)enumeration.nextElement());
			if (stateSetsToFix.get(key2) == null)
			{
				stateSetsToFix.Add(key2, array2);
			}
			for (int k = 0; k < (nint)array2.LongLength; k++)
			{
				if (ElemOccurs(array2[k], array) > 0)
				{
					array2[k] = -1;
				}
			}
		}
		next.usefulEpsilonMoves = 1;
		AddCompositeStateSet(next.epsilonMovesString, false);
		return true;
	}

	
	private static void FindStatesWithNoBreak()
	{
		Hashtable hashtable = new Hashtable();
		bool[] array = new bool[generatedStates];
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < allStates.Count; i++)
		{
			NfaState nfaState = null;
			NfaState nfaState2 = (NfaState)allStates[i];
			if (nfaState2.stateName == -1 || nfaState2.dummy || !nfaState2.UsefulState() || nfaState2.next == null || nfaState2.next.usefulEpsilonMoves < 1)
			{
				continue;
			}
			string text = nfaState2.next.epsilonMovesString;
			if (compositeStateTable.get(text) != null || hashtable.get(text) != null)
			{
				continue;
			}
			hashtable.Add(text, text);
			int[] array2 = (int[])allNextStates.get(text);
			if ((nint)array2.LongLength == 1)
			{
				continue;
			}
			int j;
			for (j = 0; j < (nint)array2.LongLength; j++)
			{
				int num3;
				if ((num3 = array2[j]) == -1)
				{
					continue;
				}
				NfaState nfaState3 = (NfaState)allStates[num3];
				if (!nfaState3.isComposite && nfaState3.inNextOf == 1)
				{
					if (array[num3])
					{
						
						throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu");
					}
					num2 = j;
					num++;
					nfaState = nfaState3;
					array[num3] = true;
					break;
				}
			}
			if (nfaState == null)
			{
				continue;
			}
			j = 0;
			while (true)
			{
				if (j < (nint)array2.LongLength)
				{
					int num3;
					if ((num3 = array2[j]) != -1)
					{
						NfaState nfaState3 = (NfaState)allStates[num3];
						if (!array[num3] && nfaState3.inNextOf > 1 && !nfaState3.isComposite && nfaState3.stateForCase == null)
						{
							num++;
							array2[j] = -1;
							array[num3] = true;
							int num4 = array2[0];
							array2[0] = array2[num2];
							array2[num2] = num4;
							nfaState3.stateForCase = nfaState;
							nfaState.stateForCase = nfaState3;
							stateSetsToFix.Add(text, array2);
							break;
						}
					}
					j++;
					continue;
				}
				for (j = 0; j < (nint)array2.LongLength; j++)
				{
					int num3;
					if ((num3 = array2[j]) != -1)
					{
						NfaState nfaState3 = (NfaState)allStates[num3];
						if (nfaState3.inNextOf <= 1)
						{
							array[num3] = false;
						}
					}
				}
				break;
			}
		}
	}

	static NfaState()
	{
		unicodeWarningGiven = false;
		generatedStates = 0;
		idCnt = 0;
		dummyStateIndex = -1;
		allStates = new ArrayList();
		indexedAllStates = new ArrayList();
		nonAsciiTableForMethod = new ArrayList();
		equivStatesTable = new Hashtable();
		allNextStates = new Hashtable();
		lohiByteTab = new Hashtable();
		stateNameForComposite = new Hashtable();
		compositeStateTable = new Hashtable();
		stateBlockTable = new Hashtable();
		stateSetsToFix = new Hashtable();
		jjCheckNAddStatesUnaryNeeded = false;
		allBitVectors = new ArrayList();
		tmpIndices = new int[512];
		allBits = "{\n   0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL\n};";
		tableToDump = new Hashtable();
		orderedStateSet = new ArrayList();
		lastIndex = 0;
	}
}
