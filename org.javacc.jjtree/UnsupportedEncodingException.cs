using System;

namespace org.javacc.jjtree
{
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
    }
}