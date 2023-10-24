﻿using Csi.HostPath.Node.Api.Configuration;
using Csi.HostPath.Node.Api.Grpc.Interceptors;
using Csi.HostPath.Node.Api.Grpc.Services.Identity;
using Microsoft.Extensions.Options;
using Serilog;
using NodeService = Csi.HostPath.Node.Api.Grpc.Services.Node.NodeService;

namespace Csi.HostPath.Node.Api;

public static class Extensions
{
    public static void ConfigureLogging(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration);
        });
    }

    public static void ConfigureWebHost(this WebApplicationBuilder builder)
    {
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
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<LoggingInterceptor>();
            options.Interceptors.Add<ExceptionInterceptor>();
        });
        
        services.AddScoped<IdentityService>();
        services.AddScoped<NodeService>();
    }

    public static void MapServices(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<IdentityService>();
        builder.MapGrpcService<NodeService>();
    }
}