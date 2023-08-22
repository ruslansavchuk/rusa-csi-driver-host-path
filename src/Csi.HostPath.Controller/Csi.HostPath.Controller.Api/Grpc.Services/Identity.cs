using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services;

public class Identity : Csi.V1.Identity.IdentityBase
{
    public override Task<ProbeResponse> Probe(ProbeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ProbeResponse());
    }

    public override Task<GetPluginInfoResponse> GetPluginInfo(GetPluginInfoRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetPluginInfoResponse
        {
            Name = "rusa",
            VendorVersion = "123"
        });
    }

    public override Task<GetPluginCapabilitiesResponse> GetPluginCapabilities(GetPluginCapabilitiesRequest request, ServerCallContext context)
    {
        var response = new GetPluginCapabilitiesResponse();
        
        response.Capabilities.Add(new PluginCapability
        {
            Service = new PluginCapability.Types.Service
            {
                Type = PluginCapability.Types.Service.Types.Type.ControllerService
            }
        });
        
        return Task.FromResult(response);
    }
}
