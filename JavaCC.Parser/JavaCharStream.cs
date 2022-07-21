namespace JavaCC.Parser;
using System;
using System.IO;

public class JavaCharStream
{
    public const bool StaticFlag = false;

    public int BufPos = 0;

    public int BufSize = 0;

    public int Available = 0;

    public int TokenBegin = 0;

    protected internal int[] BufLine = Array.Empty<int>();

    protected internal int[] BufColumn = Array.Empty<int>();

    protected internal int Column = 0;

    protected internal int Line = 0;

    protected internal bool PrevCharIsCR = false;

    protected internal bool PrevCharIsLF = false;

    protected internal TextReader Reader;

    protected internal char[] NextCharBuf = Array.Empty<char>();

    protected internal char[] Buffer = Array.Empty<char>();

    protected internal int MaxNextCharInd = 0;

    protected internal int NextCharInd = 0;

    protected internal int InBuf = 0;

    protected internal int TabSize = 0;


    public JavaCharStream(Stream stream, string str, int i1, int i2)
    : this(stream, str, i1, i2, 4096)
    {
    }


    public virtual void ReInit(Stream stream, string str, int i1, int i2)
    {
        ReInit(stream, str, i1, i2, 4096);
    }


    public JavaCharStream(TextReader r, int i1, int i2)
        : this(r, i1, i2, 4096)
    {
    }


    public virtual void ReInit(TextReader reader, int i1, int i2)
    {
        ReInit(reader, i1, i2, 4096);
    }

    public virtual void AdjustBeginLineColumn(int i1, int i2)
    {
        int num = TokenBegin;
        int num2 = ((BufPos < TokenBegin) ? (BufSize - TokenBegin + BufPos + 1 + InBuf) : (BufPos - TokenBegin + InBuf + 1));
        int j = 0;
        int num3 = 0;
        int num4 = 0;
        for (; j < num2; j++)
        {
            int[] array = BufLine;
            int num5 = num;
            int num6 = BufSize;
            int num7 = array[num3 = ((num6 != -1) ? (num5 % num6) : 0)];
            int[] array2 = BufLine;
            num++;
            int num8 = num;
            int num9 = BufSize;
            int num10;
            if (num7 != array2[num10 = ((num9 != -1) ? (num8 % num9) : 0)])
            {
                break;
            }
            BufLine[num3] = i1;
            int num11 = num4 + BufColumn[num10] - BufColumn[num3];
            BufColumn[num3] = i2 + num4;
            num4 = num11;
        }
        if (j < num2)
        {
            int[] array3 = BufLine;
            int num12 = num3;
            int num13 = i1;
            i1++;
            array3[num12] = num13;
            BufColumn[num3] = i2 + num4;
            while (true)
            {
                int num14 = j;
                j++;
                if (num14 >= num2)
                {
                    break;
                }
                int[] array4 = BufLine;
                int num15 = num;
                int num16 = BufSize;
                int num17 = array4[num3 = ((num16 != -1) ? (num15 % num16) : 0)];
                int[] array5 = BufLine;
                num++;
                int num18 = num;
                int num19 = BufSize;
                if (num17 != array5[(num19 != -1) ? (num18 % num19) : 0])
                {
                    int[] array6 = BufLine;
                    int num20 = num3;
                    int num21 = i1;
                    i1++;
                    array6[num20] = num21;
                }
                else
                {
                    BufLine[num3] = i1;
                }
            }
        }
        Line = BufLine[num3];
        Column = BufColumn[num3];
    }


