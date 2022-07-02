namespace JavaCC.JJTree;

public interface INode
{
    void JJTOpen();
    void JJTClose();
    void JJTSetParent(INode n);
    INode JJTGetParent();
    void JJTAddChild(INode n, int i);
    INode JJTGetChild(int i);
    int JJTGetNumChildren();
}
