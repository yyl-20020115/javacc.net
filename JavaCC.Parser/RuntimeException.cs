namespace JavaCC.Parser;
using System;

public class RuntimeException : Exception
{
    public UnsupportedEncodingException Cause;

    public RuntimeException()
    {
    }

    public RuntimeException(UnsupportedEncodingException cause)
    {
        this.Cause = cause;
    }

    public RuntimeException(string message) : base(message)
    {
    }

    public RuntimeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
