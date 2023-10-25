using Csi.HostPath.Controller.Application.Controller.Capabilities;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override async Task<ControllerGetCapabilitiesResponse> ControllerGetCapabilities(ControllerGetCapabilitiesRequest request, ServerCallContext context)
    {
        var capabilities = await _sender.Send(new GetCapabilitiesQuery());
        return ToResponse(capabilities);
    }

    private ControllerGetCapabilitiesResponse ToResponse(List<Capability> capabilities)
    {
        var response = new ControllerGetCapabilitiesResponse();
        
        response.Capabilities.AddRange(capabilities.Select(capability => new ControllerServiceCapability
        {
            Rpc = new ControllerServiceCapability.Types.RPC
            {
                Type = (ControllerServiceCapability.Types.RPC.Types.Type)capability
            }
        }));
        
        return response;
    }
}