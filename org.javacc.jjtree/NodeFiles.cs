using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjtree;


internal sealed class NodeFiles
{
	internal const string nodeVersion = "4.1";
	internal static HashSet<Node> nodesGenerated = new();

	internal static string nodeConstants()
	{
		string result = new StringBuilder().Append(JJTreeGlobals.parserName).Append("TreeConstants").ToString();
		
		return result;
	}

	
	internal static void generateTreeConstants_java()
	{
		string str = nodeConstants();
		
		FileInfo f = new FileInfo(
			Path.Combine(JJTreeOptions.getJJTreeOutputDirectory().DirectoryName, new StringBuilder().Append(str).Append(".java").ToString()));
		IOException ex;
		try
		{
			OutputFile outputFile = new OutputFile(f);
			TextWriter printWriter = outputFile.getPrintWriter();
			ArrayList nodeIds = ASTNodeDescriptor.getNodeIds();
			ArrayList nodeNames = ASTNodeDescriptor.getNodeNames();
			generatePrologue(printWriter);
			printWriter.WriteLine(new StringBuilder().Append("public interface ").Append(str).ToString());
			printWriter.WriteLine("{");
			for (int i = 0; i < nodeIds.Count; i++)
			{
				string str2 = (string)nodeIds[i];
				printWriter.WriteLine(new StringBuilder().Append("  public int ").Append(str2).Append(" = ")
					.Append(i)
					.Append(";")
					.ToString());
			}
			printWriter.WriteLine();
			printWriter.WriteLine();
			printWriter.WriteLine("  public String[] jjtNodeName = {");
			for (int i = 0; i < nodeNames.Count; i++)
			{
				string str2 = (string)nodeNames[i];
				printWriter.WriteLine(new StringBuilder().Append("    \"").Append(str2).Append("\",")
					.ToString());
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

	
	internal static void generateVisitor_java()
	{
		if (!JJTreeOptions.getVisitor())
		{
			return;
		}
		string str = visitorClass();
		
		FileInfo f = new FileInfo(JJTreeOptions.getJJTreeOutputDirectory(), new StringBuilder().Append(str).Append(".java").ToString());
		IOException ex;
		try
		{
			OutputFile outputFile = new OutputFile(f);
			TextWriter printWriter = outputFile.getPrintWriter();
			ArrayList nodeNames = ASTNodeDescriptor.getNodeNames();
			generatePrologue(printWriter);
			printWriter.WriteLine(new StringBuilder().Append("public interface ").Append(str).ToString());
			printWriter.WriteLine("{");
			string str2 = mergeVisitorException();
			string str3 = "Object";
			if (!string.Equals(JJTreeOptions.getVisitorDataType(), ""))
			{
				str3 = JJTreeOptions.getVisitorDataType();
			}
			printWriter.WriteLine(new StringBuilder().Append("  public Object visit(SimpleNode node, ").Append(str3).Append(" data)")
				.Append(str2)
				.Append(";")
				.ToString());
			if (JJTreeOptions.getMulti())
			{
				for (int i = 0; i < nodeNames.Count; i++)
				{
					string text = (string)nodeNames[i];
					if (!string.Equals(text, "void"))
					{
						string str4 = new StringBuilder().Append(JJTreeOptions.getNodePrefix()).Append(text).ToString();
						printWriter.WriteLine(new StringBuilder().Append("  public Object visit(").Append(str4).Append(" node, ")
							.Append(str3)
							.Append(" data)")
							.Append(str2)
							.Append(";")
							.ToString());
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
		string message = (@this);
		
		throw new System.Exception(message);
	}

	
	internal static void generatePrologue(TextWriter P_0)
	{
		if (!string.Equals(JJTreeGlobals.nodePackageName, ""))
		{
			P_0.WriteLine(new StringBuilder().Append("package ").Append(JJTreeGlobals.nodePackageName).Append(";")
				.ToString());
			P_0.WriteLine();
			if (!string.Equals(JJTreeGlobals.nodePackageName, JJTreeGlobals.packageName))
			{
				P_0.WriteLine(new StringBuilder().Append("import ").Append(JJTreeGlobals.packageName).Append(".*;")
					.ToString());
				P_0.WriteLine();
			}
		}
	}

	
	internal static void ensure(IO P_0, string P_1)
	{
		
		File file = new File(JJTreeOptions.getJJTreeOutputDirectory(), new StringBuilder().Append(P_1).Append(".java").ToString());
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
		if ((!string.Equals(P_1, "Node") && !JJTreeOptions.getBuildNodeFiles()) || (file.Exists && nodesGenerated.Contains(file.getName())))
		{
			return;
		}
		IOException ex;
		try
		{
			string[] strarr = new string[7] { "MULTI", "NODE_USES_PARSER", "VISITOR", "TRACK_TOKENS", "NODE_PREFIX", "NODE_EXTENDS", "NODE_FACTORY" };
			OutputFile outputFile = new OutputFile(file, "4.1", strarr);
			outputFile.setToolName("JJTree");
			nodesGenerated.Add(file.getName());
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
		generatePrologue(printWriter);
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
		if (JJTreeOptions.getVisitor())
		{
			string str = "Object";
			if (!string.Equals(JJTreeOptions.getVisitorDataType(), ""))
			{
				str = JJTreeOptions.getVisitorDataType();
			}
			printWriter.WriteLine("");
			printWriter.WriteLine("  /** Accept the visitor. **/");
			printWriter.WriteLine(new StringBuilder().Append("  public Object jjtAccept(").Append(visitorClass()).Append(" visitor, ")
				.Append(str)
				.Append(" data)")
				.Append(mergeVisitorException())
				.Append(";")
				.ToString());
		}
		printWriter.WriteLine("}");
		printWriter.Close();
	}

	
		private static void generateSimpleNode_java(OutputFile P_0)
	{
		TextWriter printWriter = P_0.getPrintWriter();
		generatePrologue(printWriter);
		printWriter.Write("public class SimpleNode");
		if (!string.Equals(JJTreeOptions.getNodeExtends(), ""))
		{
			printWriter.Write(new StringBuilder().Append(" extends ").Append(JJTreeOptions.getNodeExtends()).ToString());
		}
		printWriter.WriteLine(" implements Node {");
		printWriter.WriteLine("  protected Node parent;");
		printWriter.WriteLine("  protected Node[] children;");
		printWriter.WriteLine("  protected int id;");
		printWriter.WriteLine("  protected Object value;");
		printWriter.WriteLine(new StringBuilder().Append("  protected ").Append(JJTreeGlobals.parserName).Append(" parser;")
			.ToString());
		if (JJTreeOptions.getTrackTokens())
		{
			printWriter.WriteLine("  protected Token firstToken;");
			printWriter.WriteLine("  protected Token lastToken;");
		}
		printWriter.WriteLine("");
		printWriter.WriteLine("  public SimpleNode(int i) {");
		printWriter.WriteLine("    id = i;");
		printWriter.WriteLine("  }");
		printWriter.WriteLine("");
		printWriter.WriteLine(new StringBuilder().Append("  public SimpleNode(").Append(JJTreeGlobals.parserName).Append(" p, int i) {")
			.ToString());
		printWriter.WriteLine("    this(i);");
		printWriter.WriteLine("    parser = p;");
		printWriter.WriteLine("  }");
		printWriter.WriteLine("");
		if ((JJTreeOptions.getNodeFactory().Length) > 0)
		{
			printWriter.WriteLine("  public static Node jjtCreate(int id) {");
			printWriter.WriteLine("    return new SimpleNode(id);");
			printWriter.WriteLine("  }");
			printWriter.WriteLine("");
			printWriter.WriteLine(new StringBuilder().Append("  public static Node jjtCreate(").Append(JJTreeGlobals.parserName).Append(" p, int id) {")
				.ToString());
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
		if (JJTreeOptions.getTrackTokens())
		{
			printWriter.WriteLine("  public Token jjtGetFirstToken() { return firstToken; }");
			printWriter.WriteLine("  public void jjtSetFirstToken(Token token) { this.firstToken = token; }");
			printWriter.WriteLine("  public Token jjtGetLastToken() { return lastToken; }");
			printWriter.WriteLine("  public void jjtSetLastToken(Token token) { this.lastToken = token; }");
			printWriter.WriteLine("");
		}
		if (JJTreeOptions.getVisitor())
		{
			string str = mergeVisitorException();
			string str2 = "Object";
			if (!string.Equals(JJTreeOptions.getVisitorDataType(), ""))
			{
				str2 = JJTreeOptions.getVisitorDataType();
			}
			printWriter.WriteLine("  /** Accept the visitor. **/");
			printWriter.WriteLine(new StringBuilder().Append("  public Object jjtAccept(").Append(visitorClass()).Append(" visitor, ")
				.Append(str2)
				.Append(" data)")
				.Append(str)
				.Append(" {")
				.ToString());
			printWriter.WriteLine("    return visitor.visit(this, data);");
			printWriter.WriteLine("  }");
			printWriter.WriteLine("");
			printWriter.WriteLine("  /** Accept the visitor. **/");
			printWriter.WriteLine(new StringBuilder().Append("  public Object childrenAccept(").Append(visitorClass()).Append(" visitor, ")
				.Append(str2)
				.Append(" data)")
				.Append(str)
				.Append(" {")
				.ToString());
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
		printWriter.WriteLine(new StringBuilder().Append("  public String ToString() { return ").Append(nodeConstants()).Append(".jjtNodeName[id]; }")
			.ToString());
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
		generatePrologue(printWriter);
		if ((JJTreeOptions.getNodeClass().Length) > 0)
		{
			printWriter.WriteLine(new StringBuilder().Append("public class ").Append(P_1).Append(" extends ")
				.Append(JJTreeOptions.getNodeClass())
				.Append("{")
				.ToString());
		}
		else
		{
			printWriter.WriteLine(new StringBuilder().Append("public class ").Append(P_1).Append(" extends SimpleNode {")
				.ToString());
		}
		printWriter.WriteLine(new StringBuilder().Append("  public ").Append(P_1).Append("(int id) {")
			.ToString());
		printWriter.WriteLine("    super(id);");
		printWriter.WriteLine("  }");
		printWriter.WriteLine();
		printWriter.WriteLine(new StringBuilder().Append("  public ").Append(P_1).Append("(")
			.Append(JJTreeGlobals.parserName)
			.Append(" p, int id) {")
			.ToString());
		printWriter.WriteLine("    super(p, id);");
		printWriter.WriteLine("  }");
		printWriter.WriteLine();
		if ((JJTreeOptions.getNodeFactory().Length) > 0)
		{
			printWriter.WriteLine("  public static Node jjtCreate(int id) {");
			printWriter.WriteLine(new StringBuilder().Append("      return new ").Append(P_1).Append("(id);")
				.ToString());
			printWriter.WriteLine("  }");
			printWriter.WriteLine();
			printWriter.WriteLine(new StringBuilder().Append("  public static Node jjtCreate(").Append(JJTreeGlobals.parserName).Append(" p, int id) {")
				.ToString());
			printWriter.WriteLine(new StringBuilder().Append("      return new ").Append(P_1).Append("(p, id);")
				.ToString());
			printWriter.WriteLine("  }");
		}
		if (JJTreeOptions.getVisitor())
		{
			string str = "Object";
			if (!string.Equals(JJTreeOptions.getVisitorDataType(), ""))
			{
				str = JJTreeOptions.getVisitorDataType();
			}
			printWriter.WriteLine("");
			printWriter.WriteLine("  /** Accept the visitor. **/");
			printWriter.WriteLine(new StringBuilder().Append("  public Object jjtAccept(").Append(visitorClass()).Append(" visitor, ")
				.Append(str)
				.Append(" data)")
				.Append(mergeVisitorException())
				.Append(" {")
				.ToString());
			printWriter.WriteLine("    return visitor.visit(this, data);");
			printWriter.WriteLine("  }");
		}
		printWriter.WriteLine("}");
		printWriter.Close();
	}

	
	internal static string visitorClass()
	{
		string result = new StringBuilder().Append(JJTreeGlobals.parserName).Append("Visitor").ToString();
		
		return result;
	}

	
	private static string mergeVisitorException()
	{
		string text = JJTreeOptions.getVisitorException();
		if (!string.Equals("", text))
		{
			text = new StringBuilder().Append(" throws ").Append(text).ToString();
		}
		return text;
	}

	
	private NodeFiles()
	{
	}

	static NodeFiles()
	{
		nodesGenerated = new ();
	}
}
