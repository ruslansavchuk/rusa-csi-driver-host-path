using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Publish;

public record PublishVolumeCommand(
    int VolumeId, 
    string TargetPath, 
    string StagePath, 
    bool ReadOnly, 
    Dictionary<string, string> Context) : IRequest;

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
        // pay attention that for ephemeral volumes kubernetes will provide empty stage path 
        // also for ephemeral volumes kubernetes do not call create volume and stage volume commands
        // it means that we need to handle it in somehow
        
        // csi specification says that we need to create target dir
        _directoryManager.EnsureExists(request.TargetPath);
        _mounter.Mount(request.StagePath, request.TargetPath, request.ReadOnly);

        return Task.CompletedTask;
    }
}