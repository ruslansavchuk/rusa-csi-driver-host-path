using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService
{
    public override Task<NodeGetInfoResponse> NodeGetInfo(NodeGetInfoRequest request, ServerCallContext context)
    {
        var response = new NodeGetInfoResponse
        {
            NodeId = _options.Value.NodeId,
            MaxVolumesPerNode = 1_000_000_000
        };
        
        return Task.FromResult(response);
    }
}