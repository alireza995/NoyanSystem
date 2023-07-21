using Basic.Exceptions;

namespace Application.Customers.Exceptions;

public class AddCustomerFailure : AddDataFailure
{
    public override string LogPhrase => "ثبت کاربر با خطا مواجه شد.";
    public override string Message => "ثبت کاربر با خطا مواجه شد.";
    public override int EventId => 101;
}