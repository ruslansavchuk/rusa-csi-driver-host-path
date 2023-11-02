using Csi.HostPath.Node.Application.Node.Publish;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override async Task<NodePublishVolumeResponse> NodePublishVolume(NodePublishVolumeRequest request, ServerCallContext context)
    {
        var command = new PublishVolumeCommand(
            ToVolumeId(request.VolumeId), 
            request.TargetPath, 
            request.StagingTargetPath,
            request.Readonly, 
            request.VolumeContext
                .ToDictionary(
                    i => i.Key, 
                    i => i.Value));
        
        await _mediator.Send(command, context.CancellationToken);
        return new NodePublishVolumeResponse();
    }

    public override async Task<NodeUnpublishVolumeResponse> NodeUnpublishVolume(NodeUnpublishVolumeRequest request, ServerCallContext context)
    {
        var command = new UnpublishVolumeCommand(ToVolumeId(request.VolumeId), request.TargetPath);
        await _mediator.Send(command, context.CancellationToken);
        return new NodeUnpublishVolumeResponse();
    }
}