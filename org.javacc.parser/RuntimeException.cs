using System;
using System.Runtime.Serialization;

namespace org.javacc.parser
{
    [Serializable]
    internal class RuntimeException : Exception
    {
        private UnsupportedEncodingException cause;

        public RuntimeException()
        {
        }

        public RuntimeException(UnsupportedEncodingException cause)
        {
            this.cause = cause;
        }

        public RuntimeException(string message) : base(message)
        {
        }

        public RuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RuntimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}