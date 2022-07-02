using System.Collections.Generic;

namespace Javacc.Parser;

public class JavaCodeProduction : NormalProduction
{
	public List<Token> CodeTokens = new ();
	public JavaCodeProduction() { }
}
