namespace JavaCC.JJDoc;

using JavaCC.Parser;
using System.Collections.Generic;

public class HTMLGenerator : TextGenerator, Generator
{
    private Dictionary<string, string> idmap = new();
    private int id = 1;

    public override void Write(string str)
    {
        writer.Write(str);
    }

    private void WriteLine(string str)
    {
        writer.WriteLine(str);
    }

    public override void ProductionStart(NormalProduction np)
    {
        if (!JJDocOptions.OneTable)
        {
            WriteLine("");
            WriteLine("<TABLE ALIGN=CENTER>");
            WriteLine(("<CAPTION><STRONG>") + (np.lhs) + ("</STRONG></CAPTION>"));
        }
        WriteLine("<TR>");
        WriteLine(("<TD ALIGN=RIGHT VALIGN=BASELINE><A NAME=\"") + (get_id(np.lhs)) + ("\">")
            + (np.lhs)
            + ("</A></TD>")
            );
        WriteLine("<TD ALIGN=CENTER VALIGN=BASELINE>::=</TD>");
        Write("<TD ALIGN=LEFT VALIGN=BASELINE>");
    }


    public override void ProductionEnd(NormalProduction np)
    {
        if (!JJDocOptions.OneTable)
        {
            WriteLine("</TABLE>");
            WriteLine("<HR>");
        }
    }

    protected internal virtual string get_id(string str)
    {
        if (this.idmap.TryGetValue(str, out var text))
        {
            text = ("prod" + id++);
            idmap.Add(str, text);
        }
        return text;
    }

    public HTMLGenerator() { }

    public override void Text(string str)
    {
        var str2 = "";
        for (int i = 0; i < str.Length; i++)
        {
            str2 = ((str[i] != '<') ? ((str[i] != '>') ? ((str[i] != '&')
                ? (str2) + (str[i])
                : (str2) + ("&amp;"))
                : (str2) + ("&gt;"))
                : (str2) + ("&lt;"))
                ;
        }
        Write(str2);
    }


    public override void DocumentStart()
    {
        writer = CreateOutputStream();
        WriteLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">");
        WriteLine("<HTML>");
        WriteLine("<HEAD>");
        if (!string.Equals("", JJDocOptions.CSS))
        {
            WriteLine(
                ("<LINK REL=\"stylesheet\" type=\"text/css\" href=\"")
                + (JJDocOptions.CSS)
                + ("\"/>")
                );
        }
        if (JJDocGlobals.InputFile != null)
        {
            WriteLine(
                ("<TITLE>BNF for ")
                + (JJDocGlobals.InputFile)
                + ("</TITLE>")
                );
        }
        else
        {
            WriteLine("<TITLE>A BNF grammar by JJDoc</TITLE>");
        }
        WriteLine("</HEAD>");
        WriteLine("<BODY>");
        WriteLine(

            ("<H1 ALIGN=CENTER>BNF for ")
            + (JJDocGlobals.InputFile)
            + ("</H1>")
            );
    }

    public override void DocumentEnd()
    {
        WriteLine("</BODY>");
        WriteLine("</HTML>");
        this.writer.Close();
    }


    public override void SpecialTokens(string str)
    {
        WriteLine(" <!-- Special token -->");
        WriteLine(" <TR>");
        WriteLine("  <TD>");
        WriteLine("<PRE>");
        Write(str);
        WriteLine("</PRE>");
        WriteLine("  </TD>");
        WriteLine(" </TR>");
    }

    public override void TokenStart(TokenProduction tp)
    {
        WriteLine(" <!-- Token -->");
        WriteLine(" <TR>");
        WriteLine("  <TD>");
        WriteLine("   <PRE>");
    }


    public override void TokenEnd(TokenProduction tp)
    {
        WriteLine("   </PRE>");
        WriteLine("  </TD>");
        WriteLine(" </TR>");
    }


    public override void NonterminalsStart()
    {
        WriteLine("<H2 ALIGN=CENTER>NON-TERMINALS</H2>");
        if (JJDocOptions.OneTable)
        {
            WriteLine("<TABLE>");
        }
    }

    public override void NonterminalsEnd()
    {
        if (JJDocOptions.OneTable)
        {
            WriteLine("</TABLE>");
        }
    }

    public override void TokensStart()
    {
        WriteLine("<H2 ALIGN=CENTER>TOKENS</H2>");
        WriteLine("<TABLE>");
    }

    public override void TokensEnd()
    {
        WriteLine("</TABLE>");
    }

    public override void Javacode(JavaCodeProduction jcp)
    {
        ProductionStart(jcp);
        WriteLine("<I>java code</I></TD></TR>");
        ProductionEnd(jcp);
    }

    public override void ExpansionStart(Expansion e, bool b)
    {
        if (!b)
        {
            WriteLine("<TR>");
            WriteLine("<TD ALIGN=RIGHT VALIGN=BASELINE></TD>");
            WriteLine("<TD ALIGN=CENTER VALIGN=BASELINE>|</TD>");
            Write("<TD ALIGN=LEFT VALIGN=BASELINE>");
        }
    }

    public override void ExpansionEnd(Expansion e, bool b)
    {
        WriteLine("</TD>");
        WriteLine("</TR>");
    }

    public override void NonTerminalStart(NonTerminal nt)
    {
        Write(
            ("<A HREF=\"#")
            + (get_id(nt.name))
            + ("\">")
            );
    }


    public override void NonTerminalEnd(NonTerminal nt)
    {
        Write("</A>");
    }

    public override void ReStart(RegularExpression re)
    {
    }

    public override void ReEnd(RegularExpression re)
    {
    }
}
