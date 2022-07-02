namespace JavaCC.JJTree;
using JavaCC.Parser;
using System.IO;
using System.Collections.Generic;

public class JJTreeParser : JJTreeParserTreeConstants //, JJTreeParserConstants
{

    internal class JJCalls
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

    protected internal JJTJJTreeParserState jjtree;

    public JJTreeParserTokenManager token_source;

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

    private List<int[]> jj_expentries;

    private int[] jj_expentry;

    private int jj_kind;

    private int[] jj_lasttokens;

    private int jj_endpos;

    public JJTreeParser(TextReader r)
    {
        jjtree = new JJTJJTreeParserState();
        lookingAhead = false;
        jj_la1 = new int[178];
        jj_2_rtns = new JJCalls[47];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new LookaheadSuccess();
        jj_expentries = new();
        jj_kind = -1;
        jj_lasttokens = new int[100];
        jj_input_stream = new JavaCharStream(r, 1, 1);

        token_source = new JJTreeParserTokenManager(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public void javacc_input()
    {
        ASTGrammar aSTGrammar = new ASTGrammar(0);
        int num = 1;
        jjtree.OpenNodeScope(aSTGrammar);
        jjtreeOpenNodeScope(aSTGrammar);
        ASTCompilationUnit aSTCompilationUnit;
        int num2;
        System.Exception ex;
        System.Exception ex2;
        System.Exception ex3;
        try
        {
            try
            {
                javacc_options();
                aSTCompilationUnit = new ASTCompilationUnit(1);
                num2 = 1;
                jjtree.OpenNodeScope(aSTCompilationUnit);
                jjtreeOpenNodeScope(aSTCompilationUnit);
                try
                {
                    try
                    {
                        jj_consume_token(3);
                        jj_consume_token(85);
                        Token token = identifier();
                        jj_consume_token(86);
                        JJTreeGlobals.ParserName = token.Image;
                        CompilationUnit();
                        jj_consume_token(4);
                        jj_consume_token(85);
                        identifier();
                        jj_consume_token(86);
                        jjtree.CloseNodeScope(aSTCompilationUnit, b: true);
                        num2 = 0;
                        jjtreeCloseNodeScope(aSTCompilationUnit);
                        if (string.Equals(JJTreeOptions.NodePackage, ""))
                        {
                            JJTreeGlobals.NodePackageName = JJTreeGlobals.PackageName;
                        }
                        else
                        {
                            JJTreeGlobals.NodePackageName = JJTreeOptions.NodePackage;
                        }
                    }
                    catch (System.Exception x)
                    {
                        ex = x;
                        goto end_IL_001c;
                    }
                }
                catch (System.Exception x2)
                {
                    ex2 = x2;
                    goto IL_012b;
                }
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTCompilationUnit, b: true);
                    jjtreeCloseNodeScope(aSTCompilationUnit);
                }
                goto IL_022d;
            end_IL_001c:;
            }
            catch (System.Exception x3)
            {
                ex3 = x3;
                goto IL_0137;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        System.Exception ex4 = ex;
        System.Exception ex6;
        try
        {
            //ex4 = ex4;
            try
            {
                //ex4 = ex4;
                try
                {
                    System.Exception ex5 = ex4;
                    if (num2 != 0)
                    {
                        jjtree.ClearNodeScope(aSTCompilationUnit);
                        num2 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    throw ex5;
                }
                catch (System.Exception x4)
                {
                    ex4 = x4;
                }
            }
            catch (System.Exception x5)
            {
                ex6 = x5;
                goto IL_01d4;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        System.Exception ex7 = ex4;
        goto IL_01db;
    IL_0137:
        System.Exception ex8 = ex3;
        goto IL_0529;
    IL_022d:
        ASTProductions aSTProductions;
        int num3;
        System.Exception ex9;
        System.Exception ex10;
        System.Exception ex11;
        try
        {
            try
            {
                aSTProductions = new ASTProductions(2);
                num3 = 1;
                jjtree.OpenNodeScope(aSTProductions);
                jjtreeOpenNodeScope(aSTProductions);
                try
                {
                    try
                    {
                        int num4;
                        do
                        {
                            production();
                            num4 = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                        }
                        while (num4 == 1 || num4 == 2 || num4 == 3 || num4 == 4 || num4 == 5 || num4 == 6 || num4 == 7 || num4 == 8 || num4 == 9 || num4 == 10 || num4 == 11 || num4 == 25 || num4 == 27 || num4 == 30 || num4 == 36 || num4 == 42 || num4 == 49 || num4 == 51 || num4 == 56 || num4 == 57 || num4 == 58 || num4 == 60 || num4 == 71 || num4 == 95 || num4 == 137);
                        jj_la1[0] = jj_gen;
                    }
                    catch (System.Exception x6)
                    {
                        ex9 = x6;
                        goto end_IL_022d;
                    }
                }
                catch (System.Exception x7)
                {
                    ex10 = x7;
                    goto IL_03d1;
                }
                if (num3 != 0)
                {
                    jjtree.CloseNodeScope(aSTProductions, b: true);
                    jjtreeCloseNodeScope(aSTProductions);
                }
                goto IL_04d6;
            end_IL_022d:;
            }
            catch (System.Exception x8)
            {
                ex11 = x8;
                goto IL_03dd;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        System.Exception ex12 = ex9;
        System.Exception ex14;
        try
        {
            //ex12 = ex12;
            try
            {
                //ex12 = ex12;
                try
                {
                    System.Exception ex13 = ex12;
                    if (num3 != 0)
                    {
                        jjtree.ClearNodeScope(aSTProductions);
                        num3 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex13 is System.Exception)
                    {
                        throw ((System.Exception)ex13);
                    }
                    if (ex13 is ParseException)
                    {
                        throw (ex13);
                    }
                    throw ((System.Exception)ex13);
                }
                catch (System.Exception x9)
                {
                    ex12 = x9;
                }
            }
            catch (System.Exception x10)
            {
                ex14 = x10;
                goto IL_047d;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        System.Exception ex15 = ex12;
        goto IL_0484;
    IL_03dd:
        ex8 = ex11;
        goto IL_0529;
    IL_04d6:
        System.Exception ex16;
        try
        {
            try
            {
                jj_consume_token(0);
            }
            catch (System.Exception x11)
            {
                ex16 = x11;
                goto IL_0506;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTGrammar, b: true);
            jjtreeCloseNodeScope(aSTGrammar);
        }
        return;
    IL_0506:
        ex8 = ex16;
        goto IL_0529;
    IL_012b:
        ex7 = ex2;
        goto IL_01db;
    IL_01db:
        System.Exception ex17 = ex7;
        try
        {
            //ex17 = ex17;
            try
            {
                System.Exception ex18 = ex17;
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTCompilationUnit, b: true);
                    jjtreeCloseNodeScope(aSTCompilationUnit);
                }
                throw (ex18);
            }
            catch (System.Exception x12)
            {
                ex17 = x12;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        ex8 = ex17;
        goto IL_0529;
    IL_03d1:
        ex15 = ex10;
        goto IL_0484;
    IL_0529:
        System.Exception ex19 = ex8;
        try
        {
            System.Exception ex20 = ex19;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTGrammar);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            throw ex20;
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
    IL_047d:
        ex8 = ex14;
        goto IL_0529;
    IL_01d4:
        ex8 = ex6;
        goto IL_0529;
    IL_0484:
        System.Exception ex21 = ex15;
        try
        {
            //ex21 = ex21;
            try
            {
                System.Exception ex22 = ex21;
                if (num3 != 0)
                {
                    jjtree.CloseNodeScope(aSTProductions, b: true);
                    jjtreeCloseNodeScope(aSTProductions);
                }
                throw (ex22);
            }
            catch (System.Exception x13)
            {
                ex21 = x13;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTGrammar, b: true);
                jjtreeCloseNodeScope(aSTGrammar);
            }
            throw;
        }
        ex8 = ex21;
        goto IL_0529;
    }


    public Token getToken(int i)
    {
        Token token = ((!lookingAhead) ? this.token : jj_scanpos);
        for (int j = 0; j < i; j++)
        {
            if (token.Next != null)
            {
                token = token.Next;
                continue;
            }
            Token obj = token;
            Token nextToken = token_source.GetNextToken();
            Token token2 = obj;
            token2.Next = nextToken;
            token = nextToken;
        }
        return token;
    }


    internal virtual void jjtreeOpenNodeScope(INode node)
    {
        ((JJTreeNode)node).FirstToken = getToken(1);
    }


    public void javacc_options()
    {
        if (!string.Equals(getToken(1).Image, "options"))
        {
            return;
        }
        ASTOptions aSTOptions = new ASTOptions(4);
        int num = 1;
        jjtree.OpenNodeScope(aSTOptions);
        jjtreeOpenNodeScope(aSTOptions);
        System.Exception ex;
        try
        {
            try
            {
                jj_consume_token(137);
                jj_consume_token(87);
                while (true)
                {
                    int num2 = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                    if (num2 != 1 && num2 != 2 && num2 != 61 && num2 != 137)
                    {
                        break;
                    }
                    option_binding();
                }
                jj_la1[1] = jj_gen;
                jj_consume_token(88);
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_00d1;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTOptions, b: true);
                jjtreeCloseNodeScope(aSTOptions);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTOptions, b: true);
            jjtreeCloseNodeScope(aSTOptions);
        }
        return;
    IL_00d1:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTOptions);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTOptions, b: true);
                jjtreeCloseNodeScope(aSTOptions);
            }
            throw;
        }
    }


    private Token jj_consume_token(int P_0)
    {
        Token token;
        if ((token = this.token).Next != null)
        {
            this.token = this.token.Next;
        }
        else
        {
            Token obj = this.token;
            Token nextToken = token_source.GetNextToken();
            Token token2 = obj;
            token2.Next = nextToken;
            this.token = nextToken;
        }
        this.m_jj_ntk = -1;
        if (this.token.Kind == P_0)
        {
            jj_gen++;
            int num = jj_gc + 1;
            jj_gc = num;
            if (num > 100)
            {
                jj_gc = 0;
                for (int i = 0; i < jj_2_rtns.Length; i++)
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
        throw (generateParseException());
    }


    public Token identifier()
    {
        return jj_consume_token(137);
    }


    public void CompilationUnit()
    {
        if (jj_2_6(int.MaxValue))
        {
            PackageDeclaration();
        }
        JJTreeGlobals.ParserImports = getToken(1);
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 47)
        {
            ImportDeclaration();
        }
        jj_la1[52] = jj_gen;
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 24 && num != 31 && num != 40 && num != 50 && num != 52 && num != 56 && num != 57 && num != 58 && num != 61 && num != 64 && num != 68 && num != 72 && num != 91 && num != 128 && num != 129 && num != 136)
            {
                break;
            }
            TypeDeclaration();
        }
        jj_la1[53] = jj_gen;
    }


    internal virtual void jjtreeCloseNodeScope(INode P_0)
    {
        ((JJTreeNode)P_0).LastToken = getToken(0);
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
            case 95:
                regular_expr_production();
                return;
            case 10:
                token_manager_decls();
                return;
            case 1:
            case 2:
            case 3:
            case 4:
            case 11:
            case 25:
            case 27:
            case 30:
            case 36:
            case 42:
            case 49:
            case 51:
            case 56:
            case 57:
            case 58:
            case 60:
            case 71:
            case 137:
                bnf_production();
                return;
        }
        jj_la1[4] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    private int jj_ntk()
    {
        Token next = this.token.Next;
        Token obj = next;
        jj_nt = next;
        int kind;
        if (obj == null)
        {
            Token obj2 = this.token;
            next = token_source.GetNextToken();
            Token token = obj2;
            Token obj3 = next;
            token.Next = next;
            kind = obj3.Kind;
            int result = kind;
            this.m_jj_ntk = kind;
            return result;
        }
        kind = jj_nt.Kind;
        int result2 = kind;
        this.m_jj_ntk = kind;
        return result2;
    }


    public void option_binding()
    {
        ASTOptionBinding aSTOptionBinding = new ASTOptionBinding(5);
        int num = 1;
        jjtree.OpenNodeScope(aSTOptionBinding);
        jjtreeOpenNodeScope(aSTOptionBinding);
        System.Exception ex;
        try
        {
            try
            {
                Token token;
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 137:
                        token = jj_consume_token(137);
                        break;
                    case 1:
                        token = jj_consume_token(1);
                        break;
                    case 2:
                        token = jj_consume_token(2);
                        break;
                    case 61:
                        token = jj_consume_token(61);
                        break;
                    default:
                        jj_la1[2] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                jj_consume_token(94);
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 74:
                        {
                            Token token2 = IntegerLiteral();
                            Token obj = token;
                            Token obj2 = token2;
                            string image = token.Image;
                            ;
                            Options.SetInputFileOption(obj, obj2, image, (token2.Image));
                            aSTOptionBinding.Initialize(token.Image, token2.Image);
                            break;
                        }
                    case 39:
                    case 69:
                        {
                            Token token2 = BooleanLiteral();
                            Options.SetInputFileOption(token, token2, token.Image, (token2.Image));
                            aSTOptionBinding.Initialize(token.Image, token2.Image);
                            break;
                        }
                    case 84:
                        {
                            Token token2 = StringLiteral();
                            string text = TokenUtils.RemoveEscapeAndQuotes(token2, token2.Image);
                            Options.SetInputFileOption(token, token2, token.Image, text);
                            aSTOptionBinding.Initialize(token.Image, text);
                            break;
                        }
                    default:
                        jj_la1[3] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                jj_consume_token(91);
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_01e5;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTOptionBinding, b: true);
                jjtreeCloseNodeScope(aSTOptionBinding);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTOptionBinding, b: true);
            jjtreeCloseNodeScope(aSTOptionBinding);
        }
        return;
    IL_01e5:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTOptionBinding);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTOptionBinding, b: true);
                jjtreeCloseNodeScope(aSTOptionBinding);
            }
            throw;
        }
    }


    public Token IntegerLiteral()
    {
        return jj_consume_token(74);
    }


    public Token BooleanLiteral()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 69:
                return jj_consume_token(69);
            case 39:
                return jj_consume_token(39);
            default:
                jj_la1[137] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public Token StringLiteral()
    {
        return jj_consume_token(84);
    }


    public void javacode_production()
    {
        ASTJavacode aSTJavacode = new ASTJavacode(6);
        int num = 1;
        jjtree.OpenNodeScope(aSTJavacode);
        jjtreeOpenNodeScope(aSTJavacode);
        ASTNodeDescriptor aSTNodeDescriptor = null;
        ASTJavacode aSTJavacode2;
        ASTJavacodeBody aSTJavacodeBody;
        int num2;
        System.Exception ex;
        System.Exception ex2;
        System.Exception ex3;
        try
        {
            try
            {
                aSTJavacode2 = aSTJavacode;
                jj_consume_token(5);
                AccessModifier();
                ResultType();
                Token token = identifier();
                FormalParameters();
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 67)
                {
                    jj_consume_token(67);
                    string obj = Name();
                    aSTJavacode2.ThrowsList.Add(obj);
                    while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                    {
                        jj_consume_token(92);
                        obj = Name();
                        aSTJavacode2.ThrowsList.Add(obj);
                    }
                    jj_la1[5] = jj_gen;
                }
                else
                {
                    jj_la1[6] = jj_gen;
                }
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 127)
                {
                    aSTNodeDescriptor = node_descriptor();
                }
                else
                {
                    jj_la1[7] = jj_gen;
                }
                Token stmBeginLoc = jj_consume_token(87);
                aSTJavacode2.Name = token.Image;
                aSTJavacode2.StmBeginLoc = stmBeginLoc;
                aSTJavacodeBody = new ASTJavacodeBody(7);
                num2 = 1;
                jjtree.OpenNodeScope(aSTJavacodeBody);
                jjtreeOpenNodeScope(aSTJavacodeBody);
                try
                {
                    try
                    {
                        while (true)
                        {
                            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                            {
                                default:
                                    jj_la1[8] = jj_gen;
                                    jjtree.CloseNodeScope(aSTJavacodeBody, b: true);
                                    num2 = 0;
                                    jjtreeCloseNodeScope(aSTJavacodeBody);
                                    aSTJavacodeBody.nodeScope = new NodeScope(aSTJavacode2, aSTNodeDescriptor);
                                    goto end_IL_015f;
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
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 30:
                                case 31:
                                case 33:
                                case 35:
                                case 36:
                                case 39:
                                case 40:
                                case 42:
                                case 43:
                                case 45:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 56:
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                case 65:
                                case 66:
                                case 68:
                                case 69:
                                case 70:
                                case 71:
                                case 72:
                                case 73:
                                case 74:
                                case 78:
                                case 83:
                                case 84:
                                case 85:
                                case 87:
                                case 91:
                                case 106:
                                case 107:
                                case 128:
                                case 135:
                                case 136:
                                case 137:
                                    break;
                            }
                            BlockStatement();
                            continue;
                        end_IL_015f:
                            break;
                        }
                    }
                    catch (System.Exception x)
                    {
                        ex = x;
                        goto end_IL_001e;
                    }
                }
                catch (System.Exception x2)
                {
                    ex2 = x2;
                    goto IL_0445;
                }
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTJavacodeBody, b: true);
                    jjtreeCloseNodeScope(aSTJavacodeBody);
                }
                goto IL_054d;
            end_IL_001e:;
            }
            catch (System.Exception x3)
            {
                ex3 = x3;
                goto IL_0451;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                jjtreeCloseNodeScope(aSTJavacode);
            }
            throw;
        }
        System.Exception ex4 = ex;
        System.Exception ex6;
        try
        {
            //ex4 = ex4;
            try
            {
                //ex4 = ex4;
                try
                {
                    System.Exception ex5 = ex4;
                    if (num2 != 0)
                    {
                        jjtree.ClearNodeScope(aSTJavacodeBody);
                        num2 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex5 is RuntimeException)
                    {
                        throw (ex5);
                    }
                    if (ex5 is ParseException)
                    {
                        throw (ex5);
                    }
                    throw (ex5);
                }
                catch (System.Exception x4)
                {
                    ex4 = x4;
                }
            }
            catch (System.Exception x5)
            {
                ex6 = x5;
                goto IL_04f1;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                jjtreeCloseNodeScope(aSTJavacode);
            }
            throw;
        }
        System.Exception ex7 = ex4;
        goto IL_04f8;
    IL_0451:
        System.Exception ex8 = ex3;
        goto IL_05c9;
    IL_054d:
        System.Exception ex9;
        try
        {
            try
            {
                jj_consume_token(88);
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                num = 0;
                jjtreeCloseNodeScope(aSTJavacode);
                JJTreeGlobals.Productions.Add(aSTJavacode2.Name, aSTJavacode2);
            }
            catch (System.Exception x6)
            {
                ex9 = x6;
                goto IL_05a6;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                jjtreeCloseNodeScope(aSTJavacode);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTJavacode, b: true);
            jjtreeCloseNodeScope(aSTJavacode);
        }
        return;
    IL_05a6:
        ex8 = ex9;
        goto IL_05c9;
    IL_04f8:
        System.Exception ex10 = ex7;
        try
        {
            //ex10 = ex10;
            try
            {
                System.Exception ex11 = ex10;
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTJavacodeBody, b: true);
                    jjtreeCloseNodeScope(aSTJavacodeBody);
                }
                throw (ex11);
            }
            catch (System.Exception x7)
            {
                ex10 = x7;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                jjtreeCloseNodeScope(aSTJavacode);
            }
            throw;
        }
        ex8 = ex10;
        goto IL_05c9;
    IL_05c9:
        System.Exception ex12 = ex8;
        try
        {
            System.Exception ex13 = ex12;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTJavacode);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex13 is RuntimeException)
            {
                throw (ex13);
            }
            if (ex13 is ParseException)
            {
                throw (ex13);
            }
            throw (ex13);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTJavacode, b: true);
                jjtreeCloseNodeScope(aSTJavacode);
            }
            throw;
        }
    IL_0445:
        ex7 = ex2;
        goto IL_04f8;
    IL_04f1:
        ex8 = ex6;
        goto IL_05c9;
    }


    public void regular_expr_production()
    {
        ASTRE aSTRE = new ASTRE(11);
        int num = 1;
        jjtree.OpenNodeScope(aSTRE);
        jjtreeOpenNodeScope(aSTRE);
        System.Exception ex;
        try
        {
            try
            {
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
                {
                    if (jj_2_1(2))
                    {
                        jj_consume_token(95);
                        jj_consume_token(110);
                        jj_consume_token(126);
                    }
                    else
                    {
                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) != 95)
                        {
                            jj_la1[16] = jj_gen;
                            jj_consume_token(-1);

                            throw new ParseException();
                        }
                        jj_consume_token(95);
                        jj_consume_token(137);
                        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                        {
                            jj_consume_token(92);
                            jj_consume_token(137);
                        }
                        jj_la1[15] = jj_gen;
                        jj_consume_token(126);
                    }
                }
                else
                {
                    jj_la1[17] = jj_gen;
                }
                regexpr_kind();
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 89)
                {
                    jj_consume_token(89);
                    jj_consume_token(2);
                    jj_consume_token(90);
                }
                else
                {
                    jj_la1[18] = jj_gen;
                }
                jj_consume_token(99);
                jj_consume_token(87);
                regexpr_spec();
                while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 113)
                {
                    jj_consume_token(113);
                    regexpr_spec();
                }
                jj_la1[19] = jj_gen;
                jj_consume_token(88);
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_020e;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRE, b: true);
                jjtreeCloseNodeScope(aSTRE);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTRE, b: true);
            jjtreeCloseNodeScope(aSTRE);
        }
        return;
    IL_020e:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTRE);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRE, b: true);
                jjtreeCloseNodeScope(aSTRE);
            }
            throw;
        }
    }


    public void token_manager_decls()
    {
        ASTTokenDecls aSTTokenDecls = new ASTTokenDecls(12);
        int num = 1;
        jjtree.OpenNodeScope(aSTTokenDecls);
        jjtreeOpenNodeScope(aSTTokenDecls);
        System.Exception ex;
        try
        {
            try
            {
                jj_consume_token(10);
                jj_consume_token(99);
                ClassOrInterfaceBody();
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_005c;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTTokenDecls, b: true);
                jjtreeCloseNodeScope(aSTTokenDecls);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTTokenDecls, b: true);
            jjtreeCloseNodeScope(aSTTokenDecls);
        }
        return;
    IL_005c:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTTokenDecls);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTTokenDecls, b: true);
                jjtreeCloseNodeScope(aSTTokenDecls);
            }
            throw;
        }
    }


    public void bnf_production()
    {
        ASTBNF aSTBNF = new ASTBNF(8);
        int num = 1;
        jjtree.OpenNodeScope(aSTBNF);
        jjtreeOpenNodeScope(aSTBNF);
        ASTNodeDescriptor aSTNodeDescriptor = null;
        ASTBNF aSTBNF2;
        NodeScope node_scope;
        ASTBNFDeclaration aSTBNFDeclaration;
        int num2;
        System.Exception ex;
        System.Exception ex2;
        System.Exception ex3;
        try
        {
            try
            {
                aSTBNF2 = aSTBNF;
                AccessModifier();
                ResultType();
                Token token = identifier();
                FormalParameters();
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 67)
                {
                    jj_consume_token(67);
                    string obj = Name();
                    aSTBNF2.ThrowsList.Add(obj);
                    while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                    {
                        jj_consume_token(92);
                        obj = Name();
                        aSTBNF2.ThrowsList.Add(obj);
                    }
                    jj_la1[9] = jj_gen;
                }
                else
                {
                    jj_la1[10] = jj_gen;
                }
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 127)
                {
                    aSTNodeDescriptor = node_descriptor();
                }
                else
                {
                    jj_la1[11] = jj_gen;
                }
                jj_consume_token(99);
                Token declBeginLoc = jj_consume_token(87);
                aSTBNF2.Name = token.Image;
                aSTBNF2.DeclBeginLoc = declBeginLoc;
                node_scope = new NodeScope(aSTBNF2, aSTNodeDescriptor);
                aSTBNFDeclaration = new ASTBNFDeclaration(9);
                num2 = 1;
                jjtree.OpenNodeScope(aSTBNFDeclaration);
                jjtreeOpenNodeScope(aSTBNFDeclaration);
                try
                {
                    try
                    {
                        while (true)
                        {
                            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                            {
                                default:
                                    jj_la1[12] = jj_gen;
                                    jjtree.CloseNodeScope(aSTBNFDeclaration, b: true);
                                    num2 = 0;
                                    jjtreeCloseNodeScope(aSTBNFDeclaration);
                                    aSTBNFDeclaration.nodeScope = node_scope;
                                    goto end_IL_016d;
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
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 30:
                                case 31:
                                case 33:
                                case 35:
                                case 36:
                                case 39:
                                case 40:
                                case 42:
                                case 43:
                                case 45:
                                case 49:
                                case 50:
                                case 51:
                                case 52:
                                case 53:
                                case 54:
                                case 56:
                                case 57:
                                case 58:
                                case 59:
                                case 60:
                                case 61:
                                case 62:
                                case 63:
                                case 64:
                                case 65:
                                case 66:
                                case 68:
                                case 69:
                                case 70:
                                case 71:
                                case 72:
                                case 73:
                                case 74:
                                case 78:
                                case 83:
                                case 84:
                                case 85:
                                case 87:
                                case 91:
                                case 106:
                                case 107:
                                case 128:
                                case 135:
                                case 136:
                                case 137:
                                    break;
                            }
                            BlockStatement();
                            continue;
                        end_IL_016d:
                            break;
                        }
                    }
                    catch (System.Exception x)
                    {
                        ex = x;
                        goto end_IL_001e;
                    }
                }
                catch (System.Exception x2)
                {
                    ex2 = x2;
                    goto IL_044f;
                }
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFDeclaration, b: true);
                    jjtreeCloseNodeScope(aSTBNFDeclaration);
                }
                goto IL_0557;
            end_IL_001e:;
            }
            catch (System.Exception x3)
            {
                ex3 = x3;
                goto IL_045b;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        System.Exception ex4 = ex;
        System.Exception ex6;
        try
        {
            try
            {
                try
                {
                    System.Exception ex5 = ex4;
                    if (num2 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFDeclaration);
                        num2 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex5 is RuntimeException)
                    {
                        throw (ex5);
                    }
                    if (ex5 is ParseException)
                    {
                        throw (ex5);
                    }
                    throw (ex5);
                }
                catch (System.Exception x4)
                {
                    ex4 = x4;
                }
            }
            catch (System.Exception x5)
            {
                ex6 = x5;
                goto IL_04fb;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        System.Exception ex7 = ex4;
        goto IL_0502;
    IL_045b:
        System.Exception ex8 = ex3;
        goto IL_07a0;
    IL_0557:
        ASTBNFNodeScope aSTBNFNodeScope;
        int num3;
        System.Exception ex9;
        System.Exception ex10;
        System.Exception ex11;
        try
        {
            try
            {
                jj_consume_token(88);
                jj_consume_token(87);
                aSTBNFNodeScope = new ASTBNFNodeScope(10);
                num3 = 1;
                jjtree.OpenNodeScope(aSTBNFNodeScope);
                jjtreeOpenNodeScope(aSTBNFNodeScope);
                try
                {
                    try
                    {
                        JJTreeNode jJTreeNode = expansion_choices(aSTBNF2);
                        jjtree.CloseNodeScope(aSTBNFNodeScope, b: true);
                        num3 = 0;
                        jjtreeCloseNodeScope(aSTBNFNodeScope);
                        aSTBNFNodeScope.nodeScope = node_scope;
                        aSTBNFNodeScope.expUnit = jJTreeNode;
                    }
                    catch (System.Exception x6)
                    {
                        ex9 = x6;
                        goto end_IL_0557;
                    }
                }
                catch (System.Exception x7)
                {
                    ex10 = x7;
                    goto IL_061c;
                }
                if (num3 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFNodeScope, b: true);
                    jjtreeCloseNodeScope(aSTBNFNodeScope);
                }
                goto IL_0724;
            end_IL_0557:;
            }
            catch (System.Exception x8)
            {
                ex11 = x8;
                goto IL_0628;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        System.Exception ex12 = ex9;
        System.Exception ex14;
        try
        {
            try
            {
                try
                {
                    System.Exception ex13 = ex12;
                    if (num3 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFNodeScope);
                        num3 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex13 is RuntimeException)
                    {
                        throw (ex13);
                    }
                    if (ex13 is ParseException)
                    {
                        throw (ex13);
                    }
                    throw (ex13);
                }
                catch (System.Exception x9)
                {
                    ex12 = x9;
                }
            }
            catch (System.Exception x10)
            {
                ex14 = x10;
                goto IL_06c8;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        System.Exception ex15 = ex12;
        goto IL_06cf;
    IL_0628:
        ex8 = ex11;
        goto IL_07a0;
    IL_0724:
        System.Exception ex16;
        try
        {
            try
            {
                jj_consume_token(88);
                jjtree.CloseNodeScope(aSTBNF, b: true);
                num = 0;
                jjtreeCloseNodeScope(aSTBNF);
                JJTreeGlobals.Productions.Add(aSTBNF2.Name, aSTBNF2);
            }
            catch (System.Exception x11)
            {
                ex16 = x11;
                goto IL_077d;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTBNF, b: true);
            jjtreeCloseNodeScope(aSTBNF);
        }
        return;
    IL_077d:
        ex8 = ex16;
        goto IL_07a0;
    IL_044f:
        ex7 = ex2;
        goto IL_0502;
    IL_0502:
        System.Exception ex17 = ex7;
        try
        {
            try
            {
                System.Exception ex18 = ex17;
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFDeclaration, b: true);
                    jjtreeCloseNodeScope(aSTBNFDeclaration);
                }
                throw (ex18);
            }
            catch (System.Exception x12)
            {
                ex17 = x12;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        ex8 = ex17;
        goto IL_07a0;
    IL_061c:
        ex15 = ex10;
        goto IL_06cf;
    IL_07a0:
        System.Exception ex19 = ex8;
        try
        {
            System.Exception ex20 = ex19;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTBNF);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex20 is RuntimeException)
            {
                throw (ex20);
            }
            if (ex20 is ParseException)
            {
                throw (ex20);
            }
            throw (ex20);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
    IL_06c8:
        ex8 = ex14;
        goto IL_07a0;
    IL_04fb:
        ex8 = ex6;
        goto IL_07a0;
    IL_06cf:
        System.Exception ex21 = ex15;
        try
        {
            try
            {
                System.Exception ex22 = ex21;
                if (num3 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFNodeScope, b: true);
                    jjtreeCloseNodeScope(aSTBNFNodeScope);
                }
                throw (ex22);
            }
            catch (System.Exception x13)
            {
                ex21 = x13;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNF, b: true);
                jjtreeCloseNodeScope(aSTBNF);
            }
            throw;
        }
        ex8 = ex21;
        goto IL_07a0;
    }


    public void AccessModifier()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 56:
            case 57:
            case 58:
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 58:
                        jj_consume_token(58);
                        break;
                    case 57:
                        jj_consume_token(57);
                        break;
                    case 56:
                        jj_consume_token(56);
                        break;
                    default:
                        jj_la1[13] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                break;
            default:
                jj_la1[14] = jj_gen;
                break;
        }
    }


    public void ResultType()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 71:
                jj_consume_token(71);
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 42:
            case 49:
            case 51:
            case 60:
            case 137:
                Type();
                return;
        }
        jj_la1[104] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void FormalParameters()
    {
        jj_consume_token(85);
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
            case 24:
            case 25:
            case 27:
            case 30:
            case 36:
            case 40:
            case 42:
            case 49:
            case 51:
            case 52:
            case 56:
            case 57:
            case 58:
            case 60:
            case 61:
            case 64:
            case 68:
            case 72:
            case 128:
            case 136:
            case 137:
                FormalParameter();
                while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                {
                    jj_consume_token(92);
                    FormalParameter();
                }
                jj_la1[89] = jj_gen;
                break;
            default:
                jj_la1[90] = jj_gen;
                break;
        }
        jj_consume_token(86);
    }


    public string Name()
    {
        string text = JavaIdentifier();
        while (jj_2_22(2))
        {
            jj_consume_token(93);
            string str = JavaIdentifier();
            text = (text) + (".") + (str)
                ;
        }
        return text;
    }


    public ASTNodeDescriptor node_descriptor()
    {
        ASTNodeDescriptor aSTNodeDescriptor = new ASTNodeDescriptor(39);
        int num = 1;
        jjtree.OpenNodeScope(aSTNodeDescriptor);
        jjtreeOpenNodeScope(aSTNodeDescriptor);


        ASTNodeDescriptor result;
        System.Exception ex;
        try
        {
            try
            {
                jj_consume_token(127);
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
                    case 137:
                        {
                            string text = (aSTNodeDescriptor.Name = Name());
                            break;
                        }
                    case 71:
                        {
                            Token token = jj_consume_token(71);
                            aSTNodeDescriptor.Name = token.Image;
                            break;
                        }
                    default:
                        jj_la1[48] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 85)
                {
                    jj_consume_token(85);
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 126)
                    {
                        jj_consume_token(126);
                        aSTNodeDescriptor.isGT = true;
                    }
                    else
                    {
                        jj_la1[49] = jj_gen;
                    }
                    node_descriptor_expression();
                    aSTNodeDescriptor.expression = (ASTNodeDescriptorExpression)jjtree.PeekNode;
                    jj_consume_token(86);
                }
                else
                {
                    jj_la1[50] = jj_gen;
                }
                jjtree.CloseNodeScope(aSTNodeDescriptor, b: true);
                num = 0;
                jjtreeCloseNodeScope(aSTNodeDescriptor);
                aSTNodeDescriptor.SetNodeId();
                result = aSTNodeDescriptor;
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_01ea;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTNodeDescriptor, b: true);
                jjtreeCloseNodeScope(aSTNodeDescriptor);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTNodeDescriptor, b: true);
            jjtreeCloseNodeScope(aSTNodeDescriptor);
        }
        return result;
    IL_01ea:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTNodeDescriptor);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTNodeDescriptor, b: true);
                jjtreeCloseNodeScope(aSTNodeDescriptor);
            }
            throw;
        }
    }


    public void BlockStatement()
    {
        if (jj_2_41(int.MaxValue))
        {
            LocalVariableDeclaration();
            jj_consume_token(91);
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
            case 25:
            case 26:
            case 27:
            case 30:
            case 33:
            case 35:
            case 36:
            case 39:
            case 42:
            case 43:
            case 45:
            case 49:
            case 51:
            case 53:
            case 54:
            case 59:
            case 60:
            case 62:
            case 63:
            case 64:
            case 65:
            case 66:
            case 69:
            case 70:
            case 71:
            case 73:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 87:
            case 91:
            case 106:
            case 107:
            case 135:
            case 137:
                Statement();
                return;
            case 31:
            case 50:
                ClassOrInterfaceDeclaration();
                return;
        }
        jj_la1[149] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public JJTreeNode expansion_choices(ASTProduction astp)
    {
        ASTBNFChoice aSTBNFChoice = new ASTBNFChoice(14);
        int num = 1;
        jjtree.OpenNodeScope(aSTBNFChoice);
        jjtreeOpenNodeScope(aSTBNFChoice);
        JJTreeNode result;
        System.Exception ex;
        try
        {
            try
            {
                expansion(astp);
                while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 113)
                {
                    jj_consume_token(113);
                    expansion(astp);
                }
                jj_la1[23] = jj_gen;
                jjtree.CloseNodeScope(aSTBNFChoice, jjtree.NodeArity > 1);
                num = 0;
                jjtreeCloseNodeScope(aSTBNFChoice);
                result = (JJTreeNode)jjtree.PeekNode;
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_00d3;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFChoice, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFChoice);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTBNFChoice, jjtree.NodeArity > 1);
            jjtreeCloseNodeScope(aSTBNFChoice);
        }
        return result;
    IL_00d3:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTBNFChoice);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFChoice, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFChoice);
            }
            throw;
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


    public void regexpr_kind()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 6:
                jj_consume_token(6);
                return;
            case 7:
                jj_consume_token(7);
                return;
            case 9:
                jj_consume_token(9);
                return;
            case 8:
                jj_consume_token(8);
                return;
        }
        jj_la1[20] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void regexpr_spec()
    {
        ASTRESpec aSTRESpec = new ASTRESpec(13);
        int num = 1;
        jjtree.OpenNodeScope(aSTRESpec);
        jjtreeOpenNodeScope(aSTRESpec);
        System.Exception ex;
        try
        {
            try
            {
                regular_expression();
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 87)
                {
                    Block();
                }
                else
                {
                    jj_la1[21] = jj_gen;
                }
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 99)
                {
                    jj_consume_token(99);
                    jj_consume_token(137);
                }
                else
                {
                    jj_la1[22] = jj_gen;
                }
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_00cb;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRESpec, b: true);
                jjtreeCloseNodeScope(aSTRESpec);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTRESpec, b: true);
            jjtreeCloseNodeScope(aSTRESpec);
        }
        return;
    IL_00cb:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTRESpec);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRESpec, b: true);
                jjtreeCloseNodeScope(aSTRESpec);
            }
            throw;
        }
    }


    public void ClassOrInterfaceBody()
    {
        jj_consume_token(87);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[75] = jj_gen;
                    jj_consume_token(88);
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
                case 24:
                case 25:
                case 27:
                case 30:
                case 31:
                case 36:
                case 40:
                case 42:
                case 49:
                case 50:
                case 51:
                case 52:
                case 56:
                case 57:
                case 58:
                case 60:
                case 61:
                case 64:
                case 68:
                case 71:
                case 72:
                case 87:
                case 91:
                case 95:
                case 128:
                case 129:
                case 136:
                case 137:
                    break;
            }
            ClassOrInterfaceBodyDeclaration();
        }
    }


    public void regular_expression()
    {
        ASTREStringLiteral aSTREStringLiteral;
        int num;
        System.Exception ex;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 84)
        {
            aSTREStringLiteral = new ASTREStringLiteral(26);
            num = 1;
            jjtree.OpenNodeScope(aSTREStringLiteral);
            jjtreeOpenNodeScope(aSTREStringLiteral);
            try
            {
                try
                {
                    StringLiteral();
                }
                catch (System.Exception x)
                {
                    ex = x;
                    goto IL_0070;
                }
            }
            catch
            {
                //try-fault
                if (num != 0)
                {
                    jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                    jjtreeCloseNodeScope(aSTREStringLiteral);
                }
                throw;
            }
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                jjtreeCloseNodeScope(aSTREStringLiteral);
            }
            return;
        }
        jj_la1[36] = jj_gen;
        ASTRENamed aSTRENamed;
        int num2;
        System.Exception ex2;
        if (jj_2_4(3))
        {
            aSTRENamed = new ASTRENamed(27);
            num2 = 1;
            jjtree.OpenNodeScope(aSTRENamed);
            jjtreeOpenNodeScope(aSTRENamed);
            try
            {
                try
                {
                    jj_consume_token(95);
                    int num3 = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                    if (num3 == 127 || num3 == 137)
                    {
                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 127)
                        {
                            jj_consume_token(127);
                        }
                        else
                        {
                            jj_la1[34] = jj_gen;
                        }
                        identifier();
                        jj_consume_token(99);
                    }
                    else
                    {
                        jj_la1[35] = jj_gen;
                    }
                    complex_regular_expression_choices();
                    jj_consume_token(126);
                }
                catch (System.Exception x2)
                {
                    ex2 = x2;
                    goto IL_0213;
                }
            }
            catch
            {
                //try-fault
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTRENamed, b: true);
                    jjtreeCloseNodeScope(aSTRENamed);
                }
                throw;
            }
            if (num2 != 0)
            {
                jjtree.CloseNodeScope(aSTRENamed, b: true);
                jjtreeCloseNodeScope(aSTRENamed);
            }
            return;
        }
        ASTREReference aSTREReference;
        System.Exception ex3;
        if (jj_2_5(2))
        {
            aSTREReference = new ASTREReference(28);
            num2 = 1;
            jjtree.OpenNodeScope(aSTREReference);
            jjtreeOpenNodeScope(aSTREReference);
            try
            {
                try
                {
                    jj_consume_token(95);
                    identifier();
                    jj_consume_token(126);
                }
                catch (System.Exception x3)
                {
                    ex3 = x3;
                    goto IL_032a;
                }
            }
            catch
            {
                //try-fault
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTREReference, b: true);
                    jjtreeCloseNodeScope(aSTREReference);
                }
                throw;
            }
            if (num2 != 0)
            {
                jjtree.CloseNodeScope(aSTREReference, b: true);
                jjtreeCloseNodeScope(aSTREReference);
            }
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            ASTREEOF aSTREEOF = new ASTREEOF(29);
            num2 = 1;
            jjtree.OpenNodeScope(aSTREEOF);
            jjtreeOpenNodeScope(aSTREEOF);
            try
            {
                jj_consume_token(95);
                jj_consume_token(11);
                jj_consume_token(126);
            }
            catch
            {
                //try-fault
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTREEOF, b: true);
                    jjtreeCloseNodeScope(aSTREEOF);
                }
                throw;
            }
            if (num2 != 0)
            {
                jjtree.CloseNodeScope(aSTREEOF, b: true);
                jjtreeCloseNodeScope(aSTREEOF);
            }
            return;
        }
        jj_la1[37] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    IL_0070:
        ex2 = ex;
        try
        {
            System.Exception ex4 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTREStringLiteral);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex4 is RuntimeException)
            {
                throw (ex4);
            }
            if (ex4 is ParseException)
            {
                throw (ex4);
            }
            throw (ex4);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                jjtreeCloseNodeScope(aSTREStringLiteral);
            }
            throw;
        }
    IL_032a:
        System.Exception ex5 = ex3;
        try
        {
            System.Exception ex6 = ex5;
            if (num2 != 0)
            {
                jjtree.ClearNodeScope(aSTREReference);
                num2 = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex6 is RuntimeException)
            {
                throw (ex6);
            }
            if (ex6 is ParseException)
            {
                throw (ex6);
            }
            throw (ex6);
        }
        catch
        {
            //try-fault
            if (num2 != 0)
            {
                jjtree.CloseNodeScope(aSTREReference, b: true);
                jjtreeCloseNodeScope(aSTREReference);
            }
            throw;
        }
    IL_0213:
        ex3 = ex2;
        try
        {
            System.Exception ex6 = ex3;
            if (num2 != 0)
            {
                jjtree.ClearNodeScope(aSTRENamed);
                num2 = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex6 is RuntimeException)
            {
                throw (ex6);
            }
            if (ex6 is ParseException)
            {
                throw (ex6);
            }
            throw (ex6);
        }
        catch
        {
            //try-fault
            if (num2 != 0)
            {
                jjtree.CloseNodeScope(aSTRENamed, b: true);
                jjtreeCloseNodeScope(aSTRENamed);
            }
            throw;
        }
    }


    public void Block()
    {
        jj_consume_token(87);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[148] = jj_gen;
                    jj_consume_token(88);
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
                case 24:
                case 25:
                case 26:
                case 27:
                case 30:
                case 31:
                case 33:
                case 35:
                case 36:
                case 39:
                case 40:
                case 42:
                case 43:
                case 45:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 87:
                case 91:
                case 106:
                case 107:
                case 128:
                case 135:
                case 136:
                case 137:
                    break;
            }
            BlockStatement();
        }
    }


    public void expansion(ASTProduction astp)
    {
        ASTBNFSequence aSTBNFSequence = new ASTBNFSequence(15);
        int num = 1;
        jjtree.OpenNodeScope(aSTBNFSequence);
        jjtreeOpenNodeScope(aSTBNFSequence);
        ASTNodeDescriptor aSTNodeDescriptor = null;
        ASTBNFLookahead aSTBNFLookahead;
        System.Exception ex;
        System.Exception ex2;
        System.Exception ex3;
        int num2;
        try
        {
            try
            {
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 1)
                {
                    aSTBNFLookahead = new ASTBNFLookahead(16);
                    num2 = 1;
                    jjtree.OpenNodeScope(aSTBNFLookahead);
                    jjtreeOpenNodeScope(aSTBNFLookahead);
                    try
                    {
                        try
                        {
                            jj_consume_token(1);
                            jj_consume_token(85);
                            local_lookahead(astp);
                            jj_consume_token(86);
                        }
                        catch (System.Exception x)
                        {
                            ex = x;
                            goto IL_00f4;
                        }
                    }
                    catch (System.Exception x2)
                    {
                        ex2 = x2;
                        goto IL_00f8;
                    }
                    if (num2 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFLookahead, b: true);
                        jjtreeCloseNodeScope(aSTBNFLookahead);
                    }
                    goto IL_0264;
                }
            }
            catch (System.Exception x3)
            {
                ex3 = x3;
                goto IL_0104;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            throw;
        }
        System.Exception ex4;
        try
        {
            try
            {
                jj_la1[24] = jj_gen;
            }
            catch (System.Exception x4)
            {
                ex4 = x4;
                goto IL_025d;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            throw;
        }
        goto IL_0264;
    IL_0104:
        System.Exception ex5 = ex3;
        goto IL_0533;
    IL_00f8:
        System.Exception ex6 = ex2;
        goto IL_01b7;
    IL_025d:
        ex5 = ex4;
        goto IL_0533;
    IL_0264:
        while (true)
        {
            ASTExpansionNodeScope aSTExpansionNodeScope;
            System.Exception ex7;
            System.Exception ex8;
            System.Exception ex9;
            try
            {
                try
                {
                    aSTExpansionNodeScope = new ASTExpansionNodeScope(17);
                    num2 = 1;
                    jjtree.OpenNodeScope(aSTExpansionNodeScope);
                    jjtreeOpenNodeScope(aSTExpansionNodeScope);
                    try
                    {
                        try
                        {
                            JJTreeNode jJTreeNode = expansion_unit(astp);
                            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 127)
                            {
                                aSTNodeDescriptor = node_descriptor();
                            }
                            else
                            {
                                jj_la1[25] = jj_gen;
                            }
                            jjtree.CloseNodeScope(aSTExpansionNodeScope, jjtree.NodeArity > 1);
                            num2 = 0;
                            jjtreeCloseNodeScope(aSTExpansionNodeScope);
                            if (jjtree.NodeCreated)
                            {
                                aSTExpansionNodeScope.nodeScope = new NodeScope(astp, aSTNodeDescriptor);
                                aSTExpansionNodeScope.expUnit = jJTreeNode;
                            }
                        }
                        catch (System.Exception x5)
                        {
                            ex7 = x5;
                            goto end_IL_0264;
                        }
                    }
                    catch (System.Exception x6)
                    {
                        ex8 = x6;
                        goto IL_0390;
                    }
                    if (num2 != 0)
                    {
                        jjtree.CloseNodeScope(aSTExpansionNodeScope, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTExpansionNodeScope);
                    }
                    goto IL_04bf;
                end_IL_0264:;
                }
                catch (System.Exception x7)
                {
                    ex9 = x7;
                    goto IL_039c;
                }
            }
            catch
            {
                //try-fault
                if (num != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                    jjtreeCloseNodeScope(aSTBNFSequence);
                }
                throw;
            }
            System.Exception ex10 = ex7;
            System.Exception ex12;
            try
            {
                try
                {
                    try
                    {
                        System.Exception ex11 = ex10;
                        if (num2 != 0)
                        {
                            jjtree.ClearNodeScope(aSTExpansionNodeScope);
                            num2 = 0;
                        }
                        else
                        {
                            jjtree.PopNode();
                        }
                        if (ex11 is RuntimeException)
                        {
                            throw (ex11);
                        }
                        if (ex11 is ParseException)
                        {
                            throw (ex11);
                        }
                        throw (ex11);
                    }
                    catch (System.Exception x8)
                    {
                        ex10 = x8;
                    }
                }
                catch (System.Exception x9)
                {
                    ex12 = x9;
                    goto IL_0449;
                }
            }
            catch
            {
                //try-fault
                if (num != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                    jjtreeCloseNodeScope(aSTBNFSequence);
                }
                throw;
            }
            System.Exception ex13 = ex10;
            goto IL_0450;
        IL_0450:
            System.Exception ex14 = ex13;
            try
            {
                try
                {
                    System.Exception ex15 = ex14;
                    if (num2 != 0)
                    {
                        jjtree.CloseNodeScope(aSTExpansionNodeScope, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTExpansionNodeScope);
                    }
                    throw (ex15);
                }
                catch (System.Exception x10)
                {
                    ex14 = x10;
                }
            }
            catch
            {
                //try-fault
                if (num != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                    jjtreeCloseNodeScope(aSTBNFSequence);
                }
                throw;
            }
            ex5 = ex14;
            break;
        IL_0503:
            System.Exception ex16;
            ex5 = ex16;
            break;
        IL_0449:
            ex5 = ex12;
            break;
        IL_039c:
            ex5 = ex9;
            break;
        IL_04bf:
            try
            {
                try
                {
                    if (notTailOfExpansionUnit())
                    {
                        continue;
                    }
                }
                catch (System.Exception x11)
                {
                    ex16 = x11;
                    goto IL_0503;
                }
            }
            catch
            {
                //try-fault
                if (num != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                    jjtreeCloseNodeScope(aSTBNFSequence);
                }
                throw;
            }
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            return;
        IL_0390:
            ex13 = ex8;
            goto IL_0450;
        }
        goto IL_0533;
    IL_0533:
        System.Exception ex17 = ex5;
        try
        {
            System.Exception ex18 = ex17;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTBNFSequence);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex18 is RuntimeException)
            {
                throw (ex18);
            }
            if (ex18 is ParseException)
            {
                throw (ex18);
            }
            throw (ex18);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            throw;
        }
    IL_01b0:
        System.Exception ex19;
        ex5 = ex19;
        goto IL_0533;
    IL_01b7:
        System.Exception ex20 = ex6;
        try
        {
            try
            {
                System.Exception ex21 = ex20;
                if (num2 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFLookahead, b: true);
                    jjtreeCloseNodeScope(aSTBNFLookahead);
                }
                throw (ex21);
            }
            catch (System.Exception x12)
            {
                ex20 = x12;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            throw;
        }
        ex5 = ex20;
        goto IL_0533;
    IL_00f4:
        System.Exception ex22 = ex;
        try
        {
            try
            {
                try
                {
                    System.Exception ex11 = ex22;
                    if (num2 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFLookahead);
                        num2 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex11 is RuntimeException)
                    {
                        throw (ex11);
                    }
                    if (ex11 is ParseException)
                    {
                        throw (ex11);
                    }
                    throw (ex11);
                }
                catch (System.Exception x13)
                {
                    ex22 = x13;
                }
            }
            catch (System.Exception x14)
            {
                ex19 = x14;
                goto IL_01b0;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTBNFSequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTBNFSequence);
            }
            throw;
        }
        ex6 = ex22;
        goto IL_01b7;
    }


    public void local_lookahead(ASTProduction astp)
    {
        int num = 0;
        int num2 = 1;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 74)
        {
            IntegerLiteral();
            num2 = 0;
        }
        else
        {
            jj_la1[26] = jj_gen;
        }
        if (num2 == 0 && getToken(1).Kind != 86)
        {
            jj_consume_token(92);
            num = 1;
        }
        if (getToken(1).Kind != 86 && getToken(1).Kind != 87)
        {
            expansion_choices(astp);
            num2 = 0;
            num = 0;
        }
        if (num2 == 0 && num == 0 && getToken(1).Kind != 86)
        {
            jj_consume_token(92);
            num = 1;
        }
        if (num2 != 0 || num != 0)
        {
            jj_consume_token(87);
            Expression();
            jj_consume_token(88);
        }
    }


    public JJTreeNode expansion_unit(ASTProduction astp)
    {
        ASTBNFLookahead aSTBNFLookahead;
        int num6;
        System.Exception ex5;
        ASTBNFAction aSTBNFAction;
        int num5;
        ASTBNFZeroOrOne aSTBNFZeroOrOne2;
        int num7;
        ASTBNFTryBlock aSTBNFTryBlock;
        int num8;
        ASTBNFAssignment aSTBNFAssignment;
        ASTBNFNonTerminal aSTBNFNonTerminal;
        System.Exception ex2;
        System.Exception ex3;
        int num;
        int num2;
        System.Exception ex4;
        System.Exception ex7;
        System.Exception ex8;
        System.Exception ex9;
        System.Exception ex11;
        System.Exception ex13;
        System.Exception ex15;
        System.Exception ex17;
        System.Exception ex12;
        System.Exception ex19;
        System.Exception ex;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 1:
                aSTBNFLookahead = new ASTBNFLookahead(16);
                num6 = 1;
                jjtree.OpenNodeScope(aSTBNFLookahead);
                jjtreeOpenNodeScope(aSTBNFLookahead);
                try
                {
                    try
                    {
                        jj_consume_token(1);
                        jj_consume_token(85);
                        local_lookahead(astp);
                        jj_consume_token(86);
                    }
                    catch (System.Exception x5)
                    {
                        ex5 = x5;
                        goto IL_00aa;
                    }
                }
                catch
                {
                    //try-fault
                    if (num6 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFLookahead, b: true);
                        jjtreeCloseNodeScope(aSTBNFLookahead);
                    }
                    throw;
                }
                if (num6 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFLookahead, b: true);
                    jjtreeCloseNodeScope(aSTBNFLookahead);
                }
                break;
            case 87:
                aSTBNFAction = new ASTBNFAction(18);
                num5 = 1;
                jjtree.OpenNodeScope(aSTBNFAction);
                jjtreeOpenNodeScope(aSTBNFAction);
                try
                {
                    try
                    {
                        Block();
                    }
                    catch (System.Exception x4)
                    {
                        ex4 = x4;
                        goto IL_0196;
                    }
                }
                catch
                {
                    //try-fault
                    if (num5 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAction, b: true);
                        jjtreeCloseNodeScope(aSTBNFAction);
                    }
                    throw;
                }
                if (num5 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFAction, b: true);
                    jjtreeCloseNodeScope(aSTBNFAction);
                }
                break;
            case 89:
                aSTBNFZeroOrOne2 = new ASTBNFZeroOrOne(19);
                num7 = 1;
                jjtree.OpenNodeScope(aSTBNFZeroOrOne2);
                jjtreeOpenNodeScope(aSTBNFZeroOrOne2);
                try
                {
                    try
                    {
                        jj_consume_token(89);
                        expansion_choices(astp);
                        jj_consume_token(90);
                    }
                    catch (System.Exception x8)
                    {
                        ex12 = x8;
                        goto IL_02a2;
                    }
                }
                catch
                {
                    //try-fault
                    if (num7 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFZeroOrOne2, b: true);
                        jjtreeCloseNodeScope(aSTBNFZeroOrOne2);
                    }
                    throw;
                }
                if (num7 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFZeroOrOne2, b: true);
                    jjtreeCloseNodeScope(aSTBNFZeroOrOne2);
                }
                break;
            case 70:
                aSTBNFTryBlock = new ASTBNFTryBlock(20);
                num8 = 1;
                jjtree.OpenNodeScope(aSTBNFTryBlock);
                jjtreeOpenNodeScope(aSTBNFTryBlock);
                try
                {
                    try
                    {
                        jj_consume_token(70);
                        jj_consume_token(87);
                        expansion_choices(astp);
                        jj_consume_token(88);
                        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 29)
                        {
                            jj_consume_token(29);
                            jj_consume_token(85);
                            Name();
                            jj_consume_token(137);
                            jj_consume_token(86);
                            Block();
                        }
                        jj_la1[27] = jj_gen;
                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 41)
                        {
                            jj_consume_token(41);
                            Block();
                        }
                        else
                        {
                            jj_la1[28] = jj_gen;
                        }
                    }
                    catch (System.Exception x11)
                    {
                        ex15 = x11;
                        goto IL_0465;
                    }
                }
                catch
                {
                    //try-fault
                    if (num8 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFTryBlock, b: true);
                        jjtreeCloseNodeScope(aSTBNFTryBlock);
                    }
                    throw;
                }
                if (num8 != 0)
                {
                    jjtree.CloseNodeScope(aSTBNFTryBlock, b: true);
                    jjtreeCloseNodeScope(aSTBNFTryBlock);
                }
                break;
            default:
                {
                    jj_la1[32] = jj_gen;
                    if (jj_2_3(int.MaxValue))
                    {
                        aSTBNFAssignment = new ASTBNFAssignment(22);
                        num = 1;
                        jjtree.OpenNodeScope(aSTBNFAssignment);
                        jjtreeOpenNodeScope(aSTBNFAssignment);
                        try
                        {
                            try
                            {
                                if (jj_2_2(int.MaxValue))
                                {
                                    PrimaryExpression();
                                    jj_consume_token(94);
                                }
                                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                                {
                                    case 84:
                                    case 95:
                                        regular_expression();
                                        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 93)
                                        {
                                            jj_consume_token(93);
                                            jj_consume_token(137);
                                        }
                                        else
                                        {
                                            jj_la1[29] = jj_gen;
                                        }
                                        goto end_IL_054c;
                                    case 137:
                                        aSTBNFNonTerminal = new ASTBNFNonTerminal(21);
                                        num2 = 1;
                                        jjtree.OpenNodeScope(aSTBNFNonTerminal);
                                        jjtreeOpenNodeScope(aSTBNFNonTerminal);
                                        try
                                        {
                                            try
                                            {
                                                identifier();
                                                Arguments();
                                            }
                                            catch (System.Exception x)
                                            {
                                                ex = x;
                                                goto IL_06a2;
                                            }
                                        }
                                        catch (System.Exception x2)
                                        {
                                            ex2 = x2;
                                            goto IL_06a6;
                                        }
                                        if (num2 != 0)
                                        {
                                            jjtree.CloseNodeScope(aSTBNFNonTerminal, b: true);
                                            jjtreeCloseNodeScope(aSTBNFNonTerminal);
                                        }
                                        goto end_IL_054c;
                                }
                                goto IL_07d0;
                            end_IL_054c:;
                            }
                            catch (System.Exception x3)
                            {
                                ex3 = x3;
                                goto IL_06b2;
                            }
                        }
                        catch
                        {
                            //try-fault
                            if (num != 0)
                            {
                                jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                                jjtreeCloseNodeScope(aSTBNFAssignment);
                            }
                            throw;
                        }
                        if (num != 0)
                        {
                            jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                            jjtreeCloseNodeScope(aSTBNFAssignment);
                        }
                        break;
                    }
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 85)
                    {
                        Token firstToken = jj_consume_token(85);
                        expansion_choices(astp);
                        jj_consume_token(86);
                        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                        {
                            case 108:
                                {
                                    ASTBNFOneOrMore aSTBNFOneOrMore = new ASTBNFOneOrMore(23);
                                    num = 1;
                                    jjtree.OpenNodeScope(aSTBNFOneOrMore);
                                    jjtreeOpenNodeScope(aSTBNFOneOrMore);
                                    try
                                    {
                                        jj_consume_token(108);
                                    }
                                    catch
                                    {
                                        //try-fault
                                        if (num != 0)
                                        {
                                            jjtree.CloseNodeScope(aSTBNFOneOrMore, 1);
                                            jjtreeCloseNodeScope(aSTBNFOneOrMore);
                                        }
                                        throw;
                                    }
                                    if (num != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTBNFOneOrMore, 1);
                                        jjtreeCloseNodeScope(aSTBNFOneOrMore);
                                    }
                                    break;
                                }
                            case 110:
                                {
                                    ASTBNFZeroOrMore aSTBNFZeroOrMore = new ASTBNFZeroOrMore(24);
                                    num2 = 1;
                                    jjtree.OpenNodeScope(aSTBNFZeroOrMore);
                                    jjtreeOpenNodeScope(aSTBNFZeroOrMore);
                                    try
                                    {
                                        jj_consume_token(110);
                                    }
                                    catch
                                    {
                                        //try-fault
                                        if (num2 != 0)
                                        {
                                            jjtree.CloseNodeScope(aSTBNFZeroOrMore, 1);
                                            jjtreeCloseNodeScope(aSTBNFZeroOrMore);
                                        }
                                        throw;
                                    }
                                    if (num2 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTBNFZeroOrMore, 1);
                                        jjtreeCloseNodeScope(aSTBNFZeroOrMore);
                                    }
                                    break;
                                }
                            case 98:
                                {
                                    ASTBNFZeroOrOne aSTBNFZeroOrOne = new ASTBNFZeroOrOne(19);
                                    int num4 = 1;
                                    jjtree.OpenNodeScope(aSTBNFZeroOrOne);
                                    jjtreeOpenNodeScope(aSTBNFZeroOrOne);
                                    try
                                    {
                                        jj_consume_token(98);
                                    }
                                    catch
                                    {
                                        //try-fault
                                        if (num4 != 0)
                                        {
                                            jjtree.CloseNodeScope(aSTBNFZeroOrOne, 1);
                                            jjtreeCloseNodeScope(aSTBNFZeroOrOne);
                                        }
                                        throw;
                                    }
                                    if (num4 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTBNFZeroOrOne, 1);
                                        jjtreeCloseNodeScope(aSTBNFZeroOrOne);
                                    }
                                    break;
                                }
                            default:
                                {
                                    jj_la1[31] = jj_gen;
                                    ASTBNFParenthesized aSTBNFParenthesized = new ASTBNFParenthesized(25);
                                    int num3 = 1;
                                    jjtree.OpenNodeScope(aSTBNFParenthesized);
                                    jjtreeOpenNodeScope(aSTBNFParenthesized);
                                    try
                                    {
                                        jjtree.CloseNodeScope(aSTBNFParenthesized, 1);
                                        num3 = 0;
                                        jjtreeCloseNodeScope(aSTBNFParenthesized);
                                    }
                                    catch
                                    {
                                        //try-fault
                                        if (num3 != 0)
                                        {
                                            jjtree.CloseNodeScope(aSTBNFParenthesized, 1);
                                            jjtreeCloseNodeScope(aSTBNFParenthesized);
                                        }
                                        throw;
                                    }
                                    if (num3 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTBNFParenthesized, 1);
                                        jjtreeCloseNodeScope(aSTBNFParenthesized);
                                    }
                                    break;
                                }
                        }
                        ((JJTreeNode)jjtree.PeekNode).FirstToken = firstToken;
                        break;
                    }
                    jj_la1[33] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
                }
            IL_00aa:
                ex4 = ex5;
                try
                {
                    System.Exception ex6 = ex4;
                    if (num6 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFLookahead);
                        num6 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex6 is RuntimeException)
                    {
                        throw (ex6);
                    }
                    if (ex6 is ParseException)
                    {
                        throw (ex6);
                    }
                    throw (ex6);
                }
                catch
                {
                    //try-fault
                    if (num6 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFLookahead, b: true);
                        jjtreeCloseNodeScope(aSTBNFLookahead);
                    }
                    throw;
                }
            IL_06b2:
                ex7 = ex3;
                goto IL_0857;
            IL_06a6:
                ex8 = ex2;
                goto IL_0769;
            IL_06a2:
                ex9 = ex;
                try
                {
                    //ex9 = ex9;
                    try
                    {
                        //ex9 = ex9;
                        try
                        {
                            System.Exception ex10 = ex9;
                            if (num2 != 0)
                            {
                                jjtree.ClearNodeScope(aSTBNFNonTerminal);
                                num2 = 0;
                            }
                            else
                            {
                                jjtree.PopNode();
                            }
                            if (ex10 is RuntimeException)
                            {
                                throw (ex10);
                            }
                            if (ex10 is ParseException)
                            {
                                throw (ex10);
                            }
                            throw (ex10);
                        }
                        catch (System.Exception x6)
                        {
                            ex9 = x6;
                        }
                    }
                    catch (System.Exception x7)
                    {
                        ex11 = x7;
                        goto IL_0762;
                    }
                }
                catch
                {
                    //try-fault
                    if (num != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTBNFAssignment);
                    }
                    throw;
                }
                ex8 = ex9;
                goto IL_0769;
            IL_0769:
                ex13 = ex8;
                try
                {
                    try
                    {
                        System.Exception ex14 = ex13;
                        if (num2 != 0)
                        {
                            jjtree.CloseNodeScope(aSTBNFNonTerminal, b: true);
                            jjtreeCloseNodeScope(aSTBNFNonTerminal);
                        }
                        throw (ex14);
                    }
                    catch (System.Exception x9)
                    {
                        ex13 = x9;
                    }
                }
                catch
                {
                    //try-fault
                    if (num != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTBNFAssignment);
                    }
                    throw;
                }
                ex7 = ex13;
                goto IL_0857;
            IL_02a2:
                ex15 = ex12;
                try
                {
                    System.Exception ex16 = ex15;
                    if (num7 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFZeroOrOne2);
                        num7 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex16 is RuntimeException)
                    {
                        throw (ex16);
                    }
                    if (ex16 is ParseException)
                    {
                        throw (ex16);
                    }
                    throw (ex16);
                }
                catch
                {
                    //try-fault
                    if (num7 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFZeroOrOne2, b: true);
                        jjtreeCloseNodeScope(aSTBNFZeroOrOne2);
                    }
                    throw;
                }
            IL_0762:
                ex7 = ex11;
                goto IL_0857;
            IL_07d0:
                try
                {
                    try
                    {
                        jj_la1[30] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                    }
                    catch (System.Exception x10)
                    {
                        ex17 = x10;
                    }
                }
                catch
                {
                    //try-fault
                    if (num != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTBNFAssignment);
                    }
                    throw;
                }
                ex7 = ex17;
                goto IL_0857;
            IL_0196:
                ex12 = ex4;
                try
                {
                    System.Exception ex18 = ex12;
                    if (num5 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFAction);
                        num5 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex18 is RuntimeException)
                    {
                        throw (ex18);
                    }
                    if (ex18 is ParseException)
                    {
                        throw (ex18);
                    }
                    throw (ex18);
                }
                catch
                {
                    //try-fault
                    if (num5 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAction, b: true);
                        jjtreeCloseNodeScope(aSTBNFAction);
                    }
                    throw;
                }
            IL_0857:
                ex19 = ex7;
                try
                {
                    System.Exception ex20 = ex19;
                    if (num != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFAssignment);
                        num = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex20 is RuntimeException)
                    {
                        throw (ex20);
                    }
                    if (ex20 is ParseException)
                    {
                        throw (ex20);
                    }
                    throw (ex20);
                }
                catch
                {
                    //try-fault
                    if (num != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFAssignment, jjtree.NodeArity > 1);
                        jjtreeCloseNodeScope(aSTBNFAssignment);
                    }
                    throw;
                }
            IL_0465:
                ex = ex15;
                try
                {
                    System.Exception ex21 = ex;
                    if (num8 != 0)
                    {
                        jjtree.ClearNodeScope(aSTBNFTryBlock);
                        num8 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex21 is RuntimeException)
                    {
                        throw (ex21);
                    }
                    if (ex21 is ParseException)
                    {
                        throw (ex21);
                    }
                    throw (ex21);
                }
                catch
                {
                    //try-fault
                    if (num8 != 0)
                    {
                        jjtree.CloseNodeScope(aSTBNFTryBlock, b: true);
                        jjtreeCloseNodeScope(aSTBNFTryBlock);
                    }
                    throw;
                }
        }
        return (JJTreeNode)jjtree.PeekNode;
    }


    private bool notTailOfExpansionUnit()
    {
        Token token = getToken(1);
        if (token.Kind == 113 || token.Kind == 92 || token.Kind == 86 || token.Kind == 88 || token.Kind == 90)
        {
            return false;
        }
        return true;
    }


    public void Expression()
    {
        ConditionalExpression();
        if (jj_2_23(2))
        {
            AssignmentOperator();
            Expression();
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
        ASTPrimaryExpression aSTPrimaryExpression = new ASTPrimaryExpression(41);
        int num = 1;
        jjtree.OpenNodeScope(aSTPrimaryExpression);
        jjtreeOpenNodeScope(aSTPrimaryExpression);
        System.Exception ex;
        try
        {
            try
            {
                PrimaryPrefix();
                while (jj_2_31(2))
                {
                    PrimarySuffix();
                }
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_0059;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTPrimaryExpression, b: true);
                jjtreeCloseNodeScope(aSTPrimaryExpression);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTPrimaryExpression, b: true);
            jjtreeCloseNodeScope(aSTPrimaryExpression);
        }
        return;
    IL_0059:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTPrimaryExpression);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTPrimaryExpression, b: true);
                jjtreeCloseNodeScope(aSTPrimaryExpression);
            }
            throw;
        }
    }


    public void Arguments()
    {
        jj_consume_token(85);
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 96:
            case 97:
            case 106:
            case 107:
            case 108:
            case 109:
            case 137:
                ArgumentList();
                break;
            default:
                jj_la1[138] = jj_gen;
                break;
        }
        jj_consume_token(86);
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


    public void complex_regular_expression_choices()
    {
        ASTREChoice aSTREChoice = new ASTREChoice(30);
        int num = 1;
        jjtree.OpenNodeScope(aSTREChoice);
        jjtreeOpenNodeScope(aSTREChoice);
        System.Exception ex;
        try
        {
            try
            {
                complex_regular_expression();
                while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 113)
                {
                    jj_consume_token(113);
                    complex_regular_expression();
                }
                jj_la1[38] = jj_gen;
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_009b;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTREChoice, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTREChoice);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTREChoice, jjtree.NodeArity > 1);
            jjtreeCloseNodeScope(aSTREChoice);
        }
        return;
    IL_009b:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTREChoice);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTREChoice, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTREChoice);
            }
            throw;
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


    public void complex_regular_expression()
    {
        ASTRESequence aSTRESequence = new ASTRESequence(31);
        int num = 1;
        jjtree.OpenNodeScope(aSTRESequence);
        jjtreeOpenNodeScope(aSTRESequence);
        System.Exception ex;
        try
        {
            try
            {
                while (true)
                {
                    complex_regular_expression_unit();
                    switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                    {
                        case 84:
                        case 85:
                        case 89:
                        case 95:
                        case 97:
                            continue;
                    }
                    jj_la1[39] = jj_gen;
                    break;
                }
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_00c1;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRESequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTRESequence);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTRESequence, jjtree.NodeArity > 1);
            jjtreeCloseNodeScope(aSTRESequence);
        }
        return;
    IL_00c1:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTRESequence);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRESequence, jjtree.NodeArity > 1);
                jjtreeCloseNodeScope(aSTRESequence);
            }
            throw;
        }
    }


    public void complex_regular_expression_unit()
    {
        ASTREStringLiteral aSTREStringLiteral;
        int num6;
        System.Exception ex;
        ASTREReference aSTREReference;
        int num7;
        System.Exception ex2;
        System.Exception ex4;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 84:
                aSTREStringLiteral = new ASTREStringLiteral(26);
                num6 = 1;
                jjtree.OpenNodeScope(aSTREStringLiteral);
                jjtreeOpenNodeScope(aSTREStringLiteral);
                try
                {
                    try
                    {
                        StringLiteral();
                    }
                    catch (System.Exception x)
                    {
                        ex = x;
                        goto IL_00a7;
                    }
                }
                catch
                {
                    //try-fault
                    if (num6 != 0)
                    {
                        jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                        jjtreeCloseNodeScope(aSTREStringLiteral);
                    }
                    throw;
                }
                if (num6 != 0)
                {
                    jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                    jjtreeCloseNodeScope(aSTREStringLiteral);
                }
                break;
            case 95:
                aSTREReference = new ASTREReference(28);
                num7 = 1;
                jjtree.OpenNodeScope(aSTREReference);
                jjtreeOpenNodeScope(aSTREReference);
                try
                {
                    try
                    {
                        jj_consume_token(95);
                        identifier();
                        jj_consume_token(126);
                    }
                    catch (System.Exception x2)
                    {
                        ex2 = x2;
                        goto IL_01a6;
                    }
                }
                catch
                {
                    //try-fault
                    if (num7 != 0)
                    {
                        jjtree.CloseNodeScope(aSTREReference, b: true);
                        jjtreeCloseNodeScope(aSTREReference);
                    }
                    throw;
                }
                if (num7 != 0)
                {
                    jjtree.CloseNodeScope(aSTREReference, b: true);
                    jjtreeCloseNodeScope(aSTREReference);
                }
                break;
            case 89:
            case 97:
                character_list();
                break;
            case 85:
                {
                    Token firstToken = jj_consume_token(85);
                    complex_regular_expression_choices();
                    jj_consume_token(86);
                    switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                    {
                        case 108:
                            {
                                ASTREOneOrMore aSTREOneOrMore = new ASTREOneOrMore(32);
                                int num3 = 1;
                                jjtree.OpenNodeScope(aSTREOneOrMore);
                                jjtreeOpenNodeScope(aSTREOneOrMore);
                                try
                                {
                                    jj_consume_token(108);
                                }
                                catch
                                {
                                    //try-fault
                                    if (num3 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTREOneOrMore, 1);
                                        jjtreeCloseNodeScope(aSTREOneOrMore);
                                    }
                                    throw;
                                }
                                if (num3 != 0)
                                {
                                    jjtree.CloseNodeScope(aSTREOneOrMore, 1);
                                    jjtreeCloseNodeScope(aSTREOneOrMore);
                                }
                                break;
                            }
                        case 110:
                            {
                                ASTREZeroOrMore aSTREZeroOrMore = new ASTREZeroOrMore(33);
                                int num2 = 1;
                                jjtree.OpenNodeScope(aSTREZeroOrMore);
                                jjtreeOpenNodeScope(aSTREZeroOrMore);
                                try
                                {
                                    jj_consume_token(110);
                                }
                                catch
                                {
                                    //try-fault
                                    if (num2 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTREZeroOrMore, 1);
                                        jjtreeCloseNodeScope(aSTREZeroOrMore);
                                    }
                                    throw;
                                }
                                if (num2 != 0)
                                {
                                    jjtree.CloseNodeScope(aSTREZeroOrMore, 1);
                                    jjtreeCloseNodeScope(aSTREZeroOrMore);
                                }
                                break;
                            }
                        case 98:
                            {
                                ASTREZeroOrOne aSTREZeroOrOne = new ASTREZeroOrOne(34);
                                int num5 = 1;
                                jjtree.OpenNodeScope(aSTREZeroOrOne);
                                jjtreeOpenNodeScope(aSTREZeroOrOne);
                                try
                                {
                                    jj_consume_token(98);
                                }
                                catch
                                {
                                    //try-fault
                                    if (num5 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTREZeroOrOne, 1);
                                        jjtreeCloseNodeScope(aSTREZeroOrOne);
                                    }
                                    throw;
                                }
                                if (num5 != 0)
                                {
                                    jjtree.CloseNodeScope(aSTREZeroOrOne, 1);
                                    jjtreeCloseNodeScope(aSTREZeroOrOne);
                                }
                                break;
                            }
                        case 87:
                            {
                                jj_consume_token(87);
                                IntegerLiteral();
                                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                                {
                                    jj_consume_token(92);
                                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 74)
                                    {
                                        IntegerLiteral();
                                    }
                                    else
                                    {
                                        jj_la1[40] = jj_gen;
                                    }
                                }
                                else
                                {
                                    jj_la1[41] = jj_gen;
                                }
                                ASTRRepetitionRange aSTRRepetitionRange = new ASTRRepetitionRange(35);
                                int num4 = 1;
                                jjtree.OpenNodeScope(aSTRRepetitionRange);
                                jjtreeOpenNodeScope(aSTRRepetitionRange);
                                try
                                {
                                    jj_consume_token(88);
                                }
                                catch
                                {
                                    //try-fault
                                    if (num4 != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTRRepetitionRange, 1);
                                        jjtreeCloseNodeScope(aSTRRepetitionRange);
                                    }
                                    throw;
                                }
                                if (num4 != 0)
                                {
                                    jjtree.CloseNodeScope(aSTRRepetitionRange, 1);
                                    jjtreeCloseNodeScope(aSTRRepetitionRange);
                                }
                                break;
                            }
                        default:
                            {
                                jj_la1[42] = jj_gen;
                                ASTREParenthesized aSTREParenthesized = new ASTREParenthesized(36);
                                int num = 1;
                                jjtree.OpenNodeScope(aSTREParenthesized);
                                jjtreeOpenNodeScope(aSTREParenthesized);
                                try
                                {
                                    jjtree.CloseNodeScope(aSTREParenthesized, 1);
                                    num = 0;
                                    jjtreeCloseNodeScope(aSTREParenthesized);
                                }
                                catch
                                {
                                    //try-fault
                                    if (num != 0)
                                    {
                                        jjtree.CloseNodeScope(aSTREParenthesized, 1);
                                        jjtreeCloseNodeScope(aSTREParenthesized);
                                    }
                                    throw;
                                }
                                if (num != 0)
                                {
                                    jjtree.CloseNodeScope(aSTREParenthesized, 1);
                                    jjtreeCloseNodeScope(aSTREParenthesized);
                                }
                                break;
                            }
                    }
                ((JJTreeNode)jjtree.PeekNode).FirstToken = firstToken;
                    break;
                }
            default:
                {
                    jj_la1[43] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
                }
            IL_00a7:
                ex2 = ex;
                try
                {
                    System.Exception ex3 = ex2;
                    if (num6 != 0)
                    {
                        jjtree.ClearNodeScope(aSTREStringLiteral);
                        num6 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex3 is RuntimeException)
                    {
                        throw (ex3);
                    }
                    if (ex3 is ParseException)
                    {
                        throw (ex3);
                    }
                    throw (ex3);
                }
                catch
                {
                    //try-fault
                    if (num6 != 0)
                    {
                        jjtree.CloseNodeScope(aSTREStringLiteral, b: true);
                        jjtreeCloseNodeScope(aSTREStringLiteral);
                    }
                    throw;
                }
            IL_01a6:
                ex4 = ex2;
                try
                {
                    System.Exception ex5 = ex4;
                    if (num7 != 0)
                    {
                        jjtree.ClearNodeScope(aSTREReference);
                        num7 = 0;
                    }
                    else
                    {
                        jjtree.PopNode();
                    }
                    if (ex5 is RuntimeException)
                    {
                        throw (ex5);
                    }
                    if (ex5 is ParseException)
                    {
                        throw (ex5);
                    }
                    throw (ex5);
                }
                catch
                {
                    //try-fault
                    if (num7 != 0)
                    {
                        jjtree.CloseNodeScope(aSTREReference, b: true);
                        jjtreeCloseNodeScope(aSTREReference);
                    }
                    throw;
                }
        }
    }


    public void character_list()
    {
        ASTRECharList aSTRECharList = new ASTRECharList(37);
        int num = 1;
        jjtree.OpenNodeScope(aSTRECharList);
        jjtreeOpenNodeScope(aSTRECharList);
        System.Exception ex;
        try
        {
            try
            {
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 97)
                {
                    jj_consume_token(97);
                }
                else
                {
                    jj_la1[44] = jj_gen;
                }
                jj_consume_token(89);
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 84)
                {
                    character_descriptor();
                    while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
                    {
                        jj_consume_token(92);
                        character_descriptor();
                    }
                    jj_la1[45] = jj_gen;
                }
                else
                {
                    jj_la1[46] = jj_gen;
                }
                jj_consume_token(90);
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_010f;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRECharList, b: true);
                jjtreeCloseNodeScope(aSTRECharList);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTRECharList, b: true);
            jjtreeCloseNodeScope(aSTRECharList);
        }
        return;
    IL_010f:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTRECharList);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTRECharList, b: true);
                jjtreeCloseNodeScope(aSTRECharList);
            }
            throw;
        }
    }


    public void character_descriptor()
    {
        ASTCharDescriptor aSTCharDescriptor = new ASTCharDescriptor(38);
        int num = 1;
        jjtree.OpenNodeScope(aSTCharDescriptor);
        jjtreeOpenNodeScope(aSTCharDescriptor);
        System.Exception ex;
        try
        {
            try
            {
                StringLiteral();
                if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 109)
                {
                    jj_consume_token(109);
                    StringLiteral();
                }
                else
                {
                    jj_la1[47] = jj_gen;
                }
            }
            catch (System.Exception x)
            {
                ex = x;
                goto IL_008e;
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTCharDescriptor, b: true);
                jjtreeCloseNodeScope(aSTCharDescriptor);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTCharDescriptor, b: true);
            jjtreeCloseNodeScope(aSTCharDescriptor);
        }
        return;
    IL_008e:
        System.Exception ex2 = ex;
        try
        {
            System.Exception ex3 = ex2;
            if (num != 0)
            {
                jjtree.ClearNodeScope(aSTCharDescriptor);
                num = 0;
            }
            else
            {
                jjtree.PopNode();
            }
            if (ex3 is RuntimeException)
            {
                throw (ex3);
            }
            if (ex3 is ParseException)
            {
                throw (ex3);
            }
            throw (ex3);
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTCharDescriptor, b: true);
                jjtreeCloseNodeScope(aSTCharDescriptor);
            }
            throw;
        }
    }


    internal virtual void node_descriptor_expression()
    {
        ASTNodeDescriptorExpression aSTNodeDescriptorExpression = new ASTNodeDescriptorExpression(40);
        int num = 1;
        jjtree.OpenNodeScope(aSTNodeDescriptorExpression);
        jjtreeOpenNodeScope(aSTNodeDescriptorExpression);
        try
        {
            int num2 = 1;
            while (true)
            {
                Token token = getToken(1);
                if (token.Kind == 0)
                {

                    throw new ParseException();
                }
                if (token.Kind == 85)
                {
                    num2++;
                }
                if (token.Kind == 86)
                {
                    num2 += -1;
                    if (num2 == 0)
                    {
                        break;
                    }
                }
                getNextToken();
            }
        }
        catch
        {
            //try-fault
            if (num != 0)
            {
                jjtree.CloseNodeScope(aSTNodeDescriptorExpression, b: true);
                jjtreeCloseNodeScope(aSTNodeDescriptorExpression);
            }
            throw;
        }
        if (num != 0)
        {
            jjtree.CloseNodeScope(aSTNodeDescriptorExpression, b: true);
            jjtreeCloseNodeScope(aSTNodeDescriptorExpression);
        }
    }


    public Token getNextToken()
    {
        if (this.token.Next != null)
        {
            this.token = this.token.Next;
        }
        else
        {
            Token obj = this.token;
            Token nextToken = token_source.GetNextToken();
            Token token = obj;
            token.Next = nextToken;
            this.token = nextToken;
        }
        this.m_jj_ntk = -1;
        jj_gen++;
        return this.token;
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


    public void PackageDeclaration()
    {
        Modifiers();
        jj_consume_token(55);
        string packageName = Name();
        jj_consume_token(91);
        JJTreeGlobals.PackageName = packageName;
    }


    public void ImportDeclaration()
    {
        jj_consume_token(47);
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 61)
        {
            jj_consume_token(61);
        }
        else
        {
            jj_la1[54] = jj_gen;
        }
        Name();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 93)
        {
            jj_consume_token(93);
            jj_consume_token(110);
        }
        else
        {
            jj_la1[55] = jj_gen;
        }
        jj_consume_token(91);
    }


    public void TypeDeclaration()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 91:
                jj_consume_token(91);
                break;
            case 24:
            case 31:
            case 40:
            case 50:
            case 52:
            case 56:
            case 57:
            case 58:
            case 61:
            case 64:
            case 68:
            case 72:
            case 128:
            case 129:
            case 136:
                Modifiers();
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 31:
                    case 50:
                        ClassOrInterfaceDeclaration();
                        break;
                    case 129:
                        EnumDeclaration();
                        break;
                    case 136:
                        AnnotationTypeDeclaration();
                        break;
                    default:
                        jj_la1[57] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                break;
            default:
                jj_la1[58] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void Modifiers()
    {
        while (jj_2_7(2))
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 58:
                    jj_consume_token(58);
                    continue;
                case 61:
                    jj_consume_token(61);
                    continue;
                case 57:
                    jj_consume_token(57);
                    continue;
                case 56:
                    jj_consume_token(56);
                    continue;
                case 40:
                    jj_consume_token(40);
                    continue;
                case 24:
                    jj_consume_token(24);
                    continue;
                case 64:
                    jj_consume_token(64);
                    continue;
                case 52:
                    jj_consume_token(52);
                    continue;
                case 68:
                    jj_consume_token(68);
                    continue;
                case 72:
                    jj_consume_token(72);
                    continue;
                case 128:
                    jj_consume_token(128);
                    continue;
                case 136:
                    Annotation();
                    continue;
            }
            jj_la1[56] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
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


    public void Annotation()
    {
        if (jj_2_44(int.MaxValue))
        {
            NormalAnnotation();
            return;
        }
        if (jj_2_45(int.MaxValue))
        {
            SingleMemberAnnotation();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 136)
        {
            MarkerAnnotation();
            return;
        }
        jj_la1[169] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ClassOrInterfaceDeclaration()
    {
        Token token = null;
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 31:
                jj_consume_token(31);
                break;
            case 50:
                jj_consume_token(50);
                break;
            default:
                jj_la1[59] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        string @this = JavaIdentifier();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[60] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 38)
        {
            ExtendsList();
        }
        else
        {
            jj_la1[61] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 46)
        {
            token = getToken(1);
            ImplementsList();
        }
        else
        {
            jj_la1[62] = jj_gen;
        }
        if (string.Equals(@this, JJTreeGlobals.ParserName))
        {
            if (token != null)
            {
                JJTreeGlobals.ParserImplements = token;
            }
            else
            {
                JJTreeGlobals.ParserImplements = getToken(1);
            }
            JJTreeGlobals.ParserClassBodyStart = getToken(1);
        }
        ClassOrInterfaceBody();
    }


    public void EnumDeclaration()
    {
        jj_consume_token(129);
        JavaIdentifier();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 46)
        {
            ImplementsList();
        }
        else
        {
            jj_la1[65] = jj_gen;
        }
        EnumBody();
    }


    public void AnnotationTypeDeclaration()
    {
        jj_consume_token(136);
        jj_consume_token(50);
        JavaIdentifier();
        AnnotationTypeBody();
    }


    public string JavaIdentifier()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 137:
                jj_consume_token(137);
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
                jj_la1[51] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
        return token.Image;
    }


    public void TypeParameters()
    {
        jj_consume_token(95);
        TypeParameter();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            TypeParameter();
        }
        jj_la1[72] = jj_gen;
        jj_consume_token(126);
    }


    public void ExtendsList()
    {
        jj_consume_token(38);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            ClassOrInterfaceType();
        }
        jj_la1[63] = jj_gen;
    }


    public void ImplementsList()
    {
        jj_consume_token(46);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            ClassOrInterfaceType();
        }
        jj_la1[64] = jj_gen;
    }


    public void ClassOrInterfaceType()
    {
        JavaIdentifier();
        if (jj_2_19(2))
        {
            TypeArguments();
        }
        while (jj_2_20(2))
        {
            jj_consume_token(93);
            JavaIdentifier();
            if (jj_2_21(2))
            {
                TypeArguments();
            }
        }
    }


    public void EnumBody()
    {
        jj_consume_token(87);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 24 || num == 40 || num == 52 || num == 56 || num == 57 || num == 58 || num == 61 || num == 64 || num == 68 || num == 72 || num == 128 || num == 136 || num == 137)
        {
            EnumConstant();
            while (jj_2_8(2))
            {
                jj_consume_token(92);
                EnumConstant();
            }
        }
        else
        {
            jj_la1[66] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
        }
        else
        {
            jj_la1[67] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 91)
        {
            jj_consume_token(91);
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
                    case 24:
                    case 25:
                    case 27:
                    case 30:
                    case 31:
                    case 36:
                    case 40:
                    case 42:
                    case 49:
                    case 50:
                    case 51:
                    case 52:
                    case 56:
                    case 57:
                    case 58:
                    case 60:
                    case 61:
                    case 64:
                    case 68:
                    case 71:
                    case 72:
                    case 87:
                    case 91:
                    case 95:
                    case 128:
                    case 129:
                    case 136:
                    case 137:
                        goto IL_0404;
                }
                break;
            IL_0404:
                ClassOrInterfaceBodyDeclaration();
            }
            jj_la1[68] = jj_gen;
        }
        else
        {
            jj_la1[69] = jj_gen;
        }
        jj_consume_token(88);
    }


    public void EnumConstant()
    {
        Modifiers();
        JavaIdentifier();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 85)
        {
            Arguments();
        }
        else
        {
            jj_la1[70] = jj_gen;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 87)
        {
            ClassOrInterfaceBody();
        }
        else
        {
            jj_la1[71] = jj_gen;
        }
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


    public void ClassOrInterfaceBodyDeclaration()
    {
        if (jj_2_11(2))
        {
            Initializer();
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
            case 24:
            case 25:
            case 27:
            case 30:
            case 31:
            case 36:
            case 40:
            case 42:
            case 49:
            case 50:
            case 51:
            case 52:
            case 56:
            case 57:
            case 58:
            case 60:
            case 61:
            case 64:
            case 68:
            case 71:
            case 72:
            case 95:
            case 128:
            case 129:
            case 136:
            case 137:
                Modifiers();
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 31:
                    case 50:
                        ClassOrInterfaceDeclaration();
                        break;
                    case 129:
                        EnumDeclaration();
                        break;
                    default:
                        {
                            jj_la1[76] = jj_gen;
                            if (jj_2_9(int.MaxValue))
                            {
                                ConstructorDeclaration();
                                break;
                            }
                            if (jj_2_10(int.MaxValue))
                            {
                                FieldDeclaration();
                                break;
                            }
                            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                            if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 25 || num == 27 || num == 30 || num == 36 || num == 42 || num == 49 || num == 51 || num == 60 || num == 71 || num == 95 || num == 137)
                            {
                                MethodDeclaration();
                                break;
                            }
                            jj_la1[77] = jj_gen;
                            jj_consume_token(-1);

                            throw new ParseException();
                        }
                }
                break;
            case 91:
                jj_consume_token(91);
                break;
            default:
                jj_la1[78] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void TypeParameter()
    {
        JavaIdentifier();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 38)
        {
            TypeBound();
        }
        else
        {
            jj_la1[73] = jj_gen;
        }
    }


    public void TypeBound()
    {
        jj_consume_token(38);
        ClassOrInterfaceType();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 112)
        {
            jj_consume_token(112);
            ClassOrInterfaceType();
        }
        jj_la1[74] = jj_gen;
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


    public void Initializer()
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 61)
        {
            jj_consume_token(61);
        }
        else
        {
            jj_la1[96] = jj_gen;
        }
        Block();
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


    public void ConstructorDeclaration()
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[92] = jj_gen;
        }
        JavaIdentifier();
        FormalParameters();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 67)
        {
            jj_consume_token(67);
            NameList();
        }
        else
        {
            jj_la1[93] = jj_gen;
        }
        jj_consume_token(87);
        if (jj_2_13(int.MaxValue))
        {
            ExplicitConstructorInvocation();
        }
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[94] = jj_gen;
                    jj_consume_token(88);
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
                case 24:
                case 25:
                case 26:
                case 27:
                case 30:
                case 31:
                case 33:
                case 35:
                case 36:
                case 39:
                case 40:
                case 42:
                case 43:
                case 45:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 87:
                case 91:
                case 106:
                case 107:
                case 128:
                case 135:
                case 136:
                case 137:
                    break;
            }
            BlockStatement();
        }
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


    public void FieldDeclaration()
    {
        Type();
        VariableDeclarator();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            VariableDeclarator();
        }
        jj_la1[79] = jj_gen;
        jj_consume_token(91);
    }


    public void MethodDeclaration()
    {
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
        {
            TypeParameters();
        }
        else
        {
            jj_la1[85] = jj_gen;
        }
        ResultType();
        MethodDeclarator();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 67)
        {
            jj_consume_token(67);
            NameList();
        }
        else
        {
            jj_la1[86] = jj_gen;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 87:
                Block();
                return;
            case 91:
                jj_consume_token(91);
                return;
        }
        jj_la1[87] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void Type()
    {
        if (jj_2_16(2))
        {
            ReferenceType();
            return;
        }
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 25 || num == 27 || num == 30 || num == 36 || num == 42 || num == 49 || num == 51 || num == 60)
        {
            PrimitiveType();
            return;
        }
        jj_la1[97] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void VariableDeclarator()
    {
        VariableDeclaratorId();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 94)
        {
            jj_consume_token(94);
            VariableInitializer();
        }
        else
        {
            jj_la1[80] = jj_gen;
        }
    }


    public void VariableDeclaratorId()
    {
        JavaIdentifier();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 89)
        {
            jj_consume_token(89);
            jj_consume_token(90);
        }
        jj_la1[81] = jj_gen;
    }


    public void VariableInitializer()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 87:
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 96:
            case 97:
            case 106:
            case 107:
            case 108:
            case 109:
            case 137:
                Expression();
                return;
        }
        jj_la1[82] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ArrayInitializer()
    {
        jj_consume_token(87);
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 87:
            case 96:
            case 97:
            case 106:
            case 107:
            case 108:
            case 109:
            case 137:
                VariableInitializer();
                while (jj_2_12(2))
                {
                    jj_consume_token(92);
                    VariableInitializer();
                }
                break;
            default:
                jj_la1[83] = jj_gen;
                break;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
        }
        else
        {
            jj_la1[84] = jj_gen;
        }
        jj_consume_token(88);
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


    public void MethodDeclarator()
    {
        JavaIdentifier();
        FormalParameters();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 89)
        {
            jj_consume_token(89);
            jj_consume_token(90);
        }
        jj_la1[88] = jj_gen;
    }


    public void NameList()
    {
        Name();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            Name();
        }
        jj_la1[105] = jj_gen;
    }


    public void FormalParameter()
    {
        Modifiers();
        Type();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 130)
        {
            jj_consume_token(130);
        }
        else
        {
            jj_la1[91] = jj_gen;
        }
        VariableDeclaratorId();
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


    public void ExplicitConstructorInvocation()
    {
        if (jj_2_15(int.MaxValue))
        {
            jj_consume_token(65);
            Arguments();
            jj_consume_token(91);
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 137:
                if (jj_2_14(2))
                {
                    PrimaryExpression();
                    jj_consume_token(93);
                }
                jj_consume_token(62);
                Arguments();
                jj_consume_token(91);
                break;
            default:
                jj_la1[95] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
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


    public void ReferenceType()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 25:
            case 27:
            case 30:
            case 36:
            case 42:
            case 49:
            case 51:
            case 60:
                PrimitiveType();
                do
                {
                    jj_consume_token(89);
                    jj_consume_token(90);
                }
                while (jj_2_17(2));
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
            case 137:
                ClassOrInterfaceType();
                while (jj_2_18(2))
                {
                    jj_consume_token(89);
                    jj_consume_token(90);
                }
                break;
            default:
                jj_la1[98] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void PrimitiveType()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 25:
                jj_consume_token(25);
                return;
            case 30:
                jj_consume_token(30);
                return;
            case 27:
                jj_consume_token(27);
                return;
            case 60:
                jj_consume_token(60);
                return;
            case 49:
                jj_consume_token(49);
                return;
            case 51:
                jj_consume_token(51);
                return;
            case 42:
                jj_consume_token(42);
                return;
            case 36:
                jj_consume_token(36);
                return;
        }
        jj_la1[103] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
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


    public void TypeArguments()
    {
        jj_consume_token(95);
        TypeArgument();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            TypeArgument();
        }
        jj_la1[99] = jj_gen;
        jj_consume_token(126);
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


    public void TypeArgument()
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 42:
            case 49:
            case 51:
            case 60:
            case 137:
                ReferenceType();
                break;
            case 98:
                {
                    jj_consume_token(98);
                    int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
                    if (num == 38 || num == 62)
                    {
                        WildcardBounds();
                    }
                    else
                    {
                        jj_la1[100] = jj_gen;
                    }
                    break;
                }
            default:
                jj_la1[101] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void WildcardBounds()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 38:
                jj_consume_token(38);
                ReferenceType();
                break;
            case 62:
                jj_consume_token(62);
                ReferenceType();
                break;
            default:
                jj_la1[102] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
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


    public void ConditionalExpression()
    {
        ConditionalOrExpression();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 98)
        {
            jj_consume_token(98);
            Expression();
            jj_consume_token(99);
            Expression();
        }
        else
        {
            jj_la1[107] = jj_gen;
        }
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


    public void AssignmentOperator()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 94:
                jj_consume_token(94);
                return;
            case 118:
                jj_consume_token(118);
                return;
            case 119:
                jj_consume_token(119);
                return;
            case 123:
                jj_consume_token(123);
                return;
            case 116:
                jj_consume_token(116);
                return;
            case 117:
                jj_consume_token(117);
                return;
            case 131:
                jj_consume_token(131);
                return;
            case 132:
                jj_consume_token(132);
                return;
            case 133:
                jj_consume_token(133);
                return;
            case 120:
                jj_consume_token(120);
                return;
            case 122:
                jj_consume_token(122);
                return;
            case 121:
                jj_consume_token(121);
                return;
        }
        jj_la1[106] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ConditionalOrExpression()
    {
        ConditionalAndExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 104)
        {
            jj_consume_token(104);
            ConditionalAndExpression();
        }
        jj_la1[108] = jj_gen;
    }


    public void ConditionalAndExpression()
    {
        InclusiveOrExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 105)
        {
            jj_consume_token(105);
            InclusiveOrExpression();
        }
        jj_la1[109] = jj_gen;
    }


    public void InclusiveOrExpression()
    {
        ExclusiveOrExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 113)
        {
            jj_consume_token(113);
            ExclusiveOrExpression();
        }
        jj_la1[110] = jj_gen;
    }


    public void ExclusiveOrExpression()
    {
        AndExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 114)
        {
            jj_consume_token(114);
            AndExpression();
        }
        jj_la1[111] = jj_gen;
    }


    public void AndExpression()
    {
        EqualityExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 112)
        {
            jj_consume_token(112);
            EqualityExpression();
        }
        jj_la1[112] = jj_gen;
    }


    public void EqualityExpression()
    {
        InstanceOfExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 100 && num != 103)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 100:
                    jj_consume_token(100);
                    break;
                case 103:
                    jj_consume_token(103);
                    break;
                default:
                    jj_la1[114] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            InstanceOfExpression();
        }
        jj_la1[113] = jj_gen;
    }


    public void InstanceOfExpression()
    {
        RelationalExpression();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 48)
        {
            jj_consume_token(48);
            Type();
        }
        else
        {
            jj_la1[115] = jj_gen;
        }
    }


    public void RelationalExpression()
    {
        ShiftExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 95 && num != 101 && num != 102 && num != 126)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 95:
                    jj_consume_token(95);
                    break;
                case 126:
                    jj_consume_token(126);
                    break;
                case 101:
                    jj_consume_token(101);
                    break;
                case 102:
                    jj_consume_token(102);
                    break;
                default:
                    jj_la1[117] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            ShiftExpression();
        }
        jj_la1[116] = jj_gen;
    }


    public void ShiftExpression()
    {
        AdditiveExpression();
        while (jj_2_24(1))
        {
            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 134)
            {
                jj_consume_token(134);
            }
            else
            {
                jj_la1[118] = jj_gen;
                if (jj_2_25(1))
                {
                    RSIGNEDSHIFT();
                }
                else
                {
                    if (!jj_2_26(1))
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
            if (num != 108 && num != 109)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 108:
                    jj_consume_token(108);
                    break;
                case 109:
                    jj_consume_token(109);
                    break;
                default:
                    jj_la1[120] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            MultiplicativeExpression();
        }
        jj_la1[119] = jj_gen;
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


    public void RSIGNEDSHIFT()
    {
        if (getToken(1).Kind != 126 || ((Token.GTToken)getToken(1)).RealKind != 125)
        {
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_consume_token(126);
        jj_consume_token(126);
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


    public void RUNSIGNEDSHIFT()
    {
        if (getToken(1).Kind != 126 || ((Token.GTToken)getToken(1)).RealKind != 124)
        {
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_consume_token(126);
        jj_consume_token(126);
        jj_consume_token(126);
    }


    public void MultiplicativeExpression()
    {
        UnaryExpression();
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 110 && num != 111 && num != 115)
            {
                break;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 110:
                    jj_consume_token(110);
                    break;
                case 111:
                    jj_consume_token(111);
                    break;
                case 115:
                    jj_consume_token(115);
                    break;
                default:
                    jj_la1[122] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            UnaryExpression();
        }
        jj_la1[121] = jj_gen;
    }


    public void UnaryExpression()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 108:
            case 109:
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 108:
                        jj_consume_token(108);
                        break;
                    case 109:
                        jj_consume_token(109);
                        break;
                    default:
                        jj_la1[123] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                UnaryExpression();
                break;
            case 106:
                PreIncrementExpression();
                break;
            case 107:
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 96:
            case 97:
            case 137:
                UnaryExpressionNotPlusMinus();
                break;
            default:
                jj_la1[124] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void PreIncrementExpression()
    {
        jj_consume_token(106);
        PrimaryExpression();
    }


    public void PreDecrementExpression()
    {
        jj_consume_token(107);
        PrimaryExpression();
    }


    public void UnaryExpressionNotPlusMinus()
    {
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 96 || num == 97)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 97:
                    jj_consume_token(97);
                    break;
                case 96:
                    jj_consume_token(96);
                    break;
                default:
                    jj_la1[125] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            UnaryExpression();
            return;
        }
        jj_la1[126] = jj_gen;
        if (jj_2_27(int.MaxValue))
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 137:
                PostfixExpression();
                return;
        }
        jj_la1[127] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
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


    public void CastExpression()
    {
        if (jj_2_30(int.MaxValue))
        {
            jj_consume_token(85);
            Type();
            jj_consume_token(86);
            UnaryExpression();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 85)
        {
            jj_consume_token(85);
            Type();
            jj_consume_token(86);
            UnaryExpressionNotPlusMinus();
            return;
        }
        jj_la1[132] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void PostfixExpression()
    {
        PrimaryExpression();
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 106 || num == 107)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 106:
                    jj_consume_token(106);
                    return;
                case 107:
                    jj_consume_token(107);
                    return;
            }
            jj_la1[130] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_la1[131] = jj_gen;
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


    public void Literal()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 74:
                jj_consume_token(74);
                return;
            case 78:
                jj_consume_token(78);
                return;
            case 83:
                jj_consume_token(83);
                return;
            case 84:
                jj_consume_token(84);
                return;
            case 39:
            case 69:
                BooleanLiteral();
                return;
            case 54:
                NullLiteral();
                return;
        }
        jj_la1[136] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
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


    public void PrimaryPrefix()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 39:
            case 54:
            case 69:
            case 74:
            case 78:
            case 83:
            case 84:
                Literal();
                return;
            case 65:
                jj_consume_token(65);
                return;
            case 62:
                jj_consume_token(62);
                jj_consume_token(93);
                JavaIdentifier();
                return;
            case 85:
                jj_consume_token(85);
                Expression();
                jj_consume_token(86);
                return;
            case 53:
                AllocationExpression();
                return;
        }
        jj_la1[133] = jj_gen;
        if (jj_2_32(int.MaxValue))
        {
            ResultType();
            jj_consume_token(93);
            jj_consume_token(31);
            return;
        }
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 137)
        {
            Name();
            return;
        }
        jj_la1[134] = jj_gen;
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


    public void PrimarySuffix()
    {
        if (jj_2_33(2))
        {
            jj_consume_token(93);
            jj_consume_token(65);
            return;
        }
        if (jj_2_34(2))
        {
            jj_consume_token(93);
            AllocationExpression();
            return;
        }
        if (jj_2_35(3))
        {
            MemberSelector();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 89:
                jj_consume_token(89);
                Expression();
                jj_consume_token(90);
                break;
            case 93:
                jj_consume_token(93);
                JavaIdentifier();
                break;
            case 85:
                Arguments();
                break;
            default:
                jj_la1[135] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void AllocationExpression()
    {
        if (jj_2_36(2))
        {
            jj_consume_token(53);
            PrimitiveType();
            ArrayDimsAndInits();
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 53)
        {
            jj_consume_token(53);
            ClassOrInterfaceType();
            if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 95)
            {
                TypeArguments();
            }
            else
            {
                jj_la1[140] = jj_gen;
            }
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 89:
                    ArrayDimsAndInits();
                    break;
                case 85:
                    Arguments();
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 87)
                    {
                        ClassOrInterfaceBody();
                    }
                    else
                    {
                        jj_la1[141] = jj_gen;
                    }
                    break;
                default:
                    jj_la1[142] = jj_gen;
                    jj_consume_token(-1);

                    throw new ParseException();
            }
            return;
        }
        jj_la1[143] = jj_gen;
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


    public void MemberSelector()
    {
        jj_consume_token(93);
        TypeArguments();
        JavaIdentifier();
    }


    public void NullLiteral()
    {
        jj_consume_token(54);
    }


    public void ArgumentList()
    {
        Expression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            Expression();
        }
        jj_la1[139] = jj_gen;
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


    public void ArrayDimsAndInits()
    {
        if (jj_2_39(2))
        {
            do
            {
                jj_consume_token(89);
                Expression();
                jj_consume_token(90);
            }
            while (jj_2_37(2));
            while (jj_2_38(2))
            {
                jj_consume_token(89);
                jj_consume_token(90);
            }
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 89)
        {
            do
            {
                jj_consume_token(89);
                jj_consume_token(90);
            }
            while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 89);
            jj_la1[144] = jj_gen;
            ArrayInitializer();
            return;
        }
        jj_la1[145] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
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


    public void LabeledStatement()
    {
        JavaIdentifier();
        jj_consume_token(99);
        Statement();
    }


    public void AssertStatement()
    {
        jj_consume_token(135);
        Expression();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 99)
        {
            jj_consume_token(99);
            Expression();
        }
        else
        {
            jj_la1[147] = jj_gen;
        }
        jj_consume_token(91);
    }


    public void EmptyStatement()
    {
        jj_consume_token(91);
    }


    public void StatementExpression()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 106:
                PreIncrementExpression();
                break;
            case 107:
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 137:
                PrimaryExpression();
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 94:
                    case 106:
                    case 107:
                    case 116:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121:
                    case 122:
                    case 123:
                    case 131:
                    case 132:
                    case 133:
                        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                        {
                            case 106:
                                jj_consume_token(106);
                                break;
                            case 107:
                                jj_consume_token(107);
                                break;
                            case 94:
                            case 116:
                            case 117:
                            case 118:
                            case 119:
                            case 120:
                            case 121:
                            case 122:
                            case 123:
                            case 131:
                            case 132:
                            case 133:
                                AssignmentOperator();
                                Expression();
                                break;
                            default:
                                jj_la1[151] = jj_gen;
                                jj_consume_token(-1);

                                throw new ParseException();
                        }
                        break;
                    default:
                        jj_la1[152] = jj_gen;
                        break;
                }
                break;
            default:
                jj_la1[153] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
    }


    public void SwitchStatement()
    {
        jj_consume_token(63);
        jj_consume_token(85);
        Expression();
        jj_consume_token(86);
        jj_consume_token(87);
        while (true)
        {
            int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
            if (num != 28 && num != 34)
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
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 30:
                    case 31:
                    case 33:
                    case 35:
                    case 36:
                    case 39:
                    case 40:
                    case 42:
                    case 43:
                    case 45:
                    case 49:
                    case 50:
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 68:
                    case 69:
                    case 70:
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                    case 78:
                    case 83:
                    case 84:
                    case 85:
                    case 87:
                    case 91:
                    case 106:
                    case 107:
                    case 128:
                    case 135:
                    case 136:
                    case 137:
                        goto IL_02d0;
                }
                break;
            IL_02d0:
                BlockStatement();
            }
            jj_la1[155] = jj_gen;
        }
        jj_la1[154] = jj_gen;
        jj_consume_token(88);
    }


    public void IfStatement()
    {
        jj_consume_token(45);
        jj_consume_token(85);
        Expression();
        jj_consume_token(86);
        Statement();
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 37)
        {
            jj_consume_token(37);
            Statement();
        }
        else
        {
            jj_la1[157] = jj_gen;
        }
    }


    public void WhileStatement()
    {
        jj_consume_token(73);
        jj_consume_token(85);
        Expression();
        jj_consume_token(86);
        Statement();
    }


    public void DoStatement()
    {
        jj_consume_token(35);
        Statement();
        jj_consume_token(73);
        jj_consume_token(85);
        Expression();
        jj_consume_token(86);
        jj_consume_token(91);
    }


    public void ForStatement()
    {
        jj_consume_token(43);
        jj_consume_token(85);
        if (jj_2_42(int.MaxValue))
        {
            Modifiers();
            Type();
            JavaIdentifier();
            jj_consume_token(99);
            Expression();
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
                case 24:
                case 25:
                case 27:
                case 30:
                case 36:
                case 39:
                case 40:
                case 42:
                case 49:
                case 51:
                case 52:
                case 53:
                case 54:
                case 56:
                case 57:
                case 58:
                case 60:
                case 61:
                case 62:
                case 64:
                case 65:
                case 68:
                case 69:
                case 71:
                case 72:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 91:
                case 106:
                case 107:
                case 128:
                case 136:
                case 137:
                    break;
                default:
                    jj_la1[161] = jj_gen;
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
                case 24:
                case 25:
                case 27:
                case 30:
                case 36:
                case 39:
                case 40:
                case 42:
                case 49:
                case 51:
                case 52:
                case 53:
                case 54:
                case 56:
                case 57:
                case 58:
                case 60:
                case 61:
                case 62:
                case 64:
                case 65:
                case 68:
                case 69:
                case 71:
                case 72:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 106:
                case 107:
                case 128:
                case 136:
                case 137:
                    ForInit();
                    break;
                default:
                    jj_la1[158] = jj_gen;
                    break;
            }
            jj_consume_token(91);
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
                case 25:
                case 27:
                case 30:
                case 36:
                case 39:
                case 42:
                case 49:
                case 51:
                case 53:
                case 54:
                case 60:
                case 62:
                case 65:
                case 69:
                case 71:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 96:
                case 97:
                case 106:
                case 107:
                case 108:
                case 109:
                case 137:
                    Expression();
                    break;
                default:
                    jj_la1[159] = jj_gen;
                    break;
            }
            jj_consume_token(91);
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
                case 25:
                case 27:
                case 30:
                case 36:
                case 39:
                case 42:
                case 49:
                case 51:
                case 53:
                case 54:
                case 60:
                case 62:
                case 65:
                case 69:
                case 71:
                case 74:
                case 78:
                case 83:
                case 84:
                case 85:
                case 106:
                case 107:
                case 137:
                    ForUpdate();
                    break;
                default:
                    jj_la1[160] = jj_gen;
                    break;
            }
        }
        jj_consume_token(86);
        Statement();
    }


    public void BreakStatement()
    {
        jj_consume_token(26);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 137)
        {
            JavaIdentifier();
        }
        else
        {
            jj_la1[164] = jj_gen;
        }
        jj_consume_token(91);
    }


    public void ContinueStatement()
    {
        jj_consume_token(33);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 137)
        {
            JavaIdentifier();
        }
        else
        {
            jj_la1[165] = jj_gen;
        }
        jj_consume_token(91);
    }


    public void ReturnStatement()
    {
        jj_consume_token(59);
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 96:
            case 97:
            case 106:
            case 107:
            case 108:
            case 109:
            case 137:
                Expression();
                break;
            default:
                jj_la1[166] = jj_gen;
                break;
        }
        jj_consume_token(91);
    }


    public void ThrowStatement()
    {
        jj_consume_token(66);
        Expression();
        jj_consume_token(91);
    }


    public void SynchronizedStatement()
    {
        jj_consume_token(64);
        jj_consume_token(85);
        Expression();
        jj_consume_token(86);
        Block();
    }


    public void TryStatement()
    {
        jj_consume_token(70);
        Block();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 29)
        {
            jj_consume_token(29);
            jj_consume_token(85);
            FormalParameter();
            jj_consume_token(86);
            Block();
        }
        jj_la1[167] = jj_gen;
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 41)
        {
            jj_consume_token(41);
            Block();
        }
        else
        {
            jj_la1[168] = jj_gen;
        }
    }


    public void Statement()
    {
        if (jj_2_40(2))
        {
            LabeledStatement();
            return;
        }
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 135:
                AssertStatement();
                break;
            case 87:
                Block();
                break;
            case 91:
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 106:
            case 107:
            case 137:
                StatementExpression();
                jj_consume_token(91);
                break;
            case 63:
                SwitchStatement();
                break;
            case 45:
                IfStatement();
                break;
            case 73:
                WhileStatement();
                break;
            case 35:
                DoStatement();
                break;
            case 43:
                ForStatement();
                break;
            case 26:
                BreakStatement();
                break;
            case 33:
                ContinueStatement();
                break;
            case 59:
                ReturnStatement();
                break;
            case 66:
                ThrowStatement();
                break;
            case 64:
                SynchronizedStatement();
                break;
            case 70:
                TryStatement();
                break;
            default:
                jj_la1[146] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
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


    public void LocalVariableDeclaration()
    {
        Modifiers();
        Type();
        VariableDeclarator();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            VariableDeclarator();
        }
        jj_la1[150] = jj_gen;
    }


    public void SwitchLabel()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 28:
                jj_consume_token(28);
                Expression();
                jj_consume_token(99);
                break;
            case 34:
                jj_consume_token(34);
                jj_consume_token(99);
                break;
            default:
                jj_la1[156] = jj_gen;
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


    public void ForInit()
    {
        if (jj_2_43(int.MaxValue))
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 106:
            case 107:
            case 137:
                StatementExpressionList();
                return;
        }
        jj_la1[162] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void ForUpdate()
    {
        StatementExpressionList();
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


    public void StatementExpressionList()
    {
        StatementExpression();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            StatementExpression();
        }
        jj_la1[163] = jj_gen;
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


    public void NormalAnnotation()
    {
        jj_consume_token(136);
        Name();
        jj_consume_token(85);
        int num = ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk());
        if (num == 1 || num == 2 || num == 3 || num == 4 || num == 5 || num == 6 || num == 7 || num == 8 || num == 9 || num == 10 || num == 11 || num == 137)
        {
            MemberValuePairs();
        }
        else
        {
            jj_la1[170] = jj_gen;
        }
        jj_consume_token(86);
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


    public void SingleMemberAnnotation()
    {
        jj_consume_token(136);
        Name();
        jj_consume_token(85);
        MemberValue();
        jj_consume_token(86);
    }


    public void MarkerAnnotation()
    {
        jj_consume_token(136);
        Name();
    }


    public void MemberValuePairs()
    {
        MemberValuePair();
        while (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
            MemberValuePair();
        }
        jj_la1[171] = jj_gen;
    }


    public void MemberValue()
    {
        switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
        {
            case 136:
                Annotation();
                return;
            case 87:
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
            case 25:
            case 27:
            case 30:
            case 36:
            case 39:
            case 42:
            case 49:
            case 51:
            case 53:
            case 54:
            case 60:
            case 62:
            case 65:
            case 69:
            case 71:
            case 74:
            case 78:
            case 83:
            case 84:
            case 85:
            case 96:
            case 97:
            case 106:
            case 107:
            case 108:
            case 109:
            case 137:
                ConditionalExpression();
                return;
        }
        jj_la1[172] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public void MemberValuePair()
    {
        JavaIdentifier();
        jj_consume_token(94);
        MemberValue();
    }


    public void MemberValueArrayInitializer()
    {
        jj_consume_token(87);
        MemberValue();
        while (jj_2_46(2))
        {
            jj_consume_token(92);
            MemberValue();
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 92)
        {
            jj_consume_token(92);
        }
        else
        {
            jj_la1[173] = jj_gen;
        }
        jj_consume_token(88);
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
        //
        try
        {
            return true;
        }
        finally
        {
            jj_save(45, P_0);
        }
    }


    public void AnnotationTypeBody()
    {
        jj_consume_token(87);
        while (true)
        {
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                default:
                    jj_la1[174] = jj_gen;
                    jj_consume_token(88);
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
                case 24:
                case 25:
                case 27:
                case 30:
                case 31:
                case 36:
                case 40:
                case 42:
                case 49:
                case 50:
                case 51:
                case 52:
                case 56:
                case 57:
                case 58:
                case 60:
                case 61:
                case 64:
                case 68:
                case 72:
                case 91:
                case 128:
                case 129:
                case 136:
                case 137:
                    break;
            }
            AnnotationTypeMemberDeclaration();
        }
    }


    public void AnnotationTypeMemberDeclaration()
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
            case 24:
            case 25:
            case 27:
            case 30:
            case 31:
            case 36:
            case 40:
            case 42:
            case 49:
            case 50:
            case 51:
            case 52:
            case 56:
            case 57:
            case 58:
            case 60:
            case 61:
            case 64:
            case 68:
            case 72:
            case 128:
            case 129:
            case 136:
            case 137:
                Modifiers();
                if (jj_2_47(int.MaxValue))
                {
                    Type();
                    JavaIdentifier();
                    jj_consume_token(85);
                    jj_consume_token(86);
                    if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 34)
                    {
                        DefaultValue();
                    }
                    else
                    {
                        jj_la1[175] = jj_gen;
                    }
                    jj_consume_token(91);
                    break;
                }
                switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
                {
                    case 31:
                    case 50:
                        ClassOrInterfaceDeclaration();
                        break;
                    case 129:
                        EnumDeclaration();
                        break;
                    case 136:
                        AnnotationTypeDeclaration();
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
                    case 25:
                    case 27:
                    case 30:
                    case 36:
                    case 42:
                    case 49:
                    case 51:
                    case 60:
                    case 137:
                        FieldDeclaration();
                        break;
                    default:
                        jj_la1[176] = jj_gen;
                        jj_consume_token(-1);

                        throw new ParseException();
                }
                break;
            case 91:
                jj_consume_token(91);
                break;
            default:
                jj_la1[177] = jj_gen;
                jj_consume_token(-1);

                throw new ParseException();
        }
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
        //
        try
        {
            return true;
        }
        finally
        {
            jj_save(46, P_0);
        }
    }


    public void DefaultValue()
    {
        jj_consume_token(34);
        MemberValue();
    }


    private bool jj_3_1()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_scan_token(110))
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
        if (jj_3R_61())
        {
            return true;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_3()
    {
        Token token = jj_scanpos;
        if (jj_3R_62())
        {
            jj_scanpos = token;
            if (jj_3R_63())
            {
                jj_scanpos = token;
                if (jj_scan_token(95))
                {
                    jj_scanpos = token;
                    if (jj_3R_64())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3_4()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_65())
        {
            jj_scanpos = token;
        }
        if (jj_3R_66())
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_5()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_67())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_6()
    {
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_68());
        jj_scanpos = token;
        if (jj_scan_token(55))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_7()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(58))
        {
            jj_scanpos = token;
            if (jj_scan_token(61))
            {
                jj_scanpos = token;
                if (jj_scan_token(57))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(56))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(40))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(24))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(64))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(52))
                                    {
                                        jj_scanpos = token;
                                        if (jj_scan_token(68))
                                        {
                                            jj_scanpos = token;
                                            if (jj_scan_token(72))
                                            {
                                                jj_scanpos = token;
                                                if (jj_scan_token(128))
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_3R_69())
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


    private bool jj_3_8()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_70())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_9()
    {
        Token token = jj_scanpos;
        if (jj_3R_71())
        {
            jj_scanpos = token;
        }
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_10()
    {
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_74());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_scan_token(92))
        {
            jj_scanpos = token;
            if (jj_scan_token(94))
            {
                jj_scanpos = token;
                if (jj_scan_token(91))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3_11()
    {
        if (jj_3R_75())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_12()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_76())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_13()
    {
        if (jj_3R_77())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_14()
    {
        if (jj_3R_61())
        {
            return true;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_15()
    {
        if (jj_scan_token(65))
        {
            return true;
        }
        if (jj_3R_78())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_16()
    {
        if (jj_3R_79())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_17()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_18()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_19()
    {
        if (jj_3R_80())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_20()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_21())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3_21()
    {
        if (jj_3R_80())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_22()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_23()
    {
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_24()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(134))
        {
            jj_scanpos = token;
            if (jj_3_25())
            {
                jj_scanpos = token;
                if (jj_3_26())
                {
                    return true;
                }
            }
        }
        if (jj_3R_247())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_25()
    {
        if (jj_3R_83())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_26()
    {
        if (jj_3R_84())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_27()
    {
        if (jj_3R_85())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_28()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_29()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_scan_token(89))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_30()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_31()
    {
        if (jj_3R_87())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_32()
    {
        if (jj_3R_88())
        {
            return true;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_scan_token(31))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_33()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_scan_token(65))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_34()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_89())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_35()
    {
        if (jj_3R_90())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_36()
    {
        if (jj_scan_token(53))
        {
            return true;
        }
        if (jj_3R_86())
        {
            return true;
        }
        if (jj_3R_169())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_37()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_38()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_39()
    {
        if (jj_3_37())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_37());
        jj_scanpos = token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_38());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3_40()
    {
        if (jj_3R_91())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_41()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_42()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_43()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_44()
    {
        if (jj_scan_token(136))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_94())
        {
            jj_scanpos = token;
            if (jj_scan_token(86))
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3_45()
    {
        if (jj_scan_token(136))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        return false;
    }


    private bool jj_3_46()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        return false;
    }


    private bool jj_3_47()
    {
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_90()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_80())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_148()
    {
        Token token = jj_scanpos;
        if (jj_3R_157())
        {
            jj_scanpos = token;
            if (jj_3R_158())
            {
                jj_scanpos = token;
                if (jj_3R_159())
                {
                    jj_scanpos = token;
                    if (jj_3R_160())
                    {
                        return true;
                    }
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
            if (jj_scanpos.Next == null)
            {
                Token obj = jj_scanpos;
                Token nextToken = token_source.GetNextToken();
                Token token = obj;
                Token obj2 = nextToken;
                token.Next = nextToken;
                nextToken = obj2;
                Token obj3 = nextToken;
                jj_scanpos = nextToken;
                jj_lastpos = obj3;
            }
            else
            {
                Token nextToken = jj_scanpos.Next;
                Token obj4 = nextToken;
                jj_scanpos = nextToken;
                jj_lastpos = obj4;
            }
        }
        else
        {
            jj_scanpos = jj_scanpos.Next;
        }
        if (jj_rescan)
        {
            int num = 0;
            Token next = this.token;
            while (next != null && next != jj_scanpos)
            {
                num++;
                next = next.Next;
            }
            if (next != null)
            {
                jj_add_error_token(P_0, num);
            }
        }
        if (jj_scanpos.Kind != P_0)
        {
            return true;
        }
        if (jj_la == 0 && jj_scanpos == jj_lastpos)
        {
            throw (jj_ls);
        }
        return false;
    }


    private bool jj_3R_89()
    {
        Token token = jj_scanpos;
        if (jj_3_36())
        {
            jj_scanpos = token;
            if (jj_3R_121())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_131()
    {
        if (jj_3R_148())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_88()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(71))
        {
            jj_scanpos = token;
            if (jj_3R_120())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_117()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_118()
    {
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_119()
    {
        if (jj_3R_78())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_98()
    {
        if (jj_3R_131())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_131());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_99()
    {
        if (jj_scan_token(113))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_93()
    {
        if (jj_3R_72())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_22());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_67()
    {
        if (jj_scan_token(137))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_82()
    {
        if (jj_3R_112())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_23())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_87()
    {
        Token token = jj_scanpos;
        if (jj_3_33())
        {
            jj_scanpos = token;
            if (jj_3_34())
            {
                jj_scanpos = token;
                if (jj_3_35())
                {
                    jj_scanpos = token;
                    if (jj_3R_117())
                    {
                        jj_scanpos = token;
                        if (jj_3R_118())
                        {
                            jj_scanpos = token;
                            if (jj_3R_119())
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


    private bool jj_3R_61()
    {
        if (jj_3R_96())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_31());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_72()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(137))
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


    private bool jj_3R_147()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(74))
        {
            jj_scanpos = token;
            if (jj_scan_token(78))
            {
                jj_scanpos = token;
                if (jj_scan_token(83))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(84))
                    {
                        jj_scanpos = token;
                        if (jj_3R_156())
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(54))
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


    private bool jj_3R_125()
    {
        if (jj_3R_147())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_126()
    {
        if (jj_scan_token(62))
        {
            return true;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_127()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_128()
    {
        if (jj_3R_89())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_129()
    {
        if (jj_3R_88())
        {
            return true;
        }
        if (jj_scan_token(93))
        {
            return true;
        }
        if (jj_scan_token(31))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_130()
    {
        if (jj_3R_93())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_65()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(127))
        {
            jj_scanpos = token;
        }
        if (jj_3R_67())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_66()
    {
        if (jj_3R_98())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_99());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_80()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_111())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_185());
        jj_scanpos = token;
        if (jj_scan_token(126))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_86()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(25))
        {
            jj_scanpos = token;
            if (jj_scan_token(30))
            {
                jj_scanpos = token;
                if (jj_scan_token(27))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(60))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(49))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(51))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(42))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(36))
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


    private bool jj_3R_96()
    {
        Token token = jj_scanpos;
        if (jj_3R_125())
        {
            jj_scanpos = token;
            if (jj_scan_token(65))
            {
                jj_scanpos = token;
                if (jj_3R_126())
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
                                jj_scanpos = token;
                                if (jj_3R_130())
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


    private bool jj_3R_73()
    {
        Token token = jj_scanpos;
        if (jj_3_16())
        {
            jj_scanpos = token;
            if (jj_3R_102())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_293()
    {
        Token token = jj_scanpos;
        if (jj_3R_297())
        {
            jj_scanpos = token;
            if (jj_3R_298())
            {
                jj_scanpos = token;
                if (jj_3R_299())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_97()
    {
        if (jj_scan_token(84))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_271()
    {
        Token token = jj_scanpos;
        if (jj_3R_281())
        {
            jj_scanpos = token;
            if (jj_3R_282())
            {
                jj_scanpos = token;
                if (jj_3R_283())
                {
                    jj_scanpos = token;
                    if (jj_3R_284())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_303()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_271())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_304()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_293())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_321()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(106))
        {
            jj_scanpos = token;
            if (jj_scan_token(107))
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_144()
    {
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_145()
    {
        if (jj_3R_147())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_62()
    {
        if (jj_3R_67())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_63()
    {
        if (jj_3R_97())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_64()
    {
        if (jj_3R_61())
        {
            return true;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_115()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_116()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(97))
        {
            jj_scanpos = token;
            if (jj_scan_token(96))
            {
                jj_scanpos = token;
                if (jj_scan_token(85))
                {
                    jj_scanpos = token;
                    if (jj_3R_144())
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(65))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(62))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(53))
                                {
                                    jj_scanpos = token;
                                    if (jj_3R_145())
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


    private bool jj_3R_85()
    {
        Token token = jj_scanpos;
        if (jj_3_28())
        {
            jj_scanpos = token;
            if (jj_3R_115())
            {
                jj_scanpos = token;
                if (jj_3R_116())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_84()
    {
        lookingAhead = true;
        jj_semLA = ((getToken(1).Kind == 126 && ((Token.GTToken)getToken(1)).RealKind == 124) ? true : false);
        lookingAhead = false;
        if (!jj_semLA || jj_3R_114())
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_302()
    {
        if (jj_3R_61())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_321())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_301()
    {
        Token token = jj_scanpos;
        if (jj_3R_303())
        {
            jj_scanpos = token;
            if (jj_3R_304())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_297()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(97))
        {
            jj_scanpos = token;
            if (jj_scan_token(96))
            {
                return true;
            }
        }
        if (jj_3R_271())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_298()
    {
        if (jj_3R_301())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_299()
    {
        if (jj_3R_302())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_253()
    {
        if (jj_3R_271())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_294());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_83()
    {
        lookingAhead = true;
        jj_semLA = ((getToken(1).Kind == 126 && ((Token.GTToken)getToken(1)).RealKind == 125) ? true : false);
        lookingAhead = false;
        if (!jj_semLA || jj_3R_113())
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        if (jj_scan_token(126))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_247()
    {
        if (jj_3R_253())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_285());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_234()
    {
        if (jj_scan_token(107))
        {
            return true;
        }
        if (jj_3R_61())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_233()
    {
        if (jj_scan_token(106))
        {
            return true;
        }
        if (jj_3R_61())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_281()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(108))
        {
            jj_scanpos = token;
            if (jj_scan_token(109))
            {
                return true;
            }
        }
        if (jj_3R_271())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_282()
    {
        if (jj_3R_233())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_283()
    {
        if (jj_3R_234())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_284()
    {
        if (jj_3R_293())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_240()
    {
        if (jj_3R_247())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_24());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_294()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(110))
        {
            jj_scanpos = token;
            if (jj_scan_token(111))
            {
                jj_scanpos = token;
                if (jj_scan_token(115))
                {
                    return true;
                }
            }
        }
        if (jj_3R_271())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_229()
    {
        if (jj_3R_235())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_248())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_285()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(108))
        {
            jj_scanpos = token;
            if (jj_scan_token(109))
            {
                return true;
            }
        }
        if (jj_3R_253())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_221()
    {
        if (jj_3R_229())
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


    private bool jj_3R_254()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(95))
        {
            jj_scanpos = token;
            if (jj_scan_token(126))
            {
                jj_scanpos = token;
                if (jj_scan_token(101))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(102))
                    {
                        return true;
                    }
                }
            }
        }
        if (jj_3R_240())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_179()
    {
        if (jj_3R_203())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_230());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_235()
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
        while (!jj_3R_254());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_248()
    {
        if (jj_scan_token(48))
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_203()
    {
        if (jj_3R_221())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_236());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_166()
    {
        if (jj_3R_179())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_222());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_241()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(100))
        {
            jj_scanpos = token;
            if (jj_scan_token(103))
            {
                return true;
            }
        }
        if (jj_3R_229())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_155()
    {
        if (jj_3R_166())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_204());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_236()
    {
        if (jj_scan_token(112))
        {
            return true;
        }
        if (jj_3R_221())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_230()
    {
        if (jj_scan_token(114))
        {
            return true;
        }
        if (jj_3R_203())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_222()
    {
        if (jj_scan_token(113))
        {
            return true;
        }
        if (jj_3R_179())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_204()
    {
        if (jj_scan_token(105))
        {
            return true;
        }
        if (jj_3R_166())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_182()
    {
        if (jj_scan_token(104))
        {
            return true;
        }
        if (jj_3R_155())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_143()
    {
        if (jj_3R_155())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_182());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_168()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_81()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(94))
        {
            jj_scanpos = token;
            if (jj_scan_token(118))
            {
                jj_scanpos = token;
                if (jj_scan_token(119))
                {
                    jj_scanpos = token;
                    if (jj_scan_token(123))
                    {
                        jj_scanpos = token;
                        if (jj_scan_token(116))
                        {
                            jj_scanpos = token;
                            if (jj_scan_token(117))
                            {
                                jj_scanpos = token;
                                if (jj_scan_token(131))
                                {
                                    jj_scanpos = token;
                                    if (jj_scan_token(132))
                                    {
                                        jj_scanpos = token;
                                        if (jj_scan_token(133))
                                        {
                                            jj_scanpos = token;
                                            if (jj_scan_token(120))
                                            {
                                                jj_scanpos = token;
                                                if (jj_scan_token(122))
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_scan_token(121))
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


    private bool jj_3R_112()
    {
        if (jj_3R_143())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_168())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_291()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_120()
    {
        if (jj_3R_73())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_111()
    {
        Token token = jj_scanpos;
        if (jj_3R_141())
        {
            jj_scanpos = token;
            if (jj_3R_142())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_79()
    {
        Token token = jj_scanpos;
        if (jj_3R_109())
        {
            jj_scanpos = token;
            if (jj_3R_110())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_232()
    {
        Token token = jj_scanpos;
        if (jj_3R_238())
        {
            jj_scanpos = token;
            if (jj_3R_239())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_238()
    {
        if (jj_scan_token(38))
        {
            return true;
        }
        if (jj_3R_79())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_239()
    {
        if (jj_scan_token(62))
        {
            return true;
        }
        if (jj_3R_79())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_225()
    {
        if (jj_3R_232())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_141()
    {
        if (jj_3R_79())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_142()
    {
        if (jj_scan_token(98))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_225())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_185()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_111())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_140()
    {
        if (jj_3R_72())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3_19())
        {
            jj_scanpos = token;
        }
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_20());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_109()
    {
        if (jj_3R_86())
        {
            return true;
        }
        if (jj_3_17())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_17());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_110()
    {
        if (jj_3R_140())
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


    private bool jj_3R_277()
    {
        if (jj_3R_93())
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


    private bool jj_3R_102()
    {
        if (jj_3R_86())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_78()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_108())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_103()
    {
        if (jj_scan_token(87))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_137());
        jj_scanpos = token;
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_77()
    {
        Token token = jj_scanpos;
        if (jj_3R_106())
        {
            jj_scanpos = token;
            if (jj_3R_107())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_106()
    {
        if (jj_scan_token(65))
        {
            return true;
        }
        if (jj_3R_78())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_107()
    {
        Token token = jj_scanpos;
        if (jj_3_14())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(62))
        {
            return true;
        }
        if (jj_3R_78())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_95()
    {
        Token token = jj_scanpos;
        if (jj_3R_122())
        {
            jj_scanpos = token;
            if (jj_3R_123())
            {
                jj_scanpos = token;
                if (jj_3R_124())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_289()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_scan_token(130))
        {
            jj_scanpos = token;
        }
        if (jj_3R_278())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_153()
    {
        Token token = jj_scanpos;
        if (jj_3R_163())
        {
            jj_scanpos = token;
            if (jj_3R_164())
            {
                jj_scanpos = token;
                if (jj_3R_165())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_101()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_135())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_136());
        jj_scanpos = token;
        if (jj_scan_token(126))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_260()
    {
        if (jj_3R_101())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_261()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_276())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_262()
    {
        if (jj_scan_token(67))
        {
            return true;
        }
        if (jj_3R_277())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_263()
    {
        if (jj_3R_77())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_264()
    {
        if (jj_3R_153())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_92()
    {
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_7());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_278()
    {
        if (jj_3R_72())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_292());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_76()
    {
        Token token = jj_scanpos;
        if (jj_3R_104())
        {
            jj_scanpos = token;
            if (jj_3R_105())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_187()
    {
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_290()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_289())
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
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_290());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_146()
    {
        if (jj_scan_token(87))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_46());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_scan_token(92))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_100()
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


    private bool jj_3R_122()
    {
        if (jj_3R_100())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_123()
    {
        if (jj_3R_146())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_124()
    {
        if (jj_3R_112())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_280()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_188()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_187())
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
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_188());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_267()
    {
        if (jj_3R_101())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_268()
    {
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_3R_261())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_280());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_269()
    {
        if (jj_scan_token(67))
        {
            return true;
        }
        if (jj_3R_277())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_270()
    {
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_265()
    {
        if (jj_3R_278())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_279())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_223()
    {
        if (jj_3R_76())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_12());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_138()
    {
        if (jj_scan_token(87))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_223())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_scan_token(92))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_104()
    {
        if (jj_3R_138())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_105()
    {
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_161()
    {
        if (jj_3R_174())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_94()
    {
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(94))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_292()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_151()
    {
        if (jj_scan_token(136))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_150()
    {
        if (jj_scan_token(136))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_95())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_279()
    {
        if (jj_scan_token(94))
        {
            return true;
        }
        if (jj_3R_76())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_149()
    {
        if (jj_scan_token(136))
        {
            return true;
        }
        if (jj_3R_93())
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_161())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_132()
    {
        if (jj_3R_149())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_133()
    {
        if (jj_3R_150())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_134()
    {
        if (jj_3R_151())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_74()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_266()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_265())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_71()
    {
        if (jj_3R_101())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_252()
    {
        Token token = jj_scanpos;
        if (jj_3R_267())
        {
            jj_scanpos = token;
        }
        if (jj_3R_88())
        {
            return true;
        }
        if (jj_3R_268())
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_269())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_270())
        {
            jj_scanpos = token;
            if (jj_scan_token(91))
            {
                return true;
            }
        }
        return false;
    }

    private bool jj_3R_113()
    {
        return false;
    }


    private bool jj_3R_251()
    {
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_265())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_266());
        jj_scanpos = token;
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_250()
    {
        Token token = jj_scanpos;
        if (jj_3R_260())
        {
            jj_scanpos = token;
        }
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_3R_261())
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_262())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(87))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_263())
        {
            jj_scanpos = token;
        }
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_264());
        jj_scanpos = token;
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_249()
    {
        if (jj_scan_token(129))
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_258())
        {
            jj_scanpos = token;
        }
        if (jj_3R_259())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_206()
    {
        if (jj_scan_token(87))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_224());
        jj_scanpos = token;
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }

    private bool jj_3R_114()
    {
        return false;
    }


    private bool jj_3R_178()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(31))
        {
            jj_scanpos = token;
            if (jj_scan_token(50))
            {
                return true;
            }
        }
        if (jj_3R_72())
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_255())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_256())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_257())
        {
            jj_scanpos = token;
        }
        if (jj_3R_206())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_242()
    {
        if (jj_3R_178())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_243()
    {
        if (jj_3R_249())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_244()
    {
        if (jj_3R_250())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_245()
    {
        if (jj_3R_251())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_246()
    {
        if (jj_3R_252())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_75()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(61))
        {
            jj_scanpos = token;
        }
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_237()
    {
        if (jj_3R_92())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_242())
        {
            jj_scanpos = token;
            if (jj_3R_243())
            {
                jj_scanpos = token;
                if (jj_3R_244())
                {
                    jj_scanpos = token;
                    if (jj_3R_245())
                    {
                        jj_scanpos = token;
                        if (jj_3R_246())
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }


    private bool jj_3R_313()
    {
        if (jj_scan_token(29))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_289())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_314()
    {
        if (jj_scan_token(41))
        {
            return true;
        }
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_162()
    {
        if (jj_scan_token(38))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_175());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_231()
    {
        Token token = jj_scanpos;
        if (jj_3_11())
        {
            jj_scanpos = token;
            if (jj_3R_237())
            {
                jj_scanpos = token;
                if (jj_scan_token(91))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_135()
    {
        if (jj_3R_72())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_152())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_224()
    {
        if (jj_3R_231())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_175()
    {
        if (jj_scan_token(112))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_326()
    {
        if (jj_3R_329())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_209()
    {
        Token token = jj_scanpos;
        if (jj_3R_226())
        {
            jj_scanpos = token;
            if (jj_3R_227())
            {
                jj_scanpos = token;
                if (jj_3R_228())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_152()
    {
        if (jj_3R_162())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_312()
    {
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_70()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_295())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_296())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_136()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_135())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_311()
    {
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_310()
    {
        if (jj_3R_72())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_295()
    {
        if (jj_3R_78())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_296()
    {
        if (jj_3R_206())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_288()
    {
        if (jj_3R_231())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_329()
    {
        if (jj_3R_209())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_330());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_274()
    {
        if (jj_3R_70())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3_8());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_275()
    {
        if (jj_scan_token(91))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_288());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_330()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_209())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_177()
    {
        Token token = jj_scanpos;
        if (jj_3_40())
        {
            jj_scanpos = token;
            if (jj_3R_189())
            {
                jj_scanpos = token;
                if (jj_3R_190())
                {
                    jj_scanpos = token;
                    if (jj_scan_token(91))
                    {
                        jj_scanpos = token;
                        if (jj_3R_191())
                        {
                            jj_scanpos = token;
                            if (jj_3R_192())
                            {
                                jj_scanpos = token;
                                if (jj_3R_193())
                                {
                                    jj_scanpos = token;
                                    if (jj_3R_194())
                                    {
                                        jj_scanpos = token;
                                        if (jj_3R_195())
                                        {
                                            jj_scanpos = token;
                                            if (jj_3R_196())
                                            {
                                                jj_scanpos = token;
                                                if (jj_3R_197())
                                                {
                                                    jj_scanpos = token;
                                                    if (jj_3R_198())
                                                    {
                                                        jj_scanpos = token;
                                                        if (jj_3R_199())
                                                        {
                                                            jj_scanpos = token;
                                                            if (jj_3R_200())
                                                            {
                                                                jj_scanpos = token;
                                                                if (jj_3R_201())
                                                                {
                                                                    jj_scanpos = token;
                                                                    if (jj_3R_202())
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


    private bool jj_3R_273()
    {
        if (jj_scan_token(46))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_287());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_258()
    {
        if (jj_3R_273())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_259()
    {
        if (jj_scan_token(87))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_274())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_scan_token(92))
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_275())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_176()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_265())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_300());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_327()
    {
        if (jj_3R_176())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_328()
    {
        if (jj_3R_329())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_325()
    {
        Token token = jj_scanpos;
        if (jj_3R_327())
        {
            jj_scanpos = token;
            if (jj_3R_328())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_287()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_318()
    {
        if (jj_3R_325())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_319()
    {
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_320()
    {
        if (jj_3R_326())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_286()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_308()
    {
        if (jj_3R_92())
        {
            return true;
        }
        if (jj_3R_73())
        {
            return true;
        }
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_309()
    {
        Token token = jj_scanpos;
        if (jj_3R_318())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_319())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_320())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_272()
    {
        if (jj_scan_token(38))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_286());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_255()
    {
        if (jj_3R_101())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_256()
    {
        if (jj_3R_272())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_257()
    {
        if (jj_3R_273())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_307()
    {
        if (jj_scan_token(37))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_323()
    {
        if (jj_scan_token(28))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_324()
    {
        if (jj_scan_token(34))
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_316()
    {
        Token token = jj_scanpos;
        if (jj_3R_323())
        {
            jj_scanpos = token;
            if (jj_3R_324())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_317()
    {
        if (jj_3R_153())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_306()
    {
        if (jj_3R_316())
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
        return false;
    }


    private bool jj_3R_322()
    {
        if (jj_3R_81())
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_315()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(106))
        {
            jj_scanpos = token;
            if (jj_scan_token(107))
            {
                jj_scanpos = token;
                if (jj_3R_322())
                {
                    return true;
                }
            }
        }
        return false;
    }


    private bool jj_3R_226()
    {
        if (jj_3R_233())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_227()
    {
        if (jj_3R_234())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_228()
    {
        if (jj_3R_61())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_315())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_300()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_265())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_69()
    {
        if (jj_3R_100())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_163()
    {
        if (jj_3R_176())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_164()
    {
        if (jj_3R_177())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_165()
    {
        if (jj_3R_178())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_137()
    {
        if (jj_3R_153())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_68()
    {
        if (jj_3R_100())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_305()
    {
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_220()
    {
        if (jj_scan_token(70))
        {
            return true;
        }
        if (jj_3R_103())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_313());
        jj_scanpos = token;
        token = jj_scanpos;
        if (jj_3R_314())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_219()
    {
        if (jj_scan_token(64))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_218()
    {
        if (jj_scan_token(66))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_217()
    {
        if (jj_scan_token(59))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_312())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_216()
    {
        if (jj_scan_token(33))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_311())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_215()
    {
        if (jj_scan_token(26))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_310())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_214()
    {
        if (jj_scan_token(43))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_308())
        {
            jj_scanpos = token;
            if (jj_3R_309())
            {
                return true;
            }
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_213()
    {
        if (jj_scan_token(35))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        if (jj_scan_token(73))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_212()
    {
        if (jj_scan_token(73))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_211()
    {
        if (jj_scan_token(45))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_307())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_210()
    {
        if (jj_scan_token(63))
        {
            return true;
        }
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        if (jj_scan_token(86))
        {
            return true;
        }
        if (jj_scan_token(87))
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_306());
        jj_scanpos = token;
        if (jj_scan_token(88))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_208()
    {
        if (jj_scan_token(135))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_305())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_91()
    {
        if (jj_3R_72())
        {
            return true;
        }
        if (jj_scan_token(99))
        {
            return true;
        }
        if (jj_3R_177())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_189()
    {
        if (jj_3R_208())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_190()
    {
        if (jj_3R_103())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_191()
    {
        if (jj_3R_209())
        {
            return true;
        }
        if (jj_scan_token(91))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_192()
    {
        if (jj_3R_210())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_193()
    {
        if (jj_3R_211())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_194()
    {
        if (jj_3R_212())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_195()
    {
        if (jj_3R_213())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_196()
    {
        if (jj_3R_214())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_197()
    {
        if (jj_3R_215())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_198()
    {
        if (jj_3R_216())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_199()
    {
        if (jj_3R_217())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_200()
    {
        if (jj_3R_218())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_201()
    {
        if (jj_3R_219())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_202()
    {
        if (jj_3R_220())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_205()
    {
        if (jj_scan_token(89))
        {
            return true;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_183()
    {
        if (jj_3R_205())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_205());
        jj_scanpos = token;
        if (jj_3R_138())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_184()
    {
        if (jj_3R_206())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_169()
    {
        Token token = jj_scanpos;
        if (jj_3_39())
        {
            jj_scanpos = token;
            if (jj_3R_183())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_170()
    {
        if (jj_3R_80())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_171()
    {
        if (jj_3R_169())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_172()
    {
        if (jj_3R_78())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_184())
        {
            jj_scanpos = token;
        }
        return false;
    }


    private bool jj_3R_121()
    {
        if (jj_scan_token(53))
        {
            return true;
        }
        if (jj_3R_140())
        {
            return true;
        }
        Token token = jj_scanpos;
        if (jj_3R_170())
        {
            jj_scanpos = token;
        }
        token = jj_scanpos;
        if (jj_3R_171())
        {
            jj_scanpos = token;
            if (jj_3R_172())
            {
                return true;
            }
        }
        return false;
    }


    private bool jj_3R_154()
    {
        if (jj_scan_token(92))
        {
            return true;
        }
        if (jj_3R_82())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_139()
    {
        if (jj_3R_82())
        {
            return true;
        }
        Token token;
        do
        {
            token = jj_scanpos;
        }
        while (!jj_3R_154());
        jj_scanpos = token;
        return false;
    }


    private bool jj_3R_108()
    {
        if (jj_3R_139())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_207()
    {
        if (jj_3R_97())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_180()
    {
        if (jj_scan_token(69))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_181()
    {
        if (jj_scan_token(39))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_186()
    {
        if (jj_3R_207())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_167()
    {
        Token token = jj_scanpos;
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


    private bool jj_3R_156()
    {
        if (jj_3R_167())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_173()
    {
        Token token = jj_scanpos;
        if (jj_scan_token(97))
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(89))
        {
            return true;
        }
        token = jj_scanpos;
        if (jj_3R_186())
        {
            jj_scanpos = token;
        }
        if (jj_scan_token(90))
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_157()
    {
        if (jj_3R_97())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_158()
    {
        if (jj_scan_token(95))
        {
            return true;
        }
        if (jj_3R_67())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_159()
    {
        if (jj_3R_173())
        {
            return true;
        }
        return false;
    }


    private bool jj_3R_160()
    {
        if (jj_scan_token(85))
        {
            return true;
        }
        if (jj_3R_66())
        {
            return true;
        }
        return false;
    }


    public JJTreeParser(Stream @is, string str)
    {
        jjtree = new();
        lookingAhead = false;
        jj_la1 = new int[178];
        jj_2_rtns = new JJCalls[47];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new();
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
            goto IL_008e;
        }
        token_source = new JJTreeParserTokenManager(jj_input_stream);
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
        return;
    IL_008e:

        throw ex;
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
        jjtree.Reset();
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
        return;
    IL_001e:
        throw ex;
    }


    public virtual ParseException generateParseException()
    {
        jj_expentries.Clear();
        bool[] array = new bool[140];
        if (jj_kind >= 0)
        {
            array[jj_kind] = true;
            jj_kind = -1;
        }
        for (int i = 0; i < 178; i++)
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
        for (int i = 0; i < 140; i++)
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
            array2[j] = jj_expentries[j];
        }
        return new ParseException(token, array2, JJTreeParserConstants.tokenImage);
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
            foreach (var array2 in jj_expentries)
            {
                //int[] array2 = (int[])iterator.next();
                if (array2.Length != jj_expentry.Length)
                {
                    continue;
                }
                i = 1;
                for (int j = 0; j < jj_expentry.Length; j++)
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
        for (int i = 0; i < 47; i++)
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
                        }
                    }
                    jJCalls = jJCalls.next;
                }
                while (jJCalls != null);
            }
            catch (LookaheadSuccess)
            {
                goto IL_033b;
            }
            continue;
        IL_033b:
            ;

        }
        jj_rescan = false;
    }

    private static void jj_la1_init_0()
    {
        jj_la1_0 = new int[178]
        {
            1241518078, 6, 6, 0, 1241518078, 0, 0, 0, -822079490, 0,
            0, 0, -822079490, 0, 0, 0, 0, 0, 0, 0,
            960, 0, 0, 0, 2, 0, 0, 536870912, 0, 0,
            0, 0, 2, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 4094, 0,
            0, 4094, 0, -2130706432, 0, 0, 16777216, -2147483648, -2130706432, -2147483648,
            0, 0, 0, 0, 0, 0, 16781310, 0, -889188354, 0,
            0, 0, 0, 0, 0, -889188354, -2147483648, 1241518078, -889188354, 0,
            0, 0, 1241518078, 1241518078, 0, 0, 0, 0, 0, 0,
            1258295294, 0, 0, 0, -822079490, 1241518078, 0, 1241513984, 1241518078, 0,
            0, 1241518078, 0, 1241513984, 1241518078, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 1241518078, 0, 0, 1241518078, 4094, 0,
            0, 0, 0, 0, 4094, 0, 0, 0, 1241518078, 0,
            0, 0, 0, 0, 0, 0, 1308626942, 0, -822079490, -838856706,
            0, 0, 0, 1241518078, 268435456, -822079490, 268435456, 0, 1258295294, 1241518078,
            1241518078, 1258295294, 1241518078, 0, 4094, 4094, 1241518078, 536870912, 0, 0,
            4094, 0, 1241518078, 0, -889188354, 0, -905965570, -889188354
        };
    }

    private static void jj_la1_init_1()
    {
        jj_la1_1 = new int[178]
        {
            386532368, 536870912, 536870912, 128, 386532368, 0, 0, 0, -8508006, 0,
            0, 0, -8508006, 117440512, 117440512, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 512, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 32768, 655622400, 536870912, 0, 655360256, 262144, 655622400, 262144,
            0, 64, 16384, 0, 0, 16384, 655360256, 0, 924714256, 0,
            0, 0, 0, 64, 0, 924714256, 262144, 269091856, 924714256, 0,
            0, 0, 1349125264, 1349125264, 0, 0, 0, 0, 0, 0,
            924452112, 0, 0, 0, -8508006, 1349125264, 536870912, 269091856, 269091856, 0,
            1073741888, 269091856, 1073741888, 269091856, 269091856, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 65536, 0, 0, 0, 0,
            0, 0, 0, 0, 1349125264, 0, 0, 1349125264, 1080033408, 0,
            0, 0, 0, 1080033408, 0, 0, 4194432, 128, 1349125264, 0,
            0, 0, 0, 2097152, 0, 0, -664130406, 0, -8508006, -663868262,
            0, 0, 0, 1349125264, 4, -8508006, 4, 32, 2004485520, 1349125264,
            1349125264, 2004485520, 1349125264, 0, 0, 0, 1349125264, 0, 512, 0,
            0, 0, 1349125264, 0, 924714256, 4, 269354000, 924714256
        };
    }

    private static void jj_la1_init_2()
    {
        jj_la1_2 = new int[178]
        {
            -2147483520, 0, 0, 1049632, -2147483520, 268435456, 8, 0, 146294775, 268435456,
            8, 0, 146294775, 0, 0, 268435456, -2147483648, -2147483648, 33554432, 0,
            0, 8388608, 0, 0, 0, 0, 1024, 0, 0, 536870912,
            -2146435072, 0, 41943104, 2097152, 0, 0, 1048576, -2147483648, 0, -2110783488,
            1024, 268435456, 8388608, -2110783488, 0, 268435456, 1048576, 0, 128, 0,
            2097152, 0, 0, 134218001, 0, 536870912, 273, 0, 134218001, 0,
            -2147483648, 0, 0, 268435456, 268435456, 0, 273, 268435456, -2004876911, 134217728,
            2097152, 8388608, 268435456, 0, 0, -2004876911, 0, -2147483520, -2013265519, 268435456,
            1073741824, 33554432, 12076194, 12076194, 268435456, -2147483648, 8, 142606336, 33554432, 268435456,
            273, 0, -2147483648, 8, 146294775, 3687586, 0, 0, 0, 268435456,
            0, 0, 0, 0, 128, 268435456, 1073741824, 0, 0, 0,
            0, 0, 0, 0, 0, 0, -2147483648, -2147483648, 0, 0,
            0, 0, 0, 0, 3687586, 0, 0, 3687586, 3687458, 2097152,
            0, 0, 2097152, 3687458, 0, 572522496, 1590304, 32, 3687586, 268435456,
            -2147483648, 8388608, 35651584, 0, 33554432, 33554432, 146294503, 0, 146294775, 146294503,
            268435456, 1073741824, 1073741824, 3687586, 0, 146294775, 0, 0, 3687859, 3687586,
            3687586, 137905587, 3687586, 268435456, 0, 0, 3687586, 0, 0, 0,
            0, 268435456, 12076194, 268435456, 134218001, 0, 0, 134218001
        };
    }

    private static void jj_la1_init_3()
    {
        jj_la1_3 = new int[178]
        {
            0, 0, 0, 0, 0, 0, 0, -2147483648, 3072, 0,
            0, -2147483648, 3072, 0, 0, 0, 0, 0, 0, 131072,
            0, 0, 8, 131072, 0, -2147483648, 0, 0, 0, 0,
            0, 20484, 0, 0, -2147483648, -2147483648, 0, 0, 131072, 2,
            0, 0, 20484, 2, 2, 0, 0, 8192, 0, 1073741824,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 65536, 0, 0, 0, 0, 0,
            0, 0, 15363, 15363, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 3072, 0, 0, 0, 0, 0,
            0, 4, 0, 0, 0, 0, 267386880, 4, 256, 512,
            131072, 262144, 65536, 144, 144, 0, 1073741920, 1073741920, 0, 12288,
            12288, 573440, 573440, 12288, 15363, 3, 3, 0, 3, 0,
            3072, 3072, 0, 0, 0, 0, 0, 0, 15363, 0,
            0, 0, 0, 0, 0, 0, 3072, 8, 3072, 3072,
            0, 267389952, 267389952, 3072, 0, 3072, 0, 0, 3072, 15363,
            3072, 3072, 3072, 0, 0, 0, 15363, 0, 0, 0,
            0, 0, 15363, 0, 0, 0, 0, 0
        };
    }

    private static void jj_la1_init_4()
    {
        jj_la1_4 = new int[178]
        {
            512, 512, 512, 0, 512, 0, 0, 0, 897, 0,
            0, 0, 897, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            512, 0, 0, 0, 0, 512, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 512, 0,
            0, 512, 0, 259, 0, 0, 257, 258, 259, 0,
            0, 0, 0, 0, 0, 0, 769, 0, 771, 0,
            0, 0, 0, 0, 0, 771, 2, 512, 771, 0,
            0, 0, 512, 512, 0, 0, 0, 0, 0, 0,
            769, 4, 0, 0, 897, 512, 0, 0, 512, 0,
            0, 512, 0, 0, 512, 0, 56, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 64, 0,
            0, 0, 0, 0, 512, 0, 0, 512, 512, 0,
            0, 0, 0, 0, 512, 0, 0, 0, 512, 0,
            0, 0, 0, 0, 0, 0, 640, 0, 897, 640,
            0, 56, 56, 512, 0, 897, 0, 0, 769, 512,
            512, 769, 512, 0, 512, 512, 512, 0, 0, 256,
            512, 0, 768, 0, 771, 0, 770, 771
        };
    }


    public void CastLookahead()
    {
        if (jj_2_28(2))
        {
            jj_consume_token(85);
            PrimitiveType();
            return;
        }
        if (jj_2_29(int.MaxValue))
        {
            jj_consume_token(85);
            Type();
            jj_consume_token(89);
            jj_consume_token(90);
            return;
        }
        if (((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk()) == 85)
        {
            jj_consume_token(85);
            Type();
            jj_consume_token(86);
            switch ((this.m_jj_ntk != -1) ? this.m_jj_ntk : jj_ntk())
            {
                case 97:
                    jj_consume_token(97);
                    return;
                case 96:
                    jj_consume_token(96);
                    return;
                case 85:
                    jj_consume_token(85);
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
                case 137:
                    JavaIdentifier();
                    return;
                case 65:
                    jj_consume_token(65);
                    return;
                case 62:
                    jj_consume_token(62);
                    return;
                case 53:
                    jj_consume_token(53);
                    return;
                case 39:
                case 54:
                case 69:
                case 74:
                case 78:
                case 83:
                case 84:
                    Literal();
                    return;
            }
            jj_la1[128] = jj_gen;
            jj_consume_token(-1);

            throw new ParseException();
        }
        jj_la1[129] = jj_gen;
        jj_consume_token(-1);

        throw new ParseException();
    }


    public JJTreeParser(Stream @is)
        : this(@is, null)
    {
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
        jjtree.Reset();
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public JJTreeParser(JJTreeParserTokenManager jjtptm)
    {
        jjtree = new();
        lookingAhead = false;
        jj_la1 = new int[178];
        jj_2_rtns = new JJCalls[47];
        jj_rescan = false;
        jj_gc = 0;
        jj_ls = new();
        jj_expentries = new();
        jj_kind = -1;
        jj_lasttokens = new int[100];
        token_source = jjtptm;
        token = new Token();
        this.m_jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }


    public virtual void ReInit(JJTreeParserTokenManager jjtptm)
    {
        token_source = jjtptm;
        token = new Token();
        this.m_jj_ntk = -1;
        jjtree.Reset();
        jj_gen = 0;
        for (int i = 0; i < 178; i++)
        {
            jj_la1[i] = -1;
        }
        for (int i = 0; i < jj_2_rtns.Length; i++)
        {
            jj_2_rtns[i] = new JJCalls();
        }
    }

    public void enable_tracing()
    {
    }

    public void disable_tracing()
    {
    }

    static JJTreeParser()
    {
        jj_la1_init_0();
        jj_la1_init_1();
        jj_la1_init_2();
        jj_la1_init_3();
        jj_la1_init_4();
    }
}
