namespace JavaCC.JJTree;

public class ASTBNF : ASTProduction
{
    internal Token DeclBeginLoc;
    internal ASTBNF(int id) : base(id)
    {
        this.ThrowsList.Add("ParseException");
        this.ThrowsList.Add("RuntimeException");
    }

    public override string ToString() => base.ToString() + ": " + (Name);
}
