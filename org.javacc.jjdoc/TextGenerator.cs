using System;
using System.IO;
using org.javacc.parser;

namespace org.javacc.jjdoc;

public class TextGenerator: Generator
{
	protected internal TextWriter writer;
	
	public virtual void Write(string str)
	{
		writer.Write(str);
	}

	
	protected internal virtual TextWriter CreateOutputStream()
	{
		if (string.Equals(JJDocOptions.OutputFile, ""))
		{
			if (string.Equals(JJDocGlobals.input_file, "standard input"))
			{
				return (Console.Out);
			}
			string text = ".html";
			if (JJDocOptions.Text)
			{
				text = ".txt";
			}
			int num = JJDocGlobals.input_file.LastIndexOf((char)46);
			if (num == -1)
			{
				JJDocGlobals.output_file = (JJDocGlobals.input_file)+(text);
			}
			else
			{
				string @this = JJDocGlobals.input_file.Substring( num);
				if (string.Equals(@this, text))
				{
					JJDocGlobals.output_file = (JJDocGlobals.input_file)+(text);
				}
				else
				{
					JJDocGlobals.output_file = (JJDocGlobals.input_file.Substring(0, num))+text;
				}
			}
		}
		else
		{
			JJDocGlobals.output_file = JJDocOptions.OutputFile;
		}
		try
		{
			writer = new StreamWriter(JJDocGlobals.output_file);
		}
		catch (IOException)
		{
			goto IL_00ff;
		}
		goto IL_0148;
		IL_0148:
		return writer;
		IL_00ff:
		
		Error(("JJDoc: can't open output stream on file ")
			+(JJDocGlobals.output_file)
			+(".  Using standard output."));
		writer = (Console.Out);
		goto IL_0148;
	}

	
	public virtual void Text(string str)
	{
		Write(str);
	}
	
	public virtual void ProductionStart(NormalProduction np)
	{
		writer.Write(("\t")+(np.lhs)+("\t:=\t"));
	}
	
	public virtual void ProductionEnd(NormalProduction np)
	{
		writer.WriteLine();
	}
	
	public virtual void Error(string str)
	{
		Console.Error.WriteLine(str);
	}
	
	public TextGenerator() { }
	
	public virtual void DocumentStart()
	{
		writer = CreateOutputStream();
		writer.Write("\nDOCUMENT START\n");
	}
	
	public virtual void DocumentEnd()
	{
		writer.Write("\nDOCUMENT END\n");
		writer.Close();
	}
	
	public virtual void SpecialTokens(string str)
	{
		writer.Write(str);
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
			writer.Write("\n\t\t|\t");
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
