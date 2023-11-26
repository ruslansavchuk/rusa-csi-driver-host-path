using Csi.HostPath.Controller.Application.Common.Dto;
using Csi.HostPath.Controller.Application.Common.Exceptions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Application.Controller.Volumes.Validators;
using Csi.HostPath.Controller.Domain.Volumes;
using FluentValidation;
using MediatR;

namespace Csi.HostPath.Controller.Application.Controller.Volumes.Commands;

public record CreateVolumeCommand(
	string Name, 
	CapacityRangeDto? Capacity,
	List<AccessMode?>? AccessModes,
	Dictionary<string, string> VolumeContext)
	: IRequest<Volume>;

public class CreateVolumeValidator : AbstractValidator<CreateVolumeCommand>
{
    public CreateVolumeValidator(CapacityRangeValidator capacityRangeValidator)
    {
	    RuleFor(r => r.Name)
		    .NotEmpty()
		    .MaximumLength(128);
	    RuleFor(r => r.Capacity).SetValidator(capacityRangeValidator!);
	    
	    RuleFor(r => r.AccessModes)
		    .NotNull()
		    .NotEmpty()
		    .ForEach(cr => cr
			    .NotNull()
			    .IsInEnum());
    }
}

public class CreateVolumeRequestHandler : IRequestHandler<CreateVolumeCommand, Volume>
{
	private readonly IVolumeRepository _volumeRepository;
	private const long DefaultVolumeCapacity = 4096;

	public CreateVolumeRequestHandler(IVolumeRepository volumeRepository)
	{
		_volumeRepository = volumeRepository;
	}

	public async Task<Volume> Handle(CreateVolumeCommand request, CancellationToken cancellationToken)
	{
		var requestedCapacity = request.Capacity?.Required ?? 0; 
		var capacity = requestedCapacity > 0 ? requestedCapacity : DefaultVolumeCapacity;

		var existingVolumes = await _volumeRepository.Get(name: request.Name);
		if (existingVolumes.Count > 0)
		{
			var existingVolume = existingVolumes.Single();
			if (existingVolume.Capacity != capacity)
			{
				throw new AlreadyExistsException("Volume with the same name already exists");
			}

			return existingVolume;
		}

		var ephemeral = IsEphemeral(request.VolumeContext);

		var volume = Volume.Create(
			request.Name,
			capacity, 
			false, 
			ephemeral,
			request.AccessModes!.Single(), 
			null, 
			null, 
			false);
		
		await _volumeRepository.Add(volume);

		return volume; 
	}

	private static bool IsEphemeral(Dictionary<string, string> volumeContext)
		=> volumeContext.TryGetValue("csi.storage.k8s.io/ephemeral", out var value)
		   && bool.TryParse(value, out var ephemeral)
		   && ephemeral;
}
