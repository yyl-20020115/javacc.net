namespace JavaCC.JJDoc;

using System;
using System.IO;
using JavaCC.Parser;

public class TextGenerator : Generator
{
    public TextWriter Writer { get; protected internal set; }

    public virtual void Write(string str)
    {
        Writer.Write(str);
    }

    protected internal virtual TextWriter CreateOutputStream()
    {
        if (string.Equals(JJDocOptions.OutputFile, ""))
        {
            if (string.Equals(JJDocGlobals.InputFile, "standard input"))
            {
                return (Console.Out);
            }
            var text = ".html";
            if (JJDocOptions.Text)
            {
                text = ".txt";
            }
            var i = JJDocGlobals.InputFile.LastIndexOf('.');
            if (i == -1)
            {
                JJDocGlobals.OutputFile = (JJDocGlobals.InputFile) + (text);
            }
            else
            {
                var s = JJDocGlobals.InputFile.Substring(i);
                if (string.Equals(s, text))
                {
                    JJDocGlobals.OutputFile = (JJDocGlobals.InputFile) + (text);
                }
                else
                {
                    JJDocGlobals.OutputFile = (JJDocGlobals.InputFile.Substring(0, i)) + text;
                }
            }
        }
        else
        {
            JJDocGlobals.OutputFile = JJDocOptions.OutputFile;
        }
        try
        {
            Writer = new StreamWriter(JJDocGlobals.OutputFile);
        }
        catch (IOException)
        {
            Error(("JJDoc: can't open output stream on file ")
                + (JJDocGlobals.OutputFile)
                + (".  Using standard output."));
            Writer = (Console.Out);
        }
        return Writer;
    }


    public virtual void Text(string str)
    {
        Write(str);
    }

    public virtual void ProductionStart(NormalProduction np)
    {
        Writer.Write(("\t") + (np.lhs) + ("\t:=\t"));
    }

    public virtual void ProductionEnd(NormalProduction np)
    {
        Writer.WriteLine();
    }

    public virtual void Error(string str)
    {
        Console.Error.WriteLine(str);
    }

    public TextGenerator() { }

    public virtual void DocumentStart()
    {
        Writer = CreateOutputStream();
        Writer.Write("\nDOCUMENT START\n");
    }

    public virtual void DocumentEnd()
    {
        Writer.Write("\nDOCUMENT END\n");
        Writer.Close();
    }

    public virtual void SpecialTokens(string str)
    {
        Writer.Write(str);
    }

    public virtual void TokenStart(TokenProduction tp)
    {
    }

    public virtual void TokenEnd(TokenProduction tp)
    {
    }

    public virtual void NonterminalsStart()
    {
        Text("NON-TERMINALS\n");
    }

    public virtual void NonterminalsEnd()
    {
    }

    public virtual void TokensStart()
    {
        Text("TOKENS\n");
    }
    public virtual void TokensEnd()
    {
    }

    public virtual void Javacode(JavaCodeProduction jcp)
    {
        ProductionStart(jcp);
        Text("java code");
        ProductionEnd(jcp);
    }

    public virtual void ExpansionStart(Expansion e, bool b)
    {
        if (!b)
        {
            Writer.Write("\n\t\t|\t");
        }
    }

    public virtual void ExpansionEnd(Expansion e, bool b)
    {
    }

    public virtual void NonTerminalStart(NonTerminal nt)
    {
    }

    public virtual void NonTerminalEnd(NonTerminal nt)
    {
    }

    public virtual void ReStart(RegularExpression re)
    {
    }

    public virtual void ReEnd(RegularExpression re)
    {
    }

    public virtual void Debug(string str)
    {
        Console.Error.WriteLine(str);
    }

    public virtual void Info(string str)
    {
        Console.Error.WriteLine(str);
    }

    public virtual void Warn(string str)
    {
        Console.Error.WriteLine(str);
    }
}
