using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record UnpublishVolumeCommand(
    string VolumeId, 
    string TargetPath) : IRequest;

public class UnpublishVolumeCommandHandler : IRequestHandler<UnpublishVolumeCommand>
{
    private readonly IMounter _mounter;
    private readonly EphemeralVolumeProvisioner _ephemeralVolumeProvisioner;

    public UnpublishVolumeCommandHandler(
        IMounter mounter, 
        EphemeralVolumeProvisioner ephemeralVolumeProvisioner)
    {
        _mounter = mounter;
        _ephemeralVolumeProvisioner = ephemeralVolumeProvisioner;
    }

    public async Task Handle(UnpublishVolumeCommand request, CancellationToken cancellationToken)
    {
        _mounter.Unmount(request.TargetPath);
        
        // as we can't define from the request if volume is ephemeral, we need to try to un-stage ephemeral volume
        await _ephemeralVolumeProvisioner.TryUnStage(request.VolumeId, cancellationToken);
    }
}