using JavaCC.Parser;

namespace JavaCC.JJDoc;

public class JJDocGlobals : JavaCCGlobals
{
	public static string input_file;
	public static string output_file;
	public static Generator generator;
	
	public static Generator GetGenerator()
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

	
	public JJDocGlobals()
	{
	}

	public static void setGenerator(Generator g)
	{
		generator = g;
	}

	
	public static void debug(string str)
	{
		GetGenerator().Debug(str);
	}

	
	public static void info(string str)
	{
		GetGenerator().Info(str);
	}

	
	public static void error(string str)
	{
		GetGenerator().Error(str);
	}
}
