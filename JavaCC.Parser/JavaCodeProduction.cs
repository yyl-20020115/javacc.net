namespace JavaCC.Parser;
using System.Collections.Generic;

public class JavaCodeProduction : NormalProduction
{
    public readonly List<Token> CodeTokens = new();
    public JavaCodeProduction() { }
}
