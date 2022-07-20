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
            Options.setCmdLineOption(args[i]);
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
                if (JavaCCErrors._Error_Count == 0)
                {
                    if (JavaCCErrors._Warning_Count == 0)
                    {
                        Info(("Grammar documentation generated successfully in ") + (JJDocGlobals.OutputFile));
                    }
                    else
                    {
                        Info(("Grammar documentation generated with 0 errors and ") + (JavaCCErrors._Warning_Count) + (" warnings."));
                    }
                    return 0;
                }
                Error(("Detected ") + (JavaCCErrors._Error_Count) + (" errors and ")
                    + (JavaCCErrors._Warning_Count)
                    + (" warnings.")
                    );
                return (JavaCCErrors._Error_Count != 0) ? 1 : 0;
            }
            catch (MetaParseException e1)
            {
                Error((e1.Message));
                Error(("Detected ") + (JavaCCErrors._Error_Count) + (" errors and ")
                    + (JavaCCErrors._Warning_Count)
                    + (" warnings.")
                    );
                return 1;
            }
        }
        catch (ParseException e2)
        {
            Error((e2.Message));
            Error(("Detected ") + (JavaCCErrors._Error_Count + 1) + (" errors and ")
                + (JavaCCErrors._Warning_Count)
                + (" warnings.")
                );
            return 1;
        }
    }

    public static void HelpMessage()
    {
        JJDocGlobals.Info("");
        JJDocGlobals.Info("    jjdoc option-settings - (to read from standard input)");
        JJDocGlobals.Info("OR");
        JJDocGlobals.Info("    jjdoc option-settings inputfile (to read from a file)");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("WHERE");
        JJDocGlobals.Info("    \"option-settings\" is a sequence of settings separated by spaces.");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("Each option setting must be of one of the following forms:");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("    -optionname=value (e.g., -TEXT=false)");
        JJDocGlobals.Info("    -optionname:value (e.g., -TEXT:false)");
        JJDocGlobals.Info("    -optionname       (equivalent to -optionname=true.  e.g., -TEXT)");
        JJDocGlobals.Info("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOTEXT)");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("Option settings are not case-sensitive, so one can say \"-nOtExT\" instead");
        JJDocGlobals.Info("of \"-NOTEXT\".  Option values must be appropriate for the corresponding");
        JJDocGlobals.Info("option, and must be either an integer, boolean or string value.");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("The string valued options are:");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("    OUTPUT_FILE");
        JJDocGlobals.Info("    CSS");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("The boolean valued options are:");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("    ONE_TABLE              (default true)");
        JJDocGlobals.Info("    TEXT                   (default false)");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("EXAMPLES:");
        JJDocGlobals.Info("    jjdoc -ONE_TABLE=false mygrammar.jj");
        JJDocGlobals.Info("    jjdoc - < mygrammar.jj");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("ABOUT JJDoc:");
        JJDocGlobals.Info("    JJDoc generates JavaDoc documentation from JavaCC grammar files.");
        JJDocGlobals.Info("");
        JJDocGlobals.Info("    For more information, see the online JJDoc documentation at");
        JJDocGlobals.Info("    https://javacc.dev.java.net/doc/JJDoc.html");
    }
}
