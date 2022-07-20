namespace JavaCC.Parser;

public class JavaCCParserConstants
{
    public const int EOF = 0;

    public const int _LOOKAHEAD = 1;

    public const int _IGNORE_CASE = 2;

    public const int _PARSER_BEGIN = 3;

    public const int _PARSER_END = 4;

    public const int _JAVACODE = 5;

    public const int _TOKEN = 6;

    public const int _SPECIAL_TOKEN = 7;

    public const int _MORE = 8;

    public const int _SKIP = 9;

    public const int _TOKEN_MGR_DECLS = 10;

    public const int _EOF = 11;

    public const int SINGLE_LINE_COMMENT = 23;

    public const int FORMAL_COMMENT = 24;

    public const int MULTI_LINE_COMMENT = 25;

    public const int ABSTRACT = 27;

    public const int ASSERT = 28;

    public const int BOOLEAN = 29;

    public const int BREAK = 30;

    public const int BYTE = 31;

    public const int CASE = 32;

    public const int CATCH = 33;

    public const int CHAR = 34;

    public const int CLASS = 35;

    public const int CONST = 36;

    public const int CONTINUE = 37;

    public const int _DEFAULT = 38;

    public const int DO = 39;

    public const int DOUBLE = 40;

    public const int ELSE = 41;

    public const int ENUM = 42;

    public const int EXTENDS = 43;

    public const int FALSE = 44;

    public const int FINAL = 45;

    public const int FINALLY = 46;

    public const int FLOAT = 47;

    public const int FOR = 48;

    public const int GOTO = 49;

    public const int IF = 50;

    public const int IMPLEMENTS = 51;

    public const int IMPORT = 52;

    public const int INSTANCEOF = 53;

    public const int INT = 54;

    public const int INTERFACE = 55;

    public const int LONG = 56;

    public const int NATIVE = 57;

    public const int NEW = 58;

    public const int NULL = 59;

    public const int PACKAGE = 60;

    public const int PRIVATE = 61;

    public const int PROTECTED = 62;

    public const int PUBLIC = 63;

    public const int RETURN = 64;

    public const int SHORT = 65;

    public const int STATIC = 66;

    public const int STRICTFP = 67;

    public const int SUPER = 68;

    public const int SWITCH = 69;

    public const int SYNCHRONIZED = 70;

    public const int THIS = 71;

    public const int THROW = 72;

    public const int THROWS = 73;

    public const int TRANSIENT = 74;

    public const int TRUE = 75;

    public const int TRY = 76;

    public const int VOID = 77;

    public const int VOLATILE = 78;

    public const int WHILE = 79;

    public const int INTEGER_LITERAL = 80;

    public const int DECIMAL_LITERAL = 81;

    public const int HEX_LITERAL = 82;

    public const int OCTAL_LITERAL = 83;

    public const int FLOATING_POINT_LITERAL = 84;

    public const int DECIMAL_FLOATING_POINT_LITERAL = 85;

    public const int DECIMAL_EXPONENT = 86;

    public const int HEXADECIMAL_FLOATING_POINT_LITERAL = 87;

    public const int HEXADECIMAL_EXPONENT = 88;

    public const int CHARACTER_LITERAL = 89;

    public const int STRING_LITERAL = 90;

    public const int LPAREN = 91;

    public const int RPAREN = 92;

    public const int LBRACE = 93;

    public const int RBRACE = 94;

    public const int LBRACKET = 95;

    public const int RBRACKET = 96;

    public const int SEMICOLON = 97;

    public const int COMMA = 98;

    public const int DOT = 99;

    public const int ASSIGN = 100;

    public const int LT = 101;

    public const int BANG = 102;

    public const int TILDE = 103;

    public const int HOOK = 104;

    public const int COLON = 105;

    public const int EQ = 106;

    public const int LE = 107;

    public const int GE = 108;

    public const int NE = 109;

    public const int SC_OR = 110;

    public const int SC_AND = 111;

    public const int INCR = 112;

    public const int DECR = 113;

    public const int PLUS = 114;

    public const int MINUS = 115;

    public const int STAR = 116;

    public const int SLASH = 117;

    public const int BIT_AND = 118;

    public const int BIT_OR = 119;

    public const int XOR = 120;

    public const int REM = 121;

    public const int PLUSASSIGN = 122;

    public const int MINUSASSIGN = 123;

    public const int STARASSIGN = 124;

    public const int SLASHASSIGN = 125;

    public const int ANDASSIGN = 126;

    public const int ORASSIGN = 127;

    public const int XORASSIGN = 128;

    public const int REMASSIGN = 129;

    public const int RUNSIGNEDSHIFT = 130;

    public const int RSIGNEDSHIFT = 131;

    public const int GT = 132;

    public const int LANGLE = 101;

    public const int RANGLE = 132;

    public const int IDENTIFIER = 140;

    public const int LETTER = 141;

    public const int PART_LETTER = 142;

    public const int DEFAULT = 0;

    public const int AFTER_EGEN = 1;

    public const int IN_SINGLE_LINE_COMMENT = 2;

    public const int IN_FORMAL_COMMENT = 3;

    public const int IN_MULTI_LINE_COMMENT = 4;

    public static readonly string[] tokenImage = new string[]
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
