using System.Collections;
using System.Collections.Generic;

namespace org.javacc.jjtree;


internal class JJTreeGlobals
{
	private static HashSet<string> jjtreeOptions =new();

	internal static List<string> toolList = new();

	public static string parserName = "";

	public static string packageName = "";

	public static string nodePackageName = "";

	public static Token parserImplements;

	public static Token parserClassBodyStart;

	public static Token parserImports;

	internal static Dictionary<string, ASTJavacode> productions = new();


    public static bool IsOptionJJTreeOnly(string name) => jjtreeOptions.Contains((name.ToUpper()));


    internal static void Initialize()
	{
		toolList = new();
		parserName = null;
		packageName = "";
		parserImplements = null;
		parserClassBodyStart = null;
		parserImports = null;
		productions = new();
		jjtreeOptions = new();
		jjtreeOptions.Add("JJTREE_OUTPUT_DIRECTORY");
		jjtreeOptions.Add("MULTI");
		jjtreeOptions.Add("NODE_PREFIX");
		jjtreeOptions.Add("NODE_PACKAGE");
		jjtreeOptions.Add("NODE_EXTENDS");
		jjtreeOptions.Add("NODE_CLASS");
		jjtreeOptions.Add("NODE_STACK_SIZE");
		jjtreeOptions.Add("NODE_DEFAULT_VOID");
		jjtreeOptions.Add("OUTPUT_FILE");
		jjtreeOptions.Add("CHECK_DEFINITE_NODE");
		jjtreeOptions.Add("NODE_SCOPE_HOOK");
		jjtreeOptions.Add("TRACK_TOKENS");
		jjtreeOptions.Add("NODE_FACTORY");
		jjtreeOptions.Add("NODE_USES_PARSER");
		jjtreeOptions.Add("BUILD_NODE_FILES");
		jjtreeOptions.Add("VISITOR");
		jjtreeOptions.Add("VISITOR_DATA_TYPE");
	}

	static JJTreeGlobals()
	{
		Initialize();
		toolList = new ();
		packageName = "";
		nodePackageName = "";
		productions = new ();
	}
}
