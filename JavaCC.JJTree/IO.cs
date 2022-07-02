using System;
using System.IO;
using JavaCC.Parser;
namespace JavaCC.JJTree;

public class IO
{
	private string ifn = "";
	private string ofn = "";
	private TextReader reader;
	private TextWriter writer;
	private TextWriter message;
	private TextWriter error;
    internal virtual void Write(string text) => writer.Write(text);
    internal virtual void WriteLine() => writer.WriteLine();
    internal virtual void WriteLine(string text) => writer.WriteLine(text);
	private string CreateOutputFileName(string _text)
	{
		string text = JJTreeOptions.OutputFile;
		if (string.Equals(text, ""))
		{
			int num = (_text.LastIndexOf(Path.DirectorySeparatorChar));
			if (num >= 0)
			{
				_text = _text.Substring(num + 1);
			}
			int num2 = (_text.LastIndexOf((char)46));
			if (num2 == -1)
			{
				text += ".jj";
			}
			else
			{
				string @this = _text.Substring(num2);
				text = (!string.Equals(@this, ".jj"))
					? (_text.Substring(0, num2) + (".jj"))
					: _text + ".jj";
			}
		}
		return text;
	}
	internal IO()
	{
		ifn = "<uninitialized input>";
		message = Console.Out;
		error = Console.Error;
	}

	internal virtual TextWriter Out => writer;
	internal virtual string OutputFileName => ofn;
	internal virtual string InputFileName => ifn;
	internal virtual TextReader In => reader;
	internal virtual TextWriter Msg => message;
	internal virtual TextWriter Err => error;
	internal virtual void CloseAll()
	{
		if (writer != null)
		{
			writer.Close();
		}
		if (message != null)
		{
			message.Flush();
		}
		if (error != null)
		{
			error.Flush();
		}
	}
	internal virtual void SetInput(string path)
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
						var file = new FileInfo(path);
						if (!file.Exists)
						{
							var text = ("File ") + (path) + (" not found.");
							//
							throw new JJTreeIOException(text);
						}
						var d = new DirectoryInfo(path);
						if (d.Exists)
						{
							var text2 = (path)+(" is a directory. Please use a valid file name.");
							//
							throw new JJTreeIOException(text2);
						}
						if (JavaCCGlobals.IsGeneratedBy("JJTree", path))
						{
							var text3 = (path)+(" was generated by jjtree.  Cannot run jjtree again.");
							//
							throw new JJTreeIOException(text3);
						}
						ifn = file.FullName;
						reader = new StreamReader(ifn);
						return;
					}
					catch (System.Exception x)
					{
						var ex = x as NullReferenceException;
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

		string text6 = "File " + path + " not found.";
		//
		throw new JJTreeIOException(text6);
	IL_00de:

		string text7 = ("Security violation while trying to open ")+(path);
		//
		throw new JJTreeIOException(text7);
	}


	internal virtual void SetOutput()
	{
		try
		{
			JavaCCGlobals.CreateOutputDir(JJTreeOptions.JJTreeOutputDirectory);
			//
			var file = new FileInfo(Path.Combine(JJTreeOptions.JJTreeOutputDirectory.FullName, CreateOutputFileName(ifn)));	
			writer = new StreamWriter(ofn = file.FullName);
			return;
		}
		catch (IOException)
		{
		}

		string text = "Can't create output file "+ofn;
		//
		throw new JJTreeIOException(text);
	}
}
