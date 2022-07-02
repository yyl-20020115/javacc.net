namespace JavaCC.JJTree;
public class ASTExpansionNodeScope : JJTreeNode
{
    internal NodeScope nodeScope;
    internal JJTreeNode expUnit;

    internal ASTExpansionNodeScope(int id) : base(id) { }

    public override void Write(IO io)
    {
        var indentation = GetIndentation(expUnit);
        JJTreeNode.OpenJJTreeComment(io, nodeScope.NodeDescriptor.Descriptor);
        io.WriteLine();
        nodeScope.InsertOpenNodeAction(io, indentation);
        nodeScope.TryExpansionUnit(io, indentation, expUnit);
        ((ASTNodeDescriptor)JJTGetChild(1)).Write(io);
    }
}
