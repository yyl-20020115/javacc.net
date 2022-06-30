using System.Collections;
namespace org.javacc.parser;

public class JavaCodeProduction : NormalProduction
{
	public ArrayList code_tokens;

	public JavaCodeProduction()
	{
		code_tokens = new ArrayList();
	}
}
