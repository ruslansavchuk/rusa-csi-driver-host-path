using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override Task<NodePublishVolumeResponse> NodePublishVolume(NodePublishVolumeRequest request, ServerCallContext context)
    {
        // ensure volume id is not empty
        // ensure target path is not empty
        // ensure capability is not empty
        // cat have both volume capability
        
        // if volume ephemeral we need to create it before
        // if volume is not staged or stages at path different from request staged directory - return error
        
        // mount block volume
        // mount mount volume
        
        var isReadonly = request.Readonly;
        var targetPath = request.TargetPath;
        var volumeId = request.VolumeId;
        
        // need to create directory before unmounting

        // var mountDirectory = CreateDataDirectory(volumeId);
        EnsureTargetDirectoryExists(targetPath);
        // _mounter.Mount(mountDirectory, targetPath, new []{"--bind"});
        // 
        return Task.FromResult(new NodePublishVolumeResponse());
    }

    private void EnsureTargetDirectoryExists(string targetPath)
    {
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
    }

    // private string CreateDataDirectory(string volumeId)
    // {
    //     var directoryPath = Path.Combine(_options.Value.CsiDataDir, volumeId);
    //     var directoryInfo = Directory.CreateDirectory(directoryPath);
    //     return directoryInfo.FullName;
    // }

    public override Task<NodeUnpublishVolumeResponse> NodeUnpublishVolume(NodeUnpublishVolumeRequest request, ServerCallContext context)
    {
        // volume id should not be empty
        // target path should not be empty
        // volume should exist
        // volume should not be published at the same target path
        // ensure target path is mount point
        
        // in case if volume ephemeral - we need to remove it
        // if not ephemeral - we need to remove target directory from volume published paths
        
        // unmount 
        // _mounter.Unmount(request.TargetPath);
        
        // we do not remove data directory
        return Task.FromResult(new NodeUnpublishVolumeResponse());
    }
}