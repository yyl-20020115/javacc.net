namespace JavaCC.Parser;

public class RJustName : RegularExpression
{
    public RegularExpression regexpr;

    public RJustName(Token t, string str)
    {
        Line = t.BeginLine;
        Column = t.BeginColumn;
        label = str;
    }


    public override Nfa GenerateNfa(bool b)
    {
        return regexpr.GenerateNfa(b);
    }
    public RJustName()
    {
    }
}
