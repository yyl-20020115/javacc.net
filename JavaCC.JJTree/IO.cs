namespace JavaCC.JJTree;
using System;
using System.IO;
using JavaCC.Parser;

public class IO : IDisposable
{
    private string ifn = "<uninitialized input>";
    private string ofn = "";
    private TextReader reader;
    private TextWriter writer;
    private TextWriter message = Console.Out;
    private TextWriter error = Console.Error;
    public virtual void Write(string text) => writer.Write(text);
    public virtual void WriteLine() => writer.WriteLine();
    public virtual void WriteLine(string text) => writer.WriteLine(text);
    public string CreateOutputFileName(string _text)
    {
        var text = JJTreeOptions.OutputFile;
        if (string.Equals(text, ""))
        {
            int i = (_text.LastIndexOf(Path.DirectorySeparatorChar));
            if (i >= 0)
            {
                _text = _text.Substring(i + 1);
            }
            int p = (_text.LastIndexOf('.'));
            if (p == -1)
            {
                text += ".jj";
            }
            else
            {
                string s = _text.Substring(p);
                text = (!string.Equals(s, ".jj"))
                    ? (_text.Substring(0, p) + (".jj"))
                    : _text + ".jj";
            }
        }
        return text;
    }
    public IO()
    {
    }

    public virtual TextWriter Writer => writer;
    public virtual string OutputFileName => ofn;
    public virtual string InputFileName => ifn;
    public virtual TextReader In => reader;
    public virtual TextWriter Msg => message;
    public virtual TextWriter Err => error;
    public virtual void CloseAll()
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
    public virtual void SetInput(string path)
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
                        throw new JJTreeIOException(("File ") + (path) + (" not found."));
                    }
                    if (new DirectoryInfo(path).Exists)
                    {
                        throw new JJTreeIOException((path) + (" is a directory. Please use a valid file name."));
                    }
                    if (JavaCCGlobals.IsGeneratedBy("JJTree", path))
                    {
                        throw new JJTreeIOException((path) + (" was generated by jjtree.  Cannot run jjtree again."));
                    }
                    ifn = file.FullName;
                    reader = new StreamReader(ifn);
                    return;
                }
                catch (System.Exception x)
                {
                    throw new JJTreeIOException(x.Message);
                }
            }
            catch (FileNotFoundException)
            {
                throw new JJTreeIOException("File " + path + " not found.");
            }
        }
        catch (IOException x2)
        {
            throw new JJTreeIOException(x2.Message);
        }
    }


    public virtual void SetOutput()
    {
        try
        {
            JavaCCGlobals.CreateOutputDir(JJTreeOptions.JJTreeOutputDirectory);
            //
            var file = new FileInfo(
                Path.Combine(JJTreeOptions.JJTreeOutputDirectory.FullName,
                    CreateOutputFileName(ifn)));
            writer = new StreamWriter(ofn = file.FullName);
            return;
        }
        catch (IOException)
        {
            throw new JJTreeIOException("Can't create output file " + ofn);
        }
    }

    public void Dispose()
    {
        this.CloseAll();

    }
}
