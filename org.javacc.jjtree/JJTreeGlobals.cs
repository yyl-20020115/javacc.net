using System.Collections;
using System.Collections.Generic;

namespace org.javacc.jjtree;


internal class JJTreeGlobals
{
	private static HashSet<string> jjtreeOptions;

	internal static ArrayList toolList;

	public static string parserName;

	public static string packageName;

	public static string nodePackageName;

	public static Token parserImplements;

	public static Token parserClassBodyStart;

	public static Token parserImports;

	internal static Hashtable productions;

	
	
	public static void ___003Cclinit_003E()
	{
	}

	
	public static bool isOptionJJTreeOnly(string P_0)
	{
		bool result = jjtreeOptions.Contains((P_0.ToUpper()));
		
		return result;
	}

	
	internal static void initialize()
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

	
	internal JJTreeGlobals()
	{
	}

	static JJTreeGlobals()
	{
		initialize();
		toolList = new ();
		packageName = "";
		nodePackageName = "";
		productions = new ();
	}
}
