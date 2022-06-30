using System.Text;
namespace org.javacc.parser;

public class CharacterRange
{
	internal int line;
	internal int column;
	public char left;
	public char right;

	internal CharacterRange() { }
	
	internal CharacterRange(char P_0, char P_1)
	{
		if (P_0 > P_1)
		{
			JavaCCErrors.Semantic_Error(this, new StringBuilder().Append("Invalid range : \"").Append((int)P_0).Append("\" - \"")
				.Append((int)P_1)
				.Append("\". First character shoud be less than or equal to the second one in a range.")
				.ToString());
		}
		left = P_0;
		right = P_1;
	}
}
