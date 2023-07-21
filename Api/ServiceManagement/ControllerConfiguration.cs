using System.Text.Json.Serialization;

namespace Api.ServiceManagement;

public static class ControllerConfiguration
{
    public static void AddControllerConfiguration(this IServiceCollection services)
    {
        services.AddControllers().AddJsonEnumConverter();
    }

    private static void AddJsonEnumConverter(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddJsonOptions(
            options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );
    }
}