namespace JavaCC.Parser;
using System;
using System.Collections.Generic;
using System.Text;

public class NormalProduction : Expansion
{
    public List<Expansion> Parents = new();

    public string AccessMod = "";

    public string Lhs="";

    public readonly List<Token> ReturnTypeToken = new();

    public readonly List<Token> ParameterListTokens = new();

    public readonly List<List<Token>> ThrowsList = new();

    public Expansion Expansion;

    public bool EmptyPossible = false;

    public NormalProduction[] LeftExpansions = Array.Empty<NormalProduction>();

    public int LeIndex = 0;

    public int WalkStatus = 0;

    public Token FirstToken;

    public Token LastToken;

    protected internal override StringBuilder DumpPrefix(int i)
    {
        var builder = new StringBuilder(128);
        for (int j = 0; j < i; j++)
        {
            builder.Append("  ");
        }
        return builder;
    }

    public NormalProduction()
    {
        LeftExpansions = new NormalProduction[10];
        //Eol = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
    }


    public new virtual StringBuilder Dump(int i, HashSet<Expansion> s)
    {
        var builder = DumpPrefix(i).Append((this.GetHashCode())).Append(' ').Append(this.GetType().Name)
            .Append(' ')
            .Append(Lhs);
        if (!s.Contains(this))
        {
            s.Add(this);
            if (Expansion != null)
            {
                builder.Append(EOL).Append(Expansion.Dump(i + 1, s));
            }
        }
        return builder;
    }
}
