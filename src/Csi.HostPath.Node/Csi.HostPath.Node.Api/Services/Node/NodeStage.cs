using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService
{
    public override Task<NodeStageVolumeResponse> NodeStageVolume(NodeStageVolumeRequest request, ServerCallContext context)
    {
        // ensure volume id is not empty
        // ensure target path is not empty
        // ensure capability is not empty
        // volume should exist
        // volume should be published
        // ensure we do not state the same volume to the same path twice
        // we shouldn't be able to stage volume to two different folders
        
        // this may be used to generate folder path
        var volumeId = request.VolumeId;
        var stageDir = request.StagingTargetPath;
        
        // format and mount to the node to the staging dir
        // in my case it means that we need to create directory and using bind mount mount it to the target directory
        return Task.FromResult(new NodeStageVolumeResponse());
    }

    public override Task<NodeUnstageVolumeResponse> NodeUnstageVolume(NodeUnstageVolumeRequest request, ServerCallContext context)
    {
        // ensure volume id is not empty
        // ensure target path is not empty
        // volume should exist
        // volume should be published
        // it should be staged at the same path
        
        // this may be used to generate folder path
        var volumeId = request.VolumeId;
        var stageDir = request.StagingTargetPath;
        
        // format and mount to the node to the staging dir
        // opposite to stage (but without remove original directory)
        // need to think how to work with snapshots and remount of volume to the same host
        return Task.FromResult(new NodeUnstageVolumeResponse());
    }
}