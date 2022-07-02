using System;
using System.Collections.Generic;
using System.Text;

namespace JavaCC.Parser;

public class Expansion
{
	public static long NextGenerationIndex = 1L;
	internal static string EOL = Environment.NewLine;
	internal int Line = 0;
	internal int Column = 0;
	internal string internal_name ="";
	internal bool phase3done =false;
	public object parent =new();
	internal int ordinal = 0;
	public long myGeneration = 0L;
	public bool inMinimumSize= false;

    protected internal static string Eol => EOL;

    protected internal virtual StringBuilder DumpPrefix(int i)
	{
		var stringBuilder = new StringBuilder(128);
		for (int j = 0; j < i; j++)
		{
			stringBuilder.Append("  ");
		}
		return stringBuilder;
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
