using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace Api.ServiceManagement;

public static class PipelineBehaviour
{
    public static void AddPipelineBehaviour(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}