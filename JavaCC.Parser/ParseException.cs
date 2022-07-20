namespace JavaCC.Parser;
using System;
using System.Text;
using JavaCC.NET;

public class ParseException : Exception
{
	protected internal bool specialConstructor;
	public Token CurrentToken;
	public int[][] ExpectedTokenSequences;
	public string[] TokenImage;
	protected internal string Eol;

	public ParseException()
	{
		Eol = Environment.NewLine;
		specialConstructor = false;
	}


	public ParseException(string str)
		: base(str)
	{
		Eol = Environment.NewLine;
		specialConstructor = false;
	}


	public ParseException(Token token, int[][] iarr, string[] strarr)
		: base("")
	{
		Eol = Environment.NewLine;
		specialConstructor = true;
		CurrentToken = token;
		ExpectedTokenSequences = iarr;
		TokenImage = strarr;
	}


	protected internal virtual string add_escapes(string str)
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
				var text = ("0000")+(Utils.ToString(ch, 16));
				builder.Append(("\\u")+(
					(text.Substring(text.Length - 4, text.Length))));
			}
			else
			{
				builder.Append((char)ch);
			}
		}
		return builder.ToString();
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
			int c = 0;
			for (int i = 0; i < ExpectedTokenSequences.Length; i++)
			{
				if (c < ExpectedTokenSequences[i].Length)
				{
					c = ExpectedTokenSequences[i].Length;
				}
				for (int j = 0; j < ExpectedTokenSequences[i].Length; j++)
				{
					builder.Append(TokenImage[ExpectedTokenSequences[i][j]]).Append(' ');
				}
				if (ExpectedTokenSequences[i][ExpectedTokenSequences[i].Length - 1] != 0)
				{
					builder.Append("...");
				}
				builder.Append(Eol).Append("    ");
			}
			var str = "Encountered \"";
			var next = CurrentToken.Next;
			for (int k = 0; k < c; k++)
			{
				if (k != 0)
				{
					str = (str)+(" ");
				}
				if (next.Kind == 0)
				{
					str = (str)+(TokenImage[0]);
					break;
				}
				str = (str)+(" ")+(TokenImage[next.Kind])
					;
				str = (str)+(" \"");
				str = (str)+(add_escapes(next.Image));
				str = (str)+(" \"");
				next = next.Next;
			}
			str = (str)+("\" at line ")+(CurrentToken.Next.BeginLine)
				+(", column ")
				+(CurrentToken.Next.BeginColumn)
				;
			str = (str)+(".")+(Eol)
				;
			str = ((ExpectedTokenSequences.Length != 1) ? (str)+("Was expecting one of:")+(Eol)
				+("    ")
				 : (str)+("Was expecting:")+(Eol)
				+("    ")
				);
			return (str)+(builder.ToString());
		}
	}
}
