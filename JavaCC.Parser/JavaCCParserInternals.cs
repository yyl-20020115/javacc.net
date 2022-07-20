namespace JavaCC.Parser;
using System.Collections.Generic;

public abstract class JavaCCParserInternals : JavaCCGlobals
{
    private static List<Token> add_cu_token_here;

    private static Token first_cu_token;

    private static bool insertionpoint1set;

    private static bool insertionpoint2set;

    private static int nextFreeLexState;


    protected internal static bool Hexchar(char ch) => ch switch
    {
        >= '0' and <= '9' => true,
        >= 'A' and <= 'F' => true,
        >= 'a' and <= 'f' => true,
        _ => false
    };

    protected internal static int HexVal(char ch) => ch switch
    {
        >= '0' and <= '9' => ch - 48,
        >= 'A' and <= 'F' => ch - 65 + 10,
        _ => ch - 97 + 10
    };


    public JavaCCParserInternals()
    {
    }


    protected internal static void Initialize()
    {
        int integer = (0);
        JavaCCGlobals.lexstate_S2I.Add("DEFAULT", integer);
        JavaCCGlobals.lexstate_I2S.Add(integer, "DEFAULT");
        JavaCCGlobals.simple_tokens_table.Add("DEFAULT", new ());
    }

    protected internal static void AddCuname(string str)
    {
        JavaCCGlobals.Cu_name = str;
    }


    protected internal static void Compare(Token t, string str1, string str2)
    {
        if (!string.Equals(str2, str1))
        {
            JavaCCErrors.Parse_Error(t, ("Name ") + (str2) + (" must be the same as that used at PARSER_BEGIN (")
                + (str1)
                + (")")
                );
        }
    }


    protected internal static void SetInsertionPoint(Token t, int i)
    {
        do
        {
            add_cu_token_here.Add(first_cu_token);
            first_cu_token = first_cu_token.Next;
        }
        while (first_cu_token != t);
        if (i == 1)
        {
            if (insertionpoint1set)
            {
                JavaCCErrors.Parse_Error(t, "Multiple declaration of parser class.");
            }
            else
            {
                insertionpoint1set = true;
                add_cu_token_here = JavaCCGlobals.Cu_to_insertion_point_2;
            }
        }
        else
        {
            add_cu_token_here = JavaCCGlobals.Cu_from_insertion_point_2;
            insertionpoint2set = true;
        }
        first_cu_token = t;
    }


    protected internal static void InsertionPointError(Token t)
    {
        while (first_cu_token != t)
        {
            add_cu_token_here.Add(first_cu_token);
            first_cu_token = first_cu_token.Next;
        }
        if (!insertionpoint1set || !insertionpoint2set)
        {
            JavaCCErrors.Parse_Error(t, "Parser class has not been defined between PARSER_BEGIN and PARSER_END.");
        }
    }

    protected internal static void SetInitialCuToken(Token t)
    {
        first_cu_token = t;
    }


    protected internal static void AddProduction(NormalProduction np)
    {
        JavaCCGlobals.BNFProductions.Add(np);
    }

    protected internal static void ProductionAddExpansion(BNFProduction bnfp, Expansion e)
    {
        e.parent = bnfp;
        bnfp.Expansion = e;
    }


