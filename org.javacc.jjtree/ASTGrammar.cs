using System.Text;
using org.javacc.parser;
namespace org.javacc.jjtree;
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
