namespace JavaCC.JJTree;
using System.Collections.Generic;

public class JJTJJTreeParserState
{
    private List<INode> Nodes = new();

    private List<int> Marks = new();

    private int SP = 0;

    private int MK = 0;

    private bool _NodeCreated = false;


    public virtual INode PopNode()
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


    public virtual void PushNode(INode n)
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


    public virtual INode RootNode => Nodes[0];


    public virtual INode PeekNode => Nodes[(Nodes.Count - 1)];


    public virtual void ClearNodeScope(INode n)
    {
        while (SP > MK)
        {
            PopNode();
        }
        MK = Marks[Marks.Count - 1];
        Marks.RemoveAt(Marks.Count - 1);
    }


    public virtual void OpenNodeScope(INode n)
    {
        var list = Marks;
        list.Add((MK));
        MK = SP;
        n.JJTOpen();
    }


    public virtual void CloseNodeScope(INode n, int i)
    {
        MK = Marks[Marks.Count - 1];
        Marks.RemoveAt(Marks.Count - 1);
        while (i-- > 0)
        {
            INode node = PopNode();
            node.JJTSetParent(n);
            n.JJTAddChild(node, i);
        }
        n.JJTClose();
        PushNode(n);
        _NodeCreated = true;
    }


    public virtual void CloseNodeScope(INode n, bool b)
    {
        if (b)
        {
            int i = NodeArity;
            MK = Marks[Marks.Count - 1];
            Marks.RemoveAt(Marks.Count - 1);
            while (i-- > 0)
            {
                INode node = PopNode();
                node.JJTSetParent(n);
                n.JJTAddChild(node, i);
            }
            n.JJTClose();
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
