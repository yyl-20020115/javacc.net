namespace JavaCC.JJTree;
public class ASTJavacode : ASTProduction
{
    public Token StmBeginLoc { get; protected internal set; }
    public ASTJavacode(int id) : base(id) { }
}
