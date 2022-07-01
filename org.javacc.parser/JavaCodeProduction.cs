using System.Collections.Generic;

namespace org.javacc.parser;

public class JavaCodeProduction : NormalProduction
{
	public List<Token> CodeTokens = new ();
	public JavaCodeProduction() { }
}
