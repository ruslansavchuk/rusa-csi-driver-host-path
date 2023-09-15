using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Identity;

public class IdentityService : Csi.V1.Identity.IdentityBase
{
    public override Task<ProbeResponse> Probe(ProbeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ProbeResponse());
    }

    public override Task<GetPluginInfoResponse> GetPluginInfo(GetPluginInfoRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetPluginInfoResponse
        {
            Name = "name",
            VendorVersion = "version"
        });
    }

    public override Task<GetPluginCapabilitiesResponse> GetPluginCapabilities(GetPluginCapabilitiesRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetPluginCapabilitiesResponse());
    }
}