using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Domain.Volumes;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record ExpandVolumeCommand(
    int? Id, 
    CapacityRangeDto? Capacity, 
    AccessType? AccessType) 
    : IRequest<(Volume, bool)>;