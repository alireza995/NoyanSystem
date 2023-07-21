namespace Api;

public static class ApplicationBuilder
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"))
           .AddFile("Logs/NoyanSystem-{Date}.txt")
           .AddConsole()
           .AddDebug()
           .AddEventSourceLogger();
    }
}