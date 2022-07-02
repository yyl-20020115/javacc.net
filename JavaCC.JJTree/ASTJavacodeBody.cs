namespace JavaCC.JJTree;
public class ASTJavacodeBody : JJTreeNode
{
    internal NodeScope nodeScope;
    internal ASTJavacodeBody(int id) : base(id) { }

    public override void Write(IO io)
    {
        if (nodeScope.IsVoid)
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
        JJTreeNode.OpenJJTreeComment(io, nodeScope.NodeDescriptorText);
        io.WriteLine();
        nodeScope.insertOpenNodeCode(io, text);
        nodeScope.TryTokenSequence(io, text, firstToken, LastToken);
    }
}
