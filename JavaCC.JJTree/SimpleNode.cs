namespace JavaCC.JJTree;
using System;

public class SimpleNode : INode
{
    protected internal INode parent;
    protected internal INode[] children = Array.Empty<INode>();
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

    public virtual void JJTOpen()
    {
    }

    public virtual void JJTClose()
    {
    }

    public virtual void JJTSetParent(INode n) => parent = n;

    public virtual INode JJTGetParent() => parent;

    public virtual void JJTAddChild(INode n, int i)
    {
        if (children == null)
        {
            children = new INode[i + 1];
        }
        else if (i >= children.Length)
        {
            INode[] dest = new INode[i + 1];
            Array.Copy(children, 0, dest, 0, children.Length);
            children = dest;
        }
        children[i] = n;
    }

    public virtual INode JJTGetChild(int i) => children[i];

    public virtual int JJTGetNumChildren() => ((children != null) ? children.Length : 0);

    public virtual void JJTSetValue(object obj) => value = obj;

    public virtual object JJTGetValue() => value;
}
