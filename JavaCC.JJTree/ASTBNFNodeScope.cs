namespace JavaCC.JJTree;
public class ASTBNFNodeScope : JJTreeNode
{
    public NodeScope NodeScope { get; protected internal set; }
    public JJTreeNode ExpUnit { get; protected internal set; }
    public ASTBNFNodeScope(int id) : base(id) { }
    public override void Write(IO io)
    {
        if (NodeScope.IsVoid)
        {
            base.Write(io);
            return;
        }
        var indentation = GetIndentation(ExpUnit);
        OpenJJTreeComment(io, NodeScope.NodeDescriptor.Descriptor);
        io.WriteLine();
        NodeScope.TryExpansionUnit(io, indentation, ExpUnit);
    }
}
