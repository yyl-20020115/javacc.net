using System;
using System.IO;
using System.Text;

namespace org.javacc.parser;


public class JavaCharStream
{
	public const bool staticFlag = false;

	public int bufpos;

	internal int bufsize;

	internal int available;

	internal int tokenBegin;

	protected internal int[] bufline;

	protected internal int[] bufcolumn;

	protected internal int column;

	protected internal int line;

	protected internal bool prevCharIsCR;

	protected internal bool prevCharIsLF;

	protected internal TextReader inputStream;

	protected internal char[] nextCharBuf;

	protected internal char[] buffer;

	protected internal int maxNextCharInd;

	protected internal int nextCharInd;

	protected internal int inBuf;

	protected internal int tabSize;


	public JavaCharStream(Stream @is, string str, int i1, int i2)
	: this(@is, str, i1, i2, 4096)
	{
	}


	public virtual void ReInit(Stream @is, string str, int i1, int i2)
	{
		ReInit(@is, str, i1, i2, 4096);
	}


	public JavaCharStream(TextReader r, int i1, int i2)
		: this(r, i1, i2, 4096)
	{
	}


	public virtual void ReInit(TextReader r, int i1, int i2)
	{
		ReInit(r, i1, i2, 4096);
	}

	public virtual void adjustBeginLineColumn(int i1, int i2)
	{
		int num = tokenBegin;
		int num2 = ((bufpos < tokenBegin) ? (bufsize - tokenBegin + bufpos + 1 + inBuf) : (bufpos - tokenBegin + inBuf + 1));
		int j = 0;
		int num3 = 0;
		_ = 0;
		_ = 0;
		int num4 = 0;
		for (; j < num2; j++)
		{
			int[] array = bufline;
			int num5 = num;
			int num6 = bufsize;
			int num7 = array[num3 = ((num6 != -1) ? (num5 % num6) : 0)];
			int[] array2 = bufline;
			num++;
			int num8 = num;
			int num9 = bufsize;
			int num10;
			if (num7 != array2[num10 = ((num9 != -1) ? (num8 % num9) : 0)])
			{
				break;
			}
			bufline[num3] = i1;
			int num11 = num4 + bufcolumn[num10] - bufcolumn[num3];
			bufcolumn[num3] = i2 + num4;
			num4 = num11;
		}
		if (j < num2)
		{
			int[] array3 = bufline;
			int num12 = num3;
			int num13 = i1;
			i1++;
			array3[num12] = num13;
			bufcolumn[num3] = i2 + num4;
			while (true)
			{
				int num14 = j;
				j++;
				if (num14 >= num2)
				{
					break;
				}
				int[] array4 = bufline;
				int num15 = num;
				int num16 = bufsize;
				int num17 = array4[num3 = ((num16 != -1) ? (num15 % num16) : 0)];
				int[] array5 = bufline;
				num++;
				int num18 = num;
				int num19 = bufsize;
				if (num17 != array5[(num19 != -1) ? (num18 % num19) : 0])
				{
					int[] array6 = bufline;
					int num20 = num3;
					int num21 = i1;
					i1++;
					array6[num20] = num21;
				}
				else
				{
					bufline[num3] = i1;
				}
			}
		}
		line = bufline[num3];
		column = bufcolumn[num3];
	}


