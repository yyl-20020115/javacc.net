namespace JavaCC.JJTree;

public class ASTBNF : ASTProduction
{
	internal Token DeclBeginLoc;
	internal ASTBNF(int id) : base(id)
	{
		ThrowsList.Add("ParseException");
		ThrowsList.Add("RuntimeException");
	}

    public override string ToString() => base.ToString() + ": " + (Name);
}
