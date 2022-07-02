namespace JavaCC.JJTree;
public class ASTBNFDeclaration : JJTreeNode
{
    internal NodeScope nodeScope;
    internal ASTBNFDeclaration(int id)
        : base(id) { }
    public override void Write(IO io)
    {
        if (!nodeScope.IsVoid)
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
            JJTreeNode.OpenJJTreeComment(io, nodeScope.NodeDescriptorText);
            io.WriteLine();
            nodeScope.InsertOpenNodeCode(io, text);
            JJTreeNode.CloseJJTreeComment(io);
        }
        base.Write(io);
    }
}