	public virtual char readChar()
	{
		//Discarded unreachable code: IL_0132
		int num;
		JavaCharStream javaCharStream;
		if (inBuf > 0)
		{
			inBuf--;
			num = bufpos + 1;
			javaCharStream = this;
			int num2 = num;
			javaCharStream.bufpos = num;
			if (num2 == bufsize)
			{
				bufpos = 0;
			}
			return buffer[bufpos];
		}
		num = bufpos + 1;
		javaCharStream = this;
		int num3 = num;
		javaCharStream.bufpos = num;
		if (num3 == available)
		{
			AdjustBuffSize();
		}
		char[] array = buffer;
		int num4 = bufpos;
		int num5;
		num = (num5 = ReadByte());
		int num6 = num4;
		char[] array2 = array;
		int num7 = num;
		array2[num6] = (char)num;
		if (num7 == 92)
		{
			UpdateLineColumn((char)num5);
			int num8 = 1;
			while (true)
			{
				num = bufpos + 1;
				javaCharStream = this;
				int num9 = num;
				javaCharStream.bufpos = num;
				if (num9 == available)
				{
					AdjustBuffSize();
				}
				try
				{
					char[] array3 = buffer;
					int num10 = bufpos;
					num = (num5 = ReadByte());
					num6 = num10;
					array2 = array3;
					int num11 = num;
					array2[num6] = (char)num;
					if (num11 != 92)
					{
						UpdateLineColumn((char)num5);
						if (num5 == 117 && (num8 & 1) == 1)
						{
							num = bufpos - 1;
							javaCharStream = this;
							int num12 = num;
							javaCharStream.bufpos = num;
							if (num12 < 0)
							{
								bufpos = bufsize - 1;
							}
							break;
						}
						goto IL_0126;
					}
				}
				catch (IOException)
				{
					goto IL_0121;
				}
				UpdateLineColumn((char)num5);
				num8++;
				continue;
			IL_0126:
				try
				{
					backup(num8);
					return '\\';
				}
				catch (IOException)
				{
				}
				object obj = null;
				goto IL_0141;
			IL_0121:
				obj = null;
				goto IL_0141;
			IL_0141:
				if (num8 > 1)
				{
					backup(num8 - 1);
				}
				return '\\';
			}
			try
			{
				while ((num5 = ReadByte()) == 117)
				{
					column++;
				}
				buffer[bufpos] = (char)(num5 = (ushort)((hexval((char)num5) << 12) | (hexval(ReadByte()) << 8) | (hexval(ReadByte()) << 4) | hexval(ReadByte())));
				column += 4;
			}
			catch (IOException)
			{
				goto IL_01d9;
			}
			if (num8 == 1)
			{
				return (char)num5;
			}
			backup(num8 - 1);
			return '\\';
		}
		UpdateLineColumn((char)num5);
		return (char)num5;
	IL_01d9:

		string message = ("Invalid escape character at line ")+(line)+(" column ")
			+(column)
			+(".")
			.ToString();

		throw new System.Exception(message);
	}


	public virtual string GetImage()
	{
		if (bufpos >= tokenBegin)
		{
			string result = new string(buffer, tokenBegin, bufpos - tokenBegin + 1);

			return result;
		}
		string result2 = (
			new string(buffer, tokenBegin, bufsize - tokenBegin))+(
			new string(buffer, 0, bufpos + 1)).ToString();

		return result2;
	}

	public virtual int getBeginLine()
	{
		return bufline[tokenBegin];
	}

	public virtual int getBeginColumn()
	{
		return bufcolumn[tokenBegin];
	}

	public virtual int getEndLine()
	{
		return bufline[bufpos];
	}

	public virtual int getEndColumn()
	{
		return bufcolumn[bufpos];
	}


	public virtual char BeginToken()
	{
		if (inBuf > 0)
		{
			inBuf--;
			int num = bufpos + 1;
			bufpos = num;
			if (num == bufsize)
			{
				bufpos = 0;
			}
			tokenBegin = bufpos;
			return buffer[bufpos];
		}
		tokenBegin = 0;
		bufpos = -1;
		char result = readChar();

		return result;
	}

	public virtual void backup(int i)
	{
		inBuf += i;
		int num = bufpos - i;
		bufpos = num;
		if (num < 0)
		{
			bufpos += bufsize;
		}
	}

	public virtual char[] GetSuffix(int i)
	{
		char[] array = new char[i];
		if (bufpos + 1 >= i)
		{
			Array.Copy(buffer, bufpos - i + 1, array, 0, i);
		}
		else
		{
			Array.Copy(buffer, bufsize - (i - bufpos - 1), array, 0, i - bufpos - 1);
			Array.Copy(buffer, 0, array, i - bufpos - 1, bufpos + 1);
		}
		return array;
	}


