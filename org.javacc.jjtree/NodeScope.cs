using System.Collections;
using org.javacc.parser;
namespace org.javacc.jjtree;

public class NodeScope
{
	private ASTProduction Production;
	private ASTNodeDescriptor nodeDescriptor;
	private string ClosedVar = "";
	private string ExceptionVar = "";
	private string NodeVar = "";
	private int ScopeNumber = 0;

	
	internal static NodeScope GetEnclosingNodeScope(Node _node)
	{
		if (_node is ASTBNFDeclaration n)
		{
			return n.nodeScope;
		}
		for (var node = _node.jjtGetParent(); node != null; node = node.jjtGetParent())
		{
            switch (node)
            {
                case ASTBNFDeclaration a:
                    return a.nodeScope;
                case ASTBNFNodeScope b:
                    return b.nodeScope;
                case ASTExpansionNodeScope c:
                    return c.nodeScope;
            }
        }
		return null;
	}

    internal virtual string NodeVariable => NodeVar;


    internal virtual bool IsVoid => nodeDescriptor.IsVoid;


    internal virtual void insertCloseNodeAction(IO io, string text)
	{
		io.WriteLine((text)+("{"));
		insertCloseNodeCode(io, (text)+("  "), false);
		io.WriteLine((text)+("}"));
	}


    internal virtual string NodeDescriptorText => nodeDescriptor.Descriptor;


    internal virtual void insertOpenNodeCode(IO io, string P_1)
	{
		string nodeType = nodeDescriptor.NodeType;
		string str = (((JJTreeOptions.NodeClass.Length) <= 0 || JJTreeOptions.Multi) ? nodeType : JJTreeOptions.NodeClass);
		NodeFiles.ensure(io, nodeType);
		io.Write((P_1)+(str)+(" ")
			+(NodeVar)
			+(" = ")
			);
		string str2 = ((!Options.getStatic()) ? "this" : "null");
		string str3 = ((!JJTreeOptions.NodeUsesParser) ? "" : (str2)+(", "));
		if (string.Equals(JJTreeOptions.NodeFactory, "*"))
		{
			io.WriteLine(("(")+(str)+(")")
				+(str)
				+(".jjtCreate(")
				+(str3)
				+(nodeDescriptor.GetNodeId())
				+(");")
				);
		}
		else if ((JJTreeOptions.NodeFactory.Length) > 0)
		{
			io.WriteLine(("(")+(str)+(")")
				+(JJTreeOptions.NodeFactory)
				+(".jjtCreate(")
				+(str3)
				+(nodeDescriptor.GetNodeId())
				+(");")
				);
		}
		else
		{
			io.WriteLine(("new ")+(str)+("(")
				+(str3)
				+(nodeDescriptor.GetNodeId())
				+(");")
				);
		}
		if (usesCloseNodeVar())
		{
			io.WriteLine((P_1)+("boolean ")+(ClosedVar)
				+(" = true;")
				);
		}
		io.WriteLine((P_1)+(nodeDescriptor.OpenNode(NodeVar)));
		if (JJTreeOptions.NodeScopeHook)
		{
			io.WriteLine((P_1)+("jjtreeOpenNodeScope(")+(NodeVar)
				+(");")
				);
		}
		if (JJTreeOptions.TrackTokens)
		{
			io.WriteLine((P_1)+(NodeVar)+(".jjtSetFirstToken(getToken(1));")
				);
		}
	}

    internal virtual ASTNodeDescriptor NodeDescriptor => nodeDescriptor;


