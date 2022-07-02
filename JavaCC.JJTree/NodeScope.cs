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


    internal static NodeScope GetEnclosingNodeScope(Node _node)
    {
        if (_node is ASTBNFDeclaration n)
        {
            return n.nodeScope;
        }
        for (var node = _node.jjtGetParent(); node != null; node = node.jjtGetParent())
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


    internal virtual void insertCloseNodeAction(IO io, string text)
    {
        io.WriteLine((text) + ("{"));
        insertCloseNodeCode(io, (text) + ("  "), false);
        io.WriteLine((text) + ("}"));
    }


    internal virtual string NodeDescriptorText => nodeDescriptor.Descriptor;


    internal virtual void insertOpenNodeCode(IO io, string P_1)
    {
        string nodeType = nodeDescriptor.NodeType;
        string str = (((JJTreeOptions.NodeClass.Length) <= 0 || JJTreeOptions.Multi) ? nodeType : JJTreeOptions.NodeClass);
        NodeFiles.ensure(io, nodeType);
        io.Write((P_1) + (str) + (" ")
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
        if (usesCloseNodeVar())
        {
            io.WriteLine((P_1) + ("boolean ") + (ClosedVar)
                + (" = true;")
                );
        }
        io.WriteLine((P_1) + (nodeDescriptor.OpenNode(NodeVar)));
        if (JJTreeOptions.NodeScopeHook)
        {
            io.WriteLine((P_1) + ("jjtreeOpenNodeScope(") + (NodeVar)
                + (");")
                );
        }
        if (JJTreeOptions.TrackTokens)
        {
            io.WriteLine((P_1) + (NodeVar) + (".jjtSetFirstToken(getToken(1));")
                );
        }
    }

    internal virtual ASTNodeDescriptor NodeDescriptor => nodeDescriptor;


    internal virtual void tryExpansionUnit(IO P_0, string P_1, JJTreeNode P_2)
    {
        P_0.WriteLine((P_1) + ("try {"));
        JJTreeNode.CloseJJTreeComment(P_0);
        P_2.Write(P_0);
        JJTreeNode.OpenJJTreeComment(P_0, null);
        P_0.WriteLine();
        var dict = new Dictionary<string, string>();
        findThrown(dict, P_2);
        insertCatchBlocks(P_0, dict.Keys, P_1);
        P_0.WriteLine((P_1) + ("} finally {"));
        if (usesCloseNodeVar())
        {
            P_0.WriteLine((P_1) + ("  if (") + (ClosedVar)
                + (") {")
                );
            insertCloseNodeCode(P_0, (P_1) + ("    "), true);
            P_0.WriteLine((P_1) + ("  }"));
        }
        P_0.WriteLine((P_1) + ("}"));
        JJTreeNode.CloseJJTreeComment(P_0);
    }


    internal virtual void insertOpenNodeAction(IO P_0, string P_1)
    {
        P_0.WriteLine((P_1) + ("{"));
        insertOpenNodeCode(P_0, (P_1) + ("  "));
        P_0.WriteLine((P_1) + ("}"));
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

        insertCatchBlocks(io, Production.ThrowsList, text);
        io.WriteLine((text) + ("} finally {"));
        if (usesCloseNodeVar())
        {
            io.WriteLine((text) + ("  if (") + (ClosedVar)
                + (") {")
                );
            insertCloseNodeCode(io, (text) + ("    "), true);
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
        NodeVar = constructVariable("n");
        ClosedVar = constructVariable("c");
        ExceptionVar = constructVariable("e");
    }


    private string constructVariable(string P_0)
    {
        string @this = ("000") + (ScopeNumber);
        return ("jjt") + (P_0) + (@this.Substring(@this.Length - 3, @this.Length));
    }

    internal virtual bool usesCloseNodeVar()
    {
        return true;
    }


    internal virtual void insertCloseNodeCode(IO P_0, string P_1, bool P_2)
    {
        P_0.WriteLine((P_1) + (nodeDescriptor.CloseNode(NodeVar)));
        if (usesCloseNodeVar() && !P_2)
        {
            P_0.WriteLine((P_1) + (ClosedVar) + (" = false;")
                );
        }
        if (JJTreeOptions.NodeScopeHook)
        {
            P_0.WriteLine((P_1) + ("jjtreeCloseNodeScope(") + (NodeVar)
                + (");")
                );
        }
        if (JJTreeOptions.TrackTokens)
        {
            P_0.WriteLine((P_1) + (NodeVar) + (".jjtSetLastToken(getToken(0));")
                );
        }
    }


    private void insertCatchBlocks(IO io, IEnumerable<string> P_1, string P_2)
    {
        var em = P_1.GetEnumerator();
        if (em.MoveNext())
        {
            io.WriteLine((P_2) + ("} catch (Throwable ") + (ExceptionVar)
                + (") {")
                );
            if (usesCloseNodeVar())
            {
                io.WriteLine((P_2) + ("  if (") + (ClosedVar)
                    + (") {")
                    );
                io.WriteLine((P_2) + ("    jjtree.clearNodeScope(") + (NodeVar)
                    + (");")
                    );
                io.WriteLine((P_2) + ("    ") + (ClosedVar)
                    + (" = false;")
                    );
                io.WriteLine((P_2) + ("  } else {"));
                io.WriteLine((P_2) + ("    jjtree.popNode();"));
                io.WriteLine((P_2) + ("  }"));
            }
            while (em.MoveNext())
            {
                string str = em.Current;
                io.WriteLine((P_2) + ("  if (") + (ExceptionVar)
                    + (" instanceof ")
                    + (str)
                    + (") {")
                    );
                io.WriteLine((P_2) + ("    throw (") + (str)
                    + (")")
                    + (ExceptionVar)
                    + (";")
                    );
                io.WriteLine((P_2) + ("  }"));
            }
            io.WriteLine((P_2) + ("  throw ") + (ExceptionVar)
                + (";")
                );
        }
    }


    private static void findThrown(Dictionary<string, string> dict, JJTreeNode node)
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
        for (int i = 0; i < node.jjtGetNumChildren(); i++)
        {
            JJTreeNode jJTreeNode = (JJTreeNode)node.jjtGetChild(i);
            findThrown(dict, jJTreeNode);
        }
    }


    internal virtual void insertOpenNodeDeclaration(IO P_0, string P_1)
    {
        insertOpenNodeCode(P_0, P_1);
    }
}
