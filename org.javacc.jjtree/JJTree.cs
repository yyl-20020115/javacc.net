using org.javacc.parser;
namespace org.javacc.jjtree;

public class JJTree 
{
	private IO io;

	private void WriteLine(string text)
	{
		io.Msg.WriteLine(text);
	}
	
	private void InitializeOptions()
	{
		JJTreeOptions.Init();
		JJTreeGlobals.Initialize();
	}

	private void HelpMessage()
	{
		WriteLine("Usage:");
		WriteLine("    jjtree option-settings inputfile");
		WriteLine("");
		WriteLine("\"option-settings\" is a sequence of settings separated by spaces.");
		WriteLine("Each option setting must be of one of the following forms:");
		WriteLine("");
		WriteLine("    -optionname=value (e.g., -STATIC=false)");
		WriteLine("    -optionname:value (e.g., -STATIC:false)");
		WriteLine("    -optionname       (equivalent to -optionname=true.  e.g., -STATIC)");
		WriteLine("    -NOoptionname     (equivalent to -optionname=false. e.g., -NOSTATIC)");
		WriteLine("");
		WriteLine("Option settings are not case-sensitive, so one can say \"-nOsTaTiC\" instead");
		WriteLine("of \"-NOSTATIC\".  Option values must be appropriate for the corresponding");
		WriteLine("option, and must be either an integer or a string value.");
		WriteLine("");
		WriteLine("The boolean valued options are:");
		WriteLine("");
		WriteLine("    STATIC                   (default true)");
		WriteLine("    MULTI                    (default false)");
		WriteLine("    NODE_DEFAULT_VOID        (default false)");
		WriteLine("    NODE_SCOPE_HOOK          (default false)");
		WriteLine("    NODE_USES_PARSER         (default false)");
		WriteLine("    BUILD_NODE_FILES         (default true)");
		WriteLine("    TRACK_TOKENS             (default false)");
		WriteLine("    VISITOR                  (default false)");
		WriteLine("");
		WriteLine("The string valued options are:");
		WriteLine("");
		WriteLine("    JDK_VERSION              (default \"1.5\")");
		WriteLine("    NODE_PREFIX              (default \"AST\")");
		WriteLine("    NODE_PACKAGE             (default \"\")");
		WriteLine("    NODE_EXTENDS             (default \"\")");
		WriteLine("    NODE_FACTORY             (default \"\")");
		WriteLine("    OUTPUT_FILE              (default remove input file suffix, Add .jj)");
		WriteLine("    OUTPUT_DIRECTORY         (default \"\")");
		WriteLine("    JJTREE_OUTPUT_DIRECTORY  (default value of OUTPUT_DIRECTORY option)");
		WriteLine("    VISITOR_DATA_TYPE        (default \"\")");
		WriteLine("    VISITOR_EXCEPTION        (default \"\")");
		WriteLine("");
		WriteLine("JJTree also accepts JavaCC options, which it inserts into the generated file.");
		WriteLine("");
		WriteLine("EXAMPLES:");
		WriteLine("    jjtree -STATIC=false mygrammar.jjt");
		WriteLine("");
		WriteLine("ABOUT JJTree:");
		WriteLine("    JJTree is a preprocessor for JavaCC that inserts actions into a");
		WriteLine("    JavaCC grammar to build parse trees for the input.");
		WriteLine("");
		WriteLine("    For more information, see the online JJTree documentation at ");
		WriteLine("    https://javacc.dev.java.net/doc/JJTree.html ");
		WriteLine("");
	}
	
