using Csi.HostPath.Controller.Api;
using Csi.HostPath.Controller.Api.Configuration;
using Csi.HostPath.Controller.Application;
using Csi.HostPath.Controller.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((ctx, lc) =>
    {
        lc.ReadFrom.Configuration(ctx.Configuration);
    });

builder.Services
    .Configure<ConfigurationOptions>(builder.Configuration.GetSection(nameof(ConfigurationOptions)));

var ops = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<ConfigurationOptions>>();

builder.WebHost.ConfigureKestrel(options =>
{
    if (!string.IsNullOrWhiteSpace(ops.Value.UnixSocket))
    {
        options.ListenUnixSocket(ops.Value.UnixSocket);
    }

    options.ListenAnyIP(ops.Value.ListeningPort ?? 80);
});

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(ops.Value.DbPath)
    .RegisterServices();

var app = builder.Build();

app.MapServices();
app.Run();