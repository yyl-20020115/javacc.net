using System.Collections;
using System.Text;

namespace org.javacc.jjtree;

public class ASTNodeDescriptor : JJTreeNode
{
	private bool faked = false;
	internal static ArrayList nodeIds;
	internal static ArrayList nodeNames;
	internal static Hashtable nodeSeen;
	internal string name;
	internal bool isGT;
	internal ASTNodeDescriptorExpression expression;


    internal virtual string Descriptor
    {
        get
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
    }

    internal ASTNodeDescriptor(int P_0)
		: base(P_0)
	{
		faked = false;
	}


	internal virtual void SetNodeId()
	{
		string nodeId = GetNodeId();
		if (!nodeSeen.ContainsKey(nodeId))
		{
			nodeSeen.Add(nodeId, nodeId);
			nodeNames.Add(name);
			nodeIds.Add(nodeId);
		}
	}


	internal virtual string GetNodeId()
	{
		return $"JJT{name.ToUpper().Replace('.', '_')}";
	}

	private string expression_text()
	{
		if (string.Equals(expression.FirstToken.image, ")") && string.Equals(expression.LastToken.image, "("))
		{
			return "true";
		}
		string text = "";
		Token token = expression.FirstToken;
		while (true)
		{
			text = new StringBuilder().Append(text).Append(" ").Append(token.image)
				.ToString();
			if (token == expression.LastToken)
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
		aSTNodeDescriptor.SetNodeId();
		aSTNodeDescriptor.faked = true;
		return aSTNodeDescriptor;
	}

    internal static ArrayList NodeIds => nodeIds;

    internal static ArrayList NodeNames => nodeNames;


    internal virtual bool IsVoid => string.Equals(name, "void");


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
		if (JJTreeOptions.Multi)
		{
			string result = new StringBuilder().Append(JJTreeOptions.NodePrefix).Append(name).ToString();
			
			return result;
		}
		return "SimpleNode";
	}

    internal virtual string getNodeName() => name;


    internal virtual string openNode(string P_0) => "jjtree.openNodeScope(" + (P_0) + (");");


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

	
	internal override string TranslateImage(Token P_0)
	{
		return WhiteOut(P_0);
	}

	static ASTNodeDescriptor()
	{
		nodeIds = new ArrayList();
		nodeNames = new ArrayList();
		nodeSeen = new Hashtable();
	}
}
