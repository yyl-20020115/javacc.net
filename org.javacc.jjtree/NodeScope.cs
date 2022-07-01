using System.Collections;
using System.Text;
using org.javacc.parser;
namespace org.javacc.jjtree;

public class NodeScope
{
	private ASTProduction production;
	private ASTNodeDescriptor node_descriptor;
	private string closedVar = "";
	private string exceptionVar = "";
	private string nodeVar = "";
	private int scopeNumber = 0;

	
	internal static NodeScope GetEnclosingNodeScope(Node _node)
	{
		if (_node is ASTBNFDeclaration n)
		{
			return n.node_scope;
		}
		for (var node = _node.jjtGetParent(); node != null; node = node.jjtGetParent())
		{
            switch (node)
            {
                case ASTBNFDeclaration a:
                    return a.node_scope;
                case ASTBNFNodeScope b:
                    return b.node_scope;
                case ASTExpansionNodeScope c:
                    return c.node_scope;
            }
        }
		return null;
	}

    internal virtual string NodeVariable => nodeVar;


    internal virtual bool IsVoid => node_descriptor.IsVoid;


    internal virtual void insertCloseNodeAction(IO io, string text)
	{
		io.WriteLine(new StringBuilder().Append(text).Append("{").ToString());
		insertCloseNodeCode(io, new StringBuilder().Append(text).Append("  ").ToString(), false);
		io.WriteLine(new StringBuilder().Append(text).Append("}").ToString());
	}

	
	internal virtual string getNodeDescriptorText()
	{
		return node_descriptor.Descriptor;
	}

	
	internal virtual void insertOpenNodeCode(IO P_0, string P_1)
	{
		string nodeType = node_descriptor.NodeType;
		string str = (((JJTreeOptions.NodeClass.Length) <= 0 || JJTreeOptions.Multi) ? nodeType : JJTreeOptions.NodeClass);
		NodeFiles.ensure(P_0, nodeType);
		P_0.Write(new StringBuilder().Append(P_1).Append(str).Append(" ")
			.Append(nodeVar)
			.Append(" = ")
			.ToString());
		string str2 = ((!Options.getStatic()) ? "this" : "null");
		string str3 = ((!JJTreeOptions.NodeUsesParser) ? "" : new StringBuilder().Append(str2).Append(", ").ToString());
		if (string.Equals(JJTreeOptions.NodeFactory, "*"))
		{
			P_0.WriteLine(new StringBuilder().Append("(").Append(str).Append(")")
				.Append(str)
				.Append(".jjtCreate(")
				.Append(str3)
				.Append(node_descriptor.GetNodeId())
				.Append(");")
				.ToString());
		}
		else if ((JJTreeOptions.NodeFactory.Length) > 0)
		{
			P_0.WriteLine(new StringBuilder().Append("(").Append(str).Append(")")
				.Append(JJTreeOptions.NodeFactory)
				.Append(".jjtCreate(")
				.Append(str3)
				.Append(node_descriptor.GetNodeId())
				.Append(");")
				.ToString());
		}
		else
		{
			P_0.WriteLine(new StringBuilder().Append("new ").Append(str).Append("(")
				.Append(str3)
				.Append(node_descriptor.GetNodeId())
				.Append(");")
				.ToString());
		}
		if (usesCloseNodeVar())
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("boolean ").Append(closedVar)
				.Append(" = true;")
				.ToString());
		}
		P_0.WriteLine(new StringBuilder().Append(P_1).Append(node_descriptor.OpenNode(nodeVar)).ToString());
		if (JJTreeOptions.NodeScopeHook)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("jjtreeOpenNodeScope(").Append(nodeVar)
				.Append(");")
				.ToString());
		}
		if (JJTreeOptions.TrackTokens)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(nodeVar).Append(".jjtSetFirstToken(getToken(1));")
				.ToString());
		}
	}

    internal virtual ASTNodeDescriptor getNodeDescriptor() => node_descriptor;


    internal virtual void tryExpansionUnit(IO P_0, string P_1, JJTreeNode P_2)
	{
		P_0.WriteLine(new StringBuilder().Append(P_1).Append("try {").ToString());
		JJTreeNode.CloseJJTreeComment(P_0);
		P_2.Write(P_0);
		JJTreeNode.OpenJJTreeComment(P_0, null);
		P_0.WriteLine();
		var hashtable = new Hashtable();
		findThrown(hashtable, P_2);
		var enumeration = hashtable.GetEnumerator();
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
		JJTreeNode.CloseJJTreeComment(P_0);
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
		JJTreeNode.CloseJJTreeComment(P_0);
		for (Token token = P_2; token != P_3.Next; token = token.Next)
		{
			TokenUtils.Write(token, P_0, "jjtThis", nodeVar);
		}
		JJTreeNode.OpenJJTreeComment(P_0, null);
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
		JJTreeNode.CloseJJTreeComment(P_0);
	}

	
	internal NodeScope(ASTProduction P_0, ASTNodeDescriptor P_1)
	{
		production = P_0;
		if (P_1 == null)
		{
			string text = production.name;
			if (JJTreeOptions.NodeDefaultVoid)
			{
				text = "void";
			}
			node_descriptor = ASTNodeDescriptor.Indefinite(text);
		}
		else
		{
			node_descriptor = P_1;
		}
		scopeNumber = production.GetNodeScopeNumber(this);
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
		P_0.WriteLine(new StringBuilder().Append(P_1).Append(node_descriptor.CloseNode(nodeVar)).ToString());
		if (usesCloseNodeVar() && !P_2)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(closedVar).Append(" = false;")
				.ToString());
		}
		if (JJTreeOptions.NodeScopeHook)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append("jjtreeCloseNodeScope(").Append(nodeVar)
				.Append(");")
				.ToString());
		}
		if (JJTreeOptions.TrackTokens)
		{
			P_0.WriteLine(new StringBuilder().Append(P_1).Append(nodeVar).Append(".jjtSetLastToken(getToken(0));")
				.ToString());
		}
	}

	
	private void insertCatchBlocks(IO P_0, IEnumerator P_1, string P_2)
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
			P_0.WriteLine(new StringBuilder().Append(P_2).Append("  throw ").Append(exceptionVar)
				.Append(";")
				.ToString());
		}
	}

	
	private static void findThrown(Hashtable P_0, JJTreeNode P_1)
	{
		if (P_1 is ASTBNFNonTerminal)
		{
			string image = P_1.FirstToken.Image;
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
