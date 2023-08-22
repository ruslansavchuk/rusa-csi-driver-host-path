using System.Linq.Expressions;
using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Volumes.Queries;

public record ListVolumesQueryResult(string NextToken, List<VolumeDto> Volumes);

public record ListVolumesQuery(string? Token, int MaxEntries) : IRequest<ListVolumesQueryResult>;

public class ListVolumesValidator : AbstractValidator<ListVolumesQuery>
{
    public ListVolumesValidator()
    {
        RuleFor(r => r.MaxEntries).GreaterThan(0);
    }
}

public class ListVolumesRequestHandler : IRequestHandler<ListVolumesQuery, ListVolumesQueryResult>
{
    private readonly IVolumeRepository _volumeRepository;

    public ListVolumesRequestHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task<ListVolumesQueryResult> Handle(ListVolumesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Volume, bool>>? filter = request.Token != null 
            ? v => true //v.Id > request.Token 
            : null;
        
        var volumes = await _volumeRepository.Get(filter);

        return new ListVolumesQueryResult(null, volumes
            .Take(request.MaxEntries)
            .Select(v => new VolumeDto(v.Id, v.Name, v.Size))
            .ToList());
    }
}