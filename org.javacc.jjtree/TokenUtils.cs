using System.Text;
using javacc.net;
using org.javacc.parser;
namespace org.javacc.jjtree;

public sealed class TokenUtils
{	
	internal static string AddUnicodeEscapes(string P_0)
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

    internal static bool HasTokens(JJTreeNode node) => node.LastToken.Next != node.FirstToken;


    internal static void Write(Token _token, IO io, string text1, string text2)
	{
		var token = _token.SpecialToken;
		if (token != null)
		{
			while (token.SpecialToken != null)
			{
				token = token.SpecialToken;
			}
			while (token != null)
			{
				io.Write(AddUnicodeEscapes(token.Image));
				token = token.Next;
			}
		}
		string text = _token.Image;
		if (text1 != null && string.Equals(text, text1))
		{
			text = text2;
		}
		io.Write(AddUnicodeEscapes(text));
	}

	
	internal static string RemoveEscapeAndQuotes(Token token, string _text)
	{
		string text = "";
		int num = 1;
		while (num < _text.Length - 1)
		{
			if (_text[num] != '\\')
			{
				text +=_text[num];
				num++;
				continue;
			}
			num++;
			int num2 = _text[num];
			switch (num2)
			{
			case 98:
				text = ('\b').ToString();
				num++;
				continue;
			case 116:
				text = ('\t').ToString();
				num++;
				continue;
			case 110:
				text = ('\n').ToString();
				num++;
				continue;
			case 102:
				text = ('\f').ToString();
				num++;
				continue;
			case 114:
				text = ('\r').ToString();
				num++;
				continue;
			case 34:
				text = ('"').ToString();
				num++;
				continue;
			case 39:
				text = ('\'').ToString();
				num++;
				continue;
			case 92:
				text = ('\\').ToString();
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
				int num4 = _text[num];
				if (num4 >= 48 && num4 <= 55)
				{
					num3 = num3 * 8 + num4 - 48;
					num++;
					num4 = _text[num];
					if (num2 <= 51 && num4 >= 48 && num4 <= 55)
					{
						num3 = num3 * 8 + num4 - 48;
						num++;
					}
				}
				text +=((char)num3).ToString();
				continue;
			}
			}
			if (num2 == 117)
			{
				num++;
				num2 = _text[num];
				if (HexChar((char)num2))
				{
					int num3 = HexVal((char)num2);
					num++;
					num2 = _text[num];
					if (HexChar((char)num2))
					{
						num3 = num3 * 16 + HexVal((char)num2);
						num++;
						num2 = _text[num];
						if (HexChar((char)num2))
						{
							num3 = num3 * 16 + HexVal((char)num2);
							num++;
							num2 = _text[num];
							if (HexChar((char)num2))
							{
								_ = num3 * 16 + HexVal((char)num2);
								num++;
								continue;
							}
						}
					}
				}
				JavaCCErrors.Parse_Error(token, new StringBuilder().Append("Encountered non-hex character '").Append((char)num2).Append("' at position ")
					.Append(num)
					.Append(" of string - Unicode escape must have 4 hex digits after it.")
					.ToString());
				return text;
			}
			JavaCCErrors.Parse_Error(token, new StringBuilder().Append("Illegal escape sequence '\\").Append((char)num2).Append("' at position ")
				.Append(num)
				.Append(" of string.")
				.ToString());
			return text;
		}
		return text;
	}

    private static bool HexChar(char c) => c switch
    {
        >= '0' and <= '9' => true,
        >= 'A' and <= 'F' => true,
        >= 'a' and <= 'f' => true,
        _ => false,
    };

    private static int HexVal(char c) => c switch
    {
        >= '0' and <= '9' => c - 48,
        >= 'A' and <= 'F' => c - 65 + 10,
        _ => c - 97 + 10
    };
}
