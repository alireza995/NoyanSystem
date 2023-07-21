using Microsoft.OpenApi.Models;

namespace Api.ServiceManagement;

public static class SwaggerServices
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(
                    "v2",
                    new OpenApiInfo
                    {
                        Title = "MainApis",
                        Version = "V1"
                    }
                );

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description =
                            "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    }
                );

                options.UseDateOnlyTimeOnlyStringConverters();
            }
        );
    }
}