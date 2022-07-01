namespace org.javacc.jjtree;

public class ASTNodeDescriptorExpression : JJTreeNode
{
	internal ASTNodeDescriptorExpression(int P_0) : base(P_0) { }

    internal override string TranslateImage(Token P_0) => WhiteOut(P_0);
}
