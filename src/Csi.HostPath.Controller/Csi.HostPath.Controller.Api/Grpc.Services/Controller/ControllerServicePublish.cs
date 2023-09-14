using Csi.HostPath.Controller.Application.Controller.Volumes.Commands;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override async Task<ControllerPublishVolumeResponse> ControllerPublishVolume(ControllerPublishVolumeRequest request, ServerCallContext context)
    {
        var command = ToCommand(request);
        var volume = await _mediator.Send(command, context.CancellationToken);
        return new ControllerPublishVolumeResponse();
    }
    
    private PublishVolumeCommand ToCommand(ControllerPublishVolumeRequest request)
    {
        var accessType = ToAccessType(request.VolumeCapability);
        return new PublishVolumeCommand(ToVolumeId(request.VolumeId), request.NodeId, accessType);
    }

    public override async Task<ControllerUnpublishVolumeResponse> ControllerUnpublishVolume(ControllerUnpublishVolumeRequest request, ServerCallContext context)
    {
        var command = new UnpublishVolumeCommand(ToVolumeId(request.VolumeId), request.NodeId);
        var volume = await _mediator.Send(command, context.CancellationToken);
        return new ControllerUnpublishVolumeResponse();
    }
}