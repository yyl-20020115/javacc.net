using System;
using System.Collections;
using System.Text;
namespace org.javacc.parser;

public class LookaheadCalc : JavaCCGlobals
{
	internal static int firstChoice(Choice P_0)
	{
		if (Options.getForceLaCheck())
		{
			return 0;
		}
		for (int i = 0; i < P_0.Choices.Count; i++)
		{
			if (!explicitLA((Expansion)P_0.Choices[i]))
			{
				return i;
			}
		}
		int result = P_0.Choices.Count;
		
		return result;
	}

	
	internal static bool javaCodeCheck(ArrayList P_0)
	{
		for (int i = 0; i < P_0.Count; i++)
		{
			if (((MatchInfo)P_0[i]).firstFreeLoc == 0)
			{
				return true;
			}
		}
		return false;
	}

	
	internal static MatchInfo overlap(ArrayList P_0, ArrayList P_1)
	{
		for (int i = 0; i < P_0.Count; i++)
		{
			MatchInfo matchInfo = (MatchInfo)P_0[i];
			for (int j = 0; j < P_1.Count; j++)
			{
				MatchInfo matchInfo2 = (MatchInfo)P_1[j];
				int firstFreeLoc = matchInfo.firstFreeLoc;
				MatchInfo result = matchInfo;
				if (firstFreeLoc > matchInfo2.firstFreeLoc)
				{
					firstFreeLoc = matchInfo2.firstFreeLoc;
					result = matchInfo2;
				}
				if (firstFreeLoc == 0)
				{
					return null;
				}
				int num = 0;
				for (int k = 0; k < firstFreeLoc; k++)
				{
					if (matchInfo.match[k] != matchInfo2.match[k])
					{
						num = 1;
						break;
					}
				}
				if (num == 0)
				{
					return result;
				}
			}
		}
		return null;
	}

	
	internal static bool explicitLA(Expansion P_0)
	{
		if (!(P_0 is Sequence))
		{
			return false;
		}
		Sequence sequence = (Sequence)P_0;
		object obj = sequence.units[0];
		if (!(obj is Lookahead))
		{
			return false;
		}
		Lookahead lookahead = (Lookahead)obj;
		return lookahead.isExplicit;
	}

	
	internal static string image(MatchInfo P_0)
	{
		string text = "";
		for (int i = 0; i < P_0.firstFreeLoc; i++)
		{
			if (P_0.match[i] == 0)
			{
				text = new StringBuilder().Append(text).Append(" <EOF>").ToString();
				continue;
			}
			Hashtable hashtable = JavaCCGlobals.rexps_of_tokens;
			;
			RegularExpression regularExpression = (RegularExpression)hashtable[P_0.match[i]];

			text = ((!(regularExpression is RStringLiteral)) ? ((regularExpression.label == null || string.Equals(regularExpression.label, "")) ? new StringBuilder().Append(text).Append(" <token of kind ").Append(i)
				.Append(">")
				.ToString() : new StringBuilder().Append(text).Append(" <").Append(regularExpression.label)
				.Append(">")
				.ToString()) : new StringBuilder().Append(text).Append(" \"").Append(JavaCCGlobals.add_escapes(((RStringLiteral)regularExpression).image))
				.Append("\"")
				.ToString());
		}
		if (P_0.firstFreeLoc == 0)
		{
			return "";
		}
		string result = text.Substring(1);// String.instancehelper_substring(text, 1);
		
		return result;
	}

