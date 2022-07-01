using System.Text;
using org.javacc.parser;
namespace org.javacc.jjtree;
public class ASTGrammar : JJTreeNode
{	
	internal ASTGrammar(int id) : base(id) {}

	internal virtual void Generate(IO io)
	{
		io.WriteLine(("/*@bgen(jjtree) ")+(JavaCCGlobals.getIdString(JJTreeGlobals.toolList, io.OutputFileName))+(" */"));
		io.Write("/*@egen*/");
		Write(io);
	}
}
