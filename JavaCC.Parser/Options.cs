namespace JavaCC.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Options
{
	protected internal static Dictionary<string,object> OptionValues = new();
	private static HashSet<string> cmdLineSetting;
	private static HashSet<string> inputFileSetting;

    public static bool getStatic() => BooleanValue("STATIC");


    public static bool IsOption(string str) => str != null && str.Length > 1 && str[0] == '-';


    public static void setCmdLineOption(string str)
	{
		string @this = ((str[0] != '-') ? str : str.Substring(1));
		int num = @this.IndexOf((char)61);
		int num2 = @this.IndexOf((char)58);
		int num3 = ((num < 0) ? num2 : ((num2 < 0) ? num : ((num >= num2) ? num2 : num)));
		string text;
		object obj;
		if (num3 < 0)
		{
			text =(@this).ToUpper();
			if (OptionValues.ContainsKey(text))
			{
				obj = true;
			}
			else
			{
				if (text.Length <= 2 || text[0] != 'N' || text[1] != 'O')
				{
					Console.WriteLine(("Warning: Bad option \"")+(str)+("\" will be ignored.")
						);
					return;
				}
				obj = false;
				text = text.Substring(2);
			}
		}
		else
		{
			text = (@this.Substring( 0, num3)).ToUpper();
			if (String.instancehelper_equalsIgnoreCase(@this.Substring(num3 + 1), "TRUE"))
			{
				obj = true;
			}
			else if (String.instancehelper_equalsIgnoreCase(@this.Substring(num3 + 1), "FALSE"))
			{
				obj = false;
			}
			else
			{
				try
				{
					int num4 = int.parseInt(@this.Substring( num3 + 1));
					if (num4 <= 0)
					{
						Console.WriteLine(("Warning: Bad option value in \"")+(str)+("\" will be ignored.")
							);
						return;
					}
					obj = (num4); 
				}
				catch (Exception)
				{
					goto IL_017b;
				}
			}
		}
		goto IL_01c9;
		IL_017b:
		
		obj = @this.Substring( num3 + 1);
		if (@this.Length > num3 + 2 && (@this[num3 + 1]) == '"' && (@this[@this.Length - 1]) == '"')
		{
			obj = @this.Substring( num3 + 2, @this.Length - 1);
		}
		goto IL_01c9;
		IL_01c9:
		if (!OptionValues.ContainsKey(text))
		{
			Console.WriteLine(("Warning: Bad option \"")+(str)+("\" will be ignored.")
				);
			return;
		}
		object this2 = OptionValues.get(text);
		if (Object.instancehelper_getClass(obj) != Object.instancehelper_getClass(this2))
		{
			Console.WriteLine(("Warning: Bad option value in \"")+(str)+("\" will be ignored.")
				);
			return;
		}
		if (cmdLineSetting.Contains(text))
		{
			Console.WriteLine(("Warning: Duplicate option setting \"")+(str)+("\" will be ignored.")
				);
			return;
		}
		obj = upgradeValue(text, obj);
		OptionValues.Add(text, obj);
		cmdLineSetting.Add(text);
	}

	
	internal static Type class_0024(string P_0)
	{
		//Discarded unreachable code: IL_000d
		
		try
		{
			return Type.GetType(P_0);
		}
		catch (Exception x)
		{
		}
		throw new System.Exception("Type Not Found");
	}

	
	public static object upgradeValue(string str, object obj)
	{
		if (String.Equals(str, "NODE_FACTORY", StringComparison.OrdinalIgnoreCase) && 
			Object.instancehelper_getClass(obj) == ((class_0024java_0024lang_0024Boolean != null) ? 
			class_0024java_0024lang_0024Boolean : (class_0024java_0024lang_0024Boolean = class_0024("java.lang.Boolean"))))
		{
			obj = (!(obj is bool b && b) ? "" : "*");
		}
		return obj;
	}


    public static bool DebugLookahead => BooleanValue("DEBUG_LOOKAHEAD");


    public static bool DebugParser => BooleanValue("DEBUG_PARSER");

    protected internal static int IntValue(string str) 
		=> OptionValues.TryGetValue(str, out var v) && v is int i ? i : 0;// ((int)OptionValues.get(str));

    protected internal static bool BooleanValue(string str) 
		=> (OptionValues.TryGetValue(str, out var s) && s is bool _s) && _s;

    protected internal static string StringValue(string str) 
		=> (OptionValues.TryGetValue(str, out var s) && s is string _s) ? _s : null;

    public static string JdkVersion => StringValue("JDK_VERSION");

    public static void Init()
	{
		OptionValues = new();
		cmdLineSetting = new();
		inputFileSetting = new();
		OptionValues.Add("LOOKAHEAD", (1));
		OptionValues.Add("CHOICE_AMBIGUITY_CHECK", (2));
		OptionValues.Add("OTHER_AMBIGUITY_CHECK", (1));
		OptionValues.Add("STATIC", true);
		OptionValues.Add("DEBUG_PARSER", false);
		OptionValues.Add("DEBUG_LOOKAHEAD", false);
		OptionValues.Add("DEBUG_TOKEN_MANAGER", false);
		OptionValues.Add("ERROR_REPORTING", true);
		OptionValues.Add("JAVA_UNICODE_ESCAPE", false);
		OptionValues.Add("UNICODE_INPUT", false);
		OptionValues.Add("IGNORE_CASE", false);
		OptionValues.Add("USER_TOKEN_MANAGER", false);
		OptionValues.Add("USER_CHAR_STREAM", false);
		OptionValues.Add("BUILD_PARSER", true);
		OptionValues.Add("BUILD_TOKEN_MANAGER", true);
		OptionValues.Add("TOKEN_MANAGER_USES_PARSER", false);
		OptionValues.Add("SANITY_CHECK", true);
		OptionValues.Add("FORCE_LA_CHECK", false);
		OptionValues.Add("COMMON_TOKEN_ACTION", false);
		OptionValues.Add("CACHE_TOKENS", false);
		OptionValues.Add("KEEP_LINE_COLUMN", true);
		OptionValues.Add("OUTPUT_DIRECTORY", ".");
		OptionValues.Add("JDK_VERSION", "1.4");
		OptionValues.Add("TOKEN_EXTENDS", "");
		OptionValues.Add("TOKEN_FACTORY", "");
	}

	
	public static string getOptionsString(string[] strarr)
	{
		var stringBuilder = new StringBuilder();
		for (int i = 0; i < (nint)strarr.LongLength; i++)
		{
			string text = strarr[i];
			stringBuilder.Append(text);
			stringBuilder.Append('=');
			if(OptionValues.TryGetValue(text, out var val))
            {
				stringBuilder.Append(val);
			}
			if (i != (nint)strarr.LongLength - 1)
			{
				stringBuilder.Append(',');
			}
		}
		return stringBuilder.ToString();
	}

	
	public static void SetInputFileOption(object obj1, object obj2, string str, object obj3)
	{
		string text = str.ToUpper();
		if (!OptionValues.ContainsKey(text))
		{
			JavaCCErrors.Warning(obj1, ("Bad option name \"")+(str)+("\".  Option setting will be ignored.")
				);
			return;
		}
		object obj4 = OptionValues.get(text);
		obj3 = upgradeValue(str, obj3);
		if (obj4 != null)
		{
			if (Object.instancehelper_getClass(obj4) != Object.instancehelper_getClass(obj3) || (obj3 is int && ((int)obj3) <= 0))
			{
				JavaCCErrors.Warning(obj2, ("Bad option value \"")+(obj3)+("\" for \"")
					+(str)
					+("\".  Option setting will be ignored.")
					);
				return;
			}
			if (inputFileSetting.Contains(text))
			{
				JavaCCErrors.Warning(obj1, ("Duplicate option setting for \"")+(str)+("\" will be ignored.")
					);
				return;
			}
			if (cmdLineSetting.Contains(text))
			{
				if (!Object.instancehelper_equals(obj4, obj3))
				{
					JavaCCErrors.Warning(obj1, ("Command line setting of \"")+(str)+("\" modifies option value in file.")
						);
				}
				return;
			}
		}
		OptionValues.Add(text, obj3);
		inputFileSetting.Add(text);
	}

	
	public static void Normalize()
	{
		if (DebugLookahead && !DebugParser)
		{
			if (cmdLineSetting.Contains("DEBUG_PARSER") || inputFileSetting.Contains("DEBUG_PARSER"))
			{
				JavaCCErrors.Warning("True setting of option DEBUG_LOOKAHEAD overrides false setting of option DEBUG_PARSER.");
			}
			OptionValues.Add("DEBUG_PARSER", true);
		}
	}


    public static int Lookahead => IntValue("LOOKAHEAD");


    public static int ChoiceAmbiguityCheck => IntValue("CHOICE_AMBIGUITY_CHECK");


    public static int OtherAmbiguityCheck => IntValue("OTHER_AMBIGUITY_CHECK");


    public static bool DebugTokenManager => BooleanValue("DEBUG_TOKEN_MANAGER");


    public static bool ErrorReporting => BooleanValue("ERROR_REPORTING");


    public static bool JavaUnicodeEscape => BooleanValue("JAVA_UNICODE_ESCAPE");


    public static bool UnicodeInput => BooleanValue("UNICODE_INPUT");


    public static bool IgnoreCase => BooleanValue("IGNORE_CASE");


    public static bool UserTokenManager => BooleanValue("USER_TOKEN_MANAGER");


    public static bool UserCharStream => BooleanValue("USER_CHAR_STREAM");


    public static bool BuildParser => BooleanValue("BUILD_PARSER");


    public static bool BuildTokenManager => BooleanValue("BUILD_TOKEN_MANAGER");


    public static bool TokenManagerUsesParser => BooleanValue("TOKEN_MANAGER_USES_PARSER");


    public static bool SanityCheck => BooleanValue("SANITY_CHECK");


    public static bool ForceLaCheck => BooleanValue("FORCE_LA_CHECK");


    public static bool CommonTokenAction => BooleanValue("COMMON_TOKEN_ACTION");


    public static bool CacheTokens => BooleanValue("CACHE_TOKENS");


    public static bool KeepLineColumn => BooleanValue("KEEP_LINE_COLUMN");


    public static string TokenExtends => StringValue("TOKEN_EXTENDS");


    public static string TokenFactory => StringValue("TOKEN_FACTORY");


    public static FileInfo OutputDirectory => new (StringValue("OUTPUT_DIRECTORY"));


    public static string StringBufOrBuild => nameof(StringBuilder);
}
