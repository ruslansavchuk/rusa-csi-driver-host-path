using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService
{
    public override Task<NodeExpandVolumeResponse> NodeExpandVolume(NodeExpandVolumeRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}