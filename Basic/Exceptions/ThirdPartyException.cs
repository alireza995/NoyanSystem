using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class ThirdPartyException : BaseException
{
    public override int StatusCode => StatusCodes.Status503ServiceUnavailable;
}