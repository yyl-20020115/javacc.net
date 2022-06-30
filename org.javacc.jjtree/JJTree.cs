using System.Collections;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjtree;


public class JJTree 
{
	private IO io;

	
	private void p(string P_0)
	{
		io.getMsg().WriteLine(P_0);
	}

	
	private void initializeOptions()
	{
		JJTreeOptions.init();
		JJTreeGlobals.initialize();
	}

	
	private void help_message()
	{
		p("Usage:");
		p("    jjtree option-settings inputfile");
		p("");
		p("\"option-settings\" is a sequence of settings separated by spaces.");
		p("Each option setting must be of one of the following forms:");
		p("");
		p("    -optionname=value (e.g., -STATIC=false)");
		p("    -optionname:value (e.g., -STATIC:false)");
		p("    -optionname       (equivalent to -optionname=true.  e.g., -STATIC)");
		p("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOSTATIC)");
		p("");
		p("Option settings are not case-sensitive, so one can say \"-nOsTaTiC\" instead");
		p("of \"-NOSTATIC\".  Option values must be appropriate for the corresponding");
		p("option, and must be either an integer or a string value.");
		p("");
		p("The boolean valued options are:");
		p("");
		p("    STATIC                   (default true)");
		p("    MULTI                    (default false)");
		p("    NODE_DEFAULT_VOID        (default false)");
		p("    NODE_SCOPE_HOOK          (default false)");
		p("    NODE_USES_PARSER         (default false)");
		p("    BUILD_NODE_FILES         (default true)");
		p("    TRACK_TOKENS             (default false)");
		p("    VISITOR                  (default false)");
		p("");
		p("The string valued options are:");
		p("");
		p("    JDK_VERSION              (default \"1.5\")");
		p("    NODE_PREFIX              (default \"AST\")");
		p("    NODE_PACKAGE             (default \"\")");
		p("    NODE_EXTENDS             (default \"\")");
		p("    NODE_FACTORY             (default \"\")");
		p("    OUTPUT_FILE              (default remove input file suffix, Add .jj)");
		p("    OUTPUT_DIRECTORY         (default \"\")");
		p("    JJTREE_OUTPUT_DIRECTORY  (default value of OUTPUT_DIRECTORY option)");
		p("    VISITOR_DATA_TYPE        (default \"\")");
		p("    VISITOR_EXCEPTION        (default \"\")");
		p("");
		p("JJTree also accepts JavaCC options, which it inserts into the generated file.");
		p("");
		p("EXAMPLES:");
		p("    jjtree -STATIC=false mygrammar.jjt");
		p("");
		p("ABOUT JJTree:");
		p("    JJTree is a preprocessor for JavaCC that inserts actions into a");
		p("    JavaCC grammar to build parse trees for the input.");
		p("");
		p("    For more information, see the online JJTree documentation at ");
		p("    https://javacc.dev.java.net/doc/JJTree.html ");
		p("");
	}

	
	public JJTree()
	{
	}

	
	public virtual int main(string[] strarr)
	{
		ASTNodeDescriptor.nodeIds = new ArrayList();
		ASTNodeDescriptor.nodeNames = new ArrayList();
		ASTNodeDescriptor.nodeSeen = new Hashtable();
		EntryPoint.reInitAll();
		JavaCCGlobals.bannerLine("Tree Builder", "");
		io = new IO();
		int result;
		try
		{
			initializeOptions();
			if (strarr.Length == 0)
			{
				p("");
				help_message();
				result = 1;
				goto IL_0070;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		string text;
		int result2;
		try
		{
			p("(type \"jjtree\" with no arguments for help)");
			text = strarr[(nint)strarr.LongLength - 1];
			if (Options.isOption(text))
			{
				p(new StringBuilder().Append("Last argument \"").Append(text).Append("\" is not a filename")
					.ToString());
				result2 = 1;
				goto IL_00d9;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		try
		{
			result2 = 0;
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		int result3;
		while (true)
		{
			try
			{
				if (result2 < (nint)strarr.LongLength - 1)
				{
					if (!Options.isOption(strarr[result2]))
					{
						p(new StringBuilder().Append("Argument \"").Append(strarr[result2]).Append("\" must be an option setting.")
							.ToString());
						result3 = 1;
						goto IL_0153;
					}
					goto IL_0160;
				}
			}
			catch
			{
				//try-fault
				io.closeAll();
				throw;
			}
			break;
			IL_0153:
			io.closeAll();
			return result3;
			IL_0160:
			try
			{
				Options.setCmdLineOption(strarr[result2]);
				result2++;
			}
			catch
			{
				//try-fault
				io.closeAll();
				throw;
			}
		}
		JJTreeIOException ex;
		try
		{
			JJTreeOptions.validate();
			try
			{
				io.setInput(text);
			}
			catch (JJTreeIOException x)
			{
				ex = ByteCodeHelper.MapException<JJTreeIOException>(x, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_01b2;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		ASTGrammar aSTGrammar;
		ParseException ex3;
		System.Exception ex5;
		JJTreeIOException ex2;
		try
		{
			p(new StringBuilder().Append("Reading from file ").Append(io.getInputFileName()).Append(" . . .")
				.ToString());
			JJTreeGlobals.toolList = JavaCCGlobals.getToolNames(text);
			JJTreeGlobals.toolList.Add("JJTree");
			try
			{
				try
				{
					JJTreeParser jJTreeParser = new JJTreeParser(io.getIn());
					jJTreeParser.javacc_input();
					aSTGrammar = (ASTGrammar)jJTreeParser.jjtree.rootNode();
					if (java.lang.Boolean.getBoolean("jjtree-dump"))
					{
						aSTGrammar.dump(" ");
					}
					try
					{
						io.setOutput();
					}
					catch (JJTreeIOException x2)
					{
						ex2 = ByteCodeHelper.MapException<JJTreeIOException>(x2, ByteCodeHelper.MapFlags.NoRemapping);
						goto IL_02e1;
					}
				}
				catch (ParseException x3)
				{
					ex3 = ByteCodeHelper.MapException<ParseException>(x3, ByteCodeHelper.MapFlags.NoRemapping);
					goto IL_02ea;
				}
			}
			catch (System.Exception x4)
			{
				System.Exception ex4 = ByteCodeHelper.MapException<System.Exception>(x4, ByteCodeHelper.MapFlags.None);
				if (ex4 == null)
				{
					throw;
				}
				ex5 = ex4;
				goto IL_02f1;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		ParseException ex6;
		System.Exception ex8;
		try
		{
			try
			{
				try
				{
					aSTGrammar.generate(io);
					io.getOut().Close();
					NodeFiles.generateTreeConstants_java();
					NodeFiles.generateVisitor_java();
					JJTreeState.generateTreeState_java();
					p(new StringBuilder().Append("Annotated grammar generated successfully in ").Append(io.getOutputFileName()).ToString());
				}
				catch (ParseException x5)
				{
					ex6 = ByteCodeHelper.MapException<ParseException>(x5, ByteCodeHelper.MapFlags.NoRemapping);
					goto IL_03f7;
				}
			}
			catch (System.Exception x6)
			{
				System.Exception ex7 = ByteCodeHelper.MapException<System.Exception>(x6, ByteCodeHelper.MapFlags.None);
				if (ex7 == null)
				{
					throw;
				}
				ex8 = ex7;
				goto IL_03fb;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		try
		{
			return 0;
		}
		finally
		{
			io.closeAll();
		}
		IL_0361:
		System.Exception ex9;
		System.Exception ex10 = ex9;
		goto IL_0451;
		IL_0451:
		System.Exception ex11 = ex10;
		try
		{
			System.Exception @this = ex11;
			p(new StringBuilder().Append("Error parsing input: ").Append((@this)).ToString());
			//Throwable.instancehelper_printStackTrace(@this, io.getMsg());
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		io.closeAll();
		return result3;
		IL_0070:
		io.closeAll();
		return result;
		IL_035a:
		ParseException ex12;
		ParseException ex13 = ex12;
		goto IL_0404;
		IL_0404:
		ParseException ex14 = ex13;
		try
		{
			ParseException this2 = ex14;
			p(new StringBuilder().Append("Error parsing input: ").Append((this2)).ToString());
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		io.closeAll();
		return result3;
		IL_02f1:
		ex10 = ex5;
		goto IL_0451;
		IL_02ea:
		ex13 = ex3;
		goto IL_0404;
		IL_00d9:
		io.closeAll();
		return result2;
		IL_03fb:
		ex10 = ex8;
		goto IL_0451;
		IL_03f7:
		ex13 = ex6;
		goto IL_0404;
		IL_01b2:
		ex2 = ex;
		try
		{
			JJTreeIOException this3 = ex2;
			p(new StringBuilder().Append("Error setting input: ").Append(
				(this3.Message)).ToString());
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		io.closeAll();
		return result3;
		IL_02e1:
		JJTreeIOException ex15 = ex2;
		int result4;
		try
		{
			ex15 = ex15;
			try
			{
				ex15 = ex15;
				try
				{
					JJTreeIOException this4 = ex15;
					p(new StringBuilder().Append("Error setting output: ").Append(
						(this4.Message)).ToString());
					result4 = 1;
				}
				catch (ParseException x7)
				{
					ex12 = ByteCodeHelper.MapException<ParseException>(x7, ByteCodeHelper.MapFlags.NoRemapping);
					goto IL_035a;
				}
			}
			catch (System.Exception x8)
			{
				System.Exception ex16 = ByteCodeHelper.MapException<System.Exception>(x8, ByteCodeHelper.MapFlags.None);
				if (ex16 == null)
				{
					throw;
				}
				ex9 = ex16;
				goto IL_0361;
			}
		}
		catch
		{
			//try-fault
			io.closeAll();
			throw;
		}
		io.closeAll();
		return result4;
	}
}
