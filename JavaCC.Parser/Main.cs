using System;
using System.IO;
using System.Text;
namespace Javacc.Parser;

public static class EntryPoint
{
	public static int Main(string[] strarr)
	{
		int status = mainProgram(strarr);
		return status;
	}

	public static void reInitAll()
	{
		Expansion.ReInit();
		//JavaCCErrors.ReInit();
		JavaCCGlobals.ReInit();
		Options.Init();
		JavaCCParserInternals.ReInit();
		RStringLiteral.ReInit();
		JavaFiles.ReInit();
		LexGen.reInit();
		NfaState.reInit();
		MatchInfo.ReInit();
		LookaheadWalk.ReInit();
		Semanticize.ReInit();
		ParseGen.reInit();
		OtherFilesGen.reInit();
		ParseEngine.ReInit();
	}


	public static int mainProgram(string[] strarr)
	{
		//Discarded unreachable code: IL_0349
		reInitAll();
		JavaCCGlobals.BannerLine("Parser Generator", "");

		if (strarr.Length == 0)
		{
			Console.WriteLine("");
			help_message();
			return 1;
		}
		Console.WriteLine("(type \"javacc\" with no arguments for help)");
		if (Options.IsOption(strarr[strarr.Length - 1]))
		{
			Console.WriteLine(("Last argument \"")+(strarr[strarr.Length - 1])+("\" is not a filename.")
				);
			return 1;
		}
		for (int i = 0; i < (nint)strarr.LongLength - 1; i++)
		{
			if (!Options.IsOption(strarr[i]))
			{
				Console.WriteLine(("Argument \"")+(strarr[i])+("\" must be an option setting.")
					);
				return 1;
			}
			Options.setCmdLineOption(strarr[i]);
		}
		JavaCCParser javaCCParser;
		try
		{
			try
			{

				FileInfo file = new FileInfo(strarr[strarr.Length - 1]);
				if (!file.Exists)
				{
					Console.WriteLine(("File ")+(strarr[strarr.Length - 1])+(" not found.")
						);
					return 1;
				}
				DirectoryInfo di = new DirectoryInfo(strarr[strarr.Length - 1]);
				if (di.Exists)
				{
					Console.WriteLine((strarr[strarr.Length - 1])+(" is a directory. Please use a valid file name."));
					return 1;
				}
				javaCCParser = new JavaCCParser(new StreamReader(strarr[strarr.Length - 1]));
			}
			catch (Exception)
			{
				goto IL_017d;
			}
		}
		catch (FileNotFoundException)
		{
			goto IL_0180;
		}
		ParseException ex4;
		try
		{
			try
			{
				Console.WriteLine(("Reading from file ")+(strarr[strarr.Length - 1])+(" . . .")
					);
				JavaCCGlobals.FileName = (JavaCCGlobals.OrigFileName = strarr[strarr.Length - 1]);
				JavaCCGlobals.jjtreeGenerated = JavaCCGlobals.IsGeneratedBy("JJTree", strarr[strarr.Length - 1]);
				JavaCCGlobals.ToolNames = JavaCCGlobals.GetToolNames(strarr[strarr.Length - 1]);
				javaCCParser.javacc_input();
				JavaCCGlobals.CreateOutputDir(Options.OutputDirectory);
				if (Options.UnicodeInput)
				{
					NfaState.unicodeWarningGiven = true;
					Console.WriteLine("Note: UNICODE_INPUT option is specified. Please make sure you create the parser/lexer using a TextReader with the correct character encoding.");
				}
				Semanticize.start();
				ParseGen.Start();
				LexGen.start();
				OtherFilesGen.start();
				if (JavaCCErrors._Error_Count == 0 && (Options.BuildParser || Options.BuildTokenManager))
				{
					if (JavaCCErrors._Warning_Count == 0)
					{
						Console.WriteLine("Parser generated successfully.");
					}
					else
					{
						Console.WriteLine(("Parser generated with 0 errors and ")+(JavaCCErrors._Warning_Count)+(" warnings.")
							);
					}
					return 0;
				}
				Console.WriteLine(("Detected ")+(JavaCCErrors._Error_Count)+(" errors and ")
					+(JavaCCErrors._Warning_Count)
					+(" warnings.")
					);
				return (JavaCCErrors._Error_Count != 0) ? 1 : 0;
			}
			catch (MetaParseException)
			{
			}
		}
		catch (ParseException x)
		{
			ex4 = x;
			goto IL_0361;
		}

		Console.WriteLine(("Detected ")+(JavaCCErrors._Error_Count)+(" errors and ")
			+(JavaCCErrors._Warning_Count)
			+(" warnings.")
			);
		return 1;
	IL_0361:
		ParseException @this = ex4;
		Console.WriteLine((@this));
		Console.WriteLine(("Detected ")+(JavaCCErrors._Error_Count + 1)+(" errors and ")
			+(JavaCCErrors._Warning_Count)
			+(" warnings.")
			);
		return 1;
	IL_0180:

		Console.WriteLine(("File ")+(strarr[strarr.Length - 1])+(" not found.")
			);
		return 1;
	IL_017d:

		Console.WriteLine(("Security violation while trying to open ")+(strarr[strarr.Length - 1]));
		return 1;
	}


