namespace JavaCC.Parser;

public class RJustName : RegularExpression
{
    public RegularExpression RegExpr;

    public RJustName(Token token, string label)
    {
        this.Line = token.BeginLine;
        this.Column = token.BeginColumn;
        this.Label = label;
    }


    public override Nfa GenerateNfa(bool b) => RegExpr.GenerateNfa(b);
}
