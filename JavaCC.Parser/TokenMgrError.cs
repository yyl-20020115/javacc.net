using JavaCC.NET;
using System.Text;
namespace JavaCC.Parser;


public class TokenMgrError : System.Exception
{
    internal const int LEXICAL_ERROR = 0;

    internal const int STATIC_LEXER_ERROR = 1;

    internal const int INVALID_LEXICAL_STATE = 2;

    internal const int LOOP_DETECTED = 3;

    internal int errorCode;

    public TokenMgrError(string str, int i)
        : base(str)
    {
        errorCode = i;
    }


    public TokenMgrError(bool b, int i1, int i2, int i3, string str, char ch, int i4)
        : this(LexicalError(b, i1, i2, i3, str, ch), i4)
    {
    }


    protected internal static string addEscapes(string str)
    {
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            switch (str[i])
            {
                case '\b':
                    stringBuilder.Append("\\b");
                    continue;
                case '\t':
                    stringBuilder.Append("\\t");
                    continue;
                case '\n':
                    stringBuilder.Append("\\n");
                    continue;
                case '\f':
                    stringBuilder.Append("\\f");
                    continue;
                case '\r':
                    stringBuilder.Append("\\r");
                    continue;
                case '"':
                    stringBuilder.Append("\\\"");
                    continue;
                case '\'':
                    stringBuilder.Append("\\'");
                    continue;
                case '\\':
                    stringBuilder.Append("\\\\");
                    continue;
                case '\0':
                    continue;
            }
            int num;
            if ((num = str[i]) < 32 || num > 126)
            {
                string @this = ("0000")+(Utils.ToString(num, 16));
                stringBuilder.Append(("\\u")+(
                    @this.Substring(@this.Length - 4, @this.Length)));
            }
            else
            {
                stringBuilder.Append((char)num);
            }
        }
        return stringBuilder.ToString();
    }


    protected internal static string LexicalError(bool b, int i1, int i2, int i3, string str, char ch)
    {
        return ("Lexical error at line ")+(i2)+(", column ")
            +(i3)
            +(".  Encountered: ")
            +((!b) ? ("\"")+(
                addEscapes(
                ch.ToString()))+("\"")
                +(" (")
                +((int)ch)
                +("), ")
                 : "<EOF> ")
            +("after : \"")
            +(addEscapes(str))
            +("\"")
            ;
    }

    public TokenMgrError()
    {
    }
}