	internal static void help_message()
	{
		Console.WriteLine("Usage:");
		Console.WriteLine("    javacc option-settings inputfile");
		Console.WriteLine("");
		Console.WriteLine("\"option-settings\" is a sequence of settings separated by spaces.");
		Console.WriteLine("Each option setting must be of one of the following forms:");
		Console.WriteLine("");
		Console.WriteLine("    -optionname=value (e.g., -STATIC=false)");
		Console.WriteLine("    -optionname:value (e.g., -STATIC:false)");
		Console.WriteLine("    -optionname       (equivalent to -optionname=true.  e.g., -STATIC)");
		Console.WriteLine("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOSTATIC)");
		Console.WriteLine("");
		Console.WriteLine("Option settings are not case-sensitive, so one can say \"-nOsTaTiC\" instead");
		Console.WriteLine("of \"-NOSTATIC\".  Option values must be appropriate for the corresponding");
		Console.WriteLine("option, and must be either an integer, a boolean, or a string value.");
		Console.WriteLine("");
		Console.WriteLine("The integer valued options are:");
		Console.WriteLine("");
		Console.WriteLine("    LOOKAHEAD              (default 1)");
		Console.WriteLine("    CHOICE_AMBIGUITY_CHECK (default 2)");
		Console.WriteLine("    OTHER_AMBIGUITY_CHECK  (default 1)");
		Console.WriteLine("");
		Console.WriteLine("The boolean valued options are:");
		Console.WriteLine("");
		Console.WriteLine("    STATIC                 (default true)");
		Console.WriteLine("    DEBUG_PARSER           (default false)");
		Console.WriteLine("    DEBUG_LOOKAHEAD        (default false)");
		Console.WriteLine("    DEBUG_TOKEN_MANAGER    (default false)");
		Console.WriteLine("    ERROR_REPORTING        (default true)");
		Console.WriteLine("    JAVA_UNICODE_ESCAPE    (default false)");
		Console.WriteLine("    UNICODE_INPUT          (default false)");
		Console.WriteLine("    IGNORE_CASE            (default false)");
		Console.WriteLine("    COMMON_TOKEN_ACTION    (default false)");
		Console.WriteLine("    USER_TOKEN_MANAGER     (default false)");
		Console.WriteLine("    USER_CHAR_STREAM       (default false)");
		Console.WriteLine("    BUILD_PARSER           (default true)");
		Console.WriteLine("    BUILD_TOKEN_MANAGER    (default true)");
		Console.WriteLine("    TOKEN_MANAGER_USES_PARSER (default false)");
		Console.WriteLine("    SANITY_CHECK           (default true)");
		Console.WriteLine("    FORCE_LA_CHECK         (default false)");
		Console.WriteLine("    CACHE_TOKENS           (default false)");
		Console.WriteLine("    KEEP_LINE_COLUMN       (default true)");
		Console.WriteLine("");
		Console.WriteLine("The string valued options are:");
		Console.WriteLine("");
		Console.WriteLine("    OUTPUT_DIRECTORY       (default Current Directory)");
		Console.WriteLine("    TOKEN_EXTENDS          (default java.lang.Object)");
		Console.WriteLine("    TOKEN_FACTORY          (default none)");
		Console.WriteLine("    JDK_VERSION            (default 1.5)");
		Console.WriteLine("");
		Console.WriteLine("EXAMPLE:");
		Console.WriteLine("    javacc -STATIC=false -LOOKAHEAD:2 -debug_parser mygrammar.jj");
		Console.WriteLine("");
	}
}
