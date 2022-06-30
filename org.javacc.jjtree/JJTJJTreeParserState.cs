using System.Collections;

namespace org.javacc.jjtree;


public class JJTJJTreeParserState
{
	private ArrayList nodes;

	private ArrayList marks;

	private int sp;

	private int mk;

	private bool node_created;

	
	public virtual Node popNode()
	{
		int num = sp - 1;
		sp = num;
		if (num < mk)
		{
			mk = ((int)marks.remove(marks.Count - 1)).intValue();
		}
		return (Node)nodes.remove(nodes.Count - 1);
	}

	
	public virtual void pushNode(Node n)
	{
		nodes.Add(n);
		sp++;
	}

	public virtual int nodeArity()
	{
		return sp - mk;
	}

	
	public JJTJJTreeParserState()
	{
		nodes = new ArrayList();
		marks = new ArrayList();
		sp = 0;
		mk = 0;
	}

	public virtual bool nodeCreated()
	{
		return node_created;
	}

	
	public virtual void reset()
	{
		nodes.Clear();
		marks.Clear();
		sp = 0;
		mk = 0;
	}

	
	public virtual Node rootNode()
	{
		return (Node)nodes.get(0);
	}

	
	public virtual Node peekNode()
	{
		return (Node)nodes.get(nodes.Count - 1);
	}

	
	public virtual void clearNodeScope(Node n)
	{
		while (sp > mk)
		{
			popNode();
		}
		mk = ((int)marks.remove(marks.Count - 1)).intValue();
	}

	
	public virtual void openNodeScope(Node n)
	{
		List list = marks;
		;
		list.Add(new int(mk));
		mk = sp;
		n.jjtOpen();
	}

	
	public virtual void closeNodeScope(Node n, int i)
	{
		mk = ((int)marks.remove(marks.Count - 1)).intValue();
		while (i-- > 0)
		{
			Node node = popNode();
			node.jjtSetParent(n);
			n.jjtAddChild(node, i);
		}
		n.jjtClose();
		pushNode(n);
		node_created = true;
	}

	
	public virtual void closeNodeScope(Node n, bool b)
	{
		if (b)
		{
			int i = nodeArity();
			mk = ((int)marks.remove(marks.Count - 1)).intValue();
			while (i-- > 0)
			{
				Node node = popNode();
				node.jjtSetParent(n);
				n.jjtAddChild(node, i);
			}
			n.jjtClose();
			pushNode(n);
			node_created = true;
		}
		else
		{
			mk = ((int)marks.remove(marks.Count - 1)).intValue();
			node_created = false;
		}
	}
}
