using org.javacc.parser;
using System.Collections;
using System.Text;

namespace org.javacc.jjdoc;


public class JJDoc : JJDocGlobals
{	
	private static void emitTokenProductions(Generator P_0, ArrayList P_1)
	{
		P_0.TokensStart();
		foreach(TokenProduction tokenProduction in P_1)
		{
			emitTopLevelSpecialTokens(tokenProduction.firstToken, P_0);
			string text = "";
			if (tokenProduction.isExplicit)
			{
				if (tokenProduction.lexStates == null)
				{
					text = new StringBuilder().Append(text).Append("<*> ").ToString();
				}
				else
				{
					text = new StringBuilder().Append(text).Append("<").ToString();
					for (int i = 0; i < (nint)tokenProduction.lexStates.LongLength; i++)
					{
						text = new StringBuilder().Append(text).Append(tokenProduction.lexStates[i]).ToString();
						if (i < (nint)tokenProduction.lexStates.LongLength - 1)
						{
							text = new StringBuilder().Append(text).Append(",").ToString();
						}
					}
					text = new StringBuilder().Append(text).Append("> ").ToString();
				}
				text = new StringBuilder().Append(text).Append(TokenProduction._KindImage[tokenProduction.kind]).ToString();
				if (tokenProduction.ignoreCase)
				{
					text = new StringBuilder().Append(text).Append(" [IGNORE_CASE]").ToString();
				}
				text = new StringBuilder().Append(text).Append(" : {\n").ToString();
				foreach(var regExprSpec in tokenProduction.respecs)
				{
					RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
					text = new StringBuilder().Append(text).Append(emitRE(regExprSpec.rexp)).ToString();
					if (regExprSpec.nsTok != null)
					{
						text = new StringBuilder().Append(text).Append(" : ").Append(regExprSpec.nsTok.image)
							.ToString();
					}
					text = new StringBuilder().Append(text).Append("\n").ToString();
					if (enumeration2.hasMoreElements())
					{
						text = new StringBuilder().Append(text).Append("| ").ToString();
					}
				}
				text = new StringBuilder().Append(text).Append("}\n\n").ToString();
			}
			if (!string.Equals(text, ""))
			{
				P_0.TokenStart(tokenProduction);
				P_0.Text(text);
				P_0.TokenEnd(tokenProduction);
			}
		}
		P_0.TokensEnd();
	}

	
	private static void emitNormalProductions(Generator P_0, ArrayList P_1)
	{
		P_0.NonterminalsStart();
		Enumeration enumeration = P_1.elements();
		while (enumeration.hasMoreElements())
		{
			NormalProduction normalProduction = (NormalProduction)enumeration.nextElement();
			emitTopLevelSpecialTokens(normalProduction.firstToken, P_0);
			if (normalProduction is BNFProduction)
			{
				P_0.ProductionStart(normalProduction);
				if (normalProduction.expansion is Choice)
				{
					int b = 1;
					Choice choice = (Choice)normalProduction.expansion;
					Enumeration enumeration2 = choice.choices.elements();
					while (enumeration2.hasMoreElements())
					{
						Expansion expansion = (Expansion)enumeration2.nextElement();
						P_0.ExpansionStart(expansion, (byte)b != 0);
						emitExpansionTree(expansion, P_0);
						P_0.ExpansionEnd(expansion, (byte)b != 0);
						b = 0;
					}
				}
				else
				{
					P_0.ExpansionStart(normalProduction.expansion, b: true);
					emitExpansionTree(normalProduction.expansion, P_0);
					P_0.ExpansionEnd(normalProduction.expansion, b: true);
				}
				P_0.ProductionEnd(normalProduction);
			}
			else if (normalProduction is JavaCodeProduction)
			{
				P_0.Javacode((JavaCodeProduction)normalProduction);
			}
		}
		P_0.NonterminalsEnd();
	}

