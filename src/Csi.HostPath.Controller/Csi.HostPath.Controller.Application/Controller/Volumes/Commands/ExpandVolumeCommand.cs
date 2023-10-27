using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record ExpandVolumeCommand(
    int? Id, 
    CapacityRangeDto? Capacity, 
    AccessType? AccessType) 
    : IRequest<(Volume, bool)>;

public class ExpandVolumeCommandHandler : IRequestHandler<ExpandVolumeCommand, (Volume, bool)>
{
    private readonly IVolumeRepository _repository;

    public ExpandVolumeCommandHandler(IVolumeRepository repository)
    {
        _repository = repository;
    }

    public async Task<(Volume, bool)> Handle(ExpandVolumeCommand request, CancellationToken cancellationToken)
    {
        var volume = await _repository.Get(request.Id!.Value);
        volume.SetCapacity(request.Capacity!.Required!);
        await _repository.Update(volume);
        return (volume, true);
    }
}