    public virtual char ReadChar()
    {
        //Discarded unreachable code: IL_0132
        int num;
        JavaCharStream javaCharStream;
        if (InBuf > 0)
        {
            InBuf--;
            num = BufPos + 1;
            javaCharStream = this;
            int num2 = num;
            javaCharStream.BufPos = num;
            if (num2 == BufSize)
            {
                BufPos = 0;
            }
            return Buffer[BufPos];
        }
        num = BufPos + 1;
        javaCharStream = this;
        int num3 = num;
        javaCharStream.BufPos = num;
        if (num3 == Available)
        {
            AdjustBuffSize();
        }
        char[] array = Buffer;
        int num4 = BufPos;
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
                num = BufPos + 1;
                javaCharStream = this;
                int num9 = num;
                javaCharStream.BufPos = num;
                if (num9 == Available)
                {
                    AdjustBuffSize();
                }
                try
                {
                    char[] array3 = Buffer;
                    int num10 = BufPos;
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
                            num = BufPos - 1;
                            javaCharStream = this;
                            int num12 = num;
                            javaCharStream.BufPos = num;
                            if (num12 < 0)
                            {
                                BufPos = BufSize - 1;
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
                    Backup(num8);
                    return '\\';
                }
                catch (IOException)
                {
                }
                goto IL_0141;
            IL_0121:
                goto IL_0141;
            IL_0141:
                if (num8 > 1)
                {
                    Backup(num8 - 1);
                }
                return '\\';
            }
            try
            {
                while ((num5 = ReadByte()) == 117)
                {
                    Column++;
                }
                Buffer[BufPos] = (char)(num5 = (ushort)((hexval((char)num5) << 12) | (hexval(ReadByte()) << 8) | (hexval(ReadByte()) << 4) | hexval(ReadByte())));
                Column += 4;
            }
            catch (IOException)
            {
                goto IL_01d9;
            }
            if (num8 == 1)
            {
                return (char)num5;
            }
            Backup(num8 - 1);
            return '\\';
        }
        UpdateLineColumn((char)num5);
        return (char)num5;
    IL_01d9:

        string message = ("Invalid escape character at line ") + (Line) + (" column ")
            + (Column)
            + (".")
            ;

