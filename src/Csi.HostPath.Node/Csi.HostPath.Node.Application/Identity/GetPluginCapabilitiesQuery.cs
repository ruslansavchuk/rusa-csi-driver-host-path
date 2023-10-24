using MediatR;

namespace Csi.HostPath.Node.Application.Identity;

public enum PluginCapability
{
}

public record GetPluginCapabilitiesQuery : IRequest<List<PluginCapability>>;

public class GetPluginCapabilitiesQueryHandler : IRequestHandler<GetPluginCapabilitiesQuery, List<PluginCapability>>
{
    public Task<List<PluginCapability>> Handle(GetPluginCapabilitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<PluginCapability>());
    }
}