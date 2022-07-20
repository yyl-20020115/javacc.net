namespace JavaCC.JJTree;
public class ASTOptionBinding : JJTreeNode
{
    public bool Suppressed { get; protected internal set; } = false;
    public string Name { get; protected internal set; } = "";

    public ASTOptionBinding(int id) : base(id) { }

    public virtual void Initialize(string name, string text)
    {
        if (JJTreeGlobals.IsOptionJJTreeOnly(Name = name))
        {
            Suppressed = true;
        }
    }

    public virtual bool IsSuppressed => Suppressed;

    public virtual void SuppressOption(bool suppress)
    {
        Suppressed = suppress;
    }

    public override string TranslateImage(Token token) => Suppressed ? WhiteOut(token) : token.Image;
}
