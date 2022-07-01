using System.Text;
namespace org.javacc.jjtree;

public class JJTreeNode : SimpleNode
{
	private int ordinal = 0;
	private Token first;
	private Token last;
	private bool whitingOut = false;
	public JJTreeNode(int i) : base(i) { }
    public virtual int Ordinal { get => this.ordinal; set => this.ordinal = value; }
    public virtual Token LastToken { get => last; set => last = value; }
    public virtual Token FirstToken { get => first; set => first = value; }

    protected internal virtual void Write(Token t, IO io)
	{
		var token = t.SpecialToken;
		if (token != null)
		{
			while (token.SpecialToken != null)
			{
				token = token.SpecialToken;
			}
			while (token != null)
			{
				io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(token)));
				token = token.Next;
			}
		}
		var enclosingNodeScope = NodeScope.GetEnclosingNodeScope(this);
		if (enclosingNodeScope == null)
		{
			io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(t)));
			return;
		}
		if (string.Equals(t.Image, "jjtThis"))
		{
			io.Write(enclosingNodeScope.NodeVariable);
			return;
		}
		if (string.Equals(t.Image, "jjtree") && string.Equals(t.Next.Image, ".") && string.Equals(t.Next.Next.Image, "currentNode") && string.Equals(t.Next.Next.Next.Image, "(") && string.Equals(t.Next.Next.Next.Next.Image, ")"))
		{
			whitingOut = true;
		}
		if (whitingOut)
		{
			if (string.Equals(t.Image, "jjtree"))
			{
				io.Write(enclosingNodeScope.NodeVariable);
				io.Write(" ");
				return;
			}
			if (string.Equals(t.Image, ")"))
			{
				io.Write(" ");
				whitingOut = false;
				return;
			}
			for (int i = 0; i < (t.Image.Length); i++)
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
		if (LastToken.Next == FirstToken)
		{
			return;
		}
		var firstToken = FirstToken;
		var token = new Token();
		token.Next = firstToken;
		for (int i = 0; i < jjtGetNumChildren(); i++)
		{
			var jJTreeNode = (JJTreeNode)jjtGetChild(i);
			while (true)
			{
				token = token.Next;
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
			token = token.Next;
			Write(token, io);
		}
	}

    internal virtual string TranslateImage(Token token) => token.Image;

    internal virtual string getIndentation(JJTreeNode node, int idx)
	{
		var text = "";
		for (int i = idx + 1; i < node.FirstToken.BeginColumn; i++)
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
		var stringBuilder = new StringBuilder(P_0.Image.Length);
		for (int i = 0; i < P_0.Image.Length; i++)
		{
			int num = P_0.Image[i];
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
}