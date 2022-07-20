namespace JavaCC.JJTree;
using System.Text;

public class JJTreeNode : SimpleNode
{
    private int ordinal = 0;
    private Token first;
    private Token last;
    private bool whitingOut = false;
    public JJTreeNode(int id) : base(id) { }
    public virtual int Ordinal { get => this.ordinal; set => this.ordinal = value; }
    public virtual Token LastToken { get => last; set => last = value; }
    public virtual Token FirstToken { get => first; set => first = value; }

    protected internal virtual void Write(Token _token, IO io)
    {
        var token = _token.SpecialToken;
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
            io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(_token)));
            return;
        }
        if (string.Equals(_token.Image, "jjtThis"))
        {
            io.Write(enclosingNodeScope.NodeVariable);
            return;
        }
        if (string.Equals(_token.Image, "jjtree") && string.Equals(_token.Next.Image, ".") && string.Equals(_token.Next.Next.Image, "currentNode") && string.Equals(_token.Next.Next.Next.Image, "(") && string.Equals(_token.Next.Next.Next.Next.Image, ")"))
        {
            whitingOut = true;
        }
        if (whitingOut)
        {
            if (string.Equals(_token.Image, "jjtree"))
            {
                io.Write(enclosingNodeScope.NodeVariable);
                io.Write(" ");
                return;
            }
            if (string.Equals(_token.Image, ")"))
            {
                io.Write(" ");
                whitingOut = false;
                return;
            }
            for (int i = 0; i < (_token.Image.Length); i++)
            {
                io.Write(" ");
            }
        }
        else
        {
            io.Write(TokenUtils.AddUnicodeEscapes(TranslateImage(_token)));
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
        for (int i = 0; i < JJTGetNumChildren(); i++)
        {
            var jJTreeNode = (JJTreeNode)JJTGetChild(i);
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

    public virtual string TranslateImage(Token token) => token.Image;

    public virtual string GetIndentation(JJTreeNode node, int idx)
    {
        var text = "";
        for (int i = idx + 1; i < node.FirstToken.BeginColumn; i++)
        {
            text += " ";
        }
        return text;
    }


    public JJTreeNode(JJTreeParser jjtp, int id)
        : this(id) { }


    public static INode JJTCreate(int i) => new JJTreeNode(i);

    public override void JJTAddChild(INode n, int i)
    {
        base.JJTAddChild(n, i);
        ((JJTreeNode)n).Ordinal = i;
    }

    public virtual int Ordinal1 => ordinal;

    internal virtual string WhiteOut(Token token)
    {
        var stringBuilder = new StringBuilder(token.Image.Length);
        for (int i = 0; i < token.Image.Length; i++)
        {
            int num = token.Image[i];
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


    internal static void OpenJJTreeComment(IO io, string comment = null)
    {
        if (comment != null)
        {
            io.Write(("/*@bgen(jjtree) ") + (comment) + (" */"));
        }
        else
        {
            io.Write("/*@bgen(jjtree)*/");
        }
    }


    internal static void CloseJJTreeComment(IO io) => io.Write("/*@egen*/");


    internal virtual string GetIndentation(JJTreeNode node) => GetIndentation(node, 0);
}