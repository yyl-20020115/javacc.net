using System.Text;
namespace org.javacc.jjtree;

public class ASTJavacodeBody : JJTreeNode
{
	internal NodeScope node_scope;
	internal ASTJavacodeBody(int P_0)
		: base(P_0) { }


	public override void Write(IO io)
	{
		if (node_scope.isVoid())
		{
			base.Write(io);
			return;
		}
		Token firstToken = FirstToken;
		string text = "";
		for (int i = 4; i < firstToken.beginColumn; i++)
		{
			text += " ";
		}
		JJTreeNode.OpenJJTreeComment(io, node_scope.getNodeDescriptorText());
		io.WriteLine();
		node_scope.insertOpenNodeCode(io, text);
		node_scope.tryTokenSequence(io, text, firstToken, LastToken);
	}


	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}
}
