namespace JavaCC.Parser;

public interface TreeWalkerOp
{
    void Action(Expansion e);
    bool GoDeeper(Expansion e);
}
