using System.IO;
namespace Javacc.JJTree;

internal class JJTreeIOException : IOException
{
	internal JJTreeIOException(string text) : base(text) { }
}