    protected internal static void AddRegexpr(TokenProduction tp)
    {
        JavaCCGlobals.rexprlist.Add(tp);
        if (Options.UserTokenManager && (tp.LexStates == null || tp.LexStates.Length != 1 || !string.Equals(tp.LexStates[0], "DEFAULT")))
        {
            JavaCCErrors.Warning(tp, "Ignoring lexical state specifications since option USER_TOKEN_MANAGER has been set to true.");
        }
        if (tp.LexStates == null)
        {
            return;
        }
        for (int i = 0; i < tp.LexStates.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (string.Equals(tp.LexStates[i], tp.LexStates[j]))
                {
                    JavaCCErrors.Parse_Error(tp, ("Multiple occurrence of \"") + (tp.LexStates[i]) + ("\" in lexical state list.")
                        );
                }
            }
            if (!JavaCCGlobals.lexstate_S2I.TryGetValue(tp.LexStates[i], out var _))
            {
                int integer = (nextFreeLexState++);
                JavaCCGlobals.lexstate_S2I.Add(tp.LexStates[i], integer);
                JavaCCGlobals.lexstate_I2S.Add(integer, tp.LexStates[i]);
                JavaCCGlobals.simple_tokens_table.Add(tp.LexStates[i], new ());
            }
        }
    }


    protected internal static void AddTokenManagerDecl(Token t, List<Token> v)
    {
        if (JavaCCGlobals.token_mgr_decls != null)
        {
            JavaCCErrors.Parse_Error(t, "Multiple occurrence of \"TOKEN_MGR_DECLS\".");
            return;
        }
        JavaCCGlobals.token_mgr_decls = v;
        if (Options.UserTokenManager)
        {
            JavaCCErrors.Warning(t, "Ignoring declarations in \"TOKEN_MGR_DECLS\" since option USER_TOKEN_MANAGER has been set to true.");
        }
    }


    protected internal static void AddInlineRegexpr(RegularExpression re)
    {
        if (re is not REndOfFile)
        {
            var tokenProduction = new TokenProduction
            {
                IsExplicit = false,
                LexStates = new string[1] { "DEFAULT" },
                Kind = 0
            };
            var regExprSpec = new RegExprSpec
            {
                rexp = re,
                act = new Action(),
                nextState = null,
                nsTok = null
            };
            regExprSpec.rexp.tpContext = tokenProduction;
            tokenProduction.Respecs.Add(regExprSpec);
            JavaCCGlobals.rexprlist.Add(tokenProduction);
        }
    }


    protected internal static string RemoveEscapesAndQuotes(Token t, string str)
    {
        string text = "";
        int num = 1;
        while (num < str.Length - 1)
        {
            if (str[num] != '\\')
            {
                text = (text) + (str[num]);
                num++;
                continue;
            }
            num++;
            int num2 = str[num];
            switch (num2)
            {
                case 98:
                    text = (text) + ('\b');
                    num++;
                    continue;
                case 116:
                    text = (text) + ('\t');
                    num++;
                    continue;
                case 110:
                    text = (text) + ('\n');
                    num++;
                    continue;
                case 102:
                    text = (text) + ('\f');
                    num++;
                    continue;
                case 114:
                    text = (text) + ('\r');
                    num++;
                    continue;
                case 34:
                    text = (text) + ('"');
                    num++;
                    continue;
                case 39:
                    text = (text) + ('\'');
                    num++;
                    continue;
                case 92:
                    text = (text) + ('\\');
                    num++;
                    continue;
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    {
                        int num3 = num2 - 48;
                        num++;
                        int num4 = str[num];
                        if (num4 >= 48 && num4 <= 55)
                        {
                            num3 = num3 * 8 + num4 - 48;
                            num++;
                            num4 = str[num];
                            if (num2 <= 51 && num4 >= 48 && num4 <= 55)
                            {
                                num3 = num3 * 8 + num4 - 48;
                                num++;
                            }
                        }
                        text = (text) + ((char)num3);
                        continue;
                    }
            }
            if (num2 == 117)
            {
                num++;
                num2 = str[num];
                if (Hexchar((char)num2))
                {
                    int num3 = HexVal((char)num2);
                    num++;
                    num2 = str[num];
                    if (Hexchar((char)num2))
                    {
                        num3 = num3 * 16 + HexVal((char)num2);
                        num++;
                        num2 = str[num];
                        if (Hexchar((char)num2))
                        {
                            num3 = num3 * 16 + HexVal((char)num2);
                            num++;
                            num2 = str[num];
                            if (Hexchar((char)num2))
                            {
                                _ = num3 * 16 + HexVal((char)num2);
                                num++;
                                continue;
                            }
                        }
                    }
                }
                JavaCCErrors.Parse_Error(t, ("Encountered non-hex character '") + ((char)num2) + ("' at position ")
                    + (num)
                    + (" of string ")
                    + ("- Unicode escape must have 4 hex digits after it.")
                    );
                return text;
            }
            JavaCCErrors.Parse_Error(t, ("Illegal escape sequence '\\") + ((char)num2) + ("' at position ")
                + (num)
                + (" of string.")
                );
            return text;
        }
        return text;
    }


    protected internal static char CharDescriptorAssign(Token t, string str)
    {
        if (str.Length != 1)
        {
            JavaCCErrors.Parse_Error(t, "String in character list may contain only one character.");
            return ' ';
        }
        char result = str[0];

        return result;
    }


    protected internal static char CharDescriptorAssign(Token t, string str1, string str2)
    {
        if (str1.Length != 1)
        {
            JavaCCErrors.Parse_Error(t, "String in character list may contain only one character.");
            return ' ';
        }
        if (str2[0] > str1[0])
        {
            JavaCCErrors.Parse_Error(t, ("Right end of character range '") + (str1) + ("' has a lower ordinal value than the left end of character range '")
                + (str2)
                + ("'.")
                );
            char result = str2[0];

            return result;
        }
        char result2 = str1[0];

        return result2;
    }


    protected internal static void MakeTryBlock(Token t, Container c1, Container c2, List<List<Token>> v1, List<Token> v2, List<List<Token>> v3, List<Token> v4)
    {
        if (v3.Count == 0 && v4 == null)
        {
            JavaCCErrors.Parse_Error(t, "Try block must contain at least one catch or finally block.");
            return;
        }
        var tryBlock = new TryBlock
        {
            Line = t.BeginLine,
            Column = t.BeginColumn,
            Expression = c2.member as Expansion,
            Types = v1,
            Ids = v2,
            CatchBlocks = v3,
            FinallyBlock = v4
        };
        tryBlock.Expression.parent = tryBlock;
        tryBlock.Expression.ordinal = 0;
        c1.member = tryBlock;
    }

    public new static void ReInit()
    {
        add_cu_token_here = JavaCCGlobals.Cu_to_insertion_point_1;
        first_cu_token = null;
        insertionpoint1set = false;
        insertionpoint2set = false;
        nextFreeLexState = 1;
    }

    static JavaCCParserInternals()
    {
        add_cu_token_here = JavaCCGlobals.Cu_to_insertion_point_1;
        insertionpoint1set = false;
        insertionpoint2set = false;
        nextFreeLexState = 1;
    }
}
