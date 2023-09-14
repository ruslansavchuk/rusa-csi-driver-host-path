using Csi.HostPath.Controller.Application.Common.Exceptions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record UnpublishVolumeCommand(int? VolumeId, string NodeId) : IRequest<Volume>;

public class UnpublishVolumeCommandValidator : AbstractValidator<UnpublishVolumeCommand>
{
    public UnpublishVolumeCommandValidator()
    {
        RuleFor(c => c.VolumeId).NotEmpty();
        RuleFor(c => c.NodeId).NotEmpty();
    }
}

public class UnpublishVolumeCommandHandler : IRequestHandler<UnpublishVolumeCommand, Volume>
{
    private readonly IVolumeRepository _volumeRepository;

    public UnpublishVolumeCommandHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task<Volume> Handle(UnpublishVolumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var volume = await _volumeRepository.Get(request.VolumeId!.Value);
            if (volume.NodeId != request.NodeId)
            {
                throw new ServiceLogicException("unable to unpublich volume which is published to different node");
            }

            volume.NodeId = null;
            await _volumeRepository.Update(volume);
            return volume;
        }
        catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no elements.")
        {
            throw new NotFoundException("volume does not exists");
        }
    }
}
