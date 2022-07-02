namespace JavaCC.JJTree;

using System.IO;
using System.Collections.Generic;
using JavaCC.Parser;

internal sealed class NodeFiles
{
    internal const string nodeVersion = "4.1";
    internal static HashSet<string> nodesGenerated = new();

    internal static string NodeConstants => JJTreeGlobals.ParserName + "TreeConstants";

    internal static void GenerateTreeConstants_java()
    {
        var str = NodeConstants;

        var f = new FileInfo(
            Path.Combine(JJTreeOptions.JJTreeOutputDirectory.DirectoryName, (str) + (".java")));
        IOException ex;
        try
        {
            OutputFile outputFile = new OutputFile(f);
            TextWriter printWriter = outputFile.getPrintWriter();
            var nodeIds = ASTNodeDescriptor.NodeIds;
            var nodeNames = ASTNodeDescriptor.NodeNames;
            GeneratePrologue(printWriter);
            printWriter.WriteLine(("public interface ") + (str));
            printWriter.WriteLine("{");
            for (int i = 0; i < nodeIds.Count; i++)
            {
                string str2 = (string)nodeIds[i];
                printWriter.WriteLine(("  public int ") + (str2) + (" = ")
                    + (i)
                    + (";")
                    );
            }
            printWriter.WriteLine();
            printWriter.WriteLine();
            printWriter.WriteLine("  public String[] jjtNodeName = {");
            for (int i = 0; i < nodeNames.Count; i++)
            {
                string str2 = (string)nodeNames[i];
                printWriter.WriteLine(("    \"") + (str2) + ("\","));
            }
            printWriter.WriteLine("  };");
            printWriter.WriteLine("}");
            printWriter.Close();
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


    internal static void GenerateVisitor_java()
    {
        if (!JJTreeOptions.Visitor)
        {
            return;
        }
        var str = visitorClass();

        var f = new FileInfo(Path.Combine(JJTreeOptions.JJTreeOutputDirectory.FullName,
            (str) + (".java")));
        IOException ex;
        try
        {
            var outputFile = new OutputFile(f);
            var printWriter = outputFile.getPrintWriter();
            var nodeNames = ASTNodeDescriptor.NodeNames;
            GeneratePrologue(printWriter);
            printWriter.WriteLine(("public interface ") + (str));
            printWriter.WriteLine("{");
            string str2 = mergeVisitorException();
            string str3 = "Object";
            if (!string.Equals(JJTreeOptions.VisitorDataType, ""))
            {
                str3 = JJTreeOptions.VisitorDataType;
            }
            printWriter.WriteLine(("  public Object visit(SimpleNode node, ") + (str3) + (" data)")
                + (str2)
                + (";")
                );
            if (JJTreeOptions.Multi)
            {
                for (int i = 0; i < nodeNames.Count; i++)
                {
                    string text = (string)nodeNames[i];
                    if (!string.Equals(text, "void"))
                    {
                        string str4 = (JJTreeOptions.NodePrefix) + (text);
                        printWriter.WriteLine(("  public Object visit(") + (str4) + (" node, ")
                            + (str3)
                            + (" data)")
                            + (str2)
                            + (";")
                            );
                    }
                }
            }
            printWriter.WriteLine("}");
            printWriter.Close();
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


    internal static void GeneratePrologue(TextWriter P_0)
    {
        if (!string.Equals(JJTreeGlobals.NodePackageName, ""))
        {
            P_0.WriteLine(("package ") + (JJTreeGlobals.NodePackageName) + (";")
                );
            P_0.WriteLine();
            if (!string.Equals(JJTreeGlobals.NodePackageName, JJTreeGlobals.PackageName))
            {
                P_0.WriteLine(("import ") + (JJTreeGlobals.PackageName) + (".*;")
                );
                P_0.WriteLine();
            }
        }
    }


    internal static void ensure(IO P_0, string P_1)
    {

        var file = new FileInfo(
            Path.Combine(
                JJTreeOptions.JJTreeOutputDirectory.FullName, (P_1) + (".java")));
        if (!string.Equals(P_1, "Node"))
        {
            if (string.Equals(P_1, "SimpleNode"))
            {
                ensure(P_0, "Node");
            }
            else
            {
                ensure(P_0, "SimpleNode");
            }
        }
        if ((!string.Equals(P_1, "Node") && !JJTreeOptions.BuildNodeFiles)
            || (file.Exists && nodesGenerated.Contains(file.FullName)))
        {
            return;
        }
        IOException ex;
        try
        {
            string[] strarr = new string[7] { "MULTI", "NODE_USES_PARSER", "VISITOR", "TRACK_TOKENS", "NODE_PREFIX", "NODE_EXTENDS", "NODE_FACTORY" };
            OutputFile outputFile = new OutputFile(file, "4.1", strarr);
            outputFile.setToolName("JJTree");
            nodesGenerated.Add(file.FullName);
            if (outputFile.needToWrite)
            {
                if (string.Equals(P_1, "Node"))
                {
                    generateNode_java(outputFile);
                }
                else if (string.Equals(P_1, "SimpleNode"))
                {
                    generateSimpleNode_java(outputFile);
                }
                else
                {
                    generateMULTINode_java(outputFile, P_1);
                }
                outputFile.Close();
            }
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


    private static void generateNode_java(OutputFile P_0)
    {
        TextWriter printWriter = P_0.getPrintWriter();
        GeneratePrologue(printWriter);
        printWriter.WriteLine("/* All AST nodes must implement this interface.  It provides basic");
        printWriter.WriteLine("   machinery for constructing the parent and child relationships");
        printWriter.WriteLine("   between nodes. */");
        printWriter.WriteLine("");
        printWriter.WriteLine("public interface Node {");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** This method is called after the node has been made the current");
        printWriter.WriteLine("    node.  It indicates that child nodes can now be added to it. */");
        printWriter.WriteLine("  public void jjtOpen();");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** This method is called after all the child nodes have been");
        printWriter.WriteLine("    added. */");
        printWriter.WriteLine("  public void jjtClose();");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** This pair of methods are used to inform the node of its");
        printWriter.WriteLine("    parent. */");
        printWriter.WriteLine("  public void jjtSetParent(Node n);");
        printWriter.WriteLine("  public Node jjtGetParent();");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** This method tells the node to Add its argument to the node's");
        printWriter.WriteLine("    list of children.  */");
        printWriter.WriteLine("  public void jjtAddChild(Node n, int i);");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** This method returns a child node.  The children are numbered");
        printWriter.WriteLine("     from zero, left to right. */");
        printWriter.WriteLine("  public Node jjtGetChild(int i);");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /** Return the number of children the node has. */");
        printWriter.WriteLine("  public int jjtGetNumChildren();");
        if (JJTreeOptions.Visitor)
        {
            string str = "Object";
            if (!string.Equals(JJTreeOptions.VisitorDataType, ""))
            {
                str = JJTreeOptions.VisitorDataType;
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Accept the visitor. **/");
            printWriter.WriteLine(("  public Object jjtAccept(") + (visitorClass()) + (" visitor, ")
                + (str)
                + (" data)")
                + (mergeVisitorException())
                + (";")
                );
        }
        printWriter.WriteLine("}");
        printWriter.Close();
    }


    private static void generateSimpleNode_java(OutputFile P_0)
    {
        TextWriter printWriter = P_0.getPrintWriter();
        GeneratePrologue(printWriter);
        printWriter.Write("public class SimpleNode");
        if (!string.Equals(JJTreeOptions.NodeExtends, ""))
        {
            printWriter.Write((" extends ") + (JJTreeOptions.NodeExtends));
        }
        printWriter.WriteLine(" implements Node {");
        printWriter.WriteLine("  protected Node parent;");
        printWriter.WriteLine("  protected Node[] children;");
        printWriter.WriteLine("  protected int id;");
        printWriter.WriteLine("  protected Object value;");
        printWriter.WriteLine(("  protected ") + (JJTreeGlobals.ParserName) + (" parser;")
            );
        if (JJTreeOptions.TrackTokens)
        {
            printWriter.WriteLine("  protected Token firstToken;");
            printWriter.WriteLine("  protected Token lastToken;");
        }
        printWriter.WriteLine("");
        printWriter.WriteLine("  public SimpleNode(int i) {");
        printWriter.WriteLine("    id = i;");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        printWriter.WriteLine(("  public SimpleNode(") + (JJTreeGlobals.ParserName) + (" p, int i) {")
            );
        printWriter.WriteLine("    this(i);");
        printWriter.WriteLine("    parser = p;");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        if ((JJTreeOptions.NodeFactory.Length) > 0)
        {
            printWriter.WriteLine("  public static Node jjtCreate(int id) {");
            printWriter.WriteLine("    return new SimpleNode(id);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine(("  public static Node jjtCreate(") + (JJTreeGlobals.ParserName) + (" p, int id) {")
                );
            printWriter.WriteLine("    return new SimpleNode(p, id);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
        }
        printWriter.WriteLine("  public void jjtOpen() {");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public void jjtClose() {");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("  ");
        printWriter.WriteLine("  public void jjtSetParent(Node n) { parent = n; }");
        printWriter.WriteLine("  public Node jjtGetParent() { return parent; }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public void jjtAddChild(Node n, int i) {");
        printWriter.WriteLine("    if (children == null) {");
        printWriter.WriteLine("      children = new Node[i + 1];");
        printWriter.WriteLine("    } else if (i >= children.length) {");
        printWriter.WriteLine("      Node c[] = new Node[i + 1];");
        printWriter.WriteLine("      System.arraycopy(children, 0, c, 0, children.length);");
        printWriter.WriteLine("      children = c;");
        printWriter.WriteLine("    }");
        printWriter.WriteLine("    children[i] = n;");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public Node jjtGetChild(int i) {");
        printWriter.WriteLine("    return children[i];");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public int jjtGetNumChildren() {");
        printWriter.WriteLine("    return (children == null) ? 0 : children.length;");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public void jjtSetValue(Object value) { this.value = value; }");
        printWriter.WriteLine("  public Object jjtGetValue() { return value; }");
        printWriter.WriteLine("");
        if (JJTreeOptions.TrackTokens)
        {
            printWriter.WriteLine("  public Token jjtGetFirstToken() { return firstToken; }");
            printWriter.WriteLine("  public void jjtSetFirstToken(Token token) { this.firstToken = token; }");
            printWriter.WriteLine("  public Token jjtGetLastToken() { return lastToken; }");
            printWriter.WriteLine("  public void jjtSetLastToken(Token token) { this.lastToken = token; }");
            printWriter.WriteLine("");
        }
        if (JJTreeOptions.Visitor)
        {
            string str = mergeVisitorException();
            string str2 = "Object";
            if (!string.Equals(JJTreeOptions.VisitorDataType, ""))
            {
                str2 = JJTreeOptions.VisitorDataType;
            }
            printWriter.WriteLine("  /** Accept the visitor. **/");
            printWriter.WriteLine(("  public Object jjtAccept(") + (visitorClass()) + (" visitor, ")
                + (str2)
                + (" data)")
                + (str)
                + (" {")
                );
            printWriter.WriteLine("    return visitor.visit(this, data);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Accept the visitor. **/");
            printWriter.WriteLine(("  public Object childrenAccept(") + (visitorClass()) + (" visitor, ")
                + (str2)
                + (" data)")
                + (str)
                + (" {")
                );
            printWriter.WriteLine("    if (children != null) {");
            printWriter.WriteLine("      for (int i = 0; i < children.length; ++i) {");
            printWriter.WriteLine("        children[i].jjtAccept(visitor, data);");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("    }");
            printWriter.WriteLine("    return data;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
        }
        printWriter.WriteLine("  /* You can override these two methods in subclasses of SimpleNode to");
        printWriter.WriteLine("     customize the way the node appears when the tree is dumped.  If");
        printWriter.WriteLine("     your output uses more than one line you should override");
        printWriter.WriteLine("     ToString(String), otherwise overriding ToString() is probably all");
        printWriter.WriteLine("     you need to do. */");
        printWriter.WriteLine("");
        printWriter.WriteLine(("  public String ToString() { return ") + (NodeConstants) + (".jjtNodeName[id]; }")
            );
        printWriter.WriteLine("  public String ToString(String prefix) { return prefix + ToString(); }");
        printWriter.WriteLine("");
        printWriter.WriteLine("  /* Override this method if you want to customize how the node dumps");
        printWriter.WriteLine("     out its children. */");
        printWriter.WriteLine("");
        printWriter.WriteLine("  public void dump(String prefix) {");
        printWriter.WriteLine("    System.out.WriteLine(ToString(prefix));");
        printWriter.WriteLine("    if (children != null) {");
        printWriter.WriteLine("      for (int i = 0; i < children.length; ++i) {");
        printWriter.WriteLine("  SimpleNode n = (SimpleNode)children[i];");
        printWriter.WriteLine("  if (n != null) {");
        printWriter.WriteLine("    n.dump(prefix + \" \");");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("      }");
        printWriter.WriteLine("    }");
        printWriter.WriteLine("  }");
        printWriter.WriteLine("}");
        printWriter.WriteLine("");
    }


    private static void generateMULTINode_java(OutputFile P_0, string P_1)
    {
        TextWriter printWriter = P_0.getPrintWriter();
        GeneratePrologue(printWriter);
        if ((JJTreeOptions.NodeClass.Length) > 0)
        {
            printWriter.WriteLine(("public class ") + (P_1) + (" extends ")
                + (JJTreeOptions.NodeClass)
                + ("{")
                );
        }
        else
        {
            printWriter.WriteLine(("public class ") + (P_1) + (" extends SimpleNode {")
                );
        }
        printWriter.WriteLine(("  public ") + (P_1) + ("(int id) {")
            );
        printWriter.WriteLine("    super(id);");
        printWriter.WriteLine("  }");
        printWriter.WriteLine();
        printWriter.WriteLine(("  public ") + (P_1) + ("(")
            + (JJTreeGlobals.ParserName)
            + (" p, int id) {")
            );
        printWriter.WriteLine("    super(p, id);");
        printWriter.WriteLine("  }");
        printWriter.WriteLine();
        if ((JJTreeOptions.NodeFactory.Length) > 0)
        {
            printWriter.WriteLine("  public static Node jjtCreate(int id) {");
            printWriter.WriteLine(("      return new ") + (P_1) + ("(id);")
                );
            printWriter.WriteLine("  }");
            printWriter.WriteLine();
            printWriter.WriteLine(("  public static Node jjtCreate(") + (JJTreeGlobals.ParserName) + (" p, int id) {")
                );
            printWriter.WriteLine(("      return new ") + (P_1) + ("(p, id);")
                );
            printWriter.WriteLine("  }");
        }
        if (JJTreeOptions.Visitor)
        {
            string str = "Object";
            if (!string.Equals(JJTreeOptions.VisitorDataType, ""))
            {
                str = JJTreeOptions.VisitorDataType;
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Accept the visitor. **/");
            printWriter.WriteLine(("  public Object jjtAccept(") + (visitorClass()) + (" visitor, ")
                + (str)
                + (" data)")
                + (mergeVisitorException())
                + (" {")
                );
            printWriter.WriteLine("    return visitor.visit(this, data);");
            printWriter.WriteLine("  }");
        }
        printWriter.WriteLine("}");
        printWriter.Close();
    }


    internal static string visitorClass()
    {
        return (JJTreeGlobals.ParserName) + ("Visitor");
    }


    private static string mergeVisitorException()
    {
        string text = JJTreeOptions.VisitorException;
        if (!string.Equals("", text))
        {
            text = (" throws ") + (text);
        }
        return text;
    }


    private NodeFiles()
    {
    }

    static NodeFiles()
    {
        nodesGenerated = new();
    }
}
