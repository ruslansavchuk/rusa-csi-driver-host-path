using Csi.HostPath.Controller.Api;
using Csi.HostPath.Controller.Api.Configuration;
using Csi.HostPath.Controller.Application;
using Csi.HostPath.Controller.Infrastructure;
using Csi.HostPath.Controller.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<ConfigurationOptions>(builder.Configuration.GetSection(nameof(ConfigurationOptions)));

var ops = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<ConfigurationOptions>>();

if (!string.IsNullOrWhiteSpace(ops.Value.UnixSocket))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenUnixSocket(ops.Value.UnixSocket);
    });    
}

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(ops.Value.DbPath)
    .RegisterServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

app.MapServices();
app.Run();