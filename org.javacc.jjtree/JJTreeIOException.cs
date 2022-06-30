using System.Runtime.Serialization;
using System.IO;

namespace org.javacc.jjtree;



internal class JJTreeIOException : IOException
{

	internal JJTreeIOException(string P_0)
		: base(P_0)
	{
	}


	protected JJTreeIOException(SerializationInfo P_0, StreamingContext P_1)
	: base(P_0, P_1)
	{
	}
}