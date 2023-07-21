using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class UpdateDataFailure : DataBaseException
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
}