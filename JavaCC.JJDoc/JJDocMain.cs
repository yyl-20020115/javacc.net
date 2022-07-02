using System;
using System.IO;
using System.Text;
using JavaCC.Parser;

namespace JavaCC.JJDoc;

public sealed class JJDocMain : JJDocGlobals
{
	public static int Main(string[] strarr)
	{
		return mainProgram(strarr);
	}

	public static int mainProgram(string[] strarr)
	{
		//Discarded unreachable code: IL_02fd
		EntryPoint.reInitAll();
		JJDocOptions.Init();
		JavaCCGlobals.BannerLine("Documentation Generator", "0.1.4");
		
		if (strarr.Length == 0)
		{
			help_message();
			return 1;
		}
		JJDocGlobals.info("(type \"jjdoc\" with no arguments for help)");
		if (Options.IsOption(strarr[strarr.Length - 1]))
		{
			JJDocGlobals.error(("Last argument \"")+(strarr[strarr.Length - 1])+("\" is not a filename or \"-\".  ")
				);
			return 1;
		}
		for (int i = 0; i < (nint)strarr.LongLength - 1; i++)
		{
			if (!Options.IsOption(strarr[i]))
			{
				JJDocGlobals.error(("Argument \"")+(strarr[i])+("\" must be an option setting.  ")
					);
				return 1;
			}
			Options.setCmdLineOption(strarr[i]);
		}
		JavaCCParser javaCCParser;
		if (string.Equals(strarr[strarr.Length - 1], "-"))
		{
			JJDocGlobals.info("Reading from standard input . . .");
			javaCCParser = new JavaCCParser(Console.In);
			JJDocGlobals.input_file = "standard input";
			JJDocGlobals.output_file = "standard output";
		}
		else
		{
			JJDocGlobals.info(("Reading from file ")+(strarr[strarr.Length - 1])+(" . . .")
				);
			try
			{
				try
				{
					//
					var path = strarr[strarr.Length - 1];
					FileInfo file = new FileInfo(path);
					if (!file.Exists)
					{
						JJDocGlobals.error(("File ")+(strarr[strarr.Length - 1])+(" not found.")
							);
						return 1;
					}
					if (new DirectoryInfo(path).Exists)
					{
						JJDocGlobals.error((strarr[strarr.Length - 1])+(" is a directory. Please use a valid file name."));
						return 1;
					}
					JJDocGlobals.input_file = file.Name;
					javaCCParser = new JavaCCParser(new StreamReader(strarr[strarr.Length - 1]));
				}
				catch 
				{
					goto IL_01dd;
				}
			}
			catch (FileNotFoundException)
			{
				goto IL_01e0;
			}
		}
		MetaParseException ex3;
		ParseException ex4;
		try
		{
			try
			{
				javaCCParser.javacc_input();
				JJDoc.Start();
				if (JavaCCErrors._Error_Count == 0)
				{
					if (JavaCCErrors._Warning_Count == 0)
					{
						JJDocGlobals.info(("Grammar documentation generated successfully in ")+(JJDocGlobals.output_file));
					}
					else
					{
						JJDocGlobals.info(("Grammar documentation generated with 0 errors and ")+(JavaCCErrors._Warning_Count)+(" warnings.")
							);
					}
					return 0;
				}
				JJDocGlobals.error(("Detected ")+(JavaCCErrors._Error_Count)+(" errors and ")
					+(JavaCCErrors._Warning_Count)
					+(" warnings.")
					);
				return (JavaCCErrors._Error_Count != 0) ? 1 : 0;
			}
			catch (MetaParseException x)
			{
				ex3 = x;
			}
		}
		catch (ParseException x2)
		{
			ex4 = x2;
			goto IL_031e;
		}
		MetaParseException @this = ex3;
		JJDocGlobals.error((@this.Message));
		JJDocGlobals.error(("Detected ")+(JavaCCErrors._Error_Count)+(" errors and ")
			+(JavaCCErrors._Warning_Count)
			+(" warnings.")
			);
		return 1;
		IL_031e:
		ParseException this2 = ex4;
		JJDocGlobals.error((this2.Message));
		JJDocGlobals.error(("Detected ")+(JavaCCErrors._Error_Count + 1)+(" errors and ")
			+(JavaCCErrors._Warning_Count)
			+(" warnings.")
			);
		return 1;
		IL_01e0:
		
		JJDocGlobals.error(("File ")+(strarr[strarr.Length - 1])+(" not found.")
			);
		return 1;
		IL_01dd:
		
		JJDocGlobals.error(("Security violation while trying to open ")+(strarr[strarr.Length - 1]));
		return 1;
	}

	
	internal static void help_message()
	{
		JJDocGlobals.info("");
		JJDocGlobals.info("    jjdoc option-settings - (to read from standard input)");
		JJDocGlobals.info("OR");
		JJDocGlobals.info("    jjdoc option-settings inputfile (to read from a file)");
		JJDocGlobals.info("");
		JJDocGlobals.info("WHERE");
		JJDocGlobals.info("    \"option-settings\" is a sequence of settings separated by spaces.");
		JJDocGlobals.info("");
		JJDocGlobals.info("Each option setting must be of one of the following forms:");
		JJDocGlobals.info("");
		JJDocGlobals.info("    -optionname=value (e.g., -TEXT=false)");
		JJDocGlobals.info("    -optionname:value (e.g., -TEXT:false)");
		JJDocGlobals.info("    -optionname       (equivalent to -optionname=true.  e.g., -TEXT)");
		JJDocGlobals.info("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOTEXT)");
		JJDocGlobals.info("");
		JJDocGlobals.info("Option settings are not case-sensitive, so one can say \"-nOtExT\" instead");
		JJDocGlobals.info("of \"-NOTEXT\".  Option values must be appropriate for the corresponding");
		JJDocGlobals.info("option, and must be either an integer, boolean or string value.");
		JJDocGlobals.info("");
		JJDocGlobals.info("The string valued options are:");
		JJDocGlobals.info("");
		JJDocGlobals.info("    OUTPUT_FILE");
		JJDocGlobals.info("    CSS");
		JJDocGlobals.info("");
		JJDocGlobals.info("The boolean valued options are:");
		JJDocGlobals.info("");
		JJDocGlobals.info("    ONE_TABLE              (default true)");
		JJDocGlobals.info("    TEXT                   (default false)");
		JJDocGlobals.info("");
		JJDocGlobals.info("");
		JJDocGlobals.info("EXAMPLES:");
		JJDocGlobals.info("    jjdoc -ONE_TABLE=false mygrammar.jj");
		JJDocGlobals.info("    jjdoc - < mygrammar.jj");
		JJDocGlobals.info("");
		JJDocGlobals.info("ABOUT JJDoc:");
		JJDocGlobals.info("    JJDoc generates JavaDoc documentation from JavaCC grammar files.");
		JJDocGlobals.info("");
		JJDocGlobals.info("    For more information, see the online JJDoc documentation at");
		JJDocGlobals.info("    https://javacc.dev.java.net/doc/JJDoc.html");
	}

	
	private JJDocMain()
	{
	}
}
