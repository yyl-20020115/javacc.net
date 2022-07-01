using org.javacc.parser;
namespace org.javacc.jjdoc;

public class JJDocOptions : Options
{
    public static string OutputFile => Options.StringValue("OUTPUT_FILE");
    public static bool Text => Options.BooleanValue("TEXT");
    public static string CSS => Options.StringValue("CSS");
    public static bool OneTable => Options.BooleanValue("ONE_TABLE");
    public new static void Init()
	{
		Options.Init();
		Options.OptionValues.Add("ONE_TABLE", true);
		Options.OptionValues.Add("TEXT", false);
		Options.OptionValues.Add("OUTPUT_FILE", "");
		Options.OptionValues.Add("CSS", "");
	}

}
