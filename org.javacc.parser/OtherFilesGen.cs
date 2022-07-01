using System.Collections;
using System.IO;
using System.Text;

namespace org.javacc.parser;

public class OtherFilesGen : JavaCCParserConstants //JavaCCGlobals, 
{
	public static bool keepLineCol;

	private static TextWriter ostr;
	
	public static void start()
	{
		Token t = null;
		keepLineCol = Options.getKeepLineColumn();
		if (JavaCCErrors._Error_Count != 0)
		{
			throw new MetaParseException();
		}
		JavaFiles.gen_TokenMgrError();
		JavaFiles.gen_ParseException();
		JavaFiles.gen_Token();
		if (Options.getUserTokenManager())
		{
			JavaFiles.gen_TokenManager();
		}
		else if (Options.getUserCharStream())
		{
			JavaFiles.gen_CharStream();
		}
		else if (Options.getJavaUnicodeEscape())
		{
			JavaFiles.gen_JavaCharStream();
		}
		else
		{
			JavaFiles.gen_SimpleCharStream();
		}
		try
		{
			
			ostr = new StreamWriter(
				Path.Combine(Options.getOutputDirectory().FullName, (JavaCCGlobals.cu_name)+("Constants.java")));
		}
		catch (IOException)
		{
			goto IL_00aa;
		}
		var vector = JavaCCGlobals.toolNames.Clone() as ArrayList;
		vector.Add("JavaCC");
		ostr.WriteLine(("/* ")+(JavaCCGlobals.getIdString(vector, (JavaCCGlobals.cu_name)+("Constants.java").ToString()))+(" */")
			.ToString());
		if (JavaCCGlobals.cu_to_insertion_point_1.Count != 0 && ((Token)JavaCCGlobals.cu_to_insertion_point_1[0]).kind == 60)
		{
			for (int i = 1; i < JavaCCGlobals.cu_to_insertion_point_1.Count; i++)
			{
				if (((Token)JavaCCGlobals.cu_to_insertion_point_1[i]).kind == 97)
				{
					JavaCCGlobals.printTokenSetup((Token)JavaCCGlobals.cu_to_insertion_point_1[0]);
					for (int j = 0; j <= i; j++)
					{
						t = (Token)JavaCCGlobals.cu_to_insertion_point_1[j];
						JavaCCGlobals.printToken(t, ostr);
					}
					JavaCCGlobals.printTrailingComments(t, ostr);
					ostr.WriteLine("");
					ostr.WriteLine("");
					break;
				}
			}
		}
		ostr.WriteLine("");
		ostr.WriteLine("/** ");
		ostr.WriteLine(" * Token literal values and constants.");
		ostr.WriteLine(" * Generated by org.javacc.parser.OtherFilesGen#start()");
		ostr.WriteLine(" */");
		ostr.WriteLine(("public interface ")+(JavaCCGlobals.cu_name)+("Constants {")
			.ToString());
		ostr.WriteLine("");
		ostr.WriteLine("  /** End of File. */");
		ostr.WriteLine("  int EOF = 0;");
		Enumeration enumeration = JavaCCGlobals.ordered_named_tokens.elements();
		while (enumeration.hasMoreElements())
		{
			RegularExpression regularExpression = (RegularExpression)enumeration.nextElement();
			ostr.WriteLine("  /** RegularExpression Id. */");
			ostr.WriteLine(("  int ")+(regularExpression.label)+(" = ")
				+(regularExpression.ordinal)
				+(";")
				.ToString());
		}
		ostr.WriteLine("");
		if (!Options.getUserTokenManager() && Options.getBuildTokenManager())
		{
			for (int j = 0; j < (nint)LexGen.lexStateName.LongLength; j++)
			{
				ostr.WriteLine("  /** Lexical state. */");
				ostr.WriteLine(("  int ")+(LexGen.lexStateName[j])+(" = ")
					+(j)
					+(";")
					.ToString());
			}
			ostr.WriteLine("");
		}
		ostr.WriteLine("  /** Literal token values. */");
		ostr.WriteLine("  String[] tokenImage = {");
		ostr.WriteLine("    \"<EOF>\",");
		enumeration = JavaCCGlobals.rexprlist.elements();
		while (enumeration.hasMoreElements())
		{
			TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
			ArrayList respecs = tokenProduction.respecs;
			Enumeration enumeration2 = respecs.elements();
			while (enumeration2.hasMoreElements())
			{
				RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
				RegularExpression regularExpression = regExprSpec.rexp;
				if (regularExpression is RStringLiteral)
				{
					ostr.WriteLine(("    \"\\\"")+(JavaCCGlobals.add_escapes(JavaCCGlobals.add_escapes(((RStringLiteral)regularExpression).image)))+("\\\"\",")
						.ToString());
					continue;
				}
				if (!string.Equals(regularExpression.label, ""))
				{
					ostr.WriteLine(("    \"<")+(regularExpression.label)+(">\",")
						.ToString());
					continue;
				}
				if (regularExpression.tpContext.Kind == 0)
				{
					JavaCCErrors.Warning(regularExpression, "Consider giving this non-string token a label for better error reporting.");
				}
				ostr.WriteLine(("    \"<token of kind ")+(regularExpression.ordinal)+(">\",")
					.ToString());
			}
		}
		ostr.WriteLine("  };");
		ostr.WriteLine("");
		ostr.WriteLine("}");
		ostr.Close();
		return;
		IL_00aa:
		
		JavaCCErrors.Semantic_Error(("Could not open file ")+(JavaCCGlobals.cu_name)+("Constants.java for writing.")
			.ToString());
		
		throw new System.Exception();
	}

	public static void reInit()
	{
		ostr = null;
	}

	
	public OtherFilesGen()
	{
	}

}
