using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService
{
    public override Task<NodeGetVolumeStatsResponse> NodeGetVolumeStats(NodeGetVolumeStatsRequest request, ServerCallContext context)
    {
        return Task.FromResult(new NodeGetVolumeStatsResponse{});
        throw new NotImplementedException();
    }
}