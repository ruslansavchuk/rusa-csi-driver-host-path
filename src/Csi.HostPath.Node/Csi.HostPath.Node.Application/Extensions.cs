using Csi.HostPath.Node.Application.Node.Publish;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Node.Application;

public static class Extensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<EphemeralVolumeProvisioner>();
        
        return services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
            });
    }
}