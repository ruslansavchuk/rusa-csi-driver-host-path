using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Capabilities;

public enum ControllerCapability
{
    CreateDeleteVolume = 1,
    PublishUnpublishVolume = 2,
    ListVolumes = 3,
    GetCapacity = 4,
    ExpandVolume = 9
}

public record GetControllerCapabilitiesQuery : IRequest<List<ControllerCapability>>;

public class GetControllerCapabilitiesQueryHandler : IRequestHandler<GetControllerCapabilitiesQuery, List<ControllerCapability>>
{
    public Task<List<ControllerCapability>> Handle(GetControllerCapabilitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<ControllerCapability>
        {
            ControllerCapability.CreateDeleteVolume,
            ControllerCapability.PublishUnpublishVolume,
            ControllerCapability.ListVolumes,
            ControllerCapability.GetCapacity,
            ControllerCapability.ExpandVolume
        });
    }
}