	private static Token getPrecedingSpecialToken(Token P_0)
	{
		Token token = P_0;
		while (token.specialToken != null)
		{
			token = token.specialToken;
		}
		return (token == P_0) ? null : token;
	}

	
	private static void emitTopLevelSpecialTokens(Token P_0, Generator P_1)
	{
		if (P_0 == null)
		{
			return;
		}
		P_0 = getPrecedingSpecialToken(P_0);
		string text = "";
		if (P_0 != null)
		{
			JavaCCGlobals.cline = P_0.beginLine;
			JavaCCGlobals.ccol = P_0.beginColumn;
			while (P_0 != null)
			{
				text = new StringBuilder().Append(text).Append(JavaCCGlobals.printTokenOnly(P_0)).ToString();
				P_0 = P_0.next;
			}
		}
		if (!string.Equals(text, ""))
		{
			P_1.SpecialTokens(text);
		}
	}

	
	private static string emitRE(RegularExpression P_0)
	{
		string text = "";
		int num = ((!string.Equals(P_0.label, "")) ? 1 : 0);
		int num2 = ((P_0 is RJustName) ? 1 : 0);
		int num3 = ((P_0 is REndOfFile) ? 1 : 0);
		int num4 = ((P_0 is RStringLiteral) ? 1 : 0);
		int num5 = ((P_0.tpContext != null) ? 1 : 0);
		int num6 = ((num2 != 0 || num3 != 0 || num != 0 || (num4 == 0 && num5 != 0)) ? 1 : 0);
		if (num6 != 0)
		{
			text = new StringBuilder().Append(text).Append("<").ToString();
			if (num2 == 0)
			{
				if (P_0.private_rexp)
				{
					text = new StringBuilder().Append(text).Append("#").ToString();
				}
				if (num != 0)
				{
					text = new StringBuilder().Append(text).Append(P_0.label).ToString();
					text = new StringBuilder().Append(text).Append(": ").ToString();
				}
			}
		}
		if (P_0 is RCharacterList)
		{
			RCharacterList rCharacterList = (RCharacterList)P_0;
			if (rCharacterList.negated_list)
			{
				text = new StringBuilder().Append(text).Append("~").ToString();
			}
			text = new StringBuilder().Append(text).Append("[").ToString();
			Enumeration enumeration = rCharacterList.descriptors.elements();
			while (enumeration.hasMoreElements())
			{
				object obj = enumeration.nextElement();
				if (obj is SingleCharacter)
				{
					text = new StringBuilder().Append(text).Append("\"").ToString();
					char[] value = new char[1] { ((SingleCharacter)obj).ch };
					text = new StringBuilder().Append(text).Append(JavaCCGlobals.add_escapes(String.newhelper(value))).ToString();
					text = new StringBuilder().Append(text).Append("\"").ToString();
				}
				else if (obj is CharacterRange)
				{
					text = new StringBuilder().Append(text).Append("\"").ToString();
					char[] value = new char[1] { ((CharacterRange)obj).left };
					text = new StringBuilder().Append(text).Append(JavaCCGlobals.add_escapes(String.newhelper(value))).ToString();
					text = new StringBuilder().Append(text).Append("\"-\"").ToString();
					value[0] = ((CharacterRange)obj).right;
					text = new StringBuilder().Append(text).Append(JavaCCGlobals.add_escapes(String.newhelper(value))).ToString();
					text = new StringBuilder().Append(text).Append("\"").ToString();
				}
				else
				{
					JJDocGlobals.error("Oops: unknown character list element type.");
				}
				if (enumeration.hasMoreElements())
				{
					text = new StringBuilder().Append(text).Append(",").ToString();
				}
			}
			text = new StringBuilder().Append(text).Append("]").ToString();
		}
		else if (P_0 is RChoice)
		{
			RChoice rChoice = (RChoice)P_0;
			Enumeration enumeration = rChoice.choices.elements();
			while (enumeration.hasMoreElements())
			{
				RegularExpression regularExpression = (RegularExpression)enumeration.nextElement();
				text = new StringBuilder().Append(text).Append(emitRE(regularExpression)).ToString();
				if (enumeration.hasMoreElements())
				{
					text = new StringBuilder().Append(text).Append(" | ").ToString();
				}
			}
		}
		else if (P_0 is REndOfFile)
		{
			text = new StringBuilder().Append(text).Append("EOF").ToString();
		}
		else if (P_0 is RJustName)
		{
			RJustName rJustName = (RJustName)P_0;
			text = new StringBuilder().Append(text).Append(rJustName.label).ToString();
		}
		else if (P_0 is ROneOrMore)
		{
			ROneOrMore rOneOrMore = (ROneOrMore)P_0;
			text = new StringBuilder().Append(text).Append("(").ToString();
			text = new StringBuilder().Append(text).Append(emitRE(rOneOrMore.regexpr)).ToString();
			text = new StringBuilder().Append(text).Append(")+").ToString();
		}
		else if (P_0 is RSequence)
		{
			RSequence rSequence = (RSequence)P_0;
			Enumeration enumeration = rSequence.units.elements();
			while (enumeration.hasMoreElements())
			{
				RegularExpression regularExpression = (RegularExpression)enumeration.nextElement();
				int num7 = 0;
				if (regularExpression is RChoice)
				{
					num7 = 1;
				}
				if (num7 != 0)
				{
					text = new StringBuilder().Append(text).Append("(").ToString();
				}
				text = new StringBuilder().Append(text).Append(emitRE(regularExpression)).ToString();
				if (num7 != 0)
				{
					text = new StringBuilder().Append(text).Append(")").ToString();
				}
				if (enumeration.hasMoreElements())
				{
					text = new StringBuilder().Append(text).Append(" ").ToString();
				}
			}
		}
		else if (P_0 is RStringLiteral)
		{
			RStringLiteral rStringLiteral = (RStringLiteral)P_0;
			text = new StringBuilder().Append(text).Append("\"").Append(JavaCCGlobals.add_escapes(rStringLiteral.image))
				.Append("\"")
				.ToString();
		}
		else if (P_0 is RZeroOrMore)
		{
			RZeroOrMore rZeroOrMore = (RZeroOrMore)P_0;
			text = new StringBuilder().Append(text).Append("(").ToString();
			text = new StringBuilder().Append(text).Append(emitRE(rZeroOrMore.regexpr)).ToString();
			text = new StringBuilder().Append(text).Append(")*").ToString();
		}
		else if (P_0 is RZeroOrOne)
		{
			RZeroOrOne rZeroOrOne = (RZeroOrOne)P_0;
			text = new StringBuilder().Append(text).Append("(").ToString();
			text = new StringBuilder().Append(text).Append(emitRE(rZeroOrOne.regexpr)).ToString();
			text = new StringBuilder().Append(text).Append(")?").ToString();
		}
		else
		{
			JJDocGlobals.error("Oops: Unknown regular expression type.");
		}
		if (num6 != 0)
		{
			text = new StringBuilder().Append(text).Append(">").ToString();
		}
		return text;
	}

	
	private static void emitExpansionTree(Expansion P_0, Generator P_1)
	{
		if (P_0 is Action)
		{
			emitExpansionAction((Action)P_0, P_1);
		}
		else if (P_0 is Choice)
		{
			emitExpansionChoice((Choice)P_0, P_1);
		}
		else if (P_0 is Lookahead)
		{
			emitExpansionLookahead((Lookahead)P_0, P_1);
		}
		else if (P_0 is NonTerminal)
		{
			emitExpansionNonTerminal((NonTerminal)P_0, P_1);
		}
		else if (P_0 is OneOrMore)
		{
			emitExpansionOneOrMore((OneOrMore)P_0, P_1);
		}
		else if (P_0 is RegularExpression)
		{
			emitExpansionRegularExpression((RegularExpression)P_0, P_1);
		}
		else if (P_0 is Sequence)
		{
			emitExpansionSequence((Sequence)P_0, P_1);
		}
		else if (P_0 is TryBlock)
		{
			emitExpansionTryBlock((TryBlock)P_0, P_1);
		}
		else if (P_0 is ZeroOrMore)
		{
			emitExpansionZeroOrMore((ZeroOrMore)P_0, P_1);
		}
		else if (P_0 is ZeroOrOne)
		{
			emitExpansionZeroOrOne((ZeroOrOne)P_0, P_1);
		}
		else
		{
			JJDocGlobals.error("Oops: Unknown expansion type.");
		}
	}

