
using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjdoc;


public sealed class JJDocMain : JJDocGlobals
{
	public static int Main(string[] strarr)
	{
		int status = mainProgram(strarr);
		return status;
	}

	public static int mainProgram(string[] strarr)
	{
		//Discarded unreachable code: IL_02fd
		EntryPoint.reInitAll();
		JJDocOptions.init();
		JavaCCGlobals.bannerLine("Documentation Generator", "0.1.4");
		
		if (strarr.Length == 0)
		{
			help_message();
			return 1;
		}
		JJDocGlobals.info("(type \"jjdoc\" with no arguments for help)");
		if (Options.isOption(strarr[(nint)strarr.LongLength - 1]))
		{
			JJDocGlobals.error(new StringBuilder().Append("Last argument \"").Append(strarr[(nint)strarr.LongLength - 1]).Append("\" is not a filename or \"-\".  ")
				.ToString());
			return 1;
		}
		for (int i = 0; i < (nint)strarr.LongLength - 1; i++)
		{
			if (!Options.isOption(strarr[i]))
			{
				JJDocGlobals.error(new StringBuilder().Append("Argument \"").Append(strarr[i]).Append("\" must be an option setting.  ")
					.ToString());
				return 1;
			}
			Options.setCmdLineOption(strarr[i]);
		}
		JavaCCParser javaCCParser;
		if (string.Equals(strarr[(nint)strarr.LongLength - 1], "-"))
		{
			JJDocGlobals.info("Reading from standard input . . .");
			JavaCCParser.___003Cclinit_003E();
			javaCCParser = new JavaCCParser(new DataInputStream(java.lang.System.@in));
			JJDocGlobals.input_file = "standard input";
			JJDocGlobals.output_file = "standard output";
		}
		else
		{
			JJDocGlobals.info(new StringBuilder().Append("Reading from file ").Append(strarr[(nint)strarr.LongLength - 1]).Append(" . . .")
				.ToString());
			try
			{
				try
				{
					//
					File file = new File(strarr[(nint)strarr.LongLength - 1]);
					if (!file.Exists)
					{
						JJDocGlobals.error(new StringBuilder().Append("File ").Append(strarr[(nint)strarr.LongLength - 1]).Append(" not found.")
							.ToString());
						return 1;
					}
					if (file.isDirectory())
					{
						JJDocGlobals.error(new StringBuilder().Append(strarr[(nint)strarr.LongLength - 1]).Append(" is a directory. Please use a valid file name.").ToString());
						return 1;
					}
					JJDocGlobals.input_file = file.getName();
					JavaCCParser.___003Cclinit_003E();
					javaCCParser = new JavaCCParser(new FileReader(strarr[(nint)strarr.LongLength - 1]));
				}
				catch (Expansion)
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
				JJDoc.start();
				if (JavaCCErrors.Get_Error_Count() == 0)
				{
					if (JavaCCErrors.Get_Warning_Count() == 0)
					{
						JJDocGlobals.info(new StringBuilder().Append("Grammar documentation generated successfully in ").Append(JJDocGlobals.output_file).ToString());
					}
					else
					{
						JJDocGlobals.info(new StringBuilder().Append("Grammar documentation generated with 0 errors and ").Append(JavaCCErrors.Get_Warning_Count()).Append(" warnings.")
							.ToString());
					}
					return 0;
				}
				JJDocGlobals.error(new StringBuilder().Append("Detected ").Append(JavaCCErrors.Get_Error_Count()).Append(" errors and ")
					.Append(JavaCCErrors.Get_Warning_Count())
					.Append(" warnings.")
					.ToString());
				return (JavaCCErrors.Get_Error_Count() != 0) ? 1 : 0;
			}
			catch (MetaParseException x)
			{
				ex3 = ByteCodeHelper.MapException<MetaParseException>(x, ByteCodeHelper.MapFlags.NoRemapping);
			}
		}
		catch (ParseException x2)
		{
			ex4 = ByteCodeHelper.MapException<ParseException>(x2, ByteCodeHelper.MapFlags.NoRemapping);
			goto IL_031e;
		}
		MetaParseException @this = ex3;
		JJDocGlobals.error((@this));
		JJDocGlobals.error(new StringBuilder().Append("Detected ").Append(JavaCCErrors.Get_Error_Count()).Append(" errors and ")
			.Append(JavaCCErrors.Get_Warning_Count())
			.Append(" warnings.")
			.ToString());
		return 1;
		IL_031e:
		ParseException this2 = ex4;
		JJDocGlobals.error((this2));
		JJDocGlobals.error(new StringBuilder().Append("Detected ").Append(JavaCCErrors.Get_Error_Count() + 1).Append(" errors and ")
			.Append(JavaCCErrors.Get_Warning_Count())
			.Append(" warnings.")
			.ToString());
		return 1;
		IL_01e0:
		
		JJDocGlobals.error(new StringBuilder().Append("File ").Append(strarr[(nint)strarr.LongLength - 1]).Append(" not found.")
			.ToString());
		return 1;
		IL_01dd:
		
		JJDocGlobals.error(new StringBuilder().Append("Security violation while trying to open ").Append(strarr[(nint)strarr.LongLength - 1]).ToString());
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
