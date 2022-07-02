namespace Javacc.JJTree;
public class ASTOptionBinding : JJTreeNode
{
	private bool Suppressed = false;
	private string Name ="";
	
	internal ASTOptionBinding(int id) : base(id) { }
	
	internal virtual void Initialize(string name, string text)
	{
		Name = name;
		if (JJTreeGlobals.IsOptionJJTreeOnly(Name))
		{
			Suppressed = true;
		}
	}

    internal virtual bool IsSuppressed => Suppressed;

    internal virtual void SuppressOption(bool P_0)
	{
		int num = ((Suppressed = P_0) ? 1 : 0);
	}

    internal override string TranslateImage(Token token) => Suppressed ? WhiteOut(token) : token.Image;
}
