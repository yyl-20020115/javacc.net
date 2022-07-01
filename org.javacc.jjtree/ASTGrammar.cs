using System.Text;
using org.javacc.parser;
namespace org.javacc.jjtree;

public class ASTGrammar : JJTreeNode
{	
	internal ASTGrammar(int P_0) : base(P_0) {}

	internal virtual void generate(IO P_0)
	{
		P_0.WriteLine(("/*@bgen(jjtree) ")+(JavaCCGlobals.getIdString(JJTreeGlobals.toolList, P_0.OutputFileName))+(" */"));
		P_0.Write("/*@egen*/");
		Write(P_0);
	}
}
