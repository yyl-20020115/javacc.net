namespace JavaCC.Parser;
using System;
using System.Collections.Generic;
using System.Text;

public class Expansion
{
    public static long NextGenerationIndex = 1L;
    public static readonly string EOL = Environment.NewLine;
    public int Line = 1;
    public int Column = 1;
    public string InternalName = "";
    public bool Phase3done = false;
    public Expansion Parent;
    public int Ordinal = 0;
    public long MyGeneration = 0L;
    public bool InMinimumSize = false;
    protected internal virtual StringBuilder DumpPrefix(int i)
    {
        var builder = new StringBuilder(128);
        for (int j = 0; j < i; j++)
        {
            builder.Append("  ");
        }
        return builder;
    }
    public override int GetHashCode() => Line + Column;

    public static void ReInit()
    {
        NextGenerationIndex = 1L;
    }

    public override string ToString() => ("[") + (Line) + (",")
            + (Column)
            + (" ")
            + (this.GetHashCode())
            + (" ")
            + (this.GetType().Name)
            + ("]");

    public virtual StringBuilder Dump(int i, HashSet<Expansion> s) => DumpPrefix(i).Append(
            this.GetHashCode()).Append(" ").Append(
                this.GetType().Name);
}
