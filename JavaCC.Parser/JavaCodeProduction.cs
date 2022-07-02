using System.Collections.Generic;

namespace JavaCC.Parser;

public class JavaCodeProduction : NormalProduction
{
	public List<Token> CodeTokens = new ();
	public JavaCodeProduction() { }
}
