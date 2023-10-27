using MediatR;

namespace Csi.HostPath.Node.Api.Grpc.Services.Node;

public partial class NodeService : Csi.V1.Node.NodeBase
{
    private readonly IMediator _mediator;

    public NodeService(IMediator mediator)
    {
        _mediator = mediator;
    }

    private static int ToVolumeId(string volumeId) => int.Parse(volumeId);
}