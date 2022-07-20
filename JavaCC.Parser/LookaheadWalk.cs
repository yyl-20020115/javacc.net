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
                var matchInfo = v[i];
                var matchInfo2 = new MatchInfo();
                for (int j = 0; j < matchInfo.FirstFreeLoc; j++)
                {
                    matchInfo2.Match[j] = matchInfo.Match[j];
                }
                matchInfo2.FirstFreeLoc = matchInfo.FirstFreeLoc;
                int[] match = matchInfo2.Match;
                int firstFreeLoc = matchInfo2.FirstFreeLoc;
                MatchInfo matchInfo3 = matchInfo2;
                matchInfo3.FirstFreeLoc = firstFreeLoc + 1;
                match[firstFreeLoc] = re.Ordinal;
                if (matchInfo2.FirstFreeLoc == MatchInfo.LaLimit)
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
            var prod = n.prod;
            if (prod is JavaCodeProduction)
            {
                return new();
            }
            return GenFirstSet(v, prod.Expansion);
        }
        if (e is Choice choice)
        {
            vector = new ();
            for (int k = 0; k < choice.Choices.Count; k++)
            {
                var v2 = GenFirstSet(v, choice.Choices[k]);
                VectorAppend(vector, v2);
            }
            return vector;
        }
        if (e is Sequence sequence)
        {
            vector = v;
            for (int k = 0; k < sequence.Units.Count; k++)
            {
                vector = GenFirstSet(vector, sequence.Units[k]);
                if (vector.Count == 0)
                {
                    break;
                }
            }
            return vector;
        }
        if (e is OneOrMore more)
        {
            vector = new ();
            var vector2 = v;
            while (true)
            {
                vector2 = GenFirstSet(vector2, more.Expansion);
                if (vector2.Count == 0)
                {
                    break;
                }
                VectorAppend(vector, vector2);
            }
            return vector;
        }
        if (e is ZeroOrMore more1)
        {
            vector = new ();
            VectorAppend(vector, v);
            var vector2 = v;
            while (true)
            {
                vector2 = GenFirstSet(vector2, more1.Expansion);
                if (vector2.Count == 0)
                {
                    break;
                }
                VectorAppend(vector, vector2);
            }
            return vector;
        }
        if (e is ZeroOrOne one)
        {
            vector = new ();
            VectorAppend(vector, v);
            VectorAppend(vector, GenFirstSet(v, one.Expansion));
            return vector;
        }
        if (e is TryBlock block)
        {
            return GenFirstSet(v, block.Expression);
        }
        if (considerSemanticLA && e is Lookahead lookahead && lookahead.ActionTokens.Count != 0)
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
        if (e.Parent == null)
        {
            List<MatchInfo> vector = new ();
            VectorAppend(vector, v);
            return vector;
        }
        if (e.Parent is NormalProduction)
        {
            var vector = ((NormalProduction)e.Parent).Parents;
            var vector2 = new List<MatchInfo>();
            for (int i = 0; i < vector.Count; i++)
            {
                var v2 = GenFollowSet(v, (Expansion)vector[i], l);
                VectorAppend(vector2, v2);
            }
            return vector2;
        }
        if (e.Parent is Sequence)
        {
            Sequence sequence = (Sequence)e.Parent;
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
        if (e.Parent is OneOrMore || e.Parent is ZeroOrMore)
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
                vector3 = GenFollowSet(vector3, (Expansion)e.Parent, l);
            }
            if (v2.Count != 0)
            {
                v2 = GenFollowSet(v2, (Expansion)e.Parent, Expansion.NextGenerationIndex++);
            }
            VectorAppend(v2, vector3);
            return v2;
        }
        return GenFollowSet(v, (Expansion)e.Parent, l);
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
