using System;
using System.Text;

namespace org.javacc.parser;

public sealed class JavaCCErrors
{
	private static int parse_error_count;
	private static int semantic_error_count;
	private static int warning_count;

	
	public static void Warning(string str)
	{
		Console.Error.Write("Warning: ");
		Console.Error.WriteLine(str);
		warning_count++;
	}

	
	public static void Semantic_Error(string str)
	{
		Console.Error.Write("Error: ");
		Console.Error.WriteLine(str);
		semantic_error_count++;
	}

	public static int Get_Error_Count()
	{
		return parse_error_count + semantic_error_count;
	}

	public static int Get_Warning_Count()
	{
		return warning_count;
	}

	
	public static void Warning(object obj, string str)
	{
		Console.Error.Write("Warning: ");
		PrintLocationInfo(obj);
		Console.Error.WriteLine(str);
		warning_count++;
	}

	
	public static void Parse_Error(object obj, string str)
	{
		Console.Error.Write("Error: ");
		PrintLocationInfo(obj);
		Console.Error.WriteLine(str);
		parse_error_count++;
	}

	
	public static void Semantic_Error(object obj, string str)
	{
		Console.Error.Write("Error: ");
		PrintLocationInfo(obj);
		Console.Error.WriteLine(str);
		semantic_error_count++;
	}

	
	private static void PrintLocationInfo(object P_0)
	{
		if (P_0 is NormalProduction)
		{
			NormalProduction normalProduction = (NormalProduction)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(normalProduction.line).Append(", Column ")
				.Append(normalProduction.column)
				.Append(": ")
				.ToString());
		}
		else if (P_0 is TokenProduction)
		{
			TokenProduction tokenProduction = (TokenProduction)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(tokenProduction.line).Append(", Column ")
				.Append(tokenProduction.column)
				.Append(": ")
				.ToString());
		}
		else if (P_0 is Expansion)
		{
			Expansion expansion = (Expansion)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(expansion.line).Append(", Column ")
				.Append(expansion.column)
				.Append(": ")
				.ToString());
		}
		else if (P_0 is CharacterRange)
		{
			CharacterRange characterRange = (CharacterRange)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(characterRange.line).Append(", Column ")
				.Append(characterRange.column)
				.Append(": ")
				.ToString());
		}
		else if (P_0 is SingleCharacter)
		{
			SingleCharacter singleCharacter = (SingleCharacter)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(singleCharacter.line).Append(", Column ")
				.Append(singleCharacter.column)
				.Append(": ")
				.ToString());
		}
		else if (P_0 is Token)
		{
			Token token = (Token)P_0;
			Console.Error.Write(new StringBuilder().Append("Line ").Append(token.beginLine).Append(", Column ")
				.Append(token.beginColumn)
				.Append(": ")
				.ToString());
		}
	}

	
	private JavaCCErrors()
	{
	}

	
	public static void Parse_Error(string str)
	{
		Console.Error.Write("Error: ");
		Console.Error.WriteLine(str);
		parse_error_count++;
	}

	public static int Get_Parse_Error_Count()
	{
		return parse_error_count;
	}

	public static int Get_Semantic_Error_Count()
	{
		return semantic_error_count;
	}

	public static void ReInit()
	{
		parse_error_count = 0;
		semantic_error_count = 0;
		warning_count = 0;
	}
}
