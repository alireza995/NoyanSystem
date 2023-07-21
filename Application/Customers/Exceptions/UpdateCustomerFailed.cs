using Basic.Exceptions;

namespace Application.Customers.Exceptions;

public class UpdateCustomerFailed : UpdateDataFailure
{
    private readonly int _customerId;

    public UpdateCustomerFailed(int customerId)
    {
        _customerId = customerId;
    }

    public override string LogPhrase => $"ویرایش مشتری با کد {_customerId} با خطا مواجه شد.";
    public override string Message => "ویرایش مشتری با خطا مواجه شد.";
    public override int EventId => 104;
}