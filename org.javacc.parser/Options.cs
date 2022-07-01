using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace org.javacc.parser;


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
		int num = String.instancehelper_indexOf(@this, 61);
		int num2 = String.instancehelper_indexOf(@this, 58);
		int num3 = ((num < 0) ? num2 : ((num2 < 0) ? num : ((num >= num2) ? num2 : num)));
		string text;
		object obj;
		if (num3 < 0)
		{
			text = String.instancehelper_toUpperCase(@this);
			if (OptionValues.ContainsKey(text))
			{
				obj = true;
			}
			else
			{
				if (String.instancehelper_length(text) <= 2 || String.instancehelper_charAt(text, 0) != 'N' || String.instancehelper_charAt(text, 1) != 'O')
				{
					Console.WriteLine(new StringBuilder().Append("Warning: Bad option \"").Append(str).Append("\" will be ignored.")
						.ToString());
					return;
				}
				obj = false;
				text = String.instancehelper_substring(text, 2);
			}
		}
		else
		{
			text = String.instancehelper_toUpperCase(String.instancehelper_substring(@this, 0, num3));
			if (String.instancehelper_equalsIgnoreCase(String.instancehelper_substring(@this, num3 + 1), "TRUE"))
			{
				obj = true;
			}
			else if (String.instancehelper_equalsIgnoreCase(String.instancehelper_substring(@this, num3 + 1), "FALSE"))
			{
				obj = false;
			}
			else
			{
				try
				{
					int num4 = int.parseInt(String.instancehelper_substring(@this, num3 + 1));
					if (num4 <= 0)
					{
						Console.WriteLine(new StringBuilder().Append("Warning: Bad option value in \"").Append(str).Append("\" will be ignored.")
							.ToString());
						return;
					}
					obj = (num4);
				}
				catch (NumberFormatException)
				{
					goto IL_017b;
				}
			}
		}
		goto IL_01c9;
		IL_017b:
		
		obj = String.instancehelper_substring(@this, num3 + 1);
		if (@this.Length > num3 + 2 && String.instancehelper_charAt(@this, num3 + 1) == '"' && String.instancehelper_charAt(@this, @this.Length - 1) == '"')
		{
			obj = String.instancehelper_substring(@this, num3 + 2, @this.Length - 1);
		}
		goto IL_01c9;
		IL_01c9:
		if (!OptionValues.ContainsKey(text))
		{
			Console.WriteLine(new StringBuilder().Append("Warning: Bad option \"").Append(str).Append("\" will be ignored.")
				.ToString());
			return;
		}
		object this2 = OptionValues.get(text);
		if (Object.instancehelper_getClass(obj) != Object.instancehelper_getClass(this2))
		{
			Console.WriteLine(new StringBuilder().Append("Warning: Bad option value in \"").Append(str).Append("\" will be ignored.")
				.ToString());
			return;
		}
		if (cmdLineSetting.Contains(text))
		{
			Console.WriteLine(new StringBuilder().Append("Warning: Duplicate option setting \"").Append(str).Append("\" will be ignored.")
				.ToString());
			return;
		}
		obj = upgradeValue(text, obj);
		OptionValues.Add(text, obj);
		cmdLineSetting.Add(text);
	}

	
	internal static Type class_0024(string P_0)
	{
		//Discarded unreachable code: IL_000d
		java.lang.ClassNotFoundException ex;
		try
		{
			return Class.forName(P_0, Options.___003CGetCallerID_003E());
		}
		catch (java.lang.ClassNotFoundException x)
		{
			ex = ByteCodeHelper.MapException<java.lang.ClassNotFoundException>(x, ByteCodeHelper.MapFlags.NoRemapping);
		}
		java.lang.ClassNotFoundException cause = ex;
		throw new System.Exception(Throwable.instancehelper_initCause(new java.lang.NoClassDefFoundError(), cause));
	}

	
	public static object upgradeValue(string str, object obj)
	{
		if (String.instancehelper_equalsIgnoreCase(str, "NODE_FACTORY") && Object.instancehelper_getClass(obj) == ((class_0024java_0024lang_0024Boolean != null) ? 
			class_0024java_0024lang_0024Boolean : (class_0024java_0024lang_0024Boolean = class_0024("java.lang.Boolean"))))
		{
			obj = ((!(obj.booleanValue()) ? "" : "*");
		}
		return obj;
	}

	
	public static bool getDebugLookahead()
	{
		return BooleanValue("DEBUG_LOOKAHEAD");
	}

	
	public static bool getDebugParser()
	{
		return BooleanValue("DEBUG_PARSER");
	}

	
	protected internal static int IntValue(string str)
	{
		return  ((int)OptionValues.get(str)).intValue();
	}

	
	protected internal static bool BooleanValue(string str)
	{
		return (OptionValues.get(str)).booleanValue();
	}

	
	protected internal static string StringValue(string str)
	{
		return (string)OptionValues.get(str);
	}

	
	public static string getJdkVersion()
	{
		return StringValue("JDK_VERSION");
	}

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
			stringBuilder.Append(OptionValues.get(text));
			if (i != (nint)strarr.LongLength - 1)
			{
				stringBuilder.Append(',');
			}
		}
		return stringBuilder.ToString();
	}

	
	public static void setInputFileOption(object obj1, object obj2, string str, object obj3)
	{
		string text = str.ToUpper();
		if (!OptionValues.ContainsKey(text))
		{
			JavaCCErrors.Warning(obj1, new StringBuilder().Append("Bad option name \"").Append(str).Append("\".  Option setting will be ignored.")
				.ToString());
			return;
		}
		object obj4 = OptionValues.get(text);
		obj3 = upgradeValue(str, obj3);
		if (obj4 != null)
		{
			if (Object.instancehelper_getClass(obj4) != Object.instancehelper_getClass(obj3) || (obj3 is int && ((int)obj3).intValue() <= 0))
			{
				JavaCCErrors.Warning(obj2, new StringBuilder().Append("Bad option value \"").Append(obj3).Append("\" for \"")
					.Append(str)
					.Append("\".  Option setting will be ignored.")
					.ToString());
				return;
			}
			if (inputFileSetting.Contains(text))
			{
				JavaCCErrors.Warning(obj1, new StringBuilder().Append("Duplicate option setting for \"").Append(str).Append("\" will be ignored.")
					.ToString());
				return;
			}
			if (cmdLineSetting.Contains(text))
			{
				if (!Object.instancehelper_equals(obj4, obj3))
				{
					JavaCCErrors.Warning(obj1, new StringBuilder().Append("Command line setting of \"").Append(str).Append("\" modifies option value in file.")
						.ToString());
				}
				return;
			}
		}
		OptionValues.Add(text, obj3);
		inputFileSetting.Add(text);
	}

	
	public static void normalize()
	{
		if (getDebugLookahead() && !getDebugParser())
		{
			if (cmdLineSetting.Contains("DEBUG_PARSER") || inputFileSetting.Contains("DEBUG_PARSER"))
			{
				JavaCCErrors.Warning("True setting of option DEBUG_LOOKAHEAD overrides false setting of option DEBUG_PARSER.");
			}
			OptionValues.Add("DEBUG_PARSER", true);
		}
	}

	
	public static int getLookahead()
	{
		return IntValue("LOOKAHEAD");
	}

	
	public static int getChoiceAmbiguityCheck()
	{
		return IntValue("CHOICE_AMBIGUITY_CHECK");
	}

	
	public static int getOtherAmbiguityCheck()
	{
		return IntValue("OTHER_AMBIGUITY_CHECK");
	}

	
	public static bool getDebugTokenManager()
	{
		return BooleanValue("DEBUG_TOKEN_MANAGER");
	}

	
	public static bool getErrorReporting()
	{
		return BooleanValue("ERROR_REPORTING");
	}

	
	public static bool getJavaUnicodeEscape()
	{
		return BooleanValue("JAVA_UNICODE_ESCAPE");
	}

	
	public static bool getUnicodeInput()
	{
		return BooleanValue("UNICODE_INPUT");
	}

	
	public static bool getIgnoreCase()
	{
		return BooleanValue("IGNORE_CASE");
	}

	
	public static bool getUserTokenManager()
	{
		return BooleanValue("USER_TOKEN_MANAGER");
	}

	
	public static bool getUserCharStream()
	{
		return BooleanValue("USER_CHAR_STREAM");
	}

	
	public static bool getBuildParser()
	{
		return BooleanValue("BUILD_PARSER");
	}

	
	public static bool getBuildTokenManager()
	{
		return BooleanValue("BUILD_TOKEN_MANAGER");
	}

	
	public static bool getTokenManagerUsesParser()
	{
		return BooleanValue("TOKEN_MANAGER_USES_PARSER");
	}

	
	public static bool getSanityCheck()
	{
		return BooleanValue("SANITY_CHECK");
	}

	
	public static bool getForceLaCheck()
	{
		return BooleanValue("FORCE_LA_CHECK");
	}

	
	public static bool getCommonTokenAction()
	{
		return BooleanValue("COMMON_TOKEN_ACTION");
	}

	
	public static bool getCacheTokens()
	{
		return BooleanValue("CACHE_TOKENS");
	}

	
	public static bool getKeepLineColumn()
	{
		return BooleanValue("KEEP_LINE_COLUMN");
	}

	
	public static string getTokenExtends()
	{
		return StringValue("TOKEN_EXTENDS");
	}

	
	public static string getTokenFactory()
	{
		return StringValue("TOKEN_FACTORY");
	}

	
	public static FileInfo getOutputDirectory()
	{

		return new FileInfo(StringValue("OUTPUT_DIRECTORY"));
	}

	
	public static string getStringBufOrBuild()
	{
		return nameof(StringBuilder);

	}
}
