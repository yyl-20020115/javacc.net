namespace JavaCC.JJTree;

public class JJTreeParserConstants
{
	const int EOF = 0;

	const int _LOOKAHEAD = 1;

	const int _IGNORE_CASE = 2;

	const int _PARSER_BEGIN = 3;

	const int _PARSER_END = 4;

	const int _JAVACODE = 5;

	const int _TOKEN = 6;

	const int _SPECIAL_TOKEN = 7;

	const int _MORE = 8;

	const int _SKIP = 9;

	const int _TOKEN_MGR_DECLS = 10;

	const int _EOF = 11;

	const int SINGLE_LINE_COMMENT = 20;

	const int FORMAL_COMMENT = 21;

	const int MULTI_LINE_COMMENT = 22;

	const int ABSTRACT = 24;

	const int BOOLEAN = 25;

	const int BREAK = 26;

	const int BYTE = 27;

	const int CASE = 28;

	const int CATCH = 29;

	const int CHAR = 30;

	const int CLASS = 31;

	const int CONST = 32;

	const int CONTINUE = 33;

	const int _DEFAULT = 34;

	const int DO = 35;

	const int DOUBLE = 36;

	const int ELSE = 37;

	const int EXTENDS = 38;

	const int FALSE = 39;

	const int FINAL = 40;

	const int FINALLY = 41;

	const int FLOAT = 42;

	const int FOR = 43;

	const int GOTO = 44;

	const int IF = 45;

	const int IMPLEMENTS = 46;

	const int IMPORT = 47;

	const int INSTANCEOF = 48;

	const int INT = 49;

	const int INTERFACE = 50;

	const int LONG = 51;

	const int NATIVE = 52;

	const int NEW = 53;

	const int NULL = 54;

	const int PACKAGE = 55;

	const int PRIVATE = 56;

	const int PROTECTED = 57;

	const int PUBLIC = 58;

	const int RETURN = 59;

	const int SHORT = 60;

	const int STATIC = 61;

	const int SUPER = 62;

	const int SWITCH = 63;

	const int SYNCHRONIZED = 64;

	const int THIS = 65;

	const int THROW = 66;

	const int THROWS = 67;

	const int TRANSIENT = 68;

	const int TRUE = 69;

	const int TRY = 70;

	const int VOID = 71;

	const int VOLATILE = 72;

	const int WHILE = 73;

	const int INTEGER_LITERAL = 74;

	const int DECIMAL_LITERAL = 75;

	const int HEX_LITERAL = 76;

	const int OCTAL_LITERAL = 77;

	const int FLOATING_POINT_LITERAL = 78;

	const int DECIMAL_FLOATING_POINT_LITERAL = 79;

	const int DECIMAL_EXPONENT = 80;

	const int HEXADECIMAL_FLOATING_POINT_LITERAL = 81;

	const int HEXADECIMAL_EXPONENT = 82;

	const int CHARACTER_LITERAL = 83;

	const int STRING_LITERAL = 84;

	const int LPAREN = 85;

	const int RPAREN = 86;

	const int LBRACE = 87;

	const int RBRACE = 88;

	const int LBRACKET = 89;

	const int RBRACKET = 90;

	const int SEMICOLON = 91;

	const int COMMA = 92;

	const int DOT = 93;

	const int ASSIGN = 94;

	const int LT = 95;

	const int BANG = 96;

	const int TILDE = 97;

	const int HOOK = 98;

	const int COLON = 99;

	const int EQ = 100;

	const int LE = 101;

	const int GE = 102;

	const int NE = 103;

	const int SC_OR = 104;

	const int SC_AND = 105;

	const int INCR = 106;

	const int DECR = 107;

	const int PLUS = 108;

	const int MINUS = 109;

	const int STAR = 110;

	const int SLASH = 111;

	const int BIT_AND = 112;

	const int BIT_OR = 113;

	const int XOR = 114;

	const int REM = 115;

	const int PLUSASSIGN = 116;

	const int MINUSASSIGN = 117;

	const int STARASSIGN = 118;

