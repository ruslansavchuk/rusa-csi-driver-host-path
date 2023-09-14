using Csi.HostPath.Controller.Application.Common.Dto;
using MediatR;

namespace Csi.HostPath.Controller.Application.Identity;

public record GetPluginCapabilitiesQuery : IRequest<List<PluginCapability>>;

public class GetPluginCapabilitiesQueryHandler : IRequestHandler<GetPluginCapabilitiesQuery, List<PluginCapability>>
{
    public Task<List<PluginCapability>> Handle(GetPluginCapabilitiesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<PluginCapability>
        {
            PluginCapability.ControllerService
        });
    }
}