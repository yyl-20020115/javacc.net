using System.Collections;
using System.IO;
using System.Linq;
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
		var vector = JavaCCGlobals.toolNames.ToList();
		vector.Add("JavaCC");
		ostr.WriteLine(("/* ")+(JavaCCGlobals.GetIdStringList(vector, (JavaCCGlobals.cu_name)+("Constants.java")))+(" */")
			);
		if (JavaCCGlobals.cu_to_insertion_point_1.Count != 0 && ((Token)JavaCCGlobals.cu_to_insertion_point_1[0]).kind == 60)
		{
			for (int i = 1; i < JavaCCGlobals.cu_to_insertion_point_1.Count; i++)
			{
				if (((Token)JavaCCGlobals.cu_to_insertion_point_1[i]).kind == 97)
				{
					JavaCCGlobals.PrintTokenSetup((Token)JavaCCGlobals.cu_to_insertion_point_1[0]);
					for (int j = 0; j <= i; j++)
					{
						t = (Token)JavaCCGlobals.cu_to_insertion_point_1[j];
						JavaCCGlobals.PrintToken(t, ostr);
					}
					JavaCCGlobals.PrintTrailingComments(t, ostr);
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
			);
		ostr.WriteLine("");
		ostr.WriteLine("  /** End of File. */");
		ostr.WriteLine("  int EOF = 0;");
		foreach(var regularExpression in JavaCCGlobals.ordered_named_tokens)
		{
			ostr.WriteLine("  /** RegularExpression Id. */");
			ostr.WriteLine(("  int ")+(regularExpression.label)+(" = ")
				+(regularExpression.ordinal)
				+(";")
				);
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
					);
			}
			ostr.WriteLine("");
		}
		ostr.WriteLine("  /** Literal token values. */");
		ostr.WriteLine("  String[] tokenImage = {");
		ostr.WriteLine("    \"<EOF>\",");
		foreach(var tokenProduction in JavaCCGlobals.rexprlist)
		{
			var respecs = tokenProduction.respecs;
			foreach(var regExprSpec in tokenProduction.respecs)
			{
				RegularExpression regularExpression = regExprSpec.rexp;
				if (regularExpression is RStringLiteral)
				{
					ostr.WriteLine(("    \"\\\"")+(JavaCCGlobals.AddEscapes(JavaCCGlobals.AddEscapes(((RStringLiteral)regularExpression).image)))+("\\\"\",")
						);
					continue;
				}
				if (!string.Equals(regularExpression.label, ""))
				{
					ostr.WriteLine(("    \"<")+(regularExpression.label)+(">\",")
						);
					continue;
				}
				if (regularExpression.tpContext.Kind == 0)
				{
					JavaCCErrors.Warning(regularExpression, "Consider giving this non-string token a label for better error reporting.");
				}
				ostr.WriteLine(("    \"<token of kind ")+(regularExpression.ordinal)+(">\",")
					);
			}
		}
		ostr.WriteLine("  };");
		ostr.WriteLine("");
		ostr.WriteLine("}");
		ostr.Close();
		return;
		IL_00aa:
		
		JavaCCErrors.Semantic_Error(("Could not open file ")+(JavaCCGlobals.cu_name)+("Constants.java for writing.")
			);
		
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
