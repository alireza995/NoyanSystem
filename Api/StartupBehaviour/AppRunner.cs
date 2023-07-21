using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api.StartupBehaviour;

public static class AppRunner
{
    public static void RunApp(this WebApplication application)
    {
        application.UseMiddleware<ErrorHandlingMiddleware>();
        application.UseHttpsRedirection();
        application.MapControllers();

        application.UseSwagger();
        application.UseSwaggerUI(
            c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "NoyanSystem");
            }
        );

        application.Run();
    }
}