using System.IO;
namespace JavaCC.JJTree;

internal class JJTreeIOException : IOException
{
	internal JJTreeIOException(string text) : base(text) { }
}
