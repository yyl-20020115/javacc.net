using System.Text;
namespace org.javacc.jjtree;

public class JJTreeNode : SimpleNode
{
	private int ordinal = 0;
	private Token first;
	private Token last;
	private bool whitingOut = false;

	public JJTreeNode(int i)
		: base(i)
	{
		whitingOut = false;
	}
    public virtual int Ordinal { get => this.ordinal; set => this.ordinal = value; }
    public virtual Token LastToken { get => last; set => last = value; }
    public virtual Token FirstToken { get => first; set => first = value; }

    protected internal virtual void Write(Token t, IO io)
	{
		var token = t.specialToken;
		if (token != null)
		{
			while (token.specialToken != null)
			{
				token = token.specialToken;
			}
			while (token != null)
			{
				io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(token)));
				token = token.next;
			}
		}
		var enclosingNodeScope = NodeScope.getEnclosingNodeScope(this);
		if (enclosingNodeScope == null)
		{
			io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(t)));
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
			io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(t)));
		}
	}

	public virtual void Write(IO io)
	{
		if (LastToken.next == FirstToken)
		{
			return;
		}
		var firstToken = FirstToken;
		var token = new Token();
		token.next = firstToken;
		for (int i = 0; i < jjtGetNumChildren(); i++)
		{
			var jJTreeNode = (JJTreeNode)jjtGetChild(i);
			while (true)
			{
				token = token.next;
				if (token == jJTreeNode.FirstToken)
				{
					break;
				}
				Write(token, io);
			}
			jJTreeNode.Write(io);
			token = jJTreeNode.LastToken;
		}
		while (token != LastToken)
		{
			token = token.next;
			Write(token, io);
		}
	}

    internal virtual string TranslateImage(Token P_0) => P_0.image;


    internal virtual string getIndentation(JJTreeNode P_0, int P_1)
	{
		var text = "";
		for (int i = P_1 + 1; i < P_0.FirstToken.beginColumn; i++)
		{
			text += " "; 
		}
		return text;
	}


	public JJTreeNode(JJTreeParser jjtp, int i)
		: this(i) { }


    public static Node jjtCreate(int i) => new JJTreeNode(i);

    public override void jjtAddChild(Node n, int i)
	{
		base.jjtAddChild(n, i);
		((JJTreeNode)n).Ordinal = i;
	}

    public virtual int Ordinal1 => ordinal;

    internal virtual string WhiteOut(Token P_0)
	{
		var stringBuilder = new StringBuilder(P_0.image.Length);
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
		return stringBuilder.ToString();
	}


	internal static void OpenJJTreeComment(IO P_0, string P_1)
	{
		if (P_1 != null)
		{
			P_0.Write(("/*@bgen(jjtree) ")+(P_1)+(" */"));
		}
		else
		{
			P_0.Write("/*@bgen(jjtree)*/");
		}
	}


    internal static void CloseJJTreeComment(IO P_0) => P_0.Write("/*@egen*/");


    internal virtual string GetIndentation(JJTreeNode P_0) => getIndentation(P_0, 0);


    protected internal void Write(Token P_0, object P_1)
	{
		Write(P_0, (IO)P_1);
	}
}