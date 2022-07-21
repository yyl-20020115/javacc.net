using JavaCC.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace JavaCC.Parser;


public class NfaState 
{
	public static bool UnicodeWarningGiven =false;

	public static int GeneratedStates;

	internal static int IdCnt;

	internal static int LohiByteCnt;

	internal static int DummyStateIndex;

	internal static bool Done;

	internal static bool[] Mark;

	internal static bool[] StateDone;

	internal static List<NfaState> AllStates = new();

	internal static List<NfaState> IndexedAllStates = new();

	internal static List<NfaState> NonAsciiTableForMethod = new();

	internal static Dictionary<string, NfaState> EquivStatesTable =new();

	internal static Dictionary<string,int[]> AllNextStates=new();

	internal static Dictionary<string, int> LohiByteTab = new();

	internal static Dictionary<string,int> _StateNameForComposite = new();

	internal static Dictionary<string,int[]> CompositeStateTable =new();

	internal static Dictionary<string, string> StateBlockTable = new();

	internal static Dictionary<string,int[]> StateSetsToFix= new ();

	internal static bool jjCheckNAddStatesUnaryNeeded;

	internal long[] asciiMoves;

	internal char[] charMoves;

	internal char[] rangeMoves;

	internal NfaState next;

	internal NfaState stateForCase;

	internal List<NfaState> epsilonMoves;

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

	public List<int> loByteVec = new();

	public int[] nonAsciiMoveIndices;

	internal int round;

	internal int onlyChar;

	internal char matchSingleChar;

	private bool closureDone;

	internal static List<string> allBitVectors =new();

	internal static int[] tmpIndices;

	internal static string allBits;

	internal static Dictionary<string,int[]> tableToDump =new();

	internal static List<int[]> orderedStateSet = new();

	internal static int lastIndex;

	internal static int[][] kinds;

