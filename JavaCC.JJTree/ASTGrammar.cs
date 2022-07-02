using System.Text;
using JavaCC.Parser;
namespace JavaCC.JJTree;
public class ASTGrammar : JJTreeNode
{	
	internal ASTGrammar(int id) : base(id) {}

	internal virtual void Generate(IO io)
	{
		io.WriteLine(("/*@bgen(jjtree) ")+(JavaCCGlobals.GetIdStringList(JJTreeGlobals.ToolList, io.OutputFileName))+(" */"));
		io.Write("/*@egen*/");
		Write(io);
	}
}
