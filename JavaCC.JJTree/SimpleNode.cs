namespace JavaCC.JJTree;
using System;

public class SimpleNode : Node
{
    protected internal Node parent;
    protected internal Node[] children = Array.Empty<Node>();
    protected internal int id = 0;
    protected internal object value = new();
    protected internal JJTreeParser parser;

    public SimpleNode(int id) { this.id = id; }

    public override string ToString() => JJTreeParserTreeConstants.jjtNodeName[id];


    public virtual string ToString(string str) => str + this.ToString();


    public virtual void Dump(string str)
    {
        Console.WriteLine(ToString(str));
        if (children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                ((SimpleNode)children[i])?.Dump(str + " ");
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

    public virtual void jjtSetParent(Node n) => parent = n;

    public virtual Node jjtGetParent() => parent;

    public virtual void jjtAddChild(Node n, int i)
    {
        if (children == null)
        {
            children = new Node[i + 1];
        }
        else if (i >= children.Length)
        {
            Node[] dest = new Node[i + 1];
            Array.Copy(children, 0, dest, 0, children.Length);
            children = dest;
        }
        children[i] = n;
    }

    public virtual Node jjtGetChild(int i) => children[i];

    public virtual int jjtGetNumChildren() => ((children != null) ? children.Length : 0);

    public virtual void jjtSetValue(object obj) => value = obj;

    public virtual object jjtGetValue() => value;
}
