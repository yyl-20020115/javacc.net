namespace JavaCC.JJDoc;

using JavaCC.Parser;

public class JJDocGlobals : JavaCCGlobals
{
    public static string InputFile = "";
    public static string OutputFile = "";
    protected static Generator generator;

    public static Generator Generator
    {
        get
        {
            if (generator == null)
            {
                if (JJDocOptions.Text)
                {
                    generator = new TextGenerator();
                }
                else
                {
                    generator = new HTMLGenerator();
                }
            }
            else if (JJDocOptions.Text)
            {
                if (generator is HTMLGenerator)
                {
                    generator = new TextGenerator();
                }
            }
            else if (generator is TextGenerator)
            {
                generator = new HTMLGenerator();
            }
            return generator;
        }

        set => generator = value;
    }

    public JJDocGlobals()
    {
    }

    public static void Debug(string str)
    {
        Generator.Debug(str);
    }


    public static void Info(string str)
    {
        Generator.Info(str);
    }


    public static void Error(string str)
    {
        Generator.Error(str);
    }
}
