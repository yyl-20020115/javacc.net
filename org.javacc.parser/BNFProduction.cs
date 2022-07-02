using System.Collections.Generic;
namespace org.javacc.parser;

public class BNFProduction : NormalProduction
{
	public List<Token> declaration_tokens = new();
	public bool jumpPatched = false;
	public BNFProduction() { }
}
