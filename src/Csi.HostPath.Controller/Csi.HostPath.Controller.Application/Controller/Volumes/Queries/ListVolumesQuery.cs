using System.Linq.Expressions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Queries;

public record ListVolumesQueryResult(int? NextToken, List<Volume> Volumes);

public record ListVolumesQuery(string? Token, int MaxEntries) : IRequest<ListVolumesQueryResult>;

public class ListVolumesQueryValidator: AbstractValidator<ListVolumesQuery>
{
    public ListVolumesQueryValidator()
    {
        RuleFor(i => i.Token)
            .Must(t => string.IsNullOrWhiteSpace(t) || int.TryParse(t, out var _))
            .WithMessage("'Token' should be valid int or null");
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
        var items = volumes.OrderBy(v => v.Id).AsEnumerable();

        if (!string.IsNullOrEmpty(request.Token) && int.TryParse(request.Token, out var idFilter))
        {
            items = volumes.Where(v => v.Id > idFilter);
        }

        var result = items.Take(request.MaxEntries != default ? request.MaxEntries : 20).ToList();

        return new ListVolumesQueryResult(result.Any() ? result.Max(i => i.Id) : null, result);
    }
}