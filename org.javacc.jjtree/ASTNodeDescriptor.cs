using System.Collections;
using System.Text;

namespace org.javacc.jjtree;

public class ASTNodeDescriptor : JJTreeNode
{
	private bool faked;
	internal static ArrayList nodeIds;
	internal static ArrayList nodeNames;
	internal static Hashtable nodeSeen;
	internal string name;
	internal bool isGT;
	internal ASTNodeDescriptorExpression expression;

	
	internal virtual string getDescriptor()
	{
		if (expression == null)
		{
			return name;
		}
		string result = new StringBuilder().Append("#").Append(name).Append("(")
			.Append((!isGT) ? "" : ">")
			.Append(expression_text())
			.Append(")")
			.ToString();
		
		return result;
	}

	
	internal ASTNodeDescriptor(int P_0)
		: base(P_0)
	{
		faked = false;
	}

	
	internal virtual void setNodeIdValue()
	{
		string nodeId = getNodeId();
		if (!nodeSeen.ContainsKey(nodeId))
		{
			nodeSeen.Add(nodeId, nodeId);
			nodeNames.Add(name);
			nodeIds.Add(nodeId);
		}
	}

	
	internal virtual string getNodeId()
	{
		string result = new StringBuilder().Append("JJT").Append(name.ToUpper().Replace('.', '_')).ToString();
		
		return result;
	}

	
	private string expression_text()
	{
		if (string.Equals(expression.getFirstToken().image, ")") && string.Equals(expression.getLastToken().image, "("))
		{
			return "true";
		}
		string text = "";
		Token token = expression.getFirstToken();
		while (true)
		{
			text = new StringBuilder().Append(text).Append(" ").Append(token.image)
				.ToString();
			if (token == expression.getLastToken())
			{
				break;
			}
			token = token.next;
		}
		return text;
	}

	
	internal static ASTNodeDescriptor indefinite(string P_0)
	{
		ASTNodeDescriptor aSTNodeDescriptor = new ASTNodeDescriptor(39);
		aSTNodeDescriptor.name = P_0;
		aSTNodeDescriptor.setNodeIdValue();
		aSTNodeDescriptor.faked = true;
		return aSTNodeDescriptor;
	}

	internal static ArrayList getNodeIds()
	{
		return nodeIds;
	}

	internal static ArrayList getNodeNames()
	{
		return nodeNames;
	}

	
	internal virtual bool isVoid()
	{
		bool result = string.Equals(name, "void");
		
		return result;
	}

	
	public override string ToString()
	{
		if (faked)
		{
			string result = new StringBuilder().Append("(faked) ").Append(name).ToString();
			
			return result;
		}
		string result2 = new StringBuilder().Append(base.ToString()).Append(": ").Append(name)
			.ToString();
		
		return result2;
	}

	
	internal virtual string getNodeType()
	{
		if (JJTreeOptions.getMulti())
		{
			string result = new StringBuilder().Append(JJTreeOptions.getNodePrefix()).Append(name).ToString();
			
			return result;
		}
		return "SimpleNode";
	}

	internal virtual string getNodeName()
	{
		return name;
	}

	
	internal virtual string openNode(string P_0)
	{
		string result = new StringBuilder().Append("jjtree.openNodeScope(").Append(P_0).Append(");")
			.ToString();
		
		return result;
	}

	
	internal virtual string closeNode(string P_0)
	{
		if (expression == null)
		{
			string result = new StringBuilder().Append("jjtree.closeNodeScope(").Append(P_0).Append(", true);")
				.ToString();
			
			return result;
		}
		if (isGT)
		{
			string result2 = new StringBuilder().Append("jjtree.closeNodeScope(").Append(P_0).Append(", jjtree.nodeArity() >")
				.Append(expression_text())
				.Append(");")
				.ToString();
			
			return result2;
		}
		string result3 = new StringBuilder().Append("jjtree.closeNodeScope(").Append(P_0).Append(", ")
			.Append(expression_text())
			.Append(");")
			.ToString();
		
		return result3;
	}

	
	internal override string translateImage(Token P_0)
	{
		string result = whiteOut(P_0);
		
		return result;
	}

	static ASTNodeDescriptor()
	{
		nodeIds = new ArrayList();
		nodeNames = new ArrayList();
		nodeSeen = new Hashtable();
	}
}
