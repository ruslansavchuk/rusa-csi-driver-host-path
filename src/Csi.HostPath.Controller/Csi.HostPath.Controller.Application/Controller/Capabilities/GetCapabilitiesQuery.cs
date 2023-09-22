using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Capabilities;

public enum Capability
{
    CreateDeleteVolume = 1,
    PublishUnpublishVolume = 2,
    ListVolumes = 3,
    GetCapacity = 4,
    ExpandVolume = 9
}

public record GetCapabilitiesQuery : IRequest<List<Capability>>;

public class GetCapabilitiesRequestHandler : IRequestHandler<GetCapabilitiesQuery, List<Capability>>
{
    public Task<List<Capability>> Handle(GetCapabilitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<Capability>
        {
            Capability.CreateDeleteVolume,
            Capability.PublishUnpublishVolume,
            Capability.ListVolumes,
            Capability.GetCapacity,
            Capability.ExpandVolume            
        });
    }
}