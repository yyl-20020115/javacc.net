namespace JavaCC.Parser;

public interface TreeWalkerOp
{
    void Action(Expansion expression);
    bool GoDeeper(Expansion expression);
}
