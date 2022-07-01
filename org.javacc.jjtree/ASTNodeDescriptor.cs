using System.Collections;
using System.Text;
namespace org.javacc.jjtree;
public class ASTNodeDescriptor : JJTreeNode
{
	private bool Faked = false;
	internal static ArrayList nodeIds = new();
	internal static ArrayList nodeNames = new();
	internal static Hashtable nodeSeen = new();
	internal string Name = "";
	internal bool isGT =false;
	internal ASTNodeDescriptorExpression expression;

	internal virtual string Descriptor
	{
		get
		{
			if (expression == null)
			{
				return Name;
			}
			string result = ("#") + (Name) + ("(")
				+ ((!isGT) ? "" : ">")
				+ (ExpressionText())
				+ (")")
				;

			return result;
		}
	}

	internal ASTNodeDescriptor(int id)
		: base(id) { }


	internal virtual void SetNodeId()
	{
		var nodeId = GetNodeId();
		if (!nodeSeen.ContainsKey(nodeId))
		{
			nodeSeen.Add(nodeId, nodeId);
			nodeNames.Add(Name);
			nodeIds.Add(nodeId);
		}
	}


    internal virtual string GetNodeId() => $"JJT{Name.ToUpper().Replace('.', '_')}";

    private string ExpressionText()
	{
		if (string.Equals(expression.FirstToken.Image, ")") && string.Equals(expression.LastToken.Image, "("))
		{
			return "true";
		}
		var text = "";
		var token = expression.FirstToken;
		while (true)
		{
			text = new StringBuilder().Append(text).Append(" ").Append(token.Image)
				.ToString();
			if (token == expression.LastToken) break;
			token = token.Next;
		}
		return text;
	}

	
	internal static ASTNodeDescriptor Indefinite(string P_0)
	{
		var aSTNodeDescriptor = new ASTNodeDescriptor(39)
		{
			Name = P_0,
			Faked = true
        };
        aSTNodeDescriptor.SetNodeId();
		return aSTNodeDescriptor;
	}

    internal static ArrayList NodeIds => nodeIds;
    internal static ArrayList NodeNames => nodeNames;
    internal virtual bool IsVoid => string.Equals(Name, "void");
    public override string ToString() => Faked ? "(faked) " + Name : base.ToString() + ": " + Name;

    internal virtual string NodeType => JJTreeOptions.Multi ? JJTreeOptions.NodePrefix + Name : "SimpleNode";

    internal virtual string NodeName => Name;


    internal virtual string OpenNode(string P_0) => "jjtree.openNodeScope(" + (P_0) + (");");


    internal virtual string CloseNode(string P_0) => expression switch
    {
        null => "jjtree.closeNodeScope(" + P_0 + ", true);",
        _ => isGT
                            ? "jjtree.closeNodeScope(" + P_0 + ", jjtree.nodeArity() >"
                                + ExpressionText()
                                + ");"
                            : "jjtree.closeNodeScope(" + P_0 + ", "
                            + ExpressionText()
                            + ");",
    };


    internal override string TranslateImage(Token P_0) => WhiteOut(P_0);

    static ASTNodeDescriptor()
	{
		nodeIds = new ArrayList();
		nodeNames = new ArrayList();
		nodeSeen = new Hashtable();
	}
}
