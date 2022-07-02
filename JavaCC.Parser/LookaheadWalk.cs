using System.Collections;
namespace JavaCC.Parser;

public sealed class LookaheadWalk
{
	public static bool considerSemanticLA;
	public static ArrayList sizeLimitedMatches;
	
	public static ArrayList GenFirstSet(ArrayList v, Expansion e)
	{
		ArrayList vector;
		if (e is RegularExpression re)
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
				match[firstFreeLoc] = re.ordinal;
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
		if (e is NonTerminal n)
		{
			NormalProduction prod = n.prod;
			if (prod is JavaCodeProduction)
			{
				ArrayList result = new ArrayList();
				
				return result;
			}
			ArrayList result2 = GenFirstSet(v, prod.Expansion);
			
			return result2;
		}
		if (e is Choice)
		{
			vector = new ArrayList();
			Choice choice = (Choice)e;
			for (int k = 0; k < choice.Choices.Count; k++)
			{
				ArrayList v2 = GenFirstSet(v, (Expansion)choice.Choices[k]);
				VectorAppend(vector, v2);
			}
			return vector;
		}
		if (e is Sequence)
		{
			vector = v;
			Sequence sequence = (Sequence)e;
			for (int k = 0; k < sequence.Units.Count; k++)
			{
				vector = GenFirstSet(vector, (Expansion)sequence.Units[k]);
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
				vector2 = GenFirstSet(vector2, oneOrMore.expansion);
				if (vector2.Count == 0)
				{
					break;
				}
				VectorAppend(vector, vector2);
			}
			return vector;
		}
		if (e is ZeroOrMore)
		{
			vector = new ArrayList();
			VectorAppend(vector, v);
			ArrayList vector2 = v;
			ZeroOrMore zeroOrMore = (ZeroOrMore)e;
			while (true)
			{
				vector2 = GenFirstSet(vector2, zeroOrMore.expansion);
				if (vector2.Count == 0)
				{
					break;
				}
				VectorAppend(vector, vector2);
			}
			return vector;
		}
		if (e is ZeroOrOne)
		{
			vector = new ArrayList();
			VectorAppend(vector, v);
			VectorAppend(vector, GenFirstSet(v, ((ZeroOrOne)e).expansion));
			return vector;
		}
		if (e is TryBlock)
		{
			ArrayList result3 = GenFirstSet(v, ((TryBlock)e).exp);
			
			return result3;
		}
		if (considerSemanticLA && e is Lookahead && ((Lookahead)e).action_tokens.Count != 0)
		{
			ArrayList result4 = new ArrayList();
			
			return result4;
		}
		vector = new ArrayList();
		VectorAppend(vector, v);
		return vector;
	}

	
	public static ArrayList GenFollowSet(ArrayList v, Expansion e, long l)
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
			VectorAppend(vector, v);
			return vector;
		}
		if (e.parent is NormalProduction)
		{
			var vector = ((NormalProduction)e.parent).parents;
			ArrayList vector2 = new ArrayList();
			for (int i = 0; i < vector.Count; i++)
			{
				ArrayList v2 = GenFollowSet(v, (Expansion)vector[i], l);
				VectorAppend(vector2, v2);
			}
			return vector2;
		}
		if (e.parent is Sequence)
		{
			Sequence sequence = (Sequence)e.parent;
			ArrayList vector2 = v;
			for (int i = e.ordinal + 1; i < sequence.Units.Count; i++)
			{
				vector2 = GenFirstSet(vector2, (Expansion)sequence.Units[i]);
				if (vector2.Count == 0)
				{
					return vector2;
				}
			}
			ArrayList vector3 = new ArrayList();
			ArrayList v2 = new ArrayList();
			VectorSplit(vector2, v, vector3, v2);
			if (vector3.Count != 0)
			{
				vector3 = GenFollowSet(vector3, sequence, l);
			}
			if (v2.Count != 0)
			{
				v2 = GenFollowSet(v2, sequence, Expansion.NextGenerationIndex++);
			}
			VectorAppend(v2, vector3);
			return v2;
		}
		if (e.parent is OneOrMore || e.parent is ZeroOrMore)
		{
			ArrayList vector = new ArrayList();
			VectorAppend(vector, v);
			ArrayList vector2 = v;
			while (true)
			{
				vector2 = GenFirstSet(vector2, e);
				if (vector2.Count == 0)
				{
					break;
				}
				VectorAppend(vector, vector2);
			}
			ArrayList vector3 = new ArrayList();
			ArrayList v2 = new ArrayList();
			VectorSplit(vector, v, vector3, v2);
			if (vector3.Count != 0)
			{
				vector3 = GenFollowSet(vector3, (Expansion)e.parent, l);
			}
			if (v2.Count != 0)
			{
				v2 = GenFollowSet(v2, (Expansion)e.parent, Expansion.NextGenerationIndex++);
			}
			VectorAppend(v2, vector3);
			return v2;
		}
		ArrayList result2 = GenFollowSet(v, (Expansion)e.parent, l);
		
		return result2;
	}

	
	public static void VectorAppend(ArrayList v1, ArrayList v2)
	{
		for (int i = 0; i < v2.Count; i++)
		{
			v1.Add(v2[i]);
		}
	}

	
	public static void VectorSplit(ArrayList v1, ArrayList v2, ArrayList v3, ArrayList v4)
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

	public static void ReInit()
	{
		considerSemanticLA = false;
		sizeLimitedMatches = null;
	}
}
