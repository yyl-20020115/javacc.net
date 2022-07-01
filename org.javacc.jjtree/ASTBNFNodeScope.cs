namespace org.javacc.jjtree;

public class ASTBNFNodeScope : JJTreeNode
{
	internal NodeScope node_scope;
	internal JJTreeNode expansion_unit;

	internal ASTBNFNodeScope(int P_0) : base(P_0) { }

	public override void Write(IO io)
	{
		if (node_scope.isVoid())
		{
			base.Write(io);
			return;
		}
		string indentation = GetIndentation(expansion_unit);
		JJTreeNode.OpenJJTreeComment(io, node_scope.getNodeDescriptor().Descriptor);
		io.WriteLine();
		node_scope.tryExpansionUnit(io, indentation, expansion_unit);
	}
}
