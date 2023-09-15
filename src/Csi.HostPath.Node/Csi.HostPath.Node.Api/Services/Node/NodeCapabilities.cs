using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService
{
    public override Task<NodeGetCapabilitiesResponse> NodeGetCapabilities(NodeGetCapabilitiesRequest request, ServerCallContext context)
    {
        var response = new NodeGetCapabilitiesResponse();

        response = AddCapabilities(response,
            NodeServiceCapability.Types.RPC.Types.Type.StageUnstageVolume,
            NodeServiceCapability.Types.RPC.Types.Type.VolumeCondition,
            NodeServiceCapability.Types.RPC.Types.Type.GetVolumeStats,
            NodeServiceCapability.Types.RPC.Types.Type.SingleNodeMultiWriter,
            NodeServiceCapability.Types.RPC.Types.Type.ExpandVolume);
        
        return Task.FromResult(response);
    }

    private NodeGetCapabilitiesResponse AddCapabilities(
        NodeGetCapabilitiesResponse response,
        params NodeServiceCapability.Types.RPC.Types.Type[] capabilities)
    {
        foreach (var capability in capabilities)
        {
            response.Capabilities.Add(new NodeServiceCapability
            {
                Rpc = new NodeServiceCapability.Types.RPC
                {
                    Type = capability
                }
            });
        }

        return response;
    }
}