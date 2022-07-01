using System;
using System.Collections.Generic;

namespace org.javacc.parser;

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
	public List<RegExprSpec> respecs = new();
	public bool isExplicit = true;
	public bool ignoreCase = false;
	public Token firstToken;
	public Token lastToken;
    public TokenProduction()
	{
	}
}
