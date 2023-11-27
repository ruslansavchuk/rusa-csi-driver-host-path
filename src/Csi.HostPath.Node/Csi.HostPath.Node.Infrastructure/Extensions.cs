using System.Runtime.InteropServices;
using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Common.Controller;
using Csi.HostPath.Node.Application.Node.Common;
using Csi.HostPath.Node.Infrastructure.Controller;
using Csi.HostPath.Node.Infrastructure.Mounter.Linux;
using Csi.HostPath.Node.Infrastructure.Mounter.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Node.Infrastructure;

public static class Extensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDirectoryManager, DirectoriesManager>();
        services.AddGrpcClient<Csi.V1.Controller.ControllerClient>((serviceProvider, options) =>
        {
            var nodeConfig = serviceProvider.GetRequiredService<INodeConfiguration>();
            
            Console.WriteLine(nodeConfig.ControllerEndpoint);
            options.Address = new Uri(nodeConfig.ControllerEndpoint);
        });

        services.AddScoped<IVolumeController, VolumeController>();
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            services.AddSingleton<IMounter, LinuxMounter>();
        }
        else
        {
            services.AddSingleton<IMounter, WindowsMounter>();
        }

        return services;
    }
}