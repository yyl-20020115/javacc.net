using System;
using System.Text;
namespace org.javacc.jjtree;

public class SimpleNode: Node
{
	protected internal Node parent;

	protected internal Node[] children;

	protected internal int id;

	protected internal object value;

	protected internal JJTreeParser parser;

	public SimpleNode(int i)
	{
		id = i;
	}

	
	public override string ToString()
	{
		return JJTreeParserTreeConstants.jjtNodeName[id];
	}

	
	public virtual string ToString(string str)
	{
		string result = new StringBuilder().Append(str).Append(ToString()).ToString();
		
		return result;
	}

	
	public virtual void dump(string str)
	{
		Console.WriteLine(ToString(str));
		if (children != null)
		{
			for (int i = 0; i < (nint)children.LongLength; i++)
			{
				((SimpleNode)children[i])?.dump(new StringBuilder().Append(str).Append(" ").ToString());
			}
		}
	}

	
	public SimpleNode(JJTreeParser jjtp, int i)
		: this(i)
	{
		parser = jjtp;
	}

	public virtual void jjtOpen()
	{
	}

	public virtual void jjtClose()
	{
	}

	public virtual void jjtSetParent(Node n)
	{
		parent = n;
	}

	public virtual Node jjtGetParent()
	{
		return parent;
	}

	public virtual void jjtAddChild(Node n, int i)
	{
		if (children == null)
		{
			children = new Node[i + 1];
		}
		else if (i >= (nint)children.LongLength)
		{
			Node[] dest = new Node[i + 1];
			ByteCodeHelper.arraycopy(children, 0, dest, 0, children.Length);
			children = dest;
		}
		children[i] = n;
	}

	public virtual Node jjtGetChild(int i)
	{
		return children[i];
	}

	public virtual int jjtGetNumChildren()
	{
		return (int)((children != null) ? children.LongLength : 0);
	}

	public virtual void jjtSetValue(object obj)
	{
		value = obj;
	}

	public virtual object jjtGetValue()
	{
		return value;
	}
}
