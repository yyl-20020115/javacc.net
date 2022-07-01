using javacc.net;
using System.Collections;
using System.IO;
using System.Text;

namespace org.javacc.parser;


public class ParseGen : JavaCCParserConstants // JavaCCGlobals, 
{
	private static TextWriter ostr;

	
	public static void start()
	{
		Token token = null;
		if (JavaCCErrors._Error_Count != 0)
		{
			
			throw new MetaParseException();
		}
		if (!Options.getBuildParser())
		{
			return;
		}
		try
		{
			
			ostr = new StreamWriter(Path.Combine(Options.getOutputDirectory().FullName, (JavaCCGlobals.cu_name)+(".java")));
		}
		catch (IOException)
		{
			goto IL_006c;
		}
		ArrayList vector = (ArrayList)JavaCCGlobals.toolNames.Clone();
		vector.Add("JavaCC");
		ostr.WriteLine(new StringBuilder().Append("/* ").Append(JavaCCGlobals.getIdString(vector, new StringBuilder().Append(JavaCCGlobals.cu_name).Append(".java").ToString())).Append(" */")
			.ToString());
		int num = 0;
		if (JavaCCGlobals.cu_to_insertion_point_1.Count != 0)
		{
			JavaCCGlobals.printTokenSetup((Token)JavaCCGlobals.cu_to_insertion_point_1[0]);
			JavaCCGlobals.ccol = 1;
			Enumeration enumeration = JavaCCGlobals.cu_to_insertion_point_1.elements();
			while (enumeration.hasMoreElements())
			{
				token = (Token)enumeration.nextElement();
				if (token.kind == 51)
				{
					num = 1;
				}
				else if (token.kind == 35)
				{
					num = 0;
				}
				JavaCCGlobals.printToken(token, ostr);
			}
		}
		if (num != 0)
		{
			ostr.Write(", ");
		}
		else
		{
			ostr.Write(" implements ");
		}
		ostr.Write(new StringBuilder().Append(JavaCCGlobals.cu_name).Append("Constants ").ToString());
		if (JavaCCGlobals.cu_to_insertion_point_2.Count != 0)
		{
			JavaCCGlobals.printTokenSetup((Token)JavaCCGlobals.cu_to_insertion_point_2[0]);
			Enumeration enumeration = JavaCCGlobals.cu_to_insertion_point_2.elements();
			while (enumeration.hasMoreElements())
			{
				token = (Token)enumeration.nextElement();
				JavaCCGlobals.printToken(token, ostr);
			}
		}
		ostr.WriteLine("");
		ostr.WriteLine("");
		ParseEngine.build(ostr);
		if (Options.getStatic())
		{
			ostr.WriteLine("  static private boolean jj_initialized_once = false;");
		}
		if (Options.getUserTokenManager())
		{
			ostr.WriteLine("  /** User defined Token Manager. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public TokenManager token_source;")
				.ToString());
		}
		else
		{
			ostr.WriteLine("  /** Generated Token Manager. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public ")
				.Append(JavaCCGlobals.cu_name)
				.Append("TokenManager token_source;")
				.ToString());
			if (!Options.getUserCharStream())
			{
				if (Options.getJavaUnicodeEscape())
				{
					ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("JavaCharStream jj_input_stream;")
						.ToString());
				}
				else
				{
					ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("SimpleCharStream jj_input_stream;")
						.ToString());
				}
			}
		}
		ostr.WriteLine("  /** Current token. */");
		ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public Token token;")
			.ToString());
		ostr.WriteLine("  /** Next token. */");
		ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public Token jj_nt;")
			.ToString());
		if (!Options.getCacheTokens())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_ntk;")
				.ToString());
		}
		if (JavaCCGlobals.jj2index != 0)
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private Token jj_scanpos, jj_lastpos;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_la;")
				.ToString());
			ostr.WriteLine("  /** Whether we are looking ahead. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private boolean jj_lookingAhead = false;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private boolean jj_semLA;")
				.ToString());
		}
		if (Options.getErrorReporting())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_gen;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final private int[] jj_la1 = new int[")
				.Append(JavaCCGlobals.maskindex)
				.Append("];")
				.ToString());
			int num2 = (JavaCCGlobals.tokenCount - 1) / 32 + 1;
			for (int i = 0; i < num2; i++)
			{
				ostr.WriteLine(new StringBuilder().Append("  static private int[] jj_la1_").Append(i).Append(";")
					.ToString());
			}
			ostr.WriteLine("  static {");
			for (int i = 0; i < num2; i++)
			{
				ostr.WriteLine(new StringBuilder().Append("      jj_la1_init_").Append(i).Append("();")
					.ToString());
			}
			ostr.WriteLine("   }");
			for (int i = 0; i < num2; i++)
			{
				ostr.WriteLine(new StringBuilder().Append("   private static void jj_la1_init_").Append(i).Append("() {")
					.ToString());
				ostr.Write(new StringBuilder().Append("      jj_la1_").Append(i).Append(" = new int[] {")
					.ToString());
				Enumeration enumeration2 = JavaCCGlobals.maskVals.elements();
				while (enumeration2.hasMoreElements())
				{
					int[] array = (int[])enumeration2.nextElement();
					ostr.Write(new StringBuilder().Append("0x").Append(Utils.ToString(array[i],16)).Append(",")
						.ToString());
				}
				ostr.WriteLine("};");
				ostr.WriteLine("   }");
			}
		}
		if (JavaCCGlobals.jj2index != 0 && Options.getErrorReporting())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final private JJCalls[] jj_2_rtns = new JJCalls[")
				.Append(JavaCCGlobals.jj2index)
				.Append("];")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private boolean jj_rescan = false;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_gc = 0;")
				.ToString());
		}
		ostr.WriteLine("");
		if (!Options.getUserTokenManager())
		{
			if (Options.getUserCharStream())
			{
				ostr.WriteLine("  /** Constructor with user supplied CharStream. */");
				ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(CharStream stream) {")
					.ToString());
				if (Options.getStatic())
				{
					ostr.WriteLine("    if (jj_initialized_once) {");
					ostr.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser.  \");");
					ostr.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					ostr.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					ostr.WriteLine("      throw new System.Exception();");
					ostr.WriteLine("    }");
					ostr.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.getTokenManagerUsesParser() && !Options.getStatic())
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(this, stream);")
						.ToString());
				}
				else
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(stream);")
						.ToString());
				}
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
				ostr.WriteLine("");
				ostr.WriteLine("  /** Reinitialise. */");
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public void ReInit(CharStream stream) {")
					.ToString());
				ostr.WriteLine("    token_source.ReInit(stream);");
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				ostr.WriteLine("    jj_lookingAhead = false;");
				if (JavaCCGlobals.jjtreeGenerated)
				{
					ostr.WriteLine("    jjtree.reset();");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
			}
			else
			{
				ostr.WriteLine("  /** Constructor with Stream. */");
				ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(java.io.Stream stream) {")
					.ToString());
				ostr.WriteLine("     this(stream, null);");
				ostr.WriteLine("  }");
				ostr.WriteLine("  /** Constructor with Stream and supplied encoding */");
				ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(java.io.Stream stream, String encoding) {")
					.ToString());
				if (Options.getStatic())
				{
					ostr.WriteLine("    if (jj_initialized_once) {");
					ostr.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser.  \");");
					ostr.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					ostr.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					ostr.WriteLine("      throw new System.Exception();");
					ostr.WriteLine("    }");
					ostr.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.getJavaUnicodeEscape())
				{
					if (string.Equals(Options.getJdkVersion(), "1.3"))
					{
						ostr.WriteLine("    try { jj_input_stream = new JavaCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
					}
					else
					{
						ostr.WriteLine("    try { jj_input_stream = new JavaCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
					}
				}
				else if (string.Equals(Options.getJdkVersion(), "1.3"))
				{
					ostr.WriteLine("    try { jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
				}
				else
				{
					ostr.WriteLine("    try { jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
				}
				if (Options.getTokenManagerUsesParser() && !Options.getStatic())
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(this, jj_input_stream);")
						.ToString());
				}
				else
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(jj_input_stream);")
						.ToString());
				}
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
				ostr.WriteLine("");
				ostr.WriteLine("  /** Reinitialise. */");
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public void ReInit(java.io.Stream stream) {")
					.ToString());
				ostr.WriteLine("     ReInit(stream, null);");
				ostr.WriteLine("  }");
				ostr.WriteLine("  /** Reinitialise. */");
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public void ReInit(java.io.Stream stream, String encoding) {")
					.ToString());
				if (string.Equals(Options.getJdkVersion(), "1.3"))
				{
					ostr.WriteLine("    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
				}
				else
				{
					ostr.WriteLine("    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
				}
				ostr.WriteLine("    token_source.ReInit(jj_input_stream);");
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				if (JavaCCGlobals.jjtreeGenerated)
				{
					ostr.WriteLine("    jjtree.reset();");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
				ostr.WriteLine("");
				ostr.WriteLine("  /** Constructor. */");
				ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(java.io.TextReader stream) {")
					.ToString());
				if (Options.getStatic())
				{
					ostr.WriteLine("    if (jj_initialized_once) {");
					ostr.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser. \");");
					ostr.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					ostr.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					ostr.WriteLine("      throw new System.Exception();");
					ostr.WriteLine("    }");
					ostr.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.getJavaUnicodeEscape())
				{
					ostr.WriteLine("    jj_input_stream = new JavaCharStream(stream, 1, 1);");
				}
				else
				{
					ostr.WriteLine("    jj_input_stream = new SimpleCharStream(stream, 1, 1);");
				}
				if (Options.getTokenManagerUsesParser() && !Options.getStatic())
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(this, jj_input_stream);")
						.ToString());
				}
				else
				{
					ostr.WriteLine(new StringBuilder().Append("    token_source = new ").Append(JavaCCGlobals.cu_name).Append("TokenManager(jj_input_stream);")
						.ToString());
				}
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
				ostr.WriteLine("");
				ostr.WriteLine("  /** Reinitialise. */");
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public void ReInit(java.io.TextReader stream) {")
					.ToString());
				if (Options.getJavaUnicodeEscape())
				{
					ostr.WriteLine("    jj_input_stream.ReInit(stream, 1, 1);");
				}
				else
				{
					ostr.WriteLine("    jj_input_stream.ReInit(stream, 1, 1);");
				}
				ostr.WriteLine("    token_source.ReInit(jj_input_stream);");
				ostr.WriteLine("    token = new Token();");
				if (Options.getCacheTokens())
				{
					ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					ostr.WriteLine("    jj_ntk = -1;");
				}
				if (JavaCCGlobals.jjtreeGenerated)
				{
					ostr.WriteLine("    jjtree.reset();");
				}
				if (Options.getErrorReporting())
				{
					ostr.WriteLine("    jj_gen = 0;");
					ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
						.ToString());
					if (JavaCCGlobals.jj2index != 0)
					{
						ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				ostr.WriteLine("  }");
			}
		}
		ostr.WriteLine("");
		if (Options.getUserTokenManager())
		{
			ostr.WriteLine("  /** Constructor with user supplied Token Manager. */");
			ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(TokenManager tm) {")
				.ToString());
		}
		else
		{
			ostr.WriteLine("  /** Constructor with generated Token Manager. */");
			ostr.WriteLine(new StringBuilder().Append("  public ").Append(JavaCCGlobals.cu_name).Append("(")
				.Append(JavaCCGlobals.cu_name)
				.Append("TokenManager tm) {")
				.ToString());
		}
		if (Options.getStatic())
		{
			ostr.WriteLine("    if (jj_initialized_once) {");
			ostr.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser. \");");
			ostr.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
			ostr.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
			ostr.WriteLine("      throw new System.Exception();");
			ostr.WriteLine("    }");
			ostr.WriteLine("    jj_initialized_once = true;");
		}
		ostr.WriteLine("    token_source = tm;");
		ostr.WriteLine("    token = new Token();");
		if (Options.getCacheTokens())
		{
			ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
		}
		else
		{
			ostr.WriteLine("    jj_ntk = -1;");
		}
		if (Options.getErrorReporting())
		{
			ostr.WriteLine("    jj_gen = 0;");
			ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
				.ToString());
			if (JavaCCGlobals.jj2index != 0)
			{
				ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
			}
		}
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		if (Options.getUserTokenManager())
		{
			ostr.WriteLine("  /** Reinitialise. */");
			ostr.WriteLine("  public void ReInit(TokenManager tm) {");
		}
		else
		{
			ostr.WriteLine("  /** Reinitialise. */");
			ostr.WriteLine(new StringBuilder().Append("  public void ReInit(").Append(JavaCCGlobals.cu_name).Append("TokenManager tm) {")
				.ToString());
		}
		ostr.WriteLine("    token_source = tm;");
		ostr.WriteLine("    token = new Token();");
		if (Options.getCacheTokens())
		{
			ostr.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
		}
		else
		{
			ostr.WriteLine("    jj_ntk = -1;");
		}
		if (JavaCCGlobals.jjtreeGenerated)
		{
			ostr.WriteLine("    jjtree.reset();");
		}
		if (Options.getErrorReporting())
		{
			ostr.WriteLine("    jj_gen = 0;");
			ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) jj_la1[i] = -1;")
				.ToString());
			if (JavaCCGlobals.jj2index != 0)
			{
				ostr.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
			}
		}
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private Token jj_consume_token(int kind) throws ParseException {")
			.ToString());
		if (Options.getCacheTokens())
		{
			ostr.WriteLine("    Token oldToken = token;");
			ostr.WriteLine("    if ((token = jj_nt).next != null) jj_nt = jj_nt.next;");
			ostr.WriteLine("    else jj_nt = jj_nt.next = token_source.getNextToken();");
		}
		else
		{
			ostr.WriteLine("    Token oldToken;");
			ostr.WriteLine("    if ((oldToken = token).next != null) token = token.next;");
			ostr.WriteLine("    else token = token.next = token_source.getNextToken();");
			ostr.WriteLine("    jj_ntk = -1;");
		}
		ostr.WriteLine("    if (token.kind == kind) {");
		if (Options.getErrorReporting())
		{
			ostr.WriteLine("      jj_gen++;");
			if (JavaCCGlobals.jj2index != 0)
			{
				ostr.WriteLine("      if (++jj_gc > 100) {");
				ostr.WriteLine("        jj_gc = 0;");
				ostr.WriteLine("        for (int i = 0; i < jj_2_rtns.length; i++) {");
				ostr.WriteLine("          JJCalls c = jj_2_rtns[i];");
				ostr.WriteLine("          while (c != null) {");
				ostr.WriteLine("            if (c.gen < jj_gen) c.first = null;");
				ostr.WriteLine("            c = c.next;");
				ostr.WriteLine("          }");
				ostr.WriteLine("        }");
				ostr.WriteLine("      }");
			}
		}
		if (Options.getDebugParser())
		{
			ostr.WriteLine("      trace_token(token, \"\");");
		}
		ostr.WriteLine("      return token;");
		ostr.WriteLine("    }");
		if (Options.getCacheTokens())
		{
			ostr.WriteLine("    jj_nt = token;");
		}
		ostr.WriteLine("    token = oldToken;");
		if (Options.getErrorReporting())
		{
			ostr.WriteLine("    jj_kind = kind;");
		}
		ostr.WriteLine("    throw generateParseException();");
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		if (JavaCCGlobals.jj2index != 0)
		{
			ostr.WriteLine("  static private final class LookaheadSuccess extends java.lang.Error { }");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final private LookaheadSuccess jj_ls = new LookaheadSuccess();")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private boolean jj_scan_token(int kind) {")
				.ToString());
			ostr.WriteLine("    if (jj_scanpos == jj_lastpos) {");
			ostr.WriteLine("      jj_la--;");
			ostr.WriteLine("      if (jj_scanpos.next == null) {");
			ostr.WriteLine("        jj_lastpos = jj_scanpos = jj_scanpos.next = token_source.getNextToken();");
			ostr.WriteLine("      } else {");
			ostr.WriteLine("        jj_lastpos = jj_scanpos = jj_scanpos.next;");
			ostr.WriteLine("      }");
			ostr.WriteLine("    } else {");
			ostr.WriteLine("      jj_scanpos = jj_scanpos.next;");
			ostr.WriteLine("    }");
			if (Options.getErrorReporting())
			{
				ostr.WriteLine("    if (jj_rescan) {");
				ostr.WriteLine("      int i = 0; Token tok = token;");
				ostr.WriteLine("      while (tok != null && tok != jj_scanpos) { i++; tok = tok.next; }");
				ostr.WriteLine("      if (tok != null) jj_add_error_token(kind, i);");
				if (Options.getDebugLookahead())
				{
					ostr.WriteLine("    } else {");
					ostr.WriteLine("      trace_scan(jj_scanpos, kind);");
				}
				ostr.WriteLine("    }");
			}
			else if (Options.getDebugLookahead())
			{
				ostr.WriteLine("    trace_scan(jj_scanpos, kind);");
			}
			ostr.WriteLine("    if (jj_scanpos.kind != kind) return true;");
			ostr.WriteLine("    if (jj_la == 0 && jj_scanpos == jj_lastpos) throw jj_ls;");
			ostr.WriteLine("    return false;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		ostr.WriteLine("");
		ostr.WriteLine("/** Get the next Token. */");
		ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public Token getNextToken() {")
			.ToString());
		if (Options.getCacheTokens())
		{
			ostr.WriteLine("    if ((token = jj_nt).next != null) jj_nt = jj_nt.next;");
			ostr.WriteLine("    else jj_nt = jj_nt.next = token_source.getNextToken();");
		}
		else
		{
			ostr.WriteLine("    if (token.next != null) token = token.next;");
			ostr.WriteLine("    else token = token.next = token_source.getNextToken();");
			ostr.WriteLine("    jj_ntk = -1;");
		}
		if (Options.getErrorReporting())
		{
			ostr.WriteLine("    jj_gen++;");
		}
		if (Options.getDebugParser())
		{
			ostr.WriteLine("      trace_token(token, \" (in getNextToken)\");");
		}
		ostr.WriteLine("    return token;");
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		ostr.WriteLine("/** Get the specific Token. */");
		ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public Token getToken(int index) {")
			.ToString());
		if (JavaCCGlobals.jj2index != 0)
		{
			ostr.WriteLine("    Token t = jj_lookingAhead ? jj_scanpos : token;");
		}
		else
		{
			ostr.WriteLine("    Token t = token;");
		}
		ostr.WriteLine("    for (int i = 0; i < index; i++) {");
		ostr.WriteLine("      if (t.next != null) t = t.next;");
		ostr.WriteLine("      else t = t.next = token_source.getNextToken();");
		ostr.WriteLine("    }");
		ostr.WriteLine("    return t;");
		ostr.WriteLine("  }");
		ostr.WriteLine("");
		if (!Options.getCacheTokens())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_ntk() {")
				.ToString());
			ostr.WriteLine("    if ((jj_nt=token.next) == null)");
			ostr.WriteLine("      return (jj_ntk = (token.next=token_source.getNextToken()).kind);");
			ostr.WriteLine("    else");
			ostr.WriteLine("      return (jj_ntk = jj_nt.kind);");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		if (Options.getErrorReporting())
		{
			if (!string.Equals(Options.getJdkVersion(), "1.5"))
			{
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private java.util.List jj_expentries = new java.util.ArrayList();")
					.ToString());
			}
			else
			{
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private java.util.List<int[]> jj_expentries = new java.util.ArrayList<int[]>();")
					.ToString());
			}
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int[] jj_expentry;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_kind = -1;")
				.ToString());
			if (JavaCCGlobals.jj2index != 0)
			{
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int[] jj_lasttokens = new int[100];")
					.ToString());
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int jj_endpos;")
					.ToString());
				ostr.WriteLine("");
				ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void jj_add_error_token(int kind, int pos) {")
					.ToString());
				ostr.WriteLine("    if (pos >= 100) return;");
				ostr.WriteLine("    if (pos == jj_endpos + 1) {");
				ostr.WriteLine("      jj_lasttokens[jj_endpos++] = kind;");
				ostr.WriteLine("    } else if (jj_endpos != 0) {");
				ostr.WriteLine("      jj_expentry = new int[jj_endpos];");
				ostr.WriteLine("      for (int i = 0; i < jj_endpos; i++) {");
				ostr.WriteLine("        jj_expentry[i] = jj_lasttokens[i];");
				ostr.WriteLine("      }");
				ostr.WriteLine("      boolean exists = false;");
				ostr.WriteLine("      for (java.util.Iterator it = jj_expentries.iterator(); it.hasNext();) {");
				ostr.WriteLine("        int[] oldentry = (int[])(it.next());");
				ostr.WriteLine("        if (oldentry.length == jj_expentry.length) {");
				ostr.WriteLine("          exists = true;");
				ostr.WriteLine("          for (int i = 0; i < jj_expentry.length; i++) {");
				ostr.WriteLine("            if (oldentry[i] != jj_expentry[i]) {");
				ostr.WriteLine("              exists = false;");
				ostr.WriteLine("              break;");
				ostr.WriteLine("            }");
				ostr.WriteLine("          }");
				ostr.WriteLine("          if (exists) break;");
				ostr.WriteLine("        }");
				ostr.WriteLine("      }");
				ostr.WriteLine("      if (!exists) jj_expentries.Add(jj_expentry);");
				ostr.WriteLine("      if (pos != 0) jj_lasttokens[(jj_endpos = pos) - 1] = kind;");
				ostr.WriteLine("    }");
				ostr.WriteLine("  }");
			}
			ostr.WriteLine("");
			ostr.WriteLine("  /** Generate ParseException. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public ParseException generateParseException() {")
				.ToString());
			ostr.WriteLine("    jj_expentries.Clear();");
			ostr.WriteLine(new StringBuilder().Append("    boolean[] la1tokens = new boolean[").Append(JavaCCGlobals.tokenCount).Append("];")
				.ToString());
			ostr.WriteLine("    if (jj_kind >= 0) {");
			ostr.WriteLine("      la1tokens[jj_kind] = true;");
			ostr.WriteLine("      jj_kind = -1;");
			ostr.WriteLine("    }");
			ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.maskindex).Append("; i++) {")
				.ToString());
			ostr.WriteLine("      if (jj_la1[i] == jj_gen) {");
			ostr.WriteLine("        for (int j = 0; j < 32; j++) {");
			for (int num2 = 0; num2 < (JavaCCGlobals.tokenCount - 1) / 32 + 1; num2++)
			{
				ostr.WriteLine(new StringBuilder().Append("          if ((jj_la1_").Append(num2).Append("[i] & (1<<j)) != 0) {")
					.ToString());
				ostr.Write("            la1tokens[");
				if (num2 != 0)
				{
					ostr.Write(new StringBuilder().Append(32 * num2).Append("+").ToString());
				}
				ostr.WriteLine("j] = true;");
				ostr.WriteLine("          }");
			}
			ostr.WriteLine("        }");
			ostr.WriteLine("      }");
			ostr.WriteLine("    }");
			ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.tokenCount).Append("; i++) {")
				.ToString());
			ostr.WriteLine("      if (la1tokens[i]) {");
			ostr.WriteLine("        jj_expentry = new int[1];");
			ostr.WriteLine("        jj_expentry[0] = i;");
			ostr.WriteLine("        jj_expentries.Add(jj_expentry);");
			ostr.WriteLine("      }");
			ostr.WriteLine("    }");
			if (JavaCCGlobals.jj2index != 0)
			{
				ostr.WriteLine("    jj_endpos = 0;");
				ostr.WriteLine("    jj_rescan_token();");
				ostr.WriteLine("    jj_add_error_token(0, 0);");
			}
			ostr.WriteLine("    int[][] exptokseq = new int[jj_expentries.Count][];");
			ostr.WriteLine("    for (int i = 0; i < jj_expentries.Count; i++) {");
			if (!string.Equals(Options.getJdkVersion(), "1.5"))
			{
				ostr.WriteLine("      exptokseq[i] = (int[])jj_expentries[i];");
			}
			else
			{
				ostr.WriteLine("      exptokseq[i] = jj_expentries[i];");
			}
			ostr.WriteLine("    }");
			ostr.WriteLine("    return new ParseException(token, exptokseq, tokenImage);");
			ostr.WriteLine("  }");
		}
		else
		{
			ostr.WriteLine("  /** Generate ParseException. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("public ParseException generateParseException() {")
				.ToString());
			ostr.WriteLine("    Token errortok = token.next;");
			if (Options.getKeepLineColumn())
			{
				ostr.WriteLine("    int line = errortok.beginLine, column = errortok.beginColumn;");
			}
			ostr.WriteLine("    String mess = (errortok.kind == 0) ? tokenImage[0] : errortok.image;");
			if (Options.getKeepLineColumn())
			{
				ostr.WriteLine("    return new ParseException(\"Parse error at line \" + line + \", column \" + column + \".  Encountered: \" + mess);");
			}
			else
			{
				ostr.WriteLine("    return new ParseException(\"Parse error at <unknown location>.  Encountered: \" + mess);");
			}
			ostr.WriteLine("  }");
		}
		ostr.WriteLine("");
		if (Options.getDebugParser())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private int trace_indent = 0;")
				.ToString());
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private boolean trace_enabled = true;")
				.ToString());
			ostr.WriteLine("");
			ostr.WriteLine("/** Enable tracing. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public void enable_tracing() {")
				.ToString());
			ostr.WriteLine("    trace_enabled = true;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine("/** Disable tracing. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public void disable_tracing() {")
				.ToString());
			ostr.WriteLine("    trace_enabled = false;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void trace_call(String s) {")
				.ToString());
			ostr.WriteLine("    if (trace_enabled) {");
			ostr.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			ostr.WriteLine("      System.out.WriteLine(\"Call:   \" + s);");
			ostr.WriteLine("    }");
			ostr.WriteLine("    trace_indent = trace_indent + 2;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void trace_return(String s) {")
				.ToString());
			ostr.WriteLine("    trace_indent = trace_indent - 2;");
			ostr.WriteLine("    if (trace_enabled) {");
			ostr.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			ostr.WriteLine("      System.out.WriteLine(\"Return: \" + s);");
			ostr.WriteLine("    }");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void trace_token(Token t, String where) {")
				.ToString());
			ostr.WriteLine("    if (trace_enabled) {");
			ostr.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			ostr.WriteLine("      System.out.Write(\"Consumed token: <\" + tokenImage[t.kind]);");
			ostr.WriteLine("      if (t.kind != 0 && !tokenImage[t.kind].equals(\"\\\"\" + t.image + \"\\\"\")) {");
			ostr.WriteLine("        System.out.Write(\": \\\"\" + t.image + \"\\\"\");");
			ostr.WriteLine("      }");
			ostr.WriteLine("      System.out.WriteLine(\" at line \" + t.beginLine + \" column \" + t.beginColumn + \">\" + where);");
			ostr.WriteLine("    }");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void trace_scan(Token t1, int t2) {")
				.ToString());
			ostr.WriteLine("    if (trace_enabled) {");
			ostr.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			ostr.WriteLine("      System.out.Write(\"Visited token: <\" + tokenImage[t1.kind]);");
			ostr.WriteLine("      if (t1.kind != 0 && !tokenImage[t1.kind].equals(\"\\\"\" + t1.image + \"\\\"\")) {");
			ostr.WriteLine("        System.out.Write(\": \\\"\" + t1.image + \"\\\"\");");
			ostr.WriteLine("      }");
			ostr.WriteLine("      System.out.WriteLine(\" at line \" + t1.beginLine + \" column \" + t1.beginColumn + \">; Expected token: <\" + tokenImage[t2] + \">\");");
			ostr.WriteLine("    }");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		else
		{
			ostr.WriteLine("  /** Enable tracing. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public void enable_tracing() {")
				.ToString());
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine("  /** Disable tracing. */");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("final public void disable_tracing() {")
				.ToString());
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		if (JavaCCGlobals.jj2index != 0 && Options.getErrorReporting())
		{
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void jj_rescan_token() {")
				.ToString());
			ostr.WriteLine("    jj_rescan = true;");
			ostr.WriteLine(new StringBuilder().Append("    for (int i = 0; i < ").Append(JavaCCGlobals.jj2index).Append("; i++) {")
				.ToString());
			ostr.WriteLine("    try {");
			ostr.WriteLine("      JJCalls p = jj_2_rtns[i];");
			ostr.WriteLine("      do {");
			ostr.WriteLine("        if (p.gen > jj_gen) {");
			ostr.WriteLine("          jj_la = p.arg; jj_lastpos = jj_scanpos = p.first;");
			ostr.WriteLine("          switch (i) {");
			for (int num2 = 0; num2 < JavaCCGlobals.jj2index; num2++)
			{
				ostr.WriteLine(new StringBuilder().Append("            case ").Append(num2).Append(": jj_3_")
					.Append(num2 + 1)
					.Append("(); break;")
					.ToString());
			}
			ostr.WriteLine("          }");
			ostr.WriteLine("        }");
			ostr.WriteLine("        p = p.next;");
			ostr.WriteLine("      } while (p != null);");
			ostr.WriteLine("      } catch(LookaheadSuccess ls) { }");
			ostr.WriteLine("    }");
			ostr.WriteLine("    jj_rescan = false;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
			ostr.WriteLine(new StringBuilder().Append("  ").Append(JavaCCGlobals.staticOpt()).Append("private void jj_save(int index, int xla) {")
				.ToString());
			ostr.WriteLine("    JJCalls p = jj_2_rtns[index];");
			ostr.WriteLine("    while (p.gen > jj_gen) {");
			ostr.WriteLine("      if (p.next == null) { p = p.next = new JJCalls(); break; }");
			ostr.WriteLine("      p = p.next;");
			ostr.WriteLine("    }");
			ostr.WriteLine("    p.gen = jj_gen + xla - jj_la; p.first = token; p.arg = xla;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		if (JavaCCGlobals.jj2index != 0 && Options.getErrorReporting())
		{
			ostr.WriteLine("  static final class JJCalls {");
			ostr.WriteLine("    int gen;");
			ostr.WriteLine("    Token first;");
			ostr.WriteLine("    int arg;");
			ostr.WriteLine("    JJCalls next;");
			ostr.WriteLine("  }");
			ostr.WriteLine("");
		}
		if (JavaCCGlobals.cu_from_insertion_point_2.Count != 0)
		{
			JavaCCGlobals.printTokenSetup((Token)JavaCCGlobals.cu_from_insertion_point_2[0]);
			JavaCCGlobals.ccol = 1;
			Enumeration enumeration = JavaCCGlobals.cu_from_insertion_point_2.elements();
			while (enumeration.hasMoreElements())
			{
				token = (Token)enumeration.nextElement();
				JavaCCGlobals.printToken(token, ostr);
			}
			JavaCCGlobals.printTrailingComments(token, ostr);
		}
		ostr.WriteLine("");
		ostr.Close();
		return;
		IL_006c:
		
		JavaCCErrors.Semantic_Error(new StringBuilder().Append("Could not open file ").Append(JavaCCGlobals.cu_name).Append(".java for writing.")
			.ToString());
		
		throw new System.Exception();
	}

	public static void reInit()
	{
		ostr = null;
	}

	
	public ParseGen()
	{
	}

}