    internal virtual void tryExpansionUnit(IO P_0, string P_1, JJTreeNode P_2)
	{
		P_0.WriteLine((P_1)+("try {"));
		JJTreeNode.CloseJJTreeComment(P_0);
		P_2.Write(P_0);
		JJTreeNode.OpenJJTreeComment(P_0, null);
		P_0.WriteLine();
		var hashtable = new Hashtable();
		findThrown(hashtable, P_2);
		var enumeration = hashtable.GetEnumerator();
		insertCatchBlocks(P_0, enumeration, P_1);
		P_0.WriteLine((P_1)+("} finally {"));
		if (usesCloseNodeVar())
		{
			P_0.WriteLine((P_1)+("  if (")+(ClosedVar)
				+(") {")
				);
			insertCloseNodeCode(P_0, (P_1)+("    "), true);
			P_0.WriteLine((P_1)+("  }"));
		}
		P_0.WriteLine((P_1)+("}"));
		JJTreeNode.CloseJJTreeComment(P_0);
	}

	
	internal virtual void insertOpenNodeAction(IO P_0, string P_1)
	{
		P_0.WriteLine((P_1)+("{"));
		insertOpenNodeCode(P_0, (P_1)+("  "));
		P_0.WriteLine((P_1)+("}"));
	}

	
	internal virtual void tryTokenSequence(IO io, string P_1, Token P_2, Token P_3)
	{
		io.WriteLine((P_1)+("try {"));
		JJTreeNode.CloseJJTreeComment(io);
		for (Token token = P_2; token != P_3.Next; token = token.Next)
		{
			TokenUtils.Write(token, io, "jjtThis", NodeVar);
		}
		JJTreeNode.OpenJJTreeComment(io, null);
		io.WriteLine();
		Enumeration enumeration = Production.ThrowsList.elements();
		insertCatchBlocks(io, enumeration, P_1);
		io.WriteLine((P_1)+("} finally {"));
		if (usesCloseNodeVar())
		{
			io.WriteLine((P_1)+("  if (")+(ClosedVar)
				+(") {")
				);
			insertCloseNodeCode(io, (P_1)+("    "), true);
			io.WriteLine((P_1)+("  }"));
		}
		io.WriteLine((P_1)+("}"));
		JJTreeNode.CloseJJTreeComment(io);
	}

	
	internal NodeScope(ASTProduction P_0, ASTNodeDescriptor P_1)
	{
		Production = P_0;
		if (P_1 == null)
		{
			string text = Production.Name;
			if (JJTreeOptions.NodeDefaultVoid)
			{
				text = "void";
			}
			nodeDescriptor = ASTNodeDescriptor.Indefinite(text);
		}
		else
		{
			nodeDescriptor = P_1;
		}
		ScopeNumber = Production.GetNodeScopeNumber(this);
		NodeVar = constructVariable("n");
		ClosedVar = constructVariable("c");
		ExceptionVar = constructVariable("e");
	}

	
	private string constructVariable(string P_0)
	{
		string @this = ("000")+(ScopeNumber);
		string result = ("jjt")+(P_0)+(String.instancehelper_substring(@this, @this.Length - 3, @this.Length))
			;
		
		return result;
	}

	internal virtual bool usesCloseNodeVar()
	{
		return true;
	}

	
	internal virtual void insertCloseNodeCode(IO P_0, string P_1, bool P_2)
	{
		P_0.WriteLine((P_1)+(nodeDescriptor.CloseNode(NodeVar)));
		if (usesCloseNodeVar() && !P_2)
		{
			P_0.WriteLine((P_1)+(ClosedVar)+(" = false;")
				);
		}
		if (JJTreeOptions.NodeScopeHook)
		{
			P_0.WriteLine((P_1)+("jjtreeCloseNodeScope(")+(NodeVar)
				+(");")
				);
		}
		if (JJTreeOptions.TrackTokens)
		{
			P_0.WriteLine((P_1)+(NodeVar)+(".jjtSetLastToken(getToken(0));")
				);
		}
	}

	
	private void insertCatchBlocks(IO P_0, IEnumerator P_1, string P_2)
	{
		if (P_1.hasMoreElements())
		{
			P_0.WriteLine((P_2)+("} catch (Throwable ")+(ExceptionVar)
				+(") {")
				);
			if (usesCloseNodeVar())
			{
				P_0.WriteLine((P_2)+("  if (")+(ClosedVar)
					+(") {")
					);
				P_0.WriteLine((P_2)+("    jjtree.clearNodeScope(")+(NodeVar)
					+(");")
					);
				P_0.WriteLine((P_2)+("    ")+(ClosedVar)
					+(" = false;")
					);
				P_0.WriteLine((P_2)+("  } else {"));
				P_0.WriteLine((P_2)+("    jjtree.popNode();"));
				P_0.WriteLine((P_2)+("  }"));
			}
			while (P_1.hasMoreElements())
			{
				string str = (string)P_1.nextElement();
				P_0.WriteLine((P_2)+("  if (")+(ExceptionVar)
					+(" instanceof ")
					+(str)
					+(") {")
					);
				P_0.WriteLine((P_2)+("    throw (")+(str)
					+(")")
					+(ExceptionVar)
					+(";")
					);
				P_0.WriteLine((P_2)+("  }"));
			}
			P_0.WriteLine((P_2)+("  throw ")+(ExceptionVar)
				+(";")
				);
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
				Enumeration enumeration = aSTProduction.ThrowsList.elements();
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