	private static void emitExpansionAction(Action P_0, Generator P_1)
	{
	}

	
	private static void emitExpansionChoice(Choice P_0, Generator P_1)
	{
		Enumeration enumeration = P_0.choices.elements();
		while (enumeration.hasMoreElements())
		{
			Expansion expansion = (Expansion)enumeration.nextElement();
			emitExpansionTree(expansion, P_1);
			if (enumeration.hasMoreElements())
			{
				P_1.Text(" | ");
			}
		}
	}

	private static void emitExpansionLookahead(Lookahead P_0, Generator P_1)
	{
	}

	
	private static void emitExpansionNonTerminal(NonTerminal P_0, Generator P_1)
	{
		P_1.NonTerminalStart(P_0);
		P_1.Text(P_0.name);
		P_1.NonTerminalEnd(P_0);
	}

	
	private static void emitExpansionOneOrMore(OneOrMore P_0, Generator P_1)
	{
		P_1.Text("( ");
		emitExpansionTree(P_0.expansion, P_1);
		P_1.Text(" )+");
	}

	
	private static void emitExpansionRegularExpression(RegularExpression P_0, Generator P_1)
	{
		string text = emitRE(P_0);
		if (!string.Equals(text, ""))
		{
			P_1.ReStart(P_0);
			P_1.Text(text);
			P_1.ReEnd(P_0);
		}
	}

	
	private static void emitExpansionSequence(Sequence P_0, Generator P_1)
	{
		int num = 1;
		Enumeration enumeration = P_0.units.elements();
		while (enumeration.hasMoreElements())
		{
			Expansion expansion = (Expansion)enumeration.nextElement();
			if (!(expansion is Lookahead) && !(expansion is Action))
			{
				if (num == 0)
				{
					P_1.Text(" ");
				}
				int num2 = ((expansion is Choice || expansion is Sequence) ? 1 : 0);
				if (num2 != 0)
				{
					P_1.Text("( ");
				}
				emitExpansionTree(expansion, P_1);
				if (num2 != 0)
				{
					P_1.Text(" )");
				}
				num = 0;
			}
		}
	}

	
	private static void emitExpansionTryBlock(TryBlock P_0, Generator P_1)
	{
		int num = ((P_0.exp is Choice) ? 1 : 0);
		if (num != 0)
		{
			P_1.Text("( ");
		}
		emitExpansionTree(P_0.exp, P_1);
		if (num != 0)
		{
			P_1.Text(" )");
		}
	}

	
	private static void emitExpansionZeroOrMore(ZeroOrMore P_0, Generator P_1)
	{
		P_1.Text("( ");
		emitExpansionTree(P_0.expansion, P_1);
		P_1.Text(" )*");
	}

	
	private static void emitExpansionZeroOrOne(ZeroOrOne P_0, Generator P_1)
	{
		P_1.Text("( ");
		emitExpansionTree(P_0.expansion, P_1);
		P_1.Text(" )?");
	}

	
	public JJDoc()
	{
	}

	
	internal static void start()
	{
		JJDocGlobals.generator = JJDocGlobals.GetGenerator();
		JJDocGlobals.generator.DocumentStart();
		emitTokenProductions(JJDocGlobals.generator, JavaCCGlobals.rexprlist);
		emitNormalProductions(JJDocGlobals.generator, JavaCCGlobals.bnfproductions);
		JJDocGlobals.generator.DocumentEnd();
	}

}
