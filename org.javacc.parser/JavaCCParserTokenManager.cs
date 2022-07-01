using System;
using System.IO;
using System.Text;
namespace org.javacc.parser;

public class JavaCCParserTokenManager : JavaCCParserConstants
{
	internal int[] beginLine;

	internal int[] beginCol;

	internal int depth;

	internal int size;

	public TextWriter debugStream;


	internal static long[] jjbitVec0;


	internal static long[] jjbitVec2;


	internal static long[] jjbitVec3;


	internal static long[] jjbitVec4;


	internal static long[] jjbitVec5;


	internal static long[] jjbitVec6;


	internal static long[] jjbitVec7;


	internal static long[] jjbitVec8;


	internal static long[] jjbitVec9;


	internal static long[] jjbitVec10;


	internal static long[] jjbitVec11;


	internal static long[] jjbitVec12;


	internal static long[] jjbitVec13;


	internal static long[] jjbitVec14;


	internal static long[] jjbitVec15;


	internal static long[] jjbitVec16;


	internal static long[] jjbitVec17;


	internal static long[] jjbitVec18;


	internal static long[] jjbitVec19;


	internal static long[] jjbitVec20;


	internal static long[] jjbitVec21;


	internal static long[] jjbitVec22;


	internal static long[] jjbitVec23;


	internal static long[] jjbitVec24;


	internal static long[] jjbitVec25;


	internal static long[] jjbitVec26;


	internal static long[] jjbitVec27;


	internal static long[] jjbitVec28;


	internal static long[] jjbitVec29;


	internal static long[] jjbitVec30;


	internal static long[] jjbitVec31;


	internal static long[] jjbitVec32;


	internal static long[] jjbitVec33;


	internal static long[] jjbitVec34;


	internal static long[] jjbitVec35;


	internal static long[] jjbitVec36;


	internal static long[] jjbitVec37;


	internal static long[] jjbitVec38;


	internal static long[] jjbitVec39;


	internal static long[] jjbitVec40;


	internal static long[] jjbitVec41;


	internal static long[] jjbitVec42;


	internal static long[] jjbitVec43;


	internal static long[] jjbitVec44;


	internal static long[] jjbitVec45;


	internal static long[] jjbitVec46;


	internal static long[] jjbitVec47;


	internal static long[] jjbitVec48;


	internal static long[] jjbitVec49;


	internal static long[] jjbitVec50;


	internal static long[] jjbitVec51;


	internal static long[] jjbitVec52;


	internal static long[] jjbitVec53;


	internal static long[] jjbitVec54;


	internal static long[] jjbitVec55;


	internal static long[] jjbitVec56;


	internal static long[] jjbitVec57;


	internal static long[] jjbitVec58;


	internal static long[] jjbitVec59;


	internal static long[] jjbitVec60;


	internal static long[] jjbitVec61;


	internal static int[] jjnextStates;

	internal static string[] ___003C_003EjjstrLiteralImages;

	internal static string[] ___003C_003ElexStateNames;

	internal static int[] ___003C_003EjjnewLexState;


	internal static long[] jjtoToken;


	internal static long[] jjtoSkip;


	internal static long[] jjtoSpecial;


	internal static long[] jjtoMore;

	protected internal JavaCharStream input_stream;


	private int[] jjrounds;


	private int[] jjstateSet;

	internal StringBuilder image;

	internal int jjimageLen;

	internal int lengthOfMatch;

	protected internal char curChar;

	internal int curLexState;

	internal int defaultLexState;

	internal int jjnewStateCnt;

	internal int jjround;

	internal int jjmatchedPos;

	internal int jjmatchedKind;


	public static string[] jjstrLiteralImages
	{
		
		get
		{
			return ___003C_003EjjstrLiteralImages;
		}
	}


	public static string[] lexStateNames
	{
		
		get
		{
			return ___003C_003ElexStateNames;
		}
	}


	public static int[] jjnewLexState
	{
		
		get
		{
			return ___003C_003EjjnewLexState;
		}
	}

	
	
	public static void ___003Cclinit_003E()
	{
	}

	
	public JavaCCParserTokenManager(JavaCharStream jcs)
	{
		beginLine = new int[10];
		beginCol = new int[10];
		depth = 0;
		size = 10;
		debugStream = Console.Out;
		jjrounds = new int[65];
		jjstateSet = new int[130];
		curLexState = 0;
		defaultLexState = 0;
		input_stream = jcs;
	}

	
	public virtual void ReInit(JavaCharStream jcs)
	{
		int num = 0;
		jjnewStateCnt = num;
		jjmatchedPos = num;
		curLexState = defaultLexState;
		input_stream = jcs;
		ReInitRounds();
	}

	
	public virtual Token getNextToken()
	{
		Token token = null;
		int num = 0;
		Token token2;
		while (true)
		{
			try
			{
				curChar = input_stream.BeginToken();
			}
			catch (IOException)
			{
				break;
			}
			image = null;
			jjimageLen = 0;
			while (true)
			{
				int num2;
				string text;
				int num3;
				int num4;
				int b;
				int i;
				int i2;
				int i3;
				string str;
				char ch;
				switch (curLexState)
				{
				case 0:
					try
					{
						input_stream.backup(0);
						while (curChar <= ' ' && (0x100003600L & (1L << (int)curChar)) != 0)
						{
							curChar = input_stream.BeginToken();
						}
					}
					catch (IOException)
					{
						goto IL_00b5;
					}
					jjmatchedKind = int.MaxValue;
					jjmatchedPos = 0;
					num = jjMoveStringLiteralDfa0_0();
					goto default;
				case 1:
					jjmatchedKind = int.MaxValue;
					jjmatchedPos = 0;
					num = jjMoveStringLiteralDfa0_1();
					if (jjmatchedPos == 0 && jjmatchedKind > 18)
					{
						jjmatchedKind = 18;
					}
					goto default;
				case 2:
					jjmatchedKind = int.MaxValue;
					jjmatchedPos = 0;
					num = jjMoveStringLiteralDfa0_2();
					if (jjmatchedPos == 0 && jjmatchedKind > 26)
					{
						jjmatchedKind = 26;
					}
					goto default;
				case 3:
					jjmatchedKind = int.MaxValue;
					jjmatchedPos = 0;
					num = jjMoveStringLiteralDfa0_3();
					if (jjmatchedPos == 0 && jjmatchedKind > 26)
					{
						jjmatchedKind = 26;
					}
					goto default;
				case 4:
					jjmatchedKind = int.MaxValue;
					jjmatchedPos = 0;
					num = jjMoveStringLiteralDfa0_4();
					if (jjmatchedPos == 0 && jjmatchedKind > 26)
					{
						jjmatchedKind = 26;
					}
					goto default;
				default:
					{
						if (jjmatchedKind != int.MaxValue)
						{
							if (jjmatchedPos + 1 < num)
							{
								input_stream.backup(num - jjmatchedPos - 1);
							}
							if ((jjtoToken[jjmatchedKind >> 6] & (1L << jjmatchedKind)) != 0)
							{
								token2 = jjFillToken();
								token2.specialToken = token;
								TokenLexicalActions(token2);
								if (___003C_003EjjnewLexState[jjmatchedKind] != -1)
								{
									curLexState = ___003C_003EjjnewLexState[jjmatchedKind];
								}
								return token2;
							}
							if ((jjtoSkip[jjmatchedKind >> 6] & (1L << jjmatchedKind)) != 0)
							{
								if ((jjtoSpecial[jjmatchedKind >> 6] & (1L << jjmatchedKind)) != 0)
								{
									token2 = jjFillToken();
									if (token == null)
									{
										token = token2;
									}
									else
									{
										token2.specialToken = token;
										Token token3 = token;
										Token token4 = token2;
										Token token5 = token3;
										token5.next = token4;
										token = token4;
									}
									SkipLexicalActions(token2);
								}
								else
								{
									SkipLexicalActions(null);
								}
								if (___003C_003EjjnewLexState[jjmatchedKind] != -1)
								{
									curLexState = ___003C_003EjjnewLexState[jjmatchedKind];
								}
								break;
							}
							MoreLexicalActions();
							if (___003C_003EjjnewLexState[jjmatchedKind] != -1)
							{
								curLexState = ___003C_003EjjnewLexState[jjmatchedKind];
							}
							num = 0;
							jjmatchedKind = int.MaxValue;
							try
							{
								curChar = input_stream.readChar();
							}
							catch (IOException)
							{
								goto IL_033d;
							}
							continue;
						}
						goto IL_0346;
					}
					IL_0386:
					
					num2 = 1;
					text = ((num > 1) ? input_stream.GetImage() : "");
					if (curChar == '\n' || curChar == '\r')
					{
						num3++;
						num4 = 0;
					}
					else
					{
						num4++;
					}
					goto IL_03cc;
					IL_03cc:
					if (num2 == 0)
					{
						input_stream.backup(1);
						text = ((num > 1) ? input_stream.GetImage() : "");
					}
					b = num2;
					i = curLexState;
					i2 = num3;
					i3 = num4;
					str = text;
					ch = curChar;
					
					throw new TokenMgrError((byte)b != 0, i, i2, i3, str, ch, 0);
					IL_0346:
					num3 = input_stream.getEndLine();
					num4 = input_stream.getEndColumn();
					text = null;
					num2 = 0;
					try
					{
						input_stream.readChar();
						input_stream.backup(1);
					}
					catch (IOException)
					{
						goto IL_0386;
					}
					goto IL_03cc;
					IL_00b5:
					
					break;
					IL_033d:
					
					goto IL_0346;
				}
				break;
			}
		}
		
		jjmatchedKind = 0;
		token2 = jjFillToken();
		token2.specialToken = token;
		return token2;
	}


