using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class NoDataFound : DataBaseException
{
    public override int StatusCode => StatusCodes.Status404NotFound;
}