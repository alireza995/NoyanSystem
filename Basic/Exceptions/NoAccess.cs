using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class NoAccess : BaseException
{
    public override int StatusCode => StatusCodes.Status403Forbidden;
}