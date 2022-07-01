using System.Text;
using System.Collections;

namespace org.javacc.parser;


public abstract class JavaCCParserInternals : JavaCCGlobals
{
	private static ArrayList add_cu_token_here;

	private static Token first_cu_token;

	private static bool insertionpoint1set;

	private static bool insertionpoint2set;

	private static int nextFreeLexState;


    protected internal static bool Hexchar(char ch) => ch switch
    {
        >= '0' and <= '9' => true,
        >= 'A' and <= 'F' => true,
        >= 'a' and <= 'f' => true,
        _ => false
    };

    protected internal static int HexVal(char ch) => ch switch
    {
        >= '0' and <= '9' => ch - 48,
        >= 'A' and <= 'F' => ch - 65 + 10,
        _ => ch - 97 + 10
    };


    public JavaCCParserInternals()
	{
	}

	
	protected internal static void Initialize()
	{
		int integer = (0);
		JavaCCGlobals.lexstate_S2I.Add("DEFAULT", integer);
		JavaCCGlobals.lexstate_I2S.Add(integer, "DEFAULT");
		JavaCCGlobals.simple_tokens_table.Add("DEFAULT", new Hashtable());
	}

	protected internal static void AddCuname(string str)
	{
		JavaCCGlobals.cu_name = str;
	}

	
	protected internal static void Compare(Token t, string str1, string str2)
	{
		if (!string.Equals(str2, str1))
		{
			JavaCCErrors.Parse_Error(t, new StringBuilder().Append("Name ").Append(str2).Append(" must be the same as that used at PARSER_BEGIN (")
				.Append(str1)
				.Append(")")
				.ToString());
		}
	}

	
	protected internal static void SetInsertionPoint(Token t, int i)
	{
		do
		{
			add_cu_token_here.Add(first_cu_token);
			first_cu_token = first_cu_token.next;
		}
		while (first_cu_token != t);
		if (i == 1)
		{
			if (insertionpoint1set)
			{
				JavaCCErrors.Parse_Error(t, "Multiple declaration of parser class.");
			}
			else
			{
				insertionpoint1set = true;
				add_cu_token_here = JavaCCGlobals.cu_to_insertion_point_2;
			}
		}
		else
		{
			add_cu_token_here = JavaCCGlobals.cu_from_insertion_point_2;
			insertionpoint2set = true;
		}
		first_cu_token = t;
	}

	
	protected internal static void InsertionPointError(Token t)
	{
		while (first_cu_token != t)
		{
			add_cu_token_here.Add(first_cu_token);
			first_cu_token = first_cu_token.next;
		}
		if (!insertionpoint1set || !insertionpoint2set)
		{
			JavaCCErrors.Parse_Error(t, "Parser class has not been defined between PARSER_BEGIN and PARSER_END.");
		}
	}

	protected internal static void set_initial_cu_token(Token t)
	{
		first_cu_token = t;
	}

	
	protected internal static void addproduction(NormalProduction np)
	{
		JavaCCGlobals.bnfproductions.Add(np);
	}

