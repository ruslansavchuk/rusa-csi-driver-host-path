using MediatR;

namespace Csi.HostPath.Controller.Application.Capabilities;

public enum Capability
{
    CreateDeleteVolume = 1,
    PublishUnpublishVolume = 2,
    ListVolumes = 3,
    GetCapacity = 4
}

public record GetCapabilities : IRequest<List<Capability>>;

public class GetCapabilitiesRequestHandler : IRequestHandler<GetCapabilities, List<Capability>>
{
    public Task<List<Capability>> Handle(GetCapabilities request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<Capability>
        {
            Capability.CreateDeleteVolume,
            Capability.PublishUnpublishVolume,
            Capability.ListVolumes,
            Capability.GetCapacity
        });
    }
}