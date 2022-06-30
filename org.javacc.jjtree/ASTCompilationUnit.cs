using System.Text;
namespace org.javacc.jjtree;

public class ASTCompilationUnit : JJTreeNode
{

	internal ASTCompilationUnit(int P_0)
		: base(P_0) { }


	public override void Write(IO io)
	{
		Token token = getFirstToken();
		while (true)
		{
			if (token == JJTreeGlobals.parserImports && !string.Equals(JJTreeGlobals.nodePackageName, "") && !string.Equals(JJTreeGlobals.nodePackageName, JJTreeGlobals.packageName))
			{
				io.getOut().WriteLine("");
				io.getOut().WriteLine(new StringBuilder().Append("import ").Append(JJTreeGlobals.nodePackageName).Append(".*;")
					.ToString());
			}
			if (token == JJTreeGlobals.parserImplements)
			{
				if (string.Equals(token.image, "implements"))
				{
					Write(token, io);
					JJTreeNode.openJJTreeComment(io, null);
					io.getOut().Write(new StringBuilder().Append(" ").Append(NodeFiles.nodeConstants()).Append(", ")
						.ToString());
					JJTreeNode.closeJJTreeComment(io);
				}
				else
				{
					JJTreeNode.openJJTreeComment(io, null);
					io.getOut().Write(new StringBuilder().Append("implements ").Append(NodeFiles.nodeConstants()).ToString());
					JJTreeNode.closeJJTreeComment(io);
					Write(token, io);
				}
			}
			else
			{
				Write(token, io);
			}
			if (token == JJTreeGlobals.parserClassBodyStart)
			{
				JJTreeNode.openJJTreeComment(io, null);
				JJTreeState.insertParserMembers(io);
				JJTreeNode.closeJJTreeComment(io);
			}
			if (token == getLastToken())
			{
				break;
			}
			token = token.next;
		}
	}

	public new void Write(object P_0)
	{
		this.Write((IO)P_0);
	}
}
