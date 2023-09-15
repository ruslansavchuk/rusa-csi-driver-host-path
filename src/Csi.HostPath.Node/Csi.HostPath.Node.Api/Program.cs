using Csi.HostPath.Node.Api.Configuration;
using Csi.HostPath.Node.Api.Services;
using Csi.HostPath.Node.Api.Services.Identity;
using Csi.HostPath.Node.Api.Services.Node;
using Csi.HostPath.Node.Api.Utils;
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

builder.Services.AddGrpc();
        
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<NodeService>();
builder.Services.AddSingleton<Mounter>();

var app = builder.Build();

app.MapGrpcService<IdentityService>();
app.MapGrpcService<NodeService>();

app.Run();