using System.Collections.Generic;
namespace Javacc.Parser;

public class BNFProduction : NormalProduction
{
	public List<Token> declaration_tokens = new();
	public bool jumpPatched = false;
	public BNFProduction() { }
}