	protected internal static void production_addexpansion(BNFProduction bnfp, Expansion e)
	{
		e.parent = bnfp;
		bnfp.expansion = e;
	}

	
	protected internal static void addregexpr(TokenProduction tp)
	{
		JavaCCGlobals.rexprlist.Add(tp);
		if (Options.getUserTokenManager() && (tp.lexStates == null || (nint)tp.lexStates.LongLength != 1 || !string.Equals(tp.lexStates[0], "DEFAULT")))
		{
			JavaCCErrors.Warning(tp, "Ignoring lexical state specifications since option USER_TOKEN_MANAGER has been set to true.");
		}
		if (tp.lexStates == null)
		{
			return;
		}
		for (int i = 0; i < (nint)tp.lexStates.LongLength; i++)
		{
			for (int j = 0; j < i; j++)
			{
				if (string.Equals(tp.lexStates[i], tp.lexStates[j]))
				{
					JavaCCErrors.Parse_Error(tp, new StringBuilder().Append("Multiple occurrence of \"").Append(tp.lexStates[i]).Append("\" in lexical state list.")
						.ToString());
				}
			}
			if (!JavaCCGlobals.lexstate_S2I.TryGetValue(tp.lexStates[i], out var _))
			{
				int integer = (nextFreeLexState++);
				JavaCCGlobals.lexstate_S2I.Add(tp.lexStates[i], integer);
				JavaCCGlobals.lexstate_I2S.Add(integer, tp.lexStates[i]);
				JavaCCGlobals.simple_tokens_table.Add(tp.lexStates[i], new Hashtable());
			}
		}
	}

	
	protected internal static void add_token_manager_decls(Token t, ArrayList v)
	{
		if (JavaCCGlobals.token_mgr_decls != null)
		{
			JavaCCErrors.Parse_Error(t, "Multiple occurrence of \"TOKEN_MGR_DECLS\".");
			return;
		}
		JavaCCGlobals.token_mgr_decls = v;
		if (Options.getUserTokenManager())
		{
			JavaCCErrors.Warning(t, "Ignoring declarations in \"TOKEN_MGR_DECLS\" since option USER_TOKEN_MANAGER has been set to true.");
		}
	}

	
	protected internal static void add_inline_regexpr(RegularExpression re)
	{
		if (!(re is REndOfFile))
		{
			TokenProduction tokenProduction = new TokenProduction();
			tokenProduction.isExplicit = false;
			tokenProduction.lexStates = new string[1] { "DEFAULT" };
			tokenProduction.kind = 0;
			RegExprSpec regExprSpec = new RegExprSpec();
			regExprSpec.rexp = re;
			regExprSpec.rexp.tpContext = tokenProduction;
			regExprSpec.act = new Action();
			regExprSpec.nextState = null;
			regExprSpec.nsTok = null;
			tokenProduction.respecs.Add(regExprSpec);
			JavaCCGlobals.rexprlist.Add(tokenProduction);
		}
	}

	
	protected internal static string remove_escapes_and_quotes(Token t, string str)
	{
		string text = "";
		int num = 1;
		while (num < str.Length - 1)
		{
			if (str[num] != '\\')
			{
				text = new StringBuilder().Append(text).Append(str[num]).ToString();
				num++;
				continue;
			}
			num++;
			int num2 = str[num];
			switch (num2)
			{
			case 98:
				text = new StringBuilder().Append(text).Append('\b').ToString();
				num++;
				continue;
			case 116:
				text = new StringBuilder().Append(text).Append('\t').ToString();
				num++;
				continue;
			case 110:
				text = new StringBuilder().Append(text).Append('\n').ToString();
				num++;
				continue;
			case 102:
				text = new StringBuilder().Append(text).Append('\f').ToString();
				num++;
				continue;
			case 114:
				text = new StringBuilder().Append(text).Append('\r').ToString();
				num++;
				continue;
			case 34:
				text = new StringBuilder().Append(text).Append('"').ToString();
				num++;
				continue;
			case 39:
				text = new StringBuilder().Append(text).Append('\'').ToString();
				num++;
				continue;
			case 92:
				text = new StringBuilder().Append(text).Append('\\').ToString();
				num++;
				continue;
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			{
				int num3 = num2 - 48;
				num++;
				int num4 = str[num];
				if (num4 >= 48 && num4 <= 55)
				{
					num3 = num3 * 8 + num4 - 48;
					num++;
					num4 = str[num];
					if (num2 <= 51 && num4 >= 48 && num4 <= 55)
					{
						num3 = num3 * 8 + num4 - 48;
						num++;
					}
				}
				text = new StringBuilder().Append(text).Append((char)num3).ToString();
				continue;
			}
			}
			if (num2 == 117)
			{
				num++;
				num2 = str[num];
				if (Hexchar((char)num2))
				{
					int num3 = HexVal((char)num2);
					num++;
					num2 = str[num];
					if (Hexchar((char)num2))
					{
						num3 = num3 * 16 + HexVal((char)num2);
						num++;
						num2 = str[num];
						if (Hexchar((char)num2))
						{
							num3 = num3 * 16 + HexVal((char)num2);
							num++;
							num2 = str[num];
							if (Hexchar((char)num2))
							{
								_ = num3 * 16 + HexVal((char)num2);
								num++;
								continue;
							}
						}
					}
				}
				JavaCCErrors.Parse_Error(t, new StringBuilder().Append("Encountered non-hex character '").Append((char)num2).Append("' at position ")
					.Append(num)
					.Append(" of string ")
					.Append("- Unicode escape must have 4 hex digits after it.")
					.ToString());
				return text;
			}
			JavaCCErrors.Parse_Error(t, new StringBuilder().Append("Illegal escape sequence '\\").Append((char)num2).Append("' at position ")
				.Append(num)
				.Append(" of string.")
				.ToString());
			return text;
		}
		return text;
	}

	
	protected internal static char character_descriptor_assign(Token t, string str)
	{
		if (str.Length != 1)
		{
			JavaCCErrors.Parse_Error(t, "String in character list may contain only one character.");
			return ' ';
		}
		char result = str[0];
		
		return result;
	}

	
	protected internal static char character_descriptor_assign(Token t, string str1, string str2)
	{
		if (str1.Length != 1)
		{
			JavaCCErrors.Parse_Error(t, "String in character list may contain only one character.");
			return ' ';
		}
		if (str2[0] > str1[0])
		{
			JavaCCErrors.Parse_Error(t, new StringBuilder().Append("Right end of character range '").Append(str1).Append("' has a lower ordinal value than the left end of character range '")
				.Append(str2)
				.Append("'.")
				.ToString());
			char result = str2[0];
			
			return result;
		}
		char result2 = str1[0];
		
		return result2;
	}

	
	protected internal static void makeTryBlock(Token t, Container c1, Container c2, ArrayList v1, ArrayList v2, ArrayList v3, ArrayList v4)
	{
		if (v3.Count == 0 && v4 == null)
		{
			JavaCCErrors.Parse_Error(t, "Try block must contain at least one catch or finally block.");
			return;
		}
		TryBlock tryBlock = new TryBlock();
		tryBlock.Line = t.BeginLine;
		tryBlock.Column = t.BeginColumn;
		tryBlock.exp = (Expansion)c2.member;
		tryBlock.exp.parent = tryBlock;
		tryBlock.exp.ordinal = 0;
		tryBlock.types = v1;
		tryBlock.ids = v2;
		tryBlock.catchblks = v3;
		tryBlock.finallyblk = v4;
		c1.member = tryBlock;
	}

	public new static void reInit()
	{
		add_cu_token_here = JavaCCGlobals.cu_to_insertion_point_1;
		first_cu_token = null;
		insertionpoint1set = false;
		insertionpoint2set = false;
		nextFreeLexState = 1;
	}

	static JavaCCParserInternals()
	{
		
		add_cu_token_here = JavaCCGlobals.cu_to_insertion_point_1;
		insertionpoint1set = false;
		insertionpoint2set = false;
		nextFreeLexState = 1;
	}
}
