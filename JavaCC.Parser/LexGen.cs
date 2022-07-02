namespace JavaCC.Parser;
using JavaCC.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LexGen : JavaCCParserConstants //JavaCCGlobals, 
{
    private static TextWriter writer;

    private static string staticString;

    private static string tokMgrClassName;

    internal static Dictionary<string, List<TokenProduction>> allTpsForState = new();

    public static int lexStateIndex;

    internal static int[] kinds;

    public static int maxOrdinal;

    public static string lexStateSuffix;

    internal static string[] newLexState;

    public static int[] lexStates;

    public static bool[] ignoreCase;

    public static Action[] actions;

    public static Dictionary<string,NfaState> initStates = new();

    public static int stateSetSize;

    public static int maxLexStates;

    public static string[] lexStateName;

    internal static NfaState[] singlesToSkip;

    public static long[] toSkip;

    public static long[] toSpecial;

    public static long[] toMore;

    public static long[] toToken;

    public static int defaultLexState;

    public static RegularExpression[] rexprs;

    public static int[] maxLongsReqd;

    public static int[] initMatch;

    public static int[] canMatchAnyChar;

    public static bool hasEmptyMatch;

    public static bool[] canLoop;

    public static bool[] stateHasActions;

    public static bool hasLoop;

    public static bool[] canReachOnMore;

    public static bool[] hasNfa;

    public static bool[] mixed;

    public static NfaState initialState;

    public static int curKind;

    internal static bool hasSkipActions;

    internal static bool hasMoreActions;

    internal static bool hasTokenActions;

    internal static bool hasSpecial;

    internal static bool hasSkip;

    internal static bool hasMore;

    public static RegularExpression curRE;

    public static bool keepLineCol;


    internal static void PrintClassHead()
    {
        try
        {

            var file = new FileInfo(
                Path.Combine(Options.OutputDirectory.FullName, tokMgrClassName) + (".java"));

            writer = new StreamWriter(file.FullName);
            var vector = JavaCCGlobals.ToolNames.ToList();
            vector.Add("JavaCC");
            writer.WriteLine(("/* ") + (JavaCCGlobals.GetIdStringList(vector, (tokMgrClassName) + (".java"))) + (" */")
                );
            int num = 0;
            int i = 1;
            while (JavaCCGlobals.Cu_to_insertion_point_1.Count > num)
            {
                int kind = ((Token)JavaCCGlobals.Cu_to_insertion_point_1[num]).kind;
                if (kind != 60 && kind != 52)
                {
                    break;
                }
                for (; i < JavaCCGlobals.Cu_to_insertion_point_1.Count; i++)
                {
                    kind = ((Token)JavaCCGlobals.Cu_to_insertion_point_1[i]).kind;
                    if (kind == 97 || kind == 27 || kind == 45 || kind == 63 || kind == 35 || kind == 55)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.Cu_to_insertion_point_1[num]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.Cu_to_insertion_point_1[num]).BeginColumn;
                        int j;
                        for (j = num; j < i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.Cu_to_insertion_point_1[j], writer);
                        }
                        if (kind == 97)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.Cu_to_insertion_point_1[j], writer);
                        }
                        writer.WriteLine("");
                        break;
                    }
                }
                i++;
                num = i;
            }
            writer.WriteLine("");
            writer.WriteLine("/** Token Manager. */");
            writer.WriteLine(("public class ") + (tokMgrClassName) + (" implements ")
                + (JavaCCGlobals.Cu_name)
                + ("Constants")
                );
            writer.WriteLine("{");
        }
        catch (IOException)
        {
            goto IL_0277;
        }
        if (JavaCCGlobals.token_mgr_decls != null && JavaCCGlobals.token_mgr_decls.Count > 0)
        {
            _ = (Token)JavaCCGlobals.token_mgr_decls[0];
            int num2 = 0;
            int num = (Options.CommonTokenAction ? 1 : 0);
            JavaCCGlobals.PrintTokenSetup((Token)JavaCCGlobals.token_mgr_decls[0]);
            JavaCCGlobals.ccol = 1;
            for (int j = 0; j < JavaCCGlobals.token_mgr_decls.Count; j++)
            {
                Token token = (Token)JavaCCGlobals.token_mgr_decls[j];
                if (token.kind == 140 && num != 0 && num2 == 0)
                {
                    num2 = (string.Equals(token.image, "CommonTokenAction") ? 1 : 0);
                }
                JavaCCGlobals.PrintToken(token, writer);
            }
            writer.WriteLine("");
            if (num != 0 && num2 == 0)
            {
                JavaCCErrors.Warning(("You have the COMMON_TOKEN_ACTION option set. But it appears you have not defined the method :\n      ") + (staticString) + ("void CommonTokenAction(Token t)\n")
                    + ("in your TOKEN_MGR_DECLS. The generated token manager will not compile.")
                    );
            }
        }
        else if (Options.CommonTokenAction)
        {
            JavaCCErrors.Warning(("You have the COMMON_TOKEN_ACTION option set. But you have not defined the method :\n      ") + (staticString) + ("void CommonTokenAction(Token t)\n")
                + ("in your TOKEN_MGR_DECLS. The generated token manager will not compile.")
                );
        }
        writer.WriteLine("");
        writer.WriteLine("  /** Debug output. */");
        writer.WriteLine(("  public ") + (staticString) + (" java.io.TextWriter debugStream = System.out;")
            );
        writer.WriteLine("  /** HashSet<object> debug output. */");
        writer.WriteLine(("  public ") + (staticString) + (" void setDebugStream(java.io.TextWriter ds) { debugStream = ds; }")
            );
        if (Options.TokenManagerUsesParser && !Options.getStatic())
        {
            writer.WriteLine("");
            writer.WriteLine("  /** The parser. */");
            writer.WriteLine(("  public ") + (JavaCCGlobals.Cu_name) + (" parser = null;")
                );
        }
        return;
    IL_0277:

        JavaCCErrors.Semantic_Error(("Could not create file : ") + (tokMgrClassName) + (".java\n")
            );

        throw new System.Exception();
    }


    internal static void BuildLexStatesTable()
    {
        string[] array = new string[JavaCCGlobals.lexstate_I2S.Count];

        foreach (var tokenProduction in JavaCCGlobals.rexprlist)
        {
            var respecs = tokenProduction.Respecs;
            for (int i = 0; i < tokenProduction.LexStates.Length; i++)
            {
                if (!allTpsForState.TryGetValue(tokenProduction.LexStates[i], out var vector))
                {
                    array[maxLexStates++] = tokenProduction.LexStates[i];
                    allTpsForState.Add(tokenProduction.LexStates[i], vector = new());
                }
                vector.Add(tokenProduction);
            }
            if (respecs == null || respecs.Count == 0)
            {
                continue;
            }
            for (int i = 0; i < respecs.Count; i++)
            {
                RegularExpression rexp;
                if (maxOrdinal <= (rexp = ((RegExprSpec)respecs[i]).rexp).ordinal)
                {
                    maxOrdinal = rexp.ordinal + 1;
                }
            }
        }
        kinds = new int[maxOrdinal];
        toSkip = new long[maxOrdinal / 64 + 1];
        toSpecial = new long[maxOrdinal / 64 + 1];
        toMore = new long[maxOrdinal / 64 + 1];
        toToken = new long[maxOrdinal / 64 + 1];
        toToken[0] = 1L;
        actions = new Action[maxOrdinal];
        actions[0] = JavaCCGlobals.actForEof;
        hasTokenActions = JavaCCGlobals.actForEof != null;
        initStates = new();
        canMatchAnyChar = new int[maxLexStates];
        canLoop = new bool[maxLexStates];
        stateHasActions = new bool[maxLexStates];
        lexStateName = new string[maxLexStates];
        singlesToSkip = new NfaState[maxLexStates];
        Array.Copy(array, 0, lexStateName, 0, maxLexStates);
        for (int i = 0; i < maxLexStates; i++)
        {
            canMatchAnyChar[i] = -1;
        }
        hasNfa = new bool[maxLexStates];
        mixed = new bool[maxLexStates];
        maxLongsReqd = new int[maxLexStates];
        initMatch = new int[maxLexStates];
        newLexState = new string[maxOrdinal];
        newLexState[0] = JavaCCGlobals.nextStateForEof;
        hasEmptyMatch = false;
        lexStates = new int[maxOrdinal];
        ignoreCase = new bool[maxOrdinal];
        rexprs = new RegularExpression[maxOrdinal];
        RStringLiteral.allImages = new string[maxOrdinal];
        canReachOnMore = new bool[maxLexStates];
    }


    internal static int GetIndex(string P_0)
    {
        for (int i = 0; i < lexStateName.Length; i++)
        {
            if (lexStateName[i] != null && string.Equals(lexStateName[i], P_0))
            {
                return i;
            }
        }

        throw new System.Exception();
    }


    internal static void CheckEmptyStringMatch()
    {
        bool[] array = new bool[maxLexStates];
        bool[] array2 = new bool[maxLexStates];
        for (int i = 0; i < maxLexStates; i++)
        {
            if (array2[i] || initMatch[i] == 0 || initMatch[i] == int.MaxValue || canMatchAnyChar[i] != -1)
            {
                continue;
            }
            array2[i] = true;
            int num = 0;
            string str = "";
            string str2 = "";
            for (int j = 0; j < maxLexStates; j++)
            {
                array[j] = false;
            }
            int num2 = i;
            array[i] = true;
            str = (str) + (lexStateName[num2]) + ("-->")
                ;
            while (true)
            {
                if (newLexState[initMatch[num2]] != null)
                {
                    str = (str) + (newLexState[initMatch[num2]]);
                    if (!array[num2 = GetIndex(newLexState[initMatch[num2]])])
                    {
                        str = (str) + ("-->");
                        array2[num2] = true;
                        array[num2] = true;
                        if (initMatch[num2] == 0 || initMatch[num2] == int.MaxValue || canMatchAnyChar[num2] != -1)
                        {
                            break;
                        }
                        if (num != 0)
                        {
                            str2 = (str2) + ("; ");
                        }
                        str2 = (str2) + ("line ") + (rexprs[initMatch[num2]].Line)
                            + (", column ")
                            + (rexprs[initMatch[num2]].Column)
                            ;
                        num++;
                        continue;
                    }
                }
                if (newLexState[initMatch[num2]] == null)
                {
                    str = (str) + (lexStateName[lexStates[initMatch[num2]]]);
                }
                for (int j = 0; j < maxLexStates; j++)
                {
                    bool[] array3 = canLoop;
                    int num3 = j;
                    bool[] array4 = array3;
                    array4[num3] |= array[j];
                }
                hasLoop = true;
                if (num == 0)
                {
                    JavaCCErrors.Warning(rexprs[initMatch[i]], ("Regular expression") + ((!string.Equals(rexprs[initMatch[i]].label, "")) ? (" for ") + (rexprs[initMatch[i]].label) : "") + (" can be matched by the empty string (\"\") in lexical state ")
                        + (lexStateName[i])
                        + (". This can result in an endless loop of ")
                        + ("empty string matches.")
                        );
                }
                else
                {
                    JavaCCErrors.Warning(rexprs[initMatch[i]], ("Regular expression") + ((!string.Equals(rexprs[initMatch[i]].label, "")) ? (" for ") + (rexprs[initMatch[i]].label) : "") + (" can be matched by the empty string (\"\") in lexical state ")
                        + (lexStateName[i])
                        + (". This regular expression along with the ")
                        + ("regular expressions at ")
                        + (str2)
                        + (" forms the cycle \n   ")
                        + (str)
                        + ("\ncontaining regular expressions with empty matches.")
                        + (" This can result in an endless loop of empty string matches.")
                        );
                }
                break;
            }
        }
    }


    internal static void DumpStaticVarDeclarations()
    {
        writer.WriteLine("");
        writer.WriteLine("/** Lexer state names. */");
        writer.WriteLine("public static final String[] lexStateNames = {");
        for (int i = 0; i < maxLexStates; i++)
        {
            writer.WriteLine(("   \"") + (lexStateName[i]) + ("\", ")
                );
        }
        writer.WriteLine("};");
        if (maxLexStates > 1)
        {
            writer.WriteLine("");
            writer.WriteLine("/** Lex State array. */");
            writer.Write("public static final int[] jjnewLexState = {");
            for (int i = 0; i < maxOrdinal; i++)
            {
                int num = i;
                if (25 == -1 || num % 25 == 0)
                {
                    writer.Write("\n   ");
                }
                if (newLexState[i] == null)
                {
                    writer.Write("-1, ");
                }
                else
                {
                    writer.Write((GetIndex(newLexState[i])) + (", "));
                }
            }
            writer.WriteLine("\n};");
        }
        if (hasSkip || hasMore || hasSpecial)
        {
            writer.Write("static final long[] jjtoToken = {");
            for (int i = 0; i < maxOrdinal / 64 + 1; i++)
            {
                int num2 = i;
                if (4 == -1 || num2 % 4 == 0)
                {
                    writer.Write("\n   ");
                }
                writer.Write(("0x") + (Utils.ToHexString(toToken[i])) + ("L, ")
                    );
            }
            writer.WriteLine("\n};");
        }
        if (hasSkip || hasSpecial)
        {
            writer.Write("static final long[] jjtoSkip = {");
            for (int i = 0; i < maxOrdinal / 64 + 1; i++)
            {
                int num3 = i;
                if (4 == -1 || num3 % 4 == 0)
                {
                    writer.Write("\n   ");
                }
                writer.Write(("0x") + (Utils.ToHexString(toSkip[i])) + ("L, ")
                    );
            }
            writer.WriteLine("\n};");
        }
        if (hasSpecial)
        {
            writer.Write("static final long[] jjtoSpecial = {");
            for (int i = 0; i < maxOrdinal / 64 + 1; i++)
            {
                int num4 = i;
                if (4 == -1 || num4 % 4 == 0)
                {
                    writer.Write("\n   ");
                }
                writer.Write(("0x") + (Utils.ToHexString(toSpecial[i])) + ("L, ")
                    );
            }
            writer.WriteLine("\n};");
        }
        if (hasMore)
        {
            writer.Write("static final long[] jjtoMore = {");
            for (int i = 0; i < maxOrdinal / 64 + 1; i++)
            {
                int num5 = i;
                if (4 == -1 || num5 % 4 == 0)
                {
                    writer.Write("\n   ");
                }
                writer.Write(("0x") + (Utils.ToHexString(toMore[i])) + ("L, ")
                    );
            }
            writer.WriteLine("\n};");
        }
        string str = (Options.UserCharStream ? "CharStream" : ((!Options.JavaUnicodeEscape) ? "SimpleCharStream" : "JavaCharStream"));
        writer.WriteLine((staticString) + ("protected ") + (str)
            + (" input_stream;")
            );
        writer.WriteLine((staticString) + ("private final int[] jjrounds = ") + ("new int[")
            + (stateSetSize)
            + ("];")
            );
        writer.WriteLine((staticString) + ("private final int[] jjstateSet = ") + ("new int[")
            + (2 * stateSetSize)
            + ("];")
            );
        if (hasMoreActions || hasSkipActions || hasTokenActions)
        {
            writer.WriteLine((staticString) + (Options.StringBufOrBuild) + (" image;")
                );
            writer.WriteLine((staticString) + ("int jjimageLen;"));
            writer.WriteLine((staticString) + ("int lengthOfMatch;"));
        }
        writer.WriteLine((staticString) + ("protected char curChar;"));
        if (Options.TokenManagerUsesParser && !Options.getStatic())
        {
            writer.WriteLine("");
            writer.WriteLine("/** Constructor with parser. */");
            writer.WriteLine(("public ") + (tokMgrClassName) + ("(")
                + (JavaCCGlobals.Cu_name)
                + (" parserArg, ")
                + (str)
                + (" stream){")
                );
            writer.WriteLine("   parser = parserArg;");
        }
        else
        {
            writer.WriteLine("/** Constructor. */");
            writer.WriteLine(("public ") + (tokMgrClassName) + ("(")
                + (str)
                + (" stream){")
                );
        }
        if (Options.getStatic() && !Options.UserCharStream)
        {
            writer.WriteLine("   if (input_stream != null)");
            writer.WriteLine("      throw new TokenMgrError(\"ERROR: Second call to constructor of static lexer. You must use ReInit() to initialize the static variables.\", TokenMgrError.STATIC_LEXER_ERROR);");
        }
        else if (!Options.UserCharStream)
        {
            if (Options.JavaUnicodeEscape)
            {
                writer.WriteLine("   if (JavaCharStream.staticFlag)");
            }
            else
            {
                writer.WriteLine("   if (SimpleCharStream.staticFlag)");
            }
            writer.WriteLine("      throw new System.Exception(\"ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.\");");
        }
        writer.WriteLine("   input_stream = stream;");
        writer.WriteLine("}");
        if (Options.TokenManagerUsesParser && !Options.getStatic())
        {
            writer.WriteLine("");
            writer.WriteLine("/** Constructor with parser. */");
            writer.WriteLine(("public ") + (tokMgrClassName) + ("(")
                + (JavaCCGlobals.Cu_name)
                + (" parserArg, ")
                + (str)
                + (" stream, int lexState){")
                );
            writer.WriteLine("   this(parserArg, stream);");
        }
        else
        {
            writer.WriteLine("");
            writer.WriteLine("/** Constructor. */");
            writer.WriteLine(("public ") + (tokMgrClassName) + ("(")
                + (str)
                + (" stream, int lexState){")
                );
            writer.WriteLine("   this(stream);");
        }
        writer.WriteLine("   SwitchTo(lexState);");
        writer.WriteLine("}");
        writer.WriteLine("");
        writer.WriteLine("/** Reinitialise parser. */");
        writer.WriteLine((staticString) + ("public void ReInit(") + (str)
            + (" stream)")
            );
        writer.WriteLine("{");
        writer.WriteLine("   jjmatchedPos = jjnewStateCnt = 0;");
        writer.WriteLine("   curLexState = defaultLexState;");
        writer.WriteLine("   input_stream = stream;");
        writer.WriteLine("   ReInitRounds();");
        writer.WriteLine("}");
        writer.WriteLine((staticString) + ("private void ReInitRounds()"));
        writer.WriteLine("{");
        writer.WriteLine("   int i;");
        writer.WriteLine(("   jjround = 0x") + (Utils.ToHexString(-2147483647)) + (";")
            );
        writer.WriteLine(("   for (i = ") + (stateSetSize) + ("; i-- > 0;)")
            );
        writer.WriteLine(("      jjrounds[i] = 0x") + (Utils.ToHexString(int.MinValue)) + (";")
            );
        writer.WriteLine("}");
        writer.WriteLine("");
        writer.WriteLine("/** Reinitialise parser. */");
        writer.WriteLine((staticString) + ("public void ReInit(") + (str)
            + (" stream, int lexState)")
            );
        writer.WriteLine("{");
        writer.WriteLine("   ReInit(stream);");
        writer.WriteLine("   SwitchTo(lexState);");
        writer.WriteLine("}");
        writer.WriteLine("");
        writer.WriteLine("/** Switch to specified lex state. */");
        writer.WriteLine((staticString) + ("public void SwitchTo(int lexState)"));
        writer.WriteLine("{");
        writer.WriteLine(("   if (lexState >= ") + (lexStateName.Length) + (" || lexState < 0)")
            );
        writer.WriteLine("      throw new TokenMgrError(\"Error: Ignoring invalid lexical state : \" + lexState + \". State unchanged.\", TokenMgrError.INVALID_LEXICAL_STATE);");
        writer.WriteLine("   else");
        writer.WriteLine("      curLexState = lexState;");
        writer.WriteLine("}");
        writer.WriteLine("");
    }


    internal static void DumpFillToken()
    {
        double num = JavaFiles.GetVersion("Token.java");
        int num2 = ((num > 4.09) ? 1 : 0);
        writer.WriteLine((staticString) + ("protected Token jjFillToken()"));
        writer.WriteLine("{");
        writer.WriteLine("   final Token t;");
        writer.WriteLine("   final String tokenImage;");
        if (keepLineCol)
        {
            writer.WriteLine("   final int beginLine;");
            writer.WriteLine("   final int endLine;");
            writer.WriteLine("   final int beginColumn;");
            writer.WriteLine("   final int endColumn;");
        }
        if (hasEmptyMatch)
        {
            writer.WriteLine("   if (jjmatchedPos < 0)");
            writer.WriteLine("   {");
            writer.WriteLine("      if (image == null)");
            writer.WriteLine("         tokenImage = \"\";");
            writer.WriteLine("      else");
            writer.WriteLine("         tokenImage = image;");
            if (keepLineCol)
            {
                writer.WriteLine("      beginLine = endLine = input_stream.getBeginLine();");
                writer.WriteLine("      beginColumn = endColumn = input_stream.getBeginColumn();");
            }
            writer.WriteLine("   }");
            writer.WriteLine("   else");
            writer.WriteLine("   {");
            writer.WriteLine("      String im = jjstrLiteralImages[jjmatchedKind];");
            writer.WriteLine("      tokenImage = (im == null) ? input_stream.GetImage() : im;");
            if (keepLineCol)
            {
                writer.WriteLine("      beginLine = input_stream.getBeginLine();");
                writer.WriteLine("      beginColumn = input_stream.getBeginColumn();");
                writer.WriteLine("      endLine = input_stream.getEndLine();");
                writer.WriteLine("      endColumn = input_stream.getEndColumn();");
            }
            writer.WriteLine("   }");
        }
        else
        {
            writer.WriteLine("   String im = jjstrLiteralImages[jjmatchedKind];");
            writer.WriteLine("   tokenImage = (im == null) ? input_stream.GetImage() : im;");
            if (keepLineCol)
            {
                writer.WriteLine("   beginLine = input_stream.getBeginLine();");
                writer.WriteLine("   beginColumn = input_stream.getBeginColumn();");
                writer.WriteLine("   endLine = input_stream.getEndLine();");
                writer.WriteLine("   endColumn = input_stream.getEndColumn();");
            }
        }
        if ((Options.TokenFactory.Length) > 0)
        {
            writer.WriteLine(("   t = ") + (Options.TokenFactory) + (".newToken(jjmatchedKind, tokenImage);")
                );
        }
        else if (num2 != 0)
        {
            writer.WriteLine("   t = Token.newToken(jjmatchedKind, tokenImage);");
        }
        else
        {
            writer.WriteLine("   t = Token.newToken(jjmatchedKind);");
            writer.WriteLine("   t.kind = jjmatchedKind;");
            writer.WriteLine("   t.image = tokenImage;");
        }
        if (keepLineCol)
        {
            writer.WriteLine("");
            writer.WriteLine("   t.beginLine = beginLine;");
            writer.WriteLine("   t.endLine = endLine;");
            writer.WriteLine("   t.beginColumn = beginColumn;");
            writer.WriteLine("   t.endColumn = endColumn;");
        }
        writer.WriteLine("");
        writer.WriteLine("   return t;");
        writer.WriteLine("}");
    }


    internal static void DumpGetNextToken()
    {
        writer.WriteLine("");
        writer.WriteLine((staticString) + ("int curLexState = ") + (defaultLexState)
            + (";")
            );
        writer.WriteLine((staticString) + ("int defaultLexState = ") + (defaultLexState)
            + (";")
            );
        writer.WriteLine((staticString) + ("int jjnewStateCnt;"));
        writer.WriteLine((staticString) + ("int jjround;"));
        writer.WriteLine((staticString) + ("int jjmatchedPos;"));
        writer.WriteLine((staticString) + ("int jjmatchedKind;"));
        writer.WriteLine("");
        writer.WriteLine("/** Get the next Token. */");
        writer.WriteLine(("public ") + (staticString) + ("Token getNextToken()")
            + (" ")
            );
        writer.WriteLine("{");
        writer.WriteLine("  //int kind;");
        writer.WriteLine("  Token specialToken = null;");
        writer.WriteLine("  Token matchedToken;");
        writer.WriteLine("  int curPos = 0;");
        writer.WriteLine("");
        writer.WriteLine("  EOFLoop :\n  for (;;)");
        writer.WriteLine("  {   ");
        writer.WriteLine("   try   ");
        writer.WriteLine("   {     ");
        writer.WriteLine("      curChar = input_stream.BeginToken();");
        writer.WriteLine("   }     ");
        writer.WriteLine("   catch(java.io.IOException e)");
        writer.WriteLine("   {        ");
        if (Options.DebugTokenManager)
        {
            writer.WriteLine("      debugStream.WriteLine(\"Returning the <EOF> token.\");");
        }
        writer.WriteLine("      jjmatchedKind = 0;");
        writer.WriteLine("      matchedToken = jjFillToken();");
        if (hasSpecial)
        {
            writer.WriteLine("      matchedToken.specialToken = specialToken;");
        }
        if (JavaCCGlobals.nextStateForEof != null || JavaCCGlobals.actForEof != null)
        {
            writer.WriteLine("      TokenLexicalActions(matchedToken);");
        }
        if (Options.CommonTokenAction)
        {
            writer.WriteLine("      CommonTokenAction(matchedToken);");
        }
        writer.WriteLine("      return matchedToken;");
        writer.WriteLine("   }");
        if (hasMoreActions || hasSkipActions || hasTokenActions)
        {
            writer.WriteLine("   image = null;");
            writer.WriteLine("   jjimageLen = 0;");
        }
        writer.WriteLine("");
        string str = "";
        if (hasMore)
        {
            writer.WriteLine("   for (;;)");
            writer.WriteLine("   {");
            str = "  ";
        }
        string x = "";
        string str2 = "";
        if (maxLexStates > 1)
        {
            writer.WriteLine((str) + ("   switch(curLexState)"));
            writer.WriteLine((str) + ("   {"));
            x = (str) + ("   }");
            str2 = (str) + ("     case ");
            str = (str) + ("    ");
        }
        str = (str) + ("   ");
        for (int i = 0; i < maxLexStates; i++)
        {
            if (maxLexStates > 1)
            {
                writer.WriteLine((str2) + (i) + (":")
                    );
            }
            if (singlesToSkip[i].HasTransitions())
            {
                writer.WriteLine((str) + ("try { input_stream.backup(0);"));
                if (singlesToSkip[i].asciiMoves[0] != 0 && singlesToSkip[i].asciiMoves[1] != 0)
                {
                    writer.WriteLine((str) + ("   while ((curChar < 64") + (" && (0x")
                        + (Utils.ToHexString(singlesToSkip[i].asciiMoves[0]))
                        + ("L & (1L << curChar)) != 0L) || \n")
                        + (str)
                        + ("          (curChar >> 6) == 1")
                        + (" && (0x")
                        + (Utils.ToHexString(singlesToSkip[i].asciiMoves[1]))
                        + ("L & (1L << (curChar & 077))) != 0L)")
                        );
                }
                else if (singlesToSkip[i].asciiMoves[1] == 0)
                {
                    writer.WriteLine((str) + ("   while (curChar <= ") + ((int)MaxChar(singlesToSkip[i].asciiMoves[0]))
                        + (" && (0x")
                        + (Utils.ToHexString(singlesToSkip[i].asciiMoves[0]))
                        + ("L & (1L << curChar)) != 0L)")
                        );
                }
                else if (singlesToSkip[i].asciiMoves[0] == 0)
                {
                    writer.WriteLine((str) + ("   while (curChar > 63 && curChar <= ") + (MaxChar(singlesToSkip[i].asciiMoves[1]) + 64)
                        + (" && (0x")
                        + (Utils.ToHexString(singlesToSkip[i].asciiMoves[1]))
                        + ("L & (1L << (curChar & 077))) != 0L)")
                        );
                }
                if (Options.DebugTokenManager)
                {
                    writer.WriteLine((str) + ("{"));
                    writer.WriteLine(("      debugStream.WriteLine(") + ((maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ") + ("\"Skipping character : \" + ")
                        + ("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \")\");")
                        );
                }
                writer.WriteLine((str) + ("      curChar = input_stream.BeginToken();"));
                if (Options.DebugTokenManager)
                {
                    writer.WriteLine((str) + ("}"));
                }
                writer.WriteLine((str) + ("}"));
                writer.WriteLine((str) + ("catch (java.io.IOException e1) { continue EOFLoop; }"));
            }
            if (initMatch[i] != int.MaxValue && initMatch[i] != 0)
            {
                if (Options.DebugTokenManager)
                {
                    writer.WriteLine(("      debugStream.WriteLine(\"   Matched the empty string as \" + tokenImage[") + (initMatch[i]) + ("] + \" token.\");")
                        );
                }
                writer.WriteLine((str) + ("jjmatchedKind = ") + (initMatch[i])
                    + (";")
                    );
                writer.WriteLine((str) + ("jjmatchedPos = -1;"));
                writer.WriteLine((str) + ("curPos = 0;"));
            }
            else
            {
                writer.WriteLine((str) + ("jjmatchedKind = 0x") + (Utils.ToString(int.MaxValue, 16))
                    + (";")
                    );
                writer.WriteLine((str) + ("jjmatchedPos = 0;"));
            }
            if (Options.DebugTokenManager)
            {
                writer.WriteLine(("      debugStream.WriteLine(") + ((maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ") + ("\"Current character : \" + ")
                    + ("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
                    + ("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
                    );
            }
            writer.WriteLine((str) + ("curPos = jjMoveStringLiteralDfa0_") + (i)
                + ("();")
                );
            if (canMatchAnyChar[i] != -1)
            {
                if (initMatch[i] != int.MaxValue && initMatch[i] != 0)
                {
                    writer.WriteLine((str) + ("if (jjmatchedPos < 0 || (jjmatchedPos == 0 && jjmatchedKind > ") + (canMatchAnyChar[i])
                        + ("))")
                        );
                }
                else
                {
                    writer.WriteLine((str) + ("if (jjmatchedPos == 0 && jjmatchedKind > ") + (canMatchAnyChar[i])
                        + (")")
                        );
                }
                writer.WriteLine((str) + ("{"));
                if (Options.DebugTokenManager)
                {
                    writer.WriteLine(("           debugStream.WriteLine(\"   Current character matched as a \" + tokenImage[") + (canMatchAnyChar[i]) + ("] + \" token.\");")
                        );
                }
                writer.WriteLine((str) + ("   jjmatchedKind = ") + (canMatchAnyChar[i])
                    + (";")
                    );
                if (initMatch[i] != int.MaxValue && initMatch[i] != 0)
                {
                    writer.WriteLine((str) + ("   jjmatchedPos = 0;"));
                }
                writer.WriteLine((str) + ("}"));
            }
            if (maxLexStates > 1)
            {
                writer.WriteLine((str) + ("break;"));
            }
        }
        if (maxLexStates > 1)
        {
            writer.WriteLine(x);
        }
        else if (maxLexStates == 0)
        {
            writer.WriteLine(("       jjmatchedKind = 0x") + (Utils.ToString(int.MaxValue, 16)) + (";")
                );
        }
        str = ((maxLexStates <= 1) ? "" : "  ");
        if (maxLexStates > 0)
        {
            writer.WriteLine((str) + ("   if (jjmatchedKind != 0x") + (Utils.ToString(int.MaxValue, 16))
                + (")")
                );
            writer.WriteLine((str) + ("   {"));
            writer.WriteLine((str) + ("      if (jjmatchedPos + 1 < curPos)"));
            if (Options.DebugTokenManager)
            {
                writer.WriteLine((str) + ("      {"));
                writer.WriteLine((str) + ("         debugStream.WriteLine(") + ("\"   Putting back \" + (curPos - jjmatchedPos - 1) + \" characters into the input stream.\");")
                    );
            }
            writer.WriteLine((str) + ("         input_stream.backup(curPos - jjmatchedPos - 1);"));
            if (Options.DebugTokenManager)
            {
                writer.WriteLine((str) + ("      }"));
            }
            if (Options.DebugTokenManager)
            {
                if (Options.JavaUnicodeEscape || Options.UserCharStream)
                {
                    writer.WriteLine("    debugStream.WriteLine(\"****** FOUND A \" + tokenImage[jjmatchedKind] + \" MATCH (\" + TokenMgrError.addEscapes(new String(input_stream.GetSuffix(jjmatchedPos + 1))) + \") ******\\n\");");
                }
                else
                {
                    writer.WriteLine("    debugStream.WriteLine(\"****** FOUND A \" + tokenImage[jjmatchedKind] + \" MATCH (\" + TokenMgrError.addEscapes(new String(input_stream.GetSuffix(jjmatchedPos + 1))) + \") ******\\n\");");
                }
            }
            if (hasSkip || hasMore || hasSpecial)
            {
                writer.WriteLine((str) + ("      if ((jjtoToken[jjmatchedKind >> 6] & ") + ("(1L << (jjmatchedKind & 077))) != 0L)")
                    );
                writer.WriteLine((str) + ("      {"));
            }
            writer.WriteLine((str) + ("         matchedToken = jjFillToken();"));
            if (hasSpecial)
            {
                writer.WriteLine((str) + ("         matchedToken.specialToken = specialToken;"));
            }
            if (hasTokenActions)
            {
                writer.WriteLine((str) + ("         TokenLexicalActions(matchedToken);"));
            }
            if (maxLexStates > 1)
            {
                writer.WriteLine("       if (jjnewLexState[jjmatchedKind] != -1)");
                writer.WriteLine((str) + ("       curLexState = jjnewLexState[jjmatchedKind];"));
            }
            if (Options.CommonTokenAction)
            {
                writer.WriteLine((str) + ("         CommonTokenAction(matchedToken);"));
            }
            writer.WriteLine((str) + ("         return matchedToken;"));
            if (hasSkip || hasMore || hasSpecial)
            {
                writer.WriteLine((str) + ("      }"));
                if (hasSkip || hasSpecial)
                {
                    if (hasMore)
                    {
                        writer.WriteLine((str) + ("      else if ((jjtoSkip[jjmatchedKind >> 6] & ") + ("(1L << (jjmatchedKind & 077))) != 0L)")
                            );
                    }
                    else
                    {
                        writer.WriteLine((str) + ("      else"));
                    }
                    writer.WriteLine((str) + ("      {"));
                    if (hasSpecial)
                    {
                        writer.WriteLine((str) + ("         if ((jjtoSpecial[jjmatchedKind >> 6] & ") + ("(1L << (jjmatchedKind & 077))) != 0L)")
                            );
                        writer.WriteLine((str) + ("         {"));
                        writer.WriteLine((str) + ("            matchedToken = jjFillToken();"));
                        writer.WriteLine((str) + ("            if (specialToken == null)"));
                        writer.WriteLine((str) + ("               specialToken = matchedToken;"));
                        writer.WriteLine((str) + ("            else"));
                        writer.WriteLine((str) + ("            {"));
                        writer.WriteLine((str) + ("               matchedToken.specialToken = specialToken;"));
                        writer.WriteLine((str) + ("               specialToken = (specialToken.next = matchedToken);"));
                        writer.WriteLine((str) + ("            }"));
                        if (hasSkipActions)
                        {
                            writer.WriteLine((str) + ("            SkipLexicalActions(matchedToken);"));
                        }
                        writer.WriteLine((str) + ("         }"));
                        if (hasSkipActions)
                        {
                            writer.WriteLine((str) + ("         else "));
                            writer.WriteLine((str) + ("            SkipLexicalActions(null);"));
                        }
                    }
                    else if (hasSkipActions)
                    {
                        writer.WriteLine((str) + ("         SkipLexicalActions(null);"));
                    }
                    if (maxLexStates > 1)
                    {
                        writer.WriteLine("         if (jjnewLexState[jjmatchedKind] != -1)");
                        writer.WriteLine((str) + ("         curLexState = jjnewLexState[jjmatchedKind];"));
                    }
                    writer.WriteLine((str) + ("         continue EOFLoop;"));
                    writer.WriteLine((str) + ("      }"));
                }
                if (hasMore)
                {
                    if (hasMoreActions)
                    {
                        writer.WriteLine((str) + ("      MoreLexicalActions();"));
                    }
                    else if (hasSkipActions || hasTokenActions)
                    {
                        writer.WriteLine((str) + ("      jjimageLen += jjmatchedPos + 1;"));
                    }
                    if (maxLexStates > 1)
                    {
                        writer.WriteLine("      if (jjnewLexState[jjmatchedKind] != -1)");
                        writer.WriteLine((str) + ("      curLexState = jjnewLexState[jjmatchedKind];"));
                    }
                    writer.WriteLine((str) + ("      curPos = 0;"));
                    writer.WriteLine((str) + ("      jjmatchedKind = 0x") + (Utils.ToString(int.MaxValue, 16))
                        + (";")
                        );
                    writer.WriteLine((str) + ("      try {"));
                    writer.WriteLine((str) + ("         curChar = input_stream.readChar();"));
                    if (Options.DebugTokenManager)
                    {
                        writer.WriteLine(("   debugStream.WriteLine(") + ((maxLexStates <= 1) ? "" : "\"<\" + lexStateNames[curLexState] + \">\" + ") + ("\"Current character : \" + ")
                            + ("TokenMgrError.addEscapes(String.valueOf(curChar)) + \" (\" + (int)curChar + \") ")
                            + ("at line \" + input_stream.getEndLine() + \" column \" + input_stream.getEndColumn());")
                            );
                    }
                    writer.WriteLine((str) + ("         continue;"));
                    writer.WriteLine((str) + ("      }"));
                    writer.WriteLine((str) + ("      catch (java.io.IOException e1) { }"));
                }
            }
            writer.WriteLine((str) + ("   }"));
            writer.WriteLine((str) + ("   int error_line = input_stream.getEndLine();"));
            writer.WriteLine((str) + ("   int error_column = input_stream.getEndColumn();"));
            writer.WriteLine((str) + ("   String error_after = null;"));
            writer.WriteLine((str) + ("   boolean EOFSeen = false;"));
            writer.WriteLine((str) + ("   try { input_stream.readChar(); input_stream.backup(1); }"));
            writer.WriteLine((str) + ("   catch (java.io.IOException e1) {"));
            writer.WriteLine((str) + ("      EOFSeen = true;"));
            writer.WriteLine((str) + ("      error_after = curPos <= 1 ? \"\" : input_stream.GetImage();"));
            writer.WriteLine((str) + ("      if (curChar == '\\n' || curChar == '\\r') {"));
            writer.WriteLine((str) + ("         error_line++;"));
            writer.WriteLine((str) + ("         error_column = 0;"));
            writer.WriteLine((str) + ("      }"));
            writer.WriteLine((str) + ("      else"));
            writer.WriteLine((str) + ("         error_column++;"));
            writer.WriteLine((str) + ("   }"));
            writer.WriteLine((str) + ("   if (!EOFSeen) {"));
            writer.WriteLine((str) + ("      input_stream.backup(1);"));
            writer.WriteLine((str) + ("      error_after = curPos <= 1 ? \"\" : input_stream.GetImage();"));
            writer.WriteLine((str) + ("   }"));
            writer.WriteLine((str) + ("   throw new TokenMgrError(") + ("EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LEXICAL_ERROR);")
                );
        }
        if (hasMore)
        {
            writer.WriteLine((str) + (" }"));
        }
        writer.WriteLine("  }");
        writer.WriteLine("}");
        writer.WriteLine("");
    }


    internal static void DumpDebugMethods()
    {
        writer.WriteLine(("  ") + (staticString) + (" int kindCnt = 0;")
            );
        writer.WriteLine(("  protected ") + (staticString) + (" final String jjKindsForBitVector(int i, long vec)")
            );
        writer.WriteLine("  {");
        writer.WriteLine("    String retVal = \"\";");
        writer.WriteLine("    if (i == 0)");
        writer.WriteLine("       kindCnt = 0;");
        writer.WriteLine("    for (int j = 0; j < 64; j++)");
        writer.WriteLine("    {");
        writer.WriteLine("       if ((vec & (1L << j)) != 0L)");
        writer.WriteLine("       {");
        writer.WriteLine("          if (kindCnt++ > 0)");
        writer.WriteLine("             retVal += \", \";");
        writer.WriteLine("          if (kindCnt % 5 == 0)");
        writer.WriteLine("             retVal += \"\\n     \";");
        writer.WriteLine("          retVal += tokenImage[i * 64 + j];");
        writer.WriteLine("       }");
        writer.WriteLine("    }");
        writer.WriteLine("    return retVal;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine(("  protected ") + (staticString) + (" final String jjKindsForStateVector(")
            + ("int lexState, int[] vec, int start, int end)")
            );
        writer.WriteLine("  {");
        writer.WriteLine(("    boolean[] kindDone = new boolean[") + (maxOrdinal) + ("];")
            );
        writer.WriteLine("    String retVal = \"\";");
        writer.WriteLine("    int cnt = 0;");
        writer.WriteLine("    for (int i = start; i < end; i++)");
        writer.WriteLine("    {");
        writer.WriteLine("     if (vec[i] == -1)");
        writer.WriteLine("       continue;");
        writer.WriteLine("     int[] stateSet = statesForState[curLexState][vec[i]];");
        writer.WriteLine("     for (int j = 0; j < stateSet.length; j++)");
        writer.WriteLine("     {");
        writer.WriteLine("       int state = stateSet[j];");
        writer.WriteLine("       if (!kindDone[kindForState[lexState][state]])");
        writer.WriteLine("       {");
        writer.WriteLine("          kindDone[kindForState[lexState][state]] = true;");
        writer.WriteLine("          if (cnt++ > 0)");
        writer.WriteLine("             retVal += \", \";");
        writer.WriteLine("          if (cnt % 5 == 0)");
        writer.WriteLine("             retVal += \"\\n     \";");
        writer.WriteLine("          retVal += tokenImage[kindForState[lexState][state]];");
        writer.WriteLine("       }");
        writer.WriteLine("     }");
        writer.WriteLine("    }");
        writer.WriteLine("    if (cnt == 0)");
        writer.WriteLine("       return \"{  }\";");
        writer.WriteLine("    else");
        writer.WriteLine("       return \"{ \" + retVal + \" }\";");
        writer.WriteLine("  }");
        writer.WriteLine("");
    }


    public static void DumpSkipActions()
    {
        writer.WriteLine((staticString) + ("void SkipLexicalActions(Token matchedToken)"));
        writer.WriteLine("{");
        writer.WriteLine("   switch(jjmatchedKind)");
        writer.WriteLine("   {");
        for (int i = 0; i < maxOrdinal; i++)
        {
            long num = toSkip[i / 64];
            long num2 = 1L;
            int num3 = i;
            Action action;
            if ((num & (num2 << ((64 != -1) ? (num3 % 64) : 0))) == 0 || (((action = actions[i]) == null || action.ActionTokens == null || action.ActionTokens.Count == 0) && !canLoop[lexStates[i]]))
            {
                continue;
            }
            writer.WriteLine(("      case ") + (i) + (" :")
                );
            if (initMatch[lexStates[i]] == i && canLoop[lexStates[i]])
            {
                writer.WriteLine("         if (jjmatchedPos == -1)");
                writer.WriteLine("         {");
                writer.WriteLine(("            if (jjbeenHere[") + (lexStates[i]) + ("] &&")
                    );
                writer.WriteLine(("                jjemptyLineNo[") + (lexStates[i]) + ("] == input_stream.getBeginLine() && ")
                    );
                writer.WriteLine(("                jjemptyColNo[") + (lexStates[i]) + ("] == input_stream.getBeginColumn())")
                    );
                writer.WriteLine("               throw new TokenMgrError((\"Error: Bailing out of infinite loop caused by repeated empty string matches at line \" + input_stream.getBeginLine() + \", column \" + input_stream.getBeginColumn() + \".\"), TokenMgrError.LOOP_DETECTED);");
                writer.WriteLine(("            jjemptyLineNo[") + (lexStates[i]) + ("] = input_stream.getBeginLine();")
                    );
                writer.WriteLine(("            jjemptyColNo[") + (lexStates[i]) + ("] = input_stream.getBeginColumn();")
                    );
                writer.WriteLine(("            jjbeenHere[") + (lexStates[i]) + ("] = true;")
                    );
                writer.WriteLine("         }");
            }
            if ((action = actions[i]) != null && action.ActionTokens.Count != 0)
            {
                writer.WriteLine("         if (image == null)");
                writer.WriteLine(("            image = new ") + (Options.StringBufOrBuild) + ("();")
                    );
                writer.Write("         image+");
                if (RStringLiteral.allImages[i] != null)
                {
                    writer.WriteLine(("(jjstrLiteralImages[") + (i) + ("]);")
                        );
                    writer.WriteLine(("        lengthOfMatch = jjstrLiteralImages[") + (i) + ("].length();")
                        );
                }
                else if (Options.JavaUnicodeEscape || Options.UserCharStream)
                {
                    writer.WriteLine("(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));");
                }
                else
                {
                    writer.WriteLine("(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));");
                }
                JavaCCGlobals.PrintTokenSetup((Token)action.ActionTokens[0]);
                JavaCCGlobals.ccol = 1;
                for (int j = 0; j < action.ActionTokens.Count; j++)
                {
                    JavaCCGlobals.PrintToken((Token)action.ActionTokens[j], writer);
                }
                writer.WriteLine("");
            }
            writer.WriteLine("         break;");
        }
        writer.WriteLine("      default :");
        writer.WriteLine("         break;");
        writer.WriteLine("   }");
        writer.WriteLine("}");
    }


    public static void DumpMoreActions()
    {
        writer.WriteLine((staticString) + ("void MoreLexicalActions()"));
        writer.WriteLine("{");
        writer.WriteLine("   jjimageLen += (lengthOfMatch = jjmatchedPos + 1);");
        writer.WriteLine("   switch(jjmatchedKind)");
        writer.WriteLine("   {");
        for (int i = 0; i < maxOrdinal; i++)
        {
            long num = toMore[i / 64];
            long num2 = 1L;
            int num3 = i;
            Action action;
            if ((num & (num2 << ((64 != -1) ? (num3 % 64) : 0))) == 0 || (((action = actions[i]) == null || action.ActionTokens == null || action.ActionTokens.Count == 0) && !canLoop[lexStates[i]]))
            {
                continue;
            }
            writer.WriteLine(("      case ") + (i) + (" :")
                );
            if (initMatch[lexStates[i]] == i && canLoop[lexStates[i]])
            {
                writer.WriteLine("         if (jjmatchedPos == -1)");
                writer.WriteLine("         {");
                writer.WriteLine(("            if (jjbeenHere[") + (lexStates[i]) + ("] &&")
                    );
                writer.WriteLine(("                jjemptyLineNo[") + (lexStates[i]) + ("] == input_stream.getBeginLine() && ")
                    );
                writer.WriteLine(("                jjemptyColNo[") + (lexStates[i]) + ("] == input_stream.getBeginColumn())")
                    );
                writer.WriteLine("               throw new TokenMgrError((\"Error: Bailing out of infinite loop caused by repeated empty string matches at line \" + input_stream.getBeginLine() + \", column \" + input_stream.getBeginColumn() + \".\"), TokenMgrError.LOOP_DETECTED);");
                writer.WriteLine(("            jjemptyLineNo[") + (lexStates[i]) + ("] = input_stream.getBeginLine();")
                    );
                writer.WriteLine(("            jjemptyColNo[") + (lexStates[i]) + ("] = input_stream.getBeginColumn();")
                    );
                writer.WriteLine(("            jjbeenHere[") + (lexStates[i]) + ("] = true;")
                    );
                writer.WriteLine("         }");
            }
            if ((action = actions[i]) != null && action.ActionTokens.Count != 0)
            {
                writer.WriteLine("         if (image == null)");
                writer.WriteLine(("            image = new ") + (Options.StringBufOrBuild) + ("();")
                    );
                writer.Write("         image+");
                if (RStringLiteral.allImages[i] != null)
                {
                    writer.WriteLine(("(jjstrLiteralImages[") + (i) + ("]);")
                        );
                }
                else if (Options.JavaUnicodeEscape || Options.UserCharStream)
                {
                    writer.WriteLine("(input_stream.GetSuffix(jjimageLen));");
                }
                else
                {
                    writer.WriteLine("(input_stream.GetSuffix(jjimageLen));");
                }
                writer.WriteLine("         jjimageLen = 0;");
                JavaCCGlobals.PrintTokenSetup((Token)action.ActionTokens[0]);
                JavaCCGlobals.ccol = 1;
                for (int j = 0; j < action.ActionTokens.Count; j++)
                {
                    JavaCCGlobals.PrintToken((Token)action.ActionTokens[j], writer);
                }
                writer.WriteLine("");
            }
            writer.WriteLine("         break;");
        }
        writer.WriteLine("      default : ");
        writer.WriteLine("         break;");
        writer.WriteLine("   }");
        writer.WriteLine("}");
    }


    public static void DumpTokenActions()
    {
        writer.WriteLine((staticString) + ("void TokenLexicalActions(Token matchedToken)"));
        writer.WriteLine("{");
        writer.WriteLine("   switch(jjmatchedKind)");
        writer.WriteLine("   {");
        for (int i = 0; i < maxOrdinal; i++)
        {
            long num = toToken[i / 64];
            long num2 = 1L;
            int num3 = i;
            Action action;
            if ((num & (num2 << ((64 != -1) ? (num3 % 64) : 0))) == 0 || (((action = actions[i]) == null || action.ActionTokens == null || action.ActionTokens.Count == 0) && !canLoop[lexStates[i]]))
            {
                continue;
            }
            writer.WriteLine(("      case ") + (i) + (" :")
                );
            if (initMatch[lexStates[i]] == i && canLoop[lexStates[i]])
            {
                writer.WriteLine("         if (jjmatchedPos == -1)");
                writer.WriteLine("         {");
                writer.WriteLine(("            if (jjbeenHere[") + (lexStates[i]) + ("] &&")
                    );
                writer.WriteLine(("                jjemptyLineNo[") + (lexStates[i]) + ("] == input_stream.getBeginLine() && ")
                    );
                writer.WriteLine(("                jjemptyColNo[") + (lexStates[i]) + ("] == input_stream.getBeginColumn())")
                    );
                writer.WriteLine("               throw new TokenMgrError((\"Error: Bailing out of infinite loop caused by repeated empty string matches at line \" + input_stream.getBeginLine() + \", column \" + input_stream.getBeginColumn() + \".\"), TokenMgrError.LOOP_DETECTED);");
                writer.WriteLine(("            jjemptyLineNo[") + (lexStates[i]) + ("] = input_stream.getBeginLine();")
                    );
                writer.WriteLine(("            jjemptyColNo[") + (lexStates[i]) + ("] = input_stream.getBeginColumn();")
                    );
                writer.WriteLine(("            jjbeenHere[") + (lexStates[i]) + ("] = true;")
                    );
                writer.WriteLine("         }");
            }
            if ((action = actions[i]) != null && action.ActionTokens.Count != 0)
            {
                if (i == 0)
                {
                    writer.WriteLine("      image = null;");
                }
                else
                {
                    writer.WriteLine("        if (image == null)");
                    writer.WriteLine(("            image = new ") + (Options.StringBufOrBuild) + ("();")
                        );
                    writer.Write("        image+");
                    if (RStringLiteral.allImages[i] != null)
                    {
                        writer.WriteLine(("(jjstrLiteralImages[") + (i) + ("]);")
                            );
                        writer.WriteLine(("        lengthOfMatch = jjstrLiteralImages[") + (i) + ("].length();")
                            );
                    }
                    else if (Options.JavaUnicodeEscape || Options.UserCharStream)
                    {
                        writer.WriteLine("(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));");
                    }
                    else
                    {
                        writer.WriteLine("(input_stream.GetSuffix(jjimageLen + (lengthOfMatch = jjmatchedPos + 1)));");
                    }
                }
                JavaCCGlobals.PrintTokenSetup((Token)action.ActionTokens[0]);
                JavaCCGlobals.ccol = 1;
                for (int j = 0; j < action.ActionTokens.Count; j++)
                {
                    JavaCCGlobals.PrintToken((Token)action.ActionTokens[j], writer);
                }
                writer.WriteLine("");
            }
            writer.WriteLine("         break;");
        }
        writer.WriteLine("      default : ");
        writer.WriteLine("         break;");
        writer.WriteLine("   }");
        writer.WriteLine("}");
    }

    internal static char MaxChar(long P_0)
    {
        int num = 64;
        while (num-- > 0)
        {
            if ((P_0 & (1L << num)) != 0)
            {
                return (char)num;
            }
        }
        return '\uffff';
    }


    public LexGen()
    {
    }


    public static void AddCharToSkip(char ch, int i)
    {
        singlesToSkip[lexStateIndex].AddChar(ch);
        singlesToSkip[lexStateIndex].kind = i;
    }


    public static void Start()
    {
        if (!Options.BuildTokenManager || Options.UserTokenManager || JavaCCErrors._Error_Count > 0)
        {
            return;
        }
        keepLineCol = Options.KeepLineColumn;
        List<RegularExpression> vector = new ();
        staticString = ((!Options.getStatic()) ? "" : "static ");
        tokMgrClassName = (JavaCCGlobals.Cu_name) + ("TokenManager");
        PrintClassHead();
        BuildLexStatesTable();
        foreach (var pair in allTpsForState)
        {
            NfaState.ReInit();
            RStringLiteral.ReInit();
            string text = pair.Key;// (string)enumeration.nextElement();
            lexStateIndex = GetIndex(text);
            lexStateSuffix = ("_") + (lexStateIndex);
            var vector2 = pair.Value;
            initStates.Add(text, initialState = new NfaState());
            int num = 0;
            singlesToSkip[lexStateIndex] = new NfaState();
            singlesToSkip[lexStateIndex].dummy = true;
            if (string.Equals(text, "DEFAULT"))
            {
                defaultLexState = lexStateIndex;
            }
            int num3;
            for (int i = 0; i < vector2.Count; i++)
            {
                TokenProduction tokenProduction = (TokenProduction)vector2[i];
                int kind = tokenProduction.Kind;
                int num2 = (tokenProduction.ignoreCase ? 1 : 0);
                var respecs = tokenProduction.Respecs;
                if (i == 0)
                {
                    num = num2;
                }
                for (int j = 0; j < respecs.Count; j++)
                {
                    RegExprSpec regExprSpec = (RegExprSpec)respecs[j];
                    curRE = regExprSpec.rexp;
                    rexprs[curKind = curRE.ordinal] = curRE;
                    lexStates[curRE.ordinal] = lexStateIndex;
                    ignoreCase[curRE.ordinal] = (byte)num2 != 0;
                    if (curRE.private_rexp)
                    {
                        kinds[curRE.ordinal] = -1;
                        continue;
                    }
                    if (curRE is RStringLiteral && !string.Equals(((RStringLiteral)curRE).image, ""))
                    {
                        ((RStringLiteral)curRE).GenerateDfa(writer, curRE.ordinal);
                        if (i != 0 && !mixed[lexStateIndex] && num != num2)
                        {
                            mixed[lexStateIndex] = true;
                        }
                    }
                    else if (curRE.CanMatchAnyChar())
                    {
                        if (canMatchAnyChar[lexStateIndex] == -1 || canMatchAnyChar[lexStateIndex] > curRE.ordinal)
                        {
                            canMatchAnyChar[lexStateIndex] = curRE.ordinal;
                        }
                    }
                    else
                    {
                        if (curRE is RChoice)
                        {
                            vector.Add(curRE);
                        }
                        Nfa nfa = curRE.GenerateNfa((byte)num2 != 0);
                        nfa.End.isFinal = true;
                        nfa.End.kind = curRE.ordinal;
                        initialState.AddMove(nfa.Start);
                    }
                    if (kinds.Length < curRE.ordinal)
                    {
                        int[] dest = new int[curRE.ordinal + 1];
                        Array.Copy(kinds, 0, dest, 0, kinds.Length);
                        kinds = dest;
                    }
                    kinds[curRE.ordinal] = kind;
                    if (regExprSpec.nextState != null && !string.Equals(regExprSpec.nextState, lexStateName[lexStateIndex]))
                    {
                        newLexState[curRE.ordinal] = regExprSpec.nextState;
                    }
                    if (regExprSpec.act != null && regExprSpec.act.ActionTokens != null && regExprSpec.act.ActionTokens.Count > 0)
                    {
                        actions[curRE.ordinal] = regExprSpec.act;
                    }
                    switch (kind)
                    {
                        case 3:
                            {
                                hasSkipActions |= (byte)((actions[curRE.ordinal] != null || newLexState[curRE.ordinal] != null) ? 1 : 0) != 0;
                                hasSpecial = true;
                                long[] array4 = toSpecial;
                                num3 = curRE.ordinal / 64;
                                long[] array2 = array4;
                                long[] array5 = array2;
                                int num7 = num3;
                                long num8 = array2[num3];
                                long num9 = 1L;
                                int ordinal2 = curRE.ordinal;
                                array5[num7] = num8 | (num9 << ((64 != -1) ? (ordinal2 % 64) : 0));
                                long[] array6 = toSkip;
                                num3 = curRE.ordinal / 64;
                                array2 = array6;
                                long[] array7 = array2;
                                int num10 = num3;
                                long num11 = array2[num3];
                                long num12 = 1L;
                                int ordinal3 = curRE.ordinal;
                                array7[num10] = num11 | (num12 << ((64 != -1) ? (ordinal3 % 64) : 0));
                                break;
                            }
                        case 1:
                            {
                                hasSkipActions |= actions[curRE.ordinal] != null;
                                hasSkip = true;
                                long[] array10 = toSkip;
                                num3 = curRE.ordinal / 64;
                                long[] array2 = array10;
                                long[] array11 = array2;
                                int num16 = num3;
                                long num17 = array2[num3];
                                long num18 = 1L;
                                int ordinal5 = curRE.ordinal;
                                array11[num16] = num17 | (num18 << ((64 != -1) ? (ordinal5 % 64) : 0));
                                break;
                            }
                        case 2:
                            {
                                hasMoreActions |= actions[curRE.ordinal] != null;
                                hasMore = true;
                                long[] array8 = toMore;
                                num3 = curRE.ordinal / 64;
                                long[] array2 = array8;
                                long[] array9 = array2;
                                int num13 = num3;
                                long num14 = array2[num3];
                                long num15 = 1L;
                                int ordinal4 = curRE.ordinal;
                                array9[num13] = num14 | (num15 << ((64 != -1) ? (ordinal4 % 64) : 0));
                                if (newLexState[curRE.ordinal] != null)
                                {
                                    canReachOnMore[GetIndex(newLexState[curRE.ordinal])] = true;
                                }
                                else
                                {
                                    canReachOnMore[lexStateIndex] = true;
                                }
                                break;
                            }
                        case 0:
                            {
                                hasTokenActions |= actions[curRE.ordinal] != null;
                                long[] array = toToken;
                                num3 = curRE.ordinal / 64;
                                long[] array2 = array;
                                long[] array3 = array2;
                                int num4 = num3;
                                long num5 = array2[num3];
                                long num6 = 1L;
                                int ordinal = curRE.ordinal;
                                array3[num4] = num5 | (num6 << ((64 != -1) ? (ordinal % 64) : 0));
                                break;
                            }
                    }
                }
            }
            NfaState.ComputeClosures();
            for (int i = 0; i < initialState.epsilonMoves.Count; i++)
            {
                ((NfaState)initialState.epsilonMoves[i]).GenerateCode();
            }
            bool[] array12 = hasNfa;
            int num19 = lexStateIndex;
            num3 = ((NfaState.generatedStates != 0) ? 1 : 0);
            int num20 = num19;
            bool[] array13 = array12;
            int num21 = num3;
            array13[num20] = (byte)num3 != 0;
            if (num21 != 0)
            {
                initialState.GenerateCode();
                initialState.GenerateInitMoves(writer);
            }
            if (initialState.kind != int.MaxValue && initialState.kind != 0)
            {
                if ((toSkip[initialState.kind / 64] & (1L << initialState.kind)) != 0 || (toSpecial[initialState.kind / 64] & (1L << initialState.kind)) != 0)
                {
                    hasSkipActions = true;
                }
                else if ((toMore[initialState.kind / 64] & (1L << initialState.kind)) != 0)
                {
                    hasMoreActions = true;
                }
                else
                {
                    hasTokenActions = true;
                }
                if (initMatch[lexStateIndex] == 0 || initMatch[lexStateIndex] > initialState.kind)
                {
                    initMatch[lexStateIndex] = initialState.kind;
                    hasEmptyMatch = true;
                }
            }
            else if (initMatch[lexStateIndex] == 0)
            {
                initMatch[lexStateIndex] = int.MaxValue;
            }
            RStringLiteral.FillSubString();
            if (hasNfa[lexStateIndex] && !mixed[lexStateIndex])
            {
                RStringLiteral.GenerateNfaStartStates(writer, initialState);
            }
            RStringLiteral.DumpDfaCode(writer);
            if (hasNfa[lexStateIndex])
            {
                NfaState.DumpMoveNfa(writer);
            }
            if (stateSetSize < NfaState.generatedStates)
            {
                stateSetSize = NfaState.generatedStates;
            }
        }
        for (int i = 0; i < vector.Count; i++)
        {
            ((RChoice)vector[i]).CheckUnmatchability();
        }
        NfaState.DumpStateSets(writer);
        CheckEmptyStringMatch();
        NfaState.DumpNonAsciiMoveMethods(writer);
        RStringLiteral.DumpStrLiteralImages(writer);
        DumpStaticVarDeclarations();
        DumpFillToken();
        DumpGetNextToken();
        if (Options.DebugTokenManager)
        {
            NfaState.DumpStatesForKind(writer);
            DumpDebugMethods();
        }
        if (hasLoop)
        {
            writer.WriteLine((staticString) + ("int[] jjemptyLineNo = new int[") + (maxLexStates)
                + ("];")
                );
            writer.WriteLine((staticString) + ("int[] jjemptyColNo = new int[") + (maxLexStates)
                + ("];")
                );
            writer.WriteLine((staticString) + ("boolean[] jjbeenHere = new boolean[") + (maxLexStates)
                + ("];")
                );
        }
        if (hasSkipActions)
        {
            DumpSkipActions();
        }
        if (hasMoreActions)
        {
            DumpMoreActions();
        }
        if (hasTokenActions)
        {
            DumpTokenActions();
        }
        NfaState.PrintBoilerPlate(writer);
        writer.WriteLine("}");
        writer.Close();
    }


    internal static void PrintArrayInitializer(int P_0)
    {
        writer.Write("{");
        for (int i = 0; i < P_0; i++)
        {
            int num = i;
            if (25 == -1 || num % 25 == 0)
            {
                writer.Write("\n   ");
            }
            writer.Write("0, ");
        }
        writer.WriteLine("\n};");
    }


    public static void reInit()
    {
        writer = null;
        staticString = null;
        tokMgrClassName = null;
        allTpsForState = new();
        lexStateIndex = 0;
        kinds = null;
        maxOrdinal = 1;
        lexStateSuffix = null;
        newLexState = null;
        lexStates = null;
        ignoreCase = null;
        actions = null;
        initStates = new();
        stateSetSize = 0;
        maxLexStates = 0;
        lexStateName = null;
        singlesToSkip = null;
        toSkip = null;
        toSpecial = null;
        toMore = null;
        toToken = null;
        defaultLexState = 0;
        rexprs = null;
        maxLongsReqd = null;
        initMatch = null;
        canMatchAnyChar = null;
        hasEmptyMatch = false;
        canLoop = null;
        stateHasActions = null;
        hasLoop = false;
        canReachOnMore = null;
        hasNfa = null;
        mixed = null;
        initialState = null;
        curKind = 0;
        hasSkipActions = false;
        hasMoreActions = false;
        hasTokenActions = false;
        hasSpecial = false;
        hasSkip = false;
        hasMore = false;
        curRE = null;
    }

    static LexGen()
    {

        allTpsForState = new();
        lexStateIndex = 0;
        maxOrdinal = 1;
        initStates = new();
        hasLoop = false;
        hasSkipActions = false;
        hasMoreActions = false;
        hasTokenActions = false;
        hasSpecial = false;
        hasSkip = false;
        hasMore = false;
    }
}
