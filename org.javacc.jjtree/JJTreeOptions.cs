using System.IO;
using org.javacc.parser;
namespace org.javacc.jjtree;

internal class JJTreeOptions : Options
{

    public static bool Multi => Options.BooleanValue("MULTI");

    public static string NodePrefix => Options.StringValue("NODE_PREFIX");


    public static string OutputFile => Options.StringValue("OUTPUT_FILE");


    public static FileInfo JJTreeOutputDirectory
    {
        get
        {
            var text = Options.StringValue("JJTREE_OUTPUT_DIRECTORY");

            return string.Equals("", text) ? Options.OutputDirectory : new FileInfo(text);
        }
    }

    public static bool Visitor => Options.BooleanValue("VISITOR");


    public static string VisitorDataType => Options.StringValue("VISITOR_DATA_TYPE");


    public static string VisitorException => Options.StringValue("VISITOR_EXCEPTION");


    public static string JdkVersion => Options.StringValue("JDK_VERSION");


    public static bool NodeDefaultVoid => Options.BooleanValue("NODE_DEFAULT_VOID");


    public static bool NodeScopeHook => Options.BooleanValue("NODE_SCOPE_HOOK");


    public static string NodeFactory => Options.StringValue("NODE_FACTORY");


    public static bool NodeUsesParser => Options.BooleanValue("NODE_USES_PARSER");


    public static bool BuildNodeFiles => Options.BooleanValue("BUILD_NODE_FILES");


    public static bool TrackTokens => Options.BooleanValue("TRACK_TOKENS");


    public static string NodeExtends => Options.StringValue("NODE_EXTENDS");


    public static string NodeClass => Options.StringValue("NODE_CLASS");


    public static string NodePackage => Options.StringValue("NODE_PACKAGE");

	public static void Validate()
	{
		if (!Visitor)
		{
			if ((VisitorDataType.Length) > 0)
			{
				JavaCCErrors.Warning("VISITOR_DATA_TYPE option will be ignored since VISITOR is false");
			}
			if ((VisitorException.Length) > 0)
			{
				JavaCCErrors.Warning("VISITOR_EXCEPTION option will be ignored since VISITOR is false");
			}
		}
	}


	public new static void Init()
	{
		Options.Init();
		Options.OptionValues.Add("JDK_VERSION", "1.4");
		Options.OptionValues.Add("MULTI", false);
		Options.OptionValues.Add("NODE_DEFAULT_VOID", false);
		Options.OptionValues.Add("NODE_SCOPE_HOOK", false);
		Options.OptionValues.Add("NODE_USES_PARSER", false);
		Options.OptionValues.Add("BUILD_NODE_FILES", true);
		Options.OptionValues.Add("VISITOR", false);
		Options.OptionValues.Add("TRACK_TOKENS", false);
		Options.OptionValues.Add("NODE_PREFIX", "AST");
		Options.OptionValues.Add("NODE_PACKAGE", "");
		Options.OptionValues.Add("NODE_EXTENDS", "");
		Options.OptionValues.Add("NODE_CLASS", "");
		Options.OptionValues.Add("NODE_FACTORY", "");
		Options.OptionValues.Add("OUTPUT_FILE", "");
		Options.OptionValues.Add("VISITOR_DATA_TYPE", "");
		Options.OptionValues.Add("VISITOR_EXCEPTION", "");
		Options.OptionValues.Add("JJTREE_OUTPUT_DIRECTORY", "");
	}
}
