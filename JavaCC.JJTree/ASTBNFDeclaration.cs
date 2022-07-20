namespace JavaCC.JJTree;
public class ASTBNFDeclaration : JJTreeNode
{
    public NodeScope NodeScope;
    public ASTBNFDeclaration(int id)
        : base(id) { }
    public override void Write(IO io)
    {
        if (!NodeScope.IsVoid)
        {
            var text = "";
            if (TokenUtils.HasTokens(this))
            {
                for (int i = 1; i < FirstToken.BeginColumn; i++)
                {
                    text += " ";
                }
            }
            else
            {
                text = "  ";
            }
            OpenJJTreeComment(io, NodeScope.NodeDescriptorText);
            io.WriteLine();
            NodeScope.InsertOpenNodeCode(io, text);
            CloseJJTreeComment(io);
        }
        base.Write(io);
    }
}
