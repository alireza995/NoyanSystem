using Basic.Exceptions;

namespace Application.Customers.Exceptions;

public class DeleteCustomerFailed : DeleteDataFailure
{
    private readonly int _customerId;

    public DeleteCustomerFailed(int customerId)
    {
        _customerId = customerId;
    }

    public override string LogPhrase => $"حدف مشتری با کد {_customerId} با خطا مواجه شد.";
    public override string Message => "حذف مشتری با خطا مواجه شد.";
    public override int EventId => 103;
}