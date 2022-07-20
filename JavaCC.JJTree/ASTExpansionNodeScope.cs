namespace JavaCC.JJTree;
public class ASTExpansionNodeScope : JJTreeNode
{
    public NodeScope NodeScope { get; protected internal set; }
    public JJTreeNode ExpUnit { get; protected internal set; }

    public ASTExpansionNodeScope(int id) : base(id) { }

    public override void Write(IO io)
    {
        var indentation = GetIndentation(ExpUnit);
        OpenJJTreeComment(io, NodeScope.NodeDescriptor.Descriptor);
        io.WriteLine();
        NodeScope.InsertOpenNodeAction(io, indentation);
        NodeScope.TryExpansionUnit(io, indentation, ExpUnit);
        ((ASTNodeDescriptor)JJTGetChild(1)).Write(io);
    }
}
