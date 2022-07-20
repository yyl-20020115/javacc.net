namespace JavaCC.Parser;
using System;
using System.IO;
using System.Text;

public class JavaFiles : JavaCCGlobals //, JavaCCParserConstants
{
    public const string charStreamVersion = "4.1";

    public const string tokenManagerVersion = "4.1";

    public const string tokenVersion = "4.1";

    public const string parseExceptionVersion = "4.1";

    public const string tokenMgrErrorVersion = "4.1";


    public static string ReplaceBackslash(string name)
    {
        int num = 0;
        int num2 = name.Length;
        while (num < num2)
        {
            int index = num;
            num++;
            if ((name[index]) == '\\')
            {
                break;
            }
        }
        if (num == num2)
        {
            return name;
        }
        var builder = new StringBuilder();
        for (num = 0; num < num2; num++)
        {
            int c;
            if ((c = (name[num])) == 92)
            {
                builder.Append("\\\\");
            }
            else
            {
                builder.Append((char)c);
            }
        }
        return builder.ToString();
    }


    public JavaFiles()
    {
    }


    public static double GetVersion(string name)
    {
        //Discarded unreachable code: IL_006a
        string text = ("/* ") + (JavaCCGlobals.getIdString("JavaCC", name)) + (" Version ")
            ;

        var file = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, ReplaceBackslash(name)));
        if (!file.Exists)
        {
            double.TryParse("4.1", out var r);
            return r;
        }
        TextReader bufferedReader = null;
        double num;
        try
        {
            try
            {
                bufferedReader = (new StreamReader(file.FullName));
                num = 0.0;
                string @this;
                while ((@this = bufferedReader.ReadLine()) != null)
                {
                    if (!@this.StartsWith(text))
                    {
                        continue;
                    }
                    @this = @this.Substring(text.Length);
                    int num2 = @this.IndexOf((char)32);
                    if (num2 >= 0)
                    {
                        @this = @this.Substring(0, num2);
                    }
                    if (@this.Length > 0)
                    {
                        if (!double.TryParse(@this, out num))
                            goto IL_0117;
                    }
                    break;
                }
            }
            catch (IOException)
            {
                goto IL_011a;
            }
        }
        catch
        {
            //try-fault
            if (bufferedReader != null)
            {
                try
                {
                    bufferedReader.Close();
                }
                catch (IOException)
                {
                    goto IL_010e;
                }
            }
            goto end_IL_00fb;
        IL_010e:

        end_IL_00fb:
            throw;
        }
        goto IL_0120;
    IL_011a:
        try
        {
            num = 0.0;
        }
        catch
        {
            //try-fault
            if (bufferedReader != null)
            {
                try
                {
                    bufferedReader.Close();
                }
                catch (IOException)
                {
                    goto IL_0163;
                }
            }
            goto end_IL_0150;
        IL_0163:

        end_IL_0150:
            throw;
        }
        if (bufferedReader != null)
        {
            try
            {
                bufferedReader.Close();
                return num;
            }
            catch (IOException)
            {
            }

        }
        return num;
    IL_0117:

        goto IL_0120;
    IL_0120:
        double result = num;
        if (bufferedReader != null)
        {
            try
            {
                bufferedReader.Close();
                return result;
            }
            catch (IOException)
            {
            }

        }
        return result;
    }


    public static void gen_JavaCharStream()
    {
        IOException ex;
        try
        {

            FileInfo f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "JavaCharStream.java"));

            OutputFile outputFile = new OutputFile(f, "4.1", new string[1] { "STATIC" });
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            string str = ((!Options.Static) ? "  " : "  static ");
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * An implementation of interface CharStream, where the stream is assumed to");
            printWriter.WriteLine(" * contain only ASCII characters (with java-like unicode escape processing).");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("");
            printWriter.WriteLine("public class JavaCharStream");
            printWriter.WriteLine("{");
            printWriter.WriteLine("/** Whether parser is static. */");
            printWriter.WriteLine(("  public static final boolean staticFlag = ") + (Options.Static) + (";")
                );
            printWriter.WriteLine("  static final int hexval(char c) throws java.io.IOException {");
            printWriter.WriteLine("    switch(c)");
            printWriter.WriteLine("    {");
            printWriter.WriteLine("       case '0' :");
            printWriter.WriteLine("          return 0;");
            printWriter.WriteLine("       case '1' :");
            printWriter.WriteLine("          return 1;");
            printWriter.WriteLine("       case '2' :");
            printWriter.WriteLine("          return 2;");
            printWriter.WriteLine("       case '3' :");
            printWriter.WriteLine("          return 3;");
            printWriter.WriteLine("       case '4' :");
            printWriter.WriteLine("          return 4;");
            printWriter.WriteLine("       case '5' :");
            printWriter.WriteLine("          return 5;");
            printWriter.WriteLine("       case '6' :");
            printWriter.WriteLine("          return 6;");
            printWriter.WriteLine("       case '7' :");
            printWriter.WriteLine("          return 7;");
            printWriter.WriteLine("       case '8' :");
            printWriter.WriteLine("          return 8;");
            printWriter.WriteLine("       case '9' :");
            printWriter.WriteLine("          return 9;");
            printWriter.WriteLine("");
            printWriter.WriteLine("       case 'a' :");
            printWriter.WriteLine("       case 'A' :");
            printWriter.WriteLine("          return 10;");
            printWriter.WriteLine("       case 'b' :");
            printWriter.WriteLine("       case 'B' :");
            printWriter.WriteLine("          return 11;");
            printWriter.WriteLine("       case 'c' :");
            printWriter.WriteLine("       case 'C' :");
            printWriter.WriteLine("          return 12;");
            printWriter.WriteLine("       case 'd' :");
            printWriter.WriteLine("       case 'D' :");
            printWriter.WriteLine("          return 13;");
            printWriter.WriteLine("       case 'e' :");
            printWriter.WriteLine("       case 'E' :");
            printWriter.WriteLine("          return 14;");
            printWriter.WriteLine("       case 'f' :");
            printWriter.WriteLine("       case 'F' :");
            printWriter.WriteLine("          return 15;");
            printWriter.WriteLine("    }");
            printWriter.WriteLine("");
            printWriter.WriteLine("    throw new java.io.IOException(); // Should never come here");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Position in buffer. */");
            printWriter.WriteLine((str) + ("public int bufpos = -1;"));
            printWriter.WriteLine((str) + ("int bufsize;"));
            printWriter.WriteLine((str) + ("int available;"));
            printWriter.WriteLine((str) + ("int tokenBegin;"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine((str) + ("protected int bufline[];"));
                printWriter.WriteLine((str) + ("protected int bufcolumn[];"));
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected int column = 0;"));
                printWriter.WriteLine((str) + ("protected int line = 1;"));
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected boolean prevCharIsCR = false;"));
                printWriter.WriteLine((str) + ("protected boolean prevCharIsLF = false;"));
            }
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected java.io.TextReader inputStream;"));
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected char[] nextCharBuf;"));
            printWriter.WriteLine((str) + ("protected char[] buffer;"));
            printWriter.WriteLine((str) + ("protected int maxNextCharInd = 0;"));
            printWriter.WriteLine((str) + ("protected int nextCharInd = -1;"));
            printWriter.WriteLine((str) + ("protected int inBuf = 0;"));
            printWriter.WriteLine((str) + ("protected int tabSize = 8;"));
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void setTabSize(int i) { tabSize = i; }"));
            printWriter.WriteLine((str) + ("protected int getTabSize(int i) { return tabSize; }"));
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void ExpandBuff(boolean wrapAround)"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     char[] newbuffer = new char[bufsize + 2048];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     int newbufline[] = new int[bufsize + 2048];");
                printWriter.WriteLine("     int newbufcolumn[] = new int[bufsize + 2048];");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("     try");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        if (wrapAround)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           System.arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);");
            printWriter.WriteLine("           System.arraycopy(buffer, 0, newbuffer,");
            printWriter.WriteLine("                                             bufsize - tokenBegin, bufpos);");
            printWriter.WriteLine("           buffer = newbuffer;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           System.arraycopy(bufline, 0, newbufline, bufsize - tokenBegin, bufpos);");
                printWriter.WriteLine("           bufline = newbufline;");
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, 0, newbufcolumn, bufsize - tokenBegin, bufpos);");
                printWriter.WriteLine("           bufcolumn = newbufcolumn;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("           bufpos += (bufsize - tokenBegin);");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           System.arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);");
            printWriter.WriteLine("           buffer = newbuffer;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           bufline = newbufline;");
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           bufcolumn = newbufcolumn;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("           bufpos -= tokenBegin;");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     catch (Throwable t)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        throw new System.Exception(t.getMessage());");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     available = (bufsize += 2048);");
            printWriter.WriteLine("     tokenBegin = 0;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void FillBuff() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     int i;");
            printWriter.WriteLine("     if (maxNextCharInd == 4096)");
            printWriter.WriteLine("        maxNextCharInd = nextCharInd = 0;");
            printWriter.WriteLine("");
            printWriter.WriteLine("     try {");
            printWriter.WriteLine("        if ((i = inputStream.read(nextCharBuf, maxNextCharInd,");
            printWriter.WriteLine("                                            4096 - maxNextCharInd)) == -1)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           inputStream.Close();");
            printWriter.WriteLine("           throw new java.io.IOException();");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("           maxNextCharInd += i;");
            printWriter.WriteLine("        return;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     catch(java.io.IOException e) {");
            printWriter.WriteLine("        if (bufpos != 0)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           --bufpos;");
            printWriter.WriteLine("           backup(0);");
            printWriter.WriteLine("        }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("        else");
                printWriter.WriteLine("        {");
                printWriter.WriteLine("           bufline[bufpos] = line;");
                printWriter.WriteLine("           bufcolumn[bufpos] = column;");
                printWriter.WriteLine("        }");
            }
            printWriter.WriteLine("        throw e;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected char ReadByte() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (++nextCharInd >= maxNextCharInd)");
            printWriter.WriteLine("        FillBuff();");
            printWriter.WriteLine("");
            printWriter.WriteLine("     return nextCharBuf[nextCharInd];");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** @return starting character for token. */");
            printWriter.WriteLine((str) + ("public char BeginToken() throws java.io.IOException"));
            printWriter.WriteLine("  {     ");
            printWriter.WriteLine("     if (inBuf > 0)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        --inBuf;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        if (++bufpos == bufsize)");
            printWriter.WriteLine("           bufpos = 0;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        tokenBegin = bufpos;");
            printWriter.WriteLine("        return buffer[bufpos];");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     tokenBegin = 0;");
            printWriter.WriteLine("     bufpos = -1;");
            printWriter.WriteLine("");
            printWriter.WriteLine("     return readChar();");
            printWriter.WriteLine("  }     ");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void AdjustBuffSize()"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (available == bufsize)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        if (tokenBegin > 2048)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           bufpos = 0;");
            printWriter.WriteLine("           available = tokenBegin;");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("           ExpandBuff(false);");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     else if (available > tokenBegin)");
            printWriter.WriteLine("        available = bufsize;");
            printWriter.WriteLine("     else if ((tokenBegin - available) < 2048)");
            printWriter.WriteLine("        ExpandBuff(true);");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("        available = tokenBegin;");
            printWriter.WriteLine("  }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected void UpdateLineColumn(char c)"));
                printWriter.WriteLine("  {");
                printWriter.WriteLine("     column++;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (prevCharIsLF)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        prevCharIsLF = false;");
                printWriter.WriteLine("        line += (column = 1);");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("     else if (prevCharIsCR)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        prevCharIsCR = false;");
                printWriter.WriteLine("        if (c == '\\n')");
                printWriter.WriteLine("        {");
                printWriter.WriteLine("           prevCharIsLF = true;");
                printWriter.WriteLine("        }");
                printWriter.WriteLine("        else");
                printWriter.WriteLine("           line += (column = 1);");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     switch (c)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        case '\\r' :");
                printWriter.WriteLine("           prevCharIsCR = true;");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        case '\\n' :");
                printWriter.WriteLine("           prevCharIsLF = true;");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        case '\\t' :");
                printWriter.WriteLine("           column--;");
                printWriter.WriteLine("           column += (tabSize - (column % tabSize));");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        default :");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     bufline[bufpos] = line;");
                printWriter.WriteLine("     bufcolumn[bufpos] = column;");
                printWriter.WriteLine("  }");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Read a character. */");
            printWriter.WriteLine((str) + ("public char readChar() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (inBuf > 0)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        --inBuf;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        if (++bufpos == bufsize)");
            printWriter.WriteLine("           bufpos = 0;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        return buffer[bufpos];");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     char c;");
            printWriter.WriteLine("");
            printWriter.WriteLine("     if (++bufpos == available)");
            printWriter.WriteLine("        AdjustBuffSize();");
            printWriter.WriteLine("");
            printWriter.WriteLine("     if ((buffer[bufpos] = c = ReadByte()) == '\\\\')");
            printWriter.WriteLine("     {");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("        UpdateLineColumn(c);");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("        int backSlashCnt = 1;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        for (;;) // Read all the backslashes");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           if (++bufpos == available)");
            printWriter.WriteLine("              AdjustBuffSize();");
            printWriter.WriteLine("");
            printWriter.WriteLine("           try");
            printWriter.WriteLine("           {");
            printWriter.WriteLine("              if ((buffer[bufpos] = c = ReadByte()) != '\\\\')");
            printWriter.WriteLine("              {");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("                 UpdateLineColumn(c);");
            }
            printWriter.WriteLine("                 // found a non-backslash char.");
            printWriter.WriteLine("                 if ((c == 'u') && ((backSlashCnt & 1) == 1))");
            printWriter.WriteLine("                 {");
            printWriter.WriteLine("                    if (--bufpos < 0)");
            printWriter.WriteLine("                       bufpos = bufsize - 1;");
            printWriter.WriteLine("");
            printWriter.WriteLine("                    break;");
            printWriter.WriteLine("                 }");
            printWriter.WriteLine("");
            printWriter.WriteLine("                 backup(backSlashCnt);");
            printWriter.WriteLine("                 return '\\\\';");
            printWriter.WriteLine("              }");
            printWriter.WriteLine("           }");
            printWriter.WriteLine("           catch(java.io.IOException e)");
            printWriter.WriteLine("           {");
            printWriter.WriteLine("              if (backSlashCnt > 1)");
            printWriter.WriteLine("                 backup(backSlashCnt-1);");
            printWriter.WriteLine("");
            printWriter.WriteLine("              return '\\\\';");
            printWriter.WriteLine("           }");
            printWriter.WriteLine("");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("           UpdateLineColumn(c);");
            }
            printWriter.WriteLine("           backSlashCnt++;");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("");
            printWriter.WriteLine("        // Here, we have seen an odd number of backslash's followed by a 'u'");
            printWriter.WriteLine("        try");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           while ((c = ReadByte()) == 'u')");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("              ++column;");
            }
            else
            {
                printWriter.WriteLine("     ;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("           buffer[bufpos] = c = (char)(hexval(c) << 12 |");
            printWriter.WriteLine("                                       hexval(ReadByte()) << 8 |");
            printWriter.WriteLine("                                       hexval(ReadByte()) << 4 |");
            printWriter.WriteLine("                                       hexval(ReadByte()));");
            printWriter.WriteLine("");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("           column += 4;");
            }
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        catch(java.io.IOException e)");
            printWriter.WriteLine("        {");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("           throw new System.Exception(\"Invalid escape character at line \" + line +");
                printWriter.WriteLine("                                         \" column \" + column + \".\");");
            }
            else
            {
                printWriter.WriteLine("           throw new System.Exception(\"Invalid escape character in input\");");
            }
            printWriter.WriteLine("        }");
            printWriter.WriteLine("");
            printWriter.WriteLine("        if (backSlashCnt == 1)");
            printWriter.WriteLine("           return c;");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           backup(backSlashCnt - 1);");
            printWriter.WriteLine("           return '\\\\';");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("     {");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("        UpdateLineColumn(c);");
            }
            printWriter.WriteLine("        return c;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndColumn");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("public int getColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndLine");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("public int getLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Get end column. */");
            printWriter.WriteLine((str) + ("public int getEndColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Get end line. */");
            printWriter.WriteLine((str) + ("public int getEndLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** @return column of token start */");
            printWriter.WriteLine((str) + ("public int getBeginColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[tokenBegin];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** @return line number of token start */");
            printWriter.WriteLine((str) + ("public int getBeginLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[tokenBegin];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Retreat. */");
            printWriter.WriteLine((str) + ("public void backup(int amount) {"));
            printWriter.WriteLine("");
            printWriter.WriteLine("    inBuf += amount;");
            printWriter.WriteLine("    if ((bufpos -= amount) < 0)");
            printWriter.WriteLine("       bufpos += bufsize;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.TextReader dstream,");
            printWriter.WriteLine("                 int startline, int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            if (Options.Static)
            {
                printWriter.WriteLine("    if (inputStream != null)");
                printWriter.WriteLine("       throw new System.Exception(\"\\n   ERROR: Second call to the constructor of a static JavaCharStream.\\n\" +");
                printWriter.WriteLine("       \"       You must either use ReInit() or set the JavaCC option STATIC to false\\n\" +");
                printWriter.WriteLine("       \"       during the generation of this class.\");");
            }
            printWriter.WriteLine("    inputStream = dstream;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    line = startline;");
                printWriter.WriteLine("    column = startcolumn - 1;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("    available = bufsize = buffersize;");
            printWriter.WriteLine("    buffer = new char[buffersize];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    bufline = new int[buffersize];");
                printWriter.WriteLine("    bufcolumn = new int[buffersize];");
            }
            printWriter.WriteLine("    nextCharBuf = new char[4096];");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.TextReader dstream,");
            printWriter.WriteLine("                                        int startline, int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.TextReader dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream,");
            printWriter.WriteLine("                 int startline, int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("    inputStream = dstream;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    line = startline;");
                printWriter.WriteLine("    column = startcolumn - 1;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("    if (buffer == null || buffersize != buffer.length)");
            printWriter.WriteLine("    {");
            printWriter.WriteLine("      available = bufsize = buffersize;");
            printWriter.WriteLine("      buffer = new char[buffersize];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("      bufline = new int[buffersize];");
                printWriter.WriteLine("      bufcolumn = new int[buffersize];");
            }
            printWriter.WriteLine("      nextCharBuf = new char[4096];");
            printWriter.WriteLine("    }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    prevCharIsLF = prevCharIsCR = false;");
            }
            printWriter.WriteLine("    tokenBegin = inBuf = maxNextCharInd = 0;");
            printWriter.WriteLine("    nextCharInd = bufpos = -1;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream,");
            printWriter.WriteLine("                                        int startline, int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(encoding == null ? new java.io.StreamReader(dstream) : new java.io.StreamReader(dstream, encoding), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(new java.io.StreamReader(dstream), startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("                        int startcolumn) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, encoding, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("                        int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream, String encoding) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, encoding, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Constructor. */");
            printWriter.WriteLine("  public JavaCharStream(java.io.Stream dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(encoding == null ? new java.io.StreamReader(dstream) : new java.io.StreamReader(dstream, encoding), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(new java.io.StreamReader(dstream), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("                     int startcolumn) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, encoding, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("                     int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, encoding, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** @return token image as String */");
            printWriter.WriteLine((str) + ("public String GetImage()"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (bufpos >= tokenBegin)");
            printWriter.WriteLine("        return new String(buffer, tokenBegin, bufpos - tokenBegin + 1);");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("        return new String(buffer, tokenBegin, bufsize - tokenBegin) +");
            printWriter.WriteLine("                              new String(buffer, 0, bufpos + 1);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** @return suffix */");
            printWriter.WriteLine((str) + ("public char[] GetSuffix(int len)"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     char[] ret = new char[len];");
            printWriter.WriteLine("");
            printWriter.WriteLine("     if ((bufpos + 1) >= len)");
            printWriter.WriteLine("        System.arraycopy(buffer, bufpos - len + 1, ret, 0, len);");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        System.arraycopy(buffer, bufsize - (len - bufpos - 1), ret, 0,");
            printWriter.WriteLine("                                                          len - bufpos - 1);");
            printWriter.WriteLine("        System.arraycopy(buffer, 0, ret, len - bufpos - 1, bufpos + 1);");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     return ret;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** HashSet<object> buffers back to null when finished. */");
            printWriter.WriteLine((str) + ("public void Done()"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     nextCharBuf = null;");
            printWriter.WriteLine("     buffer = null;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     bufline = null;");
                printWriter.WriteLine("     bufcolumn = null;");
            }
            printWriter.WriteLine("  }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("  /**");
                printWriter.WriteLine("   * Method to adjust line and column numbers for the start of a token.");
                printWriter.WriteLine("   */");
                printWriter.WriteLine((str) + ("public void adjustBeginLineColumn(int newLine, int newCol)"));
                printWriter.WriteLine("  {");
                printWriter.WriteLine("     int start = tokenBegin;");
                printWriter.WriteLine("     int len;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (bufpos >= tokenBegin)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        len = bufpos - tokenBegin + inBuf + 1;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("     else");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        len = bufsize - tokenBegin + bufpos + 1 + inBuf;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     int i = 0, j = 0, k = 0;");
                printWriter.WriteLine("     int nextColDiff = 0, columnDiff = 0;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     while (i < len &&");
                printWriter.WriteLine("            bufline[j = start % bufsize] == bufline[k = ++start % bufsize])");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        bufline[j] = newLine;");
                printWriter.WriteLine("        nextColDiff = columnDiff + bufcolumn[k] - bufcolumn[j];");
                printWriter.WriteLine("        bufcolumn[j] = newCol + columnDiff;");
                printWriter.WriteLine("        columnDiff = nextColDiff;");
                printWriter.WriteLine("        i++;");
                printWriter.WriteLine("     } ");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (i < len)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        bufline[j] = newLine++;");
                printWriter.WriteLine("        bufcolumn[j] = newCol + columnDiff;");
                printWriter.WriteLine("");
                printWriter.WriteLine("        while (i++ < len)");
                printWriter.WriteLine("        {");
                printWriter.WriteLine("           if (bufline[j = start % bufsize] != bufline[++start % bufsize])");
                printWriter.WriteLine("              bufline[j] = newLine++;");
                printWriter.WriteLine("           else");
                printWriter.WriteLine("              bufline[j] = newLine;");
                printWriter.WriteLine("        }");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     line = bufline[j];");
                printWriter.WriteLine("     column = bufcolumn[j];");
                printWriter.WriteLine("  }");
                printWriter.WriteLine("");
            }
            printWriter.WriteLine("}");
            outputFile.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create JavaCharStream ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file JavaCharStream.java for writing.");

        throw new System.Exception();
    }


    public static void gen_SimpleCharStream()
    {
        IOException ex;
        try
        {

            var f = new FileInfo(Path.Combine(Options.OutputDirectory.DirectoryName, "SimpleCharStream.java"));
            OutputFile outputFile = new OutputFile(f, "4.1", new string[1] { "STATIC" });
            if (!outputFile.needToWrite)
            {
                return;
            }
            var printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            string str = ((!Options.Static) ? "  " : "  static ");
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * An implementation of interface CharStream, where the stream is assumed to");
            printWriter.WriteLine(" * contain only ASCII characters (without unicode processing).");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("");
            printWriter.WriteLine("public class SimpleCharStream");
            printWriter.WriteLine("{");
            printWriter.WriteLine("/** Whether parser is static. */");
            printWriter.WriteLine(("  public static final boolean staticFlag = ") + (Options.Static) + (";")
                );
            printWriter.WriteLine((str) + ("int bufsize;"));
            printWriter.WriteLine((str) + ("int available;"));
            printWriter.WriteLine((str) + ("int tokenBegin;"));
            printWriter.WriteLine("/** Position in buffer. */");
            printWriter.WriteLine((str) + ("public int bufpos = -1;"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine((str) + ("protected int bufline[];"));
                printWriter.WriteLine((str) + ("protected int bufcolumn[];"));
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected int column = 0;"));
                printWriter.WriteLine((str) + ("protected int line = 1;"));
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected boolean prevCharIsCR = false;"));
                printWriter.WriteLine((str) + ("protected boolean prevCharIsLF = false;"));
            }
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected java.io.TextReader inputStream;"));
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected char[] buffer;"));
            printWriter.WriteLine((str) + ("protected int maxNextCharInd = 0;"));
            printWriter.WriteLine((str) + ("protected int inBuf = 0;"));
            printWriter.WriteLine((str) + ("protected int tabSize = 8;"));
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void setTabSize(int i) { tabSize = i; }"));
            printWriter.WriteLine((str) + ("protected int getTabSize(int i) { return tabSize; }"));
            printWriter.WriteLine("");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void ExpandBuff(boolean wrapAround)"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     char[] newbuffer = new char[bufsize + 2048];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     int newbufline[] = new int[bufsize + 2048];");
                printWriter.WriteLine("     int newbufcolumn[] = new int[bufsize + 2048];");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("     try");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        if (wrapAround)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           System.arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);");
            printWriter.WriteLine("           System.arraycopy(buffer, 0, newbuffer,");
            printWriter.WriteLine("                                             bufsize - tokenBegin, bufpos);");
            printWriter.WriteLine("           buffer = newbuffer;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           System.arraycopy(bufline, 0, newbufline, bufsize - tokenBegin, bufpos);");
                printWriter.WriteLine("           bufline = newbufline;");
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, 0, newbufcolumn, bufsize - tokenBegin, bufpos);");
                printWriter.WriteLine("           bufcolumn = newbufcolumn;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("           maxNextCharInd = (bufpos += (bufsize - tokenBegin));");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           System.arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);");
            printWriter.WriteLine("           buffer = newbuffer;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           bufline = newbufline;");
                printWriter.WriteLine("");
                printWriter.WriteLine("           System.arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);");
                printWriter.WriteLine("           bufcolumn = newbufcolumn;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("           maxNextCharInd = (bufpos -= tokenBegin);");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     catch (Throwable t)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        throw new System.Exception(t.getMessage());");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("");
            printWriter.WriteLine("     bufsize += 2048;");
            printWriter.WriteLine("     available = bufsize;");
            printWriter.WriteLine("     tokenBegin = 0;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("protected void FillBuff() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (maxNextCharInd == available)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        if (available == bufsize)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           if (tokenBegin > 2048)");
            printWriter.WriteLine("           {");
            printWriter.WriteLine("              bufpos = maxNextCharInd = 0;");
            printWriter.WriteLine("              available = tokenBegin;");
            printWriter.WriteLine("           }");
            printWriter.WriteLine("           else if (tokenBegin < 0)");
            printWriter.WriteLine("              bufpos = maxNextCharInd = 0;");
            printWriter.WriteLine("           else");
            printWriter.WriteLine("              ExpandBuff(false);");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else if (available > tokenBegin)");
            printWriter.WriteLine("           available = bufsize;");
            printWriter.WriteLine("        else if ((tokenBegin - available) < 2048)");
            printWriter.WriteLine("           ExpandBuff(true);");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("           available = tokenBegin;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     int i;");
            printWriter.WriteLine("     try {");
            printWriter.WriteLine("        if ((i = inputStream.read(buffer, maxNextCharInd,");
            printWriter.WriteLine("                                    available - maxNextCharInd)) == -1)");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           inputStream.Close();");
            printWriter.WriteLine("           throw new java.io.IOException();");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("        else");
            printWriter.WriteLine("           maxNextCharInd += i;");
            printWriter.WriteLine("        return;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("     catch(java.io.IOException e) {");
            printWriter.WriteLine("        --bufpos;");
            printWriter.WriteLine("        backup(0);");
            printWriter.WriteLine("        if (tokenBegin == -1)");
            printWriter.WriteLine("           tokenBegin = bufpos;");
            printWriter.WriteLine("        throw e;");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Start. */");
            printWriter.WriteLine((str) + ("public char BeginToken() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     tokenBegin = -1;");
            printWriter.WriteLine("     char c = readChar();");
            printWriter.WriteLine("     tokenBegin = bufpos;");
            printWriter.WriteLine("");
            printWriter.WriteLine("     return c;");
            printWriter.WriteLine("  }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine((str) + ("protected void UpdateLineColumn(char c)"));
                printWriter.WriteLine("  {");
                printWriter.WriteLine("     column++;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (prevCharIsLF)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        prevCharIsLF = false;");
                printWriter.WriteLine("        line += (column = 1);");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("     else if (prevCharIsCR)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        prevCharIsCR = false;");
                printWriter.WriteLine("        if (c == '\\n')");
                printWriter.WriteLine("        {");
                printWriter.WriteLine("           prevCharIsLF = true;");
                printWriter.WriteLine("        }");
                printWriter.WriteLine("        else");
                printWriter.WriteLine("           line += (column = 1);");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     switch (c)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        case '\\r' :");
                printWriter.WriteLine("           prevCharIsCR = true;");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        case '\\n' :");
                printWriter.WriteLine("           prevCharIsLF = true;");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        case '\\t' :");
                printWriter.WriteLine("           column--;");
                printWriter.WriteLine("           column += (tabSize - (column % tabSize));");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("        default :");
                printWriter.WriteLine("           break;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     bufline[bufpos] = line;");
                printWriter.WriteLine("     bufcolumn[bufpos] = column;");
                printWriter.WriteLine("  }");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Read a character. */");
            printWriter.WriteLine((str) + ("public char readChar() throws java.io.IOException"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (inBuf > 0)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        --inBuf;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        if (++bufpos == bufsize)");
            printWriter.WriteLine("           bufpos = 0;");
            printWriter.WriteLine("");
            printWriter.WriteLine("        return buffer[bufpos];");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     if (++bufpos >= maxNextCharInd)");
            printWriter.WriteLine("        FillBuff();");
            printWriter.WriteLine("");
            printWriter.WriteLine("     char c = buffer[bufpos];");
            printWriter.WriteLine("");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     UpdateLineColumn(c);");
            }
            printWriter.WriteLine("     return c;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndColumn");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("public int getColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndLine");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("");
            printWriter.WriteLine((str) + ("public int getLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Get token end column number. */");
            printWriter.WriteLine((str) + ("public int getEndColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Get token end line number. */");
            printWriter.WriteLine((str) + ("public int getEndLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[bufpos];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Get token beginning column number. */");
            printWriter.WriteLine((str) + ("public int getBeginColumn() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufcolumn[tokenBegin];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Get token beginning line number. */");
            printWriter.WriteLine((str) + ("public int getBeginLine() {"));
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     return bufline[tokenBegin];");
            }
            else
            {
                printWriter.WriteLine("     return -1;");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("/** Backup a number of characters. */");
            printWriter.WriteLine((str) + ("public void backup(int amount) {"));
            printWriter.WriteLine("");
            printWriter.WriteLine("    inBuf += amount;");
            printWriter.WriteLine("    if ((bufpos -= amount) < 0)");
            printWriter.WriteLine("       bufpos += bufsize;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.TextReader dstream, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            if (Options.Static)
            {
                printWriter.WriteLine("    if (inputStream != null)");
                printWriter.WriteLine("       throw new System.Exception(\"\\n   ERROR: Second call to the constructor of a static SimpleCharStream.\\n\" +");
                printWriter.WriteLine("       \"       You must either use ReInit() or set the JavaCC option STATIC to false\\n\" +");
                printWriter.WriteLine("       \"       during the generation of this class.\");");
            }
            printWriter.WriteLine("    inputStream = dstream;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    line = startline;");
                printWriter.WriteLine("    column = startcolumn - 1;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("    available = bufsize = buffersize;");
            printWriter.WriteLine("    buffer = new char[buffersize];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    bufline = new int[buffersize];");
                printWriter.WriteLine("    bufcolumn = new int[buffersize];");
            }
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.TextReader dstream, int startline,");
            printWriter.WriteLine("                          int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.TextReader dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("    inputStream = dstream;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    line = startline;");
                printWriter.WriteLine("    column = startcolumn - 1;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("    if (buffer == null || buffersize != buffer.length)");
            printWriter.WriteLine("    {");
            printWriter.WriteLine("      available = bufsize = buffersize;");
            printWriter.WriteLine("      buffer = new char[buffersize];");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("      bufline = new int[buffersize];");
                printWriter.WriteLine("      bufcolumn = new int[buffersize];");
            }
            printWriter.WriteLine("    }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    prevCharIsLF = prevCharIsCR = false;");
            }
            printWriter.WriteLine("    tokenBegin = inBuf = maxNextCharInd = 0;");
            printWriter.WriteLine("    bufpos = -1;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream, int startline,");
            printWriter.WriteLine("                     int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.TextReader dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(encoding == null ? new java.io.StreamReader(dstream) : new java.io.StreamReader(dstream, encoding), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("  int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(new java.io.StreamReader(dstream), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("                          int startcolumn) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, encoding, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("                          int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream, String encoding) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, encoding, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor. */");
            printWriter.WriteLine("  public SimpleCharStream(java.io.Stream dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("                          int startcolumn, int buffersize) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(encoding == null ? new java.io.StreamReader(dstream) : new java.io.StreamReader(dstream, encoding), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("                          int startcolumn, int buffersize)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(new java.io.StreamReader(dstream), startline, startcolumn, buffersize);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, encoding, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, 1, 1, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, String encoding, int startline,");
            printWriter.WriteLine("                     int startcolumn) throws java.io.UnsupportedEncodingException");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, encoding, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("  /** Reinitialise. */");
            printWriter.WriteLine("  public void ReInit(java.io.Stream dstream, int startline,");
            printWriter.WriteLine("                     int startcolumn)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     ReInit(dstream, startline, startcolumn, 4096);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("  /** Get token literal value. */");
            printWriter.WriteLine((str) + ("public String GetImage()"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     if (bufpos >= tokenBegin)");
            printWriter.WriteLine("        return new String(buffer, tokenBegin, bufpos - tokenBegin + 1);");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("        return new String(buffer, tokenBegin, bufsize - tokenBegin) +");
            printWriter.WriteLine("                              new String(buffer, 0, bufpos + 1);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Get the suffix. */");
            printWriter.WriteLine((str) + ("public char[] GetSuffix(int len)"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     char[] ret = new char[len];");
            printWriter.WriteLine("");
            printWriter.WriteLine("     if ((bufpos + 1) >= len)");
            printWriter.WriteLine("        System.arraycopy(buffer, bufpos - len + 1, ret, 0, len);");
            printWriter.WriteLine("     else");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("        System.arraycopy(buffer, bufsize - (len - bufpos - 1), ret, 0,");
            printWriter.WriteLine("                                                          len - bufpos - 1);");
            printWriter.WriteLine("        System.arraycopy(buffer, 0, ret, len - bufpos - 1, bufpos + 1);");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("");
            printWriter.WriteLine("     return ret;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Reset buffer when finished. */");
            printWriter.WriteLine((str) + ("public void Done()"));
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     buffer = null;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("     bufline = null;");
                printWriter.WriteLine("     bufcolumn = null;");
            }
            printWriter.WriteLine("  }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("  /**");
                printWriter.WriteLine("   * Method to adjust line and column numbers for the start of a token.");
                printWriter.WriteLine("   */");
                printWriter.WriteLine((str) + ("public void adjustBeginLineColumn(int newLine, int newCol)"));
                printWriter.WriteLine("  {");
                printWriter.WriteLine("     int start = tokenBegin;");
                printWriter.WriteLine("     int len;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (bufpos >= tokenBegin)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        len = bufpos - tokenBegin + inBuf + 1;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("     else");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        len = bufsize - tokenBegin + bufpos + 1 + inBuf;");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     int i = 0, j = 0, k = 0;");
                printWriter.WriteLine("     int nextColDiff = 0, columnDiff = 0;");
                printWriter.WriteLine("");
                printWriter.WriteLine("     while (i < len &&");
                printWriter.WriteLine("            bufline[j = start % bufsize] == bufline[k = ++start % bufsize])");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        bufline[j] = newLine;");
                printWriter.WriteLine("        nextColDiff = columnDiff + bufcolumn[k] - bufcolumn[j];");
                printWriter.WriteLine("        bufcolumn[j] = newCol + columnDiff;");
                printWriter.WriteLine("        columnDiff = nextColDiff;");
                printWriter.WriteLine("        i++;");
                printWriter.WriteLine("     } ");
                printWriter.WriteLine("");
                printWriter.WriteLine("     if (i < len)");
                printWriter.WriteLine("     {");
                printWriter.WriteLine("        bufline[j] = newLine++;");
                printWriter.WriteLine("        bufcolumn[j] = newCol + columnDiff;");
                printWriter.WriteLine("");
                printWriter.WriteLine("        while (i++ < len)");
                printWriter.WriteLine("        {");
                printWriter.WriteLine("           if (bufline[j = start % bufsize] != bufline[++start % bufsize])");
                printWriter.WriteLine("              bufline[j] = newLine++;");
                printWriter.WriteLine("           else");
                printWriter.WriteLine("              bufline[j] = newLine;");
                printWriter.WriteLine("        }");
                printWriter.WriteLine("     }");
                printWriter.WriteLine("");
                printWriter.WriteLine("     line = bufline[j];");
                printWriter.WriteLine("     column = bufcolumn[j];");
                printWriter.WriteLine("  }");
                printWriter.WriteLine("");
            }
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create SimpleCharStream ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file SimpleCharStream.java for writing.");

        throw new System.Exception();
    }


    public static void gen_CharStream()
    {
        IOException ex;
        try
        {

            FileInfo f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "CharStream.java"));

            OutputFile outputFile = new OutputFile(f, "4.1", new string[1] { "STATIC" });
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * This interface describes a character stream that maintains line and");
            printWriter.WriteLine(" * column number positions of the characters.  It also has the capability");
            printWriter.WriteLine(" * to backup the stream to some extent.  An implementation of this");
            printWriter.WriteLine(" * interface is used in the TokenManager implementation generated by");
            printWriter.WriteLine(" * JavaCCParser.");
            printWriter.WriteLine(" *");
            printWriter.WriteLine(" * All the methods except backup can be implemented in any fashion. backup");
            printWriter.WriteLine(" * needs to be implemented correctly for the correct operation of the lexer.");
            printWriter.WriteLine(" * Rest of the methods are all used to get information like line number,");
            printWriter.WriteLine(" * column number and the String that constitutes a token and are not used");
            printWriter.WriteLine(" * by the lexer. Hence their implementation won't affect the generated lexer's");
            printWriter.WriteLine(" * operation.");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("");
            printWriter.WriteLine("public interface CharStream {");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the next character from the selected input.  The method");
            printWriter.WriteLine("   * of selecting the input is the responsibility of the class");
            printWriter.WriteLine("   * implementing this interface.  Can throw any java.io.IOException.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  char readChar() throws java.io.IOException;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the column position of the character last read.");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndColumn");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getColumn();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the line number of the character last read.");
            printWriter.WriteLine("   * @deprecated ");
            printWriter.WriteLine("   * @see #getEndLine");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getLine();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the column number of the last character for current token (being");
            printWriter.WriteLine("   * matched after the last call to BeginTOken).");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getEndColumn();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the line number of the last character for current token (being");
            printWriter.WriteLine("   * matched after the last call to BeginTOken).");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getEndLine();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the column number of the first character for current token (being");
            printWriter.WriteLine("   * matched after the last call to BeginTOken).");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getBeginColumn();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the line number of the first character for current token (being");
            printWriter.WriteLine("   * matched after the last call to BeginTOken).");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  int getBeginLine();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Backs up the input stream by amount steps. Lexer calls this method if it");
            printWriter.WriteLine("   * had already read some characters, but could not use them to match a");
            printWriter.WriteLine("   * (longer) token. So, they will be used again as the prefix of the next");
            printWriter.WriteLine("   * token and it is the implemetation's responsibility to do this right.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  void backup(int amount);");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the next character that marks the beginning of the next token.");
            printWriter.WriteLine("   * All characters must remain in the buffer between two successive calls");
            printWriter.WriteLine("   * to this method to implement backup correctly.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  char BeginToken() throws java.io.IOException;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns a string made up of characters from the marked token beginning ");
            printWriter.WriteLine("   * to the current buffer position. Implementations have the choice of returning");
            printWriter.WriteLine("   * anything that they want to. For example, for efficiency, one might decide");
            printWriter.WriteLine("   * to just return null, which is a valid implementation.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  String GetImage();");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns an array of characters that make up the suffix of length 'len' for");
            printWriter.WriteLine("   * the currently matched token. This is used to build up the matched string");
            printWriter.WriteLine("   * for use in actions in the case of MORE. A simple and inefficient");
            printWriter.WriteLine("   * implementation of this is as follows :");
            printWriter.WriteLine("   *");
            printWriter.WriteLine("   *   {");
            printWriter.WriteLine("   *      String t = GetImage();");
            printWriter.WriteLine("   *      return t.substring(t.length() - len, t.length()).toCharArray();");
            printWriter.WriteLine("   *   }");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  char[] GetSuffix(int len);");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * The lexer calls this function to indicate that it is done with the stream");
            printWriter.WriteLine("   * and hence implementations can free any resources held by this class.");
            printWriter.WriteLine("   * Again, the body of this function can be just empty and it will not");
            printWriter.WriteLine("   * affect the lexer's operation.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  void Done();");
            printWriter.WriteLine("");
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create CharStream ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file CharStream.java for writing.");

        throw new System.Exception();
    }


    public static void gen_ParseException()
    {
        IOException ex;
        try
        {

            var f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "ParseException.java"));

            OutputFile outputFile = new OutputFile(f, "4.1", new string[1] { "KEEP_LINE_COL" });
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * This exception is thrown when parse errors are encountered.");
            printWriter.WriteLine(" * You can explicitly create objects of this exception type by");
            printWriter.WriteLine(" * calling the method generateParseException in the generated");
            printWriter.WriteLine(" * parser.");
            printWriter.WriteLine(" *");
            printWriter.WriteLine(" * You can modify this class to customize your error reporting");
            printWriter.WriteLine(" * mechanisms so long as you retain the public fields.");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("public class ParseException extends Exception {");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This constructor is used by the method \"generateParseException\"");
            printWriter.WriteLine("   * in the generated parser.  Calling this constructor generates");
            printWriter.WriteLine("   * a new object of this type with the fields \"currentToken\",");
            printWriter.WriteLine("   * \"expectedTokenSequences\", and \"tokenImage\" set.  The boolean");
            printWriter.WriteLine("   * flag \"specialConstructor\" is also set to true to indicate that");
            printWriter.WriteLine("   * this constructor was used to create this object.");
            printWriter.WriteLine("   * This constructor calls its super class with the empty string");
            printWriter.WriteLine("   * to force the \"ToString\" method of parent class \"Throwable\" to");
            printWriter.WriteLine("   * Write the error message in the form:");
            printWriter.WriteLine("   *     ParseException: <result of getMessage>");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public ParseException(Token currentTokenVal,");
            printWriter.WriteLine("                        int[][] expectedTokenSequencesVal,");
            printWriter.WriteLine("                        String[] tokenImageVal");
            printWriter.WriteLine("                       )");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("    super(\"\");");
            printWriter.WriteLine("    specialConstructor = true;");
            printWriter.WriteLine("    currentToken = currentTokenVal;");
            printWriter.WriteLine("    expectedTokenSequences = expectedTokenSequencesVal;");
            printWriter.WriteLine("    tokenImage = tokenImageVal;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * The following constructors are for use by you for whatever");
            printWriter.WriteLine("   * purpose you can think of.  Constructing the exception in this");
            printWriter.WriteLine("   * manner makes the exception behave in the normal way - i.e., as");
            printWriter.WriteLine("   * documented in the class \"Throwable\".  The fields \"errorToken\",");
            printWriter.WriteLine("   * \"expectedTokenSequences\", and \"tokenImage\" do not contain");
            printWriter.WriteLine("   * relevant information.  The JavaCC generated code does not use");
            printWriter.WriteLine("   * these constructors.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("");
            printWriter.WriteLine("  public ParseException() {");
            printWriter.WriteLine("    super();");
            printWriter.WriteLine("    specialConstructor = false;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** Constructor with message. */");
            printWriter.WriteLine("  public ParseException(String message) {");
            printWriter.WriteLine("    super(message);");
            printWriter.WriteLine("    specialConstructor = false;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This variable determines which constructor was used to create");
            printWriter.WriteLine("   * this object and thereby affects the semantics of the");
            printWriter.WriteLine("   * \"getMessage\" method (see below).");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  protected boolean specialConstructor;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This is the last token that has been consumed successfully.  If");
            printWriter.WriteLine("   * this object has been created due to a parse error, the token");
            printWriter.WriteLine("   * followng this token will (therefore) be the first error token.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token currentToken;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Each entry in this array is an array of integers.  Each array");
            printWriter.WriteLine("   * of integers represents a sequence of tokens (by their ordinal");
            printWriter.WriteLine("   * values) that is expected at this point of the parse.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public int[][] expectedTokenSequences;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This is a reference to the \"tokenImage\" array of the generated");
            printWriter.WriteLine("   * parser within which the parse error occurred.  This array is");
            printWriter.WriteLine("   * defined in the generated ...Constants interface.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public String[] tokenImage;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This method has the standard behavior when this object has been");
            printWriter.WriteLine("   * created using the standard constructors.  Otherwise, it uses");
            printWriter.WriteLine("   * \"currentToken\" and \"expectedTokenSequences\" to generate a parse");
            printWriter.WriteLine("   * error message and returns it.  If this object has been created");
            printWriter.WriteLine("   * due to a parse error, and you do not catch it (it gets thrown");
            printWriter.WriteLine("   * from the parser), then this method is called during the printing");
            printWriter.WriteLine("   * of the final stack trace, and hence the correct error message");
            printWriter.WriteLine("   * gets displayed.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public String getMessage() {");
            printWriter.WriteLine("    if (!specialConstructor) {");
            printWriter.WriteLine("      return super.getMessage();");
            printWriter.WriteLine("    }");
            printWriter.WriteLine("    StringBuilder expected = new StringBuilder();");
            printWriter.WriteLine("    int maxSize = 0;");
            printWriter.WriteLine("    for (int i = 0; i < expectedTokenSequences.length; i++) {");
            printWriter.WriteLine("      if (maxSize < expectedTokenSequences[i].length) {");
            printWriter.WriteLine("        maxSize = expectedTokenSequences[i].length;");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      for (int j = 0; j < expectedTokenSequences[i].length; j++) {");
            printWriter.WriteLine("        expected+(tokenImage[expectedTokenSequences[i][j]])+(' ');");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      if (expectedTokenSequences[i][expectedTokenSequences[i].length - 1] != 0) {");
            printWriter.WriteLine("        expected+(\"...\");");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      expected+(eol)+(\"    \");");
            printWriter.WriteLine("    }");
            printWriter.WriteLine("    String retval = \"Encountered \\\"\";");
            printWriter.WriteLine("    Token tok = currentToken.next;");
            printWriter.WriteLine("    for (int i = 0; i < maxSize; i++) {");
            printWriter.WriteLine("      if (i != 0) retval += \" \";");
            printWriter.WriteLine("      if (tok.kind == 0) {");
            printWriter.WriteLine("        retval += tokenImage[0];");
            printWriter.WriteLine("        break;");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      retval += \" \" + tokenImage[tok.kind];");
            printWriter.WriteLine("      retval += \" \\\"\";");
            printWriter.WriteLine("      retval += add_escapes(tok.image);");
            printWriter.WriteLine("      retval += \" \\\"\";");
            printWriter.WriteLine("      tok = tok.next; ");
            printWriter.WriteLine("    }");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("    retval += \"\\\" at line \" + currentToken.next.beginLine + \", column \" + currentToken.next.beginColumn;");
            }
            printWriter.WriteLine("    retval += \".\" + eol;");
            printWriter.WriteLine("    if (expectedTokenSequences.length == 1) {");
            printWriter.WriteLine("      retval += \"Was expecting:\" + eol + \"    \";");
            printWriter.WriteLine("    } else {");
            printWriter.WriteLine("      retval += \"Was expecting one of:\" + eol + \"    \";");
            printWriter.WriteLine("    }");
            printWriter.WriteLine("    retval += expected;");
            printWriter.WriteLine("    return retval;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * The end of line string for this machine.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  protected String eol = System.getProperty(\"line.separator\", \"\\n\");");
            printWriter.WriteLine(" ");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Used to convert raw characters to their escaped version");
            printWriter.WriteLine("   * when these raw version cannot be used as part of an ASCII");
            printWriter.WriteLine("   * string literal.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  protected String add_escapes(String str) {");
            printWriter.WriteLine("      StringBuilder retval = new StringBuilder();");
            printWriter.WriteLine("      char ch;");
            printWriter.WriteLine("      for (int i = 0; i < str.length(); i++) {");
            printWriter.WriteLine("        switch (str.charAt(i))");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           case 0 :");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\b':");
            printWriter.WriteLine("              retval.Append(\"\\\\b\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\t':");
            printWriter.WriteLine("              retval.Append(\"\\\\t\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\n':");
            printWriter.WriteLine("              retval.Append(\"\\\\n\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\f':");
            printWriter.WriteLine("              retval.Append(\"\\\\f\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\r':");
            printWriter.WriteLine("              retval.Append(\"\\\\r\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\\"':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\\"\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\'':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\'\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\\\':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\\\\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           default:");
            printWriter.WriteLine("              if ((ch = str.charAt(i)) < 0x20 || ch > 0x7e) {");
            printWriter.WriteLine("                 String s = \"0000\" + int.ToString(ch, 16);");
            printWriter.WriteLine("                 retval.Append(\"\\\\u\" + s.substring(s.length() - 4, s.length()));");
            printWriter.WriteLine("              } else {");
            printWriter.WriteLine("                 retval.Append(ch);");
            printWriter.WriteLine("              }");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      return retval;");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create ParseException ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file ParseException.java for writing.");

        throw new System.Exception();
    }


    public static void gen_TokenMgrError()
    {
        IOException ex;
        try
        {

            FileInfo f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "TokenMgrError.java"));
            OutputFile outputFile = new OutputFile(f, "4.1", new string[0]);
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            printWriter.WriteLine("/** Token Manager Error. */");
            if (string.Compare(Options.JdkVersion, "1.5") >= 0)
            {
                printWriter.WriteLine("@SuppressWarnings(\"serial\")");
            }
            printWriter.WriteLine("public class TokenMgrError extends Error");
            printWriter.WriteLine("{");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /*");
            printWriter.WriteLine("    * Ordinals for various reasons why an Error of this type can be thrown.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Lexical error occurred.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   static final int LEXICAL_ERROR = 0;");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * An attempt was made to create a second instance of a static token manager.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   static final int STATIC_LEXER_ERROR = 1;");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Tried to change to an invalid lexical state.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   static final int INVALID_LEXICAL_STATE = 2;");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Detected (and bailed out of) an infinite loop in the token manager.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   static final int LOOP_DETECTED = 3;");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Indicates the reason why the exception is thrown. It will have");
            printWriter.WriteLine("    * one of the above 4 values.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   int errorCode;");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Replaces unprintable characters by their escaped (or unicode escaped)");
            printWriter.WriteLine("    * equivalents in the given string");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   protected static final String addEscapes(String str) {");
            printWriter.WriteLine("      StringBuilder retval = new StringBuilder();");
            printWriter.WriteLine("      char ch;");
            printWriter.WriteLine("      for (int i = 0; i < str.length(); i++) {");
            printWriter.WriteLine("        switch (str.charAt(i))");
            printWriter.WriteLine("        {");
            printWriter.WriteLine("           case 0 :");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\b':");
            printWriter.WriteLine("              retval.Append(\"\\\\b\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\t':");
            printWriter.WriteLine("              retval.Append(\"\\\\t\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\n':");
            printWriter.WriteLine("              retval.Append(\"\\\\n\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\f':");
            printWriter.WriteLine("              retval.Append(\"\\\\f\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\r':");
            printWriter.WriteLine("              retval.Append(\"\\\\r\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\\"':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\\"\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\'':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\'\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           case '\\\\':");
            printWriter.WriteLine("              retval.Append(\"\\\\\\\\\");");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("           default:");
            printWriter.WriteLine("              if ((ch = str.charAt(i)) < 0x20 || ch > 0x7e) {");
            printWriter.WriteLine("                 String s = \"0000\" + int.ToString(ch, 16);");
            printWriter.WriteLine("                 retval.Append(\"\\\\u\" + s.substring(s.length() - 4, s.length()));");
            printWriter.WriteLine("              } else {");
            printWriter.WriteLine("                 retval.Append(ch);");
            printWriter.WriteLine("              }");
            printWriter.WriteLine("              continue;");
            printWriter.WriteLine("        }");
            printWriter.WriteLine("      }");
            printWriter.WriteLine("      return retval;");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * Returns a detailed message for the Error when it is thrown by the");
            printWriter.WriteLine("    * token manager to indicate a lexical error.");
            printWriter.WriteLine("    * Parameters : ");
            printWriter.WriteLine("    *    EOFSeen     : indicates if EOF caused the lexical error");
            printWriter.WriteLine("    *    curLexState : lexical state in which this error occurred");
            printWriter.WriteLine("    *    errorLine   : line number when the error occurred");
            printWriter.WriteLine("    *    errorColumn : column number when the error occurred");
            printWriter.WriteLine("    *    errorAfter  : prefix that was seen before this error occurred");
            printWriter.WriteLine("    *    curchar     : the offending character");
            printWriter.WriteLine("    * Note: You can customize the lexical error message by modifying this method.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   protected static String LexicalError(boolean EOFSeen, int lexState, int errorLine, int errorColumn, String errorAfter, char curChar) {");
            printWriter.WriteLine("      return(\"Lexical error at line \" +");
            printWriter.WriteLine("           errorLine + \", column \" +");
            printWriter.WriteLine("           errorColumn + \".  Encountered: \" +");
            printWriter.WriteLine("           (EOFSeen ? \"<EOF> \" : (\"\\\"\" + addEscapes(String.valueOf(curChar)) + \"\\\"\") + \" (\" + (int)curChar + \"), \") +");
            printWriter.WriteLine("           \"after : \\\"\" + addEscapes(errorAfter) + \"\\\"\");");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /**");
            printWriter.WriteLine("    * You can also modify the body of this method to customize your error messages.");
            printWriter.WriteLine("    * For example, cases like LOOP_DETECTED and INVALID_LEXICAL_STATE are not");
            printWriter.WriteLine("    * of end-users concern, so you can return something like : ");
            printWriter.WriteLine("    *");
            printWriter.WriteLine("    *     \"Internal Error : Please file a bug report .... \"");
            printWriter.WriteLine("    *");
            printWriter.WriteLine("    * from this method for such cases in the release version of your parser.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("   public String getMessage() {");
            printWriter.WriteLine("      return super.getMessage();");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /*");
            printWriter.WriteLine("    * Constructors of various flavors follow.");
            printWriter.WriteLine("    */");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /** No arg constructor. */");
            printWriter.WriteLine("   public TokenMgrError() {");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /** Constructor with message and reason. */");
            printWriter.WriteLine("   public TokenMgrError(String message, int reason) {");
            printWriter.WriteLine("      super(message);");
            printWriter.WriteLine("      errorCode = reason;");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("");
            printWriter.WriteLine("   /** Full Constructor. */");
            printWriter.WriteLine("   public TokenMgrError(boolean EOFSeen, int lexState, int errorLine, int errorColumn, String errorAfter, char curChar, int reason) {");
            printWriter.WriteLine("      this(LexicalError(EOFSeen, lexState, errorLine, errorColumn, errorAfter, curChar), reason);");
            printWriter.WriteLine("   }");
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create TokenMgrError ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file TokenMgrError.java for writing.");

        throw new System.Exception();
    }


    public static void gen_Token()
    {
        IOException ex;
        try
        {

            FileInfo f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "Token.java"));

            OutputFile outputFile = new OutputFile(f, "4.1", new string[2] { "TOKEN_EXTENDS", "KEEP_LINE_COL" });
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * Describes the input token stream.");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("");
            if (string.Equals(Options.TokenExtends, ""))
            {
                printWriter.WriteLine("public class Token {");
            }
            else
            {
                printWriter.WriteLine(("public class Token extends ") + (Options.TokenExtends) + (" {")
                    );
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * An integer that describes the kind of this token.  This numbering");
            printWriter.WriteLine("   * system is determined by JavaCCParser, and a table of these numbers is");
            printWriter.WriteLine("   * stored in the file ...Constants.java.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public int kind;");
            if (OtherFilesGen.KeepLineCol)
            {
                printWriter.WriteLine("");
                printWriter.WriteLine("  /** The line number of the first character of this Token. */");
                printWriter.WriteLine("  public int beginLine;");
                printWriter.WriteLine("  /** The column number of the first character of this Token. */");
                printWriter.WriteLine("  public int beginColumn;");
                printWriter.WriteLine("  /** The line number of the last character of this Token. */");
                printWriter.WriteLine("  public int endLine;");
                printWriter.WriteLine("  /** The column number of the last character of this Token. */");
                printWriter.WriteLine("  public int endColumn;");
            }
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * The string image of the token.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public String image;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * A reference to the next regular (non-special) token from the input");
            printWriter.WriteLine("   * stream.  If this is the last token from the input stream, or if the");
            printWriter.WriteLine("   * token manager has not read tokens beyond this one, this field is");
            printWriter.WriteLine("   * set to null.  This is true only if this token is also a regular");
            printWriter.WriteLine("   * token.  Otherwise, see below for a description of the contents of");
            printWriter.WriteLine("   * this field.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token next;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * This field is used to access special tokens that occur prior to this");
            printWriter.WriteLine("   * token, but after the immediately preceding regular (non-special) token.");
            printWriter.WriteLine("   * If there are no such special tokens, this field is set to null.");
            printWriter.WriteLine("   * When there are more than one such special token, this field refers");
            printWriter.WriteLine("   * to the last of these special tokens, which in turn refers to the next");
            printWriter.WriteLine("   * previous special token through its specialToken field, and so on");
            printWriter.WriteLine("   * until the first special token (whose specialToken field is null).");
            printWriter.WriteLine("   * The next fields of special tokens refer to other special tokens that");
            printWriter.WriteLine("   * immediately follow it (without an intervening regular token).  If there");
            printWriter.WriteLine("   * is no such token, this field is null.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token specialToken;");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * An optional attribute value of the Token.");
            printWriter.WriteLine("   * Tokens which are not used as syntactic sugar will often contain");
            printWriter.WriteLine("   * meaningful values that will be used later on by the compiler or");
            printWriter.WriteLine("   * interpreter. This attribute value is often different from the image.");
            printWriter.WriteLine("   * Any subclass of Token that actually wants to return a non-null value can");
            printWriter.WriteLine("   * override this method as appropriate.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Object getValue() {");
            printWriter.WriteLine("    return null;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * No-argument contructor");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token() {}");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Constructs a new token for the specified Image.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token(int kind)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this(kind, null);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Constructs a new token for the specified Image and Kind.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token(int kind, String image)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     this.kind = kind;");
            printWriter.WriteLine("     this.image = image;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns the image.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public String ToString()");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     return image;");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /**");
            printWriter.WriteLine("   * Returns a new Token object, by default. However, if you want, you");
            printWriter.WriteLine("   * can create and return subclass objects based on the value of ofKind.");
            printWriter.WriteLine("   * Simply Add the cases to the switch for all those special cases.");
            printWriter.WriteLine("   * For example, if you have a subclass of Token called IDToken that");
            printWriter.WriteLine("   * you want to create if ofKind is ID, simply Add something like :");
            printWriter.WriteLine("   *");
            printWriter.WriteLine("   *    case MyParserConstants.ID : return new IDToken(ofKind, image);");
            printWriter.WriteLine("   *");
            printWriter.WriteLine("   * to the following switch statement. Then you can cast matchedToken");
            printWriter.WriteLine("   * variable to the appropriate type and use sit in your lexical actions.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public static Token newToken(int ofKind, String image)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     switch(ofKind)");
            printWriter.WriteLine("     {");
            printWriter.WriteLine("       default : return new Token(ofKind, image);");
            printWriter.WriteLine("     }");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("  public static Token newToken(int ofKind)");
            printWriter.WriteLine("  {");
            printWriter.WriteLine("     return newToken(ofKind, null);");
            printWriter.WriteLine("  }");
            printWriter.WriteLine("");
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create Token ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file Token.java for writing.");

        throw new System.Exception();
    }


    public static void gen_TokenManager()
    {
        IOException ex;
        try
        {

            FileInfo f = new FileInfo(Path.Combine(Options.OutputDirectory.FullName, "TokenManager.java"));

            OutputFile outputFile = new OutputFile(f, "4.1", new string[0]);
            if (!outputFile.needToWrite)
            {
                return;
            }
            TextWriter printWriter = outputFile.PrintWriter;
            if (JavaCCGlobals.CuToInsertionPoint1.Count != 0 && ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).Kind == 60)
            {
                for (int i = 1; i < JavaCCGlobals.CuToInsertionPoint1.Count; i++)
                {
                    if (((Token)JavaCCGlobals.CuToInsertionPoint1[i]).Kind == 97)
                    {
                        JavaCCGlobals.cline = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginLine;
                        JavaCCGlobals.ccol = ((Token)JavaCCGlobals.CuToInsertionPoint1[0]).BeginColumn;
                        for (int j = 0; j <= i; j++)
                        {
                            JavaCCGlobals.PrintToken((Token)JavaCCGlobals.CuToInsertionPoint1[j], printWriter);
                        }
                        printWriter.WriteLine("");
                        printWriter.WriteLine("");
                        break;
                    }
                }
            }
            printWriter.WriteLine("/**");
            printWriter.WriteLine(" * An implementation for this interface is generated by");
            printWriter.WriteLine(" * JavaCCParser.  The user is free to use any implementation");
            printWriter.WriteLine(" * of their choice.");
            printWriter.WriteLine(" */");
            printWriter.WriteLine("");
            printWriter.WriteLine("public interface TokenManager {");
            printWriter.WriteLine("");
            printWriter.WriteLine("  /** This gets the next token from the input stream.");
            printWriter.WriteLine("   *  A token of kind 0 (<EOF>) should be returned on EOF.");
            printWriter.WriteLine("   */");
            printWriter.WriteLine("  public Token getNextToken();");
            printWriter.WriteLine("");
            printWriter.WriteLine("}");
            printWriter.Close();
            return;
        }
        catch (IOException x)
        {
            ex = x;
        }
        IOException obj = ex;
        Console.Error.WriteLine(("Failed to create TokenManager ") + (obj));
        JavaCCErrors.Semantic_Error("Could not open file TokenManager.java for writing.");

        throw new System.Exception();
    }

    public new static void ReInit()
    {
    }


    static JavaFiles()
    {

    }
}
