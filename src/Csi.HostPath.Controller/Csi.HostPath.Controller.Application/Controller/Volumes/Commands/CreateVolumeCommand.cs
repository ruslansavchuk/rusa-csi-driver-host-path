using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Common.Exceptions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Application.Controller.Volumes.Validators;
using Csi.HostPath.Controller.Domain.Entities;
using Csi.HostPath.Controller.Domain.Enums;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record CreateVolumeCommand(
	string Name, 
	CapacityRangeDto? Capacity,
	AccessType? AccessType)
	: IRequest<Volume>;

public class CreateVolumeValidator : AbstractValidator<CreateVolumeCommand>
{
    public CreateVolumeValidator()
    {
	    RuleFor(r => r.Name)
		    .NotEmpty()
		    .MaximumLength(128);
	    RuleFor(r => r.Capacity).SetValidator(new CapacityRangeValidator()!);
	    RuleFor(r => r.AccessType)
		    .NotNull()
		    .IsInEnum();
    }
}

public class CreateVolumeRequestHandler : IRequestHandler<CreateVolumeCommand, Volume>
{
	private readonly IVolumeRepository _volumeRepository;
	private const int DefaultVolumeCapacity = 4096;

	public CreateVolumeRequestHandler(IVolumeRepository volumeRepository)
	{
		_volumeRepository = volumeRepository;
	}

	public async Task<Volume> Handle(CreateVolumeCommand request, CancellationToken cancellationToken)
	{
		var requestedCapacity = request.Capacity?.Required ?? 0; 
		var capacity = requestedCapacity > 0 ? requestedCapacity : DefaultVolumeCapacity;

		var existingVolumes = await _volumeRepository.Get(v => v.Name == request.Name);
		if (existingVolumes.Count > 0)
		{
			var existingVolume = existingVolumes.Single();
			if (existingVolume.Size < capacity)
			{
				throw new AlreadyExistsException("Volume with the same name already exists");
			}

			return existingVolume;
		}

		var volume = CreateVolumeInternal(request.Name, capacity);
		await _volumeRepository.Add(volume);

		return volume; 
	}

	private Volume CreateVolumeInternal(string name, long capacity)
	{
		return new Volume
		{
			// todo: think how to generate volume id
			// Id = Guid.NewGuid().ToString(), 
			Size = capacity,
			Name = name,
			Attached = false,
			Ephemeral = false,
			AccessType = AccessType.Mount,
			Path = null,
			NodeId = null,
			ReadOnlyAttach = false
		};
	}
}
