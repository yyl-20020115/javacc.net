namespace JavaCC.JJTree;
using System.IO;
using JavaCC.Parser;

internal sealed class JJTreeState
{
    internal static void InsertParserMembers(IO io)
    {
        var str = ((!Options.getStatic()) ? "" : "static ");
        io.WriteLine();
        io.WriteLine(("  protected ") + (str) + (NameState())
            + (" jjtree = new ")
            + (NameState())
            + ("();"));
        io.WriteLine();
    }


    internal static void GenerateTreeState_java()
    {
        var f = new FileInfo(
            Path.Combine(JJTreeOptions.JJTreeOutputDirectory.FullName,
                (NameState() + (".java"))));
        IOException ex;
        try
        {
            var outputFile = new OutputFile(f);
            var printWriter = outputFile.getPrintWriter();
            NodeFiles.GeneratePrologue(printWriter);
            InsertState(printWriter);
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


    private static string NameState() => ("JJT") + (JJTreeGlobals.ParserName) + ("State");


    private static void InsertState(TextWriter writer)
    {
        writer.WriteLine("public class " + NameState() + " {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("  private java.util.List nodes;");
        }
        else
        {
            writer.WriteLine("  private java.util.List<Node> nodes;");
        }
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("  private java.util.List marks;");
        }
        else
        {
            writer.WriteLine("  private java.util.List<int> marks;");
        }
        writer.WriteLine("");
        writer.WriteLine("  private int sp;        // number of nodes on stack");
        writer.WriteLine("  private int mk;        // current mark");
        writer.WriteLine("  private boolean node_created;");
        writer.WriteLine("");
        writer.WriteLine("  public " + (NameState()) + ("() {")
            );
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    nodes = new java.util.ArrayList();");
        }
        else
        {
            writer.WriteLine("    nodes = new java.util.ArrayList<Node>();");
        }
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    marks = new java.util.ArrayList();");
        }
        else
        {
            writer.WriteLine("    marks = new java.util.ArrayList<int>();");
        }
        writer.WriteLine("    sp = 0;");
        writer.WriteLine("    mk = 0;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Determines whether the current node was actually closed and");
        writer.WriteLine("     pushed.  This should only be called in the final user action of a");
        writer.WriteLine("     node scope.  */");
        writer.WriteLine("  public boolean nodeCreated() {");
        writer.WriteLine("    return node_created;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Call this to reinitialize the node stack.  It is called");
        writer.WriteLine("     automatically by the parser's ReInit() method. */");
        writer.WriteLine("  public void reset() {");
        writer.WriteLine("    nodes.Clear();");
        writer.WriteLine("    marks.Clear();");
        writer.WriteLine("    sp = 0;");
        writer.WriteLine("    mk = 0;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Returns the root node of the AST.  It only makes sense to call");
        writer.WriteLine("     this after a successful parse. */");
        writer.WriteLine("  public Node rootNode() {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    return (Node)nodes.get(0);");
        }
        else
        {
            writer.WriteLine("    return nodes.get(0);");
        }
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Pushes a node on to the stack. */");
        writer.WriteLine("  public void pushNode(Node n) {");
        writer.WriteLine("    nodes.Add(n);");
        writer.WriteLine("    ++sp;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Returns the node on the top of the stack, and remove it from the");
        writer.WriteLine("     stack.  */");
        writer.WriteLine("  public Node popNode() {");
        writer.WriteLine("    if (--sp < mk) {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("      mk = ((int)marks.remove(marks.Count-1));");
        }
        else
        {
            writer.WriteLine("      mk = marks.remove(marks.Count-1);");
        }
        writer.WriteLine("    }");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    return (Node)nodes.remove(nodes.Count-1);");
        }
        else
        {
            writer.WriteLine("    return nodes.remove(nodes.Count-1);");
        }
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Returns the node currently on the top of the stack. */");
        writer.WriteLine("  public Node peekNode() {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    return (Node)nodes.get(nodes.Count-1);");
        }
        else
        {
            writer.WriteLine("    return nodes.get(nodes.Count-1);");
        }
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("  /* Returns the number of children on the stack in the current node");
        writer.WriteLine("     scope. */");
        writer.WriteLine("  public int nodeArity() {");
        writer.WriteLine("    return sp - mk;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("");
        writer.WriteLine("  public void clearNodeScope(Node n) {");
        writer.WriteLine("    while (sp > mk) {");
        writer.WriteLine("      popNode();");
        writer.WriteLine("    }");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    mk = ((int)marks.remove(marks.Count-1));");
        }
        else
        {
            writer.WriteLine("    mk = marks.remove(marks.Count-1);");
        }
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("");
        writer.WriteLine("  public void openNodeScope(Node n) {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    marks.Add((mk));");
        }
        else
        {
            writer.WriteLine("    marks.Add(mk);");
        }
        writer.WriteLine("    mk = sp;");
        writer.WriteLine("    n.jjtOpen();");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("");
        writer.WriteLine("  /* A definite node is constructed from a specified number of");
        writer.WriteLine("     children.  That number of nodes are popped from the stack and");
        writer.WriteLine("     made the children of the definite node.  Then the definite node");
        writer.WriteLine("     is pushed on to the stack. */");
        writer.WriteLine("  public void closeNodeScope(Node n, int num) {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("    mk = ((int)marks.remove(marks.Count-1));");
        }
        else
        {
            writer.WriteLine("    mk = marks.remove(marks.Count-1);");
        }
        writer.WriteLine("    while (num-- > 0) {");
        writer.WriteLine("      Node c = popNode();");
        writer.WriteLine("      c.jjtSetParent(n);");
        writer.WriteLine("      n.jjtAddChild(c, num);");
        writer.WriteLine("    }");
        writer.WriteLine("    n.jjtClose();");
        writer.WriteLine("    pushNode(n);");
        writer.WriteLine("    node_created = true;");
        writer.WriteLine("  }");
        writer.WriteLine("");
        writer.WriteLine("");
        writer.WriteLine("  /* A conditional node is constructed if its condition is true.  All");
        writer.WriteLine("     the nodes that have been pushed since the node was opened are");
        writer.WriteLine("     made children of the conditional node, which is then pushed");
        writer.WriteLine("     on to the stack.  If the condition is false the node is not");
        writer.WriteLine("     constructed and they are left on the stack. */");
        writer.WriteLine("  public void closeNodeScope(Node n, boolean condition) {");
        writer.WriteLine("    if (condition) {");
        writer.WriteLine("      int a = nodeArity();");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("      mk = ((int)marks.remove(marks.Count-1));");
        }
        else
        {
            writer.WriteLine("      mk = marks.remove(marks.Count-1);");
        }
        writer.WriteLine("      while (a-- > 0) {");
        writer.WriteLine("        Node c = popNode();");
        writer.WriteLine("        c.jjtSetParent(n);");
        writer.WriteLine("        n.jjtAddChild(c, a);");
        writer.WriteLine("      }");
        writer.WriteLine("      n.jjtClose();");
        writer.WriteLine("      pushNode(n);");
        writer.WriteLine("      node_created = true;");
        writer.WriteLine("    } else {");
        if (!string.Equals(JJTreeOptions.JdkVersion, "1.5"))
        {
            writer.WriteLine("      mk = ((int)marks.remove(marks.Count-1));");
        }
        else
        {
            writer.WriteLine("      mk = marks.remove(marks.Count-1);");
        }
        writer.WriteLine("      node_created = false;");
        writer.WriteLine("    }");
        writer.WriteLine("  }");
        writer.WriteLine("}");
    }
}
