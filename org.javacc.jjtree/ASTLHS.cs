namespace org.javacc.jjtree;
public class ASTLHS : JJTreeNode
{
	internal ASTLHS(int id) : base(id) {}
	public override void Write(IO io)
	{
		var enclosingNodeScope = NodeScope.GetEnclosingNodeScope(this);
		var firstToken = FirstToken;
		var lastToken = LastToken;
		for (var token = firstToken; token != lastToken.Next; token = token.Next)
		{
			TokenUtils.Write(token, io, "jjtThis", enclosingNodeScope.NodeVariable);
		}
	}

}
