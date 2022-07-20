namespace JavaCC.JJTree;
using System.Collections.Generic;

public class ASTProduction : JJTreeNode
{
    public string Name { get; protected internal set; } = "";
    public readonly List<string> ThrowsList = new();
    public readonly Dictionary<NodeScope, int> Scopes = new();
    public int NextNodeScopeNumber { get; protected internal set; } = 0;
    public ASTProduction(int id) : base(id) { }

    public virtual int GetNodeScopeNumber(NodeScope scope)
    {
        if (!Scopes.TryGetValue(scope, out var i))
        {
            i = NextNodeScopeNumber++;
            Scopes.Add(scope, i);
        }
        return i;
    }
}
