using System;
using System.IO;
using System.Text;

namespace org.javacc.parser;


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

        public override void Write(byte[] P_0, int P_1, int P_2)
		{
		}


		public virtual void Write(byte[] P_0)
		{
		}


		public virtual void Write(int P_0)
		{
		}
		public override void Close()
        {

        }
	}



	internal class TrapClosePrintWriter : TextWriter
	{

		private OutputFile this_00240;

		public override Encoding Encoding => throw new System.NotImplementedException();

		public TrapClosePrintWriter(OutputFile P_0, Stream P_1)
		{
			this_00240 = P_0;
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

			Console.Error.WriteLine(("Could not close ")+(this_00240.file.getAbsolutePath()).ToString());
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


	public virtual TextWriter getPrintWriter()
	{
		if (pw == null)
		{
			MessageDigest instance;
			try
			{
				instance = MessageDigest.getInstance("MD5");
			}
			catch (Exception x)
			{
				goto IL_0025;
			}

			dos = new DigestOutputStream(new BufferedOutputStream(new FileOutputStream(file)), instance);
			pw = new TrapClosePrintWriter(this, dos);
			string str = ((compatibleVersion != null) ? compatibleVersion : "4.1d1");
			pw.WriteLine(("/* ")+(JavaCCGlobals.getIdString(toolName, file.Name))+(" Version ")
				+(str)
				+(" */")
				.ToString());
			if (options != null)
			{
				pw.WriteLine(("/* JavaCCOptions:")+(Options.getOptionsString(options))+(" */")
					.ToString());
			}
		}
		return pw;
	IL_0025:
		NoSuchAlgorithmException cause = ex;
		throw new System.Exception((IOException)Throwable.instancehelper_initCause(new IOException("No MD5 implementation"), cause));
	}


	public virtual void Close()
	{
		if (pw != null)
		{
			pw.WriteLine(("/* JavaCC - OriginalChecksum=")+(getMD5sum())+(" (do not edit this line) */")
				.ToString());
			pw.ClosePrintWriter();
		}
	}


	public OutputFile(FileInfo f, string str, string[] strarr)
	{
		toolName = "JavaCC";
		needToWrite = true;
		file = f;
		compatibleVersion = str;
		options = strarr;
		NoSuchAlgorithmException ex;
		if (f.Exists)
		{
			BufferedReader bufferedReader = new BufferedReader(new StreamReader(f));
			MessageDigest instance;
			try
			{
				instance = MessageDigest.getInstance("MD5");
			}
			catch (NoSuchAlgorithmException x)
			{
				ex = ByteCodeHelper.MapException<NoSuchAlgorithmException>(x, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_0060;
			}
			DigestOutputStream digestOutputStream = new DigestOutputStream(new NullOutputStream(null), instance);
			TextWriter printWriter = new TextWriter(digestOutputStream);
			string text = null;
			string text2;
			CharSequence charSequence = default(CharSequence);
			while ((text2 = bufferedReader.ReadLine()) != null)
			{
				if (String.instancehelper_startsWith(text2, "/* JavaCC - OriginalChecksum="))
				{
					object __003Cref_003E = "";
					//object obj = (charSequence.___003Cref_003E = "/* JavaCC - OriginalChecksum=");
					CharSequence target = charSequence;
					//obj = (charSequence.___003Cref_003E = __003Cref_003E);
					string @this = String.instancehelper_replace(text2, target, charSequence);
					//obj = "";
					//__003Cref_003E = (charSequence.___003Cref_003E = " (do not edit this line) */");
					CharSequence target2 = charSequence;
					//__003Cref_003E = (charSequence.___003Cref_003E = obj);
					text = String.instancehelper_replace(@this, target2, charSequence);
				}
				else
				{
					printWriter.WriteLine(text2);
				}
			}
			printWriter.Close();
			string anObject = toHexString(digestOutputStream.getMessageDigest().digest());
			if (text == null || !string.Equals(text, anObject))
			{
				needToWrite = false;
				if (str != null)
				{
					CheckVersion(f, str);
				}
				if (strarr != null)
				{
					CheckOptions(f, strarr);
				}
			}
			else
			{
				Console.WriteLine(("File \"")+(f.Names)+("\" is being rebuilt.")
					.ToString());
				needToWrite = true;
			}
		}
		else
		{
			Console.WriteLine(("File \"")+(f.Name)+("\" does not exist.  Will create one.")
				.ToString());
			needToWrite = true;
		}
		return;
	IL_0060:
		NoSuchAlgorithmException cause = ex;
		throw new System.Exception((IOException)Throwable.instancehelper_initCause(new IOException("No MD5 implementation"), cause));
	}

	public virtual void setToolName(string str)
	{
		toolName = str;
	}



	private static string toHexString(byte[] P_0)
	{
		var stringBuilder = new StringBuilder(32);
		for (int i = 0; i < (nint)P_0.LongLength; i++)
		{
			int num = P_0[i];
			stringBuilder+(HEX_DIGITS[(num & 0xF0) >> 4])+(HEX_DIGITS[num & 0xF]);
		}
		string result = stringBuilder.ToString();

		return result;
	}


	private void CheckVersion(FileInfo info, string name)
	{
		string text = ("/* ")+(JavaCCGlobals.getIdString(toolName, info.Name))+(" Version ")
			.ToString();
		try
		{
			try
			{
				BufferedReader bufferedReader = new BufferedReader(new StreamReader(info));
				string @this;
				while ((@this = bufferedReader.ReadLine()) != null)
				{
					if (String.instancehelper_startsWith(@this, text))
					{
						string text2 = String.instancehelper_replaceAll(String.instancehelper_replaceFirst(text, ".* Version ", ""), " \\*/", "");
						if ((object)text2 != name)
						{
							JavaCCErrors.Warning((info.Name)+(": File is obsolete.  Please rename or delete this file so")+(" that a new one can be generated for you.")
								.ToString());
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
			.ToString());

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
				CharSequence s = default(CharSequence);
				while ((@this = bufferedReader.ReadLine()) != null)
				{
					if (String.instancehelper_startsWith(@this, "/* JavaCCOptions:"))
					{
						string optionsString = Options.getOptionsString(text);
						//object obj = (s.___003Cref_003E = optionsString);
						if (!String.instancehelper_contains(@this, s))
						{
							JavaCCErrors.Warning((P_0.Name)+(": Generated using incompatible options. Please rename or delete this file so")+(" that a new one can be generated for you.")
								.ToString());
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
			.ToString());

		throw new System.Exception();
	IL_0082:
		;

	}


	private string getMD5sum()
	{
		pw.flush();
		byte[] array = dos.getMessageDigest().digest();
		string result = toHexString(array);

		return result;
	}

	public virtual string getToolName()
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
