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

    public static void Debug(string text)
    {
        Generator.Debug(text);
    }


    public static void Info(string text)
    {
        Generator.Info(text);
    }


    public static void Error(string text)
    {
        Generator.Error(text);
    }
}
