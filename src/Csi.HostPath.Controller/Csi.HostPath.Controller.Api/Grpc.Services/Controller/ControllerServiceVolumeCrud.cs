using System.Reflection.Metadata.Ecma335;
using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Controller.Volumes.Commands;
using Csi.HostPath.Controller.Application.Controller.Volumes.Queries;
using Csi.HostPath.Controller.Domain.Volumes;
using Csi.V1;
using Google.Protobuf.Collections;
using Grpc.Core;
using Volume = Csi.V1.Volume;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService
{
    public override async Task<CreateVolumeResponse> CreateVolume(CreateVolumeRequest request, ServerCallContext context)
    {
        var command = ToCommand(request);
        var createdVolume = await _sender.Send(command, context.CancellationToken);
        
        return new CreateVolumeResponse
        {
            Volume = ToVolumeDto(createdVolume)
        };
    }

    private CreateVolumeCommand ToCommand(CreateVolumeRequest request)
    {
        var accessType = request.VolumeCapabilities
            .Select(ToAccessType)
            .SingleOrDefault();
        
        var command = new CreateVolumeCommand(request.Name,
            new CapacityRangeDto(
                request.CapacityRange?.LimitBytes, 
                request.CapacityRange?.RequiredBytes),
            accessType);

        return command;
    }

    private AccessType? ToAccessType(VolumeCapability capability) => 
        capability.AccessTypeCase switch
        {
            VolumeCapability.AccessTypeOneofCase.Block => AccessType.Block,
            VolumeCapability.AccessTypeOneofCase.Mount => AccessType.Mount,
            VolumeCapability.AccessTypeOneofCase.None => null,
            _ => throw new ArgumentOutOfRangeException()
        };

    private Volume ToVolumeDto(Csi.HostPath.Controller.Domain.Volumes.Volume volume)
    {
        var volumeModel =new Volume
        {
            VolumeId = volume.Id.ToString(),
            CapacityBytes = volume.Capacity
        };

        foreach (var kvp in volume.Context)
        {
            volumeModel.VolumeContext.Add(kvp.Key, kvp.Value);
        }
            
        return volumeModel;
    }

    public override async Task<DeleteVolumeResponse> DeleteVolume(DeleteVolumeRequest request, ServerCallContext context)
    {
        var command = new DeleteVolumeCommand(ToVolumeId(request.VolumeId));
        await _sender.Send(command, context.CancellationToken);
        return new DeleteVolumeResponse();
    }

    private int? ToVolumeId(string id) => string.IsNullOrWhiteSpace(id) 
        ? null 
        : int.TryParse(id, out var vId) 
            ? vId 
            : -1;

    public override async Task<ListVolumesResponse> ListVolumes(ListVolumesRequest request, ServerCallContext context)
    {
        var query = new ListVolumesQuery(request.StartingToken, request.MaxEntries);
        var result = await _sender.Send(query, context.CancellationToken);
        return ToResponse(result);
    }

    private ListVolumesResponse ToResponse(ListVolumesQueryResult result)
    {
        var response = new ListVolumesResponse
        {
            NextToken = result.NextToken.ToString()
        };

        response.Entries.AddRange(result.Volumes.Select(i =>
        {
            var volume = new ListVolumesResponse.Types.Entry
            {
                Volume = ToVolumeDto(i),
                Status = new ListVolumesResponse.Types.VolumeStatus
                {
                    // check if it is the same as in get volume
                    VolumeCondition = new VolumeCondition
                    {
                        Abnormal = false,
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(i.NodeId))
            {
                volume.Status.PublishedNodeIds.Add(i.NodeId);    
            }

            return volume;
        }));

        return response;
    }

    public override async Task<ControllerGetVolumeResponse> ControllerGetVolume(ControllerGetVolumeRequest request, ServerCallContext context)
    {
        var query = new GetVolumeQuery(ToVolumeId(request.VolumeId));
        var result = await _sender.Send(query, context.CancellationToken);
        return ToGetVolumeResponse(result);
    }

    private ControllerGetVolumeResponse ToGetVolumeResponse(Csi.HostPath.Controller.Domain.Volumes.Volume volume)
    {
        var status = new ControllerGetVolumeResponse.Types.VolumeStatus();
        if (!string.IsNullOrWhiteSpace(volume.NodeId))
        {
            status.PublishedNodeIds.Add(volume.NodeId!);
        }

        return new ControllerGetVolumeResponse
        {
            Status = status,
            Volume = ToVolumeDto(volume)
        };
    }

    public override Task<ControllerModifyVolumeResponse> ControllerModifyVolume(ControllerModifyVolumeRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task<ControllerExpandVolumeResponse> ControllerExpandVolume(ControllerExpandVolumeRequest request, ServerCallContext context)
    {
        var query = ToCommand(request);
        var (expandedVolume, expansionRequired) = await _sender.Send(query, context.CancellationToken);
        return new ControllerExpandVolumeResponse
        {
            NodeExpansionRequired = expansionRequired,
            CapacityBytes = expandedVolume.Capacity
        };
    }

    private ExpandVolumeCommand ToCommand(ControllerExpandVolumeRequest request)
    {
        return new ExpandVolumeCommand(
            ToVolumeId(request.VolumeId),
            new CapacityRangeDto(
                request.CapacityRange.LimitBytes, 
                request.CapacityRange.RequiredBytes),
            ToAccessType(request.VolumeCapability));
    }
}