using System;
using System.IO;
using System.Text;
using org.javacc.parser;

namespace org.javacc.jjtree;

public class IO 
{
	private string ifn;

	private string ofn;

	private TextReader @in;

	private TextWriter @out;

	private TextWriter msg;

	private TextWriter err;

	
	internal virtual void Write(string P_0)
	{
		@out.Write(P_0);
	}

	
	internal virtual void WriteLine()
	{
		@out.WriteLine();
	}

	internal virtual TextWriter getOut()
	{
		return @out;
	}

	internal virtual string getOutputFileName()
	{
		return ofn;
	}

	
	internal virtual void WriteLine(string P_0)
	{
		@out.WriteLine(P_0);
	}

	
	private string create_output_file_name(string P_0)
	{
		string text = JJTreeOptions.getOutputFile();
		if (string.Equals(text, ""))
		{
			int num = (P_0.LastIndexOf(Path.DirectorySeparatorChar));
			if (num >= 0)
			{
				P_0 = P_0.Substring(num + 1);
			}
			int num2 = (P_0.LastIndexOf((char) 46));
			if (num2 == -1)
			{
				text = new StringBuilder().Append(P_0).Append(".jj").ToString();
			}
			else
			{
				string @this = P_0.Substring(num2);
				text = ((!string.Equals(@this, ".jj")) ? new StringBuilder().Append((P_0.Substring( 0, num2))).Append(".jj").ToString() : new StringBuilder().Append(P_0).Append(".jj").ToString());
			}
		}
		return text;
	}

	
	internal IO()
	{
		ifn = "<uninitialized input>";
		msg = Console.Out;
		err = Console.Error;
	}

	internal virtual string getInputFileName()
	{
		return ifn;
	}

	internal virtual TextReader getIn()
	{
		return @in;
	}

	internal virtual TextWriter getMsg()
	{
		return msg;
	}

	internal virtual TextWriter getErr()
	{
		return err;
	}

	
	internal virtual void closeAll()
	{
		if (@out != null)
		{
			@out.Close();
		}
		if (msg != null)
		{
			msg.Flush();
		}
		if (err != null)
		{
			err.Flush();
		}
	}

	
		internal virtual void setInput(string P_0)
	{
		NullReferenceException ex2;
		IOException ex5;
		try
		{
			try
			{
				try
				{
					try
					{
						FileInfo file = new FileInfo(P_0);
						if (!file.Exists)
						{
							string text = new StringBuilder().Append("File ").Append(P_0).Append(" not found.")
								.ToString();
							//
							throw new JJTreeIOException(text);
						}
						if (file.isDirectory())
						{
							string text2 = new StringBuilder().Append(P_0).Append(" is a directory. Please use a valid file name.").ToString();
							//
							throw new JJTreeIOException(text2);
						}
						if (JavaCCGlobals.isGeneratedBy("JJTree", P_0))
						{
							string text3 = new StringBuilder().Append(P_0).Append(" was generated by jjtree.  Cannot run jjtree again.").ToString();
							//
							throw new JJTreeIOException(text3);
						}
						ifn = file.getPath();
						@in = new FileReader(ifn);
						return;
					}
					catch (System.Exception x)
					{
						NullReferenceException ex = ByteCodeHelper.MapException<NullPointerException>(x, ByteCodeHelper.MapFlags.None);
						if (ex == null)
						{
							throw;
						}
						ex2 = ex;
					}
				}
				catch (System.Exception)
				{
					goto IL_00de;
				}
			}
			catch (FileNotFoundException)
			{
				goto IL_00e1;
			}
		}
		catch (IOException x2)
		{
			ex5 = x2;
			goto IL_00e4;
		}
		NullReferenceException @this = ex2;
		string text4 = (@this.Message);
		//
		throw new JJTreeIOException(text4);
		IL_00e4:
		IOException this2 = ex5;
		string text5 = (this2.Message);
		//
		throw new JJTreeIOException(text5);
		IL_00e1:
		
		string text6 = new StringBuilder().Append("File ").Append(P_0).Append(" not found.")
			.ToString();
		//
		throw new JJTreeIOException(text6);
		IL_00de:
		
		string text7 = new StringBuilder().Append("Security violation while trying to open ").Append(P_0).ToString();
		//
		throw new JJTreeIOException(text7);
	}

	
		internal virtual void setOutput()
	{
		try
		{
			JavaCCGlobals.createOutputDir(JJTreeOptions.getJJTreeOutputDirectory());
			//
			FileInfo file = new FileInfo(JJTreeOptions.getJJTreeOutputDirectory(), create_output_file_name(ifn));
			ofn = file.ToString();
			@out = new StringWriter(file.FullName);
			return;
		}
		catch (IOException)
		{
		}
		
		string text = new StringBuilder().Append("Can't create output file ").Append(ofn).ToString();
		//
		throw new JJTreeIOException(text);
	}
}
