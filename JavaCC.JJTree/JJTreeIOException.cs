namespace JavaCC.JJTree;
using System.IO;

internal class JJTreeIOException : IOException
{
    internal JJTreeIOException(string text) : base(text) { }
}
