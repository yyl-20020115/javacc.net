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

	public List<Token> return_type_tokens = new();

	public List<Token> parameter_list_tokens = new();

	public List<Token> ThrowsList = new();

	public Expansion Expansion;

	internal bool emptyPossible;

	internal NormalProduction[] leftExpansions;

	internal int leIndex;

	internal int walkStatus;

	public Token firstToken;

	public Token lastToken;

	protected internal string eol;

	
	protected internal override StringBuilder DumpPrefix(int i)
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
		ThrowsList = new ();
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
