using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Stage;

public record StageVolumeCommand(int VolumeId, string StagPath, Dictionary<string, string> Context) : IRequest;

public class StageVolumeCommandHandler : IRequestHandler<StageVolumeCommand>
{
    private readonly IMounter _mounter;
    private readonly IDirectoryManager _directoryManager;
    private readonly INodeConfiguration _nodeConfiguration;

    public StageVolumeCommandHandler(
        IMounter mounter, 
        IDirectoryManager directoryManager, 
        INodeConfiguration nodeConfiguration)
    {
        _mounter = mounter;
        _directoryManager = directoryManager;
        _nodeConfiguration = nodeConfiguration;
    }

    public Task Handle(StageVolumeCommand request, CancellationToken cancellationToken)
    {
        const string capacityBytesKey = "capacity-bytes";
        var volumeDir = $"volume_id-{request.VolumeId}_{capacityBytesKey}-{request.Context[capacityBytesKey]}";
        var volumeDataDir = Path.Combine(_nodeConfiguration.CsiDataDir, volumeDir);
        _directoryManager.EnsureExists(volumeDataDir);

        // csi specification says that we need to create target dir
        _directoryManager.EnsureExists(request.StagPath);
        _mounter.Mount(volumeDataDir, request.StagPath);
        
        return Task.CompletedTask;
    }
}