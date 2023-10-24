using System.Security;
using Csi.HostPath.Controller.Application.Common.Exceptions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record PublishVolumeCommand(
    int? VolumeId, 
    string? NodeId,
    AccessType? AccessType) : IRequest<Volume>;

public class PublishVolumeCommandValidator : AbstractValidator<PublishVolumeCommand>
{
    public PublishVolumeCommandValidator()
    {
        RuleFor(c => c.VolumeId).NotEmpty();
        RuleFor(c => c.NodeId).NotEmpty();
        RuleFor(c => c.AccessType).NotEmpty().IsInEnum();
    }
}

public class PublishVolumeCommandHandler : IRequestHandler<PublishVolumeCommand, Volume>
{
    private readonly IVolumeRepository _volumeRepository;

    public PublishVolumeCommandHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task<Volume> Handle(PublishVolumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var volume = await _volumeRepository.Get(request.VolumeId!.Value);

            if (volume.NodeId is not null && volume.NodeId != request.NodeId)
            {
                throw new ServiceLogicException("unable to publish volume to different node");
            }

            volume.SetNode(request.NodeId);

            if (request.AccessType != volume.AccessType)
            {
                throw new ServiceLogicException("capability do not match");
            }

            await _volumeRepository.Update(volume);
            return volume;
        }
        catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no elements.")
        {
            throw new NotFoundException("volume does not exists");
        }
    }
}