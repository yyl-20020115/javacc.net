using System.Collections;
namespace org.javacc.parser;

public sealed class LookaheadWalk
{
	public static bool considerSemanticLA;
	public static ArrayList sizeLimitedMatches;
	
	public static ArrayList genFirstSet(ArrayList v, Expansion e)
	{
		ArrayList vector;
		if (e is RegularExpression)
		{
			vector = new ArrayList();
			for (int i = 0; i < v.Count; i++)
			{
				MatchInfo matchInfo = (MatchInfo)v[i];
				MatchInfo matchInfo2 = new MatchInfo();
				for (int j = 0; j < matchInfo.firstFreeLoc; j++)
				{
					matchInfo2.match[j] = matchInfo.match[j];
				}
				matchInfo2.firstFreeLoc = matchInfo.firstFreeLoc;
				int[] match = matchInfo2.match;
				int firstFreeLoc = matchInfo2.firstFreeLoc;
				MatchInfo matchInfo3 = matchInfo2;
				matchInfo3.firstFreeLoc = firstFreeLoc + 1;
				match[firstFreeLoc] = ((RegularExpression)e).ordinal;
				if (matchInfo2.firstFreeLoc == MatchInfo.laLimit)
				{
					sizeLimitedMatches.Add(matchInfo2);
				}
				else
				{
					vector.Add(matchInfo2);
				}
			}
			return vector;
		}
		if (e is NonTerminal)
		{
			NormalProduction prod = ((NonTerminal)e).prod;
			if (prod is JavaCodeProduction)
			{
				ArrayList result = new ArrayList();
				
				return result;
			}
			ArrayList result2 = genFirstSet(v, prod.expansion);
			
			return result2;
		}
		if (e is Choice)
		{
			vector = new ArrayList();
			Choice choice = (Choice)e;
			for (int k = 0; k < choice.choices.Count; k++)
			{
				ArrayList v2 = genFirstSet(v, (Expansion)choice.choices[k]);
				vectorAppend(vector, v2);
			}
			return vector;
		}
		if (e is Sequence)
		{
			vector = v;
			Sequence sequence = (Sequence)e;
			for (int k = 0; k < sequence.units.Count; k++)
			{
				vector = genFirstSet(vector, (Expansion)sequence.units[k]);
				if (vector.Count == 0)
				{
					break;
				}
			}
			return vector;
		}
		if (e is OneOrMore)
		{
			vector = new ArrayList();
			ArrayList vector2 = v;
			OneOrMore oneOrMore = (OneOrMore)e;
			while (true)
			{
				vector2 = genFirstSet(vector2, oneOrMore.expansion);
				if (vector2.Count == 0)
				{
					break;
				}
				vectorAppend(vector, vector2);
			}
			return vector;
		}
		if (e is ZeroOrMore)
		{
			vector = new ArrayList();
			vectorAppend(vector, v);
			ArrayList vector2 = v;
			ZeroOrMore zeroOrMore = (ZeroOrMore)e;
			while (true)
			{
				vector2 = genFirstSet(vector2, zeroOrMore.expansion);
				if (vector2.Count == 0)
				{
					break;
				}
				vectorAppend(vector, vector2);
			}
			return vector;
		}
		if (e is ZeroOrOne)
		{
			vector = new ArrayList();
			vectorAppend(vector, v);
			vectorAppend(vector, genFirstSet(v, ((ZeroOrOne)e).expansion));
			return vector;
		}
		if (e is TryBlock)
		{
			ArrayList result3 = genFirstSet(v, ((TryBlock)e).exp);
			
			return result3;
		}
		if (considerSemanticLA && e is Lookahead && ((Lookahead)e).action_tokens.Count != 0)
		{
			ArrayList result4 = new ArrayList();
			
			return result4;
		}
		vector = new ArrayList();
		vectorAppend(vector, v);
		return vector;
	}

	
	public static ArrayList genFollowSet(ArrayList v, Expansion e, long l)
	{
		if (e.myGeneration == l)
		{
			ArrayList result = new ArrayList();
			
			return result;
		}
		e.myGeneration = l;
		if (e.parent == null)
		{
			ArrayList vector = new ArrayList();
			vectorAppend(vector, v);
			return vector;
		}
		if (e.parent is NormalProduction)
		{
			ArrayList vector = ((NormalProduction)e.parent).parents;
			ArrayList vector2 = new ArrayList();
			for (int i = 0; i < vector.Count; i++)
			{
				ArrayList v2 = genFollowSet(v, (Expansion)vector[i], l);
				vectorAppend(vector2, v2);
			}
			return vector2;
		}
		if (e.parent is Sequence)
		{
			Sequence sequence = (Sequence)e.parent;
			ArrayList vector2 = v;
			for (int i = e.ordinal + 1; i < sequence.units.Count; i++)
			{
				vector2 = genFirstSet(vector2, (Expansion)sequence.units[i]);
				if (vector2.Count == 0)
				{
					return vector2;
				}
			}
			ArrayList vector3 = new ArrayList();
			ArrayList v2 = new ArrayList();
			vectorSplit(vector2, v, vector3, v2);
			if (vector3.Count != 0)
			{
				vector3 = genFollowSet(vector3, sequence, l);
			}
			if (v2.Count != 0)
			{
				v2 = genFollowSet(v2, sequence, Expansion.NextGenerationIndex++);
			}
			vectorAppend(v2, vector3);
			return v2;
		}
		if (e.parent is OneOrMore || e.parent is ZeroOrMore)
		{
			ArrayList vector = new ArrayList();
			vectorAppend(vector, v);
			ArrayList vector2 = v;
			while (true)
			{
				vector2 = genFirstSet(vector2, e);
				if (vector2.Count == 0)
				{
					break;
				}
				vectorAppend(vector, vector2);
			}
			ArrayList vector3 = new ArrayList();
			ArrayList v2 = new ArrayList();
			vectorSplit(vector, v, vector3, v2);
			if (vector3.Count != 0)
			{
				vector3 = genFollowSet(vector3, (Expansion)e.parent, l);
			}
			if (v2.Count != 0)
			{
				v2 = genFollowSet(v2, (Expansion)e.parent, Expansion.NextGenerationIndex++);
			}
			vectorAppend(v2, vector3);
			return v2;
		}
		ArrayList result2 = genFollowSet(v, (Expansion)e.parent, l);
		
		return result2;
	}

	
	public static void vectorAppend(ArrayList v1, ArrayList v2)
	{
		for (int i = 0; i < v2.Count; i++)
		{
			v1.Add(v2[i]);
		}
	}

	
	public static void vectorSplit(ArrayList v1, ArrayList v2, ArrayList v3, ArrayList v4)
	{
		for (int i = 0; i < v1.Count; i++)
		{
			int num = 0;
			while (true)
			{
				if (num < v2.Count)
				{
					if (v1[i] == v2[num])
					{
						v3.Add(v1[i]);
						break;
					}
					num++;
					continue;
				}
				v4.Add(v1[i]);
				break;
			}
		}
	}

	
	private LookaheadWalk()
	{
	}

	public static void reInit()
	{
		considerSemanticLA = false;
		sizeLimitedMatches = null;
	}
}
