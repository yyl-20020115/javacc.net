using System.Text;
using org.javacc.parser;
namespace org.javacc.jjtree;


public class NodeScope
{
	private ASTProduction production;

	private ASTNodeDescriptor node_descriptor;

	private string closedVar;

	private string exceptionVar;

	private string nodeVar;

	private int scopeNumber;

	
	internal static NodeScope getEnclosingNodeScope(Node P_0)
	{
		if (P_0 is ASTBNFDeclaration)
		{
			return ((ASTBNFDeclaration)P_0).node_scope;
		}
		for (Node node = P_0.jjtGetParent(); node != null; node = node.jjtGetParent())
		{
			if (node is ASTBNFDeclaration)
			{
				return ((ASTBNFDeclaration)node).node_scope;
			}
			if (node is ASTBNFNodeScope)
			{
				return ((ASTBNFNodeScope)node).node_scope;
			}
			if (node is ASTExpansionNodeScope)
			{
				return ((ASTExpansionNodeScope)node).node_scope;
			}
		}
		return null;
	}

	internal virtual string getNodeVariable()
	{
		return nodeVar;
	}

	
	internal virtual bool isVoid()
	{
		bool result = node_descriptor.isVoid();
		
		return result;
	}

	
	internal virtual void insertCloseNodeAction(IO P_0, string P_1)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("{").ToString());
		insertCloseNodeCode(P_0, new StringBuilder().Append(P_1).Append("  ").ToString(), false);
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("}").ToString());
	}

	
	internal virtual string getNodeDescriptorText()
	{
		string descriptor = node_descriptor.getDescriptor();
		
		return descriptor;
	}

	
	internal virtual void insertOpenNodeCode(IO P_0, string P_1)
	{
		string nodeType = node_descriptor.getNodeType();
		string str = (((JJTreeOptions.getNodeClass().Length) <= 0 || JJTreeOptions.getMulti()) ? nodeType : JJTreeOptions.getNodeClass());
		NodeFiles.ensure(P_0, nodeType);
		P_0.Write(new StringBuilder().Append(P_1).Append(str).Append(" ")
			.Append(nodeVar)
			.Append(" = ")
			.ToString());
		string str2 = ((!Options.getStatic()) ? "this" : "null");
		string str3 = ((!JJTreeOptions.getNodeUsesParser()) ? "" : new StringBuilder().Append(str2).Append(", ").ToString());
		if (string.Equals(JJTreeOptions.getNodeFactory(), "*"))
		{
			P_0.WriteLine(new StringBuilder().Append("(").Append(str).Append(")")
				.Append(str)
				.Append(".jjtCreate(")
				.Append(str3)
				.Append(node_descriptor.getNodeId())
				.Append(");")
				.ToString());
		}
		else if ((JJTreeOptions.getNodeFactory().Length) > 0)
		{
			P_0.WriteLine(new StringBuilder().Append("(").Append(str).Append(")")
				.Append(JJTreeOptions.getNodeFactory())
				.Append(".jjtCreate(")
				.Append(str3)
				.Append(node_descriptor.getNodeId())
				.Append(");")
				.ToString());
		}
		else
		{
			P_0.WriteLine(new StringBuilder().Append("new ").Append(str).Append("(")
				.Append(str3)
				.Append(node_descriptor.getNodeId())
				.Append(");")
				.ToString());
		}
		if (usesCloseNodeVar())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("boolean ").Append(closedVar)
				.Append(" = true;")
				.ToString());
		}
		P_0.WriteLine(new StringBuilder().Append(P_1).Append(node_descriptor.openNode(nodeVar)).ToString());
		if (JJTreeOptions.getNodeScopeHook())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("jjtreeOpenNodeScope(").Append(nodeVar)
				.Append(");")
				.ToString());
		}
		if (JJTreeOptions.getTrackTokens())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(nodeVar).Append(".jjtSetFirstToken(getToken(1));")
				.ToString());
		}
	}

	internal virtual ASTNodeDescriptor getNodeDescriptor()
	{
		return node_descriptor;
	}

	
	internal virtual void tryExpansionUnit(IO P_0, string P_1, JJTreeNode P_2)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("try {").ToString());
		JJTreeNode.closeJJTreeComment(P_0);
		P_2.Write(P_0);
		JJTreeNode.openJJTreeComment(P_0, null);
		P_0.WriteLine();
		var hashtable = new Hashtable();
		findThrown(hashtable, P_2);
		Enumeration enumeration = hashtable.elements();
		insertCatchBlocks(P_0, enumeration, P_1);
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("} finally {").ToString());
		if (usesCloseNodeVar())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("  if (").Append(closedVar)
				.Append(") {")
				.ToString());
			insertCloseNodeCode(P_0, new StringBuilder().Append(P_1).Append("    ").ToString(), true);
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("  }").ToString());
		}
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("}").ToString());
		JJTreeNode.closeJJTreeComment(P_0);
	}

	
	internal virtual void insertOpenNodeAction(IO P_0, string P_1)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("{").ToString());
		insertOpenNodeCode(P_0, new StringBuilder().Append(P_1).Append("  ").ToString());
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("}").ToString());
	}

	
	internal virtual void tryTokenSequence(IO P_0, string P_1, Token P_2, Token P_3)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("try {").ToString());
		JJTreeNode.closeJJTreeComment(P_0);
		for (Token token = P_2; token != P_3.next; token = token.next)
		{
			TokenUtils.Write(token, P_0, "jjtThis", nodeVar);
		}
		JJTreeNode.openJJTreeComment(P_0, null);
		P_0.WriteLine();
		Enumeration enumeration = production.throws_list.elements();
		insertCatchBlocks(P_0, enumeration, P_1);
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("} finally {").ToString());
		if (usesCloseNodeVar())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("  if (").Append(closedVar)
				.Append(") {")
				.ToString());
			insertCloseNodeCode(P_0, new StringBuilder().Append(P_1).Append("    ").ToString(), true);
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("  }").ToString());
		}
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("}").ToString());
		JJTreeNode.closeJJTreeComment(P_0);
	}

	
	internal NodeScope(ASTProduction P_0, ASTNodeDescriptor P_1)
	{
		production = P_0;
		if (P_1 == null)
		{
			string text = production.name;
			if (JJTreeOptions.getNodeDefaultVoid())
			{
				text = "void";
			}
			node_descriptor = ASTNodeDescriptor.indefinite(text);
		}
		else
		{
			node_descriptor = P_1;
		}
		scopeNumber = production.getNodeScopeNumber(this);
		nodeVar = constructVariable("n");
		closedVar = constructVariable("c");
		exceptionVar = constructVariable("e");
	}

	
	private string constructVariable(string P_0)
	{
		string @this = new StringBuilder().Append("000").Append(scopeNumber).ToString();
		string result = new StringBuilder().Append("jjt").Append(P_0).Append(String.instancehelper_substring(@this, @this.Length - 3, @this.Length))
			.ToString();
		
		return result;
	}

	internal virtual bool usesCloseNodeVar()
	{
		return true;
	}

	
	internal virtual void insertCloseNodeCode(IO P_0, string P_1, bool P_2)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append(node_descriptor.closeNode(nodeVar)).ToString());
		if (usesCloseNodeVar() && !P_2)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(closedVar).Append(" = false;")
				.ToString());
		}
		if (JJTreeOptions.getNodeScopeHook())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("jjtreeCloseNodeScope(").Append(nodeVar)
				.Append(");")
				.ToString());
		}
		if (JJTreeOptions.getTrackTokens())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(nodeVar).Append(".jjtSetLastToken(getToken(0));")
				.ToString());
		}
	}

	
	private void insertCatchBlocks(IO P_0, Enumeration P_1, string P_2)
	{
		if (P_1.hasMoreElements())
		{
			P_0.WriteLine(new StringBuilder().Append(P_2).Append("} catch (Throwable ").Append(exceptionVar)
				.Append(") {")
				.ToString());
			if (usesCloseNodeVar())
			{
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("  if (").Append(closedVar)
					.Append(") {")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("    jjtree.clearNodeScope(").Append(nodeVar)
					.Append(");")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("    ").Append(closedVar)
					.Append(" = false;")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("  } else {").ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("    jjtree.popNode();").ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("  }").ToString());
			}
			while (P_1.hasMoreElements())
			{
				string str = (string)P_1.nextElement();
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("  if (").Append(exceptionVar)
					.Append(" instanceof ")
					.Append(str)
					.Append(") {")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("    throw (").Append(str)
					.Append(")")
					.Append(exceptionVar)
					.Append(";")
					.ToString());
				P_0.WriteLine(new StringBuilder().Append(P_2).Append("  }").ToString());
			}
			P_0.WriteLine(new StringBuilder().Append(P_2).Append("  throw (Error)").Append(exceptionVar)
				.Append(";")
				.ToString());
		}
	}

	
	private static void findThrown(Hashtable P_0, JJTreeNode P_1)
	{
		if (P_1 is ASTBNFNonTerminal)
		{
			string image = P_1.getFirstToken().image;
			ASTProduction aSTProduction = (ASTProduction)JJTreeGlobals.productions.get(image);
			if (aSTProduction != null)
			{
				Enumeration enumeration = aSTProduction.throws_list.elements();
				while (enumeration.hasMoreElements())
				{
					string text = (string)enumeration.nextElement();
					P_0.Add(text, text);
				}
			}
		}
		for (int i = 0; i < P_1.jjtGetNumChildren(); i++)
		{
			JJTreeNode jJTreeNode = (JJTreeNode)P_1.jjtGetChild(i);
			findThrown(P_0, jJTreeNode);
		}
	}

	
	internal virtual void insertOpenNodeDeclaration(IO P_0, string P_1)
	{
		insertOpenNodeCode(P_0, P_1);
	}
}
