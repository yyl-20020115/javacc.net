using System.Text;
namespace org.javacc.jjtree;

public class JJTreeNode : SimpleNode
{
	private int myOrdinal;

	private Token first;

	private Token last;

	private bool whitingOut;


	public JJTreeNode(int i)
		: base(i)
	{
		whitingOut = false;
	}

	public virtual void setOrdinal(int i)
	{
		myOrdinal = i;
	}

	public virtual Token getLastToken()
	{
		return last;
	}

	public virtual Token getFirstToken()
	{
		return first;
	}


	protected internal virtual void Write(Token t, IO io)
	{
		Token token = t.specialToken;
		if (token != null)
		{
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				io.Write(TokenUtils.addUnicodeEscapes(translateImage(token)));
				token = token.next;
			}
		}
		NodeScope enclosingNodeScope = NodeScope.getEnclosingNodeScope(this);
		if (enclosingNodeScope == null)
		{
			io.Write(TokenUtils.addUnicodeEscapes(translateImage(t)));
			return;
		}
		if (string.Equals(t.image, "jjtThis"))
		{
			io.Write(enclosingNodeScope.getNodeVariable());
			return;
		}
		if (string.Equals(t.image, "jjtree") && string.Equals(t.next.image, ".") && string.Equals(t.next.next.image, "currentNode") && string.Equals(t.next.next.next.image, "(") && string.Equals(t.next.next.next.next.image, ")"))
		{
			whitingOut = true;
		}
		if (whitingOut)
		{
			if (string.Equals(t.image, "jjtree"))
			{
				io.Write(enclosingNodeScope.getNodeVariable());
				io.Write(" ");
				return;
			}
			if (string.Equals(t.image, ")"))
			{
				io.Write(" ");
				whitingOut = false;
				return;
			}
			for (int i = 0; i < (t.image.Length); i++)
			{
				io.Write(" ");
			}
		}
		else
		{
			io.Write(TokenUtils.addUnicodeEscapes(translateImage(t)));
		}
	}


	public virtual void Write(IO io)
	{
		if (getLastToken().next == getFirstToken())
		{
			return;
		}
		Token firstToken = getFirstToken();
		Token token = new Token();
		token.next = firstToken;
		for (int i = 0; i < jjtGetNumChildren(); i++)
		{
			JJTreeNode jJTreeNode = (JJTreeNode)jjtGetChild(i);
			while (true)
			{
				token = token.next;
				if (token == jJTreeNode.getFirstToken())
				{
					break;
				}
				Write(token, io);
			}
			jJTreeNode.Write(io);
			token = jJTreeNode.getLastToken();
		}
		while (token != getLastToken())
		{
			token = token.next;
			Write(token, io);
		}
	}

	internal virtual string translateImage(Token P_0)
	{
		return P_0.image;
	}


	internal virtual string getIndentation(JJTreeNode P_0, int P_1)
	{
		string text = "";
		for (int i = P_1 + 1; i < P_0.getFirstToken().beginColumn; i++)
		{
			text = new StringBuilder().Append(text).Append(" ").ToString();
		}
		return text;
	}


	public JJTreeNode(JJTreeParser jjtp, int i)
		: this(i)
	{
	}


	public static Node jjtCreate(int i)
	{
		JJTreeNode result = new JJTreeNode(i);

		return result;
	}


	public override void jjtAddChild(Node n, int i)
	{
		base.jjtAddChild(n, i);
		((JJTreeNode)n).setOrdinal(i);
	}

	public virtual int getOrdinal()
	{
		return myOrdinal;
	}

	public virtual void setFirstToken(Token t)
	{
		first = t;
	}

	public virtual void setLastToken(Token t)
	{
		last = t;
	}


	internal virtual string whiteOut(Token P_0)
	{
		;
		StringBuilder stringBuilder = new StringBuilder(P_0.image.Length);
		for (int i = 0; i < P_0.image.Length; i++)
		{
			int num = P_0.image[i];
			if (num != 9 && num != 10 && num != 13 && num != 12)
			{
				stringBuilder.Append(' ');
			}
			else
			{
				stringBuilder.Append((char)num);
			}
		}
		string result = stringBuilder.ToString();

		return result;
	}


	internal static void openJJTreeComment(IO P_0, string P_1)
	{
		if (P_1 != null)
		{
			P_0.Write(new StringBuilder().Append("/*@bgen(jjtree) ").Append(P_1).Append(" */")
				.ToString());
		}
		else
		{
			P_0.Write("/*@bgen(jjtree)*/");
		}
	}


	internal static void closeJJTreeComment(IO P_0)
	{
		P_0.Write("/*@egen*/");
	}


	internal virtual string getIndentation(JJTreeNode P_0)
	{
		string indentation = getIndentation(P_0, 0);

		return indentation;
	}


	public void Write(object P_0)
	{
		Write((IO)P_0);
	}


	protected internal void _003Cnonvirtual_003E0(object P_0)
	{
		Write((IO)P_0);
	}


	protected internal void Write(Token P_0, object P_1)
	{
		Write(P_0, (IO)P_1);
	}

}