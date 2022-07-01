using System;

namespace org.javacc.parser;

public sealed class JavaCCErrors
{
	private static int parse_error_count = 0;
	private static int semantic_error_count = 0;
	private static int warning_count = 0;

	
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

    public static int _Error_Count => parse_error_count + semantic_error_count;

    public static int _Warning_Count => warning_count;


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

	
	private static void PrintLocationInfo(object _loc)
	{
		if (_loc is NormalProduction normalProduction)
		{
			Console.Error.Write(("Line ")+(normalProduction.line)+(", Column ")
				+(normalProduction.column)
				+(": ")
				);
		}
		else if (_loc is TokenProduction tokenProduction)
		{
			Console.Error.Write(("Line ")+(tokenProduction.Line)+(", Column ")
				+(tokenProduction.Column)
				+(": "))
				;
		}
		else if (_loc is Expansion loc)
		{
			Console.Error.Write(("Line ")+(loc.Line)+(", Column ")
				+(loc.Column)
				+(": ")
				);
		}
		else if (_loc is CharacterRange locr)
		{
			Console.Error.Write(("Line ")+(locr.Line)+(", Column ")
				+(locr.Column)
				+(": ")
				);
		}
		else if (_loc is SingleCharacter locs)
		{
			Console.Error.Write(("Line ")+(locs.line)+(", Column ")
				+(locs.column)
				+(": ")
				);
		}
		else if (_loc is Token token)
		{
			Console.Error.Write(("Line ")+(token.BeginLine)+(", Column ")
				+(token.BeginColumn)
				+(": ")
				);
		}
	}


	
	public static void Parse_Error(string str)
	{
		Console.Error.Write("Error: ");
		Console.Error.WriteLine(str);
		parse_error_count++;
	}

    public static int _Parse_Error_Count => parse_error_count;

    public static int _Semantic_Error_Count => semantic_error_count;
}
