using System.Reflection;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Infrastructure.Context;
using Csi.HostPath.Controller.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.HostPath.Controller.Infrastructure;

public static class Extensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, string stateDir)
    {
        services.AddDbContext<DataContext>(options => options
            .UseSqlite($"Filename={Path.Join(stateDir, "csi-state.db")}"));
        
        services.AddScoped<IVolumeRepository, VolumeRepository>();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(Extensions)));
        
        return services;
    }
}