using System.Collections;
using System.Collections.Generic;
namespace org.javacc.jjtree;

public class ASTProduction : JJTreeNode
{
	internal string name = "";
	internal ArrayList throws_list = new();
	private Dictionary<NodeScope,int> scopes = new();
	private int nextNodeScopeNumber = 0;

	internal ASTProduction(int id) : base(id) { }
	
	internal virtual int GetNodeScopeNumber(NodeScope scope)
	{
		if (!scopes.TryGetValue(scope,out var integer))
		{
			integer = nextNodeScopeNumber++;
			scopes.Add(scope, integer);
		}
		return integer;
	}
}
