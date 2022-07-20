namespace JavaCC.JJTree;
using System.Text;
using JavaCC.NET;

public class TokenMgrError : System.Exception
{
    public const int LEXICAL_ERROR = 0;

    public const int STATIC_LEXER_ERROR = 1;

    public const int INVALID_LEXICAL_STATE = 2;

    public const int LOOP_DETECTED = 3;

    public readonly int ErrorCode = 1;


    public TokenMgrError(string str, int i)
        : base(str) { }

    public TokenMgrError(bool b, int i1, int i2, int i3, string str, char ch, int i4)
        : this(LexicalError(b, i1, i2, i3, str, ch), i4) { }

    protected internal static string AddEscapes(string str)
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
            int num;
            if ((num = str[i]) < 32 || num > 126)
            {
                string @this = ("0000") + (Utils.ToString(num, 16));
                builder.Append(("\\u") + ((@this.Substring(@this.Length - 4, @this.Length))));
            }
            else
            {
                builder.Append((char)num);
            }
        }
        return builder.ToString();
    }


    protected internal static string LexicalError(bool b, int i1, int i2, int i3, string str, char ch) => "Lexical error at line " + (i2) + (", column ")
            + (i3)
            + (".  Encountered: ")
            + ((!b) ? ("\"") + (AddEscapes((ch.ToString()))) + ("\"")
                + (" (")
                + ((int)ch)
                + ("), ")
                : "<EOF> ")
            + ("after : \"")
            + (AddEscapes(str))
            + ("\"")
            ;


    public override string Message => base.Message;

    public TokenMgrError() { }
}
