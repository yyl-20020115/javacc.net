using System.Text;
namespace org.javacc.jjtree;

public class ASTBNFDeclaration : JJTreeNode
{
	internal NodeScope node_scope;


	internal ASTBNFDeclaration(int P_0)
		: base(P_0)
	{
	}


	public override void Write(IO io)
	{
		if (!node_scope.isVoid())
		{
			string text = "";
			if (TokenUtils.hasTokens(this))
			{
				for (int i = 1; i < getFirstToken().beginColumn; i++)
				{
					text = new StringBuilder().Append(text).Append(" ").ToString();
				}
			}
			else
			{
				text = "  ";
			}
			JJTreeNode.openJJTreeComment(io, node_scope.getNodeDescriptorText());
			io.WriteLine();
			node_scope.insertOpenNodeCode(io, text);
			JJTreeNode.closeJJTreeComment(io);
		}
		base.Write(io);
	}

	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}
}
