namespace JavaCC.Parser;
using System.Collections.Generic;

public sealed class LookaheadWalk
{
    public static bool considerSemanticLA =false;
    public static List<MatchInfo> sizeLimitedMatches = new();

    public static List<MatchInfo> GenFirstSet(List<MatchInfo> v, Expansion e)
    {
        List<MatchInfo> vector;
        if (e is RegularExpression re)
        {
            vector = new ();
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
                return new();
            }
            return GenFirstSet(v, prod.Expansion);
        }
        if (e is Choice)
        {
            vector = new ();
            Choice choice = (Choice)e;
            for (int k = 0; k < choice.Choices.Count; k++)
            {
                var v2 = GenFirstSet(v, (Expansion)choice.Choices[k]);
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
            vector = new ();
            var vector2 = v;
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
            vector = new ();
            VectorAppend(vector, v);
            var vector2 = v;
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
            vector = new ();
            VectorAppend(vector, v);
            VectorAppend(vector, GenFirstSet(v, ((ZeroOrOne)e).expansion));
            return vector;
        }
        if (e is TryBlock)
        {
            return GenFirstSet(v, ((TryBlock)e).exp);
        }
        if (considerSemanticLA && e is Lookahead && ((Lookahead)e).action_tokens.Count != 0)
        {
            return new();
        }
        vector = new ();
        VectorAppend(vector, v);
        return vector;
    }


    public static List<MatchInfo> GenFollowSet(List<MatchInfo> v, Expansion e, long l)
    {
        if (e.myGeneration == l)
        {
           
            return new();
        }
        e.myGeneration = l;
        if (e.parent == null)
        {
            List<MatchInfo> vector = new ();
            VectorAppend(vector, v);
            return vector;
        }
        if (e.parent is NormalProduction)
        {
            var vector = ((NormalProduction)e.parent).parents;
            var vector2 = new List<MatchInfo>();
            for (int i = 0; i < vector.Count; i++)
            {
                var v2 = GenFollowSet(v, (Expansion)vector[i], l);
                VectorAppend(vector2, v2);
            }
            return vector2;
        }
        if (e.parent is Sequence)
        {
            Sequence sequence = (Sequence)e.parent;
            var vector2 = v;
            for (int i = e.ordinal + 1; i < sequence.Units.Count; i++)
            {
                vector2 = GenFirstSet(vector2, (Expansion)sequence.Units[i]);
                if (vector2.Count == 0)
                {
                    return vector2;
                }
            }
            List<MatchInfo> vector3 = new ();
            List<MatchInfo> v2 = new ();
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
            List<MatchInfo> vector = new ();
            VectorAppend(vector, v);
            var vector2 = v;
            while (true)
            {
                vector2 = GenFirstSet(vector2, e);
                if (vector2.Count == 0)
                {
                    break;
                }
                VectorAppend(vector, vector2);
            }
            List<MatchInfo> vector3 = new ();
            List<MatchInfo> v2 = new ();
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
        return GenFollowSet(v, (Expansion)e.parent, l);
    }


    public static void VectorAppend(List<MatchInfo> v1, List<MatchInfo> v2)
    {
        for (int i = 0; i < v2.Count; i++)
        {
            v1.Add(v2[i]);
        }
    }


    public static void VectorSplit(List<MatchInfo> v1, List<MatchInfo> v2, List<MatchInfo> v3, List<MatchInfo> v4)
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
