namespace JavaCC.JJTree;
using System.Collections.Generic;
using JavaCC.Parser;

public class NodeScope
{
    private ASTProduction Production;
    private ASTNodeDescriptor nodeDescriptor;
    private string ClosedVar = "";
    private string ExceptionVar = "";
    private string NodeVar = "";
    private int ScopeNumber = 0;


    internal static NodeScope GetEnclosingNodeScope(INode _node)
    {
        if (_node is ASTBNFDeclaration n)
        {
            return n.nodeScope;
        }
        for (var node = _node.JJTGetParent(); node != null; node = node.JJTGetParent())
        {
            switch (node)
            {
                case ASTBNFDeclaration a:
                    return a.nodeScope;
                case ASTBNFNodeScope b:
                    return b.nodeScope;
                case ASTExpansionNodeScope c:
                    return c.nodeScope;
            }
        }
        return null;
    }

    internal virtual string NodeVariable => NodeVar;


    internal virtual bool IsVoid => nodeDescriptor.IsVoid;


    internal virtual void InsertCloseNodeAction(IO io, string text)
    {
        io.WriteLine((text) + ("{"));
        InsertCloseNodeCode(io, (text) + ("  "), false);
        io.WriteLine((text) + ("}"));
    }


    internal virtual string NodeDescriptorText => nodeDescriptor.Descriptor;


    internal virtual void InsertOpenNodeCode(IO io, string pre)
    {
        string nodeType = nodeDescriptor.NodeType;
        string str = (((JJTreeOptions.NodeClass.Length) <= 0 || JJTreeOptions.Multi) ? nodeType : JJTreeOptions.NodeClass);
        NodeFiles.Ensure(io, nodeType);
        io.Write((pre) + (str) + (" ")
            + (NodeVar)
            + (" = ")
            );
        string str2 = ((!Options.getStatic()) ? "this" : "null");
        string str3 = ((!JJTreeOptions.NodeUsesParser) ? "" : (str2) + (", "));
        if (string.Equals(JJTreeOptions.NodeFactory, "*"))
        {
            io.WriteLine(("(") + (str) + (")")
                + (str)
                + (".jjtCreate(")
                + (str3)
                + (nodeDescriptor.GetNodeId())
                + (");")
                );
        }
        else if ((JJTreeOptions.NodeFactory.Length) > 0)
        {
            io.WriteLine(("(") + (str) + (")")
                + (JJTreeOptions.NodeFactory)
                + (".jjtCreate(")
                + (str3)
                + (nodeDescriptor.GetNodeId())
                + (");")
                );
        }
        else
        {
            io.WriteLine(("new ") + (str) + ("(")
                + (str3)
                + (nodeDescriptor.GetNodeId())
                + (");")
                );
        }
        if (UsesCloseNodeVar)
        {
            io.WriteLine((pre) + ("boolean ") + (ClosedVar)
                + (" = true;")
                );
        }
        io.WriteLine((pre) + (nodeDescriptor.OpenNode(NodeVar)));
        if (JJTreeOptions.NodeScopeHook)
        {
            io.WriteLine((pre) + ("jjtreeOpenNodeScope(") + (NodeVar)
                + (");")
                );
        }
        if (JJTreeOptions.TrackTokens)
        {
            io.WriteLine((pre) + (NodeVar) + (".jjtSetFirstToken(getToken(1));")
                );
        }
    }

    internal virtual ASTNodeDescriptor NodeDescriptor => nodeDescriptor;


    internal virtual void TryExpansionUnit(IO io, string pre, JJTreeNode node)
    {
        io.WriteLine((pre) + ("try {"));
        JJTreeNode.CloseJJTreeComment(io);
        node.Write(io);
        JJTreeNode.OpenJJTreeComment(io, null);
        io.WriteLine();
        var dict = new Dictionary<string, string>();
        FindThrown(dict, node);
        InsertCatchBlocks(io, dict.Keys, pre);
        io.WriteLine((pre) + ("} finally {"));
        if (UsesCloseNodeVar)
        {
            io.WriteLine((pre) + ("  if (") + (ClosedVar)
                + (") {")
                );
            InsertCloseNodeCode(io, (pre) + ("    "), true);
            io.WriteLine((pre) + ("  }"));
        }
        io.WriteLine((pre) + ("}"));
        JJTreeNode.CloseJJTreeComment(io);
    }


    internal virtual void InsertOpenNodeAction(IO io, string pre)
    {
        io.WriteLine((pre) + ("{"));
        InsertOpenNodeCode(io, (pre) + ("  "));
        io.WriteLine((pre) + ("}"));
    }


