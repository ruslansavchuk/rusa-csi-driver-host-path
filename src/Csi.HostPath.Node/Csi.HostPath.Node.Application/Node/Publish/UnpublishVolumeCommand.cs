using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record UnpublishVolumeCommand(int VolumeId, string TargetPath) : IRequest;

public class UnpublishVolumeCommandHandler : IRequestHandler<UnpublishVolumeCommand>
{
    private readonly IMounter _mounter;

    public UnpublishVolumeCommandHandler(IMounter mounter)
    {
        _mounter = mounter;
    }

    public Task Handle(UnpublishVolumeCommand request, CancellationToken cancellationToken)
    {
        _mounter.Unmount(request.TargetPath);
        return Task.CompletedTask;
    }
}