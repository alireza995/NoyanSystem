using Basic.Exceptions;

namespace Application.Customers.Exceptions;

public class CustomerNotFound : DataNotFound
{
    private readonly int _requestId;

    public CustomerNotFound(int requestId)
    {
        _requestId = requestId;
    }

    public override string LogPhrase => $"مشتری با کد {_requestId} یافت نشد.";
    public override string Message => "مشتری یافت نشد.";
    public override int EventId => 102;
}