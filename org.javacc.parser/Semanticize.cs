


using System.Text;


namespace org.javacc.parser;


public class Semanticize : JavaCCGlobals
{

	internal class EmptyChecker : JavaCCGlobals, TreeWalkerOp
	{

		internal EmptyChecker()
		{
		}

		public virtual bool GoDeeper(Expansion P_0)
		{
			if (P_0 is RegularExpression)
			{
				return false;
			}
			return true;
		}


		public virtual void Action(Expansion P_0)
		{
			if (P_0 is OneOrMore)
			{
				if (emptyExpansionExists(((OneOrMore)P_0).expansion))
				{
					JavaCCErrors.Semantic_Error(P_0, "Expansion within \"(...)+\" can be matched by empty string.");
				}
			}
			else if (P_0 is ZeroOrMore)
			{
				if (emptyExpansionExists(((ZeroOrMore)P_0).expansion))
				{
					JavaCCErrors.Semantic_Error(P_0, "Expansion within \"(...)*\" can be matched by empty string.");
				}
			}
			else if (P_0 is ZeroOrOne && emptyExpansionExists(((ZeroOrOne)P_0).expansion))
			{
				JavaCCErrors.Semantic_Error(P_0, "Expansion within \"(...)?\" can be matched by empty string.");
			}
		}
	}


	internal class FixRJustNames : JavaCCGlobals, TreeWalkerOp
	{
		public RegularExpression root;

		internal FixRJustNames()
		{
		}

		public virtual bool GoDeeper(Expansion P_0)
		{
			return true;
		}