	protected internal virtual void FillBuff()
	{
		if (maxNextCharInd == 4096)
		{
			int num = 0;
			nextCharInd = num;
			maxNextCharInd = num;
		}
		IOException ex;
		try
		{
			int num2;
			if ((num2 = inputStream.Read(nextCharBuf, maxNextCharInd, 4096 - maxNextCharInd)) == -1)
			{
				inputStream.Close();

				throw new IOException();
			}
			maxNextCharInd += num2;
			return;
		}
		catch (IOException x)
		{
			ex = x;
		}
		IOException ex2 = ex;
		if (bufpos != 0)
		{
			bufpos--;
			backup(0);
		}
		else
		{
			bufline[bufpos] = line;
			bufcolumn[bufpos] = column;
		}
		throw (ex2);
	}


	protected internal virtual void ExpandBuff(bool b)
	{
		char[] dest = new char[bufsize + 2048];
		int[] dest2 = new int[bufsize + 2048];
		int[] dest3 = new int[bufsize + 2048];
		System.Exception ex;
		try
		{
			if (b)
			{
				Array.Copy(buffer, tokenBegin, dest, 0, bufsize - tokenBegin);
				Array.Copy(buffer, 0, dest, bufsize - tokenBegin, bufpos);
				buffer = dest;
				Array.Copy(bufline, tokenBegin, dest2, 0, bufsize - tokenBegin);
				Array.Copy(bufline, 0, dest2, bufsize - tokenBegin, bufpos);
				bufline = dest2;
				Array.Copy(bufcolumn, tokenBegin, dest3, 0, bufsize - tokenBegin);
				Array.Copy(bufcolumn, 0, dest3, bufsize - tokenBegin, bufpos);
				bufcolumn = dest3;
				bufpos += bufsize - tokenBegin;
			}
			else
			{
				Array.Copy(buffer, tokenBegin, dest, 0, bufsize - tokenBegin);
				buffer = dest;
				Array.Copy(bufline, tokenBegin, dest2, 0, bufsize - tokenBegin);
				bufline = dest2;
				Array.Copy(bufcolumn, tokenBegin, dest3, 0, bufsize - tokenBegin);
				bufcolumn = dest3;
				bufpos -= tokenBegin;
			}
		}
		catch (System.Exception x)
		{
			ex =x;
			goto IL_01ca;
		}
		int num = bufsize + 2048;
		bufsize = num;
		available = num;
		tokenBegin = 0;
		return;
	IL_01ca:
		System.Exception @this = ex;
		string message = @this.Message;// @this.Message;

		throw new System.Exception(message);
	}


	protected internal virtual void AdjustBuffSize()
	{
		if (available == bufsize)
		{
			if (tokenBegin > 2048)
			{
				bufpos = 0;
				available = tokenBegin;
			}
			else
			{
				ExpandBuff(b: false);
			}
		}
		else if (available > tokenBegin)
		{
			available = bufsize;
		}
		else if (tokenBegin - available < 2048)
		{
			ExpandBuff(b: true);
		}
		else
		{
			available = tokenBegin;
		}
	}


	protected internal virtual char ReadByte()
	{
		int num = nextCharInd + 1;
		nextCharInd = num;
		if (num >= maxNextCharInd)
		{
			FillBuff();
		}
		return nextCharBuf[nextCharInd];
	}

	protected internal virtual void UpdateLineColumn(char ch)
	{
		column++;
		if (prevCharIsLF)
		{
			prevCharIsLF = false;
			int num = line;
			int num2 = 1;
			int num3 = num2;
			column = num2;
			line = num + num3;
		}
		else if (prevCharIsCR)
		{
			prevCharIsCR = false;
			if (ch == '\n')
			{
				prevCharIsLF = true;
			}
			else
			{
				int num4 = line;
				int num2 = 1;
				int num5 = num2;
				column = num2;
				line = num4 + num5;
			}
		}
		switch (ch)
		{
			case '\r':
				prevCharIsCR = true;
				break;
			case '\n':
				prevCharIsLF = true;
				break;
			case '\t':
				{
					column--;
					int num6 = column;
					int num7 = tabSize;
					int num8 = column;
					int num9 = tabSize;
					column = num6 + (num7 - ((num9 != -1) ? (num8 % num9) : 0));
					break;
				}
		}
		bufline[bufpos] = line;
		bufcolumn[bufpos] = column;
	}



