namespace JavaCC.Parser;
using System;
using System.IO;
using System.Text;

public class OutputFile
{
	internal class NullOutputStream : Stream
	{

		private NullOutputStream()
		{
		}

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
			return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
			return offset;
        }

        public override void SetLength(long value)
        {
        }

        public override void Write(byte[] bytes, int offset, int length)
		{
		}


		public virtual void Write(byte[] bytes)
		{
		}


		public virtual void Write(int b4)
		{
		}
		public override void Close()
        {

        }
	}



	internal class TrapClosePrintWriter : TextWriter
	{

		private OutputFile this_00240;

		public override Encoding Encoding => Encoding.Default;

		public TrapClosePrintWriter(OutputFile output, Stream stream)
		{
			this_00240 = output;
		}


		public virtual void ClosePrintWriter()
		{
			base.Close();
		}


		public override void Close()
		{
			try
			{
				this_00240.Close();
				return;
			}
			catch (IOException)
			{
			}

			Console.Error.WriteLine(("Could not close ")+(this_00240.file.FullName));
		}
	}

	private const string MD5_LINE_PART_1 = "/* JavaCC - OriginalChecksum=";

	private const string MD5_LINE_PART_2 = " (do not edit this line) */";

	internal TrapClosePrintWriter pw;

	internal Stream dos;

	internal string toolName;


	internal FileInfo file;


	internal string compatibleVersion;


	internal string[] options;

	public bool needToWrite;


	private static char[] HEX_DIGITS;



	public OutputFile(FileInfo f)
	: this(f, null, null)
	{
	}


    public virtual TextWriter PrintWriter
    {
        get
        {
            if (pw == null)
            {
                //MessageDigest instance;
                //try
                //{
                //	instance = MessageDigest.getInstance("MD5");
                //}
                //catch (Exception x)
                //{
                //	goto IL_0025;
                //}

                dos = File.OpenRead(file.FullName);
                pw = new TrapClosePrintWriter(this, dos);
                string str = ((compatibleVersion != null) ? compatibleVersion : "4.1d1");
                pw.WriteLine(("/* ") + (JavaCCGlobals.getIdString(toolName, file.Name)) + (" Version ")
                    + (str)
                    + (" */")
                    );
                if (options != null)
                {
                    pw.WriteLine(("/* JavaCCOptions:") + (Options.getOptionsString(options)) + (" */")
                        );
                }
            }
            return pw;
            //IL_0025:
            //	throw (new IOException("No MD5 implementation"));
        }
    }

    public virtual void Close()
	{
		if (pw != null)
		{
			pw.WriteLine(("/* JavaCC - OriginalChecksum=")+(GetMD5sum())+(" (do not edit this line) */")
				);
			pw.ClosePrintWriter();
		}
	}


	public OutputFile(FileInfo f, string str, string[] strarr)
	{
		//TODO:
		toolName = "JavaCC";
		needToWrite = true;
		file = f;
		compatibleVersion = str;
		options = strarr;
		
		if (f.Exists)
		{
			var bufferedReader = (new StreamReader(f.FullName));
			var printWriter = new StreamWriter(f.FullName+".diag");
			//string text = null;
			string text2;
			while ((text2 = bufferedReader.ReadLine()) != null)
			{
				if (text2.StartsWith( "/* JavaCC - OriginalChecksum="))
				{
				}
				else
				{
					printWriter.WriteLine(text2);
				}
			}
			printWriter.Close();
			//if (text == null || !string.Equals(text, anObject))
			//{
			//	needToWrite = false;
			//	if (str != null)
			//	{
			//		CheckVersion(f, str);
			//	}
			//	if (strarr != null)
			//	{
			//		CheckOptions(f, strarr);
			//	}
			//}
			//else
			{
				Console.WriteLine(("File \"")+(f.Name)+("\" is being rebuilt.")
					);
				needToWrite = true;
			}
		}
		else
		{
			Console.WriteLine(("File \"")+(f.Name)+("\" does not exist.  Will create one.")
				);
			needToWrite = true;
		}
		return;
	//IL_0060:
	//	throw new System.Exception("No MD5 implementation");
	}

	public virtual void SetToolName(string str)
	{
		toolName = str;
	}



	private static string ToHexString(byte[] bytes)
	{
		var stringBuilder = new StringBuilder(32);
		for (int i = 0; i < bytes.Length; i++)
		{
			int num = bytes[i];
			stringBuilder.Append(HEX_DIGITS[(num & 0xF0) >> 4]).Append(HEX_DIGITS[num & 0xF]);
		}
		return stringBuilder.ToString();
	}


	private void CheckVersion(FileInfo info, string name)
	{
		string text = ("/* ")+(JavaCCGlobals.getIdString(toolName, info.Name))+(" Version ")
			;
		try
		{
			try
			{
				var bufferedReader = (new StreamReader(info.FullName));
				string @this;
				while ((@this = bufferedReader.ReadLine()) != null)
				{
					if (@this.StartsWith(text))
					{
						//TODO:
						string text2 =
							text.Replace(".* Version ", "").Replace(" \\*/", "");
						if ((object)text2 != name)
						{
							JavaCCErrors.Warning((info.Name)+
								(": File is obsolete.  Please rename or delete this file so")+(" that a new one can be generated for you.")
								);
						}
						break;
					}
				}
				return;
			}
			catch (FileNotFoundException)
			{
			}
		}
		catch (IOException)
		{
			goto IL_00b7;
		}

		JavaCCErrors.Semantic_Error(("Could not open file ")+(info.Name)+(" for writing.")
			);

		throw new System.Exception();
	IL_00b7:
		;

	}


	private void CheckOptions(FileInfo P_0, string[] text)
	{
		try
		{
			try
			{
				var bufferedReader = new StreamReader(P_0.FullName);
				string @this;
				var s = "";
				while ((@this = bufferedReader.ReadLine()) != null)
				{
					if (@this.StartsWith( "/* JavaCCOptions:"))
					{
						string optionsString = Options.getOptionsString(text);
						//object obj = (s.___003Cref_003E = optionsString);
						if (!(@this.Contains(s)))
						{
							JavaCCErrors.Warning((P_0.Name)+(": Generated using incompatible options. Please rename or delete this file so")+(" that a new one can be generated for you.")
								);
						}
						break;
					}
				}
				return;
			}
			catch (FileNotFoundException)
			{
			}
		}
		catch (IOException)
		{
			goto IL_0082;
		}

		JavaCCErrors.Semantic_Error(("Could not open file ")+(P_0.Name)+(" for writing.")
			);

		throw new System.Exception();
	IL_0082:
		;

	}


	private string GetMD5sum()
	{
		//TODO:
		pw.Flush();
		//byte[] array = dos.GetMessageDigest().digest();
		//string result = toHexString(array);
		//return result;
		return "";
	}

	public virtual string GetToolName()
	{
		return toolName;
	}

	static OutputFile()
	{
		HEX_DIGITS = new char[16]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};
	}
}
