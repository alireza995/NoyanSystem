using Application.Customers;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application;

public static class ServiceExtension
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddValidatorsFromAssemblyContaining<Add.Command>(includeInternalTypes: true);
        services.AddMediatR(i => i.RegisterServicesFromAssembly(typeof(Add.Command).Assembly));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddPersistenceServices(configuration);
    }
}