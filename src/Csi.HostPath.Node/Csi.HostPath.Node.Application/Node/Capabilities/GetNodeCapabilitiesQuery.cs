using MediatR;

namespace Csi.HostPath.Node.Application.Node.Capabilities;

public enum NodeCapabilities
{
    StageUnstageVolume,
    VolumeCondition,
    GetVolumeStats,
    SingleNodeMultiWriter,
    ExpandVolume
}

public record GetNodeCapabilitiesQuery : IRequest<List<NodeCapabilities>>;

public class GetNodeCapabilitiesQueryHandler : IRequestHandler<GetNodeCapabilitiesQuery, List<NodeCapabilities>>
{
    public Task<List<NodeCapabilities>> Handle(GetNodeCapabilitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<NodeCapabilities>
        {
            NodeCapabilities.StageUnstageVolume,
            NodeCapabilities.VolumeCondition,
            NodeCapabilities.GetVolumeStats,
            NodeCapabilities.SingleNodeMultiWriter,
            NodeCapabilities.ExpandVolume
        });
    }
}