using Csi.HostPath.Node.Application.Node.Capabilities;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override async Task<NodeGetCapabilitiesResponse> NodeGetCapabilities(NodeGetCapabilitiesRequest request, ServerCallContext context)
    {
        var capabilities = await _mediator.Send(new GetNodeCapabilitiesQuery(), context.CancellationToken);
        return ToResponse(capabilities);
    }

    private static NodeGetCapabilitiesResponse ToResponse(List<NodeCapabilities> capabilities)
    {
        var response = new NodeGetCapabilitiesResponse();
     
        foreach (var capability in capabilities)
        {
            response.Capabilities.Add(new NodeServiceCapability
            {
                Rpc = new NodeServiceCapability.Types.RPC
                {
                    Type = ToType(capability)
                }
            });
        }
        
        return response;
    }

    private static Csi.V1.NodeServiceCapability.Types.RPC.Types.Type ToType(NodeCapabilities capability) =>
        capability switch
        {
             NodeCapabilities.ExpandVolume => NodeServiceCapability.Types.RPC.Types.Type.ExpandVolume,
             NodeCapabilities.VolumeCondition => NodeServiceCapability.Types.RPC.Types.Type.VolumeCondition,
             NodeCapabilities.GetVolumeStats => NodeServiceCapability.Types.RPC.Types.Type.GetVolumeStats,
             NodeCapabilities.StageUnstageVolume => NodeServiceCapability.Types.RPC.Types.Type.StageUnstageVolume,
             NodeCapabilities.SingleNodeMultiWriter => NodeServiceCapability.Types.RPC.Types.Type.SingleNodeMultiWriter,
             _ => throw new ArgumentOutOfRangeException(nameof(capability), capability, "unknown capability")
        };
}