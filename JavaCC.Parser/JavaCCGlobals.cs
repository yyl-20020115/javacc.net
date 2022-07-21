namespace JavaCC.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using JavaCC.NET;

public class JavaCCGlobals
{
    protected internal const string ToolName = "JavaCC";

    public static string FileName = "";

    public static string OrigFileName = "";

    public static bool JJTreeGenerated = false;

    public static readonly List<string> ToolNames = new();

    public static string CuName = "";

    public static readonly List<Token> CuToInsertionPoint1 = new();

    public static readonly List<Token> Cu_to_insertion_point_2 = new();

    public static readonly List<Token> Cu_from_insertion_point_2 = new();

    public static readonly List<NormalProduction> BNFProductions = new();

    public static readonly Dictionary<string, NormalProduction> Production_table = new();

    public static readonly Dictionary<string, int> Lexstate_S2I = new();

    public static readonly Dictionary<int, string> Lexstate_I2S = new();

    public static readonly List<Token> token_mgr_decls = new();

    public static readonly List<TokenProduction> RexprList = new();

    public static int TokenCount = 0;

    public static readonly Dictionary<string, RegularExpression> NamedTokensTable = new();

    public static readonly List<RegularExpression> ordered_named_tokens = new();

    public static readonly Dictionary<int, string> NamesOfTokens = new();

    public static readonly Dictionary<int,RegularExpression> RexpsOfTokens = new();

    public static readonly Dictionary<string, Dictionary<string, Dictionary<string, RegularExpression>>> SimpleTokensTable = new();

    protected internal static int maskindex = 0;

    protected internal static int jj2index = 0;

    protected internal readonly static List<int[]> MaskVals = new();

    internal static Action actForEof = new();

    internal static string nextStateForEof = "";

    protected internal static int CLine = 0;

    protected internal static int CCol = 0;


    public static string GetIdStringList(List<string> l, string str)
    {
        string str2 = "Generated By:";
        int i;
        for (i = 0; i < l.Count - 1; i++)
        {
            str2 = (str2) + ((string)l[i]) + ("&")
                ;
        }
        str2 = (str2) + ((string)l[i]) + (":")
            ;
        if (str2.Length > 200)
        {
            Console.WriteLine("Tool names too long.");

            throw new System.Exception();
        }
        return (str2) + (" Do not edit this line. ") + (AddUnicodeEscapes(str));
    }


    public static string AddUnicodeEscapes(string str)
    {
        string text = "";
        for (int i = 0; i < str.Length; i++)
        {
            int num = str[i];
            if (num < 32 || num > 126)
            {
                string @this = ("0000") + (Utils.ToString(num, 16));
                text = (text) + ("\\u") + (@this.Substring(@this.Length - 4, @this.Length))
                    ;
            }
            else
            {
                text = (text) + ((char)num);
            }
        }
        return text;
    }


    public static List<string> GetToolNames(string str)
    {
        char[] array = new char[256];
        StreamReader fileReader = null;
        int num = 0;
        List<string> result;
        try
        {
            try
            {
                try
                {
                    fileReader = new StreamReader(str);
                    int num2;
                    while ((num2 = fileReader.Read(array, num, (int)(array.Length - num))) != -1 && (num += num2) != array.Length)
                    {
                    }
                    result = MakeToolNameVector(new string(array, 0, num));
                }
                catch (FileNotFoundException)
                {
                    goto IL_006d;
                }
            }
            catch (IOException)
            {
                goto IL_0070;
            }
        }
        catch
        {
            //try-fault
            if (fileReader != null)
            {
                try
                {
                    fileReader.Close();
                }
                catch
                {
                    goto IL_0064;
                }
            }
            goto end_IL_0049;
        IL_0064:

        end_IL_0049:
            throw;
        }
        if (fileReader != null)
        {
            try
            {
                fileReader.Close();
                return result;
            }
            catch (System.Exception)
            {
            }

        }
        return result;
    IL_0070:
        //IOException ex3 = null;
        List<string> result2;
        try
        {
            if (num > 0)
            {
                result2 = MakeToolNameVector(new string(array, 0, num));
                goto IL_010b;
            }
        }
        catch
        {
            //try-fault
            if (fileReader != null)
            {
                try
                {
                    fileReader.Close();
                }
                catch (System.Exception)
                {
                    goto IL_0100;
                }
            }
            goto end_IL_00e5;
        IL_0100:

        end_IL_00e5:
            throw;
        }
        if (fileReader != null)
        {
            try
            {
                fileReader.Close();
            }
            catch
            {
                goto IL_014a;
            }
        }
        goto IL_0152;
    IL_00b9:

        goto IL_0152;
    IL_0152:
        return new();
    IL_014a:

        goto IL_0152;
    IL_010b:
        if (fileReader != null)
        {
            try
            {
                fileReader.Close();
                return result2;
            }
            catch
            {
            }

        }
        return result2;
    IL_006d:

        if (fileReader != null)
        {
            try
            {
                fileReader.Close();
            }
            catch
            {
                goto IL_00b9;
            }
        }
        goto IL_0152;
    }


