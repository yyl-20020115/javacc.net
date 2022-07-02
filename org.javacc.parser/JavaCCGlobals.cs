using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using javacc.net;
namespace org.javacc.parser;

public class JavaCCGlobals 
{
	protected internal const string toolName = "JavaCC";

	public static string fileName = "";

	public static string origFileName = "";

	public static bool jjtreeGenerated = false;

	public static List<string> toolNames = new();

	public static string cu_name = "";

	public static List<Token> cu_to_insertion_point_1 = new();

	public static List<Token> cu_to_insertion_point_2 = new();

	public static List<Token> cu_from_insertion_point_2 = new();

	public static List<NormalProduction> bnfproductions = new();

	public static Hashtable production_table = new();

	public static Dictionary<string,int> lexstate_S2I = new();

	public static Hashtable lexstate_I2S = new();

	public static ArrayList token_mgr_decls = new();

	public static List<TokenProduction> rexprlist = new();

	public static int tokenCount = 0;

	public static Dictionary<string, RegularExpression> named_tokens_table = new();

	public static List<RegularExpression> ordered_named_tokens = new();

	public static Hashtable names_of_tokens = new();

	public static Hashtable rexps_of_tokens = new();

	public static Hashtable simple_tokens_table = new();

	protected internal static int maskindex = 0;

	protected internal static int jj2index = 0;

	protected internal static ArrayList maskVals = new();

	internal static Action actForEof = new();

	internal static string nextStateForEof = "";

	protected internal static int cline = 0;

