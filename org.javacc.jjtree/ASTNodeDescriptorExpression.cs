namespace org.javacc.jjtree;

public class ASTNodeDescriptorExpression : JJTreeNode
{
	internal ASTNodeDescriptorExpression(int P_0)
	: base(P_0)
	{
	}


	internal override string translateImage(Token P_0)
	{
		string result = whiteOut(P_0);

		return result;
	}
}
