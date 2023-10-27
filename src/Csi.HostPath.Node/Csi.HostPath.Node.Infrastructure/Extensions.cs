using System.Runtime.InteropServices;
using Csi.HostPath.Node.Application.Node.Common;
using Csi.HostPath.Node.Infrastructure.Mounter.Linux;
using Csi.HostPath.Node.Infrastructure.Mounter.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Node.Infrastructure;

public static class Extensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDirectoryManager, DirectoriesManager>();
        
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