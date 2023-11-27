using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record PublishVolumeCommand(
    string VolumeId, 
    string TargetPath, 
    string? StagePath, 
    bool ReadOnly, 
    Dictionary<string, string> Context) : IRequest;

public class PublishVolumeCommandHandler : IRequestHandler<PublishVolumeCommand>
{
    private readonly IMounter _mounter;
    private readonly IDirectoryManager _directoryManager;
    private readonly EphemeralVolumeProvisioner _ephemeralVolumeProvisioner;

    public PublishVolumeCommandHandler(
        IMounter mounter,
        IDirectoryManager directoryManager,
        EphemeralVolumeProvisioner ephemeralVolumeProvisioner)
    {
        _mounter = mounter;
        _directoryManager = directoryManager;
        _ephemeralVolumeProvisioner = ephemeralVolumeProvisioner;
    }

    public async Task Handle(PublishVolumeCommand request, CancellationToken cancellationToken)
    {
        var stagePath = request.StagePath;

        if (EphemeralVolumeHelper.IsEphemeral(request.Context))
        {
            stagePath = await _ephemeralVolumeProvisioner.Stage(request.VolumeId, request.Context, cancellationToken);
        }

        // csi specification says that we need to create target dir
        _directoryManager.EnsureExists(request.TargetPath);
        _mounter.Mount(stagePath!, request.TargetPath, request.ReadOnly);
    }
}