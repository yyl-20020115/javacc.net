namespace org.javacc.jjtree;
public class ASTNodeDescriptorExpression : JJTreeNode
{
	internal ASTNodeDescriptorExpression(int id) : base(id) { }

    internal override string TranslateImage(Token token) => WhiteOut(token);
}
