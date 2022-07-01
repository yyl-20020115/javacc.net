using System.Runtime.Serialization;
using System.Text;
using javacc.net;
namespace org.javacc.jjtree;

public class TokenMgrError : System.Exception
{
	internal const int LEXICAL_ERROR = 0;

	internal const int STATIC_LEXER_ERROR = 1;

	internal const int INVALID_LEXICAL_STATE = 2;

	internal const int LOOP_DETECTED = 3;

	internal int errorCode = 1;


	public TokenMgrError(string str, int i)
		: base(str) { }

	public TokenMgrError(bool b, int i1, int i2, int i3, string str, char ch, int i4)
		: this(LexicalError(b, i1, i2, i3, str, ch), i4) { }

	protected internal static string AddEscapes(string str)
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
				string @this = new StringBuilder().Append("0000").Append(Utils.ToString(num, 16)).ToString();
				stringBuilder.Append(new StringBuilder().Append("\\u").Append((@this.Substring(@this.Length - 4, @this.Length))).ToString());
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
		string result = new StringBuilder().Append("Lexical error at line ").Append(i2).Append(", column ")
			.Append(i3)
			.Append(".  Encountered: ")
			.Append((!b) ? new StringBuilder().Append("\"").Append(AddEscapes((ch.ToString()))).Append("\"")
				.Append(" (")
				.Append((int)ch)
				.Append("), ")
				.ToString() : "<EOF> ")
			.Append("after : \"")
			.Append(AddEscapes(str))
			.Append("\"")
			.ToString();

		return result;
	}


	public override string Message => base.Message;

	public TokenMgrError() { }
}
