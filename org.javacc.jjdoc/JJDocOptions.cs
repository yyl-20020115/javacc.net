using org.javacc.parser;
namespace org.javacc.jjdoc;

public class JJDocOptions : Options
{
	
	public static string getOutputFile()
	{
		string result = Options.stringValue("OUTPUT_FILE");
		
		return result;
	}

	
	public static bool getText()
	{
		bool result = Options.booleanValue("TEXT");
		
		return result;
	}

	
	public static string getCSS()
	{
		string result = Options.stringValue("CSS");
		
		return result;
	}

	
	public static bool getOneTable()
	{
		bool result = Options.booleanValue("ONE_TABLE");
		
		return result;
	}

	
	public new static void init()
	{
		Options.init();
		Options.optionValues.Add("ONE_TABLE", true);
		Options.optionValues.Add("TEXT", false);
		Options.optionValues.Add("OUTPUT_FILE", "");
		Options.optionValues.Add("CSS", "");
	}

	
	protected internal JJDocOptions()
	{
	}
}
