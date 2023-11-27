using Csi.HostPath.Node.Application.Node.Stats;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override async Task<NodeGetVolumeStatsResponse> NodeGetVolumeStats(NodeGetVolumeStatsRequest request, ServerCallContext context)
    {
        var query = new GetVolumeStats(request.VolumeId);
        var stats = await _mediator.Send(query, context.CancellationToken);
        return ToResponse(stats);
    }

    private static NodeGetVolumeStatsResponse ToResponse(VolumeStats stats)
    {
        var response = new NodeGetVolumeStatsResponse();
        response.Usage.Add(new VolumeUsage
        {
            Total = stats.TotalBytes,
            Used = stats.UsedBytes,
            Unit = VolumeUsage.Types.Unit.Bytes
        });

        return response;
    }
}