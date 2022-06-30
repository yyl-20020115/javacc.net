using System.Runtime.Serialization;
using System.Security.Permissions;

namespace org.javacc.parser;
public class MetaParseException : ParseException
{	
	public MetaParseException()
	{
	}

		protected MetaParseException(SerializationInfo P_0, StreamingContext P_1)
		: base(P_0, P_1)
	{
	}
}
