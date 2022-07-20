namespace JavaCC.JJDoc;

using JavaCC.Parser;

public class JJDocOptions : Options
{
    public static string OutputFile => StringValue("OUTPUT_FILE");
    public static bool Text => BooleanValue("TEXT");
    public static string CSS => StringValue("CSS");
    public static bool OneTable => BooleanValue("ONE_TABLE");
    public new static void Init()
    {
        Options.Init();
        OptionValues.Add("ONE_TABLE", true);
        OptionValues.Add("TEXT", false);
        OptionValues.Add("OUTPUT_FILE", "");
        OptionValues.Add("CSS", "");
    }
}
