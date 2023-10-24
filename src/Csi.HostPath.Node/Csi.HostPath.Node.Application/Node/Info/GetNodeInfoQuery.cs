using Csi.HostPath.Node.Application.Common.Configuration;
using MediatR;

namespace Csi.HostPath.Node.Application.Node.Info;

public record NodeInfo(string Id, int MaxVolumesPerNode);

public record GetNodeInfoQuery : IRequest<NodeInfo>;

public record GetNodeInfoQueryHandler : IRequestHandler<GetNodeInfoQuery, NodeInfo>
{
    private readonly Configuration _configuration;

    public GetNodeInfoQueryHandler()
    {
        _configuration = null;
    }

    public Task<NodeInfo> Handle(GetNodeInfoQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new NodeInfo(_configuration.NodeId, _configuration.MaxVolumesPerNode));
    }
}