namespace JavaCC.JJTree;

public class ASTBNFAction : JJTreeNode
{
    public INode GetScopingParent(NodeScope scope)
    {
        for (var node = JJTGetParent(); node != null; node = node.JJTGetParent())
        {
            if (node is ASTBNFNodeScope atbs)
            {
                if (atbs.NodeScope == scope) return node;
            }
            else if (node is ASTExpansionNodeScope ntbs && ntbs.NodeScope == scope)
            {
                return node;
            }
        }
        return null;
    }


    public ASTBNFAction(int id)
        : base(id) { }
    public override void Write(IO io)
    {
        var enclosingNodeScope = NodeScope.GetEnclosingNodeScope(this);
        if (enclosingNodeScope != null && !enclosingNodeScope.IsVoid)
        {
            bool flag = true;
            var scopingParent = GetScopingParent(enclosingNodeScope);
            JJTreeNode jJTreeNode = this;
            while (true)
            {
                var node = jJTreeNode.JJTGetParent();
                if (node is ASTBNFSequence || node is ASTBNFTryBlock)
                {
                    if (jJTreeNode.Ordinal != node.JJTGetNumChildren() - 1)
                    {
                        flag = false;
                        break;
                    }
                }
                else if (node is ASTBNFZeroOrOne || node is ASTBNFZeroOrMore || node is ASTBNFOneOrMore)
                {
                    flag = false;
                    break;
                }
                if (node == scopingParent)
                {
                    break;
                }
                jJTreeNode = (JJTreeNode)node;
            }
            if (flag)
            {
                OpenJJTreeComment(io);
                io.WriteLine();
                enclosingNodeScope.InsertCloseNodeAction(io, GetIndentation(this));
                CloseJJTreeComment(io);
            }
        }
        base.Write(io);
    }
}