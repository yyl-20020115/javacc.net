namespace Javacc.JJTree;
public class ASTCompilationUnit : JJTreeNode
{
	internal ASTCompilationUnit(int id) : base(id) { }
	public override void Write(IO io)
	{
		var token = FirstToken;
		while (true)
		{
			if (token == JJTreeGlobals.ParserImports 
				&& !string.Equals(JJTreeGlobals.NodePackageName, "") 
				&& !string.Equals(JJTreeGlobals.NodePackageName, JJTreeGlobals.PackageName))
			{
				io.Out.WriteLine("");
				io.Out.WriteLine("import "+JJTreeGlobals.NodePackageName+(".*;"));
			}
			if (token == JJTreeGlobals.ParserImplements)
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
			if (token == JJTreeGlobals.ParserClassBodyStart)
			{
				JJTreeNode.OpenJJTreeComment(io, null);
				JJTreeState.InsertParserMembers(io);
				JJTreeNode.CloseJJTreeComment(io);
			}
			if (token == LastToken) break;
			token = token.Next;
		}
	}
}
