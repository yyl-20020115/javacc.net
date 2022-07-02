namespace JavaCC.JJTree;
public class ASTExpansionNodeScope : JJTreeNode
{
    internal NodeScope nodeScope;
    internal JJTreeNode expUnit;

    internal ASTExpansionNodeScope(int id) : base(id) { }

    public override void Write(IO io)
    {
        string indentation = GetIndentation(expUnit);
        JJTreeNode.OpenJJTreeComment(io, nodeScope.NodeDescriptor.Descriptor);
        io.WriteLine();
        nodeScope.insertOpenNodeAction(io, indentation);
        nodeScope.tryExpansionUnit(io, indentation, expUnit);
        ((ASTNodeDescriptor)jjtGetChild(1)).Write(io);
    }
}
