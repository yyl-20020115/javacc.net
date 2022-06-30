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
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
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
	}



	internal class TrapClosePrintWriter : TextWriter
	{

		private OutputFile this_00240;

		public override Encoding Encoding => throw new System.NotImplementedException();

		public TrapClosePrintWriter(OutputFile P_0, Stream P_1)
		{
			this_00240 = P_0;
		}


		public virtual void closePrintWriter()
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

			Console.Error.WriteLine(new StringBuilder().Append("Could not close ").Append(this_00240.file.getAbsolutePath()).ToString());
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



	public static void ___003Cclinit_003E()
	{
	}


	public OutputFile(FileInfo f)
	: this(f, null, null)
	{
	}


	public virtual TextWriter getPrintWriter()
	{
		NoSuchAlgorithmException ex;
		if (pw == null)
		{
			MessageDigest instance;
			try
			{
				instance = MessageDigest.getInstance("MD5");
			}
			catch (NoSuchAlgorithmException x)
			{
				ex = ByteCodeHelper.MapException<NoSuchAlgorithmException>(x, ByteCodeHelper.MapFlags.NoRemapping);
				goto IL_0025;
			}

			dos = new DigestOutputStream(new BufferedOutputStream(new FileOutputStream(file)), instance);
			pw = new TrapClosePrintWriter(this, dos);
			string str = ((compatibleVersion != null) ? compatibleVersion : "4.1d1");
			pw.WriteLine(new StringBuilder().Append("/* ").Append(JavaCCGlobals.getIdString(toolName, file.getName())).Append(" Version ")
				.Append(str)
				.Append(" */")
				.ToString());
			if (options != null)
			{
				pw.WriteLine(new StringBuilder().Append("/* JavaCCOptions:").Append(Options.getOptionsString(options)).Append(" */")
					.ToString());
			}
		}
		return pw;
	IL_0025:
		NoSuchAlgorithmException cause = ex;
		throw new System.Exception((IOException)Throwable.instancehelper_initCause(new IOException("No MD5 implementation"), cause));
	}


	public virtual void close()
	{
		if (pw != null)
		{
			pw.WriteLine(new StringBuilder().Append("/* JavaCC - OriginalChecksum=").Append(getMD5sum()).Append(" (do not edit this line) */")
				.ToString());
			pw.closePrintWriter();
		}
	}


	public OutputFile(File f, string str, string[] strarr)
	{
		toolName = "JavaCC";
		needToWrite = true;
		file = f;
		compatibleVersion = str;
		options = strarr;
		NoSuchAlgorithmException ex;
		if (f.Exists)
		{
			BufferedReader bufferedReader = new BufferedReader(new FileReader(f));
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
			while ((text2 = bufferedReader.readLine()) != null)
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
					checkVersion(f, str);
				}
				if (strarr != null)
				{
					checkOptions(f, strarr);
				}
			}
			else
			{
				Console.WriteLine(new StringBuilder().Append("File \"").Append(f.getName()).Append("\" is being rebuilt.")
					.ToString());
				needToWrite = true;
			}
		}
		else
		{
			Console.WriteLine(new StringBuilder().Append("File \"").Append(f.getName()).Append("\" does not exist.  Will create one.")
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
		StringBuilder stringBuilder = new StringBuilder(32);
		for (int i = 0; i < (nint)P_0.LongLength; i++)
		{
			int num = P_0[i];
			stringBuilder.Append(HEX_DIGITS[(num & 0xF0) >> 4]).Append(HEX_DIGITS[num & 0xF]);
		}
		string result = stringBuilder.ToString();

		return result;
	}


	private void checkVersion(File P_0, string P_1)
	{
		string text = new StringBuilder().Append("/* ").Append(JavaCCGlobals.getIdString(toolName, P_0.getName())).Append(" Version ")
			.ToString();
		try
		{
			try
			{
				BufferedReader bufferedReader = new BufferedReader(new FileReader(P_0));
				string @this;
				while ((@this = bufferedReader.readLine()) != null)
				{
					if (String.instancehelper_startsWith(@this, text))
					{
						string text2 = String.instancehelper_replaceAll(String.instancehelper_replaceFirst(text, ".* Version ", ""), " \\*/", "");
						if ((object)text2 != P_1)
						{
							JavaCCErrors.Warning(new StringBuilder().Append(P_0.getName()).Append(": File is obsolete.  Please rename or delete this file so").Append(" that a new one can be generated for you.")
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

		JavaCCErrors.Semantic_Error(new StringBuilder().Append("Could not open file ").Append(P_0.getName()).Append(" for writing.")
			.ToString());

		throw new System.Exception();
	IL_00b7:
		;

	}


	private void checkOptions(File P_0, string[] P_1)
	{
		try
		{
			try
			{
				BufferedReader bufferedReader = new BufferedReader(new FileReader(P_0));
				string @this;
				CharSequence s = default(CharSequence);
				while ((@this = bufferedReader.readLine()) != null)
				{
					if (String.instancehelper_startsWith(@this, "/* JavaCCOptions:"))
					{
						string optionsString = Options.getOptionsString(P_1);
						//object obj = (s.___003Cref_003E = optionsString);
						if (!String.instancehelper_contains(@this, s))
						{
							JavaCCErrors.Warning(new StringBuilder().Append(P_0.getName()).Append(": Generated using incompatible options. Please rename or delete this file so").Append(" that a new one can be generated for you.")
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

		JavaCCErrors.Semantic_Error(new StringBuilder().Append("Could not open file ").Append(P_0.getName()).Append(" for writing.")
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
