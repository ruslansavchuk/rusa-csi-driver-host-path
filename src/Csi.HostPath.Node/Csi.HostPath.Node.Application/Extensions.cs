using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Node.Application;

public static class Extensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        var currentAssembly = typeof(Extensions).Assembly;

        services
            // do i really need fluent validation here????
            // .AddValidatorsFromAssembly(currentAssembly)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(currentAssembly);
                // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

        return services;
    }
}