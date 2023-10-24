using Csi.HostPath.Controller.Application.Common.Configuration;
using Csi.HostPath.Controller.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace Csi.HostPath.Controller.Application.Controller.Capacity;

public record Capacity(long Available, long MaximumVolumeSize, long MinimumVolumeSize);

public record GetCapacityQuery : IRequest<Capacity>;

public class GetCapacityQueryHandler : IRequestHandler<GetCapacityQuery, Capacity>
{
    private readonly IOptions<Configuration> _options;
    private readonly IVolumeRepository _volumeRepository;

    public GetCapacityQueryHandler(IOptions<Configuration> options, IVolumeRepository volumeRepository)
    {
        _options = options;
        _volumeRepository = volumeRepository;
    }

    public async Task<Capacity> Handle(GetCapacityQuery request, CancellationToken cancellationToken)
    {
        var volumes = await _volumeRepository.Get();
        var capacity = _options.Value.Capacity;
        
        return new Capacity(capacity - volumes.Sum(v => v.Capacity), 
            _options.Value.MaxVolumeSize, 
            _options.Value.MinVolumeSize);
    }
}