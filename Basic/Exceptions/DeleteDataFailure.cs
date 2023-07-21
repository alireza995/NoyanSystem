using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class DeleteDataFailure : DataBaseException
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
}