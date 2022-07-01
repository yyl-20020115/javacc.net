namespace org.javacc.jjtree;

public class ASTExpansionNodeScope : JJTreeNode
{
	internal NodeScope node_scope;
	internal JJTreeNode expansion_unit;

	internal ASTExpansionNodeScope(int P_0) : base(P_0) { }

	public override void Write(IO io)
	{
		string indentation = GetIndentation(expansion_unit);
		JJTreeNode.OpenJJTreeComment(io, node_scope.getNodeDescriptor().Descriptor);
		io.WriteLine();
		node_scope.insertOpenNodeAction(io, indentation);
		node_scope.tryExpansionUnit(io, indentation, expansion_unit);
		((ASTNodeDescriptor)jjtGetChild(1)).Write(io);
	}

	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}
}
