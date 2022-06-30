namespace org.javacc.jjtree;

public class JJTreeParserTreeConstants
{
	const int JJTGRAMMAR = 0;

	const int JJTCOMPILATIONUNIT = 1;

	const int JJTPRODUCTIONS = 2;

	const int JJTVOID = 3;

	const int JJTOPTIONS = 4;

	const int JJTOPTIONBINDING = 5;

	const int JJTJAVACODE = 6;

	const int JJTJAVACODEBODY = 7;

	const int JJTBNF = 8;

	const int JJTBNFDECLARATION = 9;

	const int JJTBNFNODESCOPE = 10;

	const int JJTRE = 11;

	const int JJTTOKENDECLS = 12;

	const int JJTRESPEC = 13;

	const int JJTBNFCHOICE = 14;

	const int JJTBNFSEQUENCE = 15;

	const int JJTBNFLOOKAHEAD = 16;

	const int JJTEXPANSIONNODESCOPE = 17;

	const int JJTBNFACTION = 18;

	const int JJTBNFZEROORONE = 19;

	const int JJTBNFTRYBLOCK = 20;

	const int JJTBNFNONTERMINAL = 21;

	const int JJTBNFASSIGNMENT = 22;

	const int JJTBNFONEORMORE = 23;

	const int JJTBNFZEROORMORE = 24;

	const int JJTBNFPARENTHESIZED = 25;

	const int JJTRESTRINGLITERAL = 26;

	const int JJTRENAMED = 27;

	const int JJTREREFERENCE = 28;

	const int JJTREEOF = 29;

	const int JJTRECHOICE = 30;

	const int JJTRESEQUENCE = 31;

	const int JJTREONEORMORE = 32;

	const int JJTREZEROORMORE = 33;

	const int JJTREZEROORONE = 34;

	const int JJTRREPETITIONRANGE = 35;

	const int JJTREPARENTHESIZED = 36;

	const int JJTRECHARLIST = 37;

	const int JJTCHARDESCRIPTOR = 38;

	const int JJTNODEDESCRIPTOR = 39;

	const int JJTNODEDESCRIPTOREXPRESSION = 40;

	const int JJTPRIMARYEXPRESSION = 41;

	public static readonly string[] jjtNodeName;

	
	static JJTreeParserTreeConstants()
	{
		jjtNodeName = new string[42]
		{
			"Grammar", "CompilationUnit", "Productions", "void", "Options", "OptionBinding", "Javacode", "JavacodeBody", "BNF", "BNFDeclaration",
			"BNFNodeScope", "RE", "TokenDecls", "RESpec", "BNFChoice", "BNFSequence", "BNFLookahead", "ExpansionNodeScope", "BNFAction", "BNFZeroOrOne",
			"BNFTryBlock", "BNFNonTerminal", "BNFAssignment", "BNFOneOrMore", "BNFZeroOrMore", "BNFParenthesized", "REStringLiteral", "RENamed", "REReference", "REEOF",
			"REChoice", "RESequence", "REOneOrMore", "REZeroOrMore", "REZeroOrOne", "RRepetitionRange", "REParenthesized", "RECharList", "CharDescriptor", "NodeDescriptor",
			"NodeDescriptorExpression", "PrimaryExpression"
		};
	}
}