	internal static int[][][] statesForState;

	
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
		if (!UnicodeWarningGiven && P_0 > 'ÿ' && !Options.JavaUnicodeEscape && !Options.UserCharStream)
		{
			UnicodeWarningGiven = true;
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
		GeneratedStates = 0;
		IdCnt = 0;
		DummyStateIndex = -1;
		Done = false;
		Mark = null;
		StateDone = null;
		AllStates.Clear();
		IndexedAllStates.Clear();
		EquivStatesTable.Clear();
		AllNextStates.Clear();
		CompositeStateTable.Clear();
		StateBlockTable.Clear();
		_StateNameForComposite.Clear();
		StateSetsToFix.Clear();
	}

	
	internal NfaState()
	{
		asciiMoves = new long[2];
		charMoves = null;
		rangeMoves = null;
		next = null;
		epsilonMoves = new ();
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
		id = IdCnt++;
		AllStates.Add(this);
		lexState = LexGen.lexStateIndex;
		lookingFor = LexGen.curKind;
	}

	
	internal virtual void AddMove(NfaState s)
	{
		if (!epsilonMoves.Contains(s))
		{
			InsertInOrder(epsilonMoves, s);
		}
	}

	
	public static void ComputeClosures()
	{
		int index = AllStates.Count;
		while (index-- > 0)
		{
			NfaState nfaState = (NfaState)AllStates[index];
			if (!nfaState.closureDone)
			{
				nfaState.OptimizeEpsilonMoves(true);
			}
		}
		for (index = 0; index < AllStates.Count; index++)
		{
			NfaState nfaState = (NfaState)AllStates[index];
			if (!nfaState.closureDone)
			{
				nfaState.OptimizeEpsilonMoves(false);
			}
		}
		for (index = 0; index < AllStates.Count; index++)
		{
			NfaState nfaState = (NfaState)AllStates[index];
			nfaState.epsilonMoveArray = new NfaState[nfaState.epsilonMoves.Count];
			nfaState.epsilonMoves.CopyTo(nfaState.epsilonMoveArray);
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
				stateName = GeneratedStates++;
				IndexedAllStates.Add(this);
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
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
			if (nfaState.lexState == LexGen.lexStateIndex && nfaState.HasTransitions() && !nfaState.dummy && nfaState.stateName != -1)
			{
				if (array == null)
				{
					array = new int[GeneratedStates];
					statesForState[LexGen.lexStateIndex] = new int[Math.Max(GeneratedStates, DummyStateIndex + 1)][];
				}
				array[nfaState.stateName] = nfaState.lookingFor;
				statesForState[LexGen.lexStateIndex][nfaState.stateName] = nfaState.compositeStates;
				nfaState.GenerateNonAsciiMoves(pw);
			}
		}
		foreach(var pair in _StateNameForComposite)
		{
			int num = pair.Value;
			if (num >= GeneratedStates)
			{
				if(AllNextStates.TryGetValue(pair.Key,out var v))
                {
					statesForState[LexGen.lexStateIndex][num]= v;
				}
			}
		}
		if (StateSetsToFix.Count != 0)
		{
			FixStateSets();
		}
		kinds[LexGen.lexStateIndex] = array;
		pw.WriteLine(((!Options.Static) ? "" : "static ")+("private int ")+("jjMoveNfa")
			+(LexGen.lexStateSuffix)
			+("(int startState, int curPos)")
			);
		pw.WriteLine("{");
		if (GeneratedStates == 0)
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
		pw.WriteLine(("   jjnewStateCnt = ")+(GeneratedStates)+(";")
			);
		pw.WriteLine("   int i = 1;");
		pw.WriteLine("   jjstateSet[0] = startState;");
		if (Options.DebugTokenManager)
		{
			pw.WriteLine("      debugStream.WriteLine(\"   Starting NFA to match one of : \" + jjKindsForStateVector(curLexState, jjstateSet, 0, 1));");
		}
		if (Options.DebugTokenManager)
		{
			pw.WriteLine(("      debugStream.WriteLine(")+((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ")+("\"Current character : \" + ")
				+("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
				+("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
				);
		}
		pw.WriteLine("   //int j; // not used");
		pw.WriteLine(("   int kind = 0x")+(Utils.ToString(int.MaxValue,16))+(";")
			);
		pw.WriteLine("   for (;;)");
		pw.WriteLine("   {");
		pw.WriteLine(("      if (++jjround == 0x")+(Utils.ToString(int.MaxValue,16))+(")")
			);
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
		pw.WriteLine(("      if (kind != 0x")+(Utils.ToString(int.MaxValue,16))+(")")
			);
		pw.WriteLine("      {");
		pw.WriteLine("         jjmatchedKind = kind;");
		pw.WriteLine("         jjmatchedPos = curPos;");
		pw.WriteLine(("         kind = 0x")+(Utils.ToString(int.MaxValue,16))+(";")
			);
		pw.WriteLine("      }");
		pw.WriteLine("      ++curPos;");
		if (Options.DebugTokenManager)
		{
			pw.WriteLine(("      if (jjmatchedKind != 0 && jjmatchedKind != 0x")+(Utils.ToString(int.MaxValue,16))+(")")
				);
			pw.WriteLine("         debugStream.WriteLine(\"   Currently matched the first \" + (jjmatchedPos + 1) + \" characters as a \" + tokenImage[jjmatchedKind] + \" token.\");");
		}
		pw.WriteLine(("      if ((i = jjnewStateCnt) == (startsAt = ")+(GeneratedStates)+(" - (jjnewStateCnt = startsAt)))")
			);
		if (LexGen.mixed[LexGen.lexStateIndex])
		{
			pw.WriteLine("         break;");
		}
		else
		{
			pw.WriteLine("         return curPos;");
		}
		if (Options.DebugTokenManager)
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
		if (Options.DebugTokenManager)
		{
			pw.WriteLine(("      debugStream.WriteLine(")+((LexGen.maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ")+("\"Current character : \" + ")
				+("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
				+("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
				);
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
		AllStates.Clear();
	}

	
	public static void DumpStateSets(TextWriter pw)
	{
		int num = 0;
		pw.Write("static final int[] jjnextStates = {");
		for (int i = 0; i < orderedStateSet.Count; i++)
		{
			int[] array = (int[])orderedStateSet[i];
			for (int j = 0; j < array.Length; j++)
			{
				int num2 = num;
				num++;
				if (16 == -1 || num2 % 16 == 0)
				{
					pw.Write("\n   ");
				}
				pw.Write((array[j])+(", "));
			}
		}
		pw.WriteLine("\n};");
	}

	
	public static void DumpNonAsciiMoveMethods(TextWriter pw)
	{
		if ((Options.JavaUnicodeEscape || UnicodeWarningGiven) && NonAsciiTableForMethod.Count > 0)
		{
			for (int i = 0; i < NonAsciiTableForMethod.Count; i++)
			{
				NfaState nfaState = NonAsciiTableForMethod[i];
				nfaState.DumpNonAsciiMoveMethod(pw);
			}
		}
	}

	
	public static void DumpStatesForKind(TextWriter pw)
	{
		DumpStatesForState(pw);
		int num = 0;
		
		pw.Write("protected static final int[][] kindForState = ");
		if (kinds == null)
		{
			pw.WriteLine("null;");
			return;
		}
		pw.WriteLine("{");
		for (int i = 0; i < kinds.Length; i++)
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
			for (int j = 0; j < kinds[i].Length; j++)
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

	
	internal static void PrintBoilerPlate(TextWriter writer)
	{
		writer.WriteLine(((!Options.Static) ? "" : "static ")+("private void ")+("jjCheckNAdd(int state)")
			);
		writer.WriteLine("{");
		writer.WriteLine("   if (jjrounds[state] != jjround)");
		writer.WriteLine("   {");
		writer.WriteLine("      jjstateSet[jjnewStateCnt++] = state;");
		writer.WriteLine("      jjrounds[state] = jjround;");
		writer.WriteLine("   }");
		writer.WriteLine("}");
		writer.WriteLine(((!Options.Static) ? "" : "static ")+("private void ")+("jjAddStates(int start, int end)")
			);
		writer.WriteLine("{");
		writer.WriteLine("   do {");
		writer.WriteLine("      jjstateSet[jjnewStateCnt++] = jjnextStates[start];");
		writer.WriteLine("   } while (start++ != end);");
		writer.WriteLine("}");
		writer.WriteLine(((!Options.Static) ? "" : "static ")+("private void ")+("jjCheckNAddTwoStates(int state1, int state2)")
			);
		writer.WriteLine("{");
		writer.WriteLine("   jjCheckNAdd(state1);");
		writer.WriteLine("   jjCheckNAdd(state2);");
		writer.WriteLine("}");
		writer.WriteLine("");
		writer.WriteLine(((!Options.Static) ? "" : "static ")+("private void ")+("jjCheckNAddStates(int start, int end)")
			);
		writer.WriteLine("{");
		writer.WriteLine("   do {");
		writer.WriteLine("      jjCheckNAdd(jjnextStates[start]);");
		writer.WriteLine("   } while (start++ != end);");
		writer.WriteLine("}");
		writer.WriteLine("");
		if (jjCheckNAddStatesUnaryNeeded)
		{
			writer.WriteLine(((!Options.Static) ? "" : "static ")+("private void ")+("jjCheckNAddStates(int start)")
				);
			writer.WriteLine("{");
			writer.WriteLine("   jjCheckNAdd(jjnextStates[start]);");
			writer.WriteLine("   jjCheckNAdd(jjnextStates[start + 1]);");
			writer.WriteLine("}");
			writer.WriteLine("");
		}
	}

	public virtual bool HasTransitions()
	{
		return (asciiMoves[0] != 0 || asciiMoves[1] != 0 || (charMoves != null && charMoves[0] != 0) || (rangeMoves != null && rangeMoves[0] != 0)) ? true : false;
	}

	
	public static void reInit()
	{
		UnicodeWarningGiven = false;
		GeneratedStates = 0;
		IdCnt = 0;
		LohiByteCnt = 0;
		DummyStateIndex = -1;
		Done = false;
		Mark = null;
		StateDone = null;
		AllStates = new ();
		IndexedAllStates = new ();
		NonAsciiTableForMethod = new ();
		EquivStatesTable = new ();
		AllNextStates = new ();
		LohiByteTab = new ();
		_StateNameForComposite = new ();
		CompositeStateTable = new ();
		StateBlockTable = new ();
		StateSetsToFix = new ();
		allBitVectors = new ();
		tmpIndices = new int[512];
		allBits = "{\n   0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL\n};";
		tableToDump = new ();
		orderedStateSet = new ();
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
				char[] dest = new char[charMoves.Length + P_0.charMoves.Length];
				Array.Copy(charMoves, 0, dest, 0, charMoves.Length);
				charMoves = dest;
				for (int i = 0; i < P_0.charMoves.Length; i++)
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
				char[] dest = new char[rangeMoves.Length + P_0.rangeMoves.Length];
				Array.Copy(rangeMoves, 0, dest, 0, rangeMoves.Length);
				rangeMoves = dest;
				for (int i = 0; i < P_0.rangeMoves.Length; i += 2)
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

	
	internal static void InsertInOrder(List<NfaState> P_0, NfaState P_1)
	{
		int i;
		for (i = 0; i < P_0.Count && ((NfaState)P_0[i]).id <= P_1.id; i++)
		{
			if (((NfaState)P_0[i]).id == P_1.id)
			{
				return;
			}
		}
		P_0.Insert(i, P_1);
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
		char[] array = new char[P_0.Length + P_1];
		Array.Copy(P_0, 0, array, 0, P_0.Length);
		return array;
	}

	
	private void EpsilonClosure()
	{
		
		if (closureDone || Mark[id])
		{
			return;
		}
		Mark[id] = true;
		for (int i = 0; i < epsilonMoves.Count; i++)
		{
			((NfaState)epsilonMoves[i]).EpsilonClosure();
		}
		foreach(var nfaState in epsilonMoves)
		{
			for (int i = 0; i < nfaState.epsilonMoves.Count; i++)
			{
				NfaState nfaState2 = (NfaState)nfaState.epsilonMoves[i];
				if (nfaState2.UsefulState() && !epsilonMoves.Contains(nfaState2))
				{
					InsertInOrder(epsilonMoves, nfaState2);
					Done = false;
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
		if (!UnicodeWarningGiven && (num > 255 || P_1 > 'ÿ') && !Options.JavaUnicodeEscape && !Options.UserCharStream)
		{
			UnicodeWarningGiven = true;
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
		if (P_0 != null && P_1 != null && P_0.Length == P_1.Length)
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
		int index = AllStates.Count;
		while (index-- > 0)
		{
			NfaState nfaState = (NfaState)AllStates[index];
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
		Done = false;
		int num;
		while (!Done)
		{
			if (Mark == null || Mark.Length < AllStates.Count)
			{
				Mark = new bool[AllStates.Count];
			}
			num = AllStates.Count;
			while (num-- > 0)
			{
				Mark[num] = false;
			}
			Done = true;
			EpsilonClosure();
		}
		num = AllStates.Count;
		while (num-- > 0)
		{
			((NfaState)AllStates[num]).closureDone = Mark[((NfaState)AllStates[num]).id];
		}
		int num2 = 1;
		NfaState nfaState = null;
		List<NfaState> vector = null;
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
								vector = new ();
								vector.Add(nfaState2);
							}
							InsertInOrder(vector, nfaState3);
							epsilonMoves.RemoveAt(i--);
						}
					}
				}
				if (vector != null)
				{
					num2 = 1;
					string text = "";
					for (int j = 0; j < vector.Count; j++)
					{
						text = (text)+((vector[j].id))+(", ")
							;
					}
					if(!EquivStatesTable.TryGetValue(text,out nfaState))
					{
						nfaState = CreateEquivState(vector);
						EquivStatesTable.Add(text, nfaState);
					}
					epsilonMoves.RemoveAt(num--);
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
						epsilonMoves.RemoveAt(i--);
					}
				}
				if (nfaState != null)
				{
					epsilonMoves.RemoveAt(num--);
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
				epsilonMoves.RemoveAt(num--);
			}
		}
	}

	
	internal virtual NfaState CreateEquivState(List<NfaState> P_0)
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
			StringBuilder builder;
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
				IndexedAllStates[(nfaState.stateName)].inNextOf++;
				array[num] = nfaState.stateName;
				builder = new StringBuilder();
				epsilonMovesString = builder+(epsilonMovesString)+(nfaState.stateName)+(", ")
					;
				int num2 = num;
				num++;
				if (num2 > 0)
				{
					int num3 = num;
					if (16 == -1 || num3 % 16 == 0)
					{
						builder = new StringBuilder();
						epsilonMovesString = builder+(epsilonMovesString)+("\n");
					}
				}
			}
			builder = new StringBuilder();
			epsilonMovesString = builder+(epsilonMovesString)+("};");
		}
		usefulEpsilonMoves = num;
		if (epsilonMovesString != null && !AllNextStates.TryGetValue(epsilonMovesString,out var s))
		{
			int[] array2 = new int[usefulEpsilonMoves];
			Array.Copy(array, 0, array2, 0, num);
			AllNextStates.Add(epsilonMovesString, array2);
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
			for (int i = 0; i < charMoves.Length; i++)
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
			for (int i = 0; i < rangeMoves.Length; i += 2)
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

	
	public virtual int MoveFrom(char ch, List<NfaState> v)
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
		for (int i = 0; i < NonAsciiTableForMethod.Count; i++)
		{
			NfaState nfaState = (NfaState)NonAsciiTableForMethod[i];
			if (EqualLoByteVectors(loByteVec, nfaState.loByteVec) && EqualNonAsciiMoveIndices(nonAsciiMoveIndices, nfaState.nonAsciiMoveIndices))
			{
				nonAsciiMethod = i;
				return;
			}
		}
		nonAsciiMethod = NonAsciiTableForMethod.Count;
		NonAsciiTableForMethod.Add(this);
	}

	
	private static bool EqualLoByteVectors(List<int> P_0, List<int> P_1)
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
			if (((int)P_0[i]) != ((int)P_1[i]))
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
		if (P_0.Length != P_1.Length)
		{
			return false;
		}
		for (int i = 0; i < P_0.Length; i++)
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
        if (!_StateNameForComposite.TryGetValue(P_0, out int integer))
        {
            int result = integer;

            return result;
        }
        int i = 0;
		if (AllNextStates.TryGetValue(P_0,out var array)&&!P_1)
		{
			StateBlockTable.Add(P_0, P_0);
		}
		if (array == null)
		{
			string message = ("JavaCC Bug: Please send mail to sankar@cs.stanford.edu; nameSet null for : ")+(P_0);
			
			throw new Exception(message);
		}
		if (array.Length == 1)
		{
			;
			integer = (array[0]);
			_StateNameForComposite.Add(P_0, integer);
			return array[0];
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] != -1)
			{
				NfaState nfaState = IndexedAllStates[array[j]];
				nfaState.isComposite = true;
				nfaState.compositeStates = array;
			}
		}
		for (; i < array.Length; i++)
		{
			if (!P_1)
			{
				break;
			}
			if (((NfaState)IndexedAllStates[array[i]]).inNextOf <= 1)
			{
				break;
			}
		}
		foreach(var pair in CompositeStateTable)
		{
			string text = pair.Key;
			if (!string.Equals(text, P_0) && Intersect(P_0, text))
			{
				for (int[] array2 = pair.Value; i < array.Length && ((P_1 && ((NfaState)IndexedAllStates[array[i]]).inNextOf > 1) || ElemOccurs(array[i], array2) >= 0); i++)
				{
				}
			}
		}
		int num = ((i < array.Length) ? array[i] : ((DummyStateIndex != -1) ? (++DummyStateIndex) : (DummyStateIndex = GeneratedStates)));
		integer = (num);
		_StateNameForComposite.Add(P_0, integer);
		CompositeStateTable.Add(P_0, array);
		return num;
	}

	
	private static bool Intersect(string P_0, string P_1)
	{
		if (P_0 == null || P_1 == null)
		{
			return false;
		}
		AllNextStates.TryGetValue(P_0, out var array);
		AllNextStates.TryGetValue(P_1, out var array2);
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
		return _StateNameForComposite.TryGetValue(P_0, out var r) ? r : -1;
	}


	internal static int AddStartStateSet(string P_0)
	{
		return AddCompositeStateSet(P_0, true);
	}

	
	internal static string GetStateSetString(int[] P_0)
	{
		string str = "{ ";
		int num = 0;
		while (num < P_0.Length)
		{
			str = (str)+(P_0[num])+(", ")
				;
			int num2 = num;
			num++;
			if (num2 > 0)
			{
				int num3 = num;
				if (16 == -1 || num3 % 16 == 0)
				{
					str = (str)+("\n");
				}
			}
		}
		str = (str)+("};");
		AllNextStates.Add(str, P_0);
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

	
	private void DumpAsciiMoveForCompositeState(TextWriter writer, int mid, bool use_else)
	{
		int num = (selfLoop() ? 1 : 0);
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.asciiMoves[mid] != 0 && num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
			{
				num = 1;
				break;
			}
		}
		string str = "";
		if (asciiMoves[mid] != -1)
		{
			int num2 = OnlyOneBitSet(asciiMoves[mid]);
			if (num2 != -1)
			{
				writer.WriteLine(("                  ")+((!use_else) ? "" : "else ")+("if (curChar == ")
					+(64 * mid + num2)
					+(")")
					);
			}
			else
			{
				writer.WriteLine(("                  ")+((!use_else) ? "" : "else ")+("if ((0x")
					+(Utils.ToHexString(asciiMoves[mid]))
					+("L & l) != 0L)")
					);
			}
			str = "   ";
		}
		if (kindToPrint != int.MaxValue)
		{
			if (asciiMoves[mid] != -1)
			{
				writer.WriteLine("                  {");
			}
			writer.WriteLine((str)+("                  if (kind > ")+(kindToPrint)
				+(")")
				);
			writer.WriteLine((str)+("                     kind = ")+(kindToPrint)
				+(";")
				);
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			AllNextStates.TryGetValue(next.epsilonMovesString, out var array);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					writer.WriteLine((str)+("                  jjCheckNAdd(")+(i2)
						+(");")
						);
				}
				else
				{
					writer.WriteLine((str)+("                  jjstateSet[jjnewStateCnt++] = ")+(i2)
						+(";")
						);
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				writer.WriteLine((str)+("                  jjCheckNAddTwoStates(")+(array[0])
					+(", ")
					+(array[1])
					+(");")
					);
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num3 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					writer.Write((str)+("                  jjCheckNAddStates(")+(stateSetIndicesForUse[0])
						);
					if (num3 != 0)
					{
						writer.Write((", ")+(stateSetIndicesForUse[1]));
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					writer.WriteLine(");");
				}
				else
				{
					writer.WriteLine((str)+("                  jjAddStates(")+(stateSetIndicesForUse[0])
						+(", ")
						+(stateSetIndicesForUse[1])
						+(");")
						);
				}
			}
		}
		if (asciiMoves[mid] != -1 && kindToPrint != int.MaxValue)
		{
			writer.WriteLine("                  }");
		}
	}

	

	private void DumpNonAsciiMoveForCompositeState(TextWriter P_0)
	{
		int num = (selfLoop() ? 1 : 0);
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.nonAsciiMethod != -1 && num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
			{
				num = 1;
				break;
			}
		}
		if (!Options.JavaUnicodeEscape && !UnicodeWarningGiven)
		{
			if (loByteVec != null && loByteVec.Count > 1)
			{
				P_0.WriteLine(("                  if ((jjbitVec") + (((int)loByteVec[1]))+("[i2")
					+("] & l2) != 0L)")
					);
			}
		}
		else
		{
			P_0.WriteLine(("                  if (jjCanMove_")+(nonAsciiMethod)+("(hiByte, i1, i2, l1, l2))")
				);
		}
		if (kindToPrint != int.MaxValue)
		{
			P_0.WriteLine("                  {");
			P_0.WriteLine(("                     if (kind > ")+(kindToPrint)+(")")
				);
			P_0.WriteLine(("                        kind = ")+(kindToPrint)+(";")
				);
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			AllNextStates.TryGetValue(next.epsilonMovesString,out var array);
			//int[] array = (int[])allNextStates.get(next.epsilonMovesString);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine(("                     jjCheckNAdd(")+(i2)+(");")
						);
				}
				else
				{
					P_0.WriteLine(("                     jjstateSet[jjnewStateCnt++] = ")+(i2)+(";")
						);
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine(("                     jjCheckNAddTwoStates(")+(array[0])+(", ")
					+(array[1])
					+(");")
					);
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num2 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write(("                     jjCheckNAddStates(")+(stateSetIndicesForUse[0]));
					if (num2 != 0)
					{
						P_0.Write((", ")+(stateSetIndicesForUse[1]));
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine(("                     jjAddStates(")+(stateSetIndicesForUse[0])+(", ")
						+(stateSetIndicesForUse[1])
						+(");")
						);
				}
			}
		}
		if (kindToPrint != int.MaxValue)
		{
			P_0.WriteLine("                  }");
		}
	}

	
	private string PrintNoBreak(TextWriter writer, int mid, bool[] visited)
	{
		if (inNextOf != 1)
		{
			
			throw new System.Exception("JavaCC Bug: Please send mail to sankar@cs.stanford.edu");
		}
		visited[stateName] = true;
		if (mid >= 0)
		{
			if (asciiMoves[mid] != 0)
			{
				writer.WriteLine(("               case ")+(stateName)+(":")
					);
				DumpAsciiMoveForCompositeState(writer, mid, false);
				return "";
			}
		}
		else if (nonAsciiMethod != -1)
		{
			writer.WriteLine(("               case ")+(stateName)+(":")
				);
			DumpNonAsciiMoveForCompositeState(writer);
			return "";
		}
		string result = ("               case ")+(stateName)+(":\n")
			;
		
		return result;
	}

	
	private void DumpAsciiMove(TextWriter P_0, int P_1, bool[] P_2)
	{
		int num = ((selfLoop() && isComposite) ? 1 : 0);
		int num2 = 1;
		int i;
		for (i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
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
					P_0.WriteLine(("               case ")+(nfaState.stateName)+(":")
						);
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
				str = (" && kind > ")+(kindToPrint);
			}
			if (i != -1)
			{
				P_0.WriteLine(("                  if (curChar == ")+(64 * P_1 + i)+(str)
					+(")")
					);
			}
			else
			{
				P_0.WriteLine(("                  if ((0x")+(Utils.ToHexString(asciiMoves[P_1]))+("L & l) != 0L")
					+(str)
					+(")")
					);
			}
			P_0.WriteLine(("                     kind = ")+(kindToPrint)+(";")
				);
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
				P_0.WriteLine(("                  if (curChar != ")+(64 * P_1 + i)+(")")
					);
				P_0.WriteLine("                     break;");
			}
			else if (asciiMoves[P_1] != -1)
			{
				P_0.WriteLine(("                  if ((0x")+(Utils.ToHexString(asciiMoves[P_1]))+("L & l) == 0L)")
					);
				P_0.WriteLine("                     break;");
			}
			if (num2 != 0)
			{
				P_0.WriteLine(("                  kind = ")+(kindToPrint)+(";")
					);
			}
			else
			{
				P_0.WriteLine(("                  if (kind > ")+(kindToPrint)+(")")
					);
				P_0.WriteLine(("                     kind = ")+(kindToPrint)+(";")
					);
			}
		}
		else if (i != -1)
		{
			P_0.WriteLine(("                  if (curChar == ")+(64 * P_1 + i)+(")")
				);
			str = "   ";
		}
		else if (asciiMoves[P_1] != -1)
		{
			P_0.WriteLine(("                  if ((0x")+(Utils.ToHexString(asciiMoves[P_1]))+("L & l) != 0L)")
				);
			str = "   ";
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			AllNextStates.TryGetValue(next.epsilonMovesString, out var array);
			if (next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					P_0.WriteLine((str)+("                  jjCheckNAdd(")+(i2)
						+(");")
						);
				}
				else
				{
					P_0.WriteLine((str)+("                  jjstateSet[jjnewStateCnt++] = ")+(i2)
						+(";")
						);
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				P_0.WriteLine((str)+("                  jjCheckNAddTwoStates(")+(array[0])
					+(", ")
					+(array[1])
					+(");")
					);
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num3 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					P_0.Write((str)+("                  jjCheckNAddStates(")+(stateSetIndicesForUse[0])
						);
					if (num3 != 0)
					{
						P_0.Write((", ")+(stateSetIndicesForUse[1]));
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					P_0.WriteLine(");");
				}
				else
				{
					P_0.WriteLine((str)+("                  jjAddStates(")+(stateSetIndicesForUse[0])
						+(", ")
						+(stateSetIndicesForUse[1])
						+(");")
						);
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

	
	private static List<List<NfaState>> PartitionStatesSetForAscii(int[] P_0, int P_1)
	{
		int[] array = new int[P_0.Length];
		var vector = new List<NfaState>();
		List<List<NfaState>> vector2 = new ();
		vector.AddRange(Enumerable.Repeat<NfaState>(new NfaState(), P_0.Length));
		int num = 0;
		for (int i = 0; i < P_0.Length; i++)
		{
			NfaState nfaState = (NfaState)AllStates[P_0[i]];
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
				vector.Insert(j,nfaState);
				num++;
			}
		}
		//TODO:
		//vector.setSize(num);
		while (vector.Count > 0)
		{
			NfaState nfaState = (NfaState)vector[0];
			vector.Remove(nfaState);
			long num3 = nfaState.asciiMoves[P_1];
			List<NfaState> vector3 = new ();
			vector3.Add(nfaState);
			for (int k = 0; k < vector.Count; k++)
			{
				NfaState nfaState2 = (NfaState)vector[k];
				if ((nfaState2.asciiMoves[P_1] & num3) == 0)
				{
					num3 |= nfaState2.asciiMoves[P_1];
					vector3.Add(nfaState2);
					vector.RemoveAt(k--);
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
		AllNextStates.TryGetValue(next.epsilonMovesString, out var array);
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

	
	private static int[] GetStateSetIndicesForUse(string text)
	{
		AllNextStates.TryGetValue(text, out var array);
		
		if (!tableToDump.TryGetValue(text, out var array2))
		{
			array2 = new int[2]
			{
				lastIndex,
				(int)(lastIndex + array.Length - 1)
			};
			lastIndex = (int)(lastIndex + array.Length);
			tableToDump.Add(text, array2);
			orderedStateSet.Add(array);
		}
		return array2;
	}

	
	private static void DumpHeadForCase(TextWriter writer, int P_1)
	{
		switch (P_1)
		{
		case 0:
			writer.WriteLine("         long l = 1L << curChar;");
			break;
		case 1:
			writer.WriteLine("         long l = 1L << (curChar & 077);");
			break;
		default:
			if (Options.JavaUnicodeEscape || UnicodeWarningGiven)
			{
				writer.WriteLine("         int hiByte = (int)(curChar >> 8);");
				writer.WriteLine("         int i1 = hiByte >> 6;");
				writer.WriteLine("         long l1 = 1L << (hiByte & 077);");
			}
			writer.WriteLine("         int i2 = (curChar & 0xff) >> 6;");
			writer.WriteLine("         long l2 = 1L << (curChar & 077);");
			break;
		}
		writer.WriteLine("         do");
		writer.WriteLine("         {");
		writer.WriteLine("            switch(jjstateSet[--i])");
		writer.WriteLine("            {");
	}

	
	private static void DumpCompositeStatesAsciiMoves(TextWriter writer, string sn, int mvi, bool[] visited)
	{
		if (AllNextStates.TryGetValue(sn, out var array) && array.Length == 1 || visited[StateNameForComposite(sn)])
		{
			return;
		}
		NfaState nfaState = null;
		int num = 0;
		NfaState nfaState2 = null;
		string text = "";

		int num2 = ((StateBlockTable.ContainsKey(sn)) ? 1 : 0);
		for (int i = 0; i < array.Length; i++)
		{
			NfaState nfaState3 = (NfaState)AllStates[array[i]];
			if (nfaState3.asciiMoves[mvi] != 0)
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
				visited[nfaState3.stateName] = true;
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
			text = nfaState2.PrintNoBreak(writer, mvi, visited);
		}
		switch (num)
		{
		case 0:
			if (nfaState2 != null && string.Equals(text, ""))
			{
				writer.WriteLine("                  break;");
			}
			return;
		case 1:
			if (!string.Equals(text, ""))
			{
				writer.Write(text);
			}
			writer.WriteLine(("               case ")+(StateNameForComposite(sn))+(":")
				);
			if (!visited[nfaState.stateName] && num2 == 0 && nfaState.inNextOf > 1)
			{
				writer.WriteLine(("               case ")+(nfaState.stateName)+(":")
					);
			}
			visited[nfaState.stateName] = true;
			nfaState.DumpAsciiMove(writer, mvi, visited);
			return;
		}
		List<List<NfaState>> vector = PartitionStatesSetForAscii(array, mvi);
		if (!string.Equals(text, ""))
		{
			writer.Write(text);
		}
		int num4 = StateNameForComposite(sn);
		writer.WriteLine(("               case ")+(num4)+(":")
			);
		if (num4 < GeneratedStates)
		{
			visited[num4] = true;
		}
		for (int i = 0; i < vector.Count; i++)
		{
			var vector2 = vector[i];
			for (int j = 0; j < vector2.Count; j++)
			{
				NfaState nfaState3 = (NfaState)vector2[j];
				if (num2 != 0)
				{
					visited[nfaState3.stateName] = true;
				}
				nfaState3.DumpAsciiMoveForCompositeState(writer, mvi, (j != 0) ? true : false);
			}
		}
		if (num2 != 0)
		{
			writer.WriteLine("                  break;");
		}
		else
		{
			writer.WriteLine("                  break;");
		}
	}

	

	private void DumpNonAsciiMove(TextWriter writer, bool[] P_1)
	{
		int num = ((selfLoop() && isComposite) ? 1 : 0);
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
			if (this != nfaState && nfaState.stateName != -1 && !nfaState.dummy && stateName != nfaState.stateName && nfaState.nonAsciiMethod != -1)
			{
				if (num == 0 && Intersect(nfaState.next.epsilonMovesString, next.epsilonMovesString))
				{
					num = 1;
				}
				if (!P_1[nfaState.stateName] && !nfaState.isComposite && nonAsciiMethod == nfaState.nonAsciiMethod && kindToPrint == nfaState.kindToPrint && ((object)next.epsilonMovesString == nfaState.next.epsilonMovesString || (next.epsilonMovesString != null && nfaState.next.epsilonMovesString != null && string.Equals(next.epsilonMovesString, nfaState.next.epsilonMovesString))))
				{
					P_1[nfaState.stateName] = true;
					writer.WriteLine(("               case ")+(nfaState.stateName)+(":")
						);
				}
			}
		}
		string str;
		if (next == null || next.usefulEpsilonMoves <= 0)
		{
			str = (" && kind > ")+(kindToPrint);
			if (!Options.JavaUnicodeEscape && !UnicodeWarningGiven)
			{
				if (loByteVec != null && loByteVec.Count > 1)
				{
					writer.WriteLine(("                  if ((jjbitVec") + (((int)loByteVec[(1)]))+("[i2")
						+("] & l2) != 0L")
						+(str)
						+(")")
						);
				}
			}
			else
			{
				writer.WriteLine(("                  if (jjCanMove_")+(nonAsciiMethod)+("(hiByte, i1, i2, l1, l2)")
					+(str)
					+(")")
					);
			}
			writer.WriteLine(("                     kind = ")+(kindToPrint)+(";")
				);
			writer.WriteLine("                  break;");
			return;
		}
		str = "   ";
		if (kindToPrint != int.MaxValue)
		{
			if (!Options.JavaUnicodeEscape && !UnicodeWarningGiven)
			{
				if (loByteVec != null && loByteVec.Count > 1)
				{
					writer.WriteLine(("                  if ((jjbitVec") + (((int)loByteVec[(1)]))+("[i2")
						+("] & l2) == 0L)")
						);
					writer.WriteLine("                     break;");
				}
			}
			else
			{
				writer.WriteLine(("                  if (!jjCanMove_")+(nonAsciiMethod)+("(hiByte, i1, i2, l1, l2))")
					);
				writer.WriteLine("                     break;");
			}
			writer.WriteLine(("                  if (kind > ")+(kindToPrint)+(")")
				);
			writer.WriteLine(("                     kind = ")+(kindToPrint)+(";")
				);
			str = "";
		}
		else if (!Options.JavaUnicodeEscape && !UnicodeWarningGiven)
		{
			if (loByteVec != null && loByteVec.Count > 1)
			{
				writer.WriteLine(("                  if ((jjbitVec") + (((int)loByteVec[(1)]))+("[i2")
					+("] & l2) != 0L)")
					);
			}
		}
		else
		{
			writer.WriteLine(("                  if (jjCanMove_")+(nonAsciiMethod)+("(hiByte, i1, i2, l1, l2))")
				);
		}
		if (next != null && next.usefulEpsilonMoves > 0)
		{
			AllNextStates.TryGetValue(next.epsilonMovesString, out var array);
			if ( next.usefulEpsilonMoves == 1)
			{
				int i2 = array[0];
				if (num != 0)
				{
					writer.WriteLine((str)+("                  jjCheckNAdd(")+(i2)
						+(");")
						);
				}
				else
				{
					writer.WriteLine((str)+("                  jjstateSet[jjnewStateCnt++] = ")+(i2)
						+(";")
						);
				}
			}
			else if (next.usefulEpsilonMoves == 2 && num != 0)
			{
				writer.WriteLine((str)+("                  jjCheckNAddTwoStates(")+(array[0])
					+(", ")
					+(array[1])
					+(");")
					);
			}
			else
			{
				int[] stateSetIndicesForUse = GetStateSetIndicesForUse(next.epsilonMovesString);
				int num2 = ((stateSetIndicesForUse[0] + 1 != stateSetIndicesForUse[1]) ? 1 : 0);
				if (num != 0)
				{
					writer.Write((str)+("                  jjCheckNAddStates(")+(stateSetIndicesForUse[0])
						);
					if (num2 != 0)
					{
						writer.Write((", ")+(stateSetIndicesForUse[1]));
					}
					else
					{
						jjCheckNAddStatesUnaryNeeded = true;
					}
					writer.WriteLine(");");
				}
				else
				{
					writer.WriteLine((str)+("                  jjAddStates(")+(stateSetIndicesForUse[0])
						+(", ")
						+(stateSetIndicesForUse[1])
						+(");")
						);
				}
			}
		}
		writer.WriteLine("                  break;");
	}

	
	private static void DumpCompositeStatesNonAsciiMoves(TextWriter writer, string mid, bool[] visited)
	{
		AllNextStates.TryGetValue(mid, out var array);

		if (array.Length == 1 || visited[StateNameForComposite(mid)])
		{
			return;
		}
		NfaState nfaState = null;
		int num = 0;
		NfaState nfaState2 = null;
		string text = "";
		int num2 = ((StateBlockTable.ContainsKey(mid)) ? 1 : 0);
		for (int i = 0; i < array.Length; i++)
		{
			NfaState nfaState3 = (NfaState)AllStates[array[i]];
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
				visited[nfaState3.stateName] = true;
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
			text = nfaState2.PrintNoBreak(writer, -1, visited);
		}
		switch (num)
		{
		case 0:
			if (nfaState2 != null && string.Equals(text, ""))
			{
				writer.WriteLine("                  break;");
			}
			return;
		case 1:
			if (!string.Equals(text, ""))
			{
				writer.Write(text);
			}
			writer.WriteLine(("               case ")+(StateNameForComposite(mid))+(":")
				);
			if (!visited[nfaState.stateName] && num2 == 0 && nfaState.inNextOf > 1)
			{
				writer.WriteLine(("               case ")+(nfaState.stateName)+(":")
					);
			}
			visited[nfaState.stateName] = true;
			nfaState.DumpNonAsciiMove(writer, visited);
			return;
		}
		
		if (!string.Equals(text, ""))
		{
			writer.Write(text);
		}
		int num4 = StateNameForComposite(mid);
		writer.WriteLine(("               case ")+(num4)+(":")
			);
		if (num4 < GeneratedStates)
		{
			visited[num4] = true;
		}
		for (int i = 0; i < array.Length; i++)
		{
			NfaState nfaState3 = (NfaState)AllStates[array[i]];
			if (nfaState3.nonAsciiMethod != -1)
			{
				if (num2 != 0)
				{
					visited[nfaState3.stateName] = true;
				}
				nfaState3.DumpNonAsciiMoveForCompositeState(writer);
			}
		}
		if (num2 != 0)
		{
			writer.WriteLine("                  break;");
		}
		else
		{
			writer.WriteLine("                  break;");
		}
	}

	
	internal virtual void DumpNonAsciiMoveMethod(TextWriter writer)
	{
		writer.WriteLine(("private static final boolean jjCanMove_")+(nonAsciiMethod)+("(int hiByte, int i1, int i2, long l1, long l2)")
			);
		writer.WriteLine("{");
		writer.WriteLine("   switch(hiByte)");
		writer.WriteLine("   {");
		if (loByteVec != null && loByteVec.Count > 0)
		{
			for (int i = 0; i < loByteVec.Count; i += 2)
			{
				writer.WriteLine(("      case ")+(((int)loByteVec[i]))+(":")
					);
				if (!AllBitsSet((string)allBitVectors[(int)loByteVec[i+1]]))
				{
					writer.WriteLine(("         return ((jjbitVec")+(((int)loByteVec[i+1]))+("[i2")
						+("] & l2) != 0L);")
						);
				}
				else
				{
					writer.WriteLine("            return true;");
				}
			}
		}
		writer.WriteLine("      default : ");
		if (nonAsciiMoveIndices != null)
		{
			int intPtr = nonAsciiMoveIndices.Length;
			int i = (int)intPtr;
			if (intPtr > 0)
			{
				do
				{
					if (!AllBitsSet((string)allBitVectors[(nonAsciiMoveIndices[i - 2])]))
					{
						writer.WriteLine(("         if ((jjbitVec")+(nonAsciiMoveIndices[i - 2])+("[i1] & l1) != 0L)")
							);
					}
					if (!AllBitsSet((string)allBitVectors[(nonAsciiMoveIndices[i - 1])]))
					{
						writer.WriteLine(("            if ((jjbitVec")+(nonAsciiMoveIndices[i - 1])+("[i2] & l2) == 0L)")
							);
						writer.WriteLine("               return false;");
						writer.WriteLine("            else");
					}
					writer.WriteLine("            return true;");
					i += -2;
				}
				while (i > 0);
			}
		}
		writer.WriteLine("         return false;");
		writer.WriteLine("   }");
		writer.WriteLine("}");
	}

	
	private static void ReArrange()
	{
		var vector = AllStates;
		AllStates = new (Enumerable.Repeat<NfaState>(null,GeneratedStates));
		for (int i = 0; i < vector.Count; i++)
		{
			NfaState nfaState = (NfaState)vector[i];
			if (nfaState.stateName != -1 && !nfaState.dummy)
			{
                AllStates[nfaState.stateName] = (nfaState);
			}
		}
	}

	
	internal virtual void GenerateNonAsciiMoves(TextWriter P_0)
	{
		
		
		int num = 0;
		int[] array = new int[2];
		int num2 = (array[1] = 4);
		num2 = (array[0] = 256);
		long[][] array2 = new long[256][];
		for(int i = 0; i < array2.Length; i++)
        {
			array2[i] = new long[4];
        }
		if ((charMoves == null || charMoves[0] == '\0') && (rangeMoves == null || rangeMoves[0] == '\0'))
		{
			return;
		}
		if (charMoves != null)
		{
			for (int i = 0; i < charMoves.Length && charMoves[i] != 0; i++)
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
			for (int i = 0; i < rangeMoves.Length && rangeMoves[i] != 0; i += 2)
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
			string text = ("{\n   0x")+(Utils.ToHexString(array8[0]))+("L, ")
				+("0x")
				+(Utils.ToHexString(array8[1]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array8[2]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array8[3]))
				+("L\n};")
				;
			if (!LohiByteTab.TryGetValue(text, out var integer))
			{
				allBitVectors.Add(text);
				if (!AllBitsSet(text))
				{
					P_0.WriteLine(("static final long[] jjbitVec")+(LohiByteCnt)+(" = ")
						+(text)
						);
				}
				var dict = LohiByteTab;
				string key = text;
				
				dict.Add(key, integer = (LohiByteCnt++));
			}
			int[] array15 = tmpIndices;
			int num33 = num;
			num++;
			array15[num33] = integer;
			text = ("{\n   0x")+(Utils.ToHexString(array2[i][0]))+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][1]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][2]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][3]))
				+("L\n};")
				;
			if (!LohiByteTab.TryGetValue(text, out integer))
			{
				allBitVectors.Add(text);
				if (!AllBitsSet(text))
				{
					P_0.WriteLine(("static final long[] jjbitVec")+(LohiByteCnt)+(" = ")
						+(text)
						);
				}
				var dict2 = LohiByteTab;
				string key2 = text;
				
				dict2.Add(key2, integer = (LohiByteCnt++));
			}
			int[] array16 = tmpIndices;
			int num34 = num;
			num++;
			array16[num34] = integer;
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
			string text2 = ("{\n   0x")+(Utils.ToHexString(array2[i][0]))+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][1]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][2]))
				+("L, ")
				+("0x")
				+(Utils.ToHexString(array2[i][3]))
				+("L\n};")
				;
			if (!LohiByteTab.TryGetValue(text2,out var obj9))
			{
				allBitVectors.Add(text2);
				if (!AllBitsSet(text2))
				{
					P_0.WriteLine(("static final long[] jjbitVec")+(LohiByteCnt)+(" = ")
						+(text2)
						);
				}
				var dict3 = LohiByteTab;
				;
				dict3.Add(text2, obj9 = (LohiByteCnt++));
			}
			if (loByteVec == null)
			{
				loByteVec = new ();
			}
			loByteVec.Add((i));
			loByteVec.Add(obj9);
		}
		UpdateDuplicateNonAsciiMoves();
	}

	
	private static void FixStateSets()
	{
		var dict = new Dictionary<string, int[]>();
		int[] array = new int[GeneratedStates];
		foreach(var pair in StateSetsToFix)
		{
			string key = pair.Key;
			int[] array2 = pair.Value;
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
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
			dict.Add(key, array3);
			AllNextStates.Add(key, array3);
		}
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
			if (nfaState.next != null && nfaState.next.usefulEpsilonMoves != 0 && 
				dict.TryGetValue(nfaState.next.epsilonMovesString,out var array3))
			{
				nfaState.FixNextStates(array3);
			}
		}
	}

	
	private static void DumpAsciiMoves(TextWriter writer, int P_1)
	{
		bool[] array = new bool[Math.Max(GeneratedStates, DummyStateIndex + 1)];
		DumpHeadForCase(writer, P_1);

		foreach(var key in CompositeStateTable.Keys)
		{
			DumpCompositeStatesAsciiMoves(writer, key, P_1, array);
		}
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
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
				text = nfaState.stateForCase.PrintNoBreak(writer, P_1, array);
				if (nfaState.asciiMoves[P_1] == 0)
				{
					if (string.Equals(text, ""))
					{
						writer.WriteLine("                  break;");
					}
					continue;
				}
			}
			if (nfaState.asciiMoves[P_1] != 0)
			{
				if (!string.Equals(text, ""))
				{
					writer.Write(text);
				}
				array[nfaState.stateName] = true;
				writer.WriteLine(("               case ")+(nfaState.stateName)+(":")
					);
				nfaState.DumpAsciiMove(writer, P_1, array);
			}
		}
		writer.WriteLine("               default : break;");
		writer.WriteLine("            }");
		writer.WriteLine("         } while(i != startsAt);");
	}

	
	public static void DumpCharAndRangeMoves(TextWriter pw)
	{
		bool[] array = new bool[Math.Max(GeneratedStates, DummyStateIndex + 1)];
		DumpHeadForCase(pw, -1);
		foreach(var pair in CompositeStateTable)
		{
			DumpCompositeStatesNonAsciiMoves(pw, pair.Key, array);
		}
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = (NfaState)AllStates[i];
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
				pw.WriteLine(("               case ")+(nfaState.stateName)+(":")
					);
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
		for (int i = 0; i < statesForState.Length; i++)
		{
			if (statesForState[i] == null)
			{
				pw.WriteLine(" null, ");
				continue;
			}
			pw.WriteLine(" {");
			for (int j = 0; j < statesForState[i].Length; j++)
			{
				int[] array = statesForState[i][j];
				if (array == null)
				{
					pw.WriteLine(("   { ")+(j)+(" }, ")
						);
					continue;
				}
				pw.Write("   { ");
				for (int k = 0; k < array.Length; k++)
				{
					pw.Write((array[k])+(", "));
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
		AllNextStates.TryGetValue(text,out var array);
		for (int i = 0; i < array.Length; i++)
		{
			NfaState nfaState = (NfaState)IndexedAllStates[array[i]];
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

	
	public static int MoveFromSet(char ch, List<NfaState> v1, List<NfaState> v2)
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

	
	internal static string GetStateSetString(List<NfaState> status)
	{
		if (status == null || status.Count == 0)
		{
			return "null;";
		}
		int[] array = new int[status.Count];
		string str = "{ ";
		int num = 0;
		while (num < status.Count)
		{
			int num2;
			str = (str)+(num2 = ((NfaState)status[num]).stateName)+(", ")
				;
			array[num] = num2;
			int num3 = num;
			num++;
			if (num3 > 0)
			{
				int num4 = num;
				if (16 == -1 || num4 % 16 == 0)
				{
					str = (str)+("\n");
				}
			}
		}
		str = (str)+("};");
		AllNextStates.Add(str, array);
		return str;
	}

	
	private bool FindCommonBlocks()
	{
		if (next == null || next.usefulEpsilonMoves <= 1)
		{
			return false;
		}
		if (StateDone == null)
		{
			StateDone = new bool[GeneratedStates];
		}
		string key = next.epsilonMovesString;
		AllNextStates.TryGetValue(next.epsilonMovesString, out var array);

		if (array.Length <= 2 || CompositeStateTable.ContainsKey(key))
		{
			return false;
		}
		int[] array2 = new int[array.Length];
		bool[] array3 = new bool[array.Length];
		int[] array4 = new int[AllNextStates.Count];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != -1)
			{
				int num = i;
				int num2 = ((!StateDone[array[i]]) ? 1 : 0);
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
		int[] array7;
		int num8;
		foreach(var pair in AllNextStates)
		{
			array7 = pair.Value;
			if (array7 == array)
			{
				continue;
			}
			int num7 = 0;
			for (int j = 0; j < array.Length; j++)
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
			for (int j = 0; j < array.Length; j++)
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
		for (int i = 0; i < array.Length; i++)
		{
			if (array3[i])
			{
				if (((NfaState)IndexedAllStates[array[i]]).isComposite)
				{
					return false;
				}
				StateDone[array[i]] = true;
				int[] array8 = array7;
				int num9 = num8;
				num8++;
				array8[num9] = array[i];
			}
		}
		string stateSetString = GetStateSetString(array7);

		foreach(var pair in AllNextStates)
		{
			int num10 = 1;
			string key2 = pair.Key;
			int[] array9 = pair.Value;
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
				if (!StateSetsToFix.ContainsKey(key2))
				{
					StateSetsToFix.Add(key2, array9);
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
		AllNextStates.TryGetValue(key, out var array);
		if (array.Length == 1 || CompositeStateTable.ContainsKey(key) || StateSetsToFix.ContainsKey(key) )
		{
			return false;
		}
		var dict = new Dictionary<string, int[]>();
		var nfaState = AllStates[array[0]];
		for (int i = 1; i < array.Length; i++)
		{
			NfaState nfaState2 = (NfaState)AllStates[array[i]];
			if (nfaState.inNextOf != nfaState2.inNextOf)
			{
				return false;
			}
		}
		foreach(var pair in AllNextStates)
		{
			string key2 = pair.Key;
			int[] array2 = pair.Value;
			if (array2 == array)
			{
				continue;
			}
			int num = 0;
			int j;
			for (j = 0; j < array.Length; j++)
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
				if (array2.Length > array.Length)
				{
					dict.Add(key2, array2);
				}
				if (CompositeStateTable.ContainsKey(key2) || StateSetsToFix.ContainsKey(key2))
				{
					return false;
				}
			}
			else if (num != 0)
			{
				return false; 
			}
		}
		foreach(var pair in dict)
		{
			string key2 = pair.Key;
			int[] array2 = pair.Value;
			if (!StateSetsToFix.ContainsKey(key2) )
			{
				StateSetsToFix.Add(key2, array2);
			}
			for (int k = 0; k < array2.Length; k++)
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
		Dictionary<string,string> dict = new ();
		bool[] array = new bool[GeneratedStates];
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < AllStates.Count; i++)
		{
			NfaState nfaState = null;
			NfaState nfaState2 = (NfaState)AllStates[i];
			if (nfaState2.stateName == -1 || nfaState2.dummy || !nfaState2.UsefulState() || nfaState2.next == null || nfaState2.next.usefulEpsilonMoves < 1)
			{
				continue;
			}
			string text = nfaState2.next.epsilonMovesString;
			if (CompositeStateTable.ContainsKey(text) || dict.ContainsKey(text))
			{
				continue;
			}
			dict.Add(text, text);
			
			if (AllNextStates.TryGetValue(text, out var array2) && array2.Length == 1)
			{
				continue;
			}
			int j;
			for (j = 0; j < array2.Length; j++)
			{
				int num3;
				if ((num3 = array2[j]) == -1)
				{
					continue;
				}
				NfaState nfaState3 = (NfaState)AllStates[num3];
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
				if (j < array2.Length)
				{
					int num3;
					if ((num3 = array2[j]) != -1)
					{
						NfaState nfaState3 = (NfaState)AllStates[num3];
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
							StateSetsToFix.Add(text, array2);
							break;
						}
					}
					j++;
					continue;
				}
				for (j = 0; j < array2.Length; j++)
				{
					int num3;
					if ((num3 = array2[j]) != -1)
					{
						NfaState nfaState3 = (NfaState)AllStates[num3];
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
		UnicodeWarningGiven = false;
		GeneratedStates = 0;
		IdCnt = 0;
		DummyStateIndex = -1;
		AllStates = new ();
		IndexedAllStates = new ();
		NonAsciiTableForMethod = new ();
		EquivStatesTable = new ();
		AllNextStates = new ();
		LohiByteTab = new ();
		_StateNameForComposite = new ();
		CompositeStateTable = new ();
		StateBlockTable = new ();
		StateSetsToFix = new ();
		jjCheckNAddStatesUnaryNeeded = false;
		allBitVectors = new ();
		tmpIndices = new int[512];
		allBits = "{\n   0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL, 0xffffffffffffffffL\n};";
		tableToDump = new ();
		orderedStateSet = new ();
		lastIndex = 0;
	}
}
