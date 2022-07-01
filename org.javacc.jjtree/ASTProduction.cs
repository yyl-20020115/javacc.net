using System.Collections;
using System.Collections.Generic;

namespace org.javacc.jjtree;

public class ASTProduction : JJTreeNode
{
	internal string name = "";
	internal ArrayList throws_list = new();
	private Dictionary<NodeScope,int> scopes = new();
	private int nextNodeScopeNumber = 0;

	internal ASTProduction(int P_0)
		: base(P_0) { }
	
	internal virtual int GetNodeScopeNumber(NodeScope P_0)
	{
		if (!scopes.TryGetValue(P_0,out var integer))
		{
			int num = nextNodeScopeNumber;
			nextNodeScopeNumber = num + 1;
			integer = (num);
			scopes.Add(P_0, integer);
		}
		int result = integer;
		
		return result;
	}
}
