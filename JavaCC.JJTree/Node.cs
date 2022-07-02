namespace JavaCC.JJTree;

public interface Node
{
    void jjtOpen();
    void jjtClose();
    void jjtSetParent(Node n);
    Node jjtGetParent();
    void jjtAddChild(Node n, int i);
    Node jjtGetChild(int i);
    int jjtGetNumChildren();
}
