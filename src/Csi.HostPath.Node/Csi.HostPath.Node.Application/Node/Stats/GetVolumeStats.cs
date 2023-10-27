using System.Text.RegularExpressions;
using Csi.HostPath.Node.Application.Common.Configuration;
using Csi.HostPath.Node.Application.Node.Common;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Stats;

public record VolumeStats(long TotalBytes, long UsedBytes);

public record GetVolumeStats(int VolumeId) : IRequest<VolumeStats>;

public class GetVolumeStatsHandler : IRequestHandler<GetVolumeStats, VolumeStats>
{
    private static readonly Regex CapacityRegex = new("capacity-bytes-\\d+");
    private static readonly Regex BytesRegex = new("\\d+");
    
    private readonly IDirectoryManager _directoryManager;
    private readonly INodeConfiguration _nodeConfiguration;

    public GetVolumeStatsHandler(
        IDirectoryManager directoryManager, 
        INodeConfiguration nodeConfiguration)
    {
        _directoryManager = directoryManager;
        _nodeConfiguration = nodeConfiguration;
    }

    public Task<VolumeStats> Handle(GetVolumeStats request, CancellationToken cancellationToken)
    {
        var volumePathPrefix = Path.Combine(_nodeConfiguration.CsiDataDir, $"volume_id-{request.VolumeId}");
        
        var volumeDir = _directoryManager
            .GetSubFolders(_nodeConfiguration.CsiDataDir)
            .Single(p => p.StartsWith(volumePathPrefix));
        
        var volumeDataDir = Path.Combine(_nodeConfiguration.CsiDataDir, volumeDir);

        var usedBytes = _directoryManager.GetUsedBytes(volumeDataDir);
        // during volume creation we specify volume context, this context is used to specify capacity-bytes
        var totalBytes = int.Parse(BytesRegex.Match(CapacityRegex.Match(volumeDir).Value).Value);

        return Task.FromResult(new VolumeStats(totalBytes, usedBytes));
    }
}