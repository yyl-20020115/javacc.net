using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using System.Text;
using javacc.net;

namespace org.javacc.jjtree;


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


	protected internal virtual string add_escapes(string str)
	{
		StringBuilder stringBuilder = new StringBuilder();
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
				string @this = new StringBuilder().Append("0000").Append(Utils.ToString(num, 16)).ToString();
				stringBuilder.Append(new StringBuilder().Append("\\u").Append(
					@this.Substring(@this.Length - 4, @this.Length)).ToString());
			}
			else
			{
				stringBuilder.Append((char)num);
			}
		}
		string result = stringBuilder.ToString();

		return result;
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
			StringBuilder stringBuilder = new StringBuilder();
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
					str = new StringBuilder().Append(str).Append(" ").ToString();
				}
				if (next.kind == 0)
				{
					str = new StringBuilder().Append(str).Append(tokenImage[0]).ToString();
					break;
				}
				str = new StringBuilder().Append(str).Append(" ").Append(tokenImage[next.kind])
					.ToString();
				str = new StringBuilder().Append(str).Append(" \"").ToString();
				str = new StringBuilder().Append(str).Append(add_escapes(next.image)).ToString();
				str = new StringBuilder().Append(str).Append(" \"").ToString();
				next = next.next;
			}
			str = new StringBuilder().Append(str).Append("\" at line ").Append(currentToken.next.beginLine)
				.Append(", column ")
				.Append(currentToken.next.beginColumn)
				.ToString();
			str = new StringBuilder().Append(str).Append(".").Append(eol)
				.ToString();
			str = (((nint)expectedTokenSequences.LongLength != 1) ? new StringBuilder().Append(str).Append("Was expecting one of:").Append(eol)
				.Append("    ")
				.ToString() : new StringBuilder().Append(str).Append("Was expecting:").Append(eol)
				.Append("    ")
				.ToString());
			return new StringBuilder().Append(str).Append(stringBuilder.ToString()).ToString();
		}
	}


	protected ParseException(SerializationInfo P_0, StreamingContext P_1)
	: base(P_0, P_1)
	{
	}
}
