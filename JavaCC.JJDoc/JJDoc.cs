namespace JavaCC.JJDoc;
using JavaCC.Parser;
using System.Collections.Generic;

public class JJDoc : JJDocGlobals
{
    private static void EmitTokenProductions(Generator generator, List<TokenProduction> list)
    {
        generator.TokensStart();
        foreach (var tokenProduction in list)
        {
            EmitTopLevelSpecialTokens(tokenProduction.FirstToken, generator);
            var text = "";
            if (tokenProduction.IsExplicit)
            {
                if (tokenProduction.LexStates == null)
                {
                    text += ("<*> ");
                }
                else
                {
                    text += ("<");
                    for (int i = 0; i < tokenProduction.LexStates.Length; i++)
                    {
                        text += (tokenProduction.LexStates[i]);
                        if (i < tokenProduction.LexStates.Length - 1)
                        {
                            text += (",");
                        }
                    }
                    text += ("> ");
                }
                text += (TokenProduction.KindImage[tokenProduction.Kind]);
                if (tokenProduction.IgnoreCase)
                {
                    text += (" [IGNORE_CASE]");
                }
                text += (" : {\n");
                foreach (var regExprSpec in tokenProduction.Respecs)
                {
                    text += (EmitRE(regExprSpec.rexp));
                    if (regExprSpec.nsTok != null)
                    {
                        text += (" : ") + (regExprSpec.nsTok.Image)
                            ;
                    }
                    text += ("\n");
                    text += ("| ");
                }
                text += ("}\n\n");
            }
            if (!string.Equals(text, ""))
            {
                generator.TokenStart(tokenProduction);
                generator.Text(text);
                generator.TokenEnd(tokenProduction);
            }
        }
        generator.TokensEnd();
    }


    private static void EmitNormalProductions(Generator generator, List<NormalProduction> ps)
    {
        generator.NonterminalsStart();
        foreach (var p in ps)
        {
            EmitTopLevelSpecialTokens(p.firstToken, generator);
            if (p is BNFProduction b2)
            {
                generator.ProductionStart(p);
                if (p.Expansion is Choice c)
                {
                    int b = 1;
                    foreach (var ex in c.Choices)
                    {
                        generator.ExpansionStart(ex, (byte)b != 0);
                        EmitExpansionTree(ex, generator);
                        generator.ExpansionEnd(ex, (byte)b != 0);
                        b = 0;
                    }
                }
                else
                {
                    generator.ExpansionStart(p.Expansion, b: true);
                    EmitExpansionTree(p.Expansion, generator);
                    generator.ExpansionEnd(p.Expansion, b: true);
                }
                generator.ProductionEnd(p);
            }
            else if (p is JavaCodeProduction j)
            {
                generator.Javacode(j);
            }
        }
        generator.NonterminalsEnd();
    }

    private static Token GetPrecedingSpecialToken(Token token)
    {
        var other = token;
        while (other.SpecialToken != null)
        {
            other = other.SpecialToken;
        }
        return (other == token) ? null : other;
    }


    private static void EmitTopLevelSpecialTokens(Token token, Generator generator)
    {
        if (token == null)
        {
            return;
        }
        token = GetPrecedingSpecialToken(token);
        var text = "";
        if (token != null)
        {
            JavaCCGlobals.cline = token.BeginLine;
            JavaCCGlobals.ccol = token.BeginColumn;
            while (token != null)
            {
                text += (JavaCCGlobals.PrintTokenOnly(token));
                token = token.Next;
            }
        }
        if (!string.Equals(text, ""))
        {
            generator.SpecialTokens(text);
        }
    }


    private static string EmitRE(RegularExpression re)
    {
        bool first = true;

        string text = "";
        bool f1 = ((!string.Equals(re.label, "")));
        bool f2 = ((re is RJustName));
        bool f3 = ((re is REndOfFile));
        bool f4 = ((re is RStringLiteral));
        bool f5 = ((re.tpContext != null));
        bool f6 = ((f2 || f3 || f1 || (!f4 && f5 )));
        if (f6)
        {
            text += ("<");
            if (!f2)
            {
                if (re.private_rexp)
                {
                    text += ("#");
                }
                if (f1)
                {
                    text += (re.label);
                    text += (": ");
                }
            }
        }
        if (re is RCharacterList rCharacterList)
        {
            if (rCharacterList.negated_list)
            {
                text += ("~");
            }
            text += ("[");
            foreach (var obj in rCharacterList.descriptors)
            {
                if (obj is SingleCharacter character)
                {
                    text += ("\"");
                    char[] value = new char[1] { character.CH };
                    text += (JavaCCGlobals.AddEscapes(new string(value)));
                    text += ("\"");
                }
                else if (obj is CharacterRange range)
                {
                    text += ("\"");
                    char[] value = new char[1] { range.Left };
                    text += (JavaCCGlobals.AddEscapes(new string(value)));
                    text += ("\"-\"");
                    value[0] = range.Right;
                    text += (JavaCCGlobals.AddEscapes(new string(value)));
                    text += ("\"");
                }
                else
                {
                    JJDocGlobals.Error("Oops: unknown character list element type.");
                }
                if (!first)
                {
                    text += (",");
                }
                first = false;
            }
            text += ("]");
        }
        else if (re is RChoice rChoice)
        {
            foreach (var regularExpression in rChoice.Choices)
            {
                text += (EmitRE(regularExpression as RegularExpression));
                if (!first)
                {
                    text += (" | ");
                }
                first = false;
            }
        }
        else if (re is REndOfFile)
        {
            text += ("EOF");
        }
        else if (re is RJustName rJustName)
        {
            text += (rJustName.label);
        }
        else if (re is ROneOrMore rOneOrMore)
        {
            text += ("(");
            text += (EmitRE(rOneOrMore.RegExpr));
            text += (")+");
        }
        else if (re is RSequence rSequence)
        {
            foreach (var regularExpression in rSequence.Units)
            {
                bool flag = false;
                if (regularExpression is RChoice)
                {
                    flag = true;
                }
                if (flag)
                {
                    text += ("(");
                }
                text += (EmitRE(regularExpression));
                if (flag)
                {
                    text += (")");
                }
                if (!first)
                {
                    text += (" ");
                }
                first = false;
            }
        }
        else if (re is RStringLiteral rStringLiteral)
        {
            text += ("\"") + (JavaCCGlobals.AddEscapes(rStringLiteral.image))
                + ("\"")
                ;
        }
        else if (re is RZeroOrMore rZeroOrMore)
        {
            text += ("(");
            text += (EmitRE(rZeroOrMore.Regexpr));
            text += (")*");
        }
        else if (re is RZeroOrOne rZeroOrOne)
        {
            text += ("(");
            text += (EmitRE(rZeroOrOne.regexpr));
            text += (")?");
        }
        else
        {
            JJDocGlobals.Error("Oops: Unknown regular expression type.");
        }
        if (f6)
        {
            text += (">");
        }
        return text;
    }


