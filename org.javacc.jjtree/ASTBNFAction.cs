namespace org.javacc.jjtree;

public class ASTBNFAction : JJTreeNode
{
	private Node getScopingParent(NodeScope P_0)
	{
		for (Node node = jjtGetParent(); node != null; node = node.jjtGetParent())
		{
			if (node is ASTBNFNodeScope)
			{
				if (((ASTBNFNodeScope)node).node_scope == P_0)
				{
					return node;
				}
			}
			else if (node is ASTExpansionNodeScope && ((ASTExpansionNodeScope)node).node_scope == P_0)
			{
				return node;
			}
		}
		return null;
	}


	internal ASTBNFAction(int P_0)
		: base(P_0)
	{
	}


	public override void Write(IO io)
	{
		NodeScope enclosingNodeScope = NodeScope.getEnclosingNodeScope(this);
		if (enclosingNodeScope != null && !enclosingNodeScope.isVoid())
		{
			int num = 1;
			Node scopingParent = getScopingParent(enclosingNodeScope);
			JJTreeNode jJTreeNode = this;
			while (true)
			{
				Node node = jJTreeNode.jjtGetParent();
				if (node is ASTBNFSequence || node is ASTBNFTryBlock)
				{
					if (jJTreeNode.getOrdinal() != node.jjtGetNumChildren() - 1)
					{
						num = 0;
						break;
					}
				}
				else if (node is ASTBNFZeroOrOne || node is ASTBNFZeroOrMore || node is ASTBNFOneOrMore)
				{
					num = 0;
					break;
				}
				if (node == scopingParent)
				{
					break;
				}
				jJTreeNode = (JJTreeNode)node;
			}
			if (num != 0)
			{
				JJTreeNode.openJJTreeComment(io, null);
				io.WriteLine();
				enclosingNodeScope.insertCloseNodeAction(io, getIndentation(this));
				JJTreeNode.closeJJTreeComment(io);
			}
		}
		base.Write(io);
	}


	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}
}