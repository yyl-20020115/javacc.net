using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace org.javacc.parser;


public class Options
{
	protected internal static Dictionary<string,object> optionValues;

	private static HashSet<string> cmdLineSetting;

	private static HashSet<string> inputFileSetting;

	internal static Type class_0024java_0024lang_0024Boolean;

	
	//private static CallerID ___003CcallerID_003E;

	
	public static bool getStatic()
	{
		bool result = booleanValue("STATIC");
		
		return result;
	}

	
	public static bool isOption(string str)
	{
		return (str != null && str.Length > 1 && str[0] == '-') ? true : false;
	}

	
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
			if (optionValues.ContainsKey(text))
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
					obj = new int(num4);
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
		if (!optionValues.ContainsKey(text))
		{
			Console.WriteLine(new StringBuilder().Append("Warning: Bad option \"").Append(str).Append("\" will be ignored.")
				.ToString());
			return;
		}
		object this2 = optionValues.get(text);
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
		optionValues.Add(text, obj);
		cmdLineSetting.Add(text);
	}

	
	internal static Class class_0024(string P_0)
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
		if (String.instancehelper_equalsIgnoreCase(str, "NODE_FACTORY") && Object.instancehelper_getClass(obj) == ((class_0024java_0024lang_0024Boolean != null) ? class_0024java_0024lang_0024Boolean : (class_0024java_0024lang_0024Boolean = class_0024("java.lang.Boolean"))))
		{
			obj = ((!((Boolean)obj).booleanValue()) ? "" : "*");
		}
		return obj;
	}

	
	public static bool getDebugLookahead()
	{
		bool result = booleanValue("DEBUG_LOOKAHEAD");
		
		return result;
	}

	
	public static bool getDebugParser()
	{
		bool result = booleanValue("DEBUG_PARSER");
		
		return result;
	}

	
	protected internal static int intValue(string str)
	{
		int result = ((int)optionValues.get(str)).intValue();
		
		return result;
	}

	
	protected internal static bool booleanValue(string str)
	{
		bool result = ((Boolean)optionValues.get(str)).booleanValue();
		
		return result;
	}

	
	protected internal static string stringValue(string str)
	{
		return (string)optionValues.get(str);
	}

	
	public static string getJdkVersion()
	{
		string result = stringValue("JDK_VERSION");
		
		return result;
	}

	
	protected internal Options()
	{
	}

	
	public static void init()
	{
		optionValues = new HashMap();
		cmdLineSetting = new HashSet();
		inputFileSetting = new HashSet();
		optionValues.Add("LOOKAHEAD", new int(1));
		optionValues.Add("CHOICE_AMBIGUITY_CHECK", new int(2));
		optionValues.Add("OTHER_AMBIGUITY_CHECK", new int(1));
		optionValues.Add("STATIC", true);
		optionValues.Add("DEBUG_PARSER", false);
		optionValues.Add("DEBUG_LOOKAHEAD", false);
		optionValues.Add("DEBUG_TOKEN_MANAGER", false);
		optionValues.Add("ERROR_REPORTING", true);
		optionValues.Add("JAVA_UNICODE_ESCAPE", false);
		optionValues.Add("UNICODE_INPUT", false);
		optionValues.Add("IGNORE_CASE", false);
		optionValues.Add("USER_TOKEN_MANAGER", false);
		optionValues.Add("USER_CHAR_STREAM", false);
		optionValues.Add("BUILD_PARSER", true);
		optionValues.Add("BUILD_TOKEN_MANAGER", true);
		optionValues.Add("TOKEN_MANAGER_USES_PARSER", false);
		optionValues.Add("SANITY_CHECK", true);
		optionValues.Add("FORCE_LA_CHECK", false);
		optionValues.Add("COMMON_TOKEN_ACTION", false);
		optionValues.Add("CACHE_TOKENS", false);
		optionValues.Add("KEEP_LINE_COLUMN", true);
		optionValues.Add("OUTPUT_DIRECTORY", ".");
		optionValues.Add("JDK_VERSION", "1.4");
		optionValues.Add("TOKEN_EXTENDS", "");
		optionValues.Add("TOKEN_FACTORY", "");
	}

	
	public static string getOptionsString(string[] strarr)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < (nint)strarr.LongLength; i++)
		{
			string text = strarr[i];
			stringBuilder.Append(text);
			stringBuilder.Append('=');
			stringBuilder.Append(optionValues.get(text));
			if (i != (nint)strarr.LongLength - 1)
			{
				stringBuilder.Append(',');
			}
		}
		string result = stringBuilder.ToString();
		
		return result;
	}

	
	public static void setInputFileOption(object obj1, object obj2, string str, object obj3)
	{
		string text = String.instancehelper_toUpperCase(str);
		if (!optionValues.ContainsKey(text))
		{
			JavaCCErrors.Warning(obj1, new StringBuilder().Append("Bad option name \"").Append(str).Append("\".  Option setting will be ignored.")
				.ToString());
			return;
		}
		object obj4 = optionValues.get(text);
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
		optionValues.Add(text, obj3);
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
			optionValues.Add("DEBUG_PARSER", true);
		}
	}

	
	public static int getLookahead()
	{
		int result = intValue("LOOKAHEAD");
		
		return result;
	}

	
	public static int getChoiceAmbiguityCheck()
	{
		int result = intValue("CHOICE_AMBIGUITY_CHECK");
		
		return result;
	}

	
	public static int getOtherAmbiguityCheck()
	{
		int result = intValue("OTHER_AMBIGUITY_CHECK");
		
		return result;
	}

	
	public static bool getDebugTokenManager()
	{
		bool result = booleanValue("DEBUG_TOKEN_MANAGER");
		
		return result;
	}

	
	public static bool getErrorReporting()
	{
		bool result = booleanValue("ERROR_REPORTING");
		
		return result;
	}

	
	public static bool getJavaUnicodeEscape()
	{
		bool result = booleanValue("JAVA_UNICODE_ESCAPE");
		
		return result;
	}

	
	public static bool getUnicodeInput()
	{
		bool result = booleanValue("UNICODE_INPUT");
		
		return result;
	}

	
	public static bool getIgnoreCase()
	{
		bool result = booleanValue("IGNORE_CASE");
		
		return result;
	}

	
	public static bool getUserTokenManager()
	{
		bool result = booleanValue("USER_TOKEN_MANAGER");
		
		return result;
	}

	
	public static bool getUserCharStream()
	{
		bool result = booleanValue("USER_CHAR_STREAM");
		
		return result;
	}

	
	public static bool getBuildParser()
	{
		bool result = booleanValue("BUILD_PARSER");
		
		return result;
	}

	
	public static bool getBuildTokenManager()
	{
		bool result = booleanValue("BUILD_TOKEN_MANAGER");
		
		return result;
	}

	
	public static bool getTokenManagerUsesParser()
	{
		bool result = booleanValue("TOKEN_MANAGER_USES_PARSER");
		
		return result;
	}

	
	public static bool getSanityCheck()
	{
		bool result = booleanValue("SANITY_CHECK");
		
		return result;
	}

	
	public static bool getForceLaCheck()
	{
		bool result = booleanValue("FORCE_LA_CHECK");
		
		return result;
	}

	
	public static bool getCommonTokenAction()
	{
		bool result = booleanValue("COMMON_TOKEN_ACTION");
		
		return result;
	}

	
	public static bool getCacheTokens()
	{
		bool result = booleanValue("CACHE_TOKENS");
		
		return result;
	}

	
	public static bool getKeepLineColumn()
	{
		bool result = booleanValue("KEEP_LINE_COLUMN");
		
		return result;
	}

	
	public static string getTokenExtends()
	{
		string result = stringValue("TOKEN_EXTENDS");
		
		return result;
	}

	
	public static string getTokenFactory()
	{
		string result = stringValue("TOKEN_FACTORY");
		
		return result;
	}

	
	public static FileInfo getOutputDirectory()
	{

		FileInfo result = new FileInfo(stringValue("OUTPUT_DIRECTORY"));
		
		return result;
	}

	
	public static string stringBufOrBuild()
	{
		if (string.Equals(getJdkVersion(), "1.5") || string.Equals(getJdkVersion(), "1.6"))
		{
			return "StringBuilder";
		}
		return "StringBuilder";
	}

	static CallerID ___003CGetCallerID_003E()
	{
		if (___003CcallerID_003E == null)
		{
			___003CcallerID_003E = new ___003CCallerID_003E();
		}
		return ___003CcallerID_003E;
	}
}
