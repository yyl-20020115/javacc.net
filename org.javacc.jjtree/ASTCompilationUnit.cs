namespace org.javacc.jjtree;
public class ASTCompilationUnit : JJTreeNode
{
	internal ASTCompilationUnit(int id) : base(id) { }
	public override void Write(IO io)
	{
		var token = FirstToken;
		while (true)
		{
			if (token == JJTreeGlobals.parserImports 
				&& !string.Equals(JJTreeGlobals.nodePackageName, "") 
				&& !string.Equals(JJTreeGlobals.nodePackageName, JJTreeGlobals.packageName))
			{
				io.Out.WriteLine("");
				io.Out.WriteLine("import "+JJTreeGlobals.nodePackageName+(".*;"));
			}
			if (token == JJTreeGlobals.parserImplements)
			{
				if (string.Equals(token.Image, "implements"))
				{
					Write(token, io);
					JJTreeNode.OpenJJTreeComment(io, null);
					io.Out.Write((" ")+(NodeFiles.NodeConstants)+(", "));
					JJTreeNode.CloseJJTreeComment(io);
				}
				else
				{
					JJTreeNode.OpenJJTreeComment(io, null);
					io.Out.Write(("implements ")+(NodeFiles.NodeConstants));
					JJTreeNode.CloseJJTreeComment(io);
					Write(token, io);
				}
			}
			else
			{
				Write(token, io);
			}
			if (token == JJTreeGlobals.parserClassBodyStart)
			{
				JJTreeNode.OpenJJTreeComment(io, null);
				JJTreeState.insertParserMembers(io);
				JJTreeNode.CloseJJTreeComment(io);
			}
			if (token == LastToken) break;
			token = token.Next;
		}
	}
}
