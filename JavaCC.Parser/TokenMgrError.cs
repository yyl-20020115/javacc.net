namespace JavaCC.Parser;
using JavaCC.NET;
using System;
using System.Text;

public class TokenMgrError : Exception
{
    public const int LEXICAL_ERROR = 0;

    public const int STATIC_LEXER_ERROR = 1;

    public const int INVALID_LEXICAL_STATE = 2;

    public const int LOOP_DETECTED = 3;

    public readonly int ErrorCode;

    public TokenMgrError(string str, int i)
        : base(str)
    {
        ErrorCode = i;
    }


    public TokenMgrError(bool b, int i1, int i2, int i3, string str, char ch, int i4)
        : this(LexicalError(b, i1, i2, i3, str, ch), i4)
    {
    }


    protected internal static string addEscapes(string str)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            switch (str[i])
            {
                case '\b':
                    builder.Append("\\b");
                    continue;
                case '\t':
                    builder.Append("\\t");
                    continue;
                case '\n':
                    builder.Append("\\n");
                    continue;
                case '\f':
                    builder.Append("\\f");
                    continue;
                case '\r':
                    builder.Append("\\r");
                    continue;
                case '"':
                    builder.Append("\\\"");
                    continue;
                case '\'':
                    builder.Append("\\'");
                    continue;
                case '\\':
                    builder.Append("\\\\");
                    continue;
                case '\0':
                    continue;
            }
            int ch;
            if ((ch = str[i]) < 32 || ch > 126)
            {
                var @this = ("0000") + (Utils.ToString(ch, 16));
                builder.Append(("\\u") + (
                    @this.Substring(@this.Length - 4, @this.Length)));
            }
            else
            {
                builder.Append((char)ch);
            }
        }
        return builder.ToString();
    }


    protected internal static string LexicalError(bool b, int i1, int i2, int i3, string str, char ch) => "Lexical error at line " + i2 + ", column "
            + (i3)
            + (".  Encountered: ")
            + ((!b) ? ("\"") + (
                addEscapes(
                ch.ToString())) + ("\"")
                + (" (")
                + ((int)ch)
                + ("), ")
                 : "<EOF> ")
            + ("after : \"")
            + (addEscapes(str))
            + ("\"")
            ;

    public TokenMgrError()
    {
    }
}
