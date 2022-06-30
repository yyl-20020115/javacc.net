

using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjtree;


internal class JJTreeOptions : Options
{
	
	public static bool getMulti()
	{
		bool result = Options.booleanValue("MULTI");
		
		return result;
	}

	
	public static string getNodePrefix()
	{
		string result = Options.stringValue("NODE_PREFIX");
		
		return result;
	}

	
	public static string getOutputFile()
	{
		string result = Options.stringValue("OUTPUT_FILE");
		
		return result;
	}

	
	public static FileInfo getJJTreeOutputDirectory()
	{
		string text = Options.stringValue("JJTREE_OUTPUT_DIRECTORY");
		
		if (string.Equals("", text))
		{
			return Options.getOutputDirectory();
		}
		return new FileInfo(text);
	}

	
	public static void validate()
	{
		if (!getVisitor())
		{
			if ((getVisitorDataType().Length) > 0)
			{
				JavaCCErrors.Warning("VISITOR_DATA_TYPE option will be ignored since VISITOR is false");
			}
			if ((getVisitorException().Length) > 0)
			{
				JavaCCErrors.Warning("VISITOR_EXCEPTION option will be ignored since VISITOR is false");
			}
		}
	}

	
	public new static void init()
	{
		Options.init();
		Options.optionValues.Add("JDK_VERSION", "1.4");
		Options.optionValues.Add("MULTI", false);
		Options.optionValues.Add("NODE_DEFAULT_VOID", false);
		Options.optionValues.Add("NODE_SCOPE_HOOK", false);
		Options.optionValues.Add("NODE_USES_PARSER", false);
		Options.optionValues.Add("BUILD_NODE_FILES", true);
		Options.optionValues.Add("VISITOR", false);
		Options.optionValues.Add("TRACK_TOKENS", false);
		Options.optionValues.Add("NODE_PREFIX", "AST");
		Options.optionValues.Add("NODE_PACKAGE", "");
		Options.optionValues.Add("NODE_EXTENDS", "");
		Options.optionValues.Add("NODE_CLASS", "");
		Options.optionValues.Add("NODE_FACTORY", "");
		Options.optionValues.Add("OUTPUT_FILE", "");
		Options.optionValues.Add("VISITOR_DATA_TYPE", "");
		Options.optionValues.Add("VISITOR_EXCEPTION", "");
		Options.optionValues.Add("JJTREE_OUTPUT_DIRECTORY", "");
	}

	
	public static bool getVisitor()
	{
		bool result = Options.booleanValue("VISITOR");
		
		return result;
	}

	
	public static string getVisitorDataType()
	{
		string result = Options.stringValue("VISITOR_DATA_TYPE");
		
		return result;
	}

	
	public static string getVisitorException()
	{
		string result = Options.stringValue("VISITOR_EXCEPTION");
		
		return result;
	}

	
	protected internal JJTreeOptions()
	{
	}

	
	public new static string getJdkVersion()
	{
		string result = Options.stringValue("JDK_VERSION");
		
		return result;
	}

	
	public static bool getNodeDefaultVoid()
	{
		bool result = Options.booleanValue("NODE_DEFAULT_VOID");
		
		return result;
	}

	
	public static bool getNodeScopeHook()
	{
		bool result = Options.booleanValue("NODE_SCOPE_HOOK");
		
		return result;
	}

	
	public static string getNodeFactory()
	{
		string result = Options.stringValue("NODE_FACTORY");
		
		return result;
	}

	
	public static bool getNodeUsesParser()
	{
		bool result = Options.booleanValue("NODE_USES_PARSER");
		
		return result;
	}

	
	public static bool getBuildNodeFiles()
	{
		bool result = Options.booleanValue("BUILD_NODE_FILES");
		
		return result;
	}

	
	public static bool getTrackTokens()
	{
		bool result = Options.booleanValue("TRACK_TOKENS");
		
		return result;
	}

	
	public static string getNodeExtends()
	{
		string result = Options.stringValue("NODE_EXTENDS");
		
		return result;
	}

	
	public static string getNodeClass()
	{
		string result = Options.stringValue("NODE_CLASS");
		
		return result;
	}

	
	public static string getNodePackage()
	{
		string result = Options.stringValue("NODE_PACKAGE");
		
		return result;
	}
}
