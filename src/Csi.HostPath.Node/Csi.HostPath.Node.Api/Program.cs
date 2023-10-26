using Csi.HostPath.Node.Api;
using Csi.HostPath.Node.Application;
using Csi.HostPath.Node.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging();
builder.ConfigureWebHost();

builder.Services.Configure<string>(builder.Configuration.GetSection(""));

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure()
    .RegisterServices()
    .Configure();

var app = builder.Build();

app.MapServices();

app.Run();