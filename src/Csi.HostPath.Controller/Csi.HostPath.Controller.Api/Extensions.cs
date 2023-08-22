using Csi.HostPath.Controller.Api.Grpc.Services.Interceptors;
using Identity = Csi.HostPath.Controller.Api.Grpc.Services.Identity;

namespace Csi.HostPath.Controller.Api;

public static class Extensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionInterceptor>();
        });
        
        services.AddScoped<Identity>();
        services.AddScoped<Grpc.Services.Controller>();

        return services;
    }

    public static void MapServices(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<Identity>();
        builder.MapGrpcService<Grpc.Services.Controller>();
    }
}