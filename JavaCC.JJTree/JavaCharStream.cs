namespace JavaCC.JJTree;
using System;
using System.IO;

public class JavaCharStream
{
    public const bool StaticFlag = false;

    public int BufPos = 0;

    internal int BufSize = 0;

    internal int Available = 0;

    internal int TokenBegin = 0;

    protected internal int[] BufLine = Array.Empty<int>();

    protected internal int[] BufColumn = Array.Empty<int>();

    protected internal int column = 0;

    protected internal int line = 0;

    protected internal bool prevCharIsCR = false;

    protected internal bool prevCharIsLF = false;

    protected internal TextReader inputStream;

    protected internal char[] nextCharBuf = Array.Empty<char>();

    protected internal char[] buffer = Array.Empty<char>();

    protected internal int maxNextCharInd = 0;

    protected internal int nextCharInd = 0;

    protected internal int inBuf = 0;

    protected internal int tabSize = 0;


    public JavaCharStream(Stream @is, string str, int i1, int i2)
        : this(@is, str, i1, i2, 4096) { }

    public virtual void ReInit(Stream @is, string str, int i1, int i2)
    {
        ReInit(@is, str, i1, i2, 4096);
    }


    public JavaCharStream(TextReader r, int i1, int i2)
        : this(r, i1, i2, 4096) { }

    public virtual void ReInit(TextReader r, int i1, int i2)
    {
        ReInit(r, i1, i2, 4096);
    }


