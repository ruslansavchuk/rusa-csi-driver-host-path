using Csi.HostPath.Controller.Application.Capabilities;
using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Volumes.Commands;
using Csi.HostPath.Controller.Application.Volumes.Queries;
using Csi.V1;
using Grpc.Core;
using MediatR;

namespace Csi.HostPath.Controller.Api.Grpc.Services;

public class Controller : Csi.V1.Controller.ControllerBase
{
    #region Fields
    
    private readonly IMediator _mediator;
    
    #endregion

    #region Constructors
    
    public Controller(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #endregion

    #region Capabilities
    
    public override async Task<ControllerGetCapabilitiesResponse> ControllerGetCapabilities(ControllerGetCapabilitiesRequest request, ServerCallContext context)
    {
        var capabilities = await _mediator.Send(new GetCapabilities());
        var response = new ControllerGetCapabilitiesResponse();
        response.Capabilities.AddRange(capabilities.Select(c => new ControllerServiceCapability
        {
            Rpc = new ControllerServiceCapability.Types.RPC
            {
                Type = (ControllerServiceCapability.Types.RPC.Types.Type)c
            }
        }));
        
        return response;
    }

    #endregion
    
    
    #region Create/Delete/Get
    public override async Task<CreateVolumeResponse> CreateVolume(CreateVolumeRequest request, ServerCallContext context)
    {
        var createdVolume = await _mediator.Send(new CreateVolumeCommand(request.Name, new CapacityRangeDto(request.CapacityRange?.LimitBytes, request.CapacityRange?.RequiredBytes)), context.CancellationToken);
        
        return new CreateVolumeResponse
        {
            Volume = new Volume
            {
                VolumeId = createdVolume.Id,
                CapacityBytes = createdVolume.Capacity
            }
        };
    }

    public override async Task<DeleteVolumeResponse> DeleteVolume(DeleteVolumeRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteVolumeCommand(request.VolumeId), context.CancellationToken);
        return new DeleteVolumeResponse();
    }

    public override async Task<ListVolumesResponse> ListVolumes(ListVolumesRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new ListVolumesQuery(request.StartingToken, request.MaxEntries), context.CancellationToken);
        var response = new ListVolumesResponse();

        response.NextToken = result.NextToken;
        
        response.Entries.AddRange(result.Volumes.Select(i => new ListVolumesResponse.Types.Entry
        {
            Volume = new Volume
            {
                VolumeId = i.Id,
                CapacityBytes = i.Capacity
            },
            Status = new ListVolumesResponse.Types.VolumeStatus
            {
                VolumeCondition = new VolumeCondition
                {
                    Abnormal = false,
                }
            }
        }));

        return response;
    }
    
    #endregion

    #region Publish/Unpublish

    public override async Task<ControllerPublishVolumeResponse> ControllerPublishVolume(ControllerPublishVolumeRequest request, ServerCallContext context)
    {
        await _mediator.Send(
            new PublishVolumeCommand(request.VolumeId, request.NodeId),
            context.CancellationToken);

        return new ControllerPublishVolumeResponse();
    }

    public override async Task<ControllerUnpublishVolumeResponse> ControllerUnpublishVolume(ControllerUnpublishVolumeRequest request, ServerCallContext context)
    {
        await _mediator.Send(
            new UnpublishVolumeCommand(request.VolumeId, request.NodeId),
            context.CancellationToken);

        return new ControllerUnpublishVolumeResponse();
    }

    #endregion
}