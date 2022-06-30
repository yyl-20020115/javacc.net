using System;
using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjdoc;


public class TextGenerator: Generator
{
	protected internal TextWriter ostr;

	
	public virtual void Write(string str)
	{
		ostr.Write(str);
	}

	
	protected internal virtual TextWriter create_output_stream()
	{
		if (string.Equals(JJDocOptions.getOutputFile(), ""))
		{
			if (string.Equals(JJDocGlobals.input_file, "standard input"))
			{
				return (Console.Out);
			}
			string text = ".html";
			if (JJDocOptions.getText())
			{
				text = ".txt";
			}
			int num = JJDocGlobals.input_file.LastIndexOf((char)46);
			if (num == -1)
			{
				JJDocGlobals.output_file = new StringBuilder().Append(JJDocGlobals.input_file).Append(text).ToString();
			}
			else
			{
				string @this = JJDocGlobals.input_file.Substring( num);
				if (string.Equals(@this, text))
				{
					JJDocGlobals.output_file = new StringBuilder().Append(JJDocGlobals.input_file).Append(text).ToString();
				}
				else
				{
					JJDocGlobals.output_file = new StringBuilder().Append(
						JJDocGlobals.input_file.Substring(0, num)).Append(text).ToString();
				}
			}
		}
		else
		{
			JJDocGlobals.output_file = JJDocOptions.getOutputFile();
		}
		try
		{
			ostr = new StreamWriter(JJDocGlobals.output_file);
		}
		catch (IOException)
		{
			goto IL_00ff;
		}
		goto IL_0148;
		IL_0148:
		return ostr;
		IL_00ff:
		
		Error(new StringBuilder().Append("JJDoc: can't open output stream on file ").Append(JJDocGlobals.output_file).Append(".  Using standard output.")
			.ToString());
		ostr = (Console.Out);
		goto IL_0148;
	}

	
	public virtual void Text(string str)
	{
		Write(str);
	}

	
	public virtual void ProductionStart(NormalProduction np)
	{
		ostr.Write(new StringBuilder().Append("\t").Append(np.lhs).Append("\t:=\t")
			.ToString());
	}

	
	public virtual void ProductionEnd(NormalProduction np)
	{
		ostr.Write("\n");
	}

	
	public virtual void Error(string str)
	{
		Console.Error.WriteLine(str);
	}

	
	public TextGenerator()
	{
	}

	
	public virtual void DocumentStart()
	{
		ostr = create_output_stream();
		ostr.Write("\nDOCUMENT START\n");
	}

	
	public virtual void DocumentEnd()
	{
		ostr.Write("\nDOCUMENT END\n");
		ostr.Close();
	}

	
	public virtual void SpecialTokens(string str)
	{
		ostr.Write(str);
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
			ostr.Write("\n\t\t|\t");
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
