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
            EmitTopLevelSpecialTokens(tokenProduction.firstToken, generator);
            var text = "";
            if (tokenProduction.isExplicit)
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
                if (tokenProduction.ignoreCase)
                {
                    text += (" [IGNORE_CASE]");
                }
                text += (" : {\n");
                foreach (var regExprSpec in tokenProduction.Respecs)
                {
                    text += (EmitRE(regExprSpec.rexp));
                    if (regExprSpec.nsTok != null)
                    {
                        text += (" : ") + (regExprSpec.nsTok.image)
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
        while (other.specialToken != null)
        {
            other = other.specialToken;
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
                token = token.next;
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
        int num = ((!string.Equals(re.label, "")) ? 1 : 0);
        int num2 = ((re is RJustName) ? 1 : 0);
        int num3 = ((re is REndOfFile) ? 1 : 0);
        int num4 = ((re is RStringLiteral) ? 1 : 0);
        int num5 = ((re.tpContext != null) ? 1 : 0);
        int num6 = ((num2 != 0 || num3 != 0 || num != 0 || (num4 == 0 && num5 != 0)) ? 1 : 0);
        if (num6 != 0)
        {
            text += ("<");
            if (num2 == 0)
            {
                if (re.private_rexp)
                {
                    text += ("#");
                }
                if (num != 0)
                {
                    text += (re.label);
                    text += (": ");
                }
            }
        }
        if (re is RCharacterList)
        {
            RCharacterList rCharacterList = (RCharacterList)re;
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
                    char[] value = new char[1] { character.ch };
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
        else if (re is RJustName)
        {
            RJustName rJustName = (RJustName)re;
            text += (rJustName.label);
        }
        else if (re is ROneOrMore)
        {
            ROneOrMore rOneOrMore = (ROneOrMore)re;
            text += ("(");
            text += (EmitRE(rOneOrMore.RegExpr));
            text += (")+");
        }
        else if (re is RSequence rSequence)
        {
            foreach (var regularExpression in rSequence.Units)
            {
                int num7 = 0;
                if (regularExpression is RChoice)
                {
                    num7 = 1;
                }
                if (num7 != 0)
                {
                    text += ("(");
                }
                text += (EmitRE(regularExpression));
                if (num7 != 0)
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
        else if (re is RStringLiteral)
        {
            RStringLiteral rStringLiteral = (RStringLiteral)re;
            text += ("\"") + (JavaCCGlobals.AddEscapes(rStringLiteral.image))
                + ("\"")
                ;
        }
        else if (re is RZeroOrMore)
        {
            RZeroOrMore rZeroOrMore = (RZeroOrMore)re;
            text += ("(");
            text += (EmitRE(rZeroOrMore.regexpr));
            text += (")*");
        }
        else if (re is RZeroOrOne)
        {
            RZeroOrOne rZeroOrOne = (RZeroOrOne)re;
            text += ("(");
            text += (EmitRE(rZeroOrOne.regexpr));
            text += (")?");
        }
        else
        {
            JJDocGlobals.Error("Oops: Unknown regular expression type.");
        }
        if (num6 != 0)
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
        int num = ((b.exp is Choice) ? 1 : 0);
        if (num != 0)
        {
            generator.Text("( ");
        }
        EmitExpansionTree(b.exp, generator);
        if (num != 0)
        {
            generator.Text(" )");
        }
    }


    private static void EmitExpansionZeroOrMore(ZeroOrMore z, Generator generator)
    {
        generator.Text("( ");
        EmitExpansionTree(z.expansion, generator);
        generator.Text(" )*");
    }


    private static void EmitExpansionZeroOrOne(ZeroOrOne o, Generator generator)
    {
        generator.Text("( ");
        EmitExpansionTree(o.expansion, generator);
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