	private static string image(Expansion P_0)
	{
		if (P_0 is OneOrMore)
		{
			return "(...)+";
		}
		if (P_0 is ZeroOrMore)
		{
			return "(...)*";
		}
		return "[...]";
	}

	
	public LookaheadCalc()
	{
	}

	
	public static void choiceCalc(Choice c)
	{
		int num = firstChoice(c);
		ArrayList[] array = new ArrayList[c.Choices.Count];
		ArrayList[] array2 = new ArrayList[c.Choices.Count];
		int[] array3 = new int[c.Choices.Count - 1];
		MatchInfo[] array4 = new MatchInfo[c.Choices.Count - 1];
		int[] array5 = new int[c.Choices.Count - 1];
		for (int i = 1; i <= Options.getChoiceAmbiguityCheck(); i++)
		{
			MatchInfo.laLimit = i;
			LookaheadWalk.considerSemanticLA = ((!Options.getForceLaCheck()) ? true : false);
			for (int j = num; j < c.Choices.Count - 1; j++)
			{
				LookaheadWalk.sizeLimitedMatches = new ArrayList();
				MatchInfo matchInfo = new MatchInfo();
				matchInfo.firstFreeLoc = 0;
				ArrayList vector = new ArrayList();
				vector.Add(matchInfo);
				LookaheadWalk.genFirstSet(vector, (Expansion)c.Choices[j]);
				array[j] = LookaheadWalk.sizeLimitedMatches;
			}
			LookaheadWalk.considerSemanticLA = false;
			for (int j = num + 1; j < c.Choices.Count; j++)
			{
				LookaheadWalk.sizeLimitedMatches = new ArrayList();
				MatchInfo matchInfo = new MatchInfo();
				matchInfo.firstFreeLoc = 0;
				ArrayList vector = new ArrayList();
				vector.Add(matchInfo);
				LookaheadWalk.genFirstSet(vector, (Expansion)c.Choices[j]);
				array2[j] = LookaheadWalk.sizeLimitedMatches;
			}
			if (i == 1)
			{
				for (int j = num; j < c.Choices.Count - 1; j++)
				{
					Expansion expansion = (Expansion)c.Choices[j];
					if (Semanticize.emptyExpansionExists(expansion))
					{
						JavaCCErrors.Warning(expansion, "This choice can expand to the empty token sequence and will therefore always be taken in favor of the choices appearing later.");
						break;
					}
					if (javaCodeCheck(array[j]))
					{
						JavaCCErrors.Warning(expansion, "JAVACODE non-terminal will force this choice to be taken in favor of the choices appearing later.");
						break;
					}
				}
			}
			int num2 = 0;
			for (int j = num; j < c.Choices.Count - 1; j++)
			{
				for (int k = j + 1; k < c.Choices.Count; k++)
				{
					MatchInfo matchInfo;
					if ((matchInfo = overlap(array[j], array2[k])) != null)
					{
						array3[j] = i + 1;
						array4[j] = matchInfo;
						array5[j] = k;
						num2 = 1;
						break;
					}
				}
			}
			if (num2 == 0)
			{
				break;
			}
		}
		for (int i = num; i < c.Choices.Count - 1; i++)
		{
			if (!explicitLA((Expansion)c.Choices[i]) || Options.getForceLaCheck())
			{
				if (array3[i] > Options.getChoiceAmbiguityCheck())
				{
					JavaCCErrors.Warning("Choice conflict involving two expansions at");
					Console.Error.Write(new StringBuilder().Append("         line ").Append(((Expansion)c.Choices[i]).Line).ToString());
					Console.Error.Write(new StringBuilder().Append(", column ").Append(((Expansion)c.Choices[i]).Column).ToString());
					Console.Error.Write(new StringBuilder().Append(" and line ").Append(((Expansion)c.Choices[(array5[i])]).Line).ToString());
					Console.Error.Write(new StringBuilder().Append(", column ").Append(((Expansion)c.Choices[(array5[i])]).Column).ToString());
					Console.Error.WriteLine(" respectively.");
					Console.Error.WriteLine(new StringBuilder().Append("         A common prefix is: ").Append(image(array4[i])).ToString());
					Console.Error.WriteLine(new StringBuilder().Append("         Consider using a lookahead of ").Append(array3[i]).Append(" or more for earlier expansion.")
						.ToString());
				}
				else if (array3[i] > 1)
				{
					JavaCCErrors.Warning("Choice conflict involving two expansions at");
					Console.Error.Write(new StringBuilder().Append("         line ").Append(((Expansion)c.Choices[i]).Line).ToString());
					Console.Error.Write(new StringBuilder().Append(", column ").Append(((Expansion)c.Choices[i]).Column).ToString());
					Console.Error.Write(new StringBuilder().Append(" and line ").Append(((Expansion)c.Choices[(array5[i])]).Line).ToString());
					Console.Error.Write(new StringBuilder().Append(", column ").Append(((Expansion)c.Choices[(array5[i])]).Column).ToString());
					Console.Error.WriteLine(" respectively.");
					Console.Error.WriteLine(new StringBuilder().Append("         A common prefix is: ").Append(image(array4[i])).ToString());
					Console.Error.WriteLine(new StringBuilder().Append("         Consider using a lookahead of ").Append(array3[i]).Append(" for earlier expansion.")
						.ToString());
				}
			}
		}
	}

	
	public static void ebnfCalc(Expansion e1, Expansion e2)
	{
		MatchInfo matchInfo = null;
		int i;
		for (i = 1; i <= Options.getOtherAmbiguityCheck(); i++)
		{
			MatchInfo.laLimit = i;
			LookaheadWalk.sizeLimitedMatches = new ArrayList();
			MatchInfo matchInfo2 = new MatchInfo();
			matchInfo2.firstFreeLoc = 0;
			ArrayList vector = new ArrayList();
			vector.Add(matchInfo2);
			LookaheadWalk.considerSemanticLA = ((!Options.getForceLaCheck()) ? true : false);
			LookaheadWalk.genFirstSet(vector, e2);
			ArrayList sizeLimitedMatches = LookaheadWalk.sizeLimitedMatches;
			LookaheadWalk.sizeLimitedMatches = new ArrayList();
			LookaheadWalk.considerSemanticLA = false;
			LookaheadWalk.genFollowSet(vector, e1, Expansion.NextGenerationIndex++);
			ArrayList sizeLimitedMatches2 = LookaheadWalk.sizeLimitedMatches;
			if (i == 1 && javaCodeCheck(sizeLimitedMatches))
			{
				JavaCCErrors.Warning(e2, new StringBuilder().Append("JAVACODE non-terminal within ").Append(image(e1)).Append(" construct will force this construct to be entered in favor of ")
					.Append("expansions occurring after construct.")
					.ToString());
			}
			if ((matchInfo2 = overlap(sizeLimitedMatches, sizeLimitedMatches2)) == null)
			{
				break;
			}
			matchInfo = matchInfo2;
		}
		if (i > Options.getOtherAmbiguityCheck())
		{
			JavaCCErrors.Warning(new StringBuilder().Append("Choice conflict in ").Append(image(e1)).Append(" construct ")
				.Append("at line ")
				.Append(e1.Line)
				.Append(", column ")
				.Append(e1.Column)
				.Append(".")
				.ToString());
			Console.Error.WriteLine("         Expansion nested within construct and expansion following construct");
			Console.Error.WriteLine(new StringBuilder().Append("         have common prefixes, one of which is: ").Append(image(matchInfo)).ToString());
			Console.Error.WriteLine(new StringBuilder().Append("         Consider using a lookahead of ").Append(i).Append(" or more for nested expansion.")
				.ToString());
		}
		else if (i > 1)
		{
			JavaCCErrors.Warning(new StringBuilder().Append("Choice conflict in ").Append(image(e1)).Append(" construct ")
				.Append("at line ")
				.Append(e1.Line)
				.Append(", column ")
				.Append(e1.Column)
				.Append(".")
				.ToString());
			Console.Error.WriteLine("         Expansion nested within construct and expansion following construct");
			Console.Error.WriteLine(new StringBuilder().Append("         have common prefixes, one of which is: ").Append(image(matchInfo)).ToString());
			Console.Error.WriteLine(new StringBuilder().Append("         Consider using a lookahead of ").Append(i).Append(" for nested expansion.")
				.ToString());
		}
	}
}
