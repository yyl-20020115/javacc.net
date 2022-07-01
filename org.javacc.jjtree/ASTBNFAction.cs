namespace org.javacc.jjtree;

public class ASTBNFAction : JJTreeNode
{
	private Node getScopingParent(NodeScope ns)
	{
		for (var node = jjtGetParent(); node != null; node = node.jjtGetParent())
		{
			if (node is ASTBNFNodeScope atbs)
			{
				if (atbs.node_scope == ns)
				{
					return node;
				}
			}
			else if (node is ASTExpansionNodeScope ntbs && ntbs.node_scope == ns)
			{
				return node;
			}
		}
		return null;
	}


	internal ASTBNFAction(int id)
		: base(id)
	{
	}


	public override void Write(IO io)
	{
		var enclosingNodeScope = NodeScope.getEnclosingNodeScope(this);
		if (enclosingNodeScope != null && !enclosingNodeScope.isVoid())
		{
			int num = 1;
			var scopingParent = getScopingParent(enclosingNodeScope);
			JJTreeNode jJTreeNode = this;
			while (true)
			{
				var node = jJTreeNode.jjtGetParent();
				if (node is ASTBNFSequence || node is ASTBNFTryBlock)
				{
					if (jJTreeNode.Ordinal != node.jjtGetNumChildren() - 1)
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
				JJTreeNode.OpenJJTreeComment(io, null);
				io.WriteLine();
				enclosingNodeScope.insertCloseNodeAction(io, GetIndentation(this));
				JJTreeNode.CloseJJTreeComment(io);
			}
		}
		base.Write(io);
	}
}