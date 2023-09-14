using Csi.HostPath.Node.Api.Configuration;
using Csi.HostPath.Node.Api.Services;
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
        
builder.Services.AddScoped<Identity>();
builder.Services.AddScoped<Node>();

var app = builder.Build();

app.MapGrpcService<Identity>();
app.MapGrpcService<Node>();

app.Run();