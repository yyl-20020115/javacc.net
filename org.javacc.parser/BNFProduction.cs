using System.Collections;
namespace org.javacc.parser;

public class BNFProduction : NormalProduction
{
	public ArrayList declaration_tokens = new();
	public bool jumpPatched = false;
	public BNFProduction() { }
}
