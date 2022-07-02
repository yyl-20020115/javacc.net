using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;

namespace org.javacc.parser;


public class JavaCCParser : JavaCCParserInternals //, JavaCCParserConstants
{

    internal sealed class JJCalls
    {
        internal int gen;

        internal Token first;

        internal int arg;

        internal JJCalls next;


        internal JJCalls()
        {
        }
    }



    internal class LookaheadSuccess : System.Exception
    {

        public LookaheadSuccess()
        {
        }
    }


    public class ModifierSet
    {
        public const int PUBLIC = 1;

        public const int PROTECTED = 2;

        public const int PRIVATE = 4;

        public const int ABSTRACT = 8;

        public const int STATIC = 16;

        public const int FINAL = 32;

        public const int SYNCHRONIZED = 64;

        public const int NATIVE = 128;

        public const int TRANSIENT = 256;

        public const int VOLATILE = 512;

        public const int STRICTFP = 4096;


        public ModifierSet()
        {
        }

        public virtual bool IsPublic(int i)
        {
            return (((uint)i & (true ? 1u : 0u)) != 0) ? true : false;
        }

        public virtual bool IsProtected(int i)
        {
            return (((uint)i & 2u) != 0) ? true : false;
        }

        public virtual bool IsPrivate(int i)
        {
            return (((uint)i & 4u) != 0) ? true : false;
        }

        public virtual bool IsStatic(int i)
        {
            return (((uint)i & 0x10u) != 0) ? true : false;
        }

        public virtual bool isAbstract(int i)
        {
            return (((uint)i & 8u) != 0) ? true : false;
        }

        public virtual bool isFinal(int i)
        {
            return (((uint)i & 0x20u) != 0) ? true : false;
        }

        public virtual bool isNative(int i)
        {
            return (((uint)i & 0x80u) != 0) ? true : false;
        }

        public virtual bool isStrictfp(int i)
        {
            return (((uint)i & 0x1000u) != 0) ? true : false;
        }

        public virtual bool isSynchronized(int i)
        {
            return (((uint)i & 0x40u) != 0) ? true : false;
        }

        public virtual bool isTransient(int i)
        {
            return (((uint)i & 0x100u) != 0) ? true : false;
        }

        public virtual bool isVolatile(int i)
        {
            return (((uint)i & 0x200u) != 0) ? true : false;
        }

        internal static int removeModifier(int P_0, int P_1)
        {
            return P_0 & (P_1 ^ -1);
        }
    }

    internal string parser_class_name;

    internal bool processing_cu;

    internal int class_nesting;

    internal int inLocalLA;

    internal bool inAction;

    internal bool jumpPatched;

    public JavaCCParserTokenManager token_source;

    internal JavaCharStream jj_input_stream;

    public Token token;

    public Token jj_nt;

    private int m_jj_ntk;

    private Token jj_scanpos;

    private Token jj_lastpos;

    private int jj_la;

    public bool lookingAhead;

    private bool jj_semLA;

    private int jj_gen;


    private int[] jj_la1;

    private static int[] jj_la1_0;

    private static int[] jj_la1_1;

    private static int[] jj_la1_2;

    private static int[] jj_la1_3;

    private static int[] jj_la1_4;


    private JJCalls[] jj_2_rtns;

    private bool jj_rescan;

    private int jj_gc;


    private LookaheadSuccess jj_ls;

    private List<int[]> jj_expentries =new();

    private int[] jj_expentry;

    private int jj_kind;

    private int[] jj_lasttokens;

    private int jj_endpos;


    public JavaCCParser(Stream @is)
        : this(new StreamReader(@is))
    {
    }


