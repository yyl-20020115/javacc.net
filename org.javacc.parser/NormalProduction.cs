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

	public ArrayList throws_list;

	public Expansion expansion;

	internal bool emptyPossible;

	internal NormalProduction[] leftExpansions;

	internal int leIndex;

	internal int walkStatus;

	public Token firstToken;

	public Token lastToken;

	protected internal string eol;

	
	protected internal virtual StringBuilder dumpPrefix(int i)
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
		parents = new ArrayList();
		return_type_tokens = new ArrayList();
		parameter_list_tokens = new ArrayList();
		throws_list = new ArrayList();
		emptyPossible = false;
		leftExpansions = new NormalProduction[10];
		leIndex = 0;
		walkStatus = 0;
		eol = Environment.NewLine;// java.lang.System.getProperty("line.separator", "\n");
	}

	
	public virtual StringBuilder dump(int i, HashSet<Expansion> s)
	{
		var stringBuilder = dumpPrefix(i).Append((this.GetHashCode())).Append(' ').Append(this.GetType().Name)
			.Append(' ')
			.Append(lhs);
		if (!s.Contains(this))
		{
			s.Add(this);
			if (expansion != null)
			{
				stringBuilder.Append(eol).Append(expansion.Dump(i + 1, s));
			}
		}
		return stringBuilder;
	}
}
