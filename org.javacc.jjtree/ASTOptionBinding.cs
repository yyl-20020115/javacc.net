namespace org.javacc.jjtree;
public class ASTOptionBinding : JJTreeNode
{
	private bool suppressed;
	private string name;
	
	internal ASTOptionBinding(int P_0)
		: base(P_0)
	{
		suppressed = false;
	}

	
	internal virtual void initialize(string P_0, string P_1)
	{
		name = P_0;
		if (JJTreeGlobals.isOptionJJTreeOnly(name))
		{
			suppressed = true;
		}
	}

	internal virtual bool isSuppressed()
	{
		return suppressed;
	}

	internal virtual void suppressOption(bool P_0)
	{
		int num = ((suppressed = P_0) ? 1 : 0);
	}

	
	internal override string translateImage(Token P_0)
	{
		if (suppressed)
		{
			string result = whiteOut(P_0);
			
			return result;
		}
		return P_0.image;
	}
}
