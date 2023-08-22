using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services;

public class Node : Csi.V1.Node.NodeBase
{
    #region Publish/Unpublish
    
    public override Task<NodePublishVolumeResponse> NodePublishVolume(NodePublishVolumeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodePublishVolumeResponse());
    }
    
    public override Task<NodeUnpublishVolumeResponse> NodeUnpublishVolume(NodeUnpublishVolumeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeUnpublishVolumeResponse());
    }
    
    #endregion

    #region Stage/Unstage
    
    public override Task<NodeStageVolumeResponse> NodeStageVolume(NodeStageVolumeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeStageVolumeResponse());
    }

    public override Task<NodeUnstageVolumeResponse> NodeUnstageVolume(NodeUnstageVolumeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeUnstageVolumeResponse());
    }
    
    #endregion
    
    #region Info

    public override Task<NodeGetInfoResponse> NodeGetInfo(NodeGetInfoRequest request, ServerCallContext context)
    {
        var response = new NodeGetInfoResponse
        {
            NodeId = "some value here",
            MaxVolumesPerNode = 1_000_000_000
        };
        
        return Task.FromResult(response);
    }

    #endregion
    
    #region Capabilities

    public override Task<NodeGetCapabilitiesResponse> NodeGetCapabilities(NodeGetCapabilitiesRequest request, ServerCallContext context)
    {
        var response = new NodeGetCapabilitiesResponse();
        response.Capabilities.Add(new NodeServiceCapability {Rpc = new NodeServiceCapability.Types.RPC
        {
            Type = NodeServiceCapability.Types.RPC.Types.Type.StageUnstageVolume
        }});
        response.Capabilities.Add(new NodeServiceCapability {Rpc = new NodeServiceCapability.Types.RPC
        {
            Type = NodeServiceCapability.Types.RPC.Types.Type.VolumeCondition
        }});
        response.Capabilities.Add(new NodeServiceCapability {Rpc = new NodeServiceCapability.Types.RPC
        {
            Type = NodeServiceCapability.Types.RPC.Types.Type.GetVolumeStats
        }});
        response.Capabilities.Add(new NodeServiceCapability {Rpc = new NodeServiceCapability.Types.RPC
        {
            Type = NodeServiceCapability.Types.RPC.Types.Type.SingleNodeMultiWriter
        }});
        response.Capabilities.Add(new NodeServiceCapability {Rpc = new NodeServiceCapability.Types.RPC
        {
            Type = NodeServiceCapability.Types.RPC.Types.Type.ExpandVolume
        }});

        return Task.FromResult(response);
    }

    #endregion
    
    #region Stats

    public override Task<NodeGetVolumeStatsResponse> NodeGetVolumeStats(NodeGetVolumeStatsRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeGetVolumeStatsResponse());
    }

    #endregion
    
    #region Expand

    public override Task<NodeExpandVolumeResponse> NodeExpandVolume(NodeExpandVolumeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeExpandVolumeResponse());
    }

    #endregion
}