    public JavaCCParser(TextReader r)
    {
        processing_cu = false;
        class_nesting = 0;
        inLocalLA = 0;
        inAction = false;
        jumpPatched = false;
        lookingAhead = false;
        jj_la1 = new int[172];
        jj_2_rtns = new JJCalls[48];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new LookaheadSuccess();
        jj_expentries = new();
        jj_kind = -1;
        jj_lasttokens = new int[100];
        jj_input_stream = new JavaCharStream(r, 1, 1);
        JavaCCParserTokenManager.___003Cclinit_003E();
        token_source = new JavaCCParserTokenManager(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public void javacc_input()
    {
        JavaCCParserInternals.Initialize();
        javacc_options();
        jj_consume_token(3);
        jj_consume_token(91);
        string text = identifier();
        JavaCCParserInternals.AddCuname(text);
        jj_consume_token(92);
        processing_cu = true;
        parser_class_name = text;
        CompilationUnit();
        processing_cu = false;
        jj_consume_token(4);
        jj_consume_token(91);
        string str = identifier();
        JavaCCParserInternals.Compare(getToken(0), text, str);
        jj_consume_token(92);
        int num;
        do
        {
            production();
            num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        }
        while (num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 29 || num == 31 || num == 34 || num == 40 || num == 47 || num == 54 || num == 56 || num == 61 || num == 62 || num == 63 || num == 65 || num == 77 || num == 101 || num == 140);
        jj_la1[0] = jj_gen;
        jj_consume_token(0);
    }


    public Token getToken(int i)
    {
        Token token = ((!lookingAhead) ? this.token : jj_scanpos);
        for (int j = 0; j < i; j++)
        {
            if (token.next != null)
            {
                token = token.next;
                continue;
            }
            Token obj = token;
            Token nextToken = token_source.getNextToken();
            Token token2 = obj;
            token2.next = nextToken;
            token = nextToken;
        }
        return token;
    }


    public void javacc_options()
    {
        if (string.Equals(getToken(1).image, "options"))
        {
            jj_consume_token(140);
            jj_consume_token(93);
            while (true)
            {
                int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                if (num != 1 && num != 2 && num != 66 && num != 140)
                {
                    break;
                }
                option_binding();
            }
            jj_la1[1] = jj_gen;
            jj_consume_token(94);
        }
        Options.Normalize();
    }


    private Token jj_consume_token(int P_0)
    {
        Token token;
        if ((token = this.token).next != null)
        {
            this.token = this.token.next;
        }
        else
        {
            Token obj = this.token;
            Token nextToken = token_source.getNextToken();
            Token token2 = obj;
            token2.next = nextToken;
            this.token = nextToken;
        }
        this.m_jj_ntk = -1;
        if (this.token.kind == P_0)
        {
            jj_gen++;
            int num = jj_gc + 1;
            jj_gc = num;
            if (num > 100)
            {
                jj_gc = 0;
                for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
                {
                    for (JJCalls jJCalls = jj_2_rtns[i]; jJCalls != null; jJCalls = jJCalls.next)
                    {
                        if (jJCalls.gen < jj_gen)
                        {
                            jJCalls.first = null;
                        }
                    }
                }
            }
            return this.token;
        }
        this.token = token;
        jj_kind = P_0;
        throw generateParseException();
    }


    public string identifier()
    {
        Token token = jj_consume_token(140);
        return token.image;
    }


    public void CompilationUnit()
    {
        JavaCCParserInternals.set_initial_cu_token(getToken(1));
        if (jj_2_7(int.MaxValue))
        {
            PackageDeclaration();
        }
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 52)
        {
            ImportDeclaration();
        }
        jj_la1[46] = jj_gen;
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 27 && num != 35 && num != 42 && num != 45 && num != 55 && num != 57 && num != 61 && num != 62 && num != 63 && num != 66 && num != 67 && num != 70 && num != 74 && num != 78 && num != 97 && num != 139)
            {
                break;
            }
            TypeDeclaration();
        }
        jj_la1[47] = jj_gen;
        JavaCCParserInternals.InsertionPointError(getToken(1));
    }


    public void production()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 5:
                javacode_production();
                return;
            case 6:
            case 7:
            case 8:
            case 9:
            case 101:
                regular_expr_production();
                return;
            case 10:
                token_manager_decls();
                return;
            case 29:
            case 31:
            case 34:
            case 40:
            case 47:
            case 54:
            case 56:
            case 61:
            case 62:
            case 63:
            case 65:
            case 77:
            case 140:
                bnf_production();
                return;
        }
        jj_la1[4] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private int jj_ntk()
    {
        Token next = this.token.next;
        Token obj = next;
        jj_nt = next;
        int kind;
        if (obj == null)
        {
            Token obj2 = this.token;
            next = token_source.getNextToken();
            Token token = obj2;
            Token obj3 = next;
            token.next = next;
            kind = obj3.kind;
            int result = kind;
            this.m_jj_ntk = kind;
            return result;
        }
        kind = jj_nt.kind;
        int result2 = kind;
        this.m_jj_ntk = kind;
        return result2;
    }


    public void option_binding()
    {
        Token token = getToken(1);
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 140:
                jj_consume_token(140);
                break;
            case 1:
                jj_consume_token(1);
                break;
            case 2:
                jj_consume_token(2);
                break;
            case 66:
                jj_consume_token(66);
                break;
            default:
                jj_la1[2] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        string image = token.image;
        jj_consume_token(100);
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 80:
                {
                    int value2 = IntegerLiteral();
                    Options.SetInputFileOption(token, getToken(0), image, (value2));
                    break;
                }
            case 44:
            case 75:
                {
                    int value = (BooleanLiteral() ? 1 : 0);
                    Options.SetInputFileOption(token, getToken(0), image, ((byte)value != 0));
                    break;
                }
            case 90:
                {
                    string obj = StringLiteral();
                    Options.SetInputFileOption(token, getToken(0), image, obj);
                    break;
                }
            default:
                jj_la1[3] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        jj_consume_token(97);
    }


    public int IntegerLiteral()
    {
        //Discarded unreachable code: IL_001b
        jj_consume_token(80);
        try
        {
            return int.TryParse(token.image, out var v) ? v : 0;
        }
        catch
        {
        }


        throw new System.Exception();
    }


    public bool BooleanLiteral()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 75:
                jj_consume_token(75);
                return true;
            case 44:
                jj_consume_token(44);
                return false;
            default:
                jj_la1[131] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public string StringLiteral()
    {
        Token token = jj_consume_token(90);
        string result = JavaCCParserInternals.remove_escapes_and_quotes(token, token.image);

        return result;
    }


    public void javacode_production()
    {
        JavaCodeProduction javaCodeProduction = new JavaCodeProduction();
        Token token = getToken(1);
        JavaCodeProduction javaCodeProduction2 = javaCodeProduction;
        javaCodeProduction2.firstToken = token;
        Token token2 = token;
        javaCodeProduction.ThrowsList = new();
        javaCodeProduction.line = token2.BeginLine;
        javaCodeProduction.column = token2.BeginColumn;
        jj_consume_token(5);
        AccessModifier(javaCodeProduction);
        ResultType(javaCodeProduction.return_type_tokens);
        javaCodeProduction.lhs = identifier();
        FormalParameters(javaCodeProduction.parameter_list_tokens);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 73)
        {
            jj_consume_token(73);
            var vector = new List<Token>();
            Name(vector);
            javaCodeProduction.ThrowsList.Add(vector);
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
            {
                vector = new ();
                jj_consume_token(98);
                Name(vector);
                javaCodeProduction.ThrowsList.Add(vector);
            }
            jj_la1[5] = jj_gen;
        }
        else
        {
            jj_la1[6] = jj_gen;
        }
        Block(javaCodeProduction.CodeTokens);
        javaCodeProduction.lastToken = getToken(0);
        JavaCCParserInternals.addproduction(javaCodeProduction);
    }


    public void regular_expr_production()
    {
        TokenProduction tokenProduction = new TokenProduction();
        Token token = getToken(1);
        TokenProduction tokenProduction2 = tokenProduction;
        tokenProduction2.firstToken = token;
        Token token2 = token;
        tokenProduction.Line = token2.BeginLine;
        tokenProduction.Column = token2.BeginColumn;
        tokenProduction.LexStates = new string[1] { "DEFAULT" };
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 101)
        {
            if (jj_2_1(2))
            {
                jj_consume_token(101);
                jj_consume_token(116);
                jj_consume_token(132);
                tokenProduction.LexStates = null;
            }
            else
            {
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) != 101)
                {
                    jj_la1[12] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
                }
                jj_consume_token(101);
                ArrayList vector = new ArrayList();
                token2 = jj_consume_token(140);
                vector.Add(token2.image);
                while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
                {
                    jj_consume_token(98);
                    token2 = jj_consume_token(140);
                    vector.Add(token2.image);
                }
                jj_la1[11] = jj_gen;
                jj_consume_token(132);
                tokenProduction.LexStates = new string[vector.Count];
                for (int i = 0; i < vector.Count; i++)
                {
                    tokenProduction.LexStates[i] = (string)vector[i];
                }
            }
        }
        else
        {
            jj_la1[13] = jj_gen;
        }
        regexpr_kind(tokenProduction);
        if (tokenProduction.Kind != 0 && Options.UserTokenManager)
        {
            JavaCCErrors.Warning(getToken(0), "Regular expression is being treated as if it were a TOKEN since option USER_TOKEN_MANAGER has been set to true.");
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            jj_consume_token(95);
            token2 = jj_consume_token(2);
            jj_consume_token(96);
            tokenProduction.ignoreCase = true;
            if (Options.UserTokenManager)
            {
                JavaCCErrors.Warning(token2, "Ignoring \"IGNORE_CASE\" specification since option USER_TOKEN_MANAGER has been set to true.");
            }
        }
        else
        {
            jj_la1[14] = jj_gen;
        }
        jj_consume_token(105);
        jj_consume_token(93);
        regexpr_spec(tokenProduction);
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 119)
        {
            jj_consume_token(119);
            regexpr_spec(tokenProduction);
        }
        jj_la1[15] = jj_gen;
        token2 = (tokenProduction.lastToken = jj_consume_token(94));
        JavaCCParserInternals.addregexpr(tokenProduction);
    }


    public void token_manager_decls()
    {
        ArrayList v = new ();
        Token t = jj_consume_token(10);
        jj_consume_token(105);
        ClassOrInterfaceBody(b: false, v);
        JavaCCParserInternals.add_token_manager_decls(t, v);
    }


    public void bnf_production()
    {
        BNFProduction bNFProduction = new ();
        Container container = new ();
        Token token = getToken(1);
        BNFProduction bNFProduction2 = bNFProduction;
        bNFProduction2.firstToken = token;
        Token token2 = token;
        bNFProduction.ThrowsList = new ();
        bNFProduction.line = token2.BeginLine;
        bNFProduction.column = token2.BeginColumn;
        jumpPatched = false;
        AccessModifier(bNFProduction);
        ResultType(bNFProduction.return_type_tokens);
        bNFProduction.lhs = identifier();
        FormalParameters(bNFProduction.parameter_list_tokens);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 73)
        {
            jj_consume_token(73);
            var vector = new List<Token>();
            Name(vector);
            bNFProduction.ThrowsList.Add(vector);
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
            {
                vector = new ();
                jj_consume_token(98);
                Name(vector);
                bNFProduction.ThrowsList.Add(vector);
            }
            jj_la1[7] = jj_gen;
        }
        else
        {
            jj_la1[8] = jj_gen;
        }
        jj_consume_token(105);
        Block(bNFProduction.declaration_tokens);
        jj_consume_token(93);
        expansion_choices(container);
        token2 = (bNFProduction.lastToken = jj_consume_token(94));
        bNFProduction.jumpPatched = jumpPatched;
        JavaCCParserInternals.production_addexpansion(bNFProduction, (Expansion)container.member);
        JavaCCParserInternals.addproduction(bNFProduction);
    }


    public void AccessModifier(NormalProduction np)
    {
        Token token = null;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 61:
            case 62:
            case 63:
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 63:
                        token = jj_consume_token(63);
                        break;
                    case 62:
                        token = jj_consume_token(62);
                        break;
                    case 61:
                        token = jj_consume_token(61);
                        break;
                    default:
                        jj_la1[9] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                break;
            default:
                jj_la1[10] = jj_gen;
                break;
        }
        if (token != null)
        {
            np.accessMod = token.image;
        }
    }


    public void ResultType(List<Token> v)
    {
        Token token = getToken(1);
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 77:
                jj_consume_token(77);
                break;
            case 29:
            case 31:
            case 34:
            case 40:
            case 47:
            case 54:
            case 56:
            case 65:
            case 140:
                Type();
                break;
            default:
                jj_la1[98] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        Token token2 = getToken(0);
        Token token3 = token;
        while (true)
        {
            v.Add(token3);
            if (token3 == token2)
            {
                break;
            }
            token3 = token3.next;
        }
    }


    public void FormalParameters(List<Token> v)
    {
        jj_consume_token(91);
        Token token = getToken(1);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 27 || num == 29 || num == 31 || num == 34 || num == 40 || num == 45 || num == 47 || num == 54 || num == 56 || num == 57 || num == 61 || num == 62 || num == 63 || num == 65 || num == 66 || num == 67 || num == 70 || num == 74 || num == 78 || num == 139 || num == 140)
        {
            FormalParameter();
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
            {
                jj_consume_token(98);
                FormalParameter();
            }
            jj_la1[83] = jj_gen;
        }
        else
        {
            jj_la1[84] = jj_gen;
        }
        Token token2 = getToken(0);
        jj_consume_token(92);
        if (token2.next == token)
        {
            return;
        }
        Token token3 = token;
        while (true)
        {
            v.Add(token3);
            if (token3 == token2)
            {
                break;
            }
            token3 = token3.next;
        }
    }


    public void Name(List<Token> v)
    {
        Token token = getToken(1);
        JavaIdentifier();
        while (jj_2_23(2))
        {
            jj_consume_token(99);
            JavaIdentifier();
        }
        Token token2 = getToken(0);
        Token token3 = token;
        while (true)
        {
            v.Add(token3);
            if (token3 == token2)
            {
                break;
            }
            token3 = token3.next;
        }
    }


    public void Block(List<Token> v)
    {
        jj_consume_token(93);
        Token token = getToken(1);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    {
                        jj_la1[142] = jj_gen;
                        Token token2 = getToken(0);
                        jj_consume_token(94);
                        if (token2.next == token)
                        {
                            return;
                        }
                        Token token3 = token;
                        while (true)
                        {
                            v.Add(token3);
                            if (token3 == token2)
                            {
                                break;
                            }
                            token3 = token3.next;
                        }
                        return;
                    }
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 34:
                case 35:
                case 37:
                case 39:
                case 40:
                case 44:
                case 45:
                case 47:
                case 48:
                case 50:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 93:
                case 97:
                case 112:
                case 113:
                case 139:
                case 140:
                    break;
            }
            BlockStatement();
        }
    }


    public void expansion_choices(Container c)
    {
        int num = 0;
        Choice choice = null;
        Container container = new Container();
        expansion(c);
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 119)
        {
            jj_consume_token(119);
            expansion(container);
            if (num != 0)
            {
                choice.Choices.Add(container.member as Expansion);
                ((Expansion)container.member).parent = choice;
                continue;
            }
            num = 1;
            choice = new Choice((Expansion)c.member);
            ((Expansion)c.member).parent = choice;
            choice.Choices.Add(container.member as Expansion);
            ((Expansion)container.member).parent = choice;
        }
        jj_la1[19] = jj_gen;
        if (num != 0)
        {
            c.member = choice;
        }
    }


    private bool jj_2_1(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_1()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(0, P_0);
            throw;
        }
        jj_save(0, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(0, P_0);
        }
    }


    public void regexpr_kind(TokenProduction tp)
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 6:
                jj_consume_token(6);
                tp.Kind = 0;
                break;
            case 7:
                jj_consume_token(7);
                tp.Kind = 3;
                break;
            case 9:
                jj_consume_token(9);
                tp.Kind = 1;
                break;
            case 8:
                jj_consume_token(8);
                tp.Kind = 2;
                break;
            default:
                jj_la1[16] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void regexpr_spec(TokenProduction tp)
    {
        Container container = new Container();
        Action action = new Action();
        Token token = null;
        RegExprSpec regExprSpec = new RegExprSpec();
        regular_expression(container);
        regExprSpec.rexp = (RegularExpression)container.member;
        regExprSpec.rexp.tpContext = tp;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 93)
        {
            token = getToken(1);
            Block(action.ActionTokens);
            if (Options.UserTokenManager)
            {
                JavaCCErrors.Warning(token, "Ignoring action in regular expression specification since option USER_TOKEN_MANAGER has been set to true.");
            }
            if (regExprSpec.rexp.private_rexp)
            {
                JavaCCErrors.Parse_Error(token, "Actions are not permitted on private (#) regular expressions.");
            }
        }
        else
        {
            jj_la1[17] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 105)
        {
            jj_consume_token(105);
            token = jj_consume_token(140);
            regExprSpec.nextState = token.image;
            if (regExprSpec.rexp.private_rexp)
            {
                JavaCCErrors.Parse_Error(token, "Lexical state changes are not permitted after private (#) regular expressions.");
            }
        }
        else
        {
            jj_la1[18] = jj_gen;
        }
        regExprSpec.act = action;
        regExprSpec.nsTok = token;
        tp.Respecs.Add(regExprSpec);
    }


    public void ClassOrInterfaceBody(bool b, ArrayList v)
    {
        jj_consume_token(93);
        Token token = getToken(1);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    {
                        jj_la1[69] = jj_gen;
                        Token token2 = getToken(0);
                        jj_consume_token(94);
                        if (token2.next == token)
                        {
                            return;
                        }
                        Token token3 = token;
                        while (true)
                        {
                            v.Add(token3);
                            if (token3 == token2)
                            {
                                break;
                            }
                            token3 = token3.next;
                        }
                        return;
                    }
                case 27:
                case 29:
                case 31:
                case 34:
                case 35:
                case 40:
                case 42:
                case 45:
                case 47:
                case 54:
                case 55:
                case 56:
                case 57:
                case 61:
                case 62:
                case 63:
                case 65:
                case 66:
                case 67:
                case 70:
                case 74:
                case 77:
                case 78:
                case 93:
                case 97:
                case 101:
                case 139:
                case 140:
                    break;
            }
            ClassOrInterfaceBodyDeclaration(b);
        }
    }


    public void regular_expression(Container c)
    {
        int private_rexp = 0;
        Token token = getToken(1);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 90)
        {
            string str = StringLiteral();
            c.member = new RStringLiteral(token, str);
            return;
        }
        jj_la1[32] = jj_gen;
        if (jj_2_5(3))
        {
            string str = "";
            jj_consume_token(101);
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num == 133 || num == 140)
            {
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 133)
                {
                    jj_consume_token(133);
                    private_rexp = 1;
                }
                else
                {
                    jj_la1[30] = jj_gen;
                }
                str = identifier();
                jj_consume_token(105);
            }
            else
            {
                jj_la1[31] = jj_gen;
            }
            complex_regular_expression_choices(c);
            jj_consume_token(132);
            RegularExpression regularExpression;
            if (c.member is RJustName rj)
            {
                RSequence rSequence = new RSequence();
                rSequence.Units.Add(rj);
                regularExpression = rSequence;
            }
            else
            {
                regularExpression = (RegularExpression)c.member;
            }
            regularExpression.label = str;
            regularExpression.private_rexp = (byte)private_rexp != 0;
            regularExpression.Line = token.BeginLine;
            regularExpression.Column = token.BeginColumn;
            c.member = regularExpression;
        }
        else if (jj_2_6(2))
        {
            jj_consume_token(101);
            string str = identifier();
            jj_consume_token(132);
            c.member = new RJustName(token, str);
        }
        else
        {
            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) != 101)
            {
                jj_la1[33] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
            }
            jj_consume_token(101);
            jj_consume_token(11);
            jj_consume_token(132);
            REndOfFile rEndOfFile = new REndOfFile();
            rEndOfFile.Line = token.BeginLine;
            rEndOfFile.Column = token.BeginColumn;
            rEndOfFile.ordinal = 0;
            c.member = rEndOfFile;
        }
    }


    public void expansion(Container c)
    {
        Sequence sequence = new Sequence();
        Container container = new Container();
        Lookahead lookahead = new Lookahead();
        Token token = getToken(1);
        sequence.Line = token.BeginLine;
        sequence.Column = token.BeginColumn;
        lookahead.Line = token.BeginLine;
        lookahead.Column = token.BeginColumn;
        lookahead.amount = Options.Lookahead;
        lookahead.la_expansion = null;
        lookahead.isExplicit = false;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 1)
        {
            token = jj_consume_token(1);
            jj_consume_token(91);
            lookahead = local_lookahead();
            jj_consume_token(92);
            if (inLocalLA != 0 && lookahead.amount != 0)
            {
                JavaCCErrors.Warning(token, "Only semantic lookahead specifications within other lookahead specifications is considered.  Syntactic lookahead is ignored.");
            }
        }
        else
        {
            jj_la1[20] = jj_gen;
        }
        sequence.Units.Add(lookahead);
        do
        {
            expansion_unit(container);
            sequence.Units.Add(container.member as Expansion);
            ((Expansion)container.member).parent = sequence;
            ((Expansion)container.member).ordinal = sequence.Units.Count - 1;
        }
        while (notTailOfExpansionUnit());
        if (lookahead.la_expansion == null)
        {
            lookahead.la_expansion = sequence;
        }
        c.member = sequence;
    }


    public Lookahead local_lookahead()
    {
        Lookahead lookahead = new Lookahead();
        lookahead.isExplicit = true;
        Token token = getToken(1);
        lookahead.Line = token.BeginLine;
        lookahead.Column = token.BeginColumn;
        lookahead.la_expansion = null;
        Container container = new Container();
        int num = 0;
        int num2 = 1;
        inLocalLA++;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 80)
        {
            lookahead.amount = IntegerLiteral();
            num2 = 0;
        }
        else
        {
            jj_la1[21] = jj_gen;
        }
        if (num2 == 0 && getToken(1).kind != 92)
        {
            jj_consume_token(98);
            num = 1;
        }
        if (getToken(1).kind != 92 && getToken(1).kind != 93)
        {
            expansion_choices(container);
            num2 = 0;
            num = 0;
            lookahead.la_expansion = (Expansion)container.member;
        }
        if (num2 == 0 && num == 0 && getToken(1).kind != 92)
        {
            jj_consume_token(98);
            num = 1;
        }
        if (num2 != 0 || num != 0)
        {
            jj_consume_token(93);
            Expression(lookahead.action_tokens);
            jj_consume_token(94);
            if (num2 != 0)
            {
                lookahead.amount = 0;
            }
        }
        inLocalLA--;
        return lookahead;
    }


    public void expansion_unit(Container c)
    {
        List<Token> vector = new();
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
                {
                    Token obj = jj_consume_token(1);
                    jj_consume_token(91);
                    Lookahead lookahead = local_lookahead();
                    jj_consume_token(92);
                    lookahead.la_expansion = new REndOfFile();
                    Choice choice = new Choice(obj);
                    Sequence sequence = (Sequence)(lookahead.parent = new Sequence(obj, lookahead));
                    lookahead.ordinal = 0;
                    Action action = new Action();
                    action.Line = obj.BeginLine;
                    action.Column = obj.BeginColumn;
                    sequence.Units.Add(action);
                    action.parent = sequence;
                    action.ordinal = 1;
                    choice.Choices.Add(sequence);
                    sequence.parent = choice;
                    sequence.ordinal = 0;
                    if (lookahead.amount != 0)
                    {
                        if (lookahead.action_tokens.Count != 0)
                        {
                            JavaCCErrors.Warning(obj, "Encountered LOOKAHEAD(...) at a non-choice location.  Only semantic lookahead will be considered here.");
                        }
                        else
                        {
                            JavaCCErrors.Warning(obj, "Encountered LOOKAHEAD(...) at a non-choice location.  This will be ignored.");
                        }
                    }
                    c.member = choice;
                    return;
                }
            case 93:
                {
                    Action action = new Action();
                    Token obj = getToken(1);
                    action.Line = obj.BeginLine;
                    action.Column = obj.BeginColumn;
                    inAction = true;
                    Block(action.ActionTokens);
                    inAction = false;
                    if (inLocalLA != 0)
                    {
                        JavaCCErrors.Warning(obj, "Action within lookahead specification will be ignored.");
                    }
                    c.member = action;
                    return;
                }
            case 95:
                {
                    Token obj = jj_consume_token(95);
                    expansion_choices(c);
                    jj_consume_token(96);
                    c.member = new ZeroOrOne(obj, (Expansion)c.member);
                    return;
                }
            case 76:
                {
                    Container container = new Container();
                    var vector2 = new List<List<Token>>();
                    var vector3 = new List<Token>();
                    var vector4 = new List<List<Token>>();
                    List<Token> v = null;
                    var vector5 = new List<Token>();
                    Token t = jj_consume_token(76);
                    jj_consume_token(93);
                    expansion_choices(container);
                    jj_consume_token(94);
                    while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 33)
                    {
                        jj_consume_token(33);
                        jj_consume_token(91);
                        Name(vector5);
                        Token obj = jj_consume_token(140);
                        jj_consume_token(92);
                        vector2.Add(vector5);
                        vector3.Add(obj);
                        vector5 = new ();
                        inAction = true;
                        Block(vector5);
                        inAction = false;
                        vector4.Add(vector5);
                        vector5 = new ();
                    }
                    jj_la1[22] = jj_gen;
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 46)
                    {
                        inAction = true;
                        jj_consume_token(46);
                        Block(vector5);
                        inAction = false;
                        v = vector5;
                    }
                    else
                    {
                        jj_la1[23] = jj_gen;
                    }
                    JavaCCParserInternals.makeTryBlock(t, c, container, vector2, vector3, vector4, v);
                    return;
                }
        }
        jj_la1[28] = jj_gen;
        if (jj_2_4(int.MaxValue))
        {
            if (jj_2_2(int.MaxValue))
            {
                Token token = getToken(1);
                PrimaryExpression();
                Token token2 = getToken(0);
                jj_consume_token(100);
                Token obj = token;
                while (true)
                {
                    vector.Add(obj);
                    if (obj == token2)
                    {
                        break;
                    }
                    obj = obj.next;
                }
            }
            if (jj_2_3(int.MaxValue))
            {
                NonTerminal nonTerminal = new NonTerminal();
                Token obj = getToken(1);
                nonTerminal.Line = obj.BeginLine;
                nonTerminal.Column = obj.BeginColumn;
                nonTerminal.lhsTokens = vector;
                string name = identifier();
                Arguments(nonTerminal.argument_tokens);
                nonTerminal.name = name;
                c.member = nonTerminal;
                return;
            }
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num == 90 || num == 101)
            {
                regular_expression(c);
                ((RegularExpression)c.member).lhsTokens = vector;
                JavaCCParserInternals.add_inline_regexpr((RegularExpression)c.member);
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 99)
                {
                    jj_consume_token(99);
                    Token obj = jj_consume_token(140);
                    ((RegularExpression)c.member).rhsToken = obj;
                }
                else
                {
                    jj_la1[24] = jj_gen;
                }
                return;
            }
            jj_la1[25] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 91)
        {
            Token obj = jj_consume_token(91);
            expansion_choices(c);
            jj_consume_token(92);
            int num2 = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num2 == 104 || num2 == 114 || num2 == 116)
            {
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 114:
                        jj_consume_token(114);
                        c.member = new OneOrMore(obj, (Expansion)c.member);
                        break;
                    case 116:
                        jj_consume_token(116);
                        c.member = new ZeroOrMore(obj, (Expansion)c.member);
                        break;
                    case 104:
                        jj_consume_token(104);
                        c.member = new ZeroOrOne(obj, (Expansion)c.member);
                        break;
                    default:
                        jj_la1[26] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
            }
            else
            {
                jj_la1[27] = jj_gen;
            }
            return;
        }
        jj_la1[29] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool notTailOfExpansionUnit()
    {
        Token token = getToken(1);
        if (token.kind == 119 || token.kind == 98 || token.kind == 92 || token.kind == 94 || token.kind == 96)
        {
            return false;
        }
        return true;
    }


    public void Expression(List<Token> v)
    {
        Token token = getToken(1);
        ConditionalExpression();
        if (jj_2_24(2))
        {
            AssignmentOperator();
            Expression(new ());
        }
        Token token2 = getToken(0);
        Token token3 = token;
        while (true)
        {
            v.Add(token3);
            if (token3 == token2)
            {
                break;
            }
            token3 = token3.next;
        }
    }


    private bool jj_2_4(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_4()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(3, P_0);
            throw;
        }
        jj_save(3, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(3, P_0);
        }
    }


    private bool jj_2_2(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_2()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(1, P_0);
            throw;
        }
        jj_save(1, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(1, P_0);
        }
    }


    public void PrimaryExpression()
    {
        PrimaryPrefix();
        while (jj_2_32(2))
        {
            PrimarySuffix();
        }
    }


    private bool jj_2_3(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_3()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(2, P_0);
            throw;
        }
        jj_save(2, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(2, P_0);
        }
    }


    public void Arguments(List<Token> v)
    {
        jj_consume_token(91);
        Token token = getToken(1);
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 102:
            case 103:
            case 112:
            case 113:
            case 114:
            case 115:
            case 140:
                ArgumentList();
                break;
            default:
                jj_la1[132] = jj_gen;
                break;
        }
        Token token2 = getToken(0);
        jj_consume_token(92);
        if (token2.next == token)
        {
            return;
        }
        Token token3 = token;
        while (true)
        {
            v.Add(token3);
            if (token3 == token2)
            {
                break;
            }
            token3 = token3.next;
        }
    }


    private bool jj_2_5(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_5()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(4, P_0);
            throw;
        }
        jj_save(4, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(4, P_0);
        }
    }


    public void complex_regular_expression_choices(Container c)
    {
        int num = 0;
        RChoice rChoice = null;
        Container container = new Container();
        complex_regular_expression(c);
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 119)
        {
            jj_consume_token(119);
            complex_regular_expression(container);
            if (num != 0)
            {
                rChoice.Choices.Add(container.member as Expansion);
                continue;
            }
            num = 1;
            rChoice = new RChoice();
            rChoice.Line = ((RegularExpression)c.member).Line;
            rChoice.Column = ((RegularExpression)c.member).Column;
            rChoice.Choices.Add(c.member as Expansion);
            rChoice.Choices.Add(container.member as Expansion);
        }
        jj_la1[34] = jj_gen;
        if (num != 0)
        {
            c.member = rChoice;
        }
    }


    private bool jj_2_6(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_6()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(5, P_0);
            throw;
        }
        jj_save(5, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(5, P_0);
        }
    }


    public void complex_regular_expression(Container c)
    {
        int num = 0;
        RSequence rSequence = null;
        Container container = new Container();
        while (true)
        {
            complex_regular_expression_unit(container);
            num++;
            switch (num)
            {
                case 1:
                    c.member = container.member;
                    break;
                case 2:
                    rSequence = new RSequence();
                    rSequence.Line = ((RegularExpression)c.member).Line;
                    rSequence.Column = ((RegularExpression)c.member).Column;
                    rSequence.Units.Add(c.member as RegularExpression);
                    rSequence.Units.Add(container.member as RegularExpression);
                    break;
                default:
                    rSequence.Units.Add(container.member as RegularExpression);
                    break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 90:
                case 91:
                case 95:
                case 101:
                case 103:
                    continue;
            }
            jj_la1[35] = jj_gen;
            if (num > 1)
            {
                c.member = rSequence;
            }
            return;
        }
    }


    public void complex_regular_expression_unit(Container c)
    {
        Token token = getToken(1);
        _ = 0;
        int max = -1;
        int hasMax = 0;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 90:
                {
                    string str = StringLiteral();
                    c.member = new RStringLiteral(token, str);
                    break;
                }
            case 101:
                {
                    jj_consume_token(101);
                    string str = identifier();
                    jj_consume_token(132);
                    c.member = new RJustName(token, str);
                    break;
                }
            case 95:
            case 103:
                character_list(c);
                break;
            case 91:
                {
                    jj_consume_token(91);
                    complex_regular_expression_choices(c);
                    jj_consume_token(92);
                    int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                    if (num == 93 || num == 104 || num == 114 || num == 116)
                    {
                        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                        {
                            case 114:
                                jj_consume_token(114);

                                c.member = new ROneOrMore(token, (RegularExpression)c.member);
                                break;
                            case 116:
                                jj_consume_token(116);

                                c.member = new RZeroOrMore(token, (RegularExpression)c.member);
                                break;
                            case 104:
                                {
                                    jj_consume_token(104);
                                    RZeroOrOne rZeroOrOne = new RZeroOrOne();
                                    rZeroOrOne.Line = token.BeginLine;
                                    rZeroOrOne.Column = token.BeginColumn;
                                    rZeroOrOne.regexpr = (RegularExpression)c.member;
                                    c.member = rZeroOrOne;
                                    break;
                                }
                            case 93:
                                {
                                    jj_consume_token(93);
                                    int min = IntegerLiteral();
                                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
                                    {
                                        jj_consume_token(98);
                                        hasMax = 1;
                                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 80)
                                        {
                                            max = IntegerLiteral();
                                        }
                                        else
                                        {
                                            jj_la1[36] = jj_gen;
                                        }
                                    }
                                    else
                                    {
                                        jj_la1[37] = jj_gen;
                                    }
                                    jj_consume_token(94);
                                    RRepetitionRange rRepetitionRange = new RRepetitionRange();
                                    rRepetitionRange.Line = token.BeginLine;
                                    rRepetitionRange.Column = token.BeginColumn;
                                    rRepetitionRange.min = min;
                                    rRepetitionRange.max = max;
                                    rRepetitionRange.hasMax = (byte)hasMax != 0;
                                    rRepetitionRange.regexpr = (RegularExpression)c.member;
                                    c.member = rRepetitionRange;
                                    break;
                                }
                            default:
                                jj_la1[38] = jj_gen;
                                jj_consume_token(-1);

                                throw new ParseException();
                        }
                    }
                    else
                    {
                        jj_la1[39] = jj_gen;
                    }
                    break;
                }
            default:
                jj_la1[40] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void character_list(Container c)
    {
        RCharacterList rCharacterList = new RCharacterList();
        Token token = getToken(1);
        rCharacterList.Line = token.BeginLine;
        rCharacterList.Column = token.BeginColumn;
        Container container = new Container();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 103)
        {
            jj_consume_token(103);
            rCharacterList.negated_list = true;
        }
        else
        {
            jj_la1[41] = jj_gen;
        }
        jj_consume_token(95);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 90)
        {
            character_descriptor(container);
            rCharacterList.descriptors.Add(container.member);
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
            {
                jj_consume_token(98);
                character_descriptor(container);
                rCharacterList.descriptors.Add(container.member);
            }
            jj_la1[42] = jj_gen;
        }
        else
        {
            jj_la1[43] = jj_gen;
        }
        jj_consume_token(96);
        c.member = rCharacterList;
    }


    public void character_descriptor(Container c)
    {
        int right = 32;
        int num = 0;
        Token token = getToken(1);
        string text = StringLiteral();
        int num2 = JavaCCParserInternals.character_descriptor_assign(getToken(0), text);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 115)
        {
            jj_consume_token(115);
            string str = StringLiteral();
            num = 1;
            right = JavaCCParserInternals.character_descriptor_assign(getToken(0), str, text);
        }
        else
        {
            jj_la1[44] = jj_gen;
        }
        if (num != 0)
        {
            CharacterRange characterRange = new CharacterRange();
            characterRange.Line = token.BeginLine;
            characterRange.Column = token.BeginColumn;
            characterRange.Left = (char)num2;
            characterRange.Right = (char)right;
            c.member = characterRange;
        }
        else
        {
            SingleCharacter singleCharacter = new SingleCharacter();
            singleCharacter.line = token.BeginLine;
            singleCharacter.column = token.BeginColumn;
            singleCharacter.ch = (char)num2;
            c.member = singleCharacter;
        }
    }


    private bool jj_2_7(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_7()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(6, P_0);
            throw;
        }
        jj_save(6, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(6, P_0);
        }
    }


    public void PackageDeclaration()
    {
        Modifiers();
        jj_consume_token(60);
        Name(new ());
        jj_consume_token(97);
    }


    public void ImportDeclaration()
    {
        jj_consume_token(52);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 66)
        {
            jj_consume_token(66);
        }
        else
        {
            jj_la1[48] = jj_gen;
        }
        Name(new());
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 99)
        {
            jj_consume_token(99);
            jj_consume_token(116);
        }
        else
        {
            jj_la1[49] = jj_gen;
        }
        jj_consume_token(97);
    }


    public void TypeDeclaration()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 97:
                jj_consume_token(97);
                break;
            case 27:
            case 35:
            case 42:
            case 45:
            case 55:
            case 57:
            case 61:
            case 62:
            case 63:
            case 66:
            case 67:
            case 70:
            case 74:
            case 78:
            case 139:
                {
                    int i = Modifiers();
                    switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                    {
                        case 35:
                        case 55:
                            ClassOrInterfaceDeclaration(i, new ());
                            break;
                        case 42:
                            EnumDeclaration(i);
                            break;
                        case 139:
                            AnnotationTypeDeclaration(i);
                            break;
                        default:
                            jj_la1[51] = jj_gen;
                            jj_consume_token(-1);

                            throw new ParseException();
                    }
                    break;
                }
            default:
                jj_la1[52] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public int Modifiers()
    {
        int num = 0;
        while (jj_2_8(2))
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 63:
                    jj_consume_token(63);
                    num |= 1;
                    break;
                case 66:
                    jj_consume_token(66);
                    num |= 0x10;
                    break;
                case 62:
                    jj_consume_token(62);
                    num |= 2;
                    break;
                case 61:
                    jj_consume_token(61);
                    num |= 4;
                    break;
                case 45:
                    jj_consume_token(45);
                    num |= 0x20;
                    break;
                case 27:
                    jj_consume_token(27);
                    num |= 8;
                    break;
                case 70:
                    jj_consume_token(70);
                    num |= 0x40;
                    break;
                case 57:
                    jj_consume_token(57);
                    num |= 0x80;
                    break;
                case 74:
                    jj_consume_token(74);
                    num |= 0x100;
                    break;
                case 78:
                    jj_consume_token(78);
                    num |= 0x200;
                    break;
                case 67:
                    jj_consume_token(67);
                    num |= 0x1000;
                    break;
                case 139:
                    Annotation();
                    break;
                default:
                    jj_la1[50] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
        }
        return num;
    }


    private bool jj_2_8(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_8()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(7, P_0);
            throw;
        }
        jj_save(7, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(7, P_0);
        }
    }


    public void Annotation()
    {
        if (jj_2_45(int.MaxValue))
        {
            NormalAnnotation();
            return;
        }
        if (jj_2_46(int.MaxValue))
        {
            SingleMemberAnnotation();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 139)
        {
            MarkerAnnotation();
            return;
        }
        jj_la1[163] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ClassOrInterfaceDeclaration(int i, ArrayList v)
    {
        int b = 0;
        class_nesting++;
        int num = 0;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 35:
                jj_consume_token(35);
                break;
            case 55:
                jj_consume_token(55);
                b = 1;
                break;
            default:
                jj_la1[53] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        Token token = jj_consume_token(140);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 101)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[54] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 43)
        {
            ExtendsList((byte)b != 0);
        }
        else
        {
            jj_la1[55] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 51)
        {
            ImplementsList((byte)b != 0);
        }
        else
        {
            jj_la1[56] = jj_gen;
        }
        if (string.Equals(token.image, parser_class_name) && class_nesting == 1 && processing_cu)
        {
            num = 1;
            JavaCCParserInternals.SetInsertionPoint(getToken(1), 1);
        }
        ClassOrInterfaceBody((byte)b != 0, new ());
        if (num != 0)
        {
            JavaCCParserInternals.SetInsertionPoint(getToken(0), 2);
        }
        class_nesting--;
    }


    public void EnumDeclaration(int i)
    {
        jj_consume_token(42);
        jj_consume_token(140);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 51)
        {
            ImplementsList(b: false);
        }
        else
        {
            jj_la1[59] = jj_gen;
        }
        EnumBody();
    }


    public void AnnotationTypeDeclaration(int i)
    {
        jj_consume_token(139);
        jj_consume_token(55);
        jj_consume_token(140);
        AnnotationTypeBody();
    }


    public void TypeParameters()
    {
        jj_consume_token(101);
        TypeParameter();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            TypeParameter();
        }
        jj_la1[66] = jj_gen;
        jj_consume_token(132);
    }


    public void ExtendsList(bool b)
    {
        int num = 0;
        jj_consume_token(43);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            ClassOrInterfaceType();
            num = 1;
        }
        jj_la1[57] = jj_gen;
        if (num != 0 && !b)
        {

            throw new ParseException("A class cannot extend more than one other class");
        }
    }


    public void ImplementsList(bool b)
    {
        jj_consume_token(51);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            ClassOrInterfaceType();
        }
        jj_la1[58] = jj_gen;
        if (b)
        {

            throw new ParseException("An interface cannot implement other interfaces");
        }
    }


    public void ClassOrInterfaceType()
    {
        jj_consume_token(140);
        if (jj_2_20(2))
        {
            TypeArguments();
        }
        while (jj_2_21(2))
        {
            jj_consume_token(99);
            jj_consume_token(140);
            if (jj_2_22(2))
            {
                TypeArguments();
            }
        }
    }


    public void EnumBody()
    {
        jj_consume_token(93);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 27 || num == 45 || num == 57 || num == 61 || num == 62 || num == 63 || num == 66 || num == 67 || num == 70 || num == 74 || num == 78 || num == 139 || num == 140)
        {
            EnumConstant();
            while (jj_2_9(2))
            {
                jj_consume_token(98);
                EnumConstant();
            }
        }
        else
        {
            jj_la1[60] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
        }
        else
        {
            jj_la1[61] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 97)
        {
            jj_consume_token(97);
            while (true)
            {
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 27:
                    case 29:
                    case 31:
                    case 34:
                    case 35:
                    case 40:
                    case 42:
                    case 45:
                    case 47:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 61:
                    case 62:
                    case 63:
                    case 65:
                    case 66:
                    case 67:
                    case 70:
                    case 74:
                    case 77:
                    case 78:
                    case 93:
                    case 97:
                    case 101:
                    case 139:
                    case 140:
                        goto IL_0332;
                }
                break;
            IL_0332:
                ClassOrInterfaceBodyDeclaration(b: false);
            }
            jj_la1[62] = jj_gen;
        }
        else
        {
            jj_la1[63] = jj_gen;
        }
        jj_consume_token(94);
    }


    public void EnumConstant()
    {
        Modifiers();
        jj_consume_token(140);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 91)
        {
            Arguments(new ());
        }
        else
        {
            jj_la1[64] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 93)
        {
            ClassOrInterfaceBody(b: false, new ());
        }
        else
        {
            jj_la1[65] = jj_gen;
        }
    }


    private bool jj_2_9(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_9()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003c;
            }
        }
        catch
        {
            //try-fault
            jj_save(8, P_0);
            throw;
        }
        jj_save(8, P_0);
        return (byte)result != 0;
    IL_003c:

        try
        {
            return true;
        }
        finally
        {
            jj_save(8, P_0);
        }
    }


    public void ClassOrInterfaceBodyDeclaration(bool b)
    {
        if (jj_2_12(2))
        {
            Initializer();
            if (b)
            {

                throw new ParseException("An interface cannot have initializers");
            }
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 27:
            case 29:
            case 31:
            case 34:
            case 35:
            case 40:
            case 42:
            case 45:
            case 47:
            case 54:
            case 55:
            case 56:
            case 57:
            case 61:
            case 62:
            case 63:
            case 65:
            case 66:
            case 67:
            case 70:
            case 74:
            case 77:
            case 78:
            case 101:
            case 139:
            case 140:
                {
                    int i = Modifiers();
                    switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                    {
                        case 35:
                        case 55:
                            ClassOrInterfaceDeclaration(i, new ());
                            break;
                        case 42:
                            EnumDeclaration(i);
                            break;
                        default:
                            {
                                jj_la1[70] = jj_gen;
                                if (jj_2_10(int.MaxValue))
                                {
                                    ConstructorDeclaration();
                                    break;
                                }
                                if (jj_2_11(int.MaxValue))
                                {
                                    FieldDeclaration(i);
                                    break;
                                }
                                int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                                if (num == 29 || num == 31 || num == 34 || num == 40 || num == 47 || num == 54 || num == 56 || num == 65 || num == 77 || num == 101 || num == 140)
                                {
                                    MethodDeclaration(i);
                                    break;
                                }
                                jj_la1[71] = jj_gen;
                                jj_consume_token(-1);

                                throw new ParseException();
                            }
                    }
                    break;
                }
            case 97:
                jj_consume_token(97);
                break;
            default:
                jj_la1[72] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void TypeParameter()
    {
        jj_consume_token(140);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 43)
        {
            TypeBound();
        }
        else
        {
            jj_la1[67] = jj_gen;
        }
    }


    public void TypeBound()
    {
        jj_consume_token(43);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 118)
        {
            jj_consume_token(118);
            ClassOrInterfaceType();
        }
        jj_la1[68] = jj_gen;
    }


    private bool jj_2_12(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_12()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(11, P_0);
            throw;
        }
        jj_save(11, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(11, P_0);
        }
    }


    public void Initializer()
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 66)
        {
            jj_consume_token(66);
        }
        else
        {
            jj_la1[90] = jj_gen;
        }
        Block(new ());
    }


    private bool jj_2_10(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_10()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(9, P_0);
            throw;
        }
        jj_save(9, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(9, P_0);
        }
    }


    public void ConstructorDeclaration()
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 101)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[86] = jj_gen;
        }
        jj_consume_token(140);
        FormalParameters(new ());
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 73)
        {
            jj_consume_token(73);
            NameList();
        }
        else
        {
            jj_la1[87] = jj_gen;
        }
        jj_consume_token(93);
        if (jj_2_14(int.MaxValue))
        {
            ExplicitConstructorInvocation();
        }
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[88] = jj_gen;
                    jj_consume_token(94);
                    return;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 34:
                case 35:
                case 37:
                case 39:
                case 40:
                case 44:
                case 45:
                case 47:
                case 48:
                case 50:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 93:
                case 97:
                case 112:
                case 113:
                case 139:
                case 140:
                    break;
            }
            BlockStatement();
        }
    }


    private bool jj_2_11(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_11()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(10, P_0);
            throw;
        }
        jj_save(10, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(10, P_0);
        }
    }


    public void FieldDeclaration(int i)
    {
        Type();
        VariableDeclarator();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            VariableDeclarator();
        }
        jj_la1[73] = jj_gen;
        jj_consume_token(97);
    }


    public void MethodDeclaration(int i)
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 101)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[79] = jj_gen;
        }
        ResultType(new ());
        MethodDeclarator();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 73)
        {
            jj_consume_token(73);
            NameList();
        }
        else
        {
            jj_la1[80] = jj_gen;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 93:
                Block(new ());
                return;
            case 97:
                jj_consume_token(97);
                return;
        }
        jj_la1[81] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void Type()
    {
        if (jj_2_17(2))
        {
            ReferenceType();
            return;
        }
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 29 || num == 31 || num == 34 || num == 40 || num == 47 || num == 54 || num == 56 || num == 65)
        {
            PrimitiveType();
            return;
        }
        jj_la1[91] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void VariableDeclarator()
    {
        VariableDeclaratorId();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 100)
        {
            jj_consume_token(100);
            VariableInitializer();
        }
        else
        {
            jj_la1[74] = jj_gen;
        }
    }


    public void VariableDeclaratorId()
    {
        jj_consume_token(140);
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            jj_consume_token(95);
            jj_consume_token(96);
        }
        jj_la1[75] = jj_gen;
    }


    public void VariableInitializer()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 93:
                ArrayInitializer();
                return;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 102:
            case 103:
            case 112:
            case 113:
            case 114:
            case 115:
            case 140:
                Expression(new ());
                return;
        }
        jj_la1[76] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ArrayInitializer()
    {
        jj_consume_token(93);
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 93:
            case 102:
            case 103:
            case 112:
            case 113:
            case 114:
            case 115:
            case 140:
                VariableInitializer();
                while (jj_2_13(2))
                {
                    jj_consume_token(98);
                    VariableInitializer();
                }
                break;
            default:
                jj_la1[77] = jj_gen;
                break;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
        }
        else
        {
            jj_la1[78] = jj_gen;
        }
        jj_consume_token(94);
    }


    private bool jj_2_13(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_13()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(12, P_0);
            throw;
        }
        jj_save(12, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(12, P_0);
        }
    }


    public void MethodDeclarator()
    {
        jj_consume_token(140);
        FormalParameters(new ());
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            jj_consume_token(95);
            jj_consume_token(96);
        }
        jj_la1[82] = jj_gen;
    }


    public void NameList()
    {
        Name(new ());
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            Name(new ());
        }
        jj_la1[99] = jj_gen;
    }


    public void FormalParameter()
    {
        Modifiers();
        Type();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 134)
        {
            jj_consume_token(134);
        }
        else
        {
            jj_la1[85] = jj_gen;
        }
        VariableDeclaratorId();
    }


    private bool jj_2_14(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_14()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(13, P_0);
            throw;
        }
        jj_save(13, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(13, P_0);
        }
    }


    public void ExplicitConstructorInvocation()
    {
        if (jj_2_16(int.MaxValue))
        {
            jj_consume_token(71);
            Arguments(new ());
            jj_consume_token(97);
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 140:
                if (jj_2_15(2))
                {
                    PrimaryExpression();
                    jj_consume_token(99);
                }
                jj_consume_token(68);
                Arguments(new ());
                jj_consume_token(97);
                break;
            default:
                jj_la1[89] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void BlockStatement()
    {
        if (jj_2_42(int.MaxValue))
        {
            LocalVariableDeclaration();
            jj_consume_token(97);
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 28:
            case 29:
            case 30:
            case 31:
            case 34:
            case 37:
            case 39:
            case 40:
            case 44:
            case 47:
            case 48:
            case 50:
            case 54:
            case 56:
            case 58:
            case 59:
            case 64:
            case 65:
            case 68:
            case 69:
            case 70:
            case 71:
            case 72:
            case 75:
            case 76:
            case 77:
            case 79:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 93:
            case 97:
            case 112:
            case 113:
            case 140:
                Statement();
                return;
            case 35:
            case 55:
                ClassOrInterfaceDeclaration(0, new ());
                return;
        }
        jj_la1[143] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_16(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_16()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(15, P_0);
            throw;
        }
        jj_save(15, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(15, P_0);
        }
    }


    private bool jj_2_15(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_15()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(14, P_0);
            throw;
        }
        jj_save(14, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(14, P_0);
        }
    }


    private bool jj_2_17(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_17()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(16, P_0);
            throw;
        }
        jj_save(16, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(16, P_0);
        }
    }


    public void ReferenceType()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 29:
            case 31:
            case 34:
            case 40:
            case 47:
            case 54:
            case 56:
            case 65:
                PrimitiveType();
                do
                {
                    jj_consume_token(95);
                    jj_consume_token(96);
                }
                while (jj_2_18(2));
                break;
            case 140:
                ClassOrInterfaceType();
                while (jj_2_19(2))
                {
                    jj_consume_token(95);
                    jj_consume_token(96);
                }
                break;
            default:
                jj_la1[92] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void PrimitiveType()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 29:
                jj_consume_token(29);
                return;
            case 34:
                jj_consume_token(34);
                return;
            case 31:
                jj_consume_token(31);
                return;
            case 65:
                jj_consume_token(65);
                return;
            case 54:
                jj_consume_token(54);
                return;
            case 56:
                jj_consume_token(56);
                return;
            case 47:
                jj_consume_token(47);
                return;
            case 40:
                jj_consume_token(40);
                return;
        }
        jj_la1[97] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_18(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_18()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(17, P_0);
            throw;
        }
        jj_save(17, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(17, P_0);
        }
    }


    private bool jj_2_19(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_19()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(18, P_0);
            throw;
        }
        jj_save(18, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(18, P_0);
        }
    }


    private bool jj_2_20(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_20()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(19, P_0);
            throw;
        }
        jj_save(19, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(19, P_0);
        }
    }


    public void TypeArguments()
    {
        jj_consume_token(101);
        TypeArgument();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            TypeArgument();
        }
        jj_la1[93] = jj_gen;
        jj_consume_token(132);
    }


    private bool jj_2_21(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_21()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(20, P_0);
            throw;
        }
        jj_save(20, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(20, P_0);
        }
    }


    private bool jj_2_22(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_22()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(21, P_0);
            throw;
        }
        jj_save(21, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(21, P_0);
        }
    }


    public void TypeArgument()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 29:
            case 31:
            case 34:
            case 40:
            case 47:
            case 54:
            case 56:
            case 65:
            case 140:
                ReferenceType();
                break;
            case 104:
                {
                    jj_consume_token(104);
                    int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                    if (num == 43 || num == 68)
                    {
                        WildcardBounds();
                    }
                    else
                    {
                        jj_la1[94] = jj_gen;
                    }
                    break;
                }
            default:
                jj_la1[95] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void WildcardBounds()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 43:
                jj_consume_token(43);
                ReferenceType();
                break;
            case 68:
                jj_consume_token(68);
                ReferenceType();
                break;
            default:
                jj_la1[96] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public Token JavaIdentifier()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 140:
                jj_consume_token(140);
                break;
            case 1:
                jj_consume_token(1);
                break;
            case 2:
                jj_consume_token(2);
                break;
            case 3:
                jj_consume_token(3);
                break;
            case 4:
                jj_consume_token(4);
                break;
            case 5:
                jj_consume_token(5);
                break;
            case 6:
                jj_consume_token(6);
                break;
            case 7:
                jj_consume_token(7);
                break;
            case 8:
                jj_consume_token(8);
                break;
            case 9:
                jj_consume_token(9);
                break;
            case 10:
                jj_consume_token(10);
                break;
            case 11:
                jj_consume_token(11);
                break;
            default:
                jj_la1[45] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        Token token = getToken(0);
        token.kind = 140;
        return token;
    }


    private bool jj_2_23(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_23()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(22, P_0);
            throw;
        }
        jj_save(22, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(22, P_0);
        }
    }


    public void ConditionalExpression()
    {
        ConditionalOrExpression();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 104)
        {
            jj_consume_token(104);
            Expression(new ());
            jj_consume_token(105);
            Expression(new ());
        }
        else
        {
            jj_la1[101] = jj_gen;
        }
    }


    private bool jj_2_24(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_24()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(23, P_0);
            throw;
        }
        jj_save(23, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(23, P_0);
        }
    }


    public void AssignmentOperator()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 100:
                jj_consume_token(100);
                return;
            case 124:
                jj_consume_token(124);
                return;
            case 125:
                jj_consume_token(125);
                return;
            case 129:
                jj_consume_token(129);
                return;
            case 122:
                jj_consume_token(122);
                return;
            case 123:
                jj_consume_token(123);
                return;
            case 135:
                jj_consume_token(135);
                return;
            case 136:
                jj_consume_token(136);
                return;
            case 137:
                jj_consume_token(137);
                return;
            case 126:
                jj_consume_token(126);
                return;
            case 128:
                jj_consume_token(128);
                return;
            case 127:
                jj_consume_token(127);
                return;
        }
        jj_la1[100] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ConditionalOrExpression()
    {
        ConditionalAndExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 110)
        {
            jj_consume_token(110);
            ConditionalAndExpression();
        }
        jj_la1[102] = jj_gen;
    }


    public void ConditionalAndExpression()
    {
        InclusiveOrExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 111)
        {
            jj_consume_token(111);
            InclusiveOrExpression();
        }
        jj_la1[103] = jj_gen;
    }


    public void InclusiveOrExpression()
    {
        ExclusiveOrExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 119)
        {
            jj_consume_token(119);
            ExclusiveOrExpression();
        }
        jj_la1[104] = jj_gen;
    }


    public void ExclusiveOrExpression()
    {
        AndExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 120)
        {
            jj_consume_token(120);
            AndExpression();
        }
        jj_la1[105] = jj_gen;
    }


    public void AndExpression()
    {
        EqualityExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 118)
        {
            jj_consume_token(118);
            EqualityExpression();
        }
        jj_la1[106] = jj_gen;
    }


    public void EqualityExpression()
    {
        InstanceOfExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 106 && num != 109)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 106:
                    jj_consume_token(106);
                    break;
                case 109:
                    jj_consume_token(109);
                    break;
                default:
                    jj_la1[108] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            InstanceOfExpression();
        }
        jj_la1[107] = jj_gen;
    }


    public void InstanceOfExpression()
    {
        RelationalExpression();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 53)
        {
            jj_consume_token(53);
            Type();
        }
        else
        {
            jj_la1[109] = jj_gen;
        }
    }


    public void RelationalExpression()
    {
        ShiftExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 101 && num != 107 && num != 108 && num != 132)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 101:
                    jj_consume_token(101);
                    break;
                case 132:
                    jj_consume_token(132);
                    break;
                case 107:
                    jj_consume_token(107);
                    break;
                case 108:
                    jj_consume_token(108);
                    break;
                default:
                    jj_la1[111] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            ShiftExpression();
        }
        jj_la1[110] = jj_gen;
    }


    public void ShiftExpression()
    {
        AdditiveExpression();
        while (jj_2_25(1))
        {
            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 138)
            {
                jj_consume_token(138);
            }
            else
            {
                jj_la1[112] = jj_gen;
                if (jj_2_26(1))
                {
                    RSIGNEDSHIFT();
                }
                else
                {
                    if (!jj_2_27(1))
                    {
                        jj_consume_token(-1);

                        throw new ParseException();
                    }
                    RUNSIGNEDSHIFT();
                }
            }
            AdditiveExpression();
        }
    }


    public void AdditiveExpression()
    {
        MultiplicativeExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 114 && num != 115)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 114:
                    jj_consume_token(114);
                    break;
                case 115:
                    jj_consume_token(115);
                    break;
                default:
                    jj_la1[114] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            MultiplicativeExpression();
        }
        jj_la1[113] = jj_gen;
    }


    private bool jj_2_25(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_25()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(24, P_0);
            throw;
        }
        jj_save(24, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(24, P_0);
        }
    }


    private bool jj_2_26(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_26()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(25, P_0);
            throw;
        }
        jj_save(25, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(25, P_0);
        }
    }


    public void RSIGNEDSHIFT()
    {
        if (getToken(1).kind != 132 || ((Token.GTToken)getToken(1)).realKind != 131)
        {
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_consume_token(132);
        jj_consume_token(132);
    }


    private bool jj_2_27(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_27()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(26, P_0);
            throw;
        }
        jj_save(26, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(26, P_0);
        }
    }


    public void RUNSIGNEDSHIFT()
    {
        if (getToken(1).kind != 132 || ((Token.GTToken)getToken(1)).realKind != 130)
        {
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_consume_token(132);
        jj_consume_token(132);
        jj_consume_token(132);
    }


    public void MultiplicativeExpression()
    {
        UnaryExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 116 && num != 117 && num != 121)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 116:
                    jj_consume_token(116);
                    break;
                case 117:
                    jj_consume_token(117);
                    break;
                case 121:
                    jj_consume_token(121);
                    break;
                default:
                    jj_la1[116] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            UnaryExpression();
        }
        jj_la1[115] = jj_gen;
    }


    public void UnaryExpression()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 114:
            case 115:
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 114:
                        jj_consume_token(114);
                        break;
                    case 115:
                        jj_consume_token(115);
                        break;
                    default:
                        jj_la1[117] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                UnaryExpression();
                break;
            case 112:
                PreIncrementExpression();
                break;
            case 113:
                PreDecrementExpression();
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 102:
            case 103:
            case 140:
                UnaryExpressionNotPlusMinus();
                break;
            default:
                jj_la1[118] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void PreIncrementExpression()
    {
        jj_consume_token(112);
        PrimaryExpression();
    }


    public void PreDecrementExpression()
    {
        jj_consume_token(113);
        PrimaryExpression();
    }


    public void UnaryExpressionNotPlusMinus()
    {
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 102 || num == 103)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 103:
                    jj_consume_token(103);
                    break;
                case 102:
                    jj_consume_token(102);
                    break;
                default:
                    jj_la1[119] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            UnaryExpression();
            return;
        }
        jj_la1[120] = jj_gen;
        if (jj_2_28(int.MaxValue))
        {
            CastExpression();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 140:
                PostfixExpression();
                return;
        }
        jj_la1[121] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_28(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_28()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(27, P_0);
            throw;
        }
        jj_save(27, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(27, P_0);
        }
    }


    public void CastExpression()
    {
        if (jj_2_31(int.MaxValue))
        {
            jj_consume_token(91);
            Type();
            jj_consume_token(92);
            UnaryExpression();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 91)
        {
            jj_consume_token(91);
            Type();
            jj_consume_token(92);
            UnaryExpressionNotPlusMinus();
            return;
        }
        jj_la1[126] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void PostfixExpression()
    {
        PrimaryExpression();
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 112 || num == 113)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 112:
                    jj_consume_token(112);
                    return;
                case 113:
                    jj_consume_token(113);
                    return;
            }
            jj_la1[124] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_la1[125] = jj_gen;
    }


    private bool jj_2_29(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_29()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(28, P_0);
            throw;
        }
        jj_save(28, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(28, P_0);
        }
    }


    private bool jj_2_30(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_30()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(29, P_0);
            throw;
        }
        jj_save(29, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(29, P_0);
        }
    }


    public void Literal()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 80:
                jj_consume_token(80);
                return;
            case 84:
                jj_consume_token(84);
                return;
            case 89:
                jj_consume_token(89);
                return;
            case 90:
                jj_consume_token(90);
                return;
            case 44:
            case 75:
                BooleanLiteral();
                return;
            case 59:
                NullLiteral();
                return;
        }
        jj_la1[130] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_31(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_31()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(30, P_0);
            throw;
        }
        jj_save(30, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(30, P_0);
        }
    }


    public void PrimaryPrefix()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 44:
            case 59:
            case 75:
            case 80:
            case 84:
            case 89:
            case 90:
                Literal();
                return;
            case 71:
                jj_consume_token(71);
                return;
            case 68:
                jj_consume_token(68);
                jj_consume_token(99);
                jj_consume_token(140);
                return;
            case 91:
                jj_consume_token(91);
                Expression(new ());
                jj_consume_token(92);
                return;
            case 58:
                AllocationExpression();
                return;
        }
        jj_la1[127] = jj_gen;
        if (jj_2_33(int.MaxValue))
        {
            ResultType(new ());
            jj_consume_token(99);
            jj_consume_token(35);
            return;
        }
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 140)
        {
            Name(new ());
            return;
        }
        jj_la1[128] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_32(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_32()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(31, P_0);
            throw;
        }
        jj_save(31, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(31, P_0);
        }
    }


    public void PrimarySuffix()
    {
        if (jj_2_34(2))
        {
            jj_consume_token(99);
            jj_consume_token(71);
            return;
        }
        if (jj_2_35(2))
        {
            jj_consume_token(99);
            AllocationExpression();
            return;
        }
        if (jj_2_36(3))
        {
            MemberSelector();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 95:
                jj_consume_token(95);
                Expression(new ());
                jj_consume_token(96);
                break;
            case 99:
                jj_consume_token(99);
                jj_consume_token(140);
                break;
            case 91:
                Arguments(new ());
                break;
            default:
                jj_la1[129] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void AllocationExpression()
    {
        if (jj_2_37(2))
        {
            jj_consume_token(58);
            PrimitiveType();
            ArrayDimsAndInits();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 58)
        {
            jj_consume_token(58);
            ClassOrInterfaceType();
            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 101)
            {
                TypeArguments();
            }
            else
            {
                jj_la1[134] = jj_gen;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 95:
                    ArrayDimsAndInits();
                    break;
                case 91:
                    Arguments(new ());
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 93)
                    {
                        ClassOrInterfaceBody(b: false, new ());
                    }
                    else
                    {
                        jj_la1[135] = jj_gen;
                    }
                    break;
                default:
                    jj_la1[136] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            return;
        }
        jj_la1[137] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_33(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_33()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(32, P_0);
            throw;
        }
        jj_save(32, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(32, P_0);
        }
    }


    private bool jj_2_34(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_34()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(33, P_0);
            throw;
        }
        jj_save(33, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(33, P_0);
        }
    }


    private bool jj_2_35(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_35()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(34, P_0);
            throw;
        }
        jj_save(34, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(34, P_0);
        }
    }


    private bool jj_2_36(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_36()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(35, P_0);
            throw;
        }
        jj_save(35, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(35, P_0);
        }
    }


    public void MemberSelector()
    {
        jj_consume_token(99);
        TypeArguments();
        jj_consume_token(140);
    }


    public void NullLiteral()
    {
        jj_consume_token(59);
    }


    public void ArgumentList()
    {
        Expression(new ());
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            Expression(new ());
        }
        jj_la1[133] = jj_gen;
    }


    private bool jj_2_37(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_37()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(36, P_0);
            throw;
        }
        jj_save(36, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(36, P_0);
        }
    }


    public void ArrayDimsAndInits()
    {
        if (jj_2_40(2))
        {
            do
            {
                jj_consume_token(95);
                Expression(new ());
                jj_consume_token(96);
            }
            while (jj_2_38(2));
            while (jj_2_39(2))
            {
                jj_consume_token(95);
                jj_consume_token(96);
            }
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            do
            {
                jj_consume_token(95);
                jj_consume_token(96);
            }
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95);
            jj_la1[138] = jj_gen;
            ArrayInitializer();
            return;
        }
        jj_la1[139] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private bool jj_2_40(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_40()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(39, P_0);
            throw;
        }
        jj_save(39, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(39, P_0);
        }
    }


    private bool jj_2_38(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_38()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(37, P_0);
            throw;
        }
        jj_save(37, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(37, P_0);
        }
    }


    private bool jj_2_39(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_39()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(38, P_0);
            throw;
        }
        jj_save(38, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(38, P_0);
        }
    }


    private bool jj_2_41(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_41()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(40, P_0);
            throw;
        }
        jj_save(40, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(40, P_0);
        }
    }


    public void LabeledStatement()
    {
        jj_consume_token(140);
        jj_consume_token(105);
        Statement();
    }


    public void AssertStatement()
    {
        jj_consume_token(28);
        Expression(new ());
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 105)
        {
            jj_consume_token(105);
            Expression(new ());
        }
        else
        {
            jj_la1[141] = jj_gen;
        }
        jj_consume_token(97);
    }


    public void EmptyStatement()
    {
        jj_consume_token(97);
    }


    public void StatementExpression()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 112:
                PreIncrementExpression();
                break;
            case 113:
                PreDecrementExpression();
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 140:
                PrimaryExpression();
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 100:
                    case 112:
                    case 113:
                    case 122:
                    case 123:
                    case 124:
                    case 125:
                    case 126:
                    case 127:
                    case 128:
                    case 129:
                    case 135:
                    case 136:
                    case 137:
                        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                        {
                            case 112:
                                jj_consume_token(112);
                                break;
                            case 113:
                                jj_consume_token(113);
                                break;
                            case 100:
                            case 122:
                            case 123:
                            case 124:
                            case 125:
                            case 126:
                            case 127:
                            case 128:
                            case 129:
                            case 135:
                            case 136:
                            case 137:
                                AssignmentOperator();
                                Expression(new ());
                                break;
                            default:
                                jj_la1[145] = jj_gen;
                                jj_consume_token(-1);

                                throw new ParseException();
                        }
                        break;
                    default:
                        jj_la1[146] = jj_gen;
                        break;
                }
                break;
            default:
                jj_la1[147] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void SwitchStatement()
    {
        jj_consume_token(69);
        jj_consume_token(91);
        Expression(new ());
        jj_consume_token(92);
        jj_consume_token(93);
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 32 && num != 38)
            {
                break;
            }
            SwitchLabel();
            while (true)
            {
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 34:
                    case 35:
                    case 37:
                    case 39:
                    case 40:
                    case 44:
                    case 45:
                    case 47:
                    case 48:
                    case 50:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                    case 71:
                    case 72:
                    case 74:
                    case 75:
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                    case 80:
                    case 84:
                    case 89:
                    case 90:
                    case 91:
                    case 93:
                    case 97:
                    case 112:
                    case 113:
                    case 139:
                    case 140:
                        goto IL_02e1;
                }
                break;
            IL_02e1:
                BlockStatement();
            }
            jj_la1[149] = jj_gen;
        }
        jj_la1[148] = jj_gen;
        jj_consume_token(94);
    }


    public void IfStatement()
    {
        jj_consume_token(50);
        jj_consume_token(91);
        Expression(new ());
        jj_consume_token(92);
        Statement();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 41)
        {
            jj_consume_token(41);
            Statement();
        }
        else
        {
            jj_la1[151] = jj_gen;
        }
    }


    public void WhileStatement()
    {
        jj_consume_token(79);
        jj_consume_token(91);
        Expression(new ());
        jj_consume_token(92);
        Statement();
    }


    public void DoStatement()
    {
        jj_consume_token(39);
        Statement();
        jj_consume_token(79);
        jj_consume_token(91);
        Expression(new ());
        jj_consume_token(92);
        jj_consume_token(97);
    }


    public void ForStatement()
    {
        jj_consume_token(48);
        jj_consume_token(91);
        if (jj_2_43(int.MaxValue))
        {
            Modifiers();
            Type();
            jj_consume_token(140);
            jj_consume_token(105);
            Expression(new ());
        }
        else
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 27:
                case 29:
                case 31:
                case 34:
                case 40:
                case 44:
                case 45:
                case 47:
                case 54:
                case 56:
                case 57:
                case 58:
                case 59:
                case 61:
                case 62:
                case 63:
                case 65:
                case 66:
                case 67:
                case 68:
                case 70:
                case 71:
                case 74:
                case 75:
                case 77:
                case 78:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 97:
                case 112:
                case 113:
                case 139:
                case 140:
                    break;
                default:
                    jj_la1[155] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 27:
                case 29:
                case 31:
                case 34:
                case 40:
                case 44:
                case 45:
                case 47:
                case 54:
                case 56:
                case 57:
                case 58:
                case 59:
                case 61:
                case 62:
                case 63:
                case 65:
                case 66:
                case 67:
                case 68:
                case 70:
                case 71:
                case 74:
                case 75:
                case 77:
                case 78:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 112:
                case 113:
                case 139:
                case 140:
                    ForInit();
                    break;
                default:
                    jj_la1[152] = jj_gen;
                    break;
            }
            jj_consume_token(97);
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 29:
                case 31:
                case 34:
                case 40:
                case 44:
                case 47:
                case 54:
                case 56:
                case 58:
                case 59:
                case 65:
                case 68:
                case 71:
                case 75:
                case 77:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 102:
                case 103:
                case 112:
                case 113:
                case 114:
                case 115:
                case 140:
                    Expression(new ());
                    break;
                default:
                    jj_la1[153] = jj_gen;
                    break;
            }
            jj_consume_token(97);
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 29:
                case 31:
                case 34:
                case 40:
                case 44:
                case 47:
                case 54:
                case 56:
                case 58:
                case 59:
                case 65:
                case 68:
                case 71:
                case 75:
                case 77:
                case 80:
                case 84:
                case 89:
                case 90:
                case 91:
                case 112:
                case 113:
                case 140:
                    ForUpdate();
                    break;
                default:
                    jj_la1[154] = jj_gen;
                    break;
            }
        }
        jj_consume_token(92);
        Statement();
    }


    public void BreakStatement()
    {
        jj_consume_token(30);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 140)
        {
            jj_consume_token(140);
        }
        else
        {
            jj_la1[158] = jj_gen;
        }
        jj_consume_token(97);
    }


    public void ContinueStatement()
    {
        jj_consume_token(37);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 140)
        {
            jj_consume_token(140);
        }
        else
        {
            jj_la1[159] = jj_gen;
        }
        jj_consume_token(97);
    }


    public void ReturnStatement()
    {
        Token token = jj_consume_token(64);
        if (inAction)
        {
            token.image = "{if (true) return";
            jumpPatched = true;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 102:
            case 103:
            case 112:
            case 113:
            case 114:
            case 115:
            case 140:
                Expression(new ());
                break;
            default:
                jj_la1[160] = jj_gen;
                break;
        }
        token = jj_consume_token(97);
        if (inAction)
        {
            token.image = ";}";
        }
    }


    public void ThrowStatement()
    {
        Token token = jj_consume_token(72);
        if (inAction)
        {
            token.image = "{if (true) throw";
            jumpPatched = true;
        }
        Expression(new ());
        token = jj_consume_token(97);
        if (inAction)
        {
            token.image = ";}";
        }
    }


    public void SynchronizedStatement()
    {
        jj_consume_token(70);
        jj_consume_token(91);
        Expression(new ());
        jj_consume_token(92);
        Block(new ());
    }


    public void TryStatement()
    {
        jj_consume_token(76);
        Block(new ());
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 33)
        {
            jj_consume_token(33);
            jj_consume_token(91);
            FormalParameter();
            jj_consume_token(92);
            Block(new ());
        }
        jj_la1[161] = jj_gen;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 46)
        {
            jj_consume_token(46);
            Block(new ());
        }
        else
        {
            jj_la1[162] = jj_gen;
        }
    }


    public void Statement()
    {
        if (jj_2_41(2))
        {
            LabeledStatement();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 28:
                AssertStatement();
                break;
            case 93:
                Block(new ());
                break;
            case 97:
                EmptyStatement();
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 112:
            case 113:
            case 140:
                StatementExpression();
                jj_consume_token(97);
                break;
            case 69:
                SwitchStatement();
                break;
            case 50:
                IfStatement();
                break;
            case 79:
                WhileStatement();
                break;
            case 39:
                DoStatement();
                break;
            case 48:
                ForStatement();
                break;
            case 30:
                BreakStatement();
                break;
            case 37:
                ContinueStatement();
                break;
            case 64:
                ReturnStatement();
                break;
            case 72:
                ThrowStatement();
                break;
            case 70:
                SynchronizedStatement();
                break;
            case 76:
                TryStatement();
                break;
            default:
                jj_la1[140] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    private bool jj_2_42(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_42()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(41, P_0);
            throw;
        }
        jj_save(41, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(41, P_0);
        }
    }


    public void LocalVariableDeclaration()
    {
        Modifiers();
        Type();
        VariableDeclarator();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            VariableDeclarator();
        }
        jj_la1[144] = jj_gen;
    }


    public void SwitchLabel()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 32:
                jj_consume_token(32);
                Expression(new ());
                jj_consume_token(105);
                break;
            case 38:
                jj_consume_token(38);
                jj_consume_token(105);
                break;
            default:
                jj_la1[150] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    private bool jj_2_43(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_43()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(42, P_0);
            throw;
        }
        jj_save(42, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(42, P_0);
        }
    }


    public void ForInit()
    {
        if (jj_2_44(int.MaxValue))
        {
            LocalVariableDeclaration();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 112:
            case 113:
            case 140:
                StatementExpressionList();
                return;
        }
        jj_la1[156] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ForUpdate()
    {
        StatementExpressionList();
    }


    private bool jj_2_44(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_44()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(43, P_0);
            throw;
        }
        jj_save(43, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(43, P_0);
        }
    }


    public void StatementExpressionList()
    {
        StatementExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            StatementExpression();
        }
        jj_la1[157] = jj_gen;
    }


    private bool jj_2_45(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_45()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(44, P_0);
            throw;
        }
        jj_save(44, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(44, P_0);
        }
    }


    public void NormalAnnotation()
    {
        jj_consume_token(139);
        Name(new ());
        jj_consume_token(91);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 140)
        {
            MemberValuePairs();
        }
        else
        {
            jj_la1[164] = jj_gen;
        }
        jj_consume_token(92);
    }


    private bool jj_2_46(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_46()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(45, P_0);
            throw;
        }
        jj_save(45, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(45, P_0);
        }
    }


    public void SingleMemberAnnotation()
    {
        jj_consume_token(139);
        Name(new ());
        jj_consume_token(91);
        MemberValue();
        jj_consume_token(92);
    }


    public void MarkerAnnotation()
    {
        jj_consume_token(139);
        Name(new ());
    }


    public void MemberValuePairs()
    {
        MemberValuePair();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            MemberValuePair();
        }
        jj_la1[165] = jj_gen;
    }


    public void MemberValue()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 139:
                Annotation();
                return;
            case 93:
                MemberValueArrayInitializer();
                return;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 29:
            case 31:
            case 34:
            case 40:
            case 44:
            case 47:
            case 54:
            case 56:
            case 58:
            case 59:
            case 65:
            case 68:
            case 71:
            case 75:
            case 77:
            case 80:
            case 84:
            case 89:
            case 90:
            case 91:
            case 102:
            case 103:
            case 112:
            case 113:
            case 114:
            case 115:
            case 140:
                ConditionalExpression();
                return;
        }
        jj_la1[166] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void MemberValuePair()
    {
        jj_consume_token(140);
        jj_consume_token(100);
        MemberValue();
    }


    public void MemberValueArrayInitializer()
    {
        jj_consume_token(93);
        MemberValue();
        while (jj_2_47(2))
        {
            jj_consume_token(98);
            MemberValue();
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
        }
        else
        {
            jj_la1[167] = jj_gen;
        }
        jj_consume_token(94);
    }


    private bool jj_2_47(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_47()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(46, P_0);
            throw;
        }
        jj_save(46, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(46, P_0);
        }
    }


    public void AnnotationTypeBody()
    {
        jj_consume_token(93);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[168] = jj_gen;
                    jj_consume_token(94);
                    return;
                case 27:
                case 29:
                case 31:
                case 34:
                case 35:
                case 40:
                case 42:
                case 45:
                case 47:
                case 54:
                case 55:
                case 56:
                case 57:
                case 61:
                case 62:
                case 63:
                case 65:
                case 66:
                case 67:
                case 70:
                case 74:
                case 78:
                case 97:
                case 139:
                case 140:
                    break;
            }
            AnnotationTypeMemberDeclaration();
        }
    }


    public void AnnotationTypeMemberDeclaration()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 27:
            case 29:
            case 31:
            case 34:
            case 35:
            case 40:
            case 42:
            case 45:
            case 47:
            case 54:
            case 55:
            case 56:
            case 57:
            case 61:
            case 62:
            case 63:
            case 65:
            case 66:
            case 67:
            case 70:
            case 74:
            case 78:
            case 139:
            case 140:
                {
                    int i = Modifiers();
                    if (jj_2_48(int.MaxValue))
                    {
                        Type();
                        jj_consume_token(140);
                        jj_consume_token(91);
                        jj_consume_token(92);
                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 38)
                        {
                            DefaultValue();
                        }
                        else
                        {
                            jj_la1[169] = jj_gen;
                        }
                        jj_consume_token(97);
                        break;
                    }
                    switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                    {
                        case 35:
                        case 55:
                            ClassOrInterfaceDeclaration(i, new ());
                            break;
                        case 42:
                            EnumDeclaration(i);
                            break;
                        case 139:
                            AnnotationTypeDeclaration(i);
                            break;
                        case 29:
                        case 31:
                        case 34:
                        case 40:
                        case 47:
                        case 54:
                        case 56:
                        case 65:
                        case 140:
                            FieldDeclaration(i);
                            break;
                        default:
                            jj_la1[170] = jj_gen;
                            jj_consume_token(-1);

                            throw new ParseException();
                    }
                    break;
                }
            case 97:
                jj_consume_token(97);
                break;
            default:
                jj_la1[171] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    private bool jj_2_48(int P_0)
    {
        jj_la = P_0;
        Token token = this.token;
        jj_scanpos = token;
        jj_lastpos = token;
        int result;
        try
        {
            try
            {
                result = ((!jj_3_48()) ? 1 : 0);
            }
            catch (LookaheadSuccess)
            {
                goto IL_003d;
            }
        }
        catch
        {
            //try-fault
            jj_save(47, P_0);
            throw;
        }
        jj_save(47, P_0);
        return (byte)result != 0;
    IL_003d:

        try
        {
            return true;
        }
        finally
        {
            jj_save(47, P_0);
        }
    }


    public void DefaultValue()
    {
        jj_consume_token(38);
        MemberValue();
    }


    private bool jj_3_1()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        if (jj_scan_token(116))
        {
            return true;
        }
        return false;
    }


    private void jj_save(int P_0, int P_1)
    {
        JJCalls jJCalls = jj_2_rtns[P_0];
        while (jJCalls.gen > jj_gen)
        {
            if (jJCalls.next == null)
            {
                JJCalls jJCalls2 = jJCalls;
                JJCalls jJCalls3 = new JJCalls();
                JJCalls jJCalls4 = jJCalls2;
                jJCalls4.next = jJCalls3;
                jJCalls = jJCalls3;
                break;
            }
            jJCalls = jJCalls.next;
        }
        jJCalls.gen = jj_gen + P_1 - jj_la;
        jJCalls.first = token;
        jJCalls.arg = P_1;
    }


    private bool jj_3_2()
    {
        if (jj_3R_59())
        {
            return true;
        }
        if (jj_scan_token(100))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_3()
    {
        if (jj_3R_60())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_4()
    {
        Token token = jj_scanpos;
        if (jj_3R_61())
        {
            jj_scanpos = token;
            if (jj_3R_62())
            {
                jj_scanpos = token;
                if (jj_scan_token(101))
                {
                    jj_scanpos = token;
                    if (jj_3R_63())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3_5()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_64())
        {
            jj_scanpos = token;
        }
        if (jj_3R_65())
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_6()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        if (jj_3R_60())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_7()
    {
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_66());
        jj_scanpos = token;
        if (jj_scan_token(60))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_8()
    {
        Token token = jj_scanpos;
        if (jj_3R_67())
        {
            jj_scanpos = token;
            if (jj_3R_68())
            {
                jj_scanpos = token;
                if (jj_3R_69())
                {
                    jj_scanpos = token;
                    if (jj_3R_70())
                    {
                        jj_scanpos = token;
                        if (jj_3R_71())
                        {
                            jj_scanpos = token;
                            if (jj_3R_72())
                            {
                                jj_scanpos = token;
                                if (jj_3R_73())
                                {
                                    jj_scanpos = token;
                                    if (jj_3R_74())
                                    {
                                        jj_scanpos = token;
                                        if (jj_3R_75())
                                        {
                                            jj_scanpos = token;
                                            if (jj_3R_76())
                                            {
                                                jj_scanpos = token;
                                                if (jj_3R_77())
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_3R_78())
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3_9()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_79())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_10()
    {
        Token token = jj_scanpos;
        if (jj_3R_80())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_11()
    {
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_82());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_scan_token(98))
        {
            jj_scanpos = token;
            if (jj_scan_token(100))
            {
                jj_scanpos = token;
                if (jj_scan_token(97))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3_12()
    {
        if (jj_3R_83())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_13()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_84())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_14()
    {
        if (jj_3R_85())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_15()
    {
        if (jj_3R_59())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_16()
    {
        if (jj_scan_token(71))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_17()
    {
        if (jj_3R_87())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_18()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_19()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_20()
    {
        if (jj_3R_88())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_21()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_22())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3_22()
    {
        if (jj_3R_88())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_23()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_89())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_24()
    {
        if (jj_3R_90())
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_25()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(138))
        {
            jj_scanpos = token;
            if (jj_3_26())
            {
                jj_scanpos = token;
                if (jj_3_27())
                {
                    return true;
                }
            }
        }
        if (jj_3R_258())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_26()
    {
        if (jj_3R_92())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_27()
    {
        if (jj_3R_93())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_28()
    {
        if (jj_3R_94())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_29()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_30()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(95))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_31()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_32()
    {
        if (jj_3R_96())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_33()
    {
        if (jj_3R_97())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(35))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_34()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(71))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_35()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_98())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_36()
    {
        if (jj_3R_99())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_37()
    {
        if (jj_scan_token(58))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        if (jj_3R_178())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_38()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_39()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_40()
    {
        if (jj_3_38())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_38());
        jj_scanpos = token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_39());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3_41()
    {
        if (jj_3R_100())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_42()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_43()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_44()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_45()
    {
        if (jj_scan_token(139))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_103())
        {
            jj_scanpos = token;
            if (jj_scan_token(92))
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3_46()
    {
        if (jj_scan_token(139))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_47()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_104())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_48()
    {
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_221()
    {
        if (jj_scan_token(69))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_317());
        jj_scanpos = token;
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_220()
    {
        Token token = jj_scanpos;
        if (jj_3R_237())
        {
            jj_scanpos = token;
            if (jj_3R_238())
            {
                jj_scanpos = token;
                if (jj_3R_239())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_scan_token(int P_0)
    {
        if (jj_scanpos == jj_lastpos)
        {
            jj_la--;
            if (jj_scanpos.next == null)
            {
                Token obj = jj_scanpos;
                Token nextToken = token_source.getNextToken();
                Token token = obj;
                Token obj2 = nextToken;
                token.next = nextToken;
                nextToken = obj2;
                Token obj3 = nextToken;
                jj_scanpos = nextToken;
                jj_lastpos = obj3;
            }
            else
            {
                Token nextToken = jj_scanpos.next;
                Token obj4 = nextToken;
                jj_scanpos = nextToken;
                jj_lastpos = obj4;
            }
        }
        else
        {
            jj_scanpos = jj_scanpos.next;
        }
        if (jj_rescan)
        {
            int num = 0;
            Token next = this.token;
            while (next != null && next != jj_scanpos)
            {
                num++;
                next = next.next;
            }
            if (next != null)
            {
                jj_add_error_token(P_0, num);
            }
        }
        if (jj_scanpos.kind != P_0)
        {
            return true;
        }
        if (jj_la == 0 && jj_scanpos == jj_lastpos)
        {
            throw (jj_ls);
        }
        return false;
    }


    private bool jj_3R_217()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_235());
        jj_scanpos = token;
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_113()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_147());
        jj_scanpos = token;
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_219()
    {
        if (jj_scan_token(28))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_316())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_88()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        if (jj_3R_121())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_194());
        jj_scanpos = token;
        if (jj_scan_token(132))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_100()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_199()
    {
        if (jj_3R_219())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_200()
    {
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_201()
    {
        if (jj_3R_220())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_202()
    {
        if (jj_3R_221())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_203()
    {
        if (jj_3R_222())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_204()
    {
        if (jj_3R_223())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_205()
    {
        if (jj_3R_224())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_206()
    {
        if (jj_3R_225())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_207()
    {
        if (jj_3R_226())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_208()
    {
        if (jj_3R_227())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_209()
    {
        if (jj_3R_228())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_210()
    {
        if (jj_3R_229())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_211()
    {
        if (jj_3R_230())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_212()
    {
        if (jj_3R_231())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_91()
    {
        if (jj_3R_122())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_24())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_216()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_148()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_234())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_scan_token(98))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_110()
    {
        Token token = jj_scanpos;
        if (jj_3R_142())
        {
            jj_scanpos = token;
            if (jj_3R_143())
            {
                jj_scanpos = token;
                if (jj_3R_144())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_192()
    {
        if (jj_3R_216())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_216());
        jj_scanpos = token;
        if (jj_3R_148())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_86()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_118())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_193()
    {
        if (jj_3R_217())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_178()
    {
        Token token = jj_scanpos;
        if (jj_3_40())
        {
            jj_scanpos = token;
            if (jj_3R_192())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_150()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_20())
        {
            jj_scanpos = token;
        }
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_21());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_179()
    {
        if (jj_3R_88())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_180()
    {
        if (jj_3R_178())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_181()
    {
        if (jj_3R_86())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_193())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_131()
    {
        if (jj_scan_token(58))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_179())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_180())
        {
            jj_scanpos = token;
            if (jj_3R_181())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_95()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(29))
        {
            jj_scanpos = token;
            if (jj_scan_token(34))
            {
                jj_scanpos = token;
                if (jj_scan_token(31))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(65))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(54))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(56))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(47))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(40))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_67()
    {
        if (jj_scan_token(63))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_68()
    {
        if (jj_scan_token(66))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_69()
    {
        if (jj_scan_token(62))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_70()
    {
        if (jj_scan_token(61))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_71()
    {
        if (jj_scan_token(45))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_72()
    {
        if (jj_scan_token(27))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_73()
    {
        if (jj_scan_token(70))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_74()
    {
        if (jj_scan_token(57))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_75()
    {
        if (jj_scan_token(74))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_76()
    {
        if (jj_scan_token(78))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_77()
    {
        if (jj_scan_token(67))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_78()
    {
        if (jj_3R_110())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_163()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_66()
    {
        if (jj_3R_110())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_149()
    {
        if (jj_3R_91())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_163());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_118()
    {
        if (jj_3R_149())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_189()
    {
        if (jj_scan_token(75))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_190()
    {
        if (jj_scan_token(44))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_176()
    {
        Token token = jj_scanpos;
        if (jj_3R_189())
        {
            jj_scanpos = token;
            if (jj_3R_190())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_156()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(80))
        {
            jj_scanpos = token;
            if (jj_scan_token(84))
            {
                jj_scanpos = token;
                if (jj_scan_token(89))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(90))
                    {
                        jj_scanpos = token;
                        if (jj_3R_165())
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(59))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_165()
    {
        if (jj_3R_176())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_99()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_88())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_98()
    {
        Token token = jj_scanpos;
        if (jj_3_37())
        {
            jj_scanpos = token;
            if (jj_3R_131())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_97()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(77))
        {
            jj_scanpos = token;
            if (jj_3R_130())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_127()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_128()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_129()
    {
        if (jj_3R_86())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_102()
    {
        if (jj_3R_89())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_23());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_106()
    {
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_96()
    {
        Token token = jj_scanpos;
        if (jj_3_34())
        {
            jj_scanpos = token;
            if (jj_3_35())
            {
                jj_scanpos = token;
                if (jj_3_36())
                {
                    jj_scanpos = token;
                    if (jj_3R_127())
                    {
                        jj_scanpos = token;
                        if (jj_3R_128())
                        {
                            jj_scanpos = token;
                            if (jj_3R_129())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_135()
    {
        if (jj_3R_156())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_136()
    {
        if (jj_scan_token(68))
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_137()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_138()
    {
        if (jj_3R_98())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_139()
    {
        if (jj_3R_97())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_scan_token(35))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_140()
    {
        if (jj_3R_102())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_218()
    {
        if (jj_3R_106())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_105()
    {
        Token token = jj_scanpos;
        if (jj_3R_135())
        {
            jj_scanpos = token;
            if (jj_scan_token(71))
            {
                jj_scanpos = token;
                if (jj_3R_136())
                {
                    jj_scanpos = token;
                    if (jj_3R_137())
                    {
                        jj_scanpos = token;
                        if (jj_3R_138())
                        {
                            jj_scanpos = token;
                            if (jj_3R_139())
                            {
                                jj_scanpos = token;
                                if (jj_3R_140())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_81()
    {
        Token token = jj_scanpos;
        if (jj_3_17())
        {
            jj_scanpos = token;
            if (jj_3R_112())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_304()
    {
        Token token = jj_scanpos;
        if (jj_3R_308())
        {
            jj_scanpos = token;
            if (jj_3R_309())
            {
                jj_scanpos = token;
                if (jj_3R_310())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_282()
    {
        Token token = jj_scanpos;
        if (jj_3R_292())
        {
            jj_scanpos = token;
            if (jj_3R_293())
            {
                jj_scanpos = token;
                if (jj_3R_294())
                {
                    jj_scanpos = token;
                    if (jj_3R_295())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_314()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_282())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_315()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_304())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_195()
    {
        if (jj_scan_token(103))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_196()
    {
        if (jj_3R_218())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_59()
    {
        if (jj_3R_105())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_32());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_330()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(112))
        {
            jj_scanpos = token;
            if (jj_scan_token(113))
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_154()
    {
        if (jj_3R_156())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_125()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_126()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(103))
        {
            jj_scanpos = token;
            if (jj_scan_token(102))
            {
                jj_scanpos = token;
                if (jj_scan_token(91))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(140))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(71))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(68))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(58))
                                {
                                    jj_scanpos = token;
                                    if (jj_3R_154())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_94()
    {
        Token token = jj_scanpos;
        if (jj_3_29())
        {
            jj_scanpos = token;
            if (jj_3R_125())
            {
                jj_scanpos = token;
                if (jj_3R_126())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_93()
    {
        lookingAhead = true;
        jj_semLA = ((getToken(1).kind == 132 && ((Token.GTToken)getToken(1)).realKind == 130) ? true : false);
        lookingAhead = false;
        if (!jj_semLA || jj_3R_124())
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_313()
    {
        if (jj_3R_59())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_330())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_312()
    {
        Token token = jj_scanpos;
        if (jj_3R_314())
        {
            jj_scanpos = token;
            if (jj_3R_315())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_308()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(103))
        {
            jj_scanpos = token;
            if (jj_scan_token(102))
            {
                return true;
            }
        }
        if (jj_3R_282())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_309()
    {
        if (jj_3R_312())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_310()
    {
        if (jj_3R_313())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_264()
    {
        if (jj_3R_282())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_305());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_92()
    {
        lookingAhead = true;
        jj_semLA = ((getToken(1).kind == 132 && ((Token.GTToken)getToken(1)).realKind == 131) ? true : false);
        lookingAhead = false;
        if (!jj_semLA || jj_3R_123())
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        if (jj_scan_token(132))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_65()
    {
        if (jj_3R_108())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_109());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_182()
    {
        Token token = jj_scanpos;
        if (jj_3R_195())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(95))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_196())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_60()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_258()
    {
        if (jj_3R_264())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_296());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_245()
    {
        if (jj_scan_token(113))
        {
            return true;
        }
        if (jj_3R_59())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_244()
    {
        if (jj_scan_token(112))
        {
            return true;
        }
        if (jj_3R_59())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_166()
    {
        if (jj_3R_106())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_167()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        if (jj_3R_60())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_168()
    {
        if (jj_3R_182())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_169()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_65())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_292()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(114))
        {
            jj_scanpos = token;
            if (jj_scan_token(115))
            {
                return true;
            }
        }
        if (jj_3R_282())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_293()
    {
        if (jj_3R_244())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_294()
    {
        if (jj_3R_245())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_295()
    {
        if (jj_3R_304())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_251()
    {
        if (jj_3R_258())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_25());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_305()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(116))
        {
            jj_scanpos = token;
            if (jj_scan_token(117))
            {
                jj_scanpos = token;
                if (jj_scan_token(121))
                {
                    return true;
                }
            }
        }
        if (jj_3R_282())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_240()
    {
        if (jj_3R_246())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_259())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_296()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(114))
        {
            jj_scanpos = token;
            if (jj_scan_token(115))
            {
                return true;
            }
        }
        if (jj_3R_264())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_232()
    {
        if (jj_3R_240())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_252());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_265()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(101))
        {
            jj_scanpos = token;
            if (jj_scan_token(132))
            {
                jj_scanpos = token;
                if (jj_scan_token(107))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(108))
                    {
                        return true;
                    }
                }
            }
        }
        if (jj_3R_251())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_157()
    {
        Token token = jj_scanpos;
        if (jj_3R_166())
        {
            jj_scanpos = token;
            if (jj_3R_167())
            {
                jj_scanpos = token;
                if (jj_3R_168())
                {
                    jj_scanpos = token;
                    if (jj_3R_169())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_188()
    {
        if (jj_3R_214())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_241());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_141()
    {
        if (jj_3R_157())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_246()
    {
        if (jj_3R_251())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_265());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_259()
    {
        if (jj_scan_token(53))
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_214()
    {
        if (jj_3R_232())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_247());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_175()
    {
        if (jj_3R_188())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_233());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_252()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(106))
        {
            jj_scanpos = token;
            if (jj_scan_token(109))
            {
                return true;
            }
        }
        if (jj_3R_240())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_164()
    {
        if (jj_3R_175())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_215());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_247()
    {
        if (jj_scan_token(118))
        {
            return true;
        }
        if (jj_3R_232())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_241()
    {
        if (jj_scan_token(120))
        {
            return true;
        }
        if (jj_3R_214())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_233()
    {
        if (jj_scan_token(119))
        {
            return true;
        }
        if (jj_3R_188())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_108()
    {
        if (jj_3R_141())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_141());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_109()
    {
        if (jj_scan_token(119))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_215()
    {
        if (jj_scan_token(111))
        {
            return true;
        }
        if (jj_3R_175())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_191()
    {
        if (jj_scan_token(110))
        {
            return true;
        }
        if (jj_3R_164())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_153()
    {
        if (jj_3R_164())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_191());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_177()
    {
        if (jj_scan_token(104))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_90()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(100))
        {
            jj_scanpos = token;
            if (jj_scan_token(124))
            {
                jj_scanpos = token;
                if (jj_scan_token(125))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(129))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(122))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(123))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(135))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(136))
                                    {
                                        jj_scanpos = token;
                                        if (jj_scan_token(137))
                                        {
                                            jj_scanpos = token;
                                            if (jj_scan_token(126))
                                            {
                                                jj_scanpos = token;
                                                if (jj_scan_token(128))
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_scan_token(127))
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_122()
    {
        if (jj_3R_153())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_177())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_107()
    {
        if (jj_scan_token(133))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_64()
    {
        Token token = jj_scanpos;
        if (jj_3R_107())
        {
            jj_scanpos = token;
        }
        if (jj_3R_60())
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_302()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_89()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(140))
        {
            jj_scanpos = token;
            if (jj_scan_token(1))
            {
                jj_scanpos = token;
                if (jj_scan_token(2))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(3))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(4))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(5))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(6))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(7))
                                    {
                                        jj_scanpos = token;
                                        if (jj_scan_token(8))
                                        {
                                            jj_scanpos = token;
                                            if (jj_scan_token(9))
                                            {
                                                jj_scanpos = token;
                                                if (jj_scan_token(10))
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_scan_token(11))
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_104()
    {
        Token token = jj_scanpos;
        if (jj_3R_132())
        {
            jj_scanpos = token;
            if (jj_3R_133())
            {
                jj_scanpos = token;
                if (jj_3R_134())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_130()
    {
        if (jj_3R_81())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_197()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(100))
        {
            return true;
        }
        if (jj_3R_104())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_155()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_104())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_47());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_scan_token(98))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_132()
    {
        if (jj_3R_110())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_133()
    {
        if (jj_3R_155())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_134()
    {
        if (jj_3R_122())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_183()
    {
        if (jj_3R_197())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_198());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_61()
    {
        if (jj_3R_60())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_62()
    {
        if (jj_3R_106())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_63()
    {
        if (jj_3R_59())
        {
            return true;
        }
        if (jj_scan_token(100))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_121()
    {
        Token token = jj_scanpos;
        if (jj_3R_151())
        {
            jj_scanpos = token;
            if (jj_3R_152())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_198()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_197())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_87()
    {
        Token token = jj_scanpos;
        if (jj_3R_119())
        {
            jj_scanpos = token;
            if (jj_3R_120())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_243()
    {
        Token token = jj_scanpos;
        if (jj_3R_249())
        {
            jj_scanpos = token;
            if (jj_3R_250())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_249()
    {
        if (jj_scan_token(43))
        {
            return true;
        }
        if (jj_3R_87())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_250()
    {
        if (jj_scan_token(68))
        {
            return true;
        }
        if (jj_3R_87())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_236()
    {
        if (jj_3R_243())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_151()
    {
        if (jj_3R_87())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_152()
    {
        if (jj_scan_token(104))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_236())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_194()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_121())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_170()
    {
        if (jj_3R_183())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_103()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(100))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_160()
    {
        if (jj_scan_token(139))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_288()
    {
        if (jj_3R_102())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_302());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_159()
    {
        if (jj_scan_token(139))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_104())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_158()
    {
        if (jj_scan_token(139))
        {
            return true;
        }
        if (jj_3R_102())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_170())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_142()
    {
        if (jj_3R_158())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_143()
    {
        if (jj_3R_159())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_144()
    {
        if (jj_3R_160())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_119()
    {
        if (jj_3R_95())
        {
            return true;
        }
        if (jj_3_18())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_18());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_120()
    {
        if (jj_3R_150())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_19());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_112()
    {
        if (jj_3R_95())
        {
            return true;
        }
        return false;
    }

    private bool jj_3R_123()
    {
        return false;
    }

    private bool jj_3R_124()
    {
        return false;
    }


    private bool jj_3R_85()
    {
        Token token = jj_scanpos;
        if (jj_3R_116())
        {
            jj_scanpos = token;
            if (jj_3R_117())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_300()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(134))
        {
            jj_scanpos = token;
        }
        if (jj_3R_289())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_116()
    {
        if (jj_scan_token(71))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_117()
    {
        Token token = jj_scanpos;
        if (jj_3_15())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(68))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_322()
    {
        if (jj_scan_token(33))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_300())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_323()
    {
        if (jj_scan_token(46))
        {
            return true;
        }
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_162()
    {
        Token token = jj_scanpos;
        if (jj_3R_172())
        {
            jj_scanpos = token;
            if (jj_3R_173())
            {
                jj_scanpos = token;
                if (jj_3R_174())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_111()
    {
        if (jj_scan_token(101))
        {
            return true;
        }
        if (jj_3R_145())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_146());
        jj_scanpos = token;
        if (jj_scan_token(132))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_271()
    {
        if (jj_3R_111())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_272()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_287())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_273()
    {
        if (jj_scan_token(73))
        {
            return true;
        }
        if (jj_3R_288())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_274()
    {
        if (jj_3R_85())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_275()
    {
        if (jj_3R_162())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_101()
    {
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_8());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_289()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_303());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_301()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_300())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_84()
    {
        Token token = jj_scanpos;
        if (jj_3R_114())
        {
            jj_scanpos = token;
            if (jj_3R_115())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_287()
    {
        if (jj_3R_300())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_301());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_335()
    {
        if (jj_3R_338())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_291()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_321()
    {
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_278()
    {
        if (jj_3R_111())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_279()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_3R_272())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_291());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_280()
    {
        if (jj_scan_token(73))
        {
            return true;
        }
        if (jj_3R_288())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_281()
    {
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_276()
    {
        if (jj_3R_289())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_290())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_234()
    {
        if (jj_3R_84())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_13());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_186()
    {
        Token token = jj_scanpos;
        if (jj_3_41())
        {
            jj_scanpos = token;
            if (jj_3R_199())
            {
                jj_scanpos = token;
                if (jj_3R_200())
                {
                    jj_scanpos = token;
                    if (jj_scan_token(97))
                    {
                        jj_scanpos = token;
                        if (jj_3R_201())
                        {
                            jj_scanpos = token;
                            if (jj_3R_202())
                            {
                                jj_scanpos = token;
                                if (jj_3R_203())
                                {
                                    jj_scanpos = token;
                                    if (jj_3R_204())
                                    {
                                        jj_scanpos = token;
                                        if (jj_3R_205())
                                        {
                                            jj_scanpos = token;
                                            if (jj_3R_206())
                                            {
                                                jj_scanpos = token;
                                                if (jj_3R_207())
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_3R_208())
                                                    {
                                                        jj_scanpos = token;
                                                        if (jj_3R_209())
                                                        {
                                                            jj_scanpos = token;
                                                            if (jj_3R_210())
                                                            {
                                                                jj_scanpos = token;
                                                                if (jj_3R_211())
                                                                {
                                                                    jj_scanpos = token;
                                                                    if (jj_3R_212())
                                                                    {
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_114()
    {
        if (jj_3R_148())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_115()
    {
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_338()
    {
        if (jj_3R_220())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_339());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_303()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_339()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_220())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_290()
    {
        if (jj_scan_token(100))
        {
            return true;
        }
        if (jj_3R_84())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_185()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_3R_276())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_311());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_336()
    {
        if (jj_3R_185())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_337()
    {
        if (jj_3R_338())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_82()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(96))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_277()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_276())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_80()
    {
        if (jj_3R_111())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_334()
    {
        Token token = jj_scanpos;
        if (jj_3R_336())
        {
            jj_scanpos = token;
            if (jj_3R_337())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_327()
    {
        if (jj_3R_334())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_328()
    {
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_329()
    {
        if (jj_3R_335())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_263()
    {
        Token token = jj_scanpos;
        if (jj_3R_278())
        {
            jj_scanpos = token;
        }
        if (jj_3R_97())
        {
            return true;
        }
        if (jj_3R_279())
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_280())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_281())
        {
            jj_scanpos = token;
            if (jj_scan_token(97))
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_262()
    {
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_3R_276())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_277());
        jj_scanpos = token;
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_261()
    {
        Token token = jj_scanpos;
        if (jj_3R_271())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_3R_272())
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_273())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_274())
        {
            jj_scanpos = token;
        }
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_275());
        jj_scanpos = token;
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_319()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_320()
    {
        Token token = jj_scanpos;
        if (jj_3R_327())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_328())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_329())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_260()
    {
        if (jj_scan_token(42))
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_269())
        {
            jj_scanpos = token;
        }
        if (jj_3R_270())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_187()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(35))
        {
            jj_scanpos = token;
            if (jj_3R_213())
            {
                return true;
            }
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_266())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_267())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_268())
        {
            jj_scanpos = token;
        }
        if (jj_3R_217())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_253()
    {
        if (jj_3R_187())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_254()
    {
        if (jj_3R_260())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_255()
    {
        if (jj_3R_261())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_256()
    {
        if (jj_3R_262())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_257()
    {
        if (jj_3R_263())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_83()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(66))
        {
            jj_scanpos = token;
        }
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_248()
    {
        if (jj_3R_101())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_253())
        {
            jj_scanpos = token;
            if (jj_3R_254())
            {
                jj_scanpos = token;
                if (jj_3R_255())
                {
                    jj_scanpos = token;
                    if (jj_3R_256())
                    {
                        jj_scanpos = token;
                        if (jj_3R_257())
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_318()
    {
        if (jj_scan_token(41))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_332()
    {
        if (jj_scan_token(32))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_333()
    {
        if (jj_scan_token(38))
        {
            return true;
        }
        if (jj_scan_token(105))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_325()
    {
        Token token = jj_scanpos;
        if (jj_3R_332())
        {
            jj_scanpos = token;
            if (jj_3R_333())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_326()
    {
        if (jj_3R_162())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_242()
    {
        Token token = jj_scanpos;
        if (jj_3_12())
        {
            jj_scanpos = token;
            if (jj_3R_248())
            {
                jj_scanpos = token;
                if (jj_scan_token(97))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_317()
    {
        if (jj_3R_325())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_326());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_235()
    {
        if (jj_3R_242())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_145()
    {
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_161())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_171()
    {
        if (jj_scan_token(43))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_184());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_331()
    {
        if (jj_3R_90())
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_324()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(112))
        {
            jj_scanpos = token;
            if (jj_scan_token(113))
            {
                jj_scanpos = token;
                if (jj_3R_331())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_184()
    {
        if (jj_scan_token(118))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_237()
    {
        if (jj_3R_244())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_238()
    {
        if (jj_3R_245())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_239()
    {
        if (jj_3R_59())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_324())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_161()
    {
        if (jj_3R_171())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_79()
    {
        if (jj_3R_101())
        {
            return true;
        }
        if (jj_scan_token(140))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_306())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_307())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_146()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_145())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_306()
    {
        if (jj_3R_86())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_307()
    {
        if (jj_3R_217())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_311()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_276())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_299()
    {
        if (jj_3R_242())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_285()
    {
        if (jj_3R_79())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_9());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_286()
    {
        if (jj_scan_token(97))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_299());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_172()
    {
        if (jj_3R_185())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_173()
    {
        if (jj_3R_186())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_174()
    {
        if (jj_3R_187())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_284()
    {
        if (jj_scan_token(51))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_298());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_269()
    {
        if (jj_3R_284())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_270()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_285())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_scan_token(98))
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_286())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_298()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_147()
    {
        if (jj_3R_162())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_297()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_316()
    {
        if (jj_scan_token(105))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_231()
    {
        if (jj_scan_token(76))
        {
            return true;
        }
        if (jj_3R_113())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_322());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_3R_323())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_283()
    {
        if (jj_scan_token(43))
        {
            return true;
        }
        if (jj_3R_150())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_297());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_230()
    {
        if (jj_scan_token(70))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_113())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_229()
    {
        if (jj_scan_token(72))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_213()
    {
        if (jj_scan_token(55))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_266()
    {
        if (jj_3R_111())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_267()
    {
        if (jj_3R_283())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_268()
    {
        if (jj_3R_284())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_228()
    {
        if (jj_scan_token(64))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_321())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_227()
    {
        if (jj_scan_token(37))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(140))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_226()
    {
        if (jj_scan_token(30))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(140))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_225()
    {
        if (jj_scan_token(48))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_319())
        {
            jj_scanpos = token;
            if (jj_3R_320())
            {
                return true;
            }
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_224()
    {
        if (jj_scan_token(39))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        if (jj_scan_token(79))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_scan_token(97))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_223()
    {
        if (jj_scan_token(79))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_222()
    {
        if (jj_scan_token(50))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        if (jj_3R_91())
        {
            return true;
        }
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_186())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_318())
        {
            jj_scanpos = token;
        }
        return false;
    }


    public JavaCCParser(Stream @is, string str)
    {
        processing_cu = false;
        class_nesting = 0;
        inLocalLA = 0;
        inAction = false;
        jumpPatched = false;
        lookingAhead = false;
        jj_la1 = new int[172];
        jj_2_rtns = new JJCalls[48];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new LookaheadSuccess();
        jj_expentries = new();
        jj_kind = -1;
        jj_lasttokens = new int[100];
        UnsupportedEncodingException ex;
        try
        {
            jj_input_stream = new JavaCharStream(@is, str, 1, 1);
        }
        catch (UnsupportedEncodingException x)
        {
            ex = x;
            goto IL_00a6;
        }
        JavaCCParserTokenManager.___003Cclinit_003E();
        token_source = new JavaCCParserTokenManager(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
        return;
    IL_00a6:
        UnsupportedEncodingException cause = ex;

        throw new RuntimeException(cause);
    }


    public virtual void ReInit(Stream @is, string str)
    {
        UnsupportedEncodingException ex;
        try
        {
            jj_input_stream.ReInit(@is, str, 1, 1);
        }
        catch (UnsupportedEncodingException x)
        {
            ex = x;
            goto IL_001e;
        }
        token_source.ReInit(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
        return;
    IL_001e:
        throw new RuntimeException(ex);
    }


    public virtual ParseException generateParseException()
    {
        jj_expentries.Clear();
        bool[] array = new bool[143];
        if (jj_kind >= 0)
        {
            array[jj_kind] = true;
            jj_kind = -1;
        }
        for (int i = 0; i < 172; i++)
        {
            if (jj_la1[i] != jj_gen)
            {
                continue;
            }
            for (int j = 0; j < 32; j++)
            {
                if ((jj_la1_0[i] & (1 << j)) != 0)
                {
                    array[j] = true;
                }
                if ((jj_la1_1[i] & (1 << j)) != 0)
                {
                    array[32 + j] = true;
                }
                if ((jj_la1_2[i] & (1 << j)) != 0)
                {
                    array[64 + j] = true;
                }
                if ((jj_la1_3[i] & (1 << j)) != 0)
                {
                    array[96 + j] = true;
                }
                if ((jj_la1_4[i] & (1 << j)) != 0)
                {
                    array[128 + j] = true;
                }
            }
        }
        for (int i = 0; i < 143; i++)
        {
            if (array[i])
            {
                jj_expentry = new int[1];
                jj_expentry[0] = i;
                jj_expentries.Add(jj_expentry);
            }
        }
        jj_endpos = 0;
        jj_rescan_token();
        jj_add_error_token(0, 0);
        int[][] array2 = new int[jj_expentries.Count][];
        for (int j = 0; j < jj_expentries.Count; j++)
        {
            array2[j] = (int[])jj_expentries[j];
        }
        return new ParseException(token, array2, JavaCCParserConstants.tokenImage);
    }


    private void jj_add_error_token(int P_0, int P_1)
    {
        if (P_1 >= 100)
        {
            return;
        }
        if (P_1 == jj_endpos + 1)
        {
            int[] array = jj_lasttokens;
            int num = jj_endpos;
            int num2 = num;
            jj_endpos = num + 1;
            array[num2] = P_0;
        }
        else
        {
            if (jj_endpos == 0)
            {
                return;
            }
            jj_expentry = new int[jj_endpos];
            int i;
            for (i = 0; i < jj_endpos; i++)
            {
                jj_expentry[i] = jj_lasttokens[i];
            }
            i = 0;
            foreach(var fv in jj_expentries)
            {
                int[] array2 = fv;
                if ((nint)array2.LongLength != (nint)jj_expentry.LongLength)
                {
                    continue;
                }
                i = 1;
                for (int j = 0; j < (nint)jj_expentry.LongLength; j++)
                {
                    if (array2[j] != jj_expentry[j])
                    {
                        i = 0;
                        break;
                    }
                }
                if (i != 0)
                {
                    break;
                }
            }
            if (i == 0)
            {
                jj_expentries.Add(jj_expentry);
            }
            if (P_1 != 0)
            {
                int[] array3 = jj_lasttokens;
                int num = P_1;
                int num3 = num;
                jj_endpos = num;
                array3[num3 - 1] = P_0;
            }
        }
    }


    private void jj_rescan_token()
    {
        jj_rescan = true;
        for (int i = 0; i < 48; i++)
        {
            try
            {
                JJCalls jJCalls = jj_2_rtns[i];
                do
                {
                    if (jJCalls.gen > jj_gen)
                    {
                        jj_la = jJCalls.arg;
                        Token first = jJCalls.first;
                        jj_scanpos = first;
                        jj_lastpos = first;
                        switch (i)
                        {
                            case 0:
                                jj_3_1();
                                break;
                            case 1:
                                jj_3_2();
                                break;
                            case 2:
                                jj_3_3();
                                break;
                            case 3:
                                jj_3_4();
                                break;
                            case 4:
                                jj_3_5();
                                break;
                            case 5:
                                jj_3_6();
                                break;
                            case 6:
                                jj_3_7();
                                break;
                            case 7:
                                jj_3_8();
                                break;
                            case 8:
                                jj_3_9();
                                break;
                            case 9:
                                jj_3_10();
                                break;
                            case 10:
                                jj_3_11();
                                break;
                            case 11:
                                jj_3_12();
                                break;
                            case 12:
                                jj_3_13();
                                break;
                            case 13:
                                jj_3_14();
                                break;
                            case 14:
                                jj_3_15();
                                break;
                            case 15:
                                jj_3_16();
                                break;
                            case 16:
                                jj_3_17();
                                break;
                            case 17:
                                jj_3_18();
                                break;
                            case 18:
                                jj_3_19();
                                break;
                            case 19:
                                jj_3_20();
                                break;
                            case 20:
                                jj_3_21();
                                break;
                            case 21:
                                jj_3_22();
                                break;
                            case 22:
                                jj_3_23();
                                break;
                            case 23:
                                jj_3_24();
                                break;
                            case 24:
                                jj_3_25();
                                break;
                            case 25:
                                jj_3_26();
                                break;
                            case 26:
                                jj_3_27();
                                break;
                            case 27:
                                jj_3_28();
                                break;
                            case 28:
                                jj_3_29();
                                break;
                            case 29:
                                jj_3_30();
                                break;
                            case 30:
                                jj_3_31();
                                break;
                            case 31:
                                jj_3_32();
                                break;
                            case 32:
                                jj_3_33();
                                break;
                            case 33:
                                jj_3_34();
                                break;
                            case 34:
                                jj_3_35();
                                break;
                            case 35:
                                jj_3_36();
                                break;
                            case 36:
                                jj_3_37();
                                break;
                            case 37:
                                jj_3_38();
                                break;
                            case 38:
                                jj_3_39();
                                break;
                            case 39:
                                jj_3_40();
                                break;
                            case 40:
                                jj_3_41();
                                break;
                            case 41:
                                jj_3_42();
                                break;
                            case 42:
                                jj_3_43();
                                break;
                            case 43:
                                jj_3_44();
                                break;
                            case 44:
                                jj_3_45();
                                break;
                            case 45:
                                jj_3_46();
                                break;
                            case 46:
                                jj_3_47();
                                break;
                            case 47:
                                jj_3_48();
                                break;
                        }
                    }
                    jJCalls = jJCalls.next;
                }
                while (jJCalls != null);
            }
            catch (LookaheadSuccess)
            {
                goto IL_034b;
            }
            continue;
        IL_034b:
            ;

        }
        jj_rescan = false;
    }

    private static void jj_la1_init_0()
    {
        jj_la1_0 = new int[172]
        {
            -1610610720, 6, 6, 0, -1610610720, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 960, 0, 0, 0,
            2, 0, 0, 0, 0, 0, 0, 0, 2, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 4094, 0, 134217728, 0, 0,
            134217728, 0, 134217728, 0, 0, 0, 0, 0, 0, 0,
            134217728, 0, -1476395008, 0, 0, 0, 0, 0, 0, -1476395008,
            0, -1610612736, -1476395008, 0, 0, 0, -1610608642, -1610608642, 0, 0,
            0, 0, 0, 0, -1476395008, 0, 0, 0, -134213634, -1610608642,
            0, -1610612736, -1610612736, 0, 0, -1610612736, 0, -1610612736, -1610612736, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, -1610608642, 0,
            0, -1610608642, 0, 0, 0, 0, 0, 0, 4094, 0,
            0, 0, -1610608642, 0, 0, 0, 0, 0, 0, 0,
            -268431362, 0, -134213634, -268431362, 0, 0, 0, -1610608642, 0, -134213634,
            0, 0, -1476390914, -1610608642, -1610608642, -1476390914, -1610608642, 0, 0, 0,
            -1610608642, 0, 0, 0, 0, 0, -1610608642, 0, -1476395008, 0,
            -1610612736, -1476395008
        };
    }

    private static void jj_la1_init_1()
    {
        jj_la1_1 = new int[172]
        {
            -515866364, 0, 0, 4096, -515866364, 0, 0, 0, 0, -536870912,
            -536870912, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 2, 16384, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 1048576, -494918648, 0, 0,
            -503308288, 8389640, -494918648, 8388616, 0, 2048, 524288, 0, 0, 524288,
            -503308288, 0, -473914100, 0, 0, 0, 0, 2048, 0, -473914100,
            8389640, 21004548, -473914100, 0, 0, 0, 222335236, 222335236, 0, 0,
            0, 0, 0, 0, -482303740, 0, 0, 0, -272256596, 222335236,
            0, 21004548, 21004548, 0, 2048, 21004548, 2048, 21004548, 21004548, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 2097152,
            0, 0, 0, 0, 0, 0, 0, 0, 222335236, 0,
            0, 222335236, 201330688, 0, 0, 0, 0, 201330688, 0, 0,
            134221824, 4096, 222335236, 0, 0, 0, 0, 67108864, 0, 0,
            222663076, 0, -272256596, 231051692, 0, 0, 0, 222335236, 65, -272256596,
            65, 512, -280973052, 222335236, 222335236, -280973052, 222335236, 0, 0, 0,
            222335236, 2, 16384, 0, 0, 0, 222335236, 0, -473914100, 64,
            29394188, -473914100
        };
    }

    private static void jj_la1_init_2()
    {
        jj_la1_2 = new int[172]
        {
            8194, 4, 4, 67176448, 8194, 0, 512, 0, 512, 0,
            0, 0, 0, 0, -2147483648, 0, 0, 536870912, 0, 0,
            0, 65536, 0, 0, 0, 67108864, 0, 0, -1610608640, 134217728,
            0, 0, 67108864, 0, 0, -1946157056, 65536, 0, 536870912, 536870912,
            -1946157056, 0, 0, 67108864, 0, 0, 0, 17484, 4, 0,
            17484, 0, 17484, 0, 0, 0, 0, 0, 0, 0,
            17484, 0, 536896590, 0, 134217728, 536870912, 0, 0, 0, 536896590,
            0, 8194, 25678, 0, 0, -2147483648, 772876434, 772876434, 0, 0,
            512, 536870912, -2147483648, 0, 17486, 0, 0, 512, 772931071, 236005522,
            4, 2, 2, 0, 16, 2, 16, 2, 8194, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 236005522, 0,
            0, 236005522, 235997328, 134217728, 0, 0, 134217728, 235997328, 0, -2013265920,
            101779456, 2048, 236005522, 0, 0, 536870912, -2013265920, 0, -2147483648, -2147483648,
            772913651, 0, 772931071, 772913651, 0, 0, 0, 236005522, 0, 772931071,
            0, 0, 236023006, 236005522, 236005522, 236023006, 236005522, 0, 0, 0,
            236005522, 0, 0, 0, 0, 0, 772876434, 0, 17486, 0,
            2, 17486
        };
    }

    private static void jj_la1_init_3()
    {
        jj_la1_3 = new int[172]
        {
            32, 0, 0, 0, 32, 4, 0, 4, 0, 0,
            0, 4, 32, 32, 0, 8388608, 0, 0, 512, 8388608,
            0, 0, 0, 0, 8, 32, 1310976, 1310976, 0, 0,
            0, 0, 0, 32, 8388608, 160, 0, 4, 1310976, 1310976,
            160, 128, 4, 0, 524288, 0, 0, 2, 0, 8,
            0, 0, 2, 0, 32, 0, 0, 4, 4, 0,
            0, 4, 34, 2, 0, 0, 4, 0, 4194304, 34,
            0, 32, 34, 4, 16, 0, 983232, 983232, 4, 32,
            0, 2, 0, 4, 0, 0, 32, 0, 196610, 0,
            0, 0, 0, 4, 0, 256, 0, 0, 0, 4,
            -67108848, 256, 16384, 32768, 8388608, 16777216, 4194304, 9216, 9216, 0,
            6176, 6176, 0, 786432, 786432, 36700160, 36700160, 786432, 983232, 192,
            192, 0, 192, 0, 196608, 196608, 0, 0, 0, 8,
            0, 0, 983232, 4, 32, 0, 0, 0, 0, 0,
            196610, 512, 196610, 196610, 4, -66912240, -66912240, 196608, 0, 196610,
            0, 0, 196608, 983232, 196608, 196610, 196608, 4, 0, 0,
            983232, 0, 0, 0, 0, 4, 983232, 4, 2, 0,
            0, 2
        };
    }

    private static void jj_la1_init_4()
    {
        jj_la1_4 = new int[172]
        {
            4096, 4096, 4096, 0, 4096, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            32, 4128, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 4096, 0, 2048, 0, 0,
            2048, 2048, 2048, 0, 0, 0, 0, 0, 0, 0,
            6144, 0, 6144, 0, 0, 0, 0, 0, 0, 6144,
            0, 4096, 6144, 0, 0, 0, 4096, 4096, 0, 0,
            0, 0, 0, 0, 6144, 64, 0, 0, 6144, 4096,
            0, 0, 4096, 0, 0, 4096, 0, 0, 4096, 0,
            899, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            16, 16, 1024, 0, 0, 0, 0, 0, 4096, 0,
            0, 4096, 4096, 0, 0, 0, 0, 0, 4096, 0,
            0, 0, 4096, 0, 0, 0, 0, 0, 0, 0,
            4096, 0, 6144, 4096, 0, 899, 899, 4096, 0, 6144,
            0, 0, 6144, 4096, 4096, 6144, 4096, 0, 4096, 4096,
            4096, 0, 0, 2048, 4096, 0, 6144, 0, 6144, 0,
            6144, 6144
        };
    }


    public void CastLookahead()
    {
        if (jj_2_29(2))
        {
            jj_consume_token(91);
            PrimitiveType();
            return;
        }
        if (jj_2_30(int.MaxValue))
        {
            jj_consume_token(91);
            Type();
            jj_consume_token(95);
            jj_consume_token(96);
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 91)
        {
            jj_consume_token(91);
            Type();
            jj_consume_token(92);
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 103:
                    jj_consume_token(103);
                    return;
                case 102:
                    jj_consume_token(102);
                    return;
                case 91:
                    jj_consume_token(91);
                    return;
                case 140:
                    jj_consume_token(140);
                    return;
                case 71:
                    jj_consume_token(71);
                    return;
                case 68:
                    jj_consume_token(68);
                    return;
                case 58:
                    jj_consume_token(58);
                    return;
                case 44:
                case 59:
                case 75:
                case 80:
                case 84:
                case 89:
                case 90:
                    Literal();
                    return;
            }
            jj_la1[122] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_la1[123] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public virtual void ReInit(Stream @is)
    {
        ReInit(@is, null);
    }


    public virtual void ReInit(TextReader r)
    {
        jj_input_stream.ReInit(r, 1, 1);
        token_source.ReInit(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public JavaCCParser(JavaCCParserTokenManager jccptm)
    {
        processing_cu = false;
        class_nesting = 0;
        inLocalLA = 0;
        inAction = false;
        jumpPatched = false;
        lookingAhead = false;
        jj_la1 = new int[172];
        jj_2_rtns = new JJCalls[48];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new LookaheadSuccess();
        jj_expentries = new();
        jj_kind = -1;
        jj_lasttokens = new int[100];
        token_source = jccptm;
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public virtual void ReInit(JavaCCParserTokenManager jccptm)
    {
        token_source = jccptm;
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 172; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < (nint)jj_2_rtns.LongLength; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public Token getNextToken()
    {
        if (this.token.next != null)
        {
            this.token = this.token.next;
        }
        else
        {
            Token obj = this.token;
            Token nextToken = token_source.getNextToken();
            Token token = obj;
            token.next = nextToken;
            this.token = nextToken;
        }
        this.m_jj_ntk = -1;
        jj_gen++;
        return this.token;
    }

    public void enable_tracing()
    {
    }

    public void disable_tracing()
    {
    }

    static JavaCCParser()
    {

        jj_la1_init_0();
        jj_la1_init_1();
        jj_la1_init_2();
        jj_la1_init_3();
        jj_la1_init_4();
    }
}
