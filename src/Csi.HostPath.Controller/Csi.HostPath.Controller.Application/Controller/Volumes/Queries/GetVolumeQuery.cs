using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Queries;

public record GetVolumeQuery(int? Id) : IRequest<Volume>;

public class GetVolumeQueryHandler : IRequestHandler<GetVolumeQuery, Volume>
{
    private readonly IVolumeRepository _volumeRepository;

    public GetVolumeQueryHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public Task<Volume> Handle(GetVolumeQuery request, CancellationToken cancellationToken)
    {
        return _volumeRepository.Get(request.Id!.Value);
    }
}