using Csi.HostPath.Node.Application.Node.Stage;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override async Task<NodeStageVolumeResponse> NodeStageVolume(NodeStageVolumeRequest request, ServerCallContext context)
    {
        var command = new StageVolumeCommand(
            request.VolumeId, 
            request.StagingTargetPath,
            ToDictionary(request.VolumeContext));

        await _mediator.Send(command);
        
        return new NodeStageVolumeResponse();
    }

    public override async Task<NodeUnstageVolumeResponse> NodeUnstageVolume(NodeUnstageVolumeRequest request, ServerCallContext context)
    {
        var command = new UnStageVolumeCommand(request.StagingTargetPath);
        await _mediator.Send(command);
        
        return new NodeUnstageVolumeResponse();
    }
}