    private static List<string> MakeToolNameVector(string name)
    {
        List<string> vector = new();
        int num = name.IndexOf((char)10);
        if (num == -1)
        {
            num = 1000;
        }
        int num2 = name.IndexOf((char)13);
        if (num2 == -1)
        {
            num2 = 1000;
        }
        int num3 = ((num >= num2) ? num2 : num);
        string @this = ((num3 != 1000) ? name.Substring(0, num3) : name);
        if (@this.IndexOf((char)58) == -1)
        {
            return vector;
        }
        @this = @this.Substring(@this.IndexOf((char)58) + 1);
        if (@this.IndexOf((char)58) == -1)
        {
            return vector;
        }
        @this = @this.Substring(0, (@this.IndexOf((char)58)));
        
        int num4 = 0;
        int num5;
        while (num4 < @this.Length && ((num5 = @this.IndexOf((char)38, num4)) != -1))
        {
            vector.Add(@this.Substring(num4, num5));
            num4 = num5 + 1;
        }
        if (num4 < @this.Length)
        {
            vector.Add(@this.Substring(num4));
        }
        return vector;
    }


    protected internal static void PrintTokenOnly(Token t, TextWriter pw)
    {
        while (CLine < t.BeginLine)
        {
            pw.WriteLine("");
            CCol = 1;
            CLine++;
        }
        while (CCol < t.BeginColumn)
        {
            pw.Write(" ");
            CCol++;
        }
        if (t.Kind == 90 || t.Kind == 89)
        {
            pw.Write(AddUnicodeEscapes(t.Image));
        }
        else
        {
            pw.Write(t.Image);
        }
        CLine = t.EndLine;
        CCol = t.EndColumn + 1;
        int num = t.Image[(t.Image.Length) - 1];
        if (num == 10 || num == 13)
        {
            CLine++;
            CCol = 1;
        }
    }


    protected internal static string PrintLeadingComments(Token t)
    {
        string text = "";
        if (t.SpecialToken == null)
        {
            return text;
        }
        Token token = t.SpecialToken;
        while (token.SpecialToken != null)
        {
            token = token.SpecialToken;
        }
        while (token != null)
        {
            text = (text) + (PrintTokenOnly(token));
            token = token.Next;
        }
        if (CCol != 1 && CLine != t.BeginLine)
        {
            text = (text) + ("\n");
            CLine++;
            CCol = 1;
        }
        return text;
    }


    protected internal static string PrintTokenOnly(Token t)
    {
        string str = "";
        while (CLine < t.BeginLine)
        {
            str = (str) + ("\n");
            CCol = 1;
            CLine++;
        }
        while (CCol < t.BeginColumn)
        {
            str = (str) + (" ");
            CCol++;
        }
        str = ((t.Kind != 90 && t.Kind != 89)
            ? (str) + (t.Image)
            : (str) + (AddUnicodeEscapes(t.Image)));
        CLine = t.EndLine;
        CCol = t.EndColumn + 1;
        int num = t.Image[(t.Image.Length) - 1];
        if (num == 10 || num == 13)
        {
            CLine++;
            CCol = 1;
        }
        return str;
    }


    public JavaCCGlobals()
    {
    }


    public static void BannerLine(string str1, string str2)
    {
        Console.Write(("Java Compiler Compiler Version 4.1d1 (") + (str1));
        if (!string.Equals(str2, ""))
        {
            Console.Write((" Version ") + (str2));
        }
        Console.WriteLine(")");
    }


    public static string getIdString(string str1, string str2)
    {
        var vector = new List<string>();
        vector.Add(str1);
        string idString = GetIdStringList(vector, str2);

        return idString;
    }


    public static bool IsGeneratedBy(string str1, string str2)
    {
        var vector = GetToolNames(str2);
        for (int i = 0; i < vector.Count; i++)
        {
            if (string.Equals(str1, vector[i]))
            {
                return true;
            }
        }
        return false;
    }


    public static void CreateOutputDir(FileInfo f)
    {
        if (!f.Exists)
        {
            JavaCCErrors.Warning(("Output directory \"") + (f) + ("\" does not exist. Creating the directory.")
                );
            var d = Directory.CreateDirectory(f.FullName);
            if (!d.Exists)
            {
                JavaCCErrors.Semantic_Error(("Cannot create the output directory : ") + (f));
                return;
            }
        }
        if (!new DirectoryInfo(f.FullName).Exists)
        {
            JavaCCErrors.Semantic_Error(("\"") + (f) + (" is not a valid output directory.")
                );
        }
        else if (f.IsReadOnly)
        {
            JavaCCErrors.Semantic_Error(("Cannot write to the output output directory : \"") + (f) + ("\"")
                );
        }
    }


