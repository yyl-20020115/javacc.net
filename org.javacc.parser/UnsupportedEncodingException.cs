using System;
using System.Runtime.Serialization;

namespace org.javacc.parser
{
    [Serializable]
    internal class UnsupportedEncodingException : Exception
    {
        public UnsupportedEncodingException()
        {
        }

        public UnsupportedEncodingException(string message) : base(message)
        {
        }

        public UnsupportedEncodingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedEncodingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}