	protected internal static int ccol = 0;

		
	public static string GetIdStringList(List<string> l, string str)
	{
		string str2 = "Generated By:";
		int i;
		for (i = 0; i < l.Count - 1; i++)
		{
			str2 = (str2)+((string)l[i])+("&")
				;
		}
		str2 = (str2)+((string)l[i])+(":")
			;
		if (str2.Length > 200)
		{
			Console.WriteLine("Tool names too long.");
			
			throw new System.Exception();
		}
		return (str2) + (" Do not edit this line. ") + (AddUnicodeEscapes(str));
	}

	
	public static string AddUnicodeEscapes(string str)
	{
		string text = "";
		for (int i = 0; i < str.Length; i++)
		{
			int num = str[i];
			if (num < 32 || num > 126)
			{
				string @this = ("0000")+(Utils.ToString(num, 16));
				text = (text)+("\\u")+(@this.Substring(@this.Length - 4, @this.Length))
					;
			}
			else
			{
				text = (text)+((char)num);
			}
		}
		return text;
	}

	
	public static List<string> GetToolNames(string str)
	{
		char[] array = new char[256];
		StreamReader fileReader = null;
		int num = 0;
		List<string> result;
		try
		{
			try
			{
				try
				{
					fileReader = new StreamReader(str);
					int num2;
					while ((num2 = fileReader.Read(array, num, (int)((nint)array.LongLength - num))) != -1 && (nint)(num += num2) != (nint)array.LongLength)
					{
					}
					result = MakeToolNameVector(new string(array, 0, num));
				}
				catch (FileNotFoundException)
				{
					goto IL_006d;
				}
			}
			catch (IOException)
			{
				goto IL_0070;
			}
		}
		catch
		{
			//try-fault
			if (fileReader != null)
			{
				try
				{
					fileReader.Close();
				}
				catch (System.Exception x)
				{
					goto IL_0064;
				}
			}
			goto end_IL_0049;
			IL_0064:
			
			end_IL_0049:
			throw;
		}
		if (fileReader != null)
		{
			try
			{
				fileReader.Close();
				return result;
			}
			catch (System.Exception x2)
			{
			}
			
		}
		return result;
		IL_0070:
		//IOException ex3 = null;
		List<string> result2;
		try
		{
			if (num > 0)
			{
				result2 = MakeToolNameVector(new string(array, 0, num));
				goto IL_010b;
			}
		}
		catch
		{
			//try-fault
			if (fileReader != null)
			{
				try
				{
					fileReader.Close();
				}
				catch (System.Exception x3)
				{
					goto IL_0100;
				}
			}
			goto end_IL_00e5;
			IL_0100:
			
			end_IL_00e5:
			throw;
		}
		if (fileReader != null)
		{
			try
			{
				fileReader.Close();
			}
			catch (System.Exception x4)
			{
				goto IL_014a;
			}
		}
		goto IL_0152;
		IL_00b9:
		
		goto IL_0152;
		IL_0152:
		return new ();
		IL_014a:
		
		goto IL_0152;
		IL_010b:
		if (fileReader != null)
		{
			try
			{
				fileReader.Close();
				return result2;
			}
			catch (System.Exception x5)
			{
			}
			
		}
		return result2;
		IL_006d:
		
		if (fileReader != null)
		{
			try
			{
				fileReader.Close();
			}
			catch (System.Exception x6)
			{
				goto IL_00b9;
			}
		}
		goto IL_0152;
	}

	
	private static List<string> MakeToolNameVector(string name)
	{
		List<string> vector = new ();
		int num = name.IndexOf((char)10);
		if (num == -1)
		{
			num = 1000;
		}
		int num2 = name.IndexOf((char)13);
		if (num2 == -1)
		{
			num2 = 1000;
		}
		int num3 = ((num >= num2) ? num2 : num);
		string @this = ((num3 != 1000) ? name.Substring(0, num3) : name);
		if (@this.IndexOf((char)58) == -1)
		{
			return vector;
		}
		@this = @this.Substring(@this.IndexOf((char) 58) + 1);
		if (@this.IndexOf((char) 58) == -1)
		{
			return vector;
		}
		@this = @this.Substring(0, (@this.IndexOf((char) 58)));
		_ = 0;
		int num4 = 0;
		int num5;
		while (num4 < @this.Length && ((num5 = @this.IndexOf((char) 38, num4)) != -1))
		{
			vector.Add(@this.Substring( num4, num5));
			num4 = num5 + 1;
		}
		if (num4 < @this.Length)
		{
			vector.Add(@this.Substring(num4));
		}
		return vector;
	}

	
	protected internal static void PrintTokenOnly(Token t, TextWriter pw)
	{
		while (cline < t.BeginLine)
		{
			pw.WriteLine("");
			ccol = 1;
			cline++;
		}
		while (ccol < t.BeginColumn)
		{
			pw.Write(" ");
			ccol++;
		}
		if (t.kind == 90 || t.kind == 89)
		{
			pw.Write(AddUnicodeEscapes(t.image));
		}
		else
		{
			pw.Write(t.image);
		}
		cline = t.endLine;
		ccol = t.endColumn + 1;
		int num = t.image[(t.image.Length) - 1];
		if (num == 10 || num == 13)
		{
			cline++;
			ccol = 1;
		}
	}

	
	protected internal static string PrintLeadingComments(Token t)
	{
		string text = "";
		if (t.specialToken == null)
		{
			return text;
		}
		Token token = t.specialToken;
		while (token.specialToken != null)
		{
			token = token.specialToken;
		}
		while (token != null)
		{
			text = (text)+(PrintTokenOnly(token));
			token = token.next;
		}
		if (ccol != 1 && cline != t.BeginLine)
		{
			text = (text)+("\n");
			cline++;
			ccol = 1;
		}
		return text;
	}

	
	protected internal static string PrintTokenOnly(Token t)
	{
		string str = "";
		while (cline < t.BeginLine)
		{
			str = (str)+("\n");
			ccol = 1;
			cline++;
		}
		while (ccol < t.BeginColumn)
		{
			str = (str)+(" ");
			ccol++;
		}
		str = ((t.kind != 90 && t.kind != 89)
			? (str)+(t.image)
			: (str)+(AddUnicodeEscapes(t.image)));
		cline = t.endLine;
		ccol = t.endColumn + 1;
		int num = t.image[(t.image.Length) - 1];
		if (num == 10 || num == 13)
		{
			cline++;
			ccol = 1;
		}
		return str;
	}

	
	public JavaCCGlobals()
	{
	}

	
	public static void BannerLine(string str1, string str2)
	{
		Console.Write(("Java Compiler Compiler Version 4.1d1 (")+(str1));
		if (!string.Equals(str2, ""))
		{
			Console.Write((" Version ")+(str2));
		}
		Console.WriteLine(")");
	}

	
	public static string getIdString(string str1, string str2)
	{
		var vector = new List<string>();
		vector.Add(str1);
		string idString = GetIdStringList(vector, str2);
		
		return idString;
	}

	
	public static bool IsGeneratedBy(string str1, string str2)
	{
		var vector = GetToolNames(str2);
		for (int i = 0; i < vector.Count; i++)
		{
			if (string.Equals(str1, vector[i]))
			{
				return true;
			}
		}
		return false;
	}

	
	public static void CreateOutputDir(FileInfo f)
	{
		if (!f.Exists)
		{
			JavaCCErrors.Warning(("Output directory \"")+(f)+("\" does not exist. Creating the directory.")
				);
			var d = Directory.CreateDirectory(f.FullName);
			if (! d.Exists)
			{
				JavaCCErrors.Semantic_Error(("Cannot create the output directory : ")+(f));
				return;
			}
		}
		if (!new DirectoryInfo(f.FullName).Exists)
		{
			JavaCCErrors.Semantic_Error(("\"")+(f)+(" is not a valid output directory.")
				);
		}
		else if (f.IsReadOnly)
		{
			JavaCCErrors.Semantic_Error(("Cannot write to the output output directory : \"")+(f)+("\"")
				);
		}
	}

	
	public static string StaticOpt()
	{
		if (Options.getStatic())
		{
			return "static ";
		}
		return "";
	}

	
	public static string AddEscapes(string str)
	{
		string text = "";
		for (int i = 0; i < str.Length; i++)
		{
			int num = str[i];
			if (num == 8)
			{
				text = (text)+("\\b");
				continue;
			}
			if (num == 9)
			{
				text = (text)+("\\t");
				continue;
			}
			if (num == 10)
			{
				text = (text)+("\\n");
				continue;
			}
			if (num == 12)
			{
				text = (text)+("\\f");
				continue;
			}
			if (num == 13)
			{
				text = (text)+("\\r");
				continue;
			}
			if (num == 34)
			{
				text = (text)+("\\\"");
				continue;
			}
			if (num == 39)
			{
				text = (text)+("\\'");
				continue;
			}
			switch (num)
			{
			case 92:
				text = (text)+("\\\\");
				break;
			default:
			{
				string @this = ("0000")+(Utils.ToString(num, 16));
				text = (text)+("\\u")+(@this.Substring(@this.Length - 4, @this.Length))
					;
				break;
			}
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
			case 46:
			case 47:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 59:
			case 60:
			case 61:
			case 62:
			case 63:
			case 64:
			case 65:
			case 66:
			case 67:
			case 68:
			case 69:
			case 70:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 81:
			case 82:
			case 83:
			case 84:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 91:
			case 93:
			case 94:
			case 95:
			case 96:
			case 97:
			case 98:
			case 99:
			case 100:
			case 101:
			case 102:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
			case 108:
			case 109:
			case 110:
			case 111:
			case 112:
			case 113:
			case 114:
			case 115:
			case 116:
			case 117:
			case 118:
			case 119:
			case 120:
			case 121:
			case 122:
			case 123:
			case 124:
			case 125:
			case 126:
				text = (text)+((char)num);
				break;
			}
		}
		return text;
	}

