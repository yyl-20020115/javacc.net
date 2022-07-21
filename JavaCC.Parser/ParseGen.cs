namespace JavaCC.Parser;
using JavaCC.NET;
using System.IO;
using System.Linq;

public class ParseGen : JavaCCParserConstants // JavaCCGlobals, 
{
	protected static TextWriter writer;
	
	public static void Start()
	{
		Token token = null;
		if (JavaCCErrors._Error_Count != 0)
		{
			throw new MetaParseException();
		}
		if (!Options.BuildParser)
		{
			return;
		}
		try
		{
			writer = new StreamWriter(Path.Combine(Options.OutputDirectory.FullName, (JavaCCGlobals.CuName)+(".java")));
		}
		catch (IOException)
		{
			goto IL_006c;
		}
		var vector = JavaCCGlobals.ToolNames.ToList();
		vector.Add("JavaCC");
		writer.WriteLine(("/* ")+(JavaCCGlobals.GetIdStringList(vector, (JavaCCGlobals.CuName)+(".java")))+(" */")
			);
		int num = 0;
		if (JavaCCGlobals.CuToInsertionPoint1.Count != 0)
		{
			JavaCCGlobals.PrintTokenSetup((Token)JavaCCGlobals.CuToInsertionPoint1[0]);
			JavaCCGlobals.CCol = 1;
			foreach(var _token in JavaCCGlobals.CuToInsertionPoint1)
			{
				if (_token.Kind == 51)
				{
					num = 1;
				}
				else if (_token.Kind == 35)
				{
					num = 0;
				}
				JavaCCGlobals.PrintToken(token, writer);
			}
		}
		if (num != 0)
		{
			writer.Write(", ");
		}
		else
		{
			writer.Write(" implements ");
		}
		writer.Write((JavaCCGlobals.CuName)+("Constants "));
		if (JavaCCGlobals.Cu_to_insertion_point_2.Count != 0)
		{
			JavaCCGlobals.PrintTokenSetup((Token)JavaCCGlobals.Cu_to_insertion_point_2[0]);

			foreach(var _token in JavaCCGlobals.Cu_to_insertion_point_2)
			{
				JavaCCGlobals.PrintToken(_token, writer);
			}
		}
		writer.WriteLine("");
		writer.WriteLine("");
		ParseEngine.Build(writer);
		if (Options.Static)
		{
			writer.WriteLine("  static private boolean jj_initialized_once = false;");
		}
		if (Options.UserTokenManager)
		{
			writer.WriteLine("  /** User defined Token Manager. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public TokenManager token_source;")
				);
		}
		else
		{
			writer.WriteLine("  /** Generated Token Manager. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public ")
				+(JavaCCGlobals.CuName)
				+("TokenManager token_source;")
				);
			if (!Options.UserCharStream)
			{
				if (Options.JavaUnicodeEscape)
				{
					writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("JavaCharStream jj_input_stream;")
						);
				}
				else
				{
					writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("SimpleCharStream jj_input_stream;")
						);
				}
			}
		}
		writer.WriteLine("  /** Current token. */");
		writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public Token token;")
			);
		writer.WriteLine("  /** Next token. */");
		writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public Token jj_nt;")
			);
		if (!Options.CacheTokens)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_ntk;")
				);
		}
		if (JavaCCGlobals.jj2index != 0)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private Token jj_scanpos, jj_lastpos;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_la;")
				);
			writer.WriteLine("  /** Whether we are looking ahead. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_lookingAhead = false;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_semLA;")
				);
		}
		if (Options.ErrorReporting)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_gen;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final private int[] jj_la1 = new int[")
				+(JavaCCGlobals.maskindex)
				+("];")
				);
			int num2 = (JavaCCGlobals.TokenCount - 1) / 32 + 1;
			for (int i = 0; i < num2; i++)
			{
				writer.WriteLine(("  static private int[] jj_la1_")+(i)+(";")
					);
			}
			writer.WriteLine("  static {");
			for (int i = 0; i < num2; i++)
			{
				writer.WriteLine(("      jj_la1_init_")+(i)+("();")
					);
			}
			writer.WriteLine("   }");
			for (int i = 0; i < num2; i++)
			{
				writer.WriteLine(("   private static void jj_la1_init_")+(i)+("() {")
					);
				writer.Write(("      jj_la1_")+(i)+(" = new int[] {"));
				foreach(var array in JavaCCGlobals.MaskVals)
				{
				writer.Write(("0x")+(Utils.ToString(array[i],16))+(",")
						);
				}
				writer.WriteLine("};");
				writer.WriteLine("   }");
			}
		}
		if (JavaCCGlobals.jj2index != 0 && Options.ErrorReporting)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final private JJCalls[] jj_2_rtns = new JJCalls[")
				+(JavaCCGlobals.jj2index)
				+("];")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_rescan = false;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_gc = 0;")
				);
		}
		writer.WriteLine("");
		if (!Options.UserTokenManager)
		{
			if (Options.UserCharStream)
			{
				writer.WriteLine("  /** Constructor with user supplied CharStream. */");
				writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(CharStream stream) {")
					);
				if (Options.Static)
				{
					writer.WriteLine("    if (jj_initialized_once) {");
					writer.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser.  \");");
					writer.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					writer.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					writer.WriteLine("      throw new System.Exception();");
					writer.WriteLine("    }");
					writer.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.TokenManagerUsesParser && !Options.Static)
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(this, stream);")
						);
				}
				else
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(stream);")
						);
				}
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
				writer.WriteLine("");
				writer.WriteLine("  /** Reinitialise. */");
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public void ReInit(CharStream stream) {")
					);
				writer.WriteLine("    token_source.ReInit(stream);");
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				writer.WriteLine("    jj_lookingAhead = false;");
				if (JavaCCGlobals.JJTreeGenerated)
				{
					writer.WriteLine("    jjtree.reset();");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
			}
			else
			{
				writer.WriteLine("  /** Constructor with Stream. */");
				writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(java.io.Stream stream) {")
					);
				writer.WriteLine("     this(stream, null);");
				writer.WriteLine("  }");
				writer.WriteLine("  /** Constructor with Stream and supplied encoding */");
				writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(java.io.Stream stream, String encoding) {")
					);
				if (Options.Static)
				{
					writer.WriteLine("    if (jj_initialized_once) {");
					writer.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser.  \");");
					writer.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					writer.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					writer.WriteLine("      throw new System.Exception();");
					writer.WriteLine("    }");
					writer.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.JavaUnicodeEscape)
				{
					if (string.Equals(Options.JdkVersion, "1.3"))
					{
						writer.WriteLine("    try { jj_input_stream = new JavaCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
					}
					else
					{
						writer.WriteLine("    try { jj_input_stream = new JavaCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
					}
				}
				else if (string.Equals(Options.JdkVersion, "1.3"))
				{
					writer.WriteLine("    try { jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
				}
				else
				{
					writer.WriteLine("    try { jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
				}
				if (Options.TokenManagerUsesParser && !Options.Static)
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(this, jj_input_stream);")
						);
				}
				else
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(jj_input_stream);")
						);
				}
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
				writer.WriteLine("");
				writer.WriteLine("  /** Reinitialise. */");
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public void ReInit(java.io.Stream stream) {")
					);
				writer.WriteLine("     ReInit(stream, null);");
				writer.WriteLine("  }");
				writer.WriteLine("  /** Reinitialise. */");
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public void ReInit(java.io.Stream stream, String encoding) {")
					);
				if (string.Equals(Options.JdkVersion, "1.3"))
				{
					writer.WriteLine("    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e.getMessage()); }");
				}
				else
				{
					writer.WriteLine("    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new RuntimeException(e); }");
				}
				writer.WriteLine("    token_source.ReInit(jj_input_stream);");
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				if (JavaCCGlobals.JJTreeGenerated)
				{
					writer.WriteLine("    jjtree.reset();");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
				writer.WriteLine("");
				writer.WriteLine("  /** Constructor. */");
				writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(java.io.TextReader stream) {")
					);
				if (Options.Static)
				{
					writer.WriteLine("    if (jj_initialized_once) {");
					writer.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser. \");");
					writer.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
					writer.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
					writer.WriteLine("      throw new System.Exception();");
					writer.WriteLine("    }");
					writer.WriteLine("    jj_initialized_once = true;");
				}
				if (Options.JavaUnicodeEscape)
				{
					writer.WriteLine("    jj_input_stream = new JavaCharStream(stream, 1, 1);");
				}
				else
				{
					writer.WriteLine("    jj_input_stream = new SimpleCharStream(stream, 1, 1);");
				}
				if (Options.TokenManagerUsesParser && !Options.Static)
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(this, jj_input_stream);")
						);
				}
				else
				{
					writer.WriteLine(("    token_source = new ")+(JavaCCGlobals.CuName)+("TokenManager(jj_input_stream);")
						);
				}
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
				writer.WriteLine("");
				writer.WriteLine("  /** Reinitialise. */");
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public void ReInit(java.io.TextReader stream) {")
					);
				if (Options.JavaUnicodeEscape)
				{
					writer.WriteLine("    jj_input_stream.ReInit(stream, 1, 1);");
				}
				else
				{
					writer.WriteLine("    jj_input_stream.ReInit(stream, 1, 1);");
				}
				writer.WriteLine("    token_source.ReInit(jj_input_stream);");
				writer.WriteLine("    token = new Token();");
				if (Options.CacheTokens)
				{
					writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
				}
				else
				{
					writer.WriteLine("    jj_ntk = -1;");
				}
				if (JavaCCGlobals.JJTreeGenerated)
				{
					writer.WriteLine("    jjtree.reset();");
				}
				if (Options.ErrorReporting)
				{
					writer.WriteLine("    jj_gen = 0;");
					writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
						);
					if (JavaCCGlobals.jj2index != 0)
					{
						writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
					}
				}
				writer.WriteLine("  }");
			}
		}
		writer.WriteLine("");
		if (Options.UserTokenManager)
		{
			writer.WriteLine("  /** Constructor with user supplied Token Manager. */");
			writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(TokenManager tm) {")
				);
		}
		else
		{
			writer.WriteLine("  /** Constructor with generated Token Manager. */");
			writer.WriteLine(("  public ")+(JavaCCGlobals.CuName)+("(")
				+(JavaCCGlobals.CuName)
				+("TokenManager tm) {")
				);
		}
		if (Options.Static)
		{
			writer.WriteLine("    if (jj_initialized_once) {");
			writer.WriteLine("      System.out.WriteLine(\"ERROR: Second call to constructor of static parser. \");");
			writer.WriteLine("      System.out.WriteLine(\"       You must either use ReInit() or set the JavaCC option STATIC to false\");");
			writer.WriteLine("      System.out.WriteLine(\"       during parser generation.\");");
			writer.WriteLine("      throw new System.Exception();");
			writer.WriteLine("    }");
			writer.WriteLine("    jj_initialized_once = true;");
		}
		writer.WriteLine("    token_source = tm;");
		writer.WriteLine("    token = new Token();");
		if (Options.CacheTokens)
		{
			writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
		}
		else
		{
			writer.WriteLine("    jj_ntk = -1;");
		}
		if (Options.ErrorReporting)
		{
			writer.WriteLine("    jj_gen = 0;");
			writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
				);
			if (JavaCCGlobals.jj2index != 0)
			{
				writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
			}
		}
		writer.WriteLine("  }");
		writer.WriteLine("");
		if (Options.UserTokenManager)
		{
			writer.WriteLine("  /** Reinitialise. */");
			writer.WriteLine("  public void ReInit(TokenManager tm) {");
		}
		else
		{
			writer.WriteLine("  /** Reinitialise. */");
			writer.WriteLine(("  public void ReInit(")+(JavaCCGlobals.CuName)+("TokenManager tm) {")
				);
		}
		writer.WriteLine("    token_source = tm;");
		writer.WriteLine("    token = new Token();");
		if (Options.CacheTokens)
		{
			writer.WriteLine("    token.next = jj_nt = token_source.getNextToken();");
		}
		else
		{
			writer.WriteLine("    jj_ntk = -1;");
		}
		if (JavaCCGlobals.JJTreeGenerated)
		{
			writer.WriteLine("    jjtree.reset();");
		}
		if (Options.ErrorReporting)
		{
			writer.WriteLine("    jj_gen = 0;");
			writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) jj_la1[i] = -1;")
				);
			if (JavaCCGlobals.jj2index != 0)
			{
				writer.WriteLine("    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();");
			}
		}
		writer.WriteLine("  }");
		writer.WriteLine("");
		writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private Token jj_consume_token(int kind) throws ParseException {")
			);
		if (Options.CacheTokens)
		{
			writer.WriteLine("    Token oldToken = token;");
			writer.WriteLine("    if ((token = jj_nt).next != null) jj_nt = jj_nt.next;");
			writer.WriteLine("    else jj_nt = jj_nt.next = token_source.getNextToken();");
		}
		else
		{
			writer.WriteLine("    Token oldToken;");
			writer.WriteLine("    if ((oldToken = token).next != null) token = token.next;");
			writer.WriteLine("    else token = token.next = token_source.getNextToken();");
			writer.WriteLine("    jj_ntk = -1;");
		}
		writer.WriteLine("    if (token.kind == kind) {");
		if (Options.ErrorReporting)
		{
			writer.WriteLine("      jj_gen++;");
			if (JavaCCGlobals.jj2index != 0)
			{
				writer.WriteLine("      if (++jj_gc > 100) {");
				writer.WriteLine("        jj_gc = 0;");
				writer.WriteLine("        for (int i = 0; i < jj_2_rtns.length; i++) {");
				writer.WriteLine("          JJCalls c = jj_2_rtns[i];");
				writer.WriteLine("          while (c != null) {");
				writer.WriteLine("            if (c.gen < jj_gen) c.first = null;");
				writer.WriteLine("            c = c.next;");
				writer.WriteLine("          }");
				writer.WriteLine("        }");
				writer.WriteLine("      }");
			}
		}
		if (Options.DebugParser)
		{
			writer.WriteLine("      trace_token(token, \"\");");
		}
		writer.WriteLine("      return token;");
		writer.WriteLine("    }");
		if (Options.CacheTokens)
		{
			writer.WriteLine("    jj_nt = token;");
		}
		writer.WriteLine("    token = oldToken;");
		if (Options.ErrorReporting)
		{
			writer.WriteLine("    jj_kind = kind;");
		}
		writer.WriteLine("    throw generateParseException();");
		writer.WriteLine("  }");
		writer.WriteLine("");
		if (JavaCCGlobals.jj2index != 0)
		{
			writer.WriteLine("  static private final class LookaheadSuccess extends java.lang.Error { }");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final private LookaheadSuccess jj_ls = new LookaheadSuccess();")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean jj_scan_token(int kind) {")
				);
			writer.WriteLine("    if (jj_scanpos == jj_lastpos) {");
			writer.WriteLine("      jj_la--;");
			writer.WriteLine("      if (jj_scanpos.next == null) {");
			writer.WriteLine("        jj_lastpos = jj_scanpos = jj_scanpos.next = token_source.getNextToken();");
			writer.WriteLine("      } else {");
			writer.WriteLine("        jj_lastpos = jj_scanpos = jj_scanpos.next;");
			writer.WriteLine("      }");
			writer.WriteLine("    } else {");
			writer.WriteLine("      jj_scanpos = jj_scanpos.next;");
			writer.WriteLine("    }");
			if (Options.ErrorReporting)
			{
				writer.WriteLine("    if (jj_rescan) {");
				writer.WriteLine("      int i = 0; Token tok = token;");
				writer.WriteLine("      while (tok != null && tok != jj_scanpos) { i++; tok = tok.next; }");
				writer.WriteLine("      if (tok != null) jj_add_error_token(kind, i);");
				if (Options.DebugLookahead)
				{
					writer.WriteLine("    } else {");
					writer.WriteLine("      trace_scan(jj_scanpos, kind);");
				}
				writer.WriteLine("    }");
			}
			else if (Options.DebugLookahead)
			{
				writer.WriteLine("    trace_scan(jj_scanpos, kind);");
			}
			writer.WriteLine("    if (jj_scanpos.kind != kind) return true;");
			writer.WriteLine("    if (jj_la == 0 && jj_scanpos == jj_lastpos) throw jj_ls;");
			writer.WriteLine("    return false;");
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		writer.WriteLine("");
		writer.WriteLine("/** Get the next Token. */");
		writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public Token getNextToken() {")
			);
		if (Options.CacheTokens)
		{
			writer.WriteLine("    if ((token = jj_nt).next != null) jj_nt = jj_nt.next;");
			writer.WriteLine("    else jj_nt = jj_nt.next = token_source.getNextToken();");
		}
		else
		{
			writer.WriteLine("    if (token.next != null) token = token.next;");
			writer.WriteLine("    else token = token.next = token_source.getNextToken();");
			writer.WriteLine("    jj_ntk = -1;");
		}
		if (Options.ErrorReporting)
		{
			writer.WriteLine("    jj_gen++;");
		}
		if (Options.DebugParser)
		{
			writer.WriteLine("      trace_token(token, \" (in getNextToken)\");");
		}
		writer.WriteLine("    return token;");
		writer.WriteLine("  }");
		writer.WriteLine("");
		writer.WriteLine("/** Get the specific Token. */");
		writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public Token getToken(int index) {")
			);
		if (JavaCCGlobals.jj2index != 0)
		{
			writer.WriteLine("    Token t = jj_lookingAhead ? jj_scanpos : token;");
		}
		else
		{
			writer.WriteLine("    Token t = token;");
		}
		writer.WriteLine("    for (int i = 0; i < index; i++) {");
		writer.WriteLine("      if (t.next != null) t = t.next;");
		writer.WriteLine("      else t = t.next = token_source.getNextToken();");
		writer.WriteLine("    }");
		writer.WriteLine("    return t;");
		writer.WriteLine("  }");
		writer.WriteLine("");
		if (!Options.CacheTokens)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_ntk() {")
				);
			writer.WriteLine("    if ((jj_nt=token.next) == null)");
			writer.WriteLine("      return (jj_ntk = (token.next=token_source.getNextToken()).kind);");
			writer.WriteLine("    else");
			writer.WriteLine("      return (jj_ntk = jj_nt.kind);");
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		if (Options.ErrorReporting)
		{
			if (!string.Equals(Options.JdkVersion, "1.5"))
			{
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private java.util.List jj_expentries = new java.util.ArrayList();")
					);
			}
			else
			{
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private java.util.List<int[]> jj_expentries = new java.util.ArrayList<int[]>();")
					);
			}
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int[] jj_expentry;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_kind = -1;")
				);
			if (JavaCCGlobals.jj2index != 0)
			{
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int[] jj_lasttokens = new int[100];")
					);
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int jj_endpos;")
					);
				writer.WriteLine("");
				writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void jj_add_error_token(int kind, int pos) {")
					);
				writer.WriteLine("    if (pos >= 100) return;");
				writer.WriteLine("    if (pos == jj_endpos + 1) {");
				writer.WriteLine("      jj_lasttokens[jj_endpos++] = kind;");
				writer.WriteLine("    } else if (jj_endpos != 0) {");
				writer.WriteLine("      jj_expentry = new int[jj_endpos];");
				writer.WriteLine("      for (int i = 0; i < jj_endpos; i++) {");
				writer.WriteLine("        jj_expentry[i] = jj_lasttokens[i];");
				writer.WriteLine("      }");
				writer.WriteLine("      boolean exists = false;");
				writer.WriteLine("      for (java.util.Iterator it = jj_expentries.iterator(); it.hasNext();) {");
				writer.WriteLine("        int[] oldentry = (int[])(it.next());");
				writer.WriteLine("        if (oldentry.length == jj_expentry.length) {");
				writer.WriteLine("          exists = true;");
				writer.WriteLine("          for (int i = 0; i < jj_expentry.length; i++) {");
				writer.WriteLine("            if (oldentry[i] != jj_expentry[i]) {");
				writer.WriteLine("              exists = false;");
				writer.WriteLine("              break;");
				writer.WriteLine("            }");
				writer.WriteLine("          }");
				writer.WriteLine("          if (exists) break;");
				writer.WriteLine("        }");
				writer.WriteLine("      }");
				writer.WriteLine("      if (!exists) jj_expentries.Add(jj_expentry);");
				writer.WriteLine("      if (pos != 0) jj_lasttokens[(jj_endpos = pos) - 1] = kind;");
				writer.WriteLine("    }");
				writer.WriteLine("  }");
			}
			writer.WriteLine("");
			writer.WriteLine("  /** Generate ParseException. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public ParseException generateParseException() {")
				);
			writer.WriteLine("    jj_expentries.Clear();");
			writer.WriteLine(("    boolean[] la1tokens = new boolean[")+(JavaCCGlobals.TokenCount)+("];")
				);
			writer.WriteLine("    if (jj_kind >= 0) {");
			writer.WriteLine("      la1tokens[jj_kind] = true;");
			writer.WriteLine("      jj_kind = -1;");
			writer.WriteLine("    }");
			writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.maskindex)+("; i++) {")
				);
			writer.WriteLine("      if (jj_la1[i] == jj_gen) {");
			writer.WriteLine("        for (int j = 0; j < 32; j++) {");
			for (int num2 = 0; num2 < (JavaCCGlobals.TokenCount - 1) / 32 + 1; num2++)
			{
				writer.WriteLine(("          if ((jj_la1_")+(num2)+("[i] & (1<<j)) != 0) {")
					);
				writer.Write("            la1tokens[");
				if (num2 != 0)
				{
					writer.Write((32 * num2)+("+"));
				}
				writer.WriteLine("j] = true;");
				writer.WriteLine("          }");
			}
			writer.WriteLine("        }");
			writer.WriteLine("      }");
			writer.WriteLine("    }");
			writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.TokenCount)+("; i++) {")
				);
			writer.WriteLine("      if (la1tokens[i]) {");
			writer.WriteLine("        jj_expentry = new int[1];");
			writer.WriteLine("        jj_expentry[0] = i;");
			writer.WriteLine("        jj_expentries.Add(jj_expentry);");
			writer.WriteLine("      }");
			writer.WriteLine("    }");
			if (JavaCCGlobals.jj2index != 0)
			{
				writer.WriteLine("    jj_endpos = 0;");
				writer.WriteLine("    jj_rescan_token();");
				writer.WriteLine("    jj_add_error_token(0, 0);");
			}
			writer.WriteLine("    int[][] exptokseq = new int[jj_expentries.Count][];");
			writer.WriteLine("    for (int i = 0; i < jj_expentries.Count; i++) {");
			if (!string.Equals(Options.JdkVersion, "1.5"))
			{
				writer.WriteLine("      exptokseq[i] = (int[])jj_expentries[i];");
			}
			else
			{
				writer.WriteLine("      exptokseq[i] = jj_expentries[i];");
			}
			writer.WriteLine("    }");
			writer.WriteLine("    return new ParseException(token, exptokseq, tokenImage);");
			writer.WriteLine("  }");
		}
		else
		{
			writer.WriteLine("  /** Generate ParseException. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("public ParseException generateParseException() {")
				);
			writer.WriteLine("    Token errortok = token.next;");
			if (Options.KeepLineColumn)
			{
				writer.WriteLine("    int line = errortok.beginLine, column = errortok.beginColumn;");
			}
			writer.WriteLine("    String mess = (errortok.kind == 0) ? tokenImage[0] : errortok.image;");
			if (Options.KeepLineColumn)
			{
				writer.WriteLine("    return new ParseException(\"Parse error at line \" + line + \", column \" + column + \".  Encountered: \" + mess);");
			}
			else
			{
				writer.WriteLine("    return new ParseException(\"Parse error at <unknown location>.  Encountered: \" + mess);");
			}
			writer.WriteLine("  }");
		}
		writer.WriteLine("");
		if (Options.DebugParser)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private int trace_indent = 0;")
				);
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private boolean trace_enabled = true;")
				);
			writer.WriteLine("");
			writer.WriteLine("/** Enable tracing. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public void enable_tracing() {")
				);
			writer.WriteLine("    trace_enabled = true;");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine("/** Disable tracing. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public void disable_tracing() {")
				);
			writer.WriteLine("    trace_enabled = false;");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void trace_call(String s) {")
				);
			writer.WriteLine("    if (trace_enabled) {");
			writer.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			writer.WriteLine("      System.out.WriteLine(\"Call:   \" + s);");
			writer.WriteLine("    }");
			writer.WriteLine("    trace_indent = trace_indent + 2;");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void trace_return(String s) {")
				);
			writer.WriteLine("    trace_indent = trace_indent - 2;");
			writer.WriteLine("    if (trace_enabled) {");
			writer.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			writer.WriteLine("      System.out.WriteLine(\"Return: \" + s);");
			writer.WriteLine("    }");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void trace_token(Token t, String where) {")
				);
			writer.WriteLine("    if (trace_enabled) {");
			writer.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			writer.WriteLine("      System.out.Write(\"Consumed token: <\" + tokenImage[t.kind]);");
			writer.WriteLine("      if (t.kind != 0 && !tokenImage[t.kind].equals(\"\\\"\" + t.image + \"\\\"\")) {");
			writer.WriteLine("        System.out.Write(\": \\\"\" + t.image + \"\\\"\");");
			writer.WriteLine("      }");
			writer.WriteLine("      System.out.WriteLine(\" at line \" + t.beginLine + \" column \" + t.beginColumn + \">\" + where);");
			writer.WriteLine("    }");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void trace_scan(Token t1, int t2) {")
				);
			writer.WriteLine("    if (trace_enabled) {");
			writer.WriteLine("      for (int i = 0; i < trace_indent; i++) { System.out.Write(\" \"); }");
			writer.WriteLine("      System.out.Write(\"Visited token: <\" + tokenImage[t1.kind]);");
			writer.WriteLine("      if (t1.kind != 0 && !tokenImage[t1.kind].equals(\"\\\"\" + t1.image + \"\\\"\")) {");
			writer.WriteLine("        System.out.Write(\": \\\"\" + t1.image + \"\\\"\");");
			writer.WriteLine("      }");
			writer.WriteLine("      System.out.WriteLine(\" at line \" + t1.beginLine + \" column \" + t1.beginColumn + \">; Expected token: <\" + tokenImage[t2] + \">\");");
			writer.WriteLine("    }");
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		else
		{
			writer.WriteLine("  /** Enable tracing. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public void enable_tracing() {")
				);
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine("  /** Disable tracing. */");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("final public void disable_tracing() {")
				);
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		if (JavaCCGlobals.jj2index != 0 && Options.ErrorReporting)
		{
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void jj_rescan_token() {")
				);
			writer.WriteLine("    jj_rescan = true;");
			writer.WriteLine(("    for (int i = 0; i < ")+(JavaCCGlobals.jj2index)+("; i++) {")
				);
			writer.WriteLine("    try {");
			writer.WriteLine("      JJCalls p = jj_2_rtns[i];");
			writer.WriteLine("      do {");
			writer.WriteLine("        if (p.gen > jj_gen) {");
			writer.WriteLine("          jj_la = p.arg; jj_lastpos = jj_scanpos = p.first;");
			writer.WriteLine("          switch (i) {");
			for (int num2 = 0; num2 < JavaCCGlobals.jj2index; num2++)
			{
				writer.WriteLine(("            case ")+(num2)+(": jj_3_")
					+(num2 + 1)
					+("(); break;")
					);
			}
			writer.WriteLine("          }");
			writer.WriteLine("        }");
			writer.WriteLine("        p = p.next;");
			writer.WriteLine("      } while (p != null);");
			writer.WriteLine("      } catch(LookaheadSuccess ls) { }");
			writer.WriteLine("    }");
			writer.WriteLine("    jj_rescan = false;");
			writer.WriteLine("  }");
			writer.WriteLine("");
			writer.WriteLine(("  ")+(JavaCCGlobals.StaticOpt())+("private void jj_save(int index, int xla) {")
				);
			writer.WriteLine("    JJCalls p = jj_2_rtns[index];");
			writer.WriteLine("    while (p.gen > jj_gen) {");
			writer.WriteLine("      if (p.next == null) { p = p.next = new JJCalls(); break; }");
			writer.WriteLine("      p = p.next;");
			writer.WriteLine("    }");
			writer.WriteLine("    p.gen = jj_gen + xla - jj_la; p.first = token; p.arg = xla;");
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		if (JavaCCGlobals.jj2index != 0 && Options.ErrorReporting)
		{
			writer.WriteLine("  static final class JJCalls {");
			writer.WriteLine("    int gen;");
			writer.WriteLine("    Token first;");
			writer.WriteLine("    int arg;");
			writer.WriteLine("    JJCalls next;");
			writer.WriteLine("  }");
			writer.WriteLine("");
		}
		if (JavaCCGlobals.Cu_from_insertion_point_2.Count != 0)
		{
			JavaCCGlobals.PrintTokenSetup((Token)JavaCCGlobals.Cu_from_insertion_point_2[0]);
			JavaCCGlobals.CCol = 1;
			foreach (var _token in JavaCCGlobals.Cu_from_insertion_point_2)
			{
				JavaCCGlobals.PrintToken(token, writer);
				token = _token;
			}
			JavaCCGlobals.PrintTrailingComments(token, writer);
		}
		writer.WriteLine("");
		writer.Close();
		return;
		IL_006c:
		
		JavaCCErrors.Semantic_Error(("Could not open file ")+(JavaCCGlobals.CuName)+(".java for writing.")
			);
		
		throw new System.Exception();
	}

	public static void ReInit()
	{
		writer = null;
	}
}
