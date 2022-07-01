namespace org.javacc.jjtree;

public class ASTBNF : ASTProduction
{
	internal Token declBeginLoc;
	
	internal ASTBNF(int P_0)
		: base(P_0)
	{
		throws_list.Add("ParseException");
		throws_list.Add("RuntimeException");
	}


    public override string ToString() => base.ToString() + ": " + (name);
}
