using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override Task<ValidateVolumeCapabilitiesResponse> ValidateVolumeCapabilities(ValidateVolumeCapabilitiesRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
        // volume id
        // access type
        return base.ValidateVolumeCapabilities(request, context);
    }
}