    private static void EmitExpansionTree(Expansion exp, Generator generator)
    {
        if (exp is Action a)
        {
            EmitExpansionAction(a, generator);
        }
        else if (exp is Choice c)
        {
            EmitExpansionChoice(c, generator);
        }
        else if (exp is Lookahead l)
        {
            EmitExpansionLookahead(l, generator);
        }
        else if (exp is NonTerminal t)
        {
            EmitExpansionNonTerminal(t, generator);
        }
        else if (exp is OneOrMore o)
        {
            EmitExpansionOneOrMore(o, generator);
        }
        else if (exp is RegularExpression r)
        {
            EmitExpansionRegularExpression(r, generator);
        }
        else if (exp is Sequence s)
        {
            EmitExpansionSequence(s, generator);
        }
        else if (exp is TryBlock b)
        {
            EmitExpansionTryBlock(b, generator);
        }
        else if (exp is ZeroOrMore z)
        {
            EmitExpansionZeroOrMore(z, generator);
        }
        else if (exp is ZeroOrOne e)
        {
            EmitExpansionZeroOrOne(e, generator);
        }
        else
        {
            JJDocGlobals.Error("Oops: Unknown expansion type.");
        }
    }

    private static void EmitExpansionAction(Action a, Generator generator)
    {
    }


    private static void EmitExpansionChoice(Choice c, Generator generator)
    {
        var first = true;
        foreach (var expansion in c.Choices)
        {
            EmitExpansionTree(expansion, generator);
            if (!first)
            {
                generator.Text(" | ");
            }
            first = false;
        }
    }

    private static void EmitExpansionLookahead(Lookahead l, Generator generator)
    {
    }


    private static void EmitExpansionNonTerminal(NonTerminal t, Generator generator)
    {
        generator.NonTerminalStart(t);
        generator.Text(t.name);
        generator.NonTerminalEnd(t);
    }


    private static void EmitExpansionOneOrMore(OneOrMore o, Generator generator)
    {
        generator.Text("( ");
        EmitExpansionTree(o.expansion, generator);
        generator.Text(" )+");
    }


    private static void EmitExpansionRegularExpression(RegularExpression r, Generator generator)
    {
        string text = EmitRE(r);
        if (!string.Equals(text, ""))
        {
            generator.ReStart(r);
            generator.Text(text);
            generator.ReEnd(r);
        }
    }


    private static void EmitExpansionSequence(Sequence seq, Generator generator)
    {
        int num = 1;
        foreach (var expansion in seq.Units)
        {
            if (!(expansion is Lookahead) && !(expansion is Action))
            {
                if (num == 0)
                {
                    generator.Text(" ");
                }
                int num2 = ((expansion is Choice || expansion is Sequence) ? 1 : 0);
                if (num2 != 0)
                {
                    generator.Text("( ");
                }
                EmitExpansionTree(expansion, generator);
                if (num2 != 0)
                {
                    generator.Text(" )");
                }
                num = 0;
            }
        }
    }


    private static void EmitExpansionTryBlock(TryBlock b, Generator generator)
    {
        int num = ((b.Expression is Choice) ? 1 : 0);
        if (num != 0)
        {
            generator.Text("( ");
        }
        EmitExpansionTree(b.Expression, generator);
        if (num != 0)
        {
            generator.Text(" )");
        }
    }


    private static void EmitExpansionZeroOrMore(ZeroOrMore z, Generator generator)
    {
        generator.Text("( ");
        EmitExpansionTree(z.Expansion, generator);
        generator.Text(" )*");
    }


    private static void EmitExpansionZeroOrOne(ZeroOrOne o, Generator generator)
    {
        generator.Text("( ");
        EmitExpansionTree(o.Expansion, generator);
        generator.Text(" )?");
    }


    public JJDoc() { }


    internal static void Start()
    {
        JJDocGlobals.generator = JJDocGlobals.Generator;
        JJDocGlobals.generator.DocumentStart();
        EmitTokenProductions(JJDocGlobals.generator, JavaCCGlobals.rexprlist);
        EmitNormalProductions(JJDocGlobals.generator, JavaCCGlobals.BNFProductions);
        JJDocGlobals.generator.DocumentEnd();
    }

}
