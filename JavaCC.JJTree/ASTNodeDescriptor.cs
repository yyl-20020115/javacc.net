namespace JavaCC.JJTree;
using System.Collections.Generic;

public class ASTNodeDescriptor : JJTreeNode
{
    private bool Faked = false;
    internal static List<string> nodeIds = new();
    internal static List<string> nodeNames = new();
    internal static Dictionary<string, string> nodeSeen = new();
    internal string Name = "";
    internal bool isGT = false;
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
            text += (" ") + (token.Image);
            if (token == expression.LastToken) break;
            token = token.Next;
        }
        return text;
    }


    internal static ASTNodeDescriptor Indefinite(string name)
    {
        var aSTNodeDescriptor = new ASTNodeDescriptor(39)
        {
            Name = name,
            Faked = true
        };
        aSTNodeDescriptor.SetNodeId();
        return aSTNodeDescriptor;
    }

    internal static List<string> NodeIds => nodeIds;
    internal static List<string> NodeNames => nodeNames;
    internal virtual bool IsVoid => string.Equals(Name, "void");
    public override string ToString() => Faked ? "(faked) " + Name : base.ToString() + ": " + Name;

    internal virtual string NodeType => JJTreeOptions.Multi ? JJTreeOptions.NodePrefix + Name : "SimpleNode";

    internal virtual string NodeName => Name;


    internal virtual string OpenNode(string name) => "jjtree.openNodeScope(" + (name) + (");");


    internal virtual string CloseNode(string name) => expression switch
    {
        null => "jjtree.closeNodeScope(" + name + ", true);",
        _ => isGT
                            ? "jjtree.closeNodeScope(" + name + ", jjtree.nodeArity() >"
                                + ExpressionText()
                                + ");"
                            : "jjtree.closeNodeScope(" + name + ", "
                            + ExpressionText()
                            + ");",
    };


    internal override string TranslateImage(Token token) => WhiteOut(token);
}
