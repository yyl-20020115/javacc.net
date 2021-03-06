namespace JavaCC.Parser;
using System;
using System.Collections.Generic;

public class TokenProduction
{
    public const int TOKEN = 0;
    public const int SKIP = 1;
    public const int MORE = 2;
    public const int SPECIAL = 3;
    public static string[] KindImage =
         new string[] { "TOKEN", "SKIP", "MORE", "SPECIAL" };
    public int Line = 0;
    public int Column = 0;
    public string[] LexStates = Array.Empty<string>();
    public int Kind = 0;
    public readonly List<RegExprSpec> Respecs = new();
    public bool IsExplicit = true;
    public bool IgnoreCase = false;
    public Token FirstToken;
    public Token LastToken;
    public TokenProduction()
    {
    }
}
