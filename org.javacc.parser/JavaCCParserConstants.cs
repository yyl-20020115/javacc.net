namespace org.javacc.parser;

public class JavaCCParserConstants
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

	const int SINGLE_LINE_COMMENT = 23;

	const int FORMAL_COMMENT = 24;

	const int MULTI_LINE_COMMENT = 25;

	const int ABSTRACT = 27;

	const int ASSERT = 28;

	const int BOOLEAN = 29;

	const int BREAK = 30;

	const int BYTE = 31;

	const int CASE = 32;

	const int CATCH = 33;

	const int CHAR = 34;

	const int CLASS = 35;

	const int CONST = 36;

	const int CONTINUE = 37;

	const int _DEFAULT = 38;

	const int DO = 39;

	const int DOUBLE = 40;

	const int ELSE = 41;

	const int ENUM = 42;

	const int EXTENDS = 43;

	const int FALSE = 44;

	const int FINAL = 45;

	const int FINALLY = 46;

	const int FLOAT = 47;

	const int FOR = 48;

	const int GOTO = 49;

	const int IF = 50;

	const int IMPLEMENTS = 51;

	const int IMPORT = 52;

	const int INSTANCEOF = 53;

	const int INT = 54;

	const int INTERFACE = 55;

	const int LONG = 56;

	const int NATIVE = 57;

	const int NEW = 58;

	const int NULL = 59;

	const int PACKAGE = 60;

	const int PRIVATE = 61;

	const int PROTECTED = 62;

	const int PUBLIC = 63;

	const int RETURN = 64;

	const int SHORT = 65;

	const int STATIC = 66;

	const int STRICTFP = 67;

	const int SUPER = 68;

	const int SWITCH = 69;

	const int SYNCHRONIZED = 70;

	const int THIS = 71;

	const int THROW = 72;

	const int THROWS = 73;

	const int TRANSIENT = 74;

	const int TRUE = 75;

	const int TRY = 76;

	const int VOID = 77;

	const int VOLATILE = 78;

	const int WHILE = 79;

	const int INTEGER_LITERAL = 80;

	const int DECIMAL_LITERAL = 81;

	const int HEX_LITERAL = 82;

	const int OCTAL_LITERAL = 83;

	const int FLOATING_POINT_LITERAL = 84;

	const int DECIMAL_FLOATING_POINT_LITERAL = 85;

	const int DECIMAL_EXPONENT = 86;

	const int HEXADECIMAL_FLOATING_POINT_LITERAL = 87;

	const int HEXADECIMAL_EXPONENT = 88;

	const int CHARACTER_LITERAL = 89;

	const int STRING_LITERAL = 90;

	const int LPAREN = 91;

	const int RPAREN = 92;

	const int LBRACE = 93;

	const int RBRACE = 94;

	const int LBRACKET = 95;

	const int RBRACKET = 96;

	const int SEMICOLON = 97;

	const int COMMA = 98;

	const int DOT = 99;

	const int ASSIGN = 100;

	const int LT = 101;

	const int BANG = 102;

	const int TILDE = 103;

	const int HOOK = 104;

	const int COLON = 105;

	const int EQ = 106;

	const int LE = 107;

	const int GE = 108;

	const int NE = 109;

	const int SC_OR = 110;

	const int SC_AND = 111;

	const int INCR = 112;

	const int DECR = 113;

	const int PLUS = 114;

	const int MINUS = 115;

	const int STAR = 116;

	const int SLASH = 117;

	const int BIT_AND = 118;

	const int BIT_OR = 119;

	const int XOR = 120;

	const int REM = 121;

	const int PLUSASSIGN = 122;

	const int MINUSASSIGN = 123;

	const int STARASSIGN = 124;

	const int SLASHASSIGN = 125;

	const int ANDASSIGN = 126;

	const int ORASSIGN = 127;

	const int XORASSIGN = 128;

	const int REMASSIGN = 129;

	const int RUNSIGNEDSHIFT = 130;

	const int RSIGNEDSHIFT = 131;

	const int GT = 132;

	const int LANGLE = 101;

	const int RANGLE = 132;

	const int IDENTIFIER = 140;

	const int LETTER = 141;

	const int PART_LETTER = 142;

	const int DEFAULT = 0;

	const int AFTER_EGEN = 1;

	const int IN_SINGLE_LINE_COMMENT = 2;

	const int IN_FORMAL_COMMENT = 3;

	const int IN_MULTI_LINE_COMMENT = 4;

	public static readonly string[] tokenImage;

	static JavaCCParserConstants()
	{
		tokenImage = new string[143]
		{
			"<EOF>", "\"LOOKAHEAD\"", "\"IGNORE_CASE\"", "\"PARSER_BEGIN\"", "\"PARSER_END\"", "\"JAVACODE\"", "\"TOKEN\"", "\"SPECIAL_TOKEN\"", "\"MORE\"", "\"SKIP\"",
			"\"TOKEN_MGR_DECLS\"", "\"EOF\"", "\" \"", "\"\\t\"", "\"\\n\"", "\"\\r\"", "\"\\f\"", "\"/*@egen*/\"", "<token of kind 18>", "\"//\"",
			"<token of kind 20>", "\"/*\"", "\"/*@bgen(jjtree\"", "<SINGLE_LINE_COMMENT>", "\"*/\"", "\"*/\"", "<token of kind 26>", "\"abstract\"", "\"assert\"", "\"boolean\"",
			"\"break\"", "\"byte\"", "\"case\"", "\"catch\"", "\"char\"", "\"class\"", "\"const\"", "\"continue\"", "\"default\"", "\"do\"",
			"\"double\"", "\"else\"", "\"enum\"", "\"extends\"", "\"false\"", "\"final\"", "\"finally\"", "\"float\"", "\"for\"", "\"goto\"",
			"\"if\"", "\"implements\"", "\"import\"", "\"instanceof\"", "\"int\"", "\"interface\"", "\"long\"", "\"native\"", "\"new\"", "\"null\"",
			"\"package\"", "\"private\"", "\"protected\"", "\"public\"", "\"return\"", "\"short\"", "\"static\"", "\"strictfp\"", "\"super\"", "\"switch\"",
			"\"synchronized\"", "\"this\"", "\"throw\"", "\"throws\"", "\"transient\"", "\"true\"", "\"try\"", "\"void\"", "\"volatile\"", "\"while\"",
			"<INTEGER_LITERAL>", "<DECIMAL_LITERAL>", "<HEX_LITERAL>", "<OCTAL_LITERAL>", "<FLOATING_POINT_LITERAL>", "<DECIMAL_FLOATING_POINT_LITERAL>", "<DECIMAL_EXPONENT>", "<HEXADECIMAL_FLOATING_POINT_LITERAL>", "<HEXADECIMAL_EXPONENT>", "<CHARACTER_LITERAL>",
			"<STRING_LITERAL>", "\"(\"", "\")\"", "\"{\"", "\"}\"", "\"[\"", "\"]\"", "\";\"", "\",\"", "\".\"",
			"\"=\"", "\"<\"", "\"!\"", "\"~\"", "\"?\"", "\":\"", "\"==\"", "\"<=\"", "\">=\"", "\"!=\"",
			"\"||\"", "\"&&\"", "\"++\"", "\"--\"", "\"+\"", "\"-\"", "\"*\"", "\"/\"", "\"&\"", "\"|\"",
			"\"^\"", "\"%\"", "\"+=\"", "\"-=\"", "\"*=\"", "\"/=\"", "\"&=\"", "\"|=\"", "\"^=\"", "\"%=\"",
			"\">>>\"", "\">>\"", "\">\"", "\"#\"", "\"...\"", "\"<<=\"", "\">>=\"", "\">>>=\"", "\"<<\"", "\"@\"",
			"<IDENTIFIER>", "<LETTER>", "<PART_LETTER>"
		};
	}
}
