using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record PublishVolumeCommand(int VolumeId, string TargetPath, bool ReadOnly, Dictionary<string, string> Context) : IRequest;

public class PublishVolumeCommandHandler : IRequestHandler<PublishVolumeCommand>
{
    private readonly IMounter _mounter;
    private readonly IDirectoryManager _directoryManager;
    private readonly INodeConfiguration _nodeConfiguration;

    public PublishVolumeCommandHandler(
        IMounter mounter, 
        IDirectoryManager directoryManager, 
        INodeConfiguration nodeConfiguration)
    {
        _mounter = mounter;
        _directoryManager = directoryManager;
        _nodeConfiguration = nodeConfiguration;
    }

    public Task Handle(PublishVolumeCommand request, CancellationToken cancellationToken)
    {
        var volumeDir = $"volume_id-{request.VolumeId}_{string.Join("_", request.Context.Select(kvp => $"{kvp.Key}-{kvp.Value}"))}";
        var volumeDataDir = Path.Combine(_nodeConfiguration.CsiDataDir, volumeDir);
        _directoryManager.EnsureExists(volumeDataDir);
        
        // csi specification says that we need to create target dir
        _directoryManager.EnsureExists(request.TargetPath);
        _mounter.Mount(volumeDataDir, request.TargetPath, request.ReadOnly);
        
        return Task.CompletedTask;
    }
}