	internal static int hexval(char P_0)
	{
		switch (P_0)
		{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case 'A':
			case 'a':
				return 10;
			case 'B':
			case 'b':
				return 11;
			case 'C':
			case 'c':
				return 12;
			case 'D':
			case 'd':
				return 13;
			case 'E':
			case 'e':
				return 14;
			case 'F':
			case 'f':
				return 15;
			default:

				throw new IOException();
		}
	}


	public JavaCharStream(TextReader r, int i1, int i2, int i3)
	{
		bufpos = -1;
		column = 0;
		line = 1;
		prevCharIsCR = false;
		prevCharIsLF = false;
		maxNextCharInd = 0;
		nextCharInd = -1;
		inBuf = 0;
		tabSize = 8;
		inputStream = r;
		line = i1;
		column = i2 - 1;
		bufsize = i3;
		available = i3;
		buffer = new char[i3];
		bufline = new int[i3];
		bufcolumn = new int[i3];
		nextCharBuf = new char[4096];
	}

	public virtual void ReInit(TextReader r, int i1, int i2, int i3)
	{
		inputStream = r;
		line = i1;
		column = i2 - 1;
		int num;
		if (buffer == null || (nint)i3 != (nint)buffer.LongLength)
		{
			num = i3;
			int num2 = num;
			bufsize = num;
			available = num2;
			buffer = new char[i3];
			bufline = new int[i3];
			bufcolumn = new int[i3];
			nextCharBuf = new char[4096];
		}
		num = 0;
		int num3 = num;
		prevCharIsCR = (byte)num != 0;
		prevCharIsLF = (byte)num3 != 0;
		num = 0;
		int num4 = num;
		maxNextCharInd = num;
		num = num4;
		int num5 = num;
		inBuf = num;
		tokenBegin = num5;
		num = -1;
		int num6 = num;
		bufpos = num;
		nextCharInd = num6;
	}


	public JavaCharStream(Stream @is, string str, int i1, int i2, int i3)
	: this((str != null) ? new StreamReader(@is, str) : new StreamReader(@is), i1, i2, i3)
	{
	}


	public JavaCharStream(Stream @is, int i1, int i2, int i3)
		: this(new StreamReader(@is), i1, i2, 4096)
	{
	}


	public virtual void ReInit(Stream @is, string str, int i1, int i2, int i3)
	{
		ReInit((str != null) ? new StreamReader(@is) : new StreamReader(@is), i1, i2, i3);
	}


	public virtual void ReInit(Stream @is, int i1, int i2, int i3)
	{
		ReInit(new StreamReader(@is), i1, i2, i3);
	}

	protected internal virtual void setTabSize(int i)
	{
		tabSize = i;
	}

	protected internal virtual int getTabSize(int i)
	{
		return tabSize;
	}

	[Obsolete]
	public virtual int getColumn()
	{
		return bufcolumn[bufpos];
	}

	[Obsolete]
	public virtual int getLine()
	{
		return bufline[bufpos];
	}


	public JavaCharStream(TextReader r)
		: this(r, 1, 1, 4096)
	{
	}


	public virtual void ReInit(TextReader r)
	{
		ReInit(r, 1, 1, 4096);
	}


	public JavaCharStream(Stream @is, int i1, int i2)
		: this(@is, i1, i2, 4096)
	{
	}


	public JavaCharStream(Stream @is, string str)
	: this(@is, str, 1, 1, 4096)
	{
	}


	public JavaCharStream(Stream @is)
		: this(@is, 1, 1, 4096)
	{
	}


	public virtual void ReInit(Stream @is, int i1, int i2)
	{
		ReInit(@is, i1, i2, 4096);
	}


	public virtual void ReInit(Stream @is, string str)
	{
		ReInit(@is, str, 1, 1, 4096);
	}


	public virtual void ReInit(Stream @is)
	{
		ReInit(@is, 1, 1, 4096);
	}

	public virtual void Done()
	{
		nextCharBuf = null;
		buffer = null;
		bufline = null;
		bufcolumn = null;
	}
}
