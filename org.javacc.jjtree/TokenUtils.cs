using System.Text;
using javacc.net;
using org.javacc.parser;

namespace org.javacc.jjtree;

public sealed class TokenUtils
{
	
	internal static string addUnicodeEscapes(string P_0)
	{
		;
		var stringBuilder = new StringBuilder(P_0.Length);
		for (int i = 0; i < P_0.Length; i++)
		{
			int num = P_0[i];
			if ((num < 32 || num > 126) && num != 9 && num != 10 && num != 13 && num != 12)
			{
				string @this = new StringBuilder().Append("0000").Append(Utils.ToString(num, 16)).ToString();
				stringBuilder.Append(new StringBuilder().Append("\\u").Append(@this.Substring(@this.Length - 4, @this.Length)).ToString());
			}
			else
			{
				stringBuilder.Append((char)num);
			}
		}
		string result = stringBuilder.ToString();
		
		return result;
	}

	
	internal static bool hasTokens(JJTreeNode P_0)
	{
		if (P_0.getLastToken().next == P_0.getFirstToken())
		{
			return false;
		}
		return true;
	}

	
	internal static void Write(Token P_0, IO P_1, string P_2, string P_3)
	{
		Token token = P_0.specialToken;
		if (token != null)
		{
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				P_1.Write(addUnicodeEscapes(token.image));
				token = token.next;
			}
		}
		string text = P_0.image;
		if (P_2 != null && string.Equals(text, P_2))
		{
			text = P_3;
		}
		P_1.Write(addUnicodeEscapes(text));
	}

	
	internal static string remove_escapes_and_quotes(Token P_0, string P_1)
	{
		string text = "";
		int num = 1;
		while (num < P_1.Length - 1)
		{
			if (P_1[num] != '\\')
			{
				text = new StringBuilder().Append(text).Append(P_1[num]).ToString();
				num++;
				continue;
			}
			num++;
			int num2 = P_1[num];
			switch (num2)
			{
			case 98:
				text = new StringBuilder().Append(text).Append('\b').ToString();
				num++;
				continue;
			case 116:
				text = new StringBuilder().Append(text).Append('\t').ToString();
				num++;
				continue;
			case 110:
				text = new StringBuilder().Append(text).Append('\n').ToString();
				num++;
				continue;
			case 102:
				text = new StringBuilder().Append(text).Append('\f').ToString();
				num++;
				continue;
			case 114:
				text = new StringBuilder().Append(text).Append('\r').ToString();
				num++;
				continue;
			case 34:
				text = new StringBuilder().Append(text).Append('"').ToString();
				num++;
				continue;
			case 39:
				text = new StringBuilder().Append(text).Append('\'').ToString();
				num++;
				continue;
			case 92:
				text = new StringBuilder().Append(text).Append('\\').ToString();
				num++;
				continue;
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			{
				int num3 = num2 - 48;
				num++;
				int num4 = P_1[num];
				if (num4 >= 48 && num4 <= 55)
				{
					num3 = num3 * 8 + num4 - 48;
					num++;
					num4 = P_1[num];
					if (num2 <= 51 && num4 >= 48 && num4 <= 55)
					{
						num3 = num3 * 8 + num4 - 48;
						num++;
					}
				}
				text = new StringBuilder().Append(text).Append((char)num3).ToString();
				continue;
			}
			}
			if (num2 == 117)
			{
				num++;
				num2 = P_1[num];
				if (hexchar((char)num2))
				{
					int num3 = hexval((char)num2);
					num++;
					num2 = P_1[num];
					if (hexchar((char)num2))
					{
						num3 = num3 * 16 + hexval((char)num2);
						num++;
						num2 = P_1[num];
						if (hexchar((char)num2))
						{
							num3 = num3 * 16 + hexval((char)num2);
							num++;
							num2 = P_1[num];
							if (hexchar((char)num2))
							{
								_ = num3 * 16 + hexval((char)num2);
								num++;
								continue;
							}
						}
					}
				}
				JavaCCErrors.Parse_Error(P_0, new StringBuilder().Append("Encountered non-hex character '").Append((char)num2).Append("' at position ")
					.Append(num)
					.Append(" of string - Unicode escape must have 4 hex digits after it.")
					.ToString());
				return text;
			}
			JavaCCErrors.Parse_Error(P_0, new StringBuilder().Append("Illegal escape sequence '\\").Append((char)num2).Append("' at position ")
				.Append(num)
				.Append(" of string.")
				.ToString());
			return text;
		}
		return text;
	}

	private static bool hexchar(char P_0)
	{
		if (P_0 >= '0' && P_0 <= '9')
		{
			return true;
		}
		if (P_0 >= 'A' && P_0 <= 'F')
		{
			return true;
		}
		if (P_0 >= 'a' && P_0 <= 'f')
		{
			return true;
		}
		return false;
	}

	private static int hexval(char P_0)
	{
		if (P_0 >= '0' && P_0 <= '9')
		{
			return P_0 - 48;
		}
		if (P_0 >= 'A' && P_0 <= 'F')
		{
			return P_0 - 65 + 10;
		}
		return P_0 - 97 + 10;
	}

	
	private TokenUtils()
	{
	}
	
	internal static void Write(Token P_0, IO P_1)
	{
		Write(P_0, P_1, null, null);
	}
}
