namespace org.javacc.jjtree;
public class ASTJavacodeBody : JJTreeNode
{
	internal NodeScope node_scope;
	internal ASTJavacodeBody(int id) : base(id) { }

	public override void Write(IO io)
	{
		if (node_scope.IsVoid)
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
		JJTreeNode.OpenJJTreeComment(io, node_scope.getNodeDescriptorText());
		io.WriteLine();
		node_scope.insertOpenNodeCode(io, text);
		node_scope.tryTokenSequence(io, text, firstToken, LastToken);
	}
}