	protected internal static void PrintTokenSetup(Token t)
	{
		Token token = t;
		while (token.specialToken != null)
		{
			token = token.specialToken;
		}
		cline = token.BeginLine;
		ccol = token.BeginColumn;
	}

	
	protected internal static void PrintToken(Token t, TextWriter pw)
	{
		Token token = t.specialToken;
		if (token != null)
		{
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				PrintTokenOnly(token, pw);
				token = token.next;
			}
		}
		PrintTokenOnly(t, pw);
	}

	
	protected internal static void PrintLeadingComments(Token t, TextWriter pw)
	{
		if (t.specialToken != null)
		{
			Token token = t.specialToken;
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				PrintTokenOnly(token, pw);
				token = token.next;
			}
			if (ccol != 1 && cline != t.BeginLine)
			{
				pw.WriteLine("");
				cline++;
				ccol = 1;
			}
		}
	}

	
	protected internal static void PrintTrailingComments(Token t, TextWriter pw)
	{
		if (t.next != null)
		{
			PrintLeadingComments(t.next);
		}
	}

	
	protected internal static string PrintToken(Token t)
	{
		string str = "";
		Token token = t.specialToken;
		if (token != null)
		{
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				str = (str)+(PrintTokenOnly(token));
				token = token.next;
			}
		}
		return (str)+(PrintTokenOnly(t));
	}


    protected internal static string PrintTrailingComments(Token t) => t.next == null ? "" : PrintLeadingComments(t.next);


    public static void ReInit()
	{
		fileName = null;
		origFileName = null;
		jjtreeGenerated = false;
		toolNames = null;
		cu_name = null;
		cu_to_insertion_point_1 = new ();
		cu_to_insertion_point_2 = new ();
		cu_from_insertion_point_2 = new ();
		bnfproductions = new ();
		production_table = new ();
		lexstate_S2I = new ();
		lexstate_I2S = new ();
		token_mgr_decls = null;
		rexprlist = new ();
		tokenCount = 0;
		named_tokens_table = new ();
		ordered_named_tokens = new();
		names_of_tokens = new();
		rexps_of_tokens = new();
		simple_tokens_table = new();
		maskindex = 0;
		jj2index = 0;
		maskVals = new();
		cline = 0;
		ccol = 0;
		actForEof = null;
		nextStateForEof = null;
	}

	static JavaCCGlobals()
	{
		cu_to_insertion_point_1 = new();
		cu_to_insertion_point_2 = new();
		cu_from_insertion_point_2 = new();
		bnfproductions = new();
		production_table = new();
		lexstate_S2I = new ();
		lexstate_I2S = new();
		rexprlist = new();
		named_tokens_table = new();
		ordered_named_tokens = new();
		names_of_tokens = new();
		rexps_of_tokens = new();
		simple_tokens_table = new();
		maskindex = 0;
		jj2index = 0;
		maskVals = new();
	}
}
