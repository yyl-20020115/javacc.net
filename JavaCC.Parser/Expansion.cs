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
    public string internal_name = "";
    public bool phase3done = false;
    public object Parent = new();
    public int ordinal = 0;
    public long myGeneration = 0L;
    public bool inMinimumSize = false;

    protected internal static string Eol => EOL;

    protected internal virtual StringBuilder DumpPrefix(int i)
    {
        var builder = new StringBuilder(128);
        for (int j = 0; j < i; j++)
        {
            builder.Append("  ");
        }
        return builder;
    }

    public Expansion()
    {
        internal_name = "";
        phase3done = false;
        myGeneration = 0L;
        inMinimumSize = false;
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
            (this).GetHashCode()).Append(" ").Append(
                this.GetType().Name);
}
