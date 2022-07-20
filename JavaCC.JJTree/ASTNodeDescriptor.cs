namespace JavaCC.JJTree;
using System.Collections.Generic;

public class ASTNodeDescriptor : JJTreeNode
{
    protected bool Faked = false;
    protected static readonly List<string> _NodeIds = new();
    protected static readonly List<string> _NodeNames = new();
    protected static readonly Dictionary<string, string> _NodeSeen = new();
    public string Name { get; protected internal set; }
    public bool IsGT { get; protected internal set; } = false;
    public ASTNodeDescriptorExpression Expression { get; protected internal set; }

    public virtual string Descriptor => Expression == null ? Name : ("#") + (Name) + ("(")
                + ((!IsGT) ? "" : ">")
                + (ExpressionText())
                + (")")
                ;

    public ASTNodeDescriptor(int id)
        : base(id) { }

    public virtual void SetNodeId()
    {
        var nodeId = GetNodeId();
        if (!_NodeSeen.ContainsKey(nodeId))
        {
            _NodeSeen.Add(nodeId, nodeId);
            _NodeNames.Add(Name);
            _NodeIds.Add(nodeId);
        }
    }

    public virtual string GetNodeId() => $"JJT{Name.ToUpper().Replace('.', '_')}";

    public string ExpressionText()
    {
        if (string.Equals(Expression.FirstToken.Image, ")") && string.Equals(Expression.LastToken.Image, "("))
        {
            return "true";
        }
        var text = "";
        var token = Expression.FirstToken;
        while (true)
        {
            text += (" ") + (token.Image);
            if (token == Expression.LastToken) break;
            token = token.Next;
        }
        return text;
    }


    public static ASTNodeDescriptor Indefinite(string name)
    {
        var aSTNodeDescriptor = new ASTNodeDescriptor(39)
        {
            Name = name,
            Faked = true
        };
        aSTNodeDescriptor.SetNodeId();
        return aSTNodeDescriptor;
    }

    public static List<string> NodeIds => _NodeIds;
    public static List<string> NodeNames => _NodeNames;
    public virtual bool IsVoid => string.Equals(Name, "void");
    public override string ToString() => Faked ? "(faked) " + Name : base.ToString() + ": " + Name;
    public virtual string NodeType => JJTreeOptions.Multi ? JJTreeOptions.NodePrefix + Name : "SimpleNode";
    public virtual string NodeName => Name;
    public virtual string OpenNode(string name) => "jjtree.openNodeScope(" + (name) + (");");

    public virtual string CloseNode(string name) => Expression switch
    {
        null => "jjtree.closeNodeScope(" + name + ", true);",
        _ => IsGT
                            ? "jjtree.closeNodeScope(" + name + ", jjtree.nodeArity() >"
                                + ExpressionText()
                                + ");"
                            : "jjtree.closeNodeScope(" + name + ", "
                            + ExpressionText()
                            + ");",
    };


    public override string TranslateImage(Token token) => WhiteOut(token);
}
