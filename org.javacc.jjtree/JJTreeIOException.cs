using System.IO;
namespace org.javacc.jjtree;

internal class JJTreeIOException : IOException
{
	internal JJTreeIOException(string text) : base(text) { }
}
