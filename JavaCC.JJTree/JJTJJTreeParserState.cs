using System.Collections.Generic;
namespace JavaCC.JJTree;

public class JJTJJTreeParserState
{
	private List<Node> Nodes = new();

	private List<int> Marks = new();

	private int SP = 0;

	private int MK = 0;

	private bool _NodeCreated = false;

	
	public virtual Node PopNode()
	{
		int num = SP - 1;
		SP = num;
		if (num < MK)
		{
			MK = Marks[Marks.Count - 1];
			
			Marks.RemoveAt(Marks.Count - 1);
		}
		var n = Nodes[Nodes.Count - 1];
		Nodes.RemoveAt(Nodes.Count - 1);
		return n;
	}

	
	public virtual void PushNode(Node n)
	{
		Nodes.Add(n);
		SP++;
	}

    public virtual int NodeArity => SP - MK;


    public JJTJJTreeParserState()
	{
	}

    public virtual bool NodeCreated => _NodeCreated;


    public virtual void Reset()
	{
		Nodes.Clear();
		Marks.Clear();
		SP = 0;
		MK = 0;
	}


    public virtual Node RootNode => Nodes[0];


    public virtual Node PeekNode => Nodes[(Nodes.Count - 1)];


    public virtual void ClearNodeScope(Node n)
	{
		while (SP > MK)
		{
			PopNode();
		}
		MK = Marks[Marks.Count - 1];
		Marks.RemoveAt(Marks.Count - 1);
	}

	
	public virtual void OpenNodeScope(Node n)
	{
		var list = Marks;
		list.Add((MK));
		MK = SP;
		n.jjtOpen();
	}

	
	public virtual void CloseNodeScope(Node n, int i)
	{
		MK = Marks[Marks.Count - 1];
		Marks.RemoveAt(Marks.Count - 1);
		while (i-- > 0)
		{
			Node node = PopNode();
			node.jjtSetParent(n);
			n.jjtAddChild(node, i);
		}
		n.jjtClose();
		PushNode(n);
		_NodeCreated = true;
	}

	
	public virtual void CloseNodeScope(Node n, bool b)
	{
		if (b)
		{
			int i = NodeArity;
			MK = Marks[Marks.Count - 1];
			Marks.RemoveAt(Marks.Count - 1);
			while (i-- > 0)
			{
				Node node = PopNode();
				node.jjtSetParent(n);
				n.jjtAddChild(node, i);
			}
			n.jjtClose();
			PushNode(n);
			_NodeCreated = true;
		}
		else
		{
			MK = Marks[Marks.Count - 1];
			Marks.RemoveAt(Marks.Count - 1);
			_NodeCreated = false;
		}
	}
}
