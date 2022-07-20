namespace JavaCC.JJTree;
public class ASTCompilationUnit : JJTreeNode
{
    public ASTCompilationUnit(int id) : base(id) { }
    public override void Write(IO io)
    {
        var token = FirstToken;
        while (true)
        {
            if (token == JJTreeGlobals.ParserImports
                && !string.Equals(JJTreeGlobals.NodePackageName, "")
                && !string.Equals(JJTreeGlobals.NodePackageName, JJTreeGlobals.PackageName))
            {
                io.Writer.WriteLine("");
                io.Writer.WriteLine("import " + JJTreeGlobals.NodePackageName + (".*;"));
            }
            if (token == JJTreeGlobals.ParserImplements)
            {
                if (string.Equals(token.Image, "implements"))
                {
                    Write(token, io);
                    OpenJJTreeComment(io);
                    io.Writer.Write((" ") + (NodeFiles.NodeConstants) + (", "));
                    CloseJJTreeComment(io);
                }
                else
                {
                    OpenJJTreeComment(io);
                    io.Writer.Write(("implements ") + (NodeFiles.NodeConstants));
                    CloseJJTreeComment(io);
                    Write(token, io);
                }
            }
            else
            {
                Write(token, io);
            }
            if (token == JJTreeGlobals.ParserClassBodyStart)
            {
                OpenJJTreeComment(io);
                JJTreeState.InsertParserMembers(io);
                CloseJJTreeComment(io);
            }
            if (token == LastToken) break;
            token = token.Next;
        }
    }
}
