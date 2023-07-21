using Api;
using Api.ServiceManagement;
using Api.StartupBehaviour;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureLogging();
builder.Services.ManageServices(builder.Configuration);
var app = builder.Build();
await app.RunStartup();
app.RunApp();