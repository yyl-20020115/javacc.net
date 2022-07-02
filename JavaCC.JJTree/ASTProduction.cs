using System.Collections.Generic;
namespace Javacc.JJTree;

public class ASTProduction : JJTreeNode
{
	internal string Name = "";
	internal List<string> ThrowsList = new();
	private Dictionary<NodeScope,int> Scopes = new();
	private int nextNodeScopeNumber = 0;

	internal ASTProduction(int id) : base(id) { }
	
	internal virtual int GetNodeScopeNumber(NodeScope scope)
	{
		if (!Scopes.TryGetValue(scope,out var integer))
		{
			integer = nextNodeScopeNumber++;
			Scopes.Add(scope, integer);
		}
		return integer;
	}
}
