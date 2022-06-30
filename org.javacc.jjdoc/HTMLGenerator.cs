using org.javacc.parser;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.jjdoc;

public class HTMLGenerator : TextGenerator, Generator
{
	private Dictionary<string,string> id_map =new();
	private int id = 1;
	
	public override void Write(string str)
	{
		ostr.Write(str);
	}

	
	private void WriteLine(string P_0)
	{
		Write(new StringBuilder().Append(P_0).Append("\n").ToString());
	}

	
	public override void ProductionStart(NormalProduction np)
	{
		if (!JJDocOptions.getOneTable())
		{
			WriteLine("");
			WriteLine("<TABLE ALIGN=CENTER>");
			WriteLine(new StringBuilder().Append("<CAPTION><STRONG>").Append(np.lhs).Append("</STRONG></CAPTION>")
				.ToString());
		}
		WriteLine("<TR>");
		WriteLine(new StringBuilder().Append("<TD ALIGN=RIGHT VALIGN=BASELINE><A NAME=\"").Append(get_id(np.lhs)).Append("\">")
			.Append(np.lhs)
			.Append("</A></TD>")
			.ToString());
		WriteLine("<TD ALIGN=CENTER VALIGN=BASELINE>::=</TD>");
		Write("<TD ALIGN=LEFT VALIGN=BASELINE>");
	}

	
	public override void ProductionEnd(NormalProduction np)
	{
		if (!JJDocOptions.getOneTable())
		{
			WriteLine("</TABLE>");
			WriteLine("<HR>");
		}
	}

	
	protected internal virtual string get_id(string str)
	{
		if (this.id_map.TryGetValue(str, out var text))
		{
			StringBuilder stringBuilder = new StringBuilder().Append("prod");
			int num = id;
			id = num + 1;
			text = stringBuilder.Append(num).ToString();
			id_map.Add(str, text);
		}
		return text;
	}

	
	public HTMLGenerator()
	{
	}

	
	public override void Text(string str)
	{
		string str2 = "";
		for (int i = 0; i < str.Length; i++)
		{
			str2 = ((str[i] != '<') ? ((str[i] != '>') ? ((str[i] != '&') ? new StringBuilder().Append(str2).Append(str[i]).ToString() : new StringBuilder().Append(str2).Append("&amp;").ToString()) : new StringBuilder().Append(str2).Append("&gt;").ToString()) : new StringBuilder().Append(str2).Append("&lt;").ToString());
		}
		Write(str2);
	}

	
	public override void DocumentStart()
	{
		ostr = create_output_stream();
		WriteLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">");
		WriteLine("<HTML>");
		WriteLine("<HEAD>");
		if (!string.Equals("", JJDocOptions.getCSS()))
		{
			WriteLine(new StringBuilder().Append("<LINK REL=\"stylesheet\" type=\"text/css\" href=\"").Append(JJDocOptions.getCSS()).Append("\"/>")
				.ToString());
		}
		if (JJDocGlobals.input_file != null)
		{
			WriteLine(new StringBuilder().Append("<TITLE>BNF for ").Append(JJDocGlobals.input_file).Append("</TITLE>")
				.ToString());
		}
		else
		{
			WriteLine("<TITLE>A BNF grammar by JJDoc</TITLE>");
		}
		WriteLine("</HEAD>");
		WriteLine("<BODY>");
		WriteLine(new StringBuilder().Append("<H1 ALIGN=CENTER>BNF for ").Append(JJDocGlobals.input_file).Append("</H1>")
			.ToString());
	}

	
	public override void DocumentEnd()
	{
		WriteLine("</BODY>");
		WriteLine("</HTML>");
		ostr.Close();
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
		if (JJDocOptions.getOneTable())
		{
			WriteLine("<TABLE>");
		}
	}

	
	public override void NonterminalsEnd()
	{
		if (JJDocOptions.getOneTable())
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
		Write(new StringBuilder().Append("<A HREF=\"#").Append(get_id(nt.name)).Append("\">")
			.ToString());
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
