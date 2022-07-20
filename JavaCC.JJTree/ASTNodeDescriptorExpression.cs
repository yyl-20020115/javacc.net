namespace JavaCC.JJTree;
public class ASTNodeDescriptorExpression : JJTreeNode
{
    public ASTNodeDescriptorExpression(int id) : base(id) { }

    public override string TranslateImage(Token token) => WhiteOut(token);
}
