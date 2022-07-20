namespace JavaCC.JJTree;
public class ASTJavacodeBody : JJTreeNode
{
    public NodeScope NodeScope { get; protected internal set; }
    public ASTJavacodeBody(int id) : base(id) { }

    public override void Write(IO io)
    {
        if (NodeScope.IsVoid)
        {
            base.Write(io);
            return;
        }
        var firstToken = FirstToken;
        var text = "";
        for (int i = 4; i < firstToken.BeginColumn; i++)
        {
            text += " ";
        }
        OpenJJTreeComment(io, NodeScope.NodeDescriptorText);
        io.WriteLine();
        NodeScope.InsertOpenNodeCode(io, text);
        NodeScope.TryTokenSequence(io, text, firstToken, LastToken);
    }
}
