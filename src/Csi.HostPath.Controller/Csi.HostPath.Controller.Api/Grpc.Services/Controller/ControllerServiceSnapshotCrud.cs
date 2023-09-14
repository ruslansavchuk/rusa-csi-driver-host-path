using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override Task<CreateSnapshotResponse> CreateSnapshot(CreateSnapshotRequest request, ServerCallContext context)
    {
        return base.CreateSnapshot(request, context);
    }

    public override Task<DeleteSnapshotResponse> DeleteSnapshot(DeleteSnapshotRequest request, ServerCallContext context)
    {
        return base.DeleteSnapshot(request, context);
    }

    public override Task<ListSnapshotsResponse> ListSnapshots(ListSnapshotsRequest request, ServerCallContext context)
    {
        return base.ListSnapshots(request, context);
    }
    
}