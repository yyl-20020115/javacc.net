using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjtree;


internal sealed class JJTreeState
{
	
	internal static void insertParserMembers(IO P_0)
	{
		string str = ((!Options.getStatic()) ? "" : "static ");
		P_0.WriteLine();
		P_0.WriteLine(new StringBuilder().Append("  protected ").Append(str).Append(nameState())
			.Append(" jjtree = new ")
			.Append(nameState())
			.Append("();")
			.ToString());
		P_0.WriteLine();
	}

	
	internal static void generateTreeState_java()
	{
		
		FileInfo f = new FileInfo(
			Path.Combine(JJTreeOptions.getJJTreeOutputDirectory().DirectoryName, new StringBuilder().Append(nameState()).Append(".java").ToString()));
		IOException ex;
		try
		{
			OutputFile outputFile = new OutputFile(f);
			TextWriter printWriter = outputFile.getPrintWriter();
			NodeFiles.generatePrologue(printWriter);
			insertState(printWriter);
			outputFile.Close();
			return;
		}
		catch (IOException x)
		{
			ex = x;
		}
		IOException @this = ex;
		string message = (@this.Message);
		
		throw new System.Exception(message);
	}

	
	private static string nameState()
	{
		string result = new StringBuilder().Append("JJT").Append(JJTreeGlobals.parserName).Append("State")
			.ToString();
		
		return result;
	}

	
	private static void insertState(TextWriter P_0)
	{
		P_0.WriteLine(new StringBuilder().Append("public class ").Append(nameState()).Append(" {")
			.ToString());
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("  private java.util.List nodes;");
		}
		else
		{
			P_0.WriteLine("  private java.util.List<Node> nodes;");
		}
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("  private java.util.List marks;");
		}
		else
		{
			P_0.WriteLine("  private java.util.List<int> marks;");
		}
		P_0.WriteLine("");
		P_0.WriteLine("  private int sp;        // number of nodes on stack");
		P_0.WriteLine("  private int mk;        // current mark");
		P_0.WriteLine("  private boolean node_created;");
		P_0.WriteLine("");
		P_0.WriteLine(new StringBuilder().Append("  public ").Append(nameState()).Append("() {")
			.ToString());
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    nodes = new java.util.ArrayList();");
		}
		else
		{
			P_0.WriteLine("    nodes = new java.util.ArrayList<Node>();");
		}
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    marks = new java.util.ArrayList();");
		}
		else
		{
			P_0.WriteLine("    marks = new java.util.ArrayList<int>();");
		}
		P_0.WriteLine("    sp = 0;");
		P_0.WriteLine("    mk = 0;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Determines whether the current node was actually closed and");
		P_0.WriteLine("     pushed.  This should only be called in the final user action of a");
		P_0.WriteLine("     node scope.  */");
		P_0.WriteLine("  public boolean nodeCreated() {");
		P_0.WriteLine("    return node_created;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Call this to reinitialize the node stack.  It is called");
		P_0.WriteLine("     automatically by the parser's ReInit() method. */");
		P_0.WriteLine("  public void reset() {");
		P_0.WriteLine("    nodes.Clear();");
		P_0.WriteLine("    marks.Clear();");
		P_0.WriteLine("    sp = 0;");
		P_0.WriteLine("    mk = 0;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Returns the root node of the AST.  It only makes sense to call");
		P_0.WriteLine("     this after a successful parse. */");
		P_0.WriteLine("  public Node rootNode() {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    return (Node)nodes.get(0);");
		}
		else
		{
			P_0.WriteLine("    return nodes.get(0);");
		}
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Pushes a node on to the stack. */");
		P_0.WriteLine("  public void pushNode(Node n) {");
		P_0.WriteLine("    nodes.Add(n);");
		P_0.WriteLine("    ++sp;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Returns the node on the top of the stack, and remove it from the");
		P_0.WriteLine("     stack.  */");
		P_0.WriteLine("  public Node popNode() {");
		P_0.WriteLine("    if (--sp < mk) {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("      mk = ((int)marks.remove(marks.Count-1)).intValue();");
		}
		else
		{
			P_0.WriteLine("      mk = marks.remove(marks.Count-1);");
		}
		P_0.WriteLine("    }");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    return (Node)nodes.remove(nodes.Count-1);");
		}
		else
		{
			P_0.WriteLine("    return nodes.remove(nodes.Count-1);");
		}
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Returns the node currently on the top of the stack. */");
		P_0.WriteLine("  public Node peekNode() {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    return (Node)nodes.get(nodes.Count-1);");
		}
		else
		{
			P_0.WriteLine("    return nodes.get(nodes.Count-1);");
		}
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("  /* Returns the number of children on the stack in the current node");
		P_0.WriteLine("     scope. */");
		P_0.WriteLine("  public int nodeArity() {");
		P_0.WriteLine("    return sp - mk;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("");
		P_0.WriteLine("  public void clearNodeScope(Node n) {");
		P_0.WriteLine("    while (sp > mk) {");
		P_0.WriteLine("      popNode();");
		P_0.WriteLine("    }");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    mk = ((int)marks.remove(marks.Count-1)).intValue();");
		}
		else
		{
			P_0.WriteLine("    mk = marks.remove(marks.Count-1);");
		}
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("");
		P_0.WriteLine("  public void openNodeScope(Node n) {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    marks.Add(new int(mk));");
		}
		else
		{
			P_0.WriteLine("    marks.Add(mk);");
		}
		P_0.WriteLine("    mk = sp;");
		P_0.WriteLine("    n.jjtOpen();");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("");
		P_0.WriteLine("  /* A definite node is constructed from a specified number of");
		P_0.WriteLine("     children.  That number of nodes are popped from the stack and");
		P_0.WriteLine("     made the children of the definite node.  Then the definite node");
		P_0.WriteLine("     is pushed on to the stack. */");
		P_0.WriteLine("  public void closeNodeScope(Node n, int num) {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("    mk = ((int)marks.remove(marks.Count-1)).intValue();");
		}
		else
		{
			P_0.WriteLine("    mk = marks.remove(marks.Count-1);");
		}
		P_0.WriteLine("    while (num-- > 0) {");
		P_0.WriteLine("      Node c = popNode();");
		P_0.WriteLine("      c.jjtSetParent(n);");
		P_0.WriteLine("      n.jjtAddChild(c, num);");
		P_0.WriteLine("    }");
		P_0.WriteLine("    n.jjtClose();");
		P_0.WriteLine("    pushNode(n);");
		P_0.WriteLine("    node_created = true;");
		P_0.WriteLine("  }");
		P_0.WriteLine("");
		P_0.WriteLine("");
		P_0.WriteLine("  /* A conditional node is constructed if its condition is true.  All");
		P_0.WriteLine("     the nodes that have been pushed since the node was opened are");
		P_0.WriteLine("     made children of the conditional node, which is then pushed");
		P_0.WriteLine("     on to the stack.  If the condition is false the node is not");
		P_0.WriteLine("     constructed and they are left on the stack. */");
		P_0.WriteLine("  public void closeNodeScope(Node n, boolean condition) {");
		P_0.WriteLine("    if (condition) {");
		P_0.WriteLine("      int a = nodeArity();");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("      mk = ((int)marks.remove(marks.Count-1)).intValue();");
		}
		else
		{
			P_0.WriteLine("      mk = marks.remove(marks.Count-1);");
		}
		P_0.WriteLine("      while (a-- > 0) {");
		P_0.WriteLine("        Node c = popNode();");
		P_0.WriteLine("        c.jjtSetParent(n);");
		P_0.WriteLine("        n.jjtAddChild(c, a);");
		P_0.WriteLine("      }");
		P_0.WriteLine("      n.jjtClose();");
		P_0.WriteLine("      pushNode(n);");
		P_0.WriteLine("      node_created = true;");
		P_0.WriteLine("    } else {");
		if (!string.Equals(JJTreeOptions.getJdkVersion(), "1.5"))
		{
			P_0.WriteLine("      mk = ((int)marks.remove(marks.Count-1)).intValue();");
		}
		else
		{
			P_0.WriteLine("      mk = marks.remove(marks.Count-1);");
		}
		P_0.WriteLine("      node_created = false;");
		P_0.WriteLine("    }");
		P_0.WriteLine("  }");
		P_0.WriteLine("}");
	}

	
	private JJTreeState()
	{
	}
}