    internal virtual void TryTokenSequence(IO io, string text, Token t1, Token t2)
    {
        io.WriteLine((text) + ("try {"));
        JJTreeNode.CloseJJTreeComment(io);
        for (Token token = t1; token != t2.Next; token = token.Next)
        {
            TokenUtils.Write(token, io, "jjtThis", NodeVar);
        }
        JJTreeNode.OpenJJTreeComment(io, null);
        io.WriteLine();

        InsertCatchBlocks(io, Production.ThrowsList, text);
        io.WriteLine((text) + ("} finally {"));
        if (UsesCloseNodeVar)
        {
            io.WriteLine((text) + ("  if (") + (ClosedVar)
                + (") {")
                );
            InsertCloseNodeCode(io, (text) + ("    "), true);
            io.WriteLine((text) + ("  }"));
        }
        io.WriteLine((text) + ("}"));
        JJTreeNode.CloseJJTreeComment(io);
    }


    internal NodeScope(ASTProduction astp, ASTNodeDescriptor astn)
    {
        Production = astp;
        if (astn == null)
        {
            string text = Production.Name;
            if (JJTreeOptions.NodeDefaultVoid)
            {
                text = "void";
            }
            nodeDescriptor = ASTNodeDescriptor.Indefinite(text);
        }
        else
        {
            nodeDescriptor = astn;
        }
        ScopeNumber = Production.GetNodeScopeNumber(this);
        NodeVar = ConstructVariable("n");
        ClosedVar = ConstructVariable("c");
        ExceptionVar = ConstructVariable("e");
    }


    private string ConstructVariable(string name)
    {
        string @this = ("000") + (ScopeNumber);
        return ("jjt") + (name) + (@this.Substring(@this.Length - 3, @this.Length));
    }

    internal virtual bool UsesCloseNodeVar => true;


    internal virtual void InsertCloseNodeCode(IO io, string pre, bool assigned)
    {
        io.WriteLine((pre) + (nodeDescriptor.CloseNode(NodeVar)));
        if (UsesCloseNodeVar&& !assigned)
        {
            io.WriteLine((pre) + (ClosedVar) + (" = false;")
                );
        }
        if (JJTreeOptions.NodeScopeHook)
        {
            io.WriteLine((pre) + ("jjtreeCloseNodeScope(") + (NodeVar)
                + (");")
                );
        }
        if (JJTreeOptions.TrackTokens)
        {
            io.WriteLine((pre) + (NodeVar) + (".jjtSetLastToken(getToken(0));")
                );
        }
    }


    private void InsertCatchBlocks(IO io, IEnumerable<string> texts, string pre)
    {
        var em = texts.GetEnumerator();
        if (em.MoveNext())
        {
            io.WriteLine((pre) + ("} catch (Throwable ") + (ExceptionVar)
                + (") {")
                );
            if (UsesCloseNodeVar)
            {
                io.WriteLine((pre) + ("  if (") + (ClosedVar)
                    + (") {")
                    );
                io.WriteLine((pre) + ("    jjtree.clearNodeScope(") + (NodeVar)
                    + (");")
                    );
                io.WriteLine((pre) + ("    ") + (ClosedVar)
                    + (" = false;")
                    );
                io.WriteLine((pre) + ("  } else {"));
                io.WriteLine((pre) + ("    jjtree.popNode();"));
                io.WriteLine((pre) + ("  }"));
            }
            while (em.MoveNext())
            {
                string str = em.Current;
                io.WriteLine((pre) + ("  if (") + (ExceptionVar)
                    + (" instanceof ")
                    + (str)
                    + (") {")
                    );
                io.WriteLine((pre) + ("    throw (") + (str)
                    + (")")
                    + (ExceptionVar)
                    + (";")
                    );
                io.WriteLine((pre) + ("  }"));
            }
            io.WriteLine((pre) + ("  throw ") + (ExceptionVar)
                + (";")
                );
        }
    }


    private static void FindThrown(Dictionary<string, string> dict, JJTreeNode node)
    {
        if (node is ASTBNFNonTerminal)
        {
            string image = node.FirstToken.Image;
            //ASTProduction aSTProduction = (ASTProduction)JJTreeGlobals.Productions.get(image);
            if (JJTreeGlobals.Productions.TryGetValue(image, out var aSTProduction))
            {
                foreach (var t in aSTProduction.ThrowsList)
                {
                    dict.Add(t, t);
                }
            }
        }
        for (int i = 0; i < node.JJTGetNumChildren(); i++)
        {
            JJTreeNode jJTreeNode = (JJTreeNode)node.JJTGetChild(i);
            FindThrown(dict, jJTreeNode);
        }
    }


    internal virtual void InsertOpenNodeDeclaration(IO io, string pre)
    {
        InsertOpenNodeCode(io, pre);
    }
}
