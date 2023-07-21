using Application;

namespace Api.ServiceManagement;

public static class ModuleServiceCollection
{
    public static void AddModuleServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddApplicationServices(configuration);
    }
}