        throw new Exception(message);
    }


    public virtual string GetImage()
    {
        if (BufPos >= TokenBegin)
        {
            string result = new string(Buffer, TokenBegin, BufPos - TokenBegin + 1);

            return result;
        }
        string result2 = (
            new string(Buffer, TokenBegin, BufSize - TokenBegin)) + (
            new string(Buffer, 0, BufPos + 1));

        return result2;
    }

    public virtual int BeginLine => BufLine[TokenBegin];

    public virtual int BeginColumn => BufColumn[TokenBegin];

    public virtual int EndLine => BufLine[BufPos];

    public virtual int EndColumn => BufColumn[BufPos];


    public virtual char BeginToken
    {
        get
        {
            if (InBuf > 0)
            {
                InBuf--;
                int num = BufPos + 1;
                BufPos = num;
                if (num == BufSize)
                {
                    BufPos = 0;
                }
                TokenBegin = BufPos;
                return Buffer[BufPos];
            }
            TokenBegin = 0;
            BufPos = -1;
            char result = ReadChar();

            return result;
        }
    }

    public virtual void Backup(int i)
    {
        InBuf += i;
        int num = BufPos - i;
        BufPos = num;
        if (num < 0)
        {
            BufPos += BufSize;
        }
    }

    public virtual char[] GetSuffix(int i)
    {
        char[] array = new char[i];
        if (BufPos + 1 >= i)
        {
            Array.Copy(Buffer, BufPos - i + 1, array, 0, i);
        }
        else
        {
            Array.Copy(Buffer, BufSize - (i - BufPos - 1), array, 0, i - BufPos - 1);
            Array.Copy(Buffer, 0, array, i - BufPos - 1, BufPos + 1);
        }
        return array;
    }


    protected internal virtual void FillBuff()
    {
        if (MaxNextCharInd == 4096)
        {
            int num = 0;
            NextCharInd = num;
            MaxNextCharInd = num;
        }
        IOException ex;
        try
        {
            int num2;
            if ((num2 = Reader.Read(NextCharBuf, MaxNextCharInd, 4096 - MaxNextCharInd)) == -1)
            {
                Reader.Close();

                throw new IOException();
            }
            MaxNextCharInd += num2;
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException ex2 = ex;
        if (BufPos != 0)
        {
            BufPos--;
            Backup(0);
        }
        else
        {
            BufLine[BufPos] = Line;
            BufColumn[BufPos] = Column;
        }
        throw (ex2);
    }


    protected internal virtual void ExpandBuff(bool b)
    {
        char[] dest = new char[BufSize + 2048];
        int[] dest2 = new int[BufSize + 2048];
        int[] dest3 = new int[BufSize + 2048];
        try
        {
            if (b)
            {
                Array.Copy(Buffer, TokenBegin, dest, 0, BufSize - TokenBegin);
                Array.Copy(Buffer, 0, dest, BufSize - TokenBegin, BufPos);
                Buffer = dest;
                Array.Copy(BufLine, TokenBegin, dest2, 0, BufSize - TokenBegin);
                Array.Copy(BufLine, 0, dest2, BufSize - TokenBegin, BufPos);
                BufLine = dest2;
                Array.Copy(BufColumn, TokenBegin, dest3, 0, BufSize - TokenBegin);
                Array.Copy(BufColumn, 0, dest3, BufSize - TokenBegin, BufPos);
                BufColumn = dest3;
                BufPos += BufSize - TokenBegin;
            }
            else
            {
                Array.Copy(Buffer, TokenBegin, dest, 0, BufSize - TokenBegin);
                Buffer = dest;
                Array.Copy(BufLine, TokenBegin, dest2, 0, BufSize - TokenBegin);
                BufLine = dest2;
                Array.Copy(BufColumn, TokenBegin, dest3, 0, BufSize - TokenBegin);
                BufColumn = dest3;
                BufPos -= TokenBegin;
            }
        }
        catch (Exception ex2)
        {

            throw ex2;
        }
        int num = BufSize + 2048;
        BufSize = num;
        Available = num;
        TokenBegin = 0;
        return;
    }


    protected internal virtual void AdjustBuffSize()
    {
        if (Available == BufSize)
        {
            if (TokenBegin > 2048)
            {
                BufPos = 0;
                Available = TokenBegin;
            }
            else
            {
                ExpandBuff(b: false);
            }
        }
        else if (Available > TokenBegin)
        {
            Available = BufSize;
        }
        else if (TokenBegin - Available < 2048)
        {
            ExpandBuff(b: true);
        }
        else
        {
            Available = TokenBegin;
        }
    }


    protected internal virtual char ReadByte()
    {
        int num = NextCharInd + 1;
        NextCharInd = num;
        if (num >= MaxNextCharInd)
        {
            FillBuff();
        }
        return NextCharBuf[NextCharInd];
    }

    protected internal virtual void UpdateLineColumn(char ch)
    {
        Column++;
        if (PrevCharIsLF)
        {
            PrevCharIsLF = false;
            int num = Line;
            int num2 = 1;
            int num3 = num2;
            Column = num2;
            Line = num + num3;
        }
        else if (PrevCharIsCR)
        {
            PrevCharIsCR = false;
            if (ch == '\n')
            {
                PrevCharIsLF = true;
            }
            else
            {
                int num4 = Line;
                int num2 = 1;
                int num5 = num2;
                Column = num2;
                Line = num4 + num5;
            }
        }
        switch (ch)
        {
            case '\r':
                PrevCharIsCR = true;
                break;
            case '\n':
                PrevCharIsLF = true;
                break;
            case '\t':
                {
                    Column--;
                    int num6 = Column;
                    int num7 = TabSize;
                    int num8 = Column;
                    int num9 = TabSize;
                    Column = num6 + (num7 - ((num9 != -1) ? (num8 % num9) : 0));
                    break;
                }
        }
        BufLine[BufPos] = Line;
        BufColumn[BufPos] = Column;
    }



    internal static int hexval(char c)
    {
        switch (c)
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
        BufPos = -1;
        Column = 0;
        Line = 1;
        PrevCharIsCR = false;
        PrevCharIsLF = false;
        MaxNextCharInd = 0;
        NextCharInd = -1;
        InBuf = 0;
        TabSize = 8;
        Reader = r;
        Line = i1;
        Column = i2 - 1;
        BufSize = i3;
        Available = i3;
        Buffer = new char[i3];
        BufLine = new int[i3];
        BufColumn = new int[i3];
        NextCharBuf = new char[4096];
    }

    public virtual void ReInit(TextReader r, int i1, int i2, int i3)
    {
        Reader = r;
        Line = i1;
        Column = i2 - 1;
        int num;
        if (Buffer == null || i3 != Buffer.Length)
        {
            num = i3;
            int num2 = num;
            BufSize = num;
            Available = num2;
            Buffer = new char[i3];
            BufLine = new int[i3];
            BufColumn = new int[i3];
            NextCharBuf = new char[4096];
        }
        num = 0;
        int num3 = num;
        PrevCharIsCR = (byte)num != 0;
        PrevCharIsLF = (byte)num3 != 0;
        num = 0;
        int num4 = num;
        MaxNextCharInd = num;
        num = num4;
        int num5 = num;
        InBuf = num;
        TokenBegin = num5;
        num = -1;
        int num6 = num;
        BufPos = num;
        NextCharInd = num6;
    }


    public JavaCharStream(Stream @is, string str, int i1, int i2, int i3)
    : this((str != null) ? new StreamReader(@is) : new StreamReader(@is), i1, i2, i3)
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
        NextCharBuf = null;
        Buffer = null;
        BufLine = null;
        BufColumn = null;
    }
}
