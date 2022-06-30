using System.Runtime.Serialization;
using System.Security.Permissions;

using System.Text;
using javacc.net;

namespace org.javacc.jjtree;


public class TokenMgrError : System.Exception
{
	internal const int LEXICAL_ERROR = 0;

	internal const int STATIC_LEXER_ERROR = 1;

	internal const int INVALID_LEXICAL_STATE = 2;

	internal const int LOOP_DETECTED = 3;

	internal int errorCode;

	
	public TokenMgrError(string str, int i)
		: base(str)
	{
		errorCode = i;
	}

	
	public TokenMgrError(bool b, int i1, int i2, int i3, string str, char ch, int i4)
		: this(LexicalError(b, i1, i2, i3, str, ch), i4)
	{
	}

	

	protected internal static string addEscapes(string str)
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
				stringBuilder.Append(new StringBuilder().Append("\\u").Append(java.lang.String.instancehelper_substring(@this, @this.Length - 4, @this.Length)).ToString());
			}
			else
			{
				stringBuilder.Append((char)num);
			}
		}
		string result = stringBuilder.ToString();
		
		return result;
	}

	
	protected internal static string LexicalError(bool b, int i1, int i2, int i3, string str, char ch)
	{
		string result = new StringBuilder().Append("Lexical error at line ").Append(i2).Append(", column ")
			.Append(i3)
			.Append(".  Encountered: ")
			.Append((!b) ? new StringBuilder().Append("\"").Append(addEscapes(java.lang.String.valueOf(ch))).Append("\"")
				.Append(" (")
				.Append((int)ch)
				.Append("), ")
				.ToString() : "<EOF> ")
			.Append("after : \"")
			.Append(addEscapes(str))
			.Append("\"")
			.ToString();
		
		return result;
	}


	public override string Message => base.Message;
	
	public TokenMgrError()
	{
	}

	
	[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\nversion=\"1\">\r\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\nversion=\"1\"\r\nFlags=\"SerializationFormatter\"/>\r\n</PermissionSet>\r\n")]
	protected TokenMgrError(SerializationInfo P_0, StreamingContext P_1)
		: base(P_0, P_1)
	{
	}
}
