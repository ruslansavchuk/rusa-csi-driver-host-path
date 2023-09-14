using Csi.HostPath.Controller.Application.Controller.Capacity;
using Csi.V1;
using Grpc.Core;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override async Task<GetCapacityResponse> GetCapacity(GetCapacityRequest request, ServerCallContext context)
    {
        var capacity = await _mediator.Send(new GetCapacityQuery());
        
        return new GetCapacityResponse
        {
            AvailableCapacity = capacity.Available,
            MaximumVolumeSize = capacity.MaximumVolumeSize,
            MinimumVolumeSize = capacity.MinimumVolumeSize
        };
    }
}