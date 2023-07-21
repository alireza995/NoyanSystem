using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class InvalidData : BaseException
{
    public override int StatusCode => StatusCodes.Status406NotAcceptable;
}