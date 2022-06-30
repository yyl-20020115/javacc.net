namespace org.javacc.parser;
public interface TreeWalkerOp
{
	void Action(Expansion e);
	bool GoDeeper(Expansion e);
}
