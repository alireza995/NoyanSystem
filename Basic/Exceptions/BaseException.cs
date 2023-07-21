namespace Basic.Exceptions;

public abstract class BaseException : Exception
{
    public abstract string LogPhrase { get; }
    public abstract override string Message { get; }
    public abstract int EventId { get; }
    public abstract int StatusCode { get; }
}