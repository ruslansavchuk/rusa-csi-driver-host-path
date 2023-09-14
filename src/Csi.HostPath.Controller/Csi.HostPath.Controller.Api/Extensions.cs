using Csi.HostPath.Controller.Api.Grpc.Services;
using Csi.HostPath.Controller.Api.Grpc.Services.Controller;
using Csi.HostPath.Controller.Api.Grpc.Services.Identity;
using Csi.HostPath.Controller.Api.Grpc.Services.Interceptors;

namespace Csi.HostPath.Controller.Api;

public static class Extensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ExceptionInterceptor>();
        });
        
        services.AddScoped<IdentityService>();
        services.AddScoped<ControllerService>();
    }

    public static void MapServices(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<IdentityService>();
        builder.MapGrpcService<ControllerService>();
    }
}