using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services;

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
            Name = string.Empty,
            VendorVersion = string.Empty
        });
    }

    public override Task<GetPluginCapabilitiesResponse> GetPluginCapabilities(GetPluginCapabilitiesRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetPluginCapabilitiesResponse());
    }
}