    public virtual char ReadChar()
    {
        //Discarded unreachable code: IL_0132
        int s;
        JavaCharStream javaCharStream;
        if (inBuf > 0)
        {
            inBuf--;
            s = BufPos + 1;
            javaCharStream = this;
            int num2 = s;
            javaCharStream.BufPos = s;
            if (num2 == BufSize)
            {
                BufPos = 0;
            }
            return buffer[BufPos];
        }
        s = BufPos + 1;
        javaCharStream = this;
        int num3 = s;
        javaCharStream.BufPos = s;
        if (num3 == Available)
        {
            AdjustBuffSize();
        }
        char[] array = buffer;
        int num4 = BufPos;
        int num5;
        s = (num5 = ReadByte());
        int num6 = num4;
        char[] array2 = array;
        int num7 = s;
        array2[num6] = (char)s;
        if (num7 == 92)
        {
            UpdateLineColumn((char)num5);
            int num8 = 1;
            while (true)
            {
                s = BufPos + 1;
                javaCharStream = this;
                int num9 = s;
                javaCharStream.BufPos = s;
                if (num9 == Available)
                {
                    AdjustBuffSize();
                }
                try
                {
                    char[] array3 = buffer;
                    int num10 = BufPos;
                    s = (num5 = ReadByte());
                    num6 = num10;
                    array2 = array3;
                    int num11 = s;
                    array2[num6] = (char)s;
                    if (num11 != 92)
                    {
                        UpdateLineColumn((char)num5);
                        if (num5 == 117 && (num8 & 1) == 1)
                        {
                            s = BufPos - 1;
                            javaCharStream = this;
                            int num12 = s;
                            javaCharStream.BufPos = s;
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
                    column++;
                }
                buffer[BufPos] = (char)(num5 = (ushort)((HexVal((char)num5) << 12) | (HexVal(ReadByte()) << 8) | (HexVal(ReadByte()) << 4) | HexVal(ReadByte())));
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
            Backup(num8 - 1);
            return '\\';
        }
        UpdateLineColumn((char)num5);
        return (char)num5;
    IL_01d9:

        string message = ("Invalid escape character at line ") + (line) + (" column ")
            + (column)
            + (".")
            ;

        throw new System.Exception(message);
    }


    public virtual string GetImage()
    {
        if (BufPos >= TokenBegin)
        {
            return new string(buffer, TokenBegin, BufPos - TokenBegin + 1);
        }
        return (new string(buffer, TokenBegin, BufSize - TokenBegin) + (new string(buffer, 0, BufPos + 1)));
    }

    public virtual int BeginLine => BufLine[TokenBegin];

    public virtual int BeginColumn => BufColumn[TokenBegin];

    public virtual int EndLine => BufLine[BufPos];

    public virtual int EndColumn => BufColumn[BufPos];


    public virtual char BeginToken()
    {
        if (inBuf > 0)
        {
            inBuf--;
            int num = BufPos + 1;
            BufPos = num;
            if (num == BufSize)
            {
                BufPos = 0;
            }
            TokenBegin = BufPos;
            return buffer[BufPos];
        }
        TokenBegin = 0;
        BufPos = -1;
        char result = ReadChar();

        return result;
    }

    public virtual void Backup(int i)
    {
        inBuf += i;
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
            Array.Copy(buffer, BufPos - i + 1, array, 0, i);
        }
        else
        {
            Array.Copy(buffer, BufSize - (i - BufPos - 1), array, 0, i - BufPos - 1);
            Array.Copy(buffer, 0, array, i - BufPos - 1, BufPos + 1);
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
        if (BufPos != 0)
        {
            BufPos--;
            Backup(0);
        }
        else
        {
            BufLine[BufPos] = line;
            BufColumn[BufPos] = column;
        }
        throw (ex);
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
                Array.Copy(buffer, TokenBegin, dest, 0, BufSize - TokenBegin);
                Array.Copy(buffer, 0, dest, BufSize - TokenBegin, BufPos);
                buffer = dest;
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
                Array.Copy(buffer, TokenBegin, dest, 0, BufSize - TokenBegin);
                buffer = dest;
                Array.Copy(BufLine, TokenBegin, dest2, 0, BufSize - TokenBegin);
                BufLine = dest2;
                Array.Copy(BufColumn, TokenBegin, dest3, 0, BufSize - TokenBegin);
                BufColumn = dest3;
                BufPos -= TokenBegin;
            }
        }
        catch (Exception x)
        {
            throw x;
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
        BufLine[BufPos] = line;
        BufColumn[BufPos] = column;
    }

    internal static int HexVal(char c) => c switch
    {
        '0' => 0,
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'A' or 'a' => 10,
        'B' or 'b' => 11,
        'C' or 'c' => 12,
        'D' or 'd' => 13,
        'E' or 'e' => 14,
        'F' or 'f' => 15,
        _ => throw new IOException(),
    };


    public JavaCharStream(TextReader r, int i1, int i2, int i3)
    {
        BufPos = -1;
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
        BufSize = i3;
        Available = i3;
        buffer = new char[i3];
        BufLine = new int[i3];
        BufColumn = new int[i3];
        nextCharBuf = new char[4096];
    }

    public virtual void ReInit(TextReader r, int i1, int i2, int i3)
    {
        inputStream = r;
        line = i1;
        column = i2 - 1;
        int num;
        if (buffer == null || i3 != buffer.Length)
        {
            num = i3;
            int num2 = num;
            BufSize = num;
            Available = num2;
            buffer = new char[i3];
            BufLine = new int[i3];
            BufColumn = new int[i3];
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
        TokenBegin = num5;
        num = -1;
        int num6 = num;
        BufPos = num;
        nextCharInd = num6;
    }


    public JavaCharStream(Stream @is, string str, int i1, int i2, int i3)
    : this((str != null) ? new StreamReader(@is) : new StreamReader(@is), i1, i2, i3) { }


    public JavaCharStream(Stream @is, int i1, int i2, int i3)
        : this(new StreamReader(@is), i1, i2, 4096) { }


    public virtual void ReInit(Stream @is, string str, int i1, int i2, int i3)
    {
        ReInit((str != null) ? new StreamReader(@is) : new StreamReader(@is), i1, i2, i3);
    }


    public virtual void ReInit(Stream @is, int i1, int i2, int i3)
    {
        ReInit(new StreamReader(@is), i1, i2, i3);
    }

    protected internal virtual int TabSize { get => tabSize; set => tabSize = value; }

    public virtual int Column => BufColumn[BufPos];

    public virtual int Line => BufLine[BufPos];


    public JavaCharStream(TextReader r)
        : this(r, 1, 1, 4096) { }

    public virtual void ReInit(TextReader r)
    {
        ReInit(r, 1, 1, 4096);
    }


    public JavaCharStream(Stream @is, int i1, int i2)
        : this(@is, i1, i2, 4096) { }

    public JavaCharStream(Stream @is, string str)
        : this(@is, str, 1, 1, 4096) { }


    public JavaCharStream(Stream @is)
        : this(@is, 1, 1, 4096) { }

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
        BufLine = null;
        BufColumn = null;
    }

    public virtual void AdjustBeginLineColumn(int i1, int i2)
    {
        int num = TokenBegin;
        int num2 = ((BufPos < TokenBegin) ? (BufSize - TokenBegin + BufPos + 1 + inBuf) : (BufPos - TokenBegin + inBuf + 1));
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
        line = BufLine[num3];
        column = BufColumn[num3];
    }
}