	const int SLASHASSIGN = 119;

	const int ANDASSIGN = 120;

	const int ORASSIGN = 121;

	const int XORASSIGN = 122;

	const int REMASSIGN = 123;

	const int RUNSIGNEDSHIFT = 124;

	const int RSIGNEDSHIFT = 125;

	const int GT = 126;

	const int IDENTIFIER = 137;

	const int LETTER = 138;

	const int PART_LETTER = 139;

	const int DEFAULT = 0;

	const int IN_SINGLE_LINE_COMMENT = 1;

	const int IN_FORMAL_COMMENT = 2;

	const int IN_MULTI_LINE_COMMENT = 3;

	public static readonly string[] tokenImage;

	
	static JJTreeParserConstants()
	{
		tokenImage = new string[140]
		{
			"<EOF>", "\"LOOKAHEAD\"", "\"IGNORE_CASE\"", "\"PARSER_BEGIN\"", "\"PARSER_END\"", "\"JAVACODE\"", "\"TOKEN\"", "\"SPECIAL_TOKEN\"", "\"MORE\"", "\"SKIP\"",
			"\"TOKEN_MGR_DECLS\"", "\"EOF\"", "\" \"", "\"\\t\"", "\"\\n\"", "\"\\r\"", "\"\\f\"", "\"//\"", "<token of kind 18>", "\"/*\"",
			"<SINGLE_LINE_COMMENT>", "\"*/\"", "\"*/\"", "<token of kind 23>", "\"abstract\"", "\"boolean\"", "\"break\"", "\"byte\"", "\"case\"", "\"catch\"",
			"\"char\"", "\"class\"", "\"const\"", "\"continue\"", "\"default\"", "\"do\"", "\"double\"", "\"else\"", "\"extends\"", "\"false\"",
			"\"final\"", "\"finally\"", "\"float\"", "\"for\"", "\"goto\"", "\"if\"", "\"implements\"", "\"import\"", "\"instanceof\"", "\"int\"",
			"\"interface\"", "\"long\"", "\"native\"", "\"new\"", "\"null\"", "\"package\"", "\"private\"", "\"protected\"", "\"public\"", "\"return\"",
			"\"short\"", "\"static\"", "\"super\"", "\"switch\"", "\"synchronized\"", "\"this\"", "\"throw\"", "\"throws\"", "\"transient\"", "\"true\"",
			"\"try\"", "\"void\"", "\"volatile\"", "\"while\"", "<INTEGER_LITERAL>", "<DECIMAL_LITERAL>", "<HEX_LITERAL>", "<OCTAL_LITERAL>", "<FLOATING_POINT_LITERAL>", "<DECIMAL_FLOATING_POINT_LITERAL>",
			"<DECIMAL_EXPONENT>", "<HEXADECIMAL_FLOATING_POINT_LITERAL>", "<HEXADECIMAL_EXPONENT>", "<CHARACTER_LITERAL>", "<STRING_LITERAL>", "\"(\"", "\")\"", "\"{\"", "\"}\"", "\"[\"",
			"\"]\"", "\";\"", "\",\"", "\".\"", "\"=\"", "\"<\"", "\"!\"", "\"~\"", "\"?\"", "\":\"",
			"\"==\"", "\"<=\"", "\">=\"", "\"!=\"", "\"||\"", "\"&&\"", "\"++\"", "\"--\"", "\"+\"", "\"-\"",
			"\"*\"", "\"/\"", "\"&\"", "\"|\"", "\"^\"", "\"%\"", "\"+=\"", "\"-=\"", "\"*=\"", "\"/=\"",
			"\"&=\"", "\"|=\"", "\"^=\"", "\"%=\"", "\">>>\"", "\">>\"", "\">\"", "\"#\"", "\"strictfp\"", "\"enum\"",
			"\"...\"", "\"<<=\"", "\">>=\"", "\">>>=\"", "\"<<\"", "\"assert\"", "\"@\"", "<IDENTIFIER>", "<LETTER>", "<PART_LETTER>"
		};
	}
}
