using Csi.HostPath.Controller.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Controller.Application;

public static class Extensions
{
    public static IServiceCollection RegisterApplication(
        this IServiceCollection services)
    {
        var currentAssembly = typeof(Extensions).Assembly;

        services
            .AddValidatorsFromAssembly(currentAssembly)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(currentAssembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

        return services;
    }
}