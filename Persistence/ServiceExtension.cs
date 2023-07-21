using Basic.Context;
using Basic.Repository;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence;

public static class ServiceExtension
{
    public static void AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<DataContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("Default"))
        );

        services.AddScoped<IDataContext, DataContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGenericRepositoryHandler, GenericRepositoryHandler>();
    }
}