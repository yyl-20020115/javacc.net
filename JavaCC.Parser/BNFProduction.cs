namespace JavaCC.Parser;
using System.Collections.Generic;

public class BNFProduction : NormalProduction
{
    public List<Token> DeclarationTokens = new();
    public bool JumpPatched = false;
    public BNFProduction() { }
}
