namespace JavaCC.JJTree;
public class ASTBNFNodeScope : JJTreeNode
{
    internal NodeScope nodeScope;
    internal JJTreeNode expUnit;
    internal ASTBNFNodeScope(int id) : base(id) { }
    public override void Write(IO io)
    {
        if (nodeScope.IsVoid)
        {
            base.Write(io);
            return;
        }
        var indentation = GetIndentation(expUnit);
        JJTreeNode.OpenJJTreeComment(io, nodeScope.NodeDescriptor.Descriptor);
        io.WriteLine();
        nodeScope.TryExpansionUnit(io, indentation, expUnit);
    }
}
