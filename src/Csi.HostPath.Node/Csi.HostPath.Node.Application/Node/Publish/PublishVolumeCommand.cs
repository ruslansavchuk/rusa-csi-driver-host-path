using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record PublishVolumeCommand(string VolumeId, string TargetPath, bool ReadOnly) : IRequest;

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
        var volumeDataDir = Path.Combine(_nodeConfiguration.CsiDataDir, request.VolumeId);
        _directoryManager.EnsureExists(volumeDataDir);
        _mounter.Mount(volumeDataDir, request.TargetPath, request.ReadOnly);
        
        return Task.CompletedTask;
    }
}