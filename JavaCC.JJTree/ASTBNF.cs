namespace JavaCC.JJTree;

public class ASTBNF : ASTProduction
{
    public Token DeclBeginLoc { get; protected internal set; }
    public ASTBNF(int id) : base(id)
    {
        this.ThrowsList.Add("ParseException");
        this.ThrowsList.Add("RuntimeException");
    }

    public override string ToString() => base.ToString() + ": " + (Name);
}
