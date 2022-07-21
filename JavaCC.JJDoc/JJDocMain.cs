namespace JavaCC.JJDoc;

using System;
using System.IO;
using JavaCC.Parser;

public class JJDocMain : JJDocGlobals
{
    public static int Main(string[] args)
    {
        EntryPoint.ReInitAll();
        JJDocOptions.Init();
        BannerLine("Documentation Generator", "0.1.4");

        if (args.Length == 0)
        {
            HelpMessage();
            return 1;
        }
        Info("(type \"jjdoc\" with no arguments for help)");
        if (Options.IsOption(args[args.Length - 1]))
        {
            Error(("Last argument \"") + (args[args.Length - 1]) + ("\" is not a filename or \"-\".  "));
            return 1;
        }
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (!Options.IsOption(args[i]))
            {
                Error(("Argument \"") + (args[i]) + ("\" must be an option setting.  "));
                return 1;
            }
            Options.SetCmdLineOption(args[i]);
        }
        JavaCCParser javaCCParser;
        if (string.Equals(args[args.Length - 1], "-"))
        {
            Info("Reading from standard input . . .");
            javaCCParser = new JavaCCParser(Console.In);
            InputFile = "standard input";
            OutputFile = "standard output";
        }
        else
        {
            Info(("Reading from file ") + (args[args.Length - 1]) + (" . . ."));
            try
            {
                try
                {
                    //
                    var path = args[args.Length - 1];
                    var file = new FileInfo(path);
                    if (!file.Exists)
                    {
                        Error(("File ") + (args[args.Length - 1]) + (" not found."));
                        return 1;
                    }
                    if (new DirectoryInfo(path).Exists)
                    {
                        Error((args[args.Length - 1]) + (" is a directory. Please use a valid file name."));
                        return 1;
                    }
                    InputFile = file.Name;
                    javaCCParser = new JavaCCParser(new StreamReader(args[args.Length - 1]));
                }
                catch
                {

                    Error(("Security violation while trying to open ") + (args[args.Length - 1]));
                    return 1;
                }
            }
            catch (FileNotFoundException)
            {
                Error(("File ") + (args[args.Length - 1]) + (" not found."));
                return 1;
            }
        }
        try
        {
            try
            {
                javaCCParser.JavaCC_Input();
                JJDoc.Start();
                if (JavaCCErrors.ErrorCount == 0)
                {
                    if (JavaCCErrors.WarningCount == 0)
                    {
                        Info(("Grammar documentation generated successfully in ") + (OutputFile));
                    }
                    else
                    {
                        Info(("Grammar documentation generated with 0 errors and ") + (JavaCCErrors.WarningCount) + (" warnings."));
                    }
                    return 0;
                }
                Error(("Detected ") + (JavaCCErrors.ErrorCount) + (" errors and ")
                    + (JavaCCErrors.WarningCount)
                    + (" warnings.")
                    );
                return (JavaCCErrors.ErrorCount != 0) ? 1 : 0;
            }
            catch (MetaParseException e1)
            {
                Error((e1.Message));
                Error(("Detected ") + (JavaCCErrors.ErrorCount) + (" errors and ")
                    + (JavaCCErrors.WarningCount)
                    + (" warnings.")
                    );
                return 1;
            }
        }
        catch (ParseException e2)
        {
            Error((e2.Message));
            Error(("Detected ") + (JavaCCErrors.ErrorCount + 1) + (" errors and ")
                + (JavaCCErrors.WarningCount)
                + (" warnings.")
                );
            return 1;
        }
    }

    public static void HelpMessage()
    {
        Info("");
        Info("    jjdoc option-settings - (to read from standard input)");
        Info("OR");
        Info("    jjdoc option-settings inputfile (to read from a file)");
        Info("");
        Info("WHERE");
        Info("    \"option-settings\" is a sequence of settings separated by spaces.");
        Info("");
        Info("Each option setting must be of one of the following forms:");
        Info("");
        Info("    -optionname=value (e.g., -TEXT=false)");
        Info("    -optionname:value (e.g., -TEXT:false)");
        Info("    -optionname       (equivalent to -optionname=true.  e.g., -TEXT)");
        Info("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOTEXT)");
        Info("");
        Info("Option settings are not case-sensitive, so one can say \"-nOtExT\" instead");
        Info("of \"-NOTEXT\".  Option values must be appropriate for the corresponding");
        Info("option, and must be either an integer, boolean or string value.");
        Info("");
        Info("The string valued options are:");
        Info("");
        Info("    OUTPUT_FILE");
        Info("    CSS");
        Info("");
        Info("The boolean valued options are:");
        Info("");
        Info("    ONE_TABLE              (default true)");
        Info("    TEXT                   (default false)");
        Info("");
        Info("");
        Info("EXAMPLES:");
        Info("    jjdoc -ONE_TABLE=false mygrammar.jj");
        Info("    jjdoc - < mygrammar.jj");
        Info("");
        Info("ABOUT JJDoc:");
        Info("    JJDoc generates JavaDoc documentation from JavaCC grammar files.");
        Info("");
        Info("    For more information, see the online JJDoc documentation at");
        Info("    https://javacc.dev.java.net/doc/JJDoc.html");
    }
}
