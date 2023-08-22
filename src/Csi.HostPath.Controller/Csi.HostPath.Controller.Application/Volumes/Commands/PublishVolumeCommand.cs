using Csi.HostPath.Controller.Application.Common.Repositories;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Volumes.Commands;

public record PublishVolumeCommandResult();
public record PublishVolumeCommand(string VolumeId, string NodeId) : IRequest<PublishVolumeCommandResult>;

public class PublishVolumeCommandValidator : AbstractValidator<PublishVolumeCommand>
{
    public PublishVolumeCommandValidator()
    {
        RuleFor(c => c.VolumeId).NotEmpty();
        RuleFor(c => c.NodeId).NotEmpty();
    }
}

public class PublishVolumeCommandHandler : IRequestHandler<PublishVolumeCommand, PublishVolumeCommandResult>
{
    private readonly IVolumeRepository _volumeRepository;

    public PublishVolumeCommandHandler(IVolumeRepository volumeRepository)
    {
        _volumeRepository = volumeRepository;
    }

    public async Task<PublishVolumeCommandResult> Handle(PublishVolumeCommand request, CancellationToken cancellationToken)
    {
        var volume = await _volumeRepository.Get(request.VolumeId);
        volume.NodeId = request.NodeId;
        await _volumeRepository.Update(volume);
        return new PublishVolumeCommandResult();
    }
}