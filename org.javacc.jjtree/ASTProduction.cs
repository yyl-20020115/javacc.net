using System.Collections;
using System.Collections.Generic;

namespace org.javacc.jjtree;

public class ASTProduction : JJTreeNode
{
	internal string name;

	internal ArrayList throws_list;

	private Dictionary<NodeScope,int> scopes = new();

	private int nextNodeScopeNumber;

	
	internal ASTProduction(int P_0)
		: base(P_0)
	{
		throws_list = new ArrayList();
		nextNodeScopeNumber = 0;
	}

	
	internal virtual int getNodeScopeNumber(NodeScope P_0)
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
