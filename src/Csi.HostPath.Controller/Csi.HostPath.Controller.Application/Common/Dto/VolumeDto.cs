namespace Csi.HostPath.Controller.Application.Common.Dto;

public record VolumeDto(
    string Id, 
    string Name, 
    long Capacity);