    public static string StaticOpt()
    {
        if (Options.Static)
        {
            return "static ";
        }
        return "";
    }


    public static string AddEscapes(string str)
    {
        string text = "";
        for (int i = 0; i < str.Length; i++)
        {
            int num = str[i];
            if (num == 8)
            {
                text = (text) + ("\\b");
                continue;
            }
            if (num == 9)
            {
                text = (text) + ("\\t");
                continue;
            }
            if (num == 10)
            {
                text = (text) + ("\\n");
                continue;
            }
            if (num == 12)
            {
                text = (text) + ("\\f");
                continue;
            }
            if (num == 13)
            {
                text = (text) + ("\\r");
                continue;
            }
            if (num == 34)
            {
                text = (text) + ("\\\"");
                continue;
            }
            if (num == 39)
            {
                text = (text) + ("\\'");
                continue;
            }
            switch (num)
            {
                case 92:
                    text = (text) + ("\\\\");
                    break;
                default:
                    {
                        string @this = ("0000") + (Utils.ToString(num, 16));
                        text = (text) + ("\\u") + (@this.Substring(@this.Length - 4, @this.Length))
                            ;
                        break;
                    }
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 91:
                case 93:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                    text = (text) + ((char)num);
                    break;
            }
        }
        return text;
    }

    protected internal static void PrintTokenSetup(Token t)
    {
        Token token = t;
        while (token.SpecialToken != null)
        {
            token = token.SpecialToken;
        }
        CLine = token.BeginLine;
        CCol = token.BeginColumn;
    }


    protected internal static void PrintToken(Token t, TextWriter pw)
    {
        Token token = t.SpecialToken;
        if (token != null)
        {
            while (token.SpecialToken != null)
            {
                token = token.SpecialToken;
            }
            while (token != null)
            {
                PrintTokenOnly(token, pw);
                token = token.Next;
            }
        }
        PrintTokenOnly(t, pw);
    }


    protected internal static void PrintLeadingComments(Token t, TextWriter pw)
    {
        if (t.SpecialToken != null)
        {
            Token token = t.SpecialToken;
            while (token.SpecialToken != null)
            {
                token = token.SpecialToken;
            }
            while (token != null)
            {
                PrintTokenOnly(token, pw);
                token = token.Next;
            }
            if (CCol != 1 && CLine != t.BeginLine)
            {
                pw.WriteLine("");
                CLine++;
                CCol = 1;
            }
        }
    }


    protected internal static void PrintTrailingComments(Token t, TextWriter pw)
    {
        if (t.Next != null)
        {
            PrintLeadingComments(t.Next);
        }
    }


    protected internal static string PrintToken(Token t)
    {
        string str = "";
        Token token = t.SpecialToken;
        if (token != null)
        {
            while (token.SpecialToken != null)
            {
                token = token.SpecialToken;
            }
            while (token != null)
            {
                str = (str) + (PrintTokenOnly(token));
                token = token.Next;
            }
        }
        return (str) + (PrintTokenOnly(t));
    }


    protected internal static string PrintTrailingComments(Token t) => t.Next == null ? "" : PrintLeadingComments(t.Next);


    public static void ReInit()
    {
        FileName = null;
        OrigFileName = null;
        JJTreeGenerated = false;
        ToolNames.Clear();
        CuName = null;
        CuToInsertionPoint1.Clear();
        Cu_to_insertion_point_2.Clear();
        Cu_from_insertion_point_2.Clear();
        BNFProductions.Clear();
        Production_table.Clear();
        Lexstate_S2I.Clear();
        Lexstate_I2S.Clear();
        token_mgr_decls.Clear();
        RexprList.Clear();
        TokenCount = 0;
        NamedTokensTable.Clear();
        ordered_named_tokens.Clear();
        NamesOfTokens.Clear();
        RexpsOfTokens.Clear();
        SimpleTokensTable.Clear();
        maskindex = 0;
        jj2index = 0;
        MaskVals.Clear();
        CLine = 0;
        CCol = 0;
        actForEof = null;
        nextStateForEof = null;
    }

    static JavaCCGlobals()
    {
        CuToInsertionPoint1 = new();
        Cu_to_insertion_point_2 = new();
        Cu_from_insertion_point_2 = new();
        BNFProductions = new();
        Production_table = new();
        Lexstate_S2I = new();
        Lexstate_I2S = new();
        RexprList = new();
        NamedTokensTable = new();
        ordered_named_tokens = new();
        NamesOfTokens = new();
        RexpsOfTokens = new();
        SimpleTokensTable = new();
        maskindex = 0;
        jj2index = 0;
        MaskVals = new();
    }
}
