namespace org.javacc.jjtree;
public class ASTOptionBinding : JJTreeNode
{
	private bool suppressed = false;
	private string name ="";
	
	internal ASTOptionBinding(int P_0)
		: base(P_0) { }
	
	internal virtual void Initialize(string P_0, string P_1)
	{
		name = P_0;
		if (JJTreeGlobals.isOptionJJTreeOnly(name))
		{
			suppressed = true;
		}
	}

    internal virtual bool IsSuppressed => suppressed;

    internal virtual void SuppressOption(bool P_0)
	{
		int num = ((suppressed = P_0) ? 1 : 0);
	}

    internal override string TranslateImage(Token P_0) => suppressed ? WhiteOut(P_0) : P_0.image;
}
