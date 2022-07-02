namespace JavaCC.JJTree;

public class ASTBNFAction : JJTreeNode
{
    private INode GetScopingParent(NodeScope scope)
    {
        for (var node = JJTGetParent(); node != null; node = node.JJTGetParent())
        {
            if (node is ASTBNFNodeScope atbs)
            {
                if (atbs.nodeScope == scope) return node;
            }
            else if (node is ASTExpansionNodeScope ntbs && ntbs.nodeScope == scope)
            {
                return node;
            }
        }
        return null;
    }


    internal ASTBNFAction(int id)
        : base(id) { }
    public override void Write(IO io)
    {
        var enclosingNodeScope = NodeScope.GetEnclosingNodeScope(this);
        if (enclosingNodeScope != null && !enclosingNodeScope.IsVoid)
        {
            int num = 1;
            var scopingParent = GetScopingParent(enclosingNodeScope);
            JJTreeNode jJTreeNode = this;
            while (true)
            {
                var node = jJTreeNode.JJTGetParent();
                if (node is ASTBNFSequence || node is ASTBNFTryBlock)
                {
                    if (jJTreeNode.Ordinal != node.JJTGetNumChildren() - 1)
                    {
                        num = 0;
                        break;
                    }
                }
                else if (node is ASTBNFZeroOrOne || node is ASTBNFZeroOrMore || node is ASTBNFOneOrMore)
                {
                    num = 0;
                    break;
                }
                if (node == scopingParent)
                {
                    break;
                }
                jJTreeNode = (JJTreeNode)node;
            }
            if (num != 0)
            {
                JJTreeNode.OpenJJTreeComment(io, null);
                io.WriteLine();
                enclosingNodeScope.InsertCloseNodeAction(io, GetIndentation(this));
                JJTreeNode.CloseJJTreeComment(io);
            }
        }
        base.Write(io);
    }
}