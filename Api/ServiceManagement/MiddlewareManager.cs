namespace Api.ServiceManagement;

public static class MiddlewareManager
{
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<ErrorHandlingMiddleware>();
    }
}