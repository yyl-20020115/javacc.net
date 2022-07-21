namespace JavaCC.Parser;

using System;
using System.IO;
using System.Linq;

public class OtherFilesGen : JavaCCParserConstants //JavaCCGlobals, 
{
	public static bool KeepLineCol = false;

	private static TextWriter writer;
	
	public static void Start()
	{
		Token token = null;
		KeepLineCol = Options.KeepLineColumn;
		if (JavaCCErrors.ErrorCount != 0)
		{
			throw new MetaParseException();
		}
		JavaFiles.GenTokenMgrError();
		JavaFiles.GenParseException();
		JavaFiles.GenToken();
		if (Options.UserTokenManager)
		{
			JavaFiles.GenTokenManager();
		}
		else if (Options.UserCharStream)
		{
			JavaFiles.GenCharStream();
		}
		else if (Options.JavaUnicodeEscape)
		{
			JavaFiles.GenJavaCharStream();
		}
		else
		{
			JavaFiles.GenSimpleCharStream();
		}
		try
		{
			writer = new StreamWriter(
				Path.Combine(Options.OutputDirectory.FullName, (JavaCCGlobals.CuName)+("Constants.java")));
		}
		catch (IOException)
		{
			throw new Exception(
				JavaCCErrors.Semantic_Error(("Could not open file ") + (JavaCCGlobals.CuName) + ("Constants.java for writing.")
				));
		}
		var vector = JavaCCGlobals.ToolNames.ToList();
		vector.Add("JavaCC");
		writer.WriteLine(("/* ")+(JavaCCGlobals.GetIdStringList(vector, (JavaCCGlobals.CuName)+("Constants.java")))+(" */")
			);
		if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && (JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
		{
			for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
			{
				if ((JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
				{
					JavaCCGlobals.PrintTokenSetup(JavaCCGlobals.CuToInsertionPoint1[0]);
					for (int j = 0; j <= i; j++)
					{
						token = JavaCCGlobals.CuToInsertionPoint1[j];
						JavaCCGlobals.PrintToken(token, writer);
					}
					JavaCCGlobals.PrintTrailingComments(token, writer);
					writer.WriteLine("");
					writer.WriteLine("");
					break;
				}
			}
		}
		writer.WriteLine("");
		writer.WriteLine("/** ");
		writer.WriteLine(" * Token literal values and constants.");
		writer.WriteLine(" * Generated by Javacc.Parser.OtherFilesGen#start()");
		writer.WriteLine(" */");
		writer.WriteLine(("public interface ")+(JavaCCGlobals.CuName)+("Constants {")
			);
		writer.WriteLine("");
		writer.WriteLine("  /** End of File. */");
		writer.WriteLine("  int EOF = 0;");
		foreach(var regularExpression in JavaCCGlobals.OrderedNamedTokens)
		{
			writer.WriteLine("  /** RegularExpression Id. */");
			writer.WriteLine(("  int ")+(regularExpression.Label)+(" = ")
				+(regularExpression.Ordinal)
				+(";")
				);
		}
		writer.WriteLine("");
		if (!Options.UserTokenManager && Options.BuildTokenManager)
		{
			for (int j = 0; j < LexGen.lexStateName.Length; j++)
			{
				writer.WriteLine("  /** Lexical state. */");
				writer.WriteLine(("  int ")+(LexGen.lexStateName[j])+(" = ")
					+(j)
					+(";")
					);
			}
			writer.WriteLine("");
		}
		writer.WriteLine("  /** Literal token values. */");
		writer.WriteLine("  String[] tokenImage = {");
		writer.WriteLine("    \"<EOF>\",");
		foreach(var tokenProduction in JavaCCGlobals.RexprList)
		{
			var respecs = tokenProduction.Respecs;
			foreach(var regExprSpec in tokenProduction.Respecs)
			{
				RegularExpression regularExpression = regExprSpec.Rexp;
				if (regularExpression is RStringLiteral literal)
				{
					writer.WriteLine(("    \"\\\"")+(JavaCCGlobals.AddEscapes(JavaCCGlobals.AddEscapes(literal.Image)))+("\\\"\",")
						);
					continue;
				}
				if (!string.Equals(regularExpression.Label, ""))
				{
					writer.WriteLine(("    \"<")+(regularExpression.Label)+(">\",")
						);
					continue;
				}
				if (regularExpression.TpContext.Kind == 0)
				{
					JavaCCErrors.Warning(regularExpression, "Consider giving this non-string token a label for better error reporting.");
				}
				writer.WriteLine(("    \"<token of kind ")+(regularExpression.Ordinal)+(">\",")
					);
			}
		}
		writer.WriteLine("  };");
		writer.WriteLine("");
		writer.WriteLine("}");
		writer.Close();
		
	}

	public static void ReInit()
	{
		writer = null;
	}
}