	private int jjStopStringLiteralDfa_0(int P_0, long P_1, long P_2, long P_3)
	{
		switch (P_0)
		{
		case 0:
			if ((P_1 & 0x6A0000u) != 0 || (P_2 & 0x2020000000000000L) != 0)
			{
				return 2;
			}
			if ((P_2 & 0x800000000L) != 0 || (P_3 & 0x40u) != 0)
			{
				return 8;
			}
			if ((P_1 & -134213634) != 0 || (P_2 & 0xFFFFu) != 0)
			{
				jjmatchedKind = 140;
				return 32;
			}
			return -1;
		case 1:
			if ((P_1 & 0x620000u) != 0)
			{
				return 0;
			}
			if ((P_1 & -1127549308497922L) != 0 || (P_2 & 0xFFFFu) != 0)
			{
				if (jjmatchedPos != 1)
				{
					jjmatchedKind = 140;
					jjmatchedPos = 1;
				}
				return 32;
			}
			if ((P_1 & 0x4018000000000L) != 0)
			{
				return 32;
			}
			return -1;
		case 2:
			if ((P_1 & -343681496453740546L) != 0 || (P_2 & 0xEFFFu) != 0)
			{
				if (jjmatchedPos != 2)
				{
					jjmatchedKind = 140;
					jjmatchedPos = 2;
				}
				return 32;
			}
			if ((P_1 & 0x4C1000000000800L) != 0 || (P_2 & 0x1000u) != 0)
			{
				return 32;
			}
			return -1;
		case 3:
			if ((P_1 & -956740616421636866L) != 0 || (P_2 & 0xC77Fu) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 3;
				return 32;
			}
			if ((P_1 & 0x902060580000300L) != 0 || (P_2 & 0x2880u) != 0)
			{
				return 32;
			}
			return -1;
		case 4:
			if ((P_1 & -957004611955195714L) != 0 || (P_2 & 0x446Du) != 0)
			{
				if (jjmatchedPos != 4)
				{
					jjmatchedKind = 140;
					jjmatchedPos = 4;
				}
				return 32;
			}
			if ((P_1 & 0xF01A40000440L) != 0 || (P_2 & 0x8312u) != 0)
			{
				return 32;
			}
			return -1;
		case 5:
			if ((P_1 & 0x70A84860280004BEL) != 0 || (P_2 & 0x4448u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 5;
				return 32;
			}
			if ((P_1 & -9074752149371486208L) != 0 || (P_2 & 0x225u) != 0)
			{
				return 32;
			}
			return -1;
		case 6:
			if ((P_1 & 0x40A80020080004BEL) != 0 || (P_2 & 0x4448u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 6;
				return 32;
			}
			if ((P_1 & 0x3000484020000000L) != 0)
			{
				return 32;
			}
			return -1;
		case 7:
			if ((P_1 & 0x40A800000000049EL) != 0 || (P_2 & 0x440u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 7;
				return 32;
			}
			if ((P_1 & 0x2008000020L) != 0 || (P_2 & 0x4008u) != 0)
			{
				return 32;
			}
			return -1;
		case 8:
			if ((P_1 & 0x2800000000049CL) != 0 || (P_2 & 0x40u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 8;
				return 32;
			}
			if ((P_1 & 0x4080000000000002L) != 0 || (P_2 & 0x400u) != 0)
			{
				return 32;
			}
			return -1;
		case 9:
			if ((P_1 & 0x48Cu) != 0 || (P_2 & 0x40u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 9;
				return 32;
			}
			if ((P_1 & 0x28000000000010L) != 0)
			{
				return 32;
			}
			return -1;
		case 10:
			if ((P_1 & 0x488u) != 0 || (P_2 & 0x40u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 10;
				return 32;
			}
			if ((P_1 & 4) != 0)
			{
				return 32;
			}
			return -1;
		case 11:
			if ((P_1 & 0x480u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 11;
				return 32;
			}
			if ((P_1 & 8) != 0 || (P_2 & 0x40u) != 0)
			{
				return 32;
			}
			return -1;
		case 12:
			if ((P_1 & 0x400u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 12;
				return 32;
			}
			if ((P_1 & 0x80u) != 0)
			{
				return 32;
			}
			return -1;
		case 13:
			if ((P_1 & 0x400u) != 0)
			{
				jjmatchedKind = 140;
				jjmatchedPos = 13;
				return 32;
			}
			return -1;
		default:
			return -1;
		}
	}

	
	private int jjMoveNfa_0(int P_0, int P_1)
	{
		int num = 0;
		jjnewStateCnt = 65;
		int num2 = 1;
		jjstateSet[0] = P_0;
		int num3 = int.MaxValue;
		while (true)
		{
			int num4 = jjround + 1;
			JavaCCParserTokenManager javaCCParserTokenManager = this;
			int num5 = num4;
			javaCCParserTokenManager.jjround = num4;
			if (num5 == int.MaxValue)
			{
				ReInitRounds();
			}
			if (curChar < '@')
			{
				long num6 = 1L << (int)curChar;
				do
				{
					int[] array = jjstateSet;
					num2 += -1;
					switch (array[num2])
					{
					case 3:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(0, 6);
						}
						else if (curChar == '$')
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						else if (curChar == '"')
						{
							jjCheckNAddStates(7, 9);
						}
						else if (curChar == '\'')
						{
							jjAddStates(10, 11);
						}
						else if (curChar == '.')
						{
							jjCheckNAdd(8);
						}
						else if (curChar == '/')
						{
							int[] array3 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num8 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array3[num8] = 2;
						}
						if ((0x3FE000000000000L & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(5, 6);
						}
						else if (curChar == '0')
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddStates(12, 16);
						}
						break;
					case 0:
						if (curChar == '*')
						{
							int[] array5 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num10 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array5[num10] = 1;
						}
						break;
					case 1:
						if ((-140737488355329L & num6) != 0 && num3 > 20)
						{
							num3 = 20;
						}
						break;
					case 2:
						if (curChar == '*')
						{
							int[] array6 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num11 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array6[num11] = 0;
						}
						break;
					case 4:
						if ((0x3FE000000000000L & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(5, 6);
						}
						break;
					case 5:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(5, 6);
						}
						break;
					case 7:
						if (curChar == '.')
						{
							jjCheckNAdd(8);
						}
						break;
					case 8:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddStates(17, 19);
						}
						break;
					case 10:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(11);
						}
						break;
					case 11:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddTwoStates(11, 12);
						}
						break;
					case 13:
						if (curChar == '\'')
						{
							jjAddStates(10, 11);
						}
						break;
					case 14:
						if ((-549755823105L & num6) != 0)
						{
							jjCheckNAdd(15);
						}
						break;
					case 15:
						if (curChar == '\'' && num3 > 89)
						{
							num3 = 89;
						}
						break;
					case 17:
						if ((0x8400000000L & num6) != 0)
						{
							jjCheckNAdd(15);
						}
						break;
					case 18:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(19, 15);
						}
						break;
					case 19:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAdd(15);
						}
						break;
					case 20:
						if ((0xF000000000000L & num6) != 0)
						{
							int[] array2 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num7 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array2[num7] = 21;
						}
						break;
					case 21:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAdd(19);
						}
						break;
					case 22:
						if (curChar == '"')
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 23:
						if ((-17179878401L & num6) != 0)
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 25:
						if ((0x8400000000L & num6) != 0)
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 26:
						if (curChar == '"' && num3 > 90)
						{
							num3 = 90;
						}
						break;
					case 27:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(20, 23);
						}
						break;
					case 28:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 29:
						if ((0xF000000000000L & num6) != 0)
						{
							int[] array4 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num9 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array4[num9] = 30;
						}
						break;
					case 30:
						if ((0xFF000000000000L & num6) != 0)
						{
							jjCheckNAdd(28);
						}
						break;
					case 31:
						if (curChar == '$')
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					case 32:
						if ((0x3FF00100FFFC1FFL & num6) != 0)
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					case 33:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(0, 6);
						}
						break;
					case 34:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(24, 26);
						}
						break;
					case 36:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(37);
						}
						break;
					case 37:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(37, 12);
						}
						break;
					case 38:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(38, 39);
						}
						break;
					case 40:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(41);
						}
						break;
					case 41:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddTwoStates(41, 12);
						}
						break;
					case 42:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(42, 43);
						}
						break;
					case 43:
						if (curChar == '.')
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddStates(27, 29);
						}
						break;
					case 44:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddStates(27, 29);
						}
						break;
					case 46:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(47);
						}
						break;
					case 47:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddTwoStates(47, 12);
						}
						break;
					case 48:
						if (curChar == '0')
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddStates(12, 16);
						}
						break;
					case 50:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(50, 6);
						}
						break;
					case 51:
						if ((0xFF000000000000L & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(51, 6);
						}
						break;
					case 53:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjAddStates(30, 31);
						}
						break;
					case 54:
						if (curChar == '.')
						{
							jjCheckNAdd(55);
						}
						break;
					case 55:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(55, 56);
						}
						break;
					case 57:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(58);
						}
						break;
					case 58:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddTwoStates(58, 12);
						}
						break;
					case 60:
						if ((0x3FF000000000000L & num6) != 0)
						{
							jjCheckNAddStates(32, 34);
						}
						break;
					case 61:
						if (curChar == '.')
						{
							jjCheckNAdd(62);
						}
						break;
					case 63:
						if ((0x280000000000L & num6) != 0)
						{
							jjCheckNAdd(64);
						}
						break;
					case 64:
						if ((0x3FF000000000000L & num6) != 0)
						{
							if (num3 > 84)
							{
								num3 = 84;
							}
							jjCheckNAddTwoStates(64, 12);
						}
						break;
					}
				}
				while (num2 != num);
			}
			else if (curChar < '\u0080')
			{
				long num6 = 1L << (curChar & 0x3F);
				do
				{
					int[] array7 = jjstateSet;
					num2 += -1;
					switch (array7[num2])
					{
					case 3:
						if ((0x7FFFFFE87FFFFFEL & num6) != 0)
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					case 1:
						if (num3 > 20)
						{
							num3 = 20;
						}
						break;
					case 6:
						if ((0x100000001000L & num6) != 0 && num3 > 80)
						{
							num3 = 80;
						}
						break;
					case 9:
						if ((0x2000000020L & num6) != 0)
						{
							jjAddStates(35, 36);
						}
						break;
					case 12:
						if ((0x5000000050L & num6) != 0 && num3 > 84)
						{
							num3 = 84;
						}
						break;
					case 14:
						if ((-268435457 & num6) != 0)
						{
							jjCheckNAdd(15);
						}
						break;
					case 16:
						if (curChar == '\\')
						{
							jjAddStates(37, 39);
						}
						break;
					case 17:
						if ((0x14404410000000L & num6) != 0)
						{
							jjCheckNAdd(15);
						}
						break;
					case 23:
						if ((-268435457 & num6) != 0)
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 24:
						if (curChar == '\\')
						{
							jjAddStates(40, 42);
						}
						break;
					case 25:
						if ((0x14404410000000L & num6) != 0)
						{
							jjCheckNAddStates(7, 9);
						}
						break;
					case 32:
						if ((-8646911290859585538L & num6) != 0)
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					case 35:
						if ((0x2000000020L & num6) != 0)
						{
							jjAddStates(43, 44);
						}
						break;
					case 39:
						if ((0x2000000020L & num6) != 0)
						{
							jjAddStates(45, 46);
						}
						break;
					case 45:
						if ((0x2000000020L & num6) != 0)
						{
							jjAddStates(47, 48);
						}
						break;
					case 49:
						if ((0x100000001000000L & num6) != 0)
						{
							jjCheckNAdd(50);
						}
						break;
					case 50:
						if ((0x7E0000007EL & num6) != 0)
						{
							if (num3 > 80)
							{
								num3 = 80;
							}
							jjCheckNAddTwoStates(50, 6);
						}
						break;
					case 52:
						if ((0x100000001000000L & num6) != 0)
						{
							jjCheckNAddTwoStates(53, 54);
						}
						break;
					case 53:
						if ((0x7E0000007EL & num6) != 0)
						{
							jjCheckNAddTwoStates(53, 54);
						}
						break;
					case 55:
						if ((0x7E0000007EL & num6) != 0)
						{
							jjAddStates(49, 50);
						}
						break;
					case 56:
						if ((0x1000000010000L & num6) != 0)
						{
							jjAddStates(51, 52);
						}
						break;
					case 59:
						if ((0x100000001000000L & num6) != 0)
						{
							jjCheckNAdd(60);
						}
						break;
					case 60:
						if ((0x7E0000007EL & num6) != 0)
						{
							jjCheckNAddStates(32, 34);
						}
						break;
					case 62:
						if ((0x1000000010000L & num6) != 0)
						{
							jjAddStates(53, 54);
						}
						break;
					}
				}
				while (num2 != num);
			}
			else
			{
				int num12 = (int)curChar >> 8;
				int num13 = num12 >> 6;
				long num14 = 1L << (num12 & 0x3F);
				int num15 = (curChar & 0xFF) >> 6;
				long num16 = 1L << (curChar & 0x3F);
				do
				{
					int[] array8 = jjstateSet;
					num2 += -1;
					switch (array8[num2])
					{
					case 3:
						if (jjCanMove_1(num12, num13, num15, num14, num16))
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					case 1:
						if (jjCanMove_0(num12, num13, num15, num14, num16) && num3 > 20)
						{
							num3 = 20;
						}
						break;
					case 14:
						if (jjCanMove_0(num12, num13, num15, num14, num16))
						{
							int[] array9 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num17 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array9[num17] = 15;
						}
						break;
					case 23:
						if (jjCanMove_0(num12, num13, num15, num14, num16))
						{
							jjAddStates(7, 9);
						}
						break;
					case 32:
						if (jjCanMove_2(num12, num13, num15, num14, num16))
						{
							if (num3 > 140)
							{
								num3 = 140;
							}
							jjCheckNAdd(32);
						}
						break;
					}
				}
				while (num2 != num);
			}
			if (num3 != int.MaxValue)
			{
				jjmatchedKind = num3;
				jjmatchedPos = P_1;
				num3 = int.MaxValue;
			}
			P_1++;
			int num18 = (num2 = jjnewStateCnt);
			num4 = num;
			int num19 = num4;
			jjnewStateCnt = num4;
			if (num18 == (num = 65 - num19))
			{
				return P_1;
			}
			try
			{
				curChar = input_stream.readChar();
			}
			catch (IOException)
			{
				break;
			}
		}
		
		return P_1;
	}

	
	private int jjMoveStringLiteralDfa1_0(long P_0, long P_1, long P_2)
	{
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0019;
		}
		switch (curChar)
		{
		case '&':
			if ((P_1 & 0x800000000000L) != 0)
			{
				return jjStopAtPos(1, 111);
			}
			break;
		case '*':
			if ((P_0 & 0x200000u) != 0)
			{
				jjmatchedKind = 21;
				jjmatchedPos = 1;
			}
			return jjMoveStringLiteralDfa2_0(P_0, 4325376L, P_1, 0L, P_2, 0L);
		case '+':
			if ((P_1 & 0x1000000000000L) != 0)
			{
				return jjStopAtPos(1, 112);
			}
			break;
		case '-':
			if ((P_1 & 0x2000000000000L) != 0)
			{
				return jjStopAtPos(1, 113);
			}
			break;
		case '.':
			return jjMoveStringLiteralDfa2_0(P_0, 0L, P_1, 0L, P_2, 64L);
		case '/':
			if ((P_0 & 0x80000u) != 0)
			{
				return jjStopAtPos(1, 19);
			}
			break;
		case '<':
			if ((P_2 & 0x400u) != 0)
			{
				jjmatchedKind = 138;
				jjmatchedPos = 1;
			}
			return jjMoveStringLiteralDfa2_0(P_0, 0L, P_1, 0L, P_2, 128L);
		case '=':
			if ((P_1 & 0x40000000000L) != 0)
			{
				return jjStopAtPos(1, 106);
			}
			if ((P_1 & 0x80000000000L) != 0)
			{
				return jjStopAtPos(1, 107);
			}
			if ((P_1 & 0x100000000000L) != 0)
			{
				return jjStopAtPos(1, 108);
			}
			if ((P_1 & 0x200000000000L) != 0)
			{
				return jjStopAtPos(1, 109);
			}
			if ((P_1 & 0x400000000000000L) != 0)
			{
				return jjStopAtPos(1, 122);
			}
			if ((P_1 & 0x800000000000000L) != 0)
			{
				return jjStopAtPos(1, 123);
			}
			if ((P_1 & 0x1000000000000000L) != 0)
			{
				return jjStopAtPos(1, 124);
			}
			if ((P_1 & 0x2000000000000000L) != 0)
			{
				return jjStopAtPos(1, 125);
			}
			if ((P_1 & 0x4000000000000000L) != 0)
			{
				return jjStopAtPos(1, 126);
			}
			if ((P_1 & long.MinValue) != 0)
			{
				return jjStopAtPos(1, 127);
			}
			if ((P_2 & 1) != 0)
			{
				return jjStopAtPos(1, 128);
			}
			if ((P_2 & 2) != 0)
			{
				return jjStopAtPos(1, 129);
			}
			break;
		case '>':
			if ((P_2 & 8) != 0)
			{
				jjmatchedKind = 131;
				jjmatchedPos = 1;
			}
			return jjMoveStringLiteralDfa2_0(P_0, 0L, P_1, 0L, P_2, 772L);
		case 'A':
			return jjMoveStringLiteralDfa2_0(P_0, 56L, P_1, 0L, P_2, 0L);
		case 'G':
			return jjMoveStringLiteralDfa2_0(P_0, 4L, P_1, 0L, P_2, 0L);
		case 'K':
			return jjMoveStringLiteralDfa2_0(P_0, 512L, P_1, 0L, P_2, 0L);
		case 'O':
			return jjMoveStringLiteralDfa2_0(P_0, 3394L, P_1, 0L, P_2, 0L);
		case 'P':
			return jjMoveStringLiteralDfa2_0(P_0, 128L, P_1, 0L, P_2, 0L);
		case 'a':
			return jjMoveStringLiteralDfa2_0(P_0, 1297054297753649152L, P_1, 0L, P_2, 0L);
		case 'b':
			return jjMoveStringLiteralDfa2_0(P_0, 134217728L, P_1, 0L, P_2, 0L);
		case 'e':
			return jjMoveStringLiteralDfa2_0(P_0, 288230651029618688L, P_1, 1L, P_2, 0L);
		case 'f':
			if ((P_0 & 0x4000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(1, 50, 32);
			}
			break;
		case 'h':
			return jjMoveStringLiteralDfa2_0(P_0, 17179869184L, P_1, 33666L, P_2, 0L);
		case 'i':
			return jjMoveStringLiteralDfa2_0(P_0, 105553116266496L, P_1, 0L, P_2, 0L);
		case 'l':
			return jjMoveStringLiteralDfa2_0(P_0, 142970871349248L, P_1, 0L, P_2, 0L);
		case 'm':
			return jjMoveStringLiteralDfa2_0(P_0, 6755399441055744L, P_1, 0L, P_2, 0L);
		case 'n':
			return jjMoveStringLiteralDfa2_0(P_0, 63054792829698048L, P_1, 0L, P_2, 0L);
		case 'o':
			if ((P_0 & 0x8000000000L) != 0)
			{
				jjmatchedKind = 39;
				jjmatchedPos = 1;
			}
			return jjMoveStringLiteralDfa2_0(P_0, 72903325174988800L, P_1, 24576L, P_2, 0L);
		case 'r':
			return jjMoveStringLiteralDfa2_0(P_0, 6917529028714823680L, P_1, 7168L, P_2, 0L);
		case 's':
			return jjMoveStringLiteralDfa2_0(P_0, 268435456L, P_1, 0L, P_2, 0L);
		case 't':
			return jjMoveStringLiteralDfa2_0(P_0, 0L, P_1, 12L, P_2, 0L);
		case 'u':
			return jjMoveStringLiteralDfa2_0(P_0, -8646911284551352320L, P_1, 16L, P_2, 0L);
		case 'w':
			return jjMoveStringLiteralDfa2_0(P_0, 0L, P_1, 32L, P_2, 0L);
		case 'x':
			return jjMoveStringLiteralDfa2_0(P_0, 8796093022208L, P_1, 0L, P_2, 0L);
		case 'y':
			return jjMoveStringLiteralDfa2_0(P_0, 2147483648L, P_1, 64L, P_2, 0L);
		case '|':
			if ((P_1 & 0x400000000000L) != 0)
			{
				return jjStopAtPos(1, 110);
			}
			break;
		}
		return jjStartNfa_0(0, P_0, P_1, P_2);
		IL_0019:
		
		jjStopStringLiteralDfa_0(0, P_0, P_1, P_2);
		return 1;
	}

	private int jjStopAtPos(int P_0, int P_1)
	{
		jjmatchedKind = P_1;
		jjmatchedPos = P_0;
		return P_0 + 1;
	}

	
	private int jjMoveStringLiteralDfa2_0(long P_0, long P_1, long P_2, long P_3, long P_4, long P_5)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2) | (P_5 &= P_4)) == 0)
		{
			return jjStartNfa_0(0, P_0, P_2, P_4);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0040;
		}
		switch (curChar)
		{
		case '.':
			if ((P_5 & 0x40u) != 0)
			{
				return jjStopAtPos(2, 134);
			}
			break;
		case '=':
			if ((P_5 & 0x80u) != 0)
			{
				return jjStopAtPos(2, 135);
			}
			if ((P_5 & 0x100u) != 0)
			{
				return jjStopAtPos(2, 136);
			}
			break;
		case '>':
			if ((P_5 & 4) != 0)
			{
				jjmatchedKind = 130;
				jjmatchedPos = 2;
			}
			return jjMoveStringLiteralDfa3_0(P_1, 0L, P_3, 0L, P_5, 512L);
		case '@':
			return jjMoveStringLiteralDfa3_0(P_1, 4325376L, P_3, 0L, P_5, 0L);
		case 'E':
			return jjMoveStringLiteralDfa3_0(P_1, 128L, P_3, 0L, P_5, 0L);
		case 'F':
			if ((P_1 & 0x800u) != 0)
			{
				return jjStartNfaWithStates_0(2, 11, 32);
			}
			break;
		case 'I':
			return jjMoveStringLiteralDfa3_0(P_1, 512L, P_3, 0L, P_5, 0L);
		case 'K':
			return jjMoveStringLiteralDfa3_0(P_1, 1088L, P_3, 0L, P_5, 0L);
		case 'N':
			return jjMoveStringLiteralDfa3_0(P_1, 4L, P_3, 0L, P_5, 0L);
		case 'O':
			return jjMoveStringLiteralDfa3_0(P_1, 2L, P_3, 0L, P_5, 0L);
		case 'R':
			return jjMoveStringLiteralDfa3_0(P_1, 280L, P_3, 0L, P_5, 0L);
		case 'V':
			return jjMoveStringLiteralDfa3_0(P_1, 32L, P_3, 0L, P_5, 0L);
		case 'a':
			return jjMoveStringLiteralDfa3_0(P_1, 51539607552L, P_3, 1028L, P_5, 0L);
		case 'b':
			return jjMoveStringLiteralDfa3_0(P_1, long.MinValue, P_3, 0L, P_5, 0L);
		case 'c':
			return jjMoveStringLiteralDfa3_0(P_1, 1152921504606846976L, P_3, 0L, P_5, 0L);
		case 'e':
			return jjMoveStringLiteralDfa3_0(P_1, 1073741824L, P_3, 0L, P_5, 0L);
		case 'f':
			return jjMoveStringLiteralDfa3_0(P_1, 274877906944L, P_3, 0L, P_5, 0L);
		case 'i':
			return jjMoveStringLiteralDfa3_0(P_1, 2305843009213693952L, P_3, 41120L, P_5, 0L);
		case 'l':
			return jjMoveStringLiteralDfa3_0(P_1, 576478344489467904L, P_3, 16384L, P_5, 0L);
		case 'n':
			return jjMoveStringLiteralDfa3_0(P_1, 72163353312624640L, P_3, 64L, P_5, 0L);
		case 'o':
			return jjMoveStringLiteralDfa3_0(P_1, 4611826756452614144L, P_3, 2L, P_5, 0L);
		case 'p':
			return jjMoveStringLiteralDfa3_0(P_1, 6755399441055744L, P_3, 16L, P_5, 0L);
		case 'r':
			if ((P_1 & 0x1000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(2, 48, 32);
			}
			return jjMoveStringLiteralDfa3_0(P_1, 0L, P_3, 776L, P_5, 0L);
		case 's':
			return jjMoveStringLiteralDfa3_0(P_1, 9009402975617024L, P_3, 0L, P_5, 0L);
		case 't':
			if ((P_1 & 0x40000000000000L) != 0)
			{
				jjmatchedKind = 54;
				jjmatchedPos = 2;
			}
			return jjMoveStringLiteralDfa3_0(P_1, 180715741878681600L, P_3, 1L, P_5, 0L);
		case 'u':
			return jjMoveStringLiteralDfa3_0(P_1, 5497558138880L, P_3, 2048L, P_5, 0L);
		case 'w':
			if ((P_1 & 0x400000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(2, 58, 32);
			}
			break;
		case 'y':
			if ((P_3 & 0x1000u) != 0)
			{
				return jjStartNfaWithStates_0(2, 76, 32);
			}
			break;
		}
		return jjStartNfa_0(1, P_1, P_3, P_5);
		IL_0040:
		
		jjStopStringLiteralDfa_0(1, P_1, P_3, P_5);
		return 2;
	}

	
	private int jjStartNfaWithStates_0(int P_0, int P_1, int P_2)
	{
		jjmatchedKind = P_1;
		jjmatchedPos = P_0;
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0027;
		}
		return jjMoveNfa_0(P_2, P_0 + 1);
		IL_0027:
		
		return P_0 + 1;
	}

	

	private int jjStartNfa_0(int P_0, long P_1, long P_2, long P_3)
	{
		int result = jjMoveNfa_0(jjStopStringLiteralDfa_0(P_0, P_1, P_2, P_3), P_0 + 1);
		
		return result;
	}

	
	private int jjMoveStringLiteralDfa3_0(long P_0, long P_1, long P_2, long P_3, long P_4, long P_5)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2) | (P_5 &= P_4)) == 0)
		{
			return jjStartNfa_0(1, P_0, P_2, P_4);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0040;
		}
		switch (curChar)
		{
		case '=':
			if ((P_5 & 0x200u) != 0)
			{
				return jjStopAtPos(3, 137);
			}
			break;
		case 'A':
			return jjMoveStringLiteralDfa4_0(P_1, 32L, P_3, 0L, P_5, 0L);
		case 'C':
			return jjMoveStringLiteralDfa4_0(P_1, 128L, P_3, 0L, P_5, 0L);
		case 'E':
			if ((P_1 & 0x100u) != 0)
			{
				return jjStartNfaWithStates_0(3, 8, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, 1088L, P_3, 0L, P_5, 0L);
		case 'K':
			return jjMoveStringLiteralDfa4_0(P_1, 2L, P_3, 0L, P_5, 0L);
		case 'O':
			return jjMoveStringLiteralDfa4_0(P_1, 4L, P_3, 0L, P_5, 0L);
		case 'P':
			if ((P_1 & 0x200u) != 0)
			{
				return jjStartNfaWithStates_0(3, 9, 32);
			}
			break;
		case 'S':
			return jjMoveStringLiteralDfa4_0(P_1, 24L, P_3, 0L, P_5, 0L);
		case 'a':
			return jjMoveStringLiteralDfa4_0(P_1, 246566556270592L, P_3, 16384L, P_5, 0L);
		case 'b':
			return jjMoveStringLiteralDfa4_0(P_1, 1099515822080L, P_3, 0L, P_5, 0L);
		case 'c':
			return jjMoveStringLiteralDfa4_0(P_1, 8589934592L, P_3, 64L, P_5, 0L);
		case 'd':
			if ((P_3 & 0x2000u) != 0)
			{
				return jjStartNfaWithStates_0(3, 77, 32);
			}
			break;
		case 'e':
			if ((P_1 & 0x80000000u) != 0)
			{
				return jjStartNfaWithStates_0(3, 31, 32);
			}
			if ((P_1 & 0x100000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 32, 32);
			}
			if ((P_1 & 0x20000000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 41, 32);
			}
			if ((P_3 & 0x800u) != 0)
			{
				return jjStartNfaWithStates_0(3, 75, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, 36037593380552704L, P_3, 16L, P_5, 0L);
		case 'g':
			if ((P_1 & 0x100000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 56, 32);
			}
			break;
		case 'i':
			return jjMoveStringLiteralDfa4_0(P_1, 144115188075855872L, P_3, 8L, P_5, 0L);
		case 'k':
			return jjMoveStringLiteralDfa4_0(P_1, 1152921504606846976L, P_3, 0L, P_5, 0L);
		case 'l':
			if ((P_1 & 0x800000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 59, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, -9221120236504219648L, P_3, 32768L, P_5, 0L);
		case 'm':
			if ((P_1 & 0x40000000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 42, 32);
			}
			break;
		case 'n':
			return jjMoveStringLiteralDfa4_0(P_1, 0L, P_3, 1024L, P_5, 0L);
		case 'o':
			if ((P_1 & 0x2000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 49, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, 4503599627370496L, P_3, 768L, P_5, 0L);
		case 'r':
			if ((P_1 & 0x400000000L) != 0)
			{
				return jjStartNfaWithStates_0(3, 34, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, 0L, P_3, 2L, P_5, 0L);
		case 's':
			if ((P_3 & 0x80u) != 0)
			{
				return jjStartNfaWithStates_0(3, 71, 32);
			}
			return jjMoveStringLiteralDfa4_0(P_1, 17695265259520L, P_3, 0L, P_5, 0L);
		case 't':
			return jjMoveStringLiteralDfa4_0(P_1, 4620693355255300096L, P_3, 36L, P_5, 0L);
		case 'u':
			return jjMoveStringLiteralDfa4_0(P_1, 0L, P_3, 1L, P_5, 0L);
		case 'v':
			return jjMoveStringLiteralDfa4_0(P_1, 2305843009213693952L, P_3, 0L, P_5, 0L);
		}
		return jjStartNfa_0(2, P_1, P_3, P_5);
		IL_0040:
		
		jjStopStringLiteralDfa_0(2, P_1, P_3, P_5);
		return 3;
	}

	
	private int jjMoveStringLiteralDfa4_0(long P_0, long P_1, long P_2, long P_3, long P_4, long P_5)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2) | (P_5 & P_4)) == 0)
		{
			return jjStartNfa_0(2, P_0, P_2, P_4);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_003f;
		}
		switch (curChar)
		{
		case 'A':
			return jjMoveStringLiteralDfa5_0(P_1, 2L, P_3, 0L);
		case 'C':
			return jjMoveStringLiteralDfa5_0(P_1, 32L, P_3, 0L);
		case 'E':
			return jjMoveStringLiteralDfa5_0(P_1, 24L, P_3, 0L);
		case 'I':
			return jjMoveStringLiteralDfa5_0(P_1, 128L, P_3, 0L);
		case 'N':
			if ((P_1 & 0x40u) != 0)
			{
				jjmatchedKind = 6;
				jjmatchedPos = 4;
			}
			return jjMoveStringLiteralDfa5_0(P_1, 1024L, P_3, 0L);
		case 'R':
			return jjMoveStringLiteralDfa5_0(P_1, 4L, P_3, 0L);
		case 'a':
			return jjMoveStringLiteralDfa5_0(P_1, 3467771713075281920L, P_3, 0L);
		case 'c':
			return jjMoveStringLiteralDfa5_0(P_1, 0L, P_3, 40L);
		case 'e':
			if ((P_1 & 0x100000000000L) != 0)
			{
				return jjStartNfaWithStates_0(4, 44, 32);
			}
			if ((P_3 & 0x8000u) != 0)
			{
				return jjStartNfaWithStates_0(4, 79, 32);
			}
			return jjMoveStringLiteralDfa5_0(P_1, 4613937818777944064L, P_3, 0L);
		case 'g':
			return jjMoveStringLiteralDfa5_0(P_1, 4325376L, P_3, 0L);
		case 'h':
			if ((P_1 & 0x200000000L) != 0)
			{
				return jjStartNfaWithStates_0(4, 33, 32);
			}
			return jjMoveStringLiteralDfa5_0(P_1, 0L, P_3, 64L);
		case 'i':
			return jjMoveStringLiteralDfa5_0(P_1, -9223371899415822336L, P_3, 4L);
		case 'k':
			if ((P_1 & 0x40000000u) != 0)
			{
				return jjStartNfaWithStates_0(4, 30, 32);
			}
			break;
		case 'l':
			if ((P_1 & 0x200000000000L) != 0)
			{
				jjmatchedKind = 45;
				jjmatchedPos = 4;
			}
			return jjMoveStringLiteralDfa5_0(P_1, 71468255805440L, P_3, 0L);
		case 'n':
			return jjMoveStringLiteralDfa5_0(P_1, 8796093022208L, P_3, 0L);
		case 'r':
			if ((P_3 & 0x10u) != 0)
			{
				return jjStartNfaWithStates_0(4, 68, 32);
			}
			return jjMoveStringLiteralDfa5_0(P_1, 40532397048987648L, P_3, 1L);
		case 's':
			if ((P_1 & 0x800000000L) != 0)
			{
				return jjStartNfaWithStates_0(4, 35, 32);
			}
			return jjMoveStringLiteralDfa5_0(P_1, 0L, P_3, 1024L);
		case 't':
			if ((P_1 & 0x1000000000L) != 0)
			{
				return jjStartNfaWithStates_0(4, 36, 32);
			}
			if ((P_1 & 0x800000000000L) != 0)
			{
				return jjStartNfaWithStates_0(4, 47, 32);
			}
			if ((P_3 & 2) != 0)
			{
				return jjStartNfaWithStates_0(4, 65, 32);
			}
			return jjMoveStringLiteralDfa5_0(P_1, 0L, P_3, 16384L);
		case 'u':
			return jjMoveStringLiteralDfa5_0(P_1, 274877906944L, P_3, 0L);
		case 'v':
			return jjMoveStringLiteralDfa5_0(P_1, 144115188075855872L, P_3, 0L);
		case 'w':
			if ((P_3 & 0x100u) != 0)
			{
				jjmatchedKind = 72;
				jjmatchedPos = 4;
			}
			return jjMoveStringLiteralDfa5_0(P_1, 0L, P_3, 512L);
		}
		return jjStartNfa_0(3, P_1, P_3, 0L);
		IL_003f:
		
		jjStopStringLiteralDfa_0(3, P_1, P_3, 0L);
		return 4;
	}

	
	private int jjMoveStringLiteralDfa5_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(3, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case 'A':
			return jjMoveStringLiteralDfa6_0(P_1, 128L, P_3, 0L);
		case 'E':
			return jjMoveStringLiteralDfa6_0(P_1, 4L, P_3, 0L);
		case 'H':
			return jjMoveStringLiteralDfa6_0(P_1, 2L, P_3, 0L);
		case 'O':
			return jjMoveStringLiteralDfa6_0(P_1, 32L, P_3, 0L);
		case 'R':
			return jjMoveStringLiteralDfa6_0(P_1, 24L, P_3, 0L);
		case '_':
			return jjMoveStringLiteralDfa6_0(P_1, 1024L, P_3, 0L);
		case 'a':
			return jjMoveStringLiteralDfa6_0(P_1, 671088640L, P_3, 0L);
		case 'c':
			if ((P_1 & long.MinValue) != 0)
			{
				return jjStartNfaWithStates_0(5, 63, 32);
			}
			if ((P_3 & 4) != 0)
			{
				return jjStartNfaWithStates_0(5, 66, 32);
			}
			return jjMoveStringLiteralDfa6_0(P_1, 4611686018427387904L, P_3, 0L);
		case 'd':
			return jjMoveStringLiteralDfa6_0(P_1, 8796093022208L, P_3, 0L);
		case 'e':
			if ((P_1 & 0x10000000000L) != 0)
			{
				return jjStartNfaWithStates_0(5, 40, 32);
			}
			if ((P_1 & 0x200000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(5, 57, 32);
			}
			return jjMoveStringLiteralDfa6_0(P_1, 4325376L, P_3, 0L);
		case 'f':
			return jjMoveStringLiteralDfa6_0(P_1, 36028797018963968L, P_3, 0L);
		case 'g':
			return jjMoveStringLiteralDfa6_0(P_1, 1152921504606846976L, P_3, 0L);
		case 'h':
			if ((P_3 & 0x20u) != 0)
			{
				return jjStartNfaWithStates_0(5, 69, 32);
			}
			break;
		case 'i':
			return jjMoveStringLiteralDfa6_0(P_1, 0L, P_3, 17408L);
		case 'l':
			return jjMoveStringLiteralDfa6_0(P_1, 70643622084608L, P_3, 0L);
		case 'm':
			return jjMoveStringLiteralDfa6_0(P_1, 2251799813685248L, P_3, 0L);
		case 'n':
			if ((P_3 & 1) != 0)
			{
				return jjStartNfaWithStates_0(5, 64, 32);
			}
			return jjMoveStringLiteralDfa6_0(P_1, 9007336693694464L, P_3, 0L);
		case 'r':
			return jjMoveStringLiteralDfa6_0(P_1, 0L, P_3, 64L);
		case 's':
			if ((P_3 & 0x200u) != 0)
			{
				return jjStartNfaWithStates_0(5, 73, 32);
			}
			break;
		case 't':
			if ((P_1 & 0x10000000u) != 0)
			{
				return jjStartNfaWithStates_0(5, 28, 32);
			}
			if ((P_1 & 0x10000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(5, 52, 32);
			}
			return jjMoveStringLiteralDfa6_0(P_1, 2305843009213693952L, P_3, 8L);
		}
		return jjStartNfa_0(4, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(4, P_1, P_3, 0L);
		return 5;
	}

	
	private int jjMoveStringLiteralDfa6_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(4, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case 'D':
			return jjMoveStringLiteralDfa7_0(P_1, 32L, P_3, 0L);
		case 'E':
			return jjMoveStringLiteralDfa7_0(P_1, 2L, P_3, 0L);
		case 'L':
			return jjMoveStringLiteralDfa7_0(P_1, 128L, P_3, 0L);
		case 'M':
			return jjMoveStringLiteralDfa7_0(P_1, 1024L, P_3, 0L);
		case '_':
			return jjMoveStringLiteralDfa7_0(P_1, 28L, P_3, 0L);
		case 'a':
			return jjMoveStringLiteralDfa7_0(P_1, 36028797018963968L, P_3, 0L);
		case 'c':
			return jjMoveStringLiteralDfa7_0(P_1, 9007199388958720L, P_3, 0L);
		case 'e':
			if ((P_1 & 0x1000000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(6, 60, 32);
			}
			if ((P_1 & 0x2000000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(6, 61, 32);
			}
			return jjMoveStringLiteralDfa7_0(P_1, 2251799813685248L, P_3, 1024L);
		case 'f':
			return jjMoveStringLiteralDfa7_0(P_1, 0L, P_3, 8L);
		case 'l':
			return jjMoveStringLiteralDfa7_0(P_1, 0L, P_3, 16384L);
		case 'n':
			if ((P_1 & 0x20000000u) != 0)
			{
				return jjStartNfaWithStates_0(6, 29, 32);
			}
			return jjMoveStringLiteralDfa7_0(P_1, 4325376L, P_3, 0L);
		case 'o':
			return jjMoveStringLiteralDfa7_0(P_1, 0L, P_3, 64L);
		case 's':
			if ((P_1 & 0x80000000000L) != 0)
			{
				return jjStartNfaWithStates_0(6, 43, 32);
			}
			break;
		case 't':
			if ((P_1 & 0x4000000000L) != 0)
			{
				return jjStartNfaWithStates_0(6, 38, 32);
			}
			return jjMoveStringLiteralDfa7_0(P_1, 4611686018427387904L, P_3, 0L);
		case 'u':
			return jjMoveStringLiteralDfa7_0(P_1, 137438953472L, P_3, 0L);
		case 'y':
			if ((P_1 & 0x400000000000L) != 0)
			{
				return jjStartNfaWithStates_0(6, 46, 32);
			}
			break;
		}
		return jjStartNfa_0(5, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(5, P_1, P_3, 0L);
		return 6;
	}

	
	private int jjMoveStringLiteralDfa7_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(5, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case '(':
			return jjMoveStringLiteralDfa8_0(P_1, 4194304L, P_3, 0L);
		case '*':
			return jjMoveStringLiteralDfa8_0(P_1, 131072L, P_3, 0L);
		case 'A':
			return jjMoveStringLiteralDfa8_0(P_1, 2L, P_3, 0L);
		case 'B':
			return jjMoveStringLiteralDfa8_0(P_1, 8L, P_3, 0L);
		case 'C':
			return jjMoveStringLiteralDfa8_0(P_1, 4L, P_3, 0L);
		case 'E':
			if ((P_1 & 0x20u) != 0)
			{
				return jjStartNfaWithStates_0(7, 5, 32);
			}
			return jjMoveStringLiteralDfa8_0(P_1, 16L, P_3, 0L);
		case 'G':
			return jjMoveStringLiteralDfa8_0(P_1, 1024L, P_3, 0L);
		case '_':
			return jjMoveStringLiteralDfa8_0(P_1, 128L, P_3, 0L);
		case 'c':
			return jjMoveStringLiteralDfa8_0(P_1, 36028797018963968L, P_3, 0L);
		case 'e':
			if ((P_1 & 0x2000000000L) != 0)
			{
				return jjStartNfaWithStates_0(7, 37, 32);
			}
			if ((P_3 & 0x4000u) != 0)
			{
				return jjStartNfaWithStates_0(7, 78, 32);
			}
			return jjMoveStringLiteralDfa8_0(P_1, 4620693217682128896L, P_3, 0L);
		case 'n':
			return jjMoveStringLiteralDfa8_0(P_1, 2251799813685248L, P_3, 1088L);
		case 'p':
			if ((P_3 & 8) != 0)
			{
				return jjStartNfaWithStates_0(7, 67, 32);
			}
			break;
		case 't':
			if ((P_1 & 0x8000000u) != 0)
			{
				return jjStartNfaWithStates_0(7, 27, 32);
			}
			break;
		}
		return jjStartNfa_0(6, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(6, P_1, P_3, 0L);
		return 7;
	}

	
	private int jjMoveStringLiteralDfa8_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(6, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case '/':
			if ((P_1 & 0x20000u) != 0)
			{
				return jjStopAtPos(8, 17);
			}
			break;
		case 'A':
			return jjMoveStringLiteralDfa9_0(P_1, 4L, P_3, 0L);
		case 'D':
			if ((P_1 & 2) != 0)
			{
				return jjStartNfaWithStates_0(8, 1, 32);
			}
			break;
		case 'E':
			return jjMoveStringLiteralDfa9_0(P_1, 8L, P_3, 0L);
		case 'N':
			return jjMoveStringLiteralDfa9_0(P_1, 16L, P_3, 0L);
		case 'R':
			return jjMoveStringLiteralDfa9_0(P_1, 1024L, P_3, 0L);
		case 'T':
			return jjMoveStringLiteralDfa9_0(P_1, 128L, P_3, 0L);
		case 'd':
			if ((P_1 & 0x4000000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(8, 62, 32);
			}
			break;
		case 'e':
			if ((P_1 & 0x80000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(8, 55, 32);
			}
			break;
		case 'i':
			return jjMoveStringLiteralDfa9_0(P_1, 0L, P_3, 64L);
		case 'j':
			return jjMoveStringLiteralDfa9_0(P_1, 4194304L, P_3, 0L);
		case 'o':
			return jjMoveStringLiteralDfa9_0(P_1, 9007199254740992L, P_3, 0L);
		case 't':
			if ((P_3 & 0x400u) != 0)
			{
				return jjStartNfaWithStates_0(8, 74, 32);
			}
			return jjMoveStringLiteralDfa9_0(P_1, 2251799813685248L, P_3, 0L);
		}
		return jjStartNfa_0(7, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(7, P_1, P_3, 0L);
		return 8;
	}

	
	private int jjMoveStringLiteralDfa9_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(7, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case 'D':
			if ((P_1 & 0x10u) != 0)
			{
				return jjStartNfaWithStates_0(9, 4, 32);
			}
			break;
		case 'G':
			return jjMoveStringLiteralDfa10_0(P_1, 8L, P_3, 0L);
		case 'O':
			return jjMoveStringLiteralDfa10_0(P_1, 128L, P_3, 0L);
		case 'S':
			return jjMoveStringLiteralDfa10_0(P_1, 4L, P_3, 0L);
		case '_':
			return jjMoveStringLiteralDfa10_0(P_1, 1024L, P_3, 0L);
		case 'f':
			if ((P_1 & 0x20000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(9, 53, 32);
			}
			break;
		case 'j':
			return jjMoveStringLiteralDfa10_0(P_1, 4194304L, P_3, 0L);
		case 's':
			if ((P_1 & 0x8000000000000L) != 0)
			{
				return jjStartNfaWithStates_0(9, 51, 32);
			}
			break;
		case 'z':
			return jjMoveStringLiteralDfa10_0(P_1, 0L, P_3, 64L);
		}
		return jjStartNfa_0(8, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(8, P_1, P_3, 0L);
		return 9;
	}

	
	private int jjMoveStringLiteralDfa10_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(8, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case 'D':
			return jjMoveStringLiteralDfa11_0(P_1, 1024L, P_3, 0L);
		case 'E':
			if ((P_1 & 4) != 0)
			{
				return jjStartNfaWithStates_0(10, 2, 32);
			}
			break;
		case 'I':
			return jjMoveStringLiteralDfa11_0(P_1, 8L, P_3, 0L);
		case 'K':
			return jjMoveStringLiteralDfa11_0(P_1, 128L, P_3, 0L);
		case 'e':
			return jjMoveStringLiteralDfa11_0(P_1, 0L, P_3, 64L);
		case 't':
			return jjMoveStringLiteralDfa11_0(P_1, 4194304L, P_3, 0L);
		}
		return jjStartNfa_0(9, P_1, P_3, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(9, P_1, P_3, 0L);
		return 10;
	}

	
	private int jjMoveStringLiteralDfa11_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 &= P_2)) == 0)
		{
			return jjStartNfa_0(9, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0038;
		}
		switch (curChar)
		{
		case 'E':
			return jjMoveStringLiteralDfa12_0(P_1, 1152L, P_3, 0L);
		case 'N':
			if ((P_1 & 8) != 0)
			{
				return jjStartNfaWithStates_0(11, 3, 32);
			}
			break;
		case 'd':
			if ((P_3 & 0x40u) != 0)
			{
				return jjStartNfaWithStates_0(11, 70, 32);
			}
			break;
		case 'r':
			return jjMoveStringLiteralDfa12_0(P_1, 4194304L, P_3, 0L);
		}
		return jjStartNfa_0(10, P_1, P_3, 0L);
		IL_0038:
		
		jjStopStringLiteralDfa_0(10, P_1, P_3, 0L);
		return 11;
	}

	
	private int jjMoveStringLiteralDfa12_0(long P_0, long P_1, long P_2, long P_3)
	{
		if (((P_1 &= P_0) | (P_3 & P_2)) == 0)
		{
			return jjStartNfa_0(10, P_0, P_2, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0037;
		}
		switch (curChar)
		{
		case 'C':
			return jjMoveStringLiteralDfa13_0(P_1, 1024L);
		case 'N':
			if ((P_1 & 0x80u) != 0)
			{
				return jjStartNfaWithStates_0(12, 7, 32);
			}
			break;
		case 'e':
			return jjMoveStringLiteralDfa13_0(P_1, 4194304L);
		}
		return jjStartNfa_0(11, P_1, 0L, 0L);
		IL_0037:
		
		jjStopStringLiteralDfa_0(11, P_1, 0L, 0L);
		return 12;
	}

	
	private int jjMoveStringLiteralDfa13_0(long P_0, long P_1)
	{
		if ((P_1 &= P_0) == 0)
		{
			return jjStartNfa_0(11, P_0, 0L, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0031;
		}
		switch (curChar)
		{
		case 'L':
			return jjMoveStringLiteralDfa14_0(P_1, 1024L);
		case 'e':
			if ((P_1 & 0x400000u) != 0)
			{
				return jjStopAtPos(13, 22);
			}
			break;
		}
		return jjStartNfa_0(12, P_1, 0L, 0L);
		IL_0031:
		
		jjStopStringLiteralDfa_0(12, P_1, 0L, 0L);
		return 13;
	}

	
	private int jjMoveStringLiteralDfa14_0(long P_0, long P_1)
	{
		if ((P_1 &= P_0) == 0)
		{
			return jjStartNfa_0(12, P_0, 0L, 0L);
		}
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0031;
		}
		if (curChar == 'S' && (P_1 & 0x400u) != 0)
		{
			return jjStartNfaWithStates_0(14, 10, 32);
		}
		return jjStartNfa_0(13, P_1, 0L, 0L);
		IL_0031:
		
		jjStopStringLiteralDfa_0(13, P_1, 0L, 0L);
		return 14;
	}

	private void ReInitRounds()
	{
		jjround = -2147483647;
		int num = 65;
		while (num-- > 0)
		{
			jjrounds[num] = int.MinValue;
		}
	}

	
	private void jjCheckNAddStates(int P_0, int P_1)
	{
		int num;
		do
		{
			jjCheckNAdd(jjnextStates[P_0]);
			num = P_0;
			P_0++;
		}
		while (num != P_1);
	}

	private void jjCheckNAdd(int P_0)
	{
		if (jjrounds[P_0] != jjround)
		{
			int[] array = jjstateSet;
			int num = jjnewStateCnt;
			jjnewStateCnt = num + 1;
			array[num] = P_0;
			jjrounds[P_0] = jjround;
		}
	}

	private void jjAddStates(int P_0, int P_1)
	{
		int num2;
		do
		{
			int[] array = jjstateSet;
			int num = jjnewStateCnt;
			jjnewStateCnt = num + 1;
			array[num] = jjnextStates[P_0];
			num2 = P_0;
			P_0++;
		}
		while (num2 != P_1);
	}

	
	private void jjCheckNAddTwoStates(int P_0, int P_1)
	{
		jjCheckNAdd(P_0);
		jjCheckNAdd(P_1);
	}


	private static bool jjCanMove_1(int P_0, int P_1, int P_2, long P_3, long P_4)
	{
		switch (P_0)
		{
		case 0:
			return (jjbitVec4[P_2] & P_4) != 0;
		case 2:
			return (jjbitVec5[P_2] & P_4) != 0;
		case 3:
			return (jjbitVec6[P_2] & P_4) != 0;
		case 4:
			return (jjbitVec7[P_2] & P_4) != 0;
		case 5:
			return (jjbitVec8[P_2] & P_4) != 0;
		case 6:
			return (jjbitVec9[P_2] & P_4) != 0;
		case 7:
			return (jjbitVec10[P_2] & P_4) != 0;
		case 9:
			return (jjbitVec11[P_2] & P_4) != 0;
		case 10:
			return (jjbitVec12[P_2] & P_4) != 0;
		case 11:
			return (jjbitVec13[P_2] & P_4) != 0;
		case 12:
			return (jjbitVec14[P_2] & P_4) != 0;
		case 13:
			return (jjbitVec15[P_2] & P_4) != 0;
		case 14:
			return (jjbitVec16[P_2] & P_4) != 0;
		case 15:
			return (jjbitVec17[P_2] & P_4) != 0;
		case 16:
			return (jjbitVec18[P_2] & P_4) != 0;
		case 17:
			return (jjbitVec19[P_2] & P_4) != 0;
		case 18:
			return (jjbitVec20[P_2] & P_4) != 0;
		case 19:
			return (jjbitVec21[P_2] & P_4) != 0;
		case 20:
			return (jjbitVec0[P_2] & P_4) != 0;
		case 22:
			return (jjbitVec22[P_2] & P_4) != 0;
		case 23:
			return (jjbitVec23[P_2] & P_4) != 0;
		case 24:
			return (jjbitVec24[P_2] & P_4) != 0;
		case 30:
			return (jjbitVec25[P_2] & P_4) != 0;
		case 31:
			return (jjbitVec26[P_2] & P_4) != 0;
		case 32:
			return (jjbitVec27[P_2] & P_4) != 0;
		case 33:
			return (jjbitVec28[P_2] & P_4) != 0;
		case 48:
			return (jjbitVec29[P_2] & P_4) != 0;
		case 49:
			return (jjbitVec30[P_2] & P_4) != 0;
		case 77:
			return (jjbitVec31[P_2] & P_4) != 0;
		case 159:
			return (jjbitVec32[P_2] & P_4) != 0;
		case 164:
			return (jjbitVec33[P_2] & P_4) != 0;
		case 215:
			return (jjbitVec34[P_2] & P_4) != 0;
		case 250:
			return (jjbitVec35[P_2] & P_4) != 0;
		case 251:
			return (jjbitVec36[P_2] & P_4) != 0;
		case 253:
			return (jjbitVec37[P_2] & P_4) != 0;
		case 254:
			return (jjbitVec38[P_2] & P_4) != 0;
		case 255:
			return (jjbitVec39[P_2] & P_4) != 0;
		default:
			if ((jjbitVec3[P_1] & P_3) != 0)
			{
				return true;
			}
			return false;
		}
	}


	private static bool jjCanMove_0(int P_0, int P_1, int P_2, long P_3, long P_4)
	{
		if (P_0 == 0)
		{
			return (jjbitVec2[P_2] & P_4) != 0;
		}
		if ((jjbitVec0[P_1] & P_3) != 0)
		{
			return true;
		}
		return false;
	}


	private static bool jjCanMove_2(int P_0, int P_1, int P_2, long P_3, long P_4)
	{
		switch (P_0)
		{
		case 0:
			return (jjbitVec40[P_2] & P_4) != 0;
		case 2:
			return (jjbitVec5[P_2] & P_4) != 0;
		case 3:
			return (jjbitVec41[P_2] & P_4) != 0;
		case 4:
			return (jjbitVec42[P_2] & P_4) != 0;
		case 5:
			return (jjbitVec43[P_2] & P_4) != 0;
		case 6:
			return (jjbitVec44[P_2] & P_4) != 0;
		case 7:
			return (jjbitVec45[P_2] & P_4) != 0;
		case 9:
			return (jjbitVec46[P_2] & P_4) != 0;
		case 10:
			return (jjbitVec47[P_2] & P_4) != 0;
		case 11:
			return (jjbitVec48[P_2] & P_4) != 0;
		case 12:
			return (jjbitVec49[P_2] & P_4) != 0;
		case 13:
			return (jjbitVec50[P_2] & P_4) != 0;
		case 14:
			return (jjbitVec51[P_2] & P_4) != 0;
		case 15:
			return (jjbitVec52[P_2] & P_4) != 0;
		case 16:
			return (jjbitVec53[P_2] & P_4) != 0;
		case 17:
			return (jjbitVec19[P_2] & P_4) != 0;
		case 18:
			return (jjbitVec20[P_2] & P_4) != 0;
		case 19:
			return (jjbitVec54[P_2] & P_4) != 0;
		case 20:
			return (jjbitVec0[P_2] & P_4) != 0;
		case 22:
			return (jjbitVec22[P_2] & P_4) != 0;
		case 23:
			return (jjbitVec55[P_2] & P_4) != 0;
		case 24:
			return (jjbitVec56[P_2] & P_4) != 0;
		case 30:
			return (jjbitVec25[P_2] & P_4) != 0;
		case 31:
			return (jjbitVec26[P_2] & P_4) != 0;
		case 32:
			return (jjbitVec57[P_2] & P_4) != 0;
		case 33:
			return (jjbitVec28[P_2] & P_4) != 0;
		case 48:
			return (jjbitVec58[P_2] & P_4) != 0;
		case 49:
			return (jjbitVec30[P_2] & P_4) != 0;
		case 77:
			return (jjbitVec31[P_2] & P_4) != 0;
		case 159:
			return (jjbitVec32[P_2] & P_4) != 0;
		case 164:
			return (jjbitVec33[P_2] & P_4) != 0;
		case 215:
			return (jjbitVec34[P_2] & P_4) != 0;
		case 250:
			return (jjbitVec35[P_2] & P_4) != 0;
		case 251:
			return (jjbitVec59[P_2] & P_4) != 0;
		case 253:
			return (jjbitVec37[P_2] & P_4) != 0;
		case 254:
			return (jjbitVec60[P_2] & P_4) != 0;
		case 255:
			return (jjbitVec61[P_2] & P_4) != 0;
		default:
			if ((jjbitVec3[P_1] & P_3) != 0)
			{
				return true;
			}
			return false;
		}
	}

	
	private int jjMoveStringLiteralDfa1_4(long P_0)
	{
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0019;
		}
		if (curChar == '/')
		{
			if ((P_0 & 0x2000000u) != 0)
			{
				return jjStopAtPos(1, 25);
			}
			return 2;
		}
		return 2;
		IL_0019:
		
		return 1;
	}

	
	private int jjMoveNfa_2(int P_0, int P_1)
	{
		int num = 0;
		jjnewStateCnt = 3;
		int num2 = 1;
		jjstateSet[0] = P_0;
		int num3 = int.MaxValue;
		while (true)
		{
			int num4 = jjround + 1;
			JavaCCParserTokenManager javaCCParserTokenManager = this;
			int num5 = num4;
			javaCCParserTokenManager.jjround = num4;
			if (num5 == int.MaxValue)
			{
				ReInitRounds();
			}
			if (curChar < '@')
			{
				long num6 = 1L << (int)curChar;
				do
				{
					int[] array = jjstateSet;
					num2 += -1;
					switch (array[num2])
					{
					case 0:
						if ((0x2400u & num6) != 0 && num3 > 23)
						{
							num3 = 23;
						}
						if (curChar == '\r')
						{
							int[] array3 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num8 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array3[num8] = 1;
						}
						break;
					case 1:
						if (curChar == '\n' && num3 > 23)
						{
							num3 = 23;
						}
						break;
					case 2:
						if (curChar == '\r')
						{
							int[] array2 = jjstateSet;
							num4 = jjnewStateCnt;
							javaCCParserTokenManager = this;
							int num7 = num4;
							javaCCParserTokenManager.jjnewStateCnt = num4 + 1;
							array2[num7] = 1;
						}
						break;
					}
				}
				while (num2 != num);
			}
			else if (curChar < '\u0080')
			{
				_ = 1L << (curChar & 0x3F);
				do
				{
					int[] array4 = jjstateSet;
					num2 += -1;
					_ = array4[num2];
				}
				while (num2 != num);
			}
			else
			{
				int num9 = (int)curChar >> 8;
				_ = num9 >> 6;
				_ = 1L << (num9 & 0x3F);
				_ = (curChar & 0xFF) >> 6;
				_ = 1L << (curChar & 0x3F);
				do
				{
					int[] array5 = jjstateSet;
					num2 += -1;
					_ = array5[num2];
				}
				while (num2 != num);
			}
			if (num3 != int.MaxValue)
			{
				jjmatchedKind = num3;
				jjmatchedPos = P_1;
				num3 = int.MaxValue;
			}
			P_1++;
			int num10 = (num2 = jjnewStateCnt);
			num4 = num;
			int num11 = num4;
			jjnewStateCnt = num4;
			if (num10 == (num = 3 - num11))
			{
				return P_1;
			}
			try
			{
				curChar = input_stream.readChar();
			}
			catch (IOException)
			{
				break;
			}
		}
		
		return P_1;
	}

	
	private int jjMoveStringLiteralDfa1_3(long P_0)
	{
		try
		{
			curChar = input_stream.readChar();
		}
		catch (IOException)
		{
			goto IL_0019;
		}
		if (curChar == '/')
		{
			if ((P_0 & 0x1000000u) != 0)
			{
				return jjStopAtPos(1, 24);
			}
			return 2;
		}
		return 2;
		IL_0019:
		
		return 1;
	}

	
	public virtual void SwitchTo(int i)
	{
		if (i >= 5 || i < 0)
		{
			string str = new StringBuilder().Append("Error: Ignoring invalid lexical state : ").Append(i).Append(". State unchanged.")
				.ToString();
			
			throw new TokenMgrError(str, 2);
		}
		curLexState = i;
	}

	
	protected internal virtual Token jjFillToken()
	{
		string text = ___003C_003EjjstrLiteralImages[jjmatchedKind];
		string str = ((text != null) ? text : input_stream.GetImage());
		int num = input_stream.getBeginLine();
		int beginColumn = input_stream.getBeginColumn();
		int endLine = input_stream.getEndLine();
		int endColumn = input_stream.getEndColumn();
		Token token = Token.NewToken(jjmatchedKind, str);
		token.beginLine = num;
		token.endLine = endLine;
		token.beginColumn = beginColumn;
		token.endColumn = endColumn;
		return token;
	}

	
	private int jjMoveStringLiteralDfa0_0()
	{
		switch (curChar)
		{
		case '!':
		{
			jjmatchedKind = 102;
			int result51 = jjMoveStringLiteralDfa1_0(0L, 35184372088832L, 0L);
			
			return result51;
		}
		case '#':
		{
			int result50 = jjStopAtPos(0, 133);
			
			return result50;
		}
		case '%':
		{
			jjmatchedKind = 121;
			int result49 = jjMoveStringLiteralDfa1_0(0L, 0L, 2L);
			
			return result49;
		}
		case '&':
		{
			jjmatchedKind = 118;
			int result48 = jjMoveStringLiteralDfa1_0(0L, 4611826755915743232L, 0L);
			
			return result48;
		}
		case '(':
		{
			int result47 = jjStopAtPos(0, 91);
			
			return result47;
		}
		case ')':
		{
			int result46 = jjStopAtPos(0, 92);
			
			return result46;
		}
		case '*':
		{
			jjmatchedKind = 116;
			int result45 = jjMoveStringLiteralDfa1_0(0L, 1152921504606846976L, 0L);
			
			return result45;
		}
		case '+':
		{
			jjmatchedKind = 114;
			int result44 = jjMoveStringLiteralDfa1_0(0L, 288511851128422400L, 0L);
			
			return result44;
		}
		case ',':
		{
			int result43 = jjStopAtPos(0, 98);
			
			return result43;
		}
		case '-':
		{
			jjmatchedKind = 115;
			int result42 = jjMoveStringLiteralDfa1_0(0L, 577023702256844800L, 0L);
			
			return result42;
		}
		case '.':
		{
			jjmatchedKind = 99;
			int result41 = jjMoveStringLiteralDfa1_0(0L, 0L, 64L);
			
			return result41;
		}
		case '/':
		{
			jjmatchedKind = 117;
			int result40 = jjMoveStringLiteralDfa1_0(6946816L, 2305843009213693952L, 0L);
			
			return result40;
		}
		case ':':
		{
			int result39 = jjStopAtPos(0, 105);
			
			return result39;
		}
		case ';':
		{
			int result38 = jjStopAtPos(0, 97);
			
			return result38;
		}
		case '<':
		{
			jjmatchedKind = 101;
			int result37 = jjMoveStringLiteralDfa1_0(0L, 8796093022208L, 1152L);
			
			return result37;
		}
		case '=':
		{
			jjmatchedKind = 100;
			int result36 = jjMoveStringLiteralDfa1_0(0L, 4398046511104L, 0L);
			
			return result36;
		}
		case '>':
		{
			jjmatchedKind = 132;
			int result35 = jjMoveStringLiteralDfa1_0(0L, 17592186044416L, 780L);
			
			return result35;
		}
		case '?':
		{
			int result34 = jjStopAtPos(0, 104);
			
			return result34;
		}
		case '@':
		{
			int result33 = jjStopAtPos(0, 139);
			
			return result33;
		}
		case 'E':
		{
			int result32 = jjMoveStringLiteralDfa1_0(2048L, 0L, 0L);
			
			return result32;
		}
		case 'I':
		{
			int result31 = jjMoveStringLiteralDfa1_0(4L, 0L, 0L);
			
			return result31;
		}
		case 'J':
		{
			int result30 = jjMoveStringLiteralDfa1_0(32L, 0L, 0L);
			
			return result30;
		}
		case 'L':
		{
			int result29 = jjMoveStringLiteralDfa1_0(2L, 0L, 0L);
			
			return result29;
		}
		case 'M':
		{
			int result28 = jjMoveStringLiteralDfa1_0(256L, 0L, 0L);
			
			return result28;
		}
		case 'P':
		{
			int result27 = jjMoveStringLiteralDfa1_0(24L, 0L, 0L);
			
			return result27;
		}
		case 'S':
		{
			int result26 = jjMoveStringLiteralDfa1_0(640L, 0L, 0L);
			
			return result26;
		}
		case 'T':
		{
			int result25 = jjMoveStringLiteralDfa1_0(1088L, 0L, 0L);
			
			return result25;
		}
		case '[':
		{
			int result24 = jjStopAtPos(0, 95);
			
			return result24;
		}
		case ']':
		{
			int result23 = jjStopAtPos(0, 96);
			
			return result23;
		}
		case '^':
		{
			jjmatchedKind = 120;
			int result22 = jjMoveStringLiteralDfa1_0(0L, 0L, 1L);
			
			return result22;
		}
		case 'a':
		{
			int result21 = jjMoveStringLiteralDfa1_0(402653184L, 0L, 0L);
			
			return result21;
		}
		case 'b':
		{
			int result20 = jjMoveStringLiteralDfa1_0(3758096384L, 0L, 0L);
			
			return result20;
		}
		case 'c':
		{
			int result19 = jjMoveStringLiteralDfa1_0(270582939648L, 0L, 0L);
			
			return result19;
		}
		case 'd':
		{
			int result18 = jjMoveStringLiteralDfa1_0(1924145348608L, 0L, 0L);
			
			return result18;
		}
		case 'e':
		{
			int result17 = jjMoveStringLiteralDfa1_0(15393162788864L, 0L, 0L);
			
			return result17;
		}
		case 'f':
		{
			int result16 = jjMoveStringLiteralDfa1_0(545357767376896L, 0L, 0L);
			
			return result16;
		}
		case 'g':
		{
			int result15 = jjMoveStringLiteralDfa1_0(562949953421312L, 0L, 0L);
			
			return result15;
		}
		case 'i':
		{
			int result14 = jjMoveStringLiteralDfa1_0(70931694131085312L, 0L, 0L);
			
			return result14;
		}
		case 'l':
		{
			int result13 = jjMoveStringLiteralDfa1_0(72057594037927936L, 0L, 0L);
			
			return result13;
		}
		case 'n':
		{
			int result12 = jjMoveStringLiteralDfa1_0(1008806316530991104L, 0L, 0L);
			
			return result12;
		}
		case 'p':
		{
			int result11 = jjMoveStringLiteralDfa1_0(-1152921504606846976L, 0L, 0L);
			
			return result11;
		}
		case 'r':
		{
			int result10 = jjMoveStringLiteralDfa1_0(0L, 1L, 0L);
			
			return result10;
		}
		case 's':
		{
			int result9 = jjMoveStringLiteralDfa1_0(0L, 126L, 0L);
			
			return result9;
		}
		case 't':
		{
			int result8 = jjMoveStringLiteralDfa1_0(0L, 8064L, 0L);
			
			return result8;
		}
		case 'v':
		{
			int result7 = jjMoveStringLiteralDfa1_0(0L, 24576L, 0L);
			
			return result7;
		}
		case 'w':
		{
			int result6 = jjMoveStringLiteralDfa1_0(0L, 32768L, 0L);
			
			return result6;
		}
		case '{':
		{
			int result5 = jjStopAtPos(0, 93);
			
			return result5;
		}
		case '|':
		{
			jjmatchedKind = 119;
			int result4 = jjMoveStringLiteralDfa1_0(0L, -9223301668110598144L, 0L);
			
			return result4;
		}
		case '}':
		{
			int result3 = jjStopAtPos(0, 94);
			
			return result3;
		}
		case '~':
		{
			int result2 = jjStopAtPos(0, 103);
			
			return result2;
		}
		default:
		{
			int result = jjMoveNfa_0(3, 0);
			
			return result;
		}
		}
	}

	private int jjMoveStringLiteralDfa0_1()
	{
		return 1;
	}

	
	private int jjMoveStringLiteralDfa0_2()
	{
		int result = jjMoveNfa_2(0, 0);
		
		return result;
	}

	
	private int jjMoveStringLiteralDfa0_3()
	{
		if (curChar == '*')
		{
			int result = jjMoveStringLiteralDfa1_3(16777216L);
			
			return result;
		}
		return 1;
	}

	
	private int jjMoveStringLiteralDfa0_4()
	{
		if (curChar == '*')
		{
			int result = jjMoveStringLiteralDfa1_4(33554432L);
			
			return result;
		}
		return 1;
	}

	
	internal virtual void TokenLexicalActions(Token P_0)
	{
		switch (jjmatchedKind)
		{
		case 130:
			if (image == null)
			{
				image = new StringBuilder();
			}
			image.Append(___003C_003EjjstrLiteralImages[130]);
			P_0.kind = 132;
			((Token.GTToken)P_0).realKind = 130;
			input_stream.backup(2);
			P_0.image = ">";
			break;
		case 131:
			if (image == null)
			{
				image = new StringBuilder();
			}
			image.Append(___003C_003EjjstrLiteralImages[131]);
			P_0.kind = 132;
			((Token.GTToken)P_0).realKind = 131;
			input_stream.backup(1);
			P_0.image = ">";
			break;
		}
	}

	
	internal virtual void SkipLexicalActions(Token P_0)
	{
		if (jjmatchedKind == 18)
		{
			if (image == null)
			{
				image = new StringBuilder();
			}
			StringBuilder stringBuilder = image;
			JavaCharStream javaCharStream = input_stream;
			int num = jjimageLen;
			int num2 = jjmatchedPos + 1;
			lengthOfMatch = num2;
			stringBuilder.Append(javaCharStream.GetSuffix(num + num2));
			restoreBeginLineCol();
			input_stream.backup(1);
		}
	}

	
	internal virtual void MoreLexicalActions()
	{
		int num = jjimageLen;
		int num2 = jjmatchedPos + 1;
		lengthOfMatch = num2;
		jjimageLen = num + num2;
		switch (jjmatchedKind)
		{
		case 20:
			if (image == null)
			{
				image = new StringBuilder();
			}
			image.Append(input_stream.GetSuffix(jjimageLen));
			jjimageLen = 0;
			input_stream.backup(1);
			break;
		case 22:
			if (image == null)
			{
				image = new StringBuilder();
			}
			image.Append(input_stream.GetSuffix(jjimageLen));
			jjimageLen = 0;
			saveBeginLineCol(input_stream.getBeginLine(), input_stream.getBeginColumn());
			break;
		}
	}

	
	internal virtual void restoreBeginLineCol()
	{
		depth--;
		input_stream.adjustBeginLineColumn(beginLine[depth], beginCol[depth]);
	}

	internal virtual void saveBeginLineCol(int P_0, int P_1)
	{
		if (depth == size)
		{
			size += 5;
			int[] array = new int[size];
			int[] array2 = new int[size];
			int[] src = beginLine;
			int[] array3 = array;
			int[] dest = array3;
			beginLine = array3;
			Array.Copy(src, 0, dest, 0, depth);
			int[] src2 = beginCol;
			array3 = array2;
			int[] dest2 = array3;
			beginCol = array3;
			Array.Copy(src2, 0, dest2, 0, depth);
		}
		beginLine[depth] = P_0;
		beginCol[depth] = P_1;
		depth++;
	}

	public virtual void setDebugStream(TextWriter ps)
	{
		debugStream = ps;
	}

	
	public JavaCCParserTokenManager(JavaCharStream jcs, int i)
		: this(jcs)
	{
		SwitchTo(i);
	}

	
	public virtual void ReInit(JavaCharStream jcs, int i)
	{
		ReInit(jcs);
		SwitchTo(i);
	}

	static JavaCCParserTokenManager()
	{
		jjbitVec0 = new long[4] { -2L, -1L, -1L, -1L };
		jjbitVec2 = new long[4] { 0L, 0L, -1L, -1L };
		jjbitVec3 = new long[4] { -4503599625273342L, -8193L, -17525614051329L, 1297036692691091455L };
		jjbitVec4 = new long[4] { 0L, 0L, 297242231151001600L, -36028797027352577L };
		jjbitVec5 = new long[4] { 4503586742468607L, -65536L, -432556670460100609L, 70501888360451L };
		jjbitVec6 = new long[4] { 0L, 288230376151711744L, -17179879616L, 4503599577006079L };
		jjbitVec7 = new long[4] { -1L, -1L, -4093L, 234187180623206815L };
		jjbitVec8 = new long[4] { -562949953421312L, -8547991553L, 255L, 1979120929931264L };
		jjbitVec9 = new long[4] { 576460743713488896L, -562949953419265L, -1L, 2017613045381988351L };
		jjbitVec10 = new long[4] { 35184371892224L, 0L, 274877906943L, 0L };
		jjbitVec11 = new long[4] { 2594073385365405664L, 17163157504L, 271902628478820320L, 4222140488351744L };
		jjbitVec12 = new long[4] { 247132830528276448L, 7881300924956672L, 2589004636761075680L, 4295032832L };
		jjbitVec13 = new long[4] { 2579997437506199520L, 15837691904L, 270153412153034720L, 0L };
		jjbitVec14 = new long[4] { 283724577500946400L, 12884901888L, 283724577500946400L, 13958643712L };
		jjbitVec15 = new long[4] { 288228177128316896L, 12884901888L, 3457638613854978016L, 127L };
		jjbitVec16 = new long[4] { -9219431387180826626L, 127L, 2309762420256548246L, 805306463L };
		jjbitVec17 = new long[4] { 1L, 8796093021951L, 3840L, 0L };
		jjbitVec18 = new long[4] { 7679401525247L, 4128768L, -4294967296L, 36028797018898495L };
		jjbitVec19 = new long[4] { -1L, -2080374785L, -1065151889409L, 288230376151711743L };
		jjbitVec20 = new long[4] { -129L, -3263218305L, 9168625153884503423L, -140737496776899L };
		jjbitVec21 = new long[4] { -2160230401L, 134217599L, -4294967296L, 9007199254740991L };
		jjbitVec22 = new long[4] { -1L, 35923243902697471L, -4160749570L, 8796093022207L };
		jjbitVec23 = new long[4] { 0L, 0L, 4503599627370495L, 134217728L };
		jjbitVec24 = new long[4] { -4294967296L, 72057594037927935L, 2199023255551L, 0L };
		jjbitVec25 = new long[4] { -1L, -1L, -4026531841L, 288230376151711743L };
		jjbitVec26 = new long[4] { -3233808385L, 4611686017001275199L, 6908521828386340863L, 2295745090394464220L };
		jjbitVec27 = new long[4] { -9223372036854775808L, -9223372036854775807L, 281470681743360L, 0L };
		jjbitVec28 = new long[4] { 287031153606524036L, -4294967296L, 15L, 0L };
		jjbitVec29 = new long[4] { 521858996278132960L, -2L, -6977224705L, 9223372036854775807L };
		jjbitVec30 = new long[4] { -527765581332512L, -1L, 72057589742993407L, 0L };
		jjbitVec31 = new long[4] { -1L, -1L, 18014398509481983L, 0L };
		jjbitVec32 = new long[4] { -1L, -1L, 274877906943L, 0L };
		jjbitVec33 = new long[4] { -1L, -1L, 8191L, 0L };
		jjbitVec34 = new long[4] { -1L, -1L, 68719476735L, 0L };
		jjbitVec35 = new long[4] { 70368744177663L, 0L, 0L, 0L };
		jjbitVec36 = new long[4] { 6881498030004502655L, -37L, 1125899906842623L, -524288L };
		jjbitVec37 = new long[4] { 4611686018427387903L, -65536L, -196609L, 1152640029630136575L };
		jjbitVec38 = new long[4] { 6755399441055744L, -11538275021824000L, -1L, 2305843009213693951L };
		jjbitVec39 = new long[4] { -8646911293141286896L, -137304735746L, 9223372036854775807L, 425688104188L };
		jjbitVec40 = new long[4] { 0L, 0L, 297242235445968895L, -36028797027352577L };
		jjbitVec41 = new long[4] { -1L, 288230406216515583L, -17179879616L, 4503599577006079L };
		jjbitVec42 = new long[4] { -1L, -1L, -3973L, 234187180623206815L };
		jjbitVec43 = new long[4] { -562949953421312L, -8547991553L, -4899916411759099649L, 1979120929931286L };
		jjbitVec44 = new long[4] { 576460743713488896L, -277081220972545L, -1L, 2305629702346244095L };
		jjbitVec45 = new long[4] { -246290604654592L, 2047L, 562949953421311L, 0L };
		jjbitVec46 = new long[4] { -864691128455135250L, 281268803551231L, -3186861885341720594L, 4503392135166367L };
		jjbitVec47 = new long[4] { -3211631683292264476L, 9006925953907079L, -869759877059465234L, 281204393851839L };
		jjbitVec48 = new long[4] { -878767076314341394L, 281215949093263L, -4341532606274353172L, 280925229301191L };
		jjbitVec49 = new long[4] { -4327961440926441490L, 281212990012895L, -4327961440926441492L, 281214063754719L };
		jjbitVec50 = new long[4] { -4323457841299070996L, 281212992110031L, 3457638613854978028L, 3377704004977791L };
		jjbitVec51 = new long[4] { -8646911284551352322L, 67076095L, 4323434403644581270L, 872365919L };
		jjbitVec52 = new long[4] { -4422530440275951615L, -554153860399361L, 2305843009196855263L, 64L };
		jjbitVec53 = new long[4] { 272457864671395839L, 67044351L, -4294967296L, 36028797018898495L };
		jjbitVec54 = new long[4] { -2160230401L, 1123701017804671L, -4294967296L, 9007199254740991L };
		jjbitVec55 = new long[4] { 0L, 0L, -1L, 4393886810111L };
		jjbitVec56 = new long[4] { -4227893248L, 72057594037927935L, 4398046511103L, 0L };
		jjbitVec57 = new long[4] { -9223235697412870144L, -9223094959924576255L, 281470681743360L, 9126739968L };
		jjbitVec58 = new long[4] { 522136073208332512L, -2L, -6876561409L, 9223372036854775807L };
		jjbitVec59 = new long[4] { 6881498031078244479L, -37L, 1125899906842623L, -524288L };
		jjbitVec60 = new long[4] { 6755463865565184L, -11538275021824000L, -1L, -6917529027641081857L };
		jjbitVec61 = new long[4] { -8646911293074243568L, -137304735746L, 9223372036854775807L, 1008806742219095292L };
		jjnextStates = new int[55]
		{
			34, 35, 12, 38, 39, 42, 43, 23, 24, 26,
			14, 16, 49, 51, 6, 52, 59, 8, 9, 12,
			23, 24, 28, 26, 34, 35, 12, 44, 45, 12,
			53, 54, 60, 61, 62, 10, 11, 17, 18, 20,
			25, 27, 29, 36, 37, 40, 41, 46, 47, 55,
			56, 57, 58, 63, 64
		};
		___003C_003EjjstrLiteralImages = new string[143]
		{
			"", "LOOKAHEAD", "IGNORE_CASE", "PARSER_BEGIN", "PARSER_END", "JAVACODE", "TOKEN", "SPECIAL_TOKEN", "MORE", "SKIP",
			"TOKEN_MGR_DECLS", "EOF", null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, "abstract", "assert", "boolean",
			"break", "byte", "case", "catch", "char", "class", "const", "continue", "default", "do",
			"double", "else", "enum", "extends", "false", "final", "finally", "float", "for", "goto",
			"if", "implements", "import", "instanceof", "int", "interface", "long", "native", "new", "null",
			"package", "private", "protected", "public", "return", "short", "static", "strictfp", "super", "switch",
			"synchronized", "this", "throw", "throws", "transient", "true", "try", "void", "volatile", "while",
			null, null, null, null, null, null, null, null, null, null,
			null, "(", ")", "{", "}", "[", "]", ";", ",", ".",
			"=", "<", "!", "~", "?", ":", "==", "<=", ">=", "!=",
			"||", "&&", "++", "--", "+", "-", "*", "/", "&", "|",
			"^", "%", "+=", "-=", "*=", "/=", "&=", "|=", "^=", "%=",
			">>>", ">>", ">", "#", "...", "<<=", ">>=", ">>>=", "<<", "@",
			null, null, null
		};
		___003C_003ElexStateNames = new string[5] { "DEFAULT", "AFTER_EGEN", "IN_SINGLE_LINE_COMMENT", "IN_FORMAL_COMMENT", "IN_MULTI_LINE_COMMENT" };
		___003C_003EjjnewLexState = new int[143]
		{
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, 1, 0, 2,
			3, 4, 4, 0, 0, 0, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1
		};
		jjtoToken = new long[3] { -134213633L, -32374785L, 8191L };
		jjtoSkip = new long[3] { 59240448L, 0L, 0L };
		jjtoSpecial = new long[3] { 58720256L, 0L, 0L };
		jjtoMore = new long[3] { 74973184L, 0L, 0L };
	}
}
