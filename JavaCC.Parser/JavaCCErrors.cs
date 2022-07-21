namespace JavaCC.Parser;
using System;

public class JavaCCErrors
{
    protected static int ParseErrorCount = 0;
    protected static int SemanticErrorCount = 0;
    protected static int _WarningCount = 0;
    public static int WarningCount => _WarningCount;
    public static string Warning(string text)
    {
        Console.Error.Write("Warning: ");
        Console.Error.WriteLine(text);
        _WarningCount++;
        return text; 
    }

    public static string Semantic_Error(string text)
    {
        Console.Error.Write("Error: ");
        Console.Error.WriteLine(text);
        SemanticErrorCount++;
        return text;
    }

    public static int ErrorCount => ParseErrorCount + SemanticErrorCount;

    public static string Warning(object info, string text)
    {
        Console.Error.Write("Warning: ");
        PrintLocationInfo(info);
        Console.Error.WriteLine(text);
        _WarningCount++;
        return text;
    }


    public static string Parse_Error(object info, string text)
    {
        Console.Error.Write("Error: ");
        PrintLocationInfo(info);
        Console.Error.WriteLine(text);
        ParseErrorCount++;
        return text;
    }


    public static string Semantic_Error(object info, string text)
    {
        Console.Error.Write("Error: ");
        PrintLocationInfo(info);
        Console.Error.WriteLine(text);
        SemanticErrorCount++;
        return text;

    }


    private static void PrintLocationInfo(object info)
    {
        if (info is NormalProduction production)
        {
            Console.Error.Write(("Line ") + (production.Line) + (", Column ")
                + (production.Column)
                + (": ")
                );
        }
        else if (info is TokenProduction token_production)
        {
            Console.Error.Write(("Line ") + (token_production.Line) + (", Column ")
                + (token_production.Column)
                + (": "))
                ;
        }
        else if (info is Expansion expansion)
        {
            Console.Error.Write(("Line ") + (expansion.Line) + (", Column ")
                + (expansion.Column)
                + (": ")
                );
        }
        else if (info is CharacterRange range)
        {
            Console.Error.Write(("Line ") + (range.Line) + (", Column ")
                + (range.Column)
                + (": ")
                );
        }
        else if (info is SingleCharacter schar)
        {
            Console.Error.Write(("Line ") + (schar.Line) + (", Column ")
                + (schar.Column)
                + (": ")
                );
        }
        else if (info is Token token)
        {
            Console.Error.Write(("Line ") + (token.BeginLine) + (", Column ")
                + (token.BeginColumn)
                + (": ")
                );
        }
    }

    public static string Parse_Error(string text)
    {
        Console.Error.Write("Error: ");
        Console.Error.WriteLine(text);
        ParseErrorCount++;
        return text;
    }
}
