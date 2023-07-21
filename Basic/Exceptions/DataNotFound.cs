using Microsoft.AspNetCore.Http;

namespace Basic.Exceptions;

public abstract class DataNotFound : DataBaseException
{
    public override int StatusCode => StatusCodes.Status404NotFound;
}