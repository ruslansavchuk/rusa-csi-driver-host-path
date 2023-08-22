using Csi.HostPath.Controller.Application.Common.Repositories;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Volumes.Commands;

public record UnpublishVolumeCommandResult(); 

public record UnpublishVolumeCommand(string VolumeId, string NodeId) : IRequest<UnpublishVolumeCommandResult>;

public class UnpublishVolumeCommandValidator : AbstractValidator<UnpublishVolumeCommand>
{
    public UnpublishVolumeCommandValidator()
    {
        RuleFor(c => c.VolumeId).NotEmpty();
        RuleFor(c => c.NodeId).NotEmpty();
    }
}

public class UnpublishVolumeCommandHandler : IRequestHandler<UnpublishVolumeCommand, UnpublishVolumeCommandResult>
{
    private readonly IVolumeRepository _volumeRepository;

    public UnpublishVolumeCommandHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task<UnpublishVolumeCommandResult> Handle(UnpublishVolumeCommand request, CancellationToken cancellationToken)
    {
        var volume = await _volumeRepository.Get(request.VolumeId);
        if (volume.NodeId != request.NodeId)
        {
            // it is impossible with this driver
        }

        volume.NodeId = null;
        await _volumeRepository.Update(volume);
        return new UnpublishVolumeCommandResult();
    }
}