	public JJTree()
	{
	}

	
	public virtual int Main(string[] strarr)
	{
		ASTNodeDescriptor.nodeIds = new ();
		ASTNodeDescriptor.nodeNames = new ();
		ASTNodeDescriptor.nodeSeen = new ();
		EntryPoint.reInitAll();
		JavaCCGlobals.bannerLine("Tree Builder", "");
		io = new IO();
		int result;
		try
		{
			InitializeOptions();
			if (strarr.Length == 0)
			{
				WriteLine("");
				HelpMessage();
				result = 1;
				goto IL_0070;
			}
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		string text;
		int result2;
		try
		{
			WriteLine("(type \"jjtree\" with no arguments for help)");
			text = strarr[strarr.Length - 1];
			if (Options.IsOption(text))
			{
				WriteLine(("Last argument \"")+(text)+("\" is not a filename"));
				result2 = 1;
				goto IL_00d9;
			}
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		try
		{
			result2 = 0;
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		int result3;
		while (true)
		{
			try
			{
				if (result2 < (nint)strarr.LongLength - 1)
				{
					if (!Options.IsOption(strarr[result2]))
					{
						WriteLine(("Argument \"")+(strarr[result2])+("\" must be an option setting."));
						result3 = 1;
						goto IL_0153;
					}
					goto IL_0160;
				}
			}
			catch
			{
				//try-fault
				io.CloseAll();
				throw;
			}
			break;
			IL_0153:
			io.CloseAll();
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
				io.CloseAll();
				throw;
			}
		}
		JJTreeIOException ex;
		try
		{
			JJTreeOptions.Validate();
			try
			{
				io.SetInput(text);
			}
			catch (JJTreeIOException x)
			{
				ex = x;
				goto IL_01b2;
			}
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		ASTGrammar aSTGrammar;
		ParseException ex3;
		System.Exception ex5;
		JJTreeIOException ex2;
		try
		{
			WriteLine(("Reading from file ")+(io.InputFileName)+(" . . ."));
			JJTreeGlobals.toolList = JavaCCGlobals.getToolNames(text);
			JJTreeGlobals.toolList.Add("JJTree");
			try
			{
				try
				{
					JJTreeParser jJTreeParser = new JJTreeParser(io.In);
					jJTreeParser.javacc_input();
					aSTGrammar = (ASTGrammar)jJTreeParser.jjtree.RootNode;
					//if (java.lang.Boolean.getBoolean("jjtree-dump"))
					//{
					//	aSTGrammar.dump(" ");
					//}
					try
					{
						io.SetOutput();
					}
					catch (JJTreeIOException x2)
					{
						ex2 = x2;
						goto IL_02e1;
					}
				}
				catch (ParseException x3)
				{
					ex3 = x3;
					goto IL_02ea;
				}
			}
			catch (System.Exception x4)
			{				
				System.Exception ex4 = x4; 
				; if (ex4 == null)
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
			io.CloseAll();
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
					aSTGrammar.Generate(io);
					io.Out.Close();
					NodeFiles.GenerateTreeConstants_java();
					NodeFiles.GenerateVisitor_java();
					JJTreeState.generateTreeState_java();
					WriteLine(("Annotated grammar generated successfully in ")+(io.OutputFileName));
				}
				catch (ParseException x5)
				{
					ex6 = x5;
					goto IL_03f7;
				}
			}
			catch (System.Exception x6)
			{
				System.Exception ex7 =x6;
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
			io.CloseAll();
			throw;
		}
		try
		{
			return 0;
		}
		finally
		{
			io.CloseAll();
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
			WriteLine(("Error parsing input: ")+((@this)));
			//Throwable.instancehelper_printStackTrace(@this, io.getMsg());
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		io.CloseAll();
		return result3;
		IL_0070:
		io.CloseAll();
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
			WriteLine(("Error parsing input: ")+((this2)));
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		io.CloseAll();
		return result3;
		IL_02f1:
		ex10 = ex5;
		goto IL_0451;
		IL_02ea:
		ex13 = ex3;
		goto IL_0404;
		IL_00d9:
		io.CloseAll();
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
			WriteLine(("Error setting input: ")+(
				(this3.Message)));
			result3 = 1;
		}
		catch
		{
			//try-fault
			io.CloseAll();
			throw;
		}
		io.CloseAll();
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
					WriteLine(("Error setting output: ")+(
						(this4.Message)));
					result4 = 1;
				}
				catch (ParseException x7)
				{
					ex12 =x7;
					goto IL_035a;
				}
			}
			catch (System.Exception x8)
			{
				System.Exception ex16 = x8;
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
			io.CloseAll();
			throw;
		}
		io.CloseAll();
		return result4;
	}
}
