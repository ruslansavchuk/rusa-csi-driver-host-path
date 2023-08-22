using Csi.HostPath.Controller.Application.Common.Repositories;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Volumes.Commands;

public record DeleteVolumeCommand(string Id) : IRequest;

public class DeleteVolumeValidator : AbstractValidator<DeleteVolumeCommand>
{
    public DeleteVolumeValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}

public class DeleteVolumeRequestHandler : IRequestHandler<DeleteVolumeCommand>
{
    private readonly IVolumeRepository _volumeRepository;

    public DeleteVolumeRequestHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task Handle(DeleteVolumeCommand request, CancellationToken cancellationToken)
    {
        // should return success response when no volume found
        var volume = await _volumeRepository.Get(request.Id);
        await _volumeRepository.Delete(volume);
    }
}