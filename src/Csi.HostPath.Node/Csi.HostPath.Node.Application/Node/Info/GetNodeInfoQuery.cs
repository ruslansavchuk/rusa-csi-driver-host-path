using Csi.HostPath.Node.Application.Common.Configuration;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Info;

public record NodeInfo(string Id, int MaxVolumesPerNode);

public record GetNodeInfoQuery : IRequest<NodeInfo>;

public record GetNodeInfoQueryHandler : IRequestHandler<GetNodeInfoQuery, NodeInfo>
{
    private readonly INodeConfiguration _configuration;

    public GetNodeInfoQueryHandler(INodeConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<NodeInfo> Handle(GetNodeInfoQuery request, CancellationToken cancellationToken)
    {
        // default value for max volume per node is 100
        return Task.FromResult(new NodeInfo(_configuration.NodeId, _configuration.MaxVolumesPerNode ?? 100));
    }
}