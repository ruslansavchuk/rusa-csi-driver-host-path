using MediatR;

namespace Csi.HostPath.Controller.Application.Identity;

public record PluginInfo(string Name, string Version);

public record GetPluginInfoQuery : IRequest<PluginInfo>;

public class GetPluginInfoQueryHandler : IRequestHandler<GetPluginInfoQuery, PluginInfo>
{
    public Task<PluginInfo> Handle(GetPluginInfoQuery request, CancellationToken cancellationToken)
    {
        const string name = "hostpath.csi.k8s.io";
        var version = Environment.GetEnvironmentVariable("VERSION");
        
        return Task.FromResult(new PluginInfo(name, version));
    }
}