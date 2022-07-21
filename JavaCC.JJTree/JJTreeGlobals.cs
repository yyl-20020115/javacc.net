namespace JavaCC.JJTree;
using System.Collections.Generic;

public class JJTreeGlobals
{
    public static readonly HashSet<string> JJTreeOptions = new();

    public static readonly List<string> ToolList = new();

    public static string ParserName = "";

    public static string PackageName = "";

    public static string NodePackageName = "";

    public static Token ParserImplements;

    public static Token ParserClassBodyStart;

    public static Token ParserImports;

    internal static Dictionary<string, ASTProduction> Productions = new();


    public static bool IsOptionJJTreeOnly(string name) => JJTreeOptions.Contains((name.ToUpper()));


    internal static void Initialize()
    {
        ParserName = null;
        PackageName = "";
        ParserImplements = null;
        ParserClassBodyStart = null;
        ParserImports = null;
        Productions = new();
        JJTreeOptions.Add("JJTREE_OUTPUT_DIRECTORY");
        JJTreeOptions.Add("MULTI");
        JJTreeOptions.Add("NODE_PREFIX");
        JJTreeOptions.Add("NODE_PACKAGE");
        JJTreeOptions.Add("NODE_EXTENDS");
        JJTreeOptions.Add("NODE_CLASS");
        JJTreeOptions.Add("NODE_STACK_SIZE");
        JJTreeOptions.Add("NODE_DEFAULT_VOID");
        JJTreeOptions.Add("OUTPUT_FILE");
        JJTreeOptions.Add("CHECK_DEFINITE_NODE");
        JJTreeOptions.Add("NODE_SCOPE_HOOK");
        JJTreeOptions.Add("TRACK_TOKENS");
        JJTreeOptions.Add("NODE_FACTORY");
        JJTreeOptions.Add("NODE_USES_PARSER");
        JJTreeOptions.Add("BUILD_NODE_FILES");
        JJTreeOptions.Add("VISITOR");
        JJTreeOptions.Add("VISITOR_DATA_TYPE");
    }

    static JJTreeGlobals()
    {
        Initialize();
        PackageName = "";
        NodePackageName = "";
    }
}
