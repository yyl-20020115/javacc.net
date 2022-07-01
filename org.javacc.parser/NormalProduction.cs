using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.javacc.parser;

public class NormalProduction :Expansion
{
	public int line;

	public int column;

	internal ArrayList parents;

	public string accessMod;

	public string lhs;

	public ArrayList return_type_tokens;

	public ArrayList parameter_list_tokens;

	public List<Token> throws_list;

	public Expansion Expansion;

	internal bool emptyPossible;

	internal NormalProduction[] leftExpansions;

	internal int leIndex;

	internal int walkStatus;

	public Token firstToken;

	public Token lastToken;

	protected internal string eol;

	
	protected internal virtual StringBuilder DumpPrefix(int i)
	{
		var stringBuilder = new StringBuilder(128);
		for (int j = 0; j < i; j++)
		{
			stringBuilder.Append("  ");
		}
		return stringBuilder;
	}

	
	public NormalProduction()
	{
		parents = new ();
		return_type_tokens = new ();
		parameter_list_tokens = new ();
		throws_list = new ();
		emptyPossible = false;
		leftExpansions = new NormalProduction[10];
		leIndex = 0;
		walkStatus = 0;
		eol = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
	}

	
	public new virtual StringBuilder Dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = DumpPrefix(i).Append((this.GetHashCode())).Append(' ').Append(this.GetType().Name)
			.Append(' ')
			.Append(lhs);
		if (!s.Contains(this))
		{
			s.Add(this);
			if (Expansion != null)
			{
				stringBuilder.Append(eol).Append(Expansion.Dump(i + 1, s));
			}
		}
		return stringBuilder;
	}
}
