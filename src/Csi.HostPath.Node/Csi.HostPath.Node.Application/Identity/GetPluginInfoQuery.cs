using MediatR;

namespace Csi.HostPath.Node.Application.Identity;

public record PluginInfo(string Name, string Version);

public record GetPluginInfoQuery : IRequest<PluginInfo>;

public class GetPluginInfoQueryHandler : IRequestHandler<GetPluginInfoQuery, PluginInfo>
{
    public Task<PluginInfo> Handle(GetPluginInfoQuery request, CancellationToken cancellationToken)
    {
        var currentAssemblyVersion = typeof(GetPluginInfoQuery).Assembly.GetName().Version!.ToString();
        const string driverName = "hostpath.csi.k8s.io";
        return Task.FromResult(new PluginInfo(driverName, currentAssemblyVersion));
    }
}