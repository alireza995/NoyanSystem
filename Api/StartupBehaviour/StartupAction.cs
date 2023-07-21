using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.StartupBehaviour;

public static class StartupAction
{
    private static IServiceScope? _serviceScope;

    public static async Task RunStartup(this WebApplication application)
    {
        await using var dbContext = GetService<DataContext>(application);
        await dbContext.Database.MigrateAsync();
    }

    private static T GetService<T>(IHost application)
        where T : class
    {
        return GetServiceScope(application).ServiceProvider.GetRequiredService<T>();
    }

    private static IServiceScope GetServiceScope(IHost application)
    {
        _serviceScope ??= application.Services.GetService<IServiceScopeFactory>()!.CreateScope();

        return _serviceScope;
    }
}