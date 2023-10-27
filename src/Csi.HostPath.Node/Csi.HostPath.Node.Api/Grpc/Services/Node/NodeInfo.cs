using Csi.HostPath.Node.Application.Node.Info;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService
{
    public override async Task<NodeGetInfoResponse> NodeGetInfo(NodeGetInfoRequest request, ServerCallContext context)
    {
        var nodeInfo = await _mediator.Send(new GetNodeInfoQuery(), context.CancellationToken);
        return ToResponse(nodeInfo);
    }

    private static NodeGetInfoResponse ToResponse(NodeInfo nodeInfo)
    {
        return new NodeGetInfoResponse
        {
            NodeId = nodeInfo.Id,
            MaxVolumesPerNode = nodeInfo.MaxVolumesPerNode,
        };
    }
}