		public virtual void Action(Expansion P_0)
		{
			if (P_0 is RJustName)
			{
				RJustName rJustName = (RJustName)P_0;
				RegularExpression regularExpression = (RegularExpression)JavaCCGlobals.named_tokens_table.get(rJustName.label);
				if (regularExpression == null)
				{
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Undefined lexical token name \"").Append(rJustName.label).Append("\".")
						.ToString());
					return;
				}
				if (rJustName == root && !rJustName.tpContext.isExplicit && regularExpression.private_rexp)
				{
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Token name \"").Append(rJustName.label).Append("\" refers to a private ")
						.Append("(with a #) regular expression.")
						.ToString());
					return;
				}
				if (rJustName == root && !rJustName.tpContext.isExplicit && regularExpression.tpContext.kind != 0)
				{
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Token name \"").Append(rJustName.label).Append("\" refers to a non-token ")
						.Append("(SKIP, MORE, IGNORE_IN_BNF) regular expression.")
						.ToString());
					return;
				}
				rJustName.ordinal = regularExpression.ordinal;
				rJustName.regexpr = regularExpression;
			}
		}

	}

	
		internal class LookaheadChecker : JavaCCGlobals, TreeWalkerOp
	{
		
		internal LookaheadChecker()
		{
		}

		
		internal static bool implicitLA(Expansion P_0)
		{
			if (!(P_0 is Sequence))
			{
				return true;
			}
			Sequence sequence = (Sequence)P_0;
			object obj = sequence.units[0];
			if (!(obj is Lookahead))
			{
				return true;
			}
			Lookahead lookahead = (Lookahead)obj;
			return (!lookahead.isExplicit) ? true : false;
		}

		public virtual bool GoDeeper(Expansion P_0)
		{
			if (P_0 is RegularExpression)
			{
				return false;
			}
			if (P_0 is Lookahead)
			{
				return false;
			}
			return true;
		}

		
		public virtual void Action(Expansion P_0)
		{
			if (P_0 is Choice)
			{
				if (Options.getLookahead() == 1 || Options.getForceLaCheck())
				{
					LookaheadCalc.choiceCalc((Choice)P_0);
				}
			}
			else if (P_0 is OneOrMore)
			{
				OneOrMore oneOrMore = (OneOrMore)P_0;
				if (Options.getForceLaCheck() || (implicitLA(oneOrMore.expansion) && Options.getLookahead() == 1))
				{
					LookaheadCalc.ebnfCalc(oneOrMore, oneOrMore.expansion);
				}
			}
			else if (P_0 is ZeroOrMore)
			{
				ZeroOrMore zeroOrMore = (ZeroOrMore)P_0;
				if (Options.getForceLaCheck() || (implicitLA(zeroOrMore.expansion) && Options.getLookahead() == 1))
				{
					LookaheadCalc.ebnfCalc(zeroOrMore, zeroOrMore.expansion);
				}
			}
			else if (P_0 is ZeroOrOne)
			{
				ZeroOrOne zeroOrOne = (ZeroOrOne)P_0;
				if (Options.getForceLaCheck() || (implicitLA(zeroOrOne.expansion) && Options.getLookahead() == 1))
				{
					LookaheadCalc.ebnfCalc(zeroOrOne, zeroOrOne.expansion);
				}
			}
		}

		
		static LookaheadChecker()
		{
			
		}
	}

	
		internal class LookaheadFixer : JavaCCGlobals, TreeWalkerOp
	{
		
		
		public new static void ___003Cclinit_003E()
		{
		}

		
		internal LookaheadFixer()
		{
		}

		public virtual bool GoDeeper(Expansion P_0)
		{
			if (P_0 is RegularExpression)
			{
				return false;
			}
			return true;
		}

		
		public virtual void Action(Expansion P_0)
		{
			if (!(P_0 is Sequence) || P_0.parent is Choice || P_0.parent is ZeroOrMore || P_0.parent is OneOrMore || P_0.parent is ZeroOrOne)
			{
				return;
			}
			Sequence sequence = (Sequence)P_0;
			Lookahead lookahead = (Lookahead)sequence.units[0];
			if (!lookahead.isExplicit)
			{
				return;
			}
			Choice choice = new Choice();
			choice.line = lookahead.line;
			choice.column = lookahead.column;
			choice.parent = sequence;
			Sequence sequence2 = new Sequence();
			sequence2.line = lookahead.line;
			sequence2.column = lookahead.column;
			sequence2.parent = choice;
			sequence2.units.Add(lookahead);
			lookahead.parent = sequence2;
			Action action = new Action();
			action.line = lookahead.line;
			action.column = lookahead.column;
			action.parent = sequence2;
			sequence2.units.Add(action);
			choice.choices.Add(sequence2);
			if (lookahead.amount != 0)
			{
				if (lookahead.action_tokens.Count != 0)
				{
					JavaCCErrors.Warning(lookahead, "Encountered LOOKAHEAD(...) at a non-choice location.  Only semantic lookahead will be considered here.");
				}
				else
				{
					JavaCCErrors.Warning(lookahead, "Encountered LOOKAHEAD(...) at a non-choice location.  This will be ignored.");
				}
			}
			Lookahead lookahead2 = new Lookahead();
			lookahead2.isExplicit = false;
			lookahead2.line = lookahead.line;
			lookahead2.column = lookahead.column;
			lookahead2.parent = sequence;
			lookahead.la_expansion = new REndOfFile();
			lookahead2.la_expansion = new REndOfFile();
			sequence.units.setElementAt(lookahead2, 0);
			sequence.units.insertElementAt(choice, 1);
		}

		
		static LookaheadFixer()
		{
			
		}
	}

	
		internal class ProductionDefinedChecker : JavaCCGlobals, TreeWalkerOp
	{
		
		
		public new static void ___003Cclinit_003E()
		{
		}

		
		internal ProductionDefinedChecker()
		{
		}

		public virtual bool GoDeeper(Expansion P_0)
		{
			if (P_0 is RegularExpression)
			{
				return false;
			}
			return true;
		}

		
		public virtual void Action(Expansion P_0)
		{
			if (P_0 is NonTerminal)
			{
				NonTerminal nonTerminal = (NonTerminal)P_0;
				NormalProduction normalProduction = (NormalProduction)JavaCCGlobals.production_table.get(nonTerminal.name);
				NonTerminal nonTerminal2 = nonTerminal;
				nonTerminal2.prod = normalProduction;
				if (normalProduction == null)
				{
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Non-terminal ").Append(nonTerminal.name).Append(" has not been defined.")
						.ToString());
				}
				else
				{
					nonTerminal.prod.parents.Add(nonTerminal);
				}
			}
		}

		
		static ProductionDefinedChecker()
		{
			
		}
	}

	internal static ArrayList removeList;

	internal static ArrayList itemList;

	public static RegularExpression other;

	private static string loopString;

	
	
	public new static void ___003Cclinit_003E()
	{
	}

	
	public static bool emptyExpansionExists(Expansion e)
	{
		if (e is NonTerminal)
		{
			return ((NonTerminal)e).prod.emptyPossible;
		}
		if (e is Action)
		{
			return true;
		}
		if (e is RegularExpression)
		{
			return false;
		}
		if (e is OneOrMore)
		{
			bool result = emptyExpansionExists(((OneOrMore)e).expansion);
			
			return result;
		}
		if (e is ZeroOrMore || e is ZeroOrOne)
		{
			return true;
		}
		if (e is Lookahead)
		{
			return true;
		}
		if (e is Choice)
		{
			Enumeration enumeration = ((Choice)e).choices.elements();
			while (enumeration.hasMoreElements())
			{
				if (emptyExpansionExists((Expansion)enumeration.nextElement()))
				{
					return true;
				}
			}
			return false;
		}
		if (e is Sequence)
		{
			Enumeration enumeration = ((Sequence)e).units.elements();
			while (enumeration.hasMoreElements())
			{
				if (!emptyExpansionExists((Expansion)enumeration.nextElement()))
				{
					return false;
				}
			}
			return true;
		}
		if (e is TryBlock)
		{
			bool result2 = emptyExpansionExists(((TryBlock)e).exp);
			
			return result2;
		}
		return false;
	}

	
		public static void start()
	{
		if (JavaCCErrors.Get_Error_Count() != 0)
		{
			
			throw new MetaParseException();
		}
		if (Options.getLookahead() > 1 && !Options.getForceLaCheck() && Options.getSanityCheck())
		{
			JavaCCErrors.Warning("Lookahead adequacy checking not being performed since option LOOKAHEAD is more than 1.  HashSet<object> option FORCE_LA_CHECK to true to force checking.");
		}
		Enumeration enumeration = JavaCCGlobals.bnfproductions.elements();
		while (enumeration.hasMoreElements())
		{
			ExpansionTreeWalker.postOrderWalk(((NormalProduction)enumeration.nextElement()).expansion, new LookaheadFixer());
		}
		enumeration = JavaCCGlobals.bnfproductions.elements();
		while (enumeration.hasMoreElements())
		{
			NormalProduction normalProduction = (NormalProduction)enumeration.nextElement();
			if (JavaCCGlobals.production_table.Add(normalProduction.lhs, normalProduction) != null)
			{
				JavaCCErrors.Semantic_Error(normalProduction, new StringBuilder().Append(normalProduction.lhs).Append(" occurs on the left hand side of more than one production.").ToString());
			}
		}
		enumeration = JavaCCGlobals.bnfproductions.elements();
		while (enumeration.hasMoreElements())
		{
			ExpansionTreeWalker.preOrderWalk(((NormalProduction)enumeration.nextElement()).expansion, new ProductionDefinedChecker());
		}
		enumeration = JavaCCGlobals.rexprlist.elements();
		while (enumeration.hasMoreElements())
		{
			TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
			ArrayList respecs = tokenProduction.respecs;
			Enumeration enumeration2 = respecs.elements();
			while (enumeration2.hasMoreElements())
			{
				RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
				if (regExprSpec.nextState != null && JavaCCGlobals.lexstate_S2I.get(regExprSpec.nextState) == null)
				{
					JavaCCErrors.Semantic_Error(regExprSpec.nsTok, new StringBuilder().Append("Lexical state \"").Append(regExprSpec.nextState).Append("\" has not been defined.")
						.ToString());
				}
				if (regExprSpec.rexp is REndOfFile)
				{
					if (tokenProduction.lexStates != null)
					{
						JavaCCErrors.Semantic_Error(regExprSpec.rexp, "EOF action/state change must be specified for all states, i.e., <*>TOKEN:.");
					}
					if (tokenProduction.kind != 0)
					{
						JavaCCErrors.Semantic_Error(regExprSpec.rexp, "EOF action/state change can be specified only in a TOKEN specification.");
					}
					if (JavaCCGlobals.nextStateForEof != null || JavaCCGlobals.actForEof != null)
					{
						JavaCCErrors.Semantic_Error(regExprSpec.rexp, "Duplicate action/state change specification for <EOF>.");
					}
					JavaCCGlobals.actForEof = regExprSpec.act;
					JavaCCGlobals.nextStateForEof = regExprSpec.nextState;
					prepareToRemove(respecs, regExprSpec);
				}
				else if (tokenProduction.isExplicit && Options.getUserTokenManager())
				{
					JavaCCErrors.Warning(regExprSpec.rexp, "Ignoring regular expression specification since option USER_TOKEN_MANAGER has been set to true.");
				}
				else if (tokenProduction.isExplicit && !Options.getUserTokenManager() && regExprSpec.rexp is RJustName)
				{
					JavaCCErrors.Warning(regExprSpec.rexp, new StringBuilder().Append("Ignoring free-standing regular expression reference.  If you really want this, you must give it a different label as <NEWLABEL:<").Append(regExprSpec.rexp.label).Append(">>.")
						.ToString());
					prepareToRemove(respecs, regExprSpec);
				}
				else if (!tokenProduction.isExplicit && regExprSpec.rexp.private_rexp)
				{
					JavaCCErrors.Semantic_Error(regExprSpec.rexp, "Private (#) regular expression cannot be defined within grammar productions.");
				}
			}
		}
		removePreparedItems();
		enumeration = JavaCCGlobals.rexprlist.elements();
		while (enumeration.hasMoreElements())
		{
			TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
			ArrayList respecs = tokenProduction.respecs;
			Enumeration enumeration2 = respecs.elements();
			while (enumeration2.hasMoreElements())
			{
				RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
				if (!(regExprSpec.rexp is RJustName) && !string.Equals(regExprSpec.rexp.label, ""))
				{
					string label = regExprSpec.rexp.label;
					object obj = JavaCCGlobals.named_tokens_table.Add(label, regExprSpec.rexp);
					if (obj != null)
					{
						JavaCCErrors.Semantic_Error(regExprSpec.rexp, new StringBuilder().Append("Multiply defined lexical token name \"").Append(label).Append("\".")
							.ToString());
					}
					else
					{
						JavaCCGlobals.ordered_named_tokens.Add(regExprSpec.rexp);
					}
					if (JavaCCGlobals.lexstate_S2I.get(label) != null)
					{
						JavaCCErrors.Semantic_Error(regExprSpec.rexp, new StringBuilder().Append("Lexical token name \"").Append(label).Append("\" is the same as ")
							.Append("that of a lexical state.")
							.ToString());
					}
				}
			}
		}
		JavaCCGlobals.tokenCount = 1;
		enumeration = JavaCCGlobals.rexprlist.elements();
		while (enumeration.hasMoreElements())
		{
			TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
			ArrayList respecs = tokenProduction.respecs;
			Enumeration enumeration3;
			if (tokenProduction.lexStates == null)
			{
				tokenProduction.lexStates = new string[JavaCCGlobals.lexstate_I2S.Count];
				int num = 0;
				enumeration3 = JavaCCGlobals.lexstate_I2S.elements();
				while (enumeration3.hasMoreElements())
				{
					string[] lexStates = tokenProduction.lexStates;
					int num2 = num;
					num++;
					lexStates[num2] = (string)enumeration3.nextElement();
				}
			}
			Hashtable[] array = new Hashtable[(nint)tokenProduction.lexStates.LongLength];
			for (int i = 0; i < (nint)tokenProduction.lexStates.LongLength; i++)
			{
				array[i] = (Hashtable)JavaCCGlobals.simple_tokens_table.get(tokenProduction.lexStates[i]);
			}
			enumeration3 = respecs.elements();
			while (enumeration3.hasMoreElements())
			{
				RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
				if (regExprSpec2.rexp is RStringLiteral)
				{
					RStringLiteral rStringLiteral = (RStringLiteral)regExprSpec2.rexp;
					for (int j = 0; j < (nint)array.LongLength; j++)
					{
						Hashtable hashtable = (Hashtable)array[j].get(String.instancehelper_toUpperCase(rStringLiteral.image));
						if (hashtable == null)
						{
							if (rStringLiteral.ordinal == 0)
							{
								rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
							}
							hashtable = new Hashtable();
							hashtable.Add(rStringLiteral.image, rStringLiteral);
							array[j].Add(String.instancehelper_toUpperCase(rStringLiteral.image), hashtable);
							continue;
						}
						if (hasIgnoreCase(hashtable, rStringLiteral.image))
						{
							if (!rStringLiteral.tpContext.isExplicit)
							{
								JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("String \"").Append(rStringLiteral.image).Append("\" can never be matched ")
									.Append("due to presence of more general (IGNORE_CASE) regular expression ")
									.Append("at line ")
									.Append(other.line)
									.Append(", column ")
									.Append(other.column)
									.Append(".")
									.ToString());
							}
							else
							{
								JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("Duplicate definition of string token \"").Append(rStringLiteral.image).Append("\" ")
									.Append("can never be matched.")
									.ToString());
							}
							continue;
						}
						if (rStringLiteral.tpContext.ignoreCase)
						{
							string str = "";
							int num3 = 0;
							Enumeration enumeration4 = hashtable.elements();
							while (enumeration4.hasMoreElements())
							{
								RegularExpression regularExpression = (RegularExpression)enumeration4.nextElement();
								if (num3 != 0)
								{
									str = new StringBuilder().Append(str).Append(",").ToString();
								}
								str = new StringBuilder().Append(str).Append(" line ").Append(regularExpression.line)
									.ToString();
								num3++;
							}
							if (num3 == 1)
							{
								JavaCCErrors.Warning(rStringLiteral, new StringBuilder().Append("String with IGNORE_CASE is partially superceded by string at").Append(str).Append(".")
									.ToString());
							}
							else
							{
								JavaCCErrors.Warning(rStringLiteral, new StringBuilder().Append("String with IGNORE_CASE is partially superceded by strings at").Append(str).Append(".")
									.ToString());
							}
							if (rStringLiteral.ordinal == 0)
							{
								rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
							}
							hashtable.Add(rStringLiteral.image, rStringLiteral);
							continue;
						}
						RegularExpression regularExpression2 = (RegularExpression)hashtable.get(rStringLiteral.image);
						if (regularExpression2 == null)
						{
							if (rStringLiteral.ordinal == 0)
							{
								rStringLiteral.ordinal = JavaCCGlobals.tokenCount++;
							}
							hashtable.Add(rStringLiteral.image, rStringLiteral);
						}
						else if (tokenProduction.isExplicit)
						{
							if (string.Equals(tokenProduction.lexStates[j], "DEFAULT"))
							{
								JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("Duplicate definition of string token \"").Append(rStringLiteral.image).Append("\".")
									.ToString());
							}
							else
							{
								JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("Duplicate definition of string token \"").Append(rStringLiteral.image).Append("\" in lexical state \"")
									.Append(tokenProduction.lexStates[j])
									.Append("\".")
									.ToString());
							}
						}
						else if (regularExpression2.tpContext.kind != 0)
						{
							JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("String token \"").Append(rStringLiteral.image).Append("\" has been defined as a \"")
								.Append(TokenProduction._KindImage[regularExpression2.tpContext.kind])
								.Append("\" token.")
								.ToString());
						}
						else if (regularExpression2.private_rexp)
						{
							JavaCCErrors.Semantic_Error(rStringLiteral, new StringBuilder().Append("String token \"").Append(rStringLiteral.image).Append("\" has been defined as a private regular expression.")
								.ToString());
						}
						else
						{
							rStringLiteral.ordinal = regularExpression2.ordinal;
							prepareToRemove(respecs, regExprSpec2);
						}
					}
				}
				else if (!(regExprSpec2.rexp is RJustName))
				{
					regExprSpec2.rexp.ordinal = JavaCCGlobals.tokenCount++;
				}
				if (!(regExprSpec2.rexp is RJustName) && !string.Equals(regExprSpec2.rexp.label, ""))
				{
					Hashtable hashtable2 = JavaCCGlobals.names_of_tokens;
					;
					hashtable2.Add(new int(regExprSpec2.rexp.ordinal), regExprSpec2.rexp.label);
				}
				if (!(regExprSpec2.rexp is RJustName))
				{
					Hashtable hashtable3 = JavaCCGlobals.rexps_of_tokens;
					;
					hashtable3.Add(new int(regExprSpec2.rexp.ordinal), regExprSpec2.rexp);
				}
			}
		}
		removePreparedItems();
		if (!Options.getUserTokenManager())
		{
			FixRJustNames fixRJustNames = new FixRJustNames();
			Enumeration enumeration5 = JavaCCGlobals.rexprlist.elements();
			while (enumeration5.hasMoreElements())
			{
				TokenProduction tokenProduction2 = (TokenProduction)enumeration5.nextElement();
				ArrayList respecs2 = tokenProduction2.respecs;
				Enumeration enumeration3 = respecs2.elements();
				while (enumeration3.hasMoreElements())
				{
					RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
					fixRJustNames.root = regExprSpec2.rexp;
					ExpansionTreeWalker.preOrderWalk(regExprSpec2.rexp, fixRJustNames);
					if (regExprSpec2.rexp is RJustName)
					{
						prepareToRemove(respecs2, regExprSpec2);
					}
				}
			}
		}
		removePreparedItems();
		if (Options.getUserTokenManager())
		{
			enumeration = JavaCCGlobals.rexprlist.elements();
			while (enumeration.hasMoreElements())
			{
				TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
				ArrayList respecs = tokenProduction.respecs;
				Enumeration enumeration2 = respecs.elements();
				while (enumeration2.hasMoreElements())
				{
					RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
					if (regExprSpec.rexp is RJustName)
					{
						RJustName rJustName = (RJustName)regExprSpec.rexp;
						RegularExpression regularExpression3 = (RegularExpression)JavaCCGlobals.named_tokens_table.get(rJustName.label);
						if (regularExpression3 == null)
						{
							rJustName.ordinal = JavaCCGlobals.tokenCount++;
							JavaCCGlobals.named_tokens_table.Add(rJustName.label, rJustName);
							JavaCCGlobals.ordered_named_tokens.Add(rJustName);
							Hashtable hashtable4 = JavaCCGlobals.names_of_tokens;
							;
							hashtable4.Add(new int(rJustName.ordinal), rJustName.label);
						}
						else
						{
							rJustName.ordinal = regularExpression3.ordinal;
							prepareToRemove(respecs, regExprSpec);
						}
					}
				}
			}
		}
		removePreparedItems();
		if (Options.getUserTokenManager())
		{
			enumeration = JavaCCGlobals.rexprlist.elements();
			while (enumeration.hasMoreElements())
			{
				TokenProduction tokenProduction = (TokenProduction)enumeration.nextElement();
				ArrayList respecs = tokenProduction.respecs;
				Enumeration enumeration2 = respecs.elements();
				while (enumeration2.hasMoreElements())
				{
					RegExprSpec regExprSpec = (RegExprSpec)enumeration2.nextElement();
					;
					int key = new int(regExprSpec.rexp.ordinal);
					if (JavaCCGlobals.names_of_tokens.get(key) == null)
					{
						JavaCCErrors.Warning(regExprSpec.rexp, "Unlabeled regular expression cannot be referred to by user generated token manager.");
					}
				}
			}
		}
		if (JavaCCErrors.Get_Error_Count() != 0)
		{
			
			throw new MetaParseException();
		}
		int num4 = 1;
		while (num4 != 0)
		{
			num4 = 0;
			Enumeration enumeration5 = JavaCCGlobals.bnfproductions.elements();
			while (enumeration5.hasMoreElements())
			{
				NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
				if (emptyExpansionExists(normalProduction2.expansion) && !normalProduction2.emptyPossible)
				{
					NormalProduction normalProduction3 = normalProduction2;
					int num5 = 1;
					NormalProduction normalProduction4 = normalProduction3;
					normalProduction4.emptyPossible = (byte)num5 != 0;
					num4 = num5;
				}
			}
		}
		if (Options.getSanityCheck() && JavaCCErrors.Get_Error_Count() == 0)
		{
			Enumeration enumeration5 = JavaCCGlobals.bnfproductions.elements();
			while (enumeration5.hasMoreElements())
			{
				ExpansionTreeWalker.preOrderWalk(((NormalProduction)enumeration5.nextElement()).expansion, new EmptyChecker());
			}
			enumeration5 = JavaCCGlobals.bnfproductions.elements();
			while (enumeration5.hasMoreElements())
			{
				NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
				addLeftMost(normalProduction2, normalProduction2.expansion);
			}
			enumeration5 = JavaCCGlobals.bnfproductions.elements();
			while (enumeration5.hasMoreElements())
			{
				NormalProduction normalProduction2 = (NormalProduction)enumeration5.nextElement();
				if (normalProduction2.walkStatus == 0)
				{
					prodWalk(normalProduction2);
				}
			}
			if (!Options.getUserTokenManager())
			{
				enumeration5 = JavaCCGlobals.rexprlist.elements();
				while (enumeration5.hasMoreElements())
				{
					TokenProduction tokenProduction2 = (TokenProduction)enumeration5.nextElement();
					ArrayList respecs2 = tokenProduction2.respecs;
					Enumeration enumeration3 = respecs2.elements();
					while (enumeration3.hasMoreElements())
					{
						RegExprSpec regExprSpec2 = (RegExprSpec)enumeration3.nextElement();
						RegularExpression regularExpression3 = regExprSpec2.rexp;
						if (regularExpression3.walkStatus == 0)
						{
							regularExpression3.walkStatus = -1;
							if (rexpWalk(regularExpression3))
							{
								loopString = new StringBuilder().Append("...").Append(regularExpression3.label).Append("... --> ")
									.Append(loopString)
									.ToString();
								JavaCCErrors.Semantic_Error(regularExpression3, new StringBuilder().Append("Loop in regular expression detected: \"").Append(loopString).Append("\"")
									.ToString());
							}
							regularExpression3.walkStatus = 1;
						}
					}
				}
			}
			if (JavaCCErrors.Get_Error_Count() == 0)
			{
				enumeration5 = JavaCCGlobals.bnfproductions.elements();
				while (enumeration5.hasMoreElements())
				{
					ExpansionTreeWalker.preOrderWalk(((NormalProduction)enumeration5.nextElement()).expansion, new LookaheadChecker());
				}
			}
		}
		if (JavaCCErrors.Get_Error_Count() != 0)
		{
			
			throw new MetaParseException();
		}
	}

	
	public new static void reInit()
	{
		removeList = new ArrayList();
		itemList = new ArrayList();
		other = null;
		loopString = null;
	}

	
	internal static void prepareToRemove(ArrayList P_0, object P_1)
	{
		removeList.Add(P_0);
		itemList.Add(P_1);
	}

	
	internal static void removePreparedItems()
	{
		for (int i = 0; i < removeList.Count; i++)
		{
			ArrayList vector = (ArrayList)removeList[i];
			vector.removeElement(itemList[i]);
		}
		removeList.removeAllElements();
		itemList.removeAllElements();
	}

	
	public static bool hasIgnoreCase(Hashtable h, string str)
	{
		RegularExpression regularExpression = (RegularExpression)h.get(str);
		if (regularExpression != null && !regularExpression.tpContext.ignoreCase)
		{
			return false;
		}
		Enumeration enumeration = h.elements();
		while (enumeration.hasMoreElements())
		{
			regularExpression = (RegularExpression)enumeration.nextElement();
			if (regularExpression.tpContext.ignoreCase)
			{
				other = regularExpression;
				return true;
			}
		}
		return false;
	}

	
	private static void addLeftMost(NormalProduction P_0, Expansion P_1)
	{
		if (P_1 is NonTerminal)
		{
			for (int i = 0; i < P_0.leIndex; i++)
			{
				if (P_0.leftExpansions[i] == ((NonTerminal)P_1).prod)
				{
					return;
				}
			}
			if ((nint)P_0.leIndex == (nint)P_0.leftExpansions.LongLength)
			{
				NormalProduction[] array = new NormalProduction[P_0.leIndex * 2];
				Array.Copy(P_0.leftExpansions, 0, array, 0, P_0.leIndex);
				P_0.leftExpansions = array;
			}
			NormalProduction[] leftExpansions = P_0.leftExpansions;
			int leIndex = P_0.leIndex;
			P_0.leIndex = leIndex + 1;
			leftExpansions[leIndex] = ((NonTerminal)P_1).prod;
		}
		else if (P_1 is OneOrMore)
		{
			addLeftMost(P_0, ((OneOrMore)P_1).expansion);
		}
		else if (P_1 is ZeroOrMore)
		{
			addLeftMost(P_0, ((ZeroOrMore)P_1).expansion);
		}
		else if (P_1 is ZeroOrOne)
		{
			addLeftMost(P_0, ((ZeroOrOne)P_1).expansion);
		}
		else if (P_1 is Choice)
		{
			Enumeration enumeration = ((Choice)P_1).choices.elements();
			while (enumeration.hasMoreElements())
			{
				addLeftMost(P_0, (Expansion)enumeration.nextElement());
			}
		}
		else if (P_1 is Sequence)
		{
			Enumeration enumeration = ((Sequence)P_1).units.elements();
			while (enumeration.hasMoreElements())
			{
				Expansion expansion = (Expansion)enumeration.nextElement();
				addLeftMost(P_0, expansion);
				if (!emptyExpansionExists(expansion))
				{
					break;
				}
			}
		}
		else if (P_1 is TryBlock)
		{
			addLeftMost(P_0, ((TryBlock)P_1).exp);
		}
	}

	
	private static bool prodWalk(NormalProduction P_0)
	{
		P_0.walkStatus = -1;
		for (int i = 0; i < P_0.leIndex; i++)
		{
			if (P_0.leftExpansions[i].walkStatus == -1)
			{
				P_0.leftExpansions[i].walkStatus = -2;
				loopString = new StringBuilder().Append(P_0.lhs).Append("... --> ").Append(P_0.leftExpansions[i].lhs)
					.Append("...")
					.ToString();
				if (P_0.walkStatus == -2)
				{
					P_0.walkStatus = 1;
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Left recursion detected: \"").Append(loopString).Append("\"")
						.ToString());
					return false;
				}
				P_0.walkStatus = 1;
				return true;
			}
			if (P_0.leftExpansions[i].walkStatus == 0 && prodWalk(P_0.leftExpansions[i]))
			{
				loopString = new StringBuilder().Append(P_0.lhs).Append("... --> ").Append(loopString)
					.ToString();
				if (P_0.walkStatus == -2)
				{
					P_0.walkStatus = 1;
					JavaCCErrors.Semantic_Error(P_0, new StringBuilder().Append("Left recursion detected: \"").Append(loopString).Append("\"")
						.ToString());
					return false;
				}
				P_0.walkStatus = 1;
				return true;
			}
		}
		P_0.walkStatus = 1;
		return false;
	}

	
	private static bool rexpWalk(RegularExpression P_0)
	{
		if (P_0 is RJustName)
		{
			RJustName rJustName = (RJustName)P_0;
			if (rJustName.regexpr.walkStatus == -1)
			{
				rJustName.regexpr.walkStatus = -2;
				loopString = new StringBuilder().Append("...").Append(rJustName.regexpr.label).Append("...")
					.ToString();
				return true;
			}
			if (rJustName.regexpr.walkStatus == 0)
			{
				rJustName.regexpr.walkStatus = -1;
				if (rexpWalk(rJustName.regexpr))
				{
					loopString = new StringBuilder().Append("...").Append(rJustName.regexpr.label).Append("... --> ")
						.Append(loopString)
						.ToString();
					if (rJustName.regexpr.walkStatus == -2)
					{
						rJustName.regexpr.walkStatus = 1;
						JavaCCErrors.Semantic_Error(rJustName.regexpr, new StringBuilder().Append("Loop in regular expression detected: \"").Append(loopString).Append("\"")
							.ToString());
						return false;
					}
					rJustName.regexpr.walkStatus = 1;
					return true;
				}
				rJustName.regexpr.walkStatus = 1;
				return false;
			}
		}
		else
		{
			if (P_0 is RChoice)
			{
				Enumeration enumeration = ((RChoice)P_0).choices.elements();
				while (enumeration.hasMoreElements())
				{
					if (rexpWalk((RegularExpression)enumeration.nextElement()))
					{
						return true;
					}
				}
				return false;
			}
			if (P_0 is RSequence)
			{
				Enumeration enumeration = ((RSequence)P_0).units.elements();
				while (enumeration.hasMoreElements())
				{
					if (rexpWalk((RegularExpression)enumeration.nextElement()))
					{
						return true;
					}
				}
				return false;
			}
			if (P_0 is ROneOrMore)
			{
				bool result = rexpWalk(((ROneOrMore)P_0).regexpr);
				
				return result;
			}
			if (P_0 is RZeroOrMore)
			{
				bool result2 = rexpWalk(((RZeroOrMore)P_0).regexpr);
				
				return result2;
			}
			if (P_0 is RZeroOrOne)
			{
				bool result3 = rexpWalk(((RZeroOrOne)P_0).regexpr);
				
				return result3;
			}
			if (P_0 is RRepetitionRange)
			{
				bool result4 = rexpWalk(((RRepetitionRange)P_0).regexpr);
				
				return result4;
			}
		}
		return false;
	}

	
	public Semanticize()
	{
	}

	static Semanticize()
	{
		
		removeList = new ArrayList();
		itemList = new ArrayList();
	}
}
