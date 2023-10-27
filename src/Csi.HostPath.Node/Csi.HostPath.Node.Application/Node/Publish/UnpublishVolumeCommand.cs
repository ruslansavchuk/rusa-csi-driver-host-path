using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record UnpublishVolumeCommand(int VolumeId, string TargetPath) : IRequest;

public class UnpublishVolumeCommandHandler : IRequestHandler<UnpublishVolumeCommand>
{
    public Task Handle(UnpublishVolumeCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}