using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Stage;

public record UnStageVolumeCommand(string StagePath) : IRequest;

public class UnpublishVolumeCommandHandler : IRequestHandler<UnStageVolumeCommand>
{
    private readonly IMounter _mounter;

    public UnpublishVolumeCommandHandler(IMounter mounter)
    {
        _mounter = mounter;
    }

    public Task Handle(UnStageVolumeCommand request, CancellationToken cancellationToken)
    {
        _mounter.Unmount(request.StagePath);
        
        return Task.CompletedTask;
    }
}