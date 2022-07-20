namespace JavaCC.Parser;

public class RegExprSpec
{
    public RegularExpression Rexp;
    public Action Action;
    public Token NsToken;
    public string NextState = "";

    public RegExprSpec() { }
}
