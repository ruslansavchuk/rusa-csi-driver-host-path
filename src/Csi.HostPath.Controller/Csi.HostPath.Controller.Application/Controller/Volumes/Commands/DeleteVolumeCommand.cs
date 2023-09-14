using Csi.HostPath.Controller.Application.Common.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record DeleteVolumeCommand(int? Id) : IRequest;

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
    private readonly ILogger<DeleteVolumeRequestHandler> _logger;

    public DeleteVolumeRequestHandler(
        IVolumeRepository volumeRepository, 
        ILogger<DeleteVolumeRequestHandler> logger)
    {
        _volumeRepository = volumeRepository;
        _logger = logger;
    }

    public async Task Handle(DeleteVolumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var volume = await _volumeRepository.Get(request.Id!.Value);
            await _volumeRepository.Delete(volume);
        }
        catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no elements.")
        {
            _logger.LogInformation("{message} {error}", ex.Message, ex);
            // delete operation should be idempotent that is why we need to return success response in case if volume does not exists   
        }
    }
}