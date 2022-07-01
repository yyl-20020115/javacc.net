namespace org.javacc.jjtree;

public class ASTLHS : JJTreeNode
{

	internal ASTLHS(int P_0)
		: base(P_0)
	{
	}


	public override void Write(IO io)
	{
		NodeScope enclosingNodeScope = NodeScope.getEnclosingNodeScope(this);
		Token firstToken = FirstToken;
		Token lastToken = LastToken;
		for (Token token = firstToken; token != lastToken.next; token = token.next)
		{
			TokenUtils.Write(token, io, "jjtThis", enclosingNodeScope.getNodeVariable());
		}
	}


	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}

}
