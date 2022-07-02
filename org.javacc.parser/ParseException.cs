using System;
using System.Text;
using javacc.net;

namespace org.javacc.parser;

public class ParseException : System.Exception
{
	protected internal bool specialConstructor;
	public Token currentToken;
	public int[][] expectedTokenSequences;
	public string[] tokenImage;
	protected internal string eol;

	public ParseException()
	{
		eol = Environment.NewLine;
		specialConstructor = false;
	}


	public ParseException(string str)
		: base(str)
	{
		eol = Environment.NewLine;
		specialConstructor = false;
	}


	public ParseException(Token t, int[][] iarr, string[] strarr)
		: base("")
	{
		eol = Environment.NewLine;
		specialConstructor = true;
		currentToken = t;
		expectedTokenSequences = iarr;
		tokenImage = strarr;
	}


	protected internal virtual string add_escapes(string str)
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
				string @this = ("0000")+(Utils.ToString(num, 16)).ToString();
				stringBuilder.Append(("\\u")+(
					(@this.Substring(@this.Length - 4, @this.Length))).ToString());
			}
			else
			{
				stringBuilder.Append((char)num);
			}
		}
		string result = stringBuilder.ToString();

		return result;
	}


	public override string Message
	{
		get
		{
			if (!specialConstructor)
			{
				return base.Message;
			}
			var stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 0; i < (nint)expectedTokenSequences.LongLength; i++)
			{
				if (num < (nint)expectedTokenSequences[i].LongLength)
				{
					num = expectedTokenSequences[i].Length;
				}
				for (int j = 0; j < (nint)expectedTokenSequences[i].LongLength; j++)
				{
					stringBuilder.Append(tokenImage[expectedTokenSequences[i][j]]).Append(' ');
				}
				if (expectedTokenSequences[i][(nint)expectedTokenSequences[i].LongLength - 1] != 0)
				{
					stringBuilder.Append("...");
				}
				stringBuilder.Append(eol).Append("    ");
			}
			string str = "Encountered \"";
			Token next = currentToken.next;
			for (int k = 0; k < num; k++)
			{
				if (k != 0)
				{
					str = (str)+(" ").ToString();
				}
				if (next.kind == 0)
				{
					str = (str)+(tokenImage[0]).ToString();
					break;
				}
				str = (str)+(" ")+(tokenImage[next.kind])
					.ToString();
				str = (str)+(" \"").ToString();
				str = (str)+(add_escapes(next.image)).ToString();
				str = (str)+(" \"").ToString();
				next = next.next;
			}
			str = (str)+("\" at line ")+(currentToken.next.BeginLine)
				+(", column ")
				+(currentToken.next.BeginColumn)
				.ToString();
			str = (str)+(".")+(eol)
				.ToString();
			str = (((nint)expectedTokenSequences.LongLength != 1) ? (str)+("Was expecting one of:")+(eol)
				+("    ")
				.ToString() : (str)+("Was expecting:")+(eol)
				+("    ")
				.ToString());
			return (str)+(stringBuilder.ToString()).ToString();
		}
	}

}
