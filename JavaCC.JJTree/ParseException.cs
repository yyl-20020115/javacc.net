namespace JavaCC.JJTree;
using System;
using System.Text;
using JavaCC.NET;

public class ParseException : System.Exception
{
    protected internal bool specialConstructor;

    public Token currentToken;

    public int[][] expectedTokenSequences;

    public string[] tokenImage;

    protected internal string eol;


    public ParseException()
    {
        eol = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
        specialConstructor = false;
    }


    public ParseException(Token t, int[][] iarr, string[] strarr)
        : base("")
    {
        eol = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
        specialConstructor = true;
        currentToken = t;
        expectedTokenSequences = iarr;
        tokenImage = strarr;
    }


    protected internal virtual string AddEscapes(string str)
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
                builder.Append(("\\u") + (
                    @this.Substring(@this.Length - 4, @this.Length)));
            }
            else
            {
                builder.Append((char)num);
            }
        }
        return builder.ToString();

    }


    public ParseException(string str)
        : base(str)
    {
        eol = Environment.NewLine;
        specialConstructor = false;
    }


    public override string Message
    {
        get
        {
            if (!specialConstructor)
            {
                return base.Message;
            }
            var builder = new StringBuilder();
            int num = 0;
            for (int i = 0; i < expectedTokenSequences.Length; i++)
            {
                if (num < expectedTokenSequences[i].Length)
                {
                    num = expectedTokenSequences[i].Length;
                }
                for (int j = 0; j < expectedTokenSequences[i].Length; j++)
                {
                    builder.Append(tokenImage[expectedTokenSequences[i][j]]).Append(' ');
                }
                if (expectedTokenSequences[i][expectedTokenSequences[i].Length - 1] != 0)
                {
                    builder.Append("...");
                }
                builder.Append(eol).Append("    ");
            }
            var str = "Encountered \"";
            var next = currentToken.Next;
            for (int k = 0; k < num; k++)
            {
                if (k != 0)
                {
                    str += (" ");
                }
                if (next.Kind == 0)
                {
                    str += tokenImage[0];
                    break;
                }
                str += tokenImage[next.Kind];
                str += " \"";
                str += AddEscapes(next.Image);
                str += " \"";
                next = next.Next;
            }
            str = (str) + ("\" at line ") + (currentToken.Next.BeginLine)
                + (", column ")
                + (currentToken.Next.BeginColumn)
                ;
            str = (str) + (".") + (eol);
            str = ((expectedTokenSequences.Length != 1) ? (str) + ("Was expecting one of:") + (eol)
                + ("    ")
                : (str) + ("Was expecting:") + (eol)
                + ("    ")
                );
            return str + builder.ToString();
        }
    }
}
