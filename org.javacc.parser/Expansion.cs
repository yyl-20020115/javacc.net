using System;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class Expansion
{
	internal int line;
	internal int column;
	internal string internal_name;
	internal bool phase3done;
	public object parent;
	internal int ordinal;
	public static long NextGenerationIndex;
	public long myGeneration;
	public bool inMinimumSize;
	internal static string EOL;

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

    public override int GetHashCode() => line + column;

    public static void ReInit()
	{
		NextGenerationIndex = 1L;
	}

	public override string ToString()
	{
		return new StringBuilder().Append("[").Append(line).Append(",")
			.Append(column)
			.Append(" ")
			.Append(this.GetHashCode())
			.Append(" ")
			.Append(this.GetType().Name)
			.Append("]")
			.ToString();
	}

	
	public virtual StringBuilder dump(int i, HashSet<Expansion> s)
	{
		return DumpPrefix(i).Append(
			(this).GetHashCode()).Append(" ").Append(
				this.GetType().Name);
	}

	static Expansion()
	{
		NextGenerationIndex = 1L;
		EOL = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
	}
}
