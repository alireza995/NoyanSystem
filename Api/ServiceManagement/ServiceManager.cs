namespace Api.ServiceManagement;

public static class ServiceManager
{
    public static void ManageServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllerConfiguration();
        services.AddEndpointsApiExplorer();
        services.AddModuleServices(configuration);
        services.AddPipelineBehaviour();
        services.AddMiddlewares();
        services.AddSwagger();
    }
}