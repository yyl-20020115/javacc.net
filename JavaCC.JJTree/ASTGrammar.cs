namespace JavaCC.JJTree;
using JavaCC.Parser;
public class ASTGrammar : JJTreeNode
{
    public ASTGrammar(int id) : base(id) { }

    public virtual void Generate(IO io)
    {
        io.WriteLine(("/*@bgen(jjtree) ") + (JavaCCGlobals.GetIdStringList(JJTreeGlobals.ToolList, io.OutputFileName)) + (" */"));
        io.Write("/*@egen*/");
        Write(io);
    }
}
