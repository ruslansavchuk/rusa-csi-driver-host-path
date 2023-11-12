using Csi.V1;

namespace Csi.HostPath.Controller.Tests.Utils.DataGenerators;

public class VolumeDataGenerator
{
    public static CreateVolumeRequest GenerateCreateVolumeCommand(
        string? withName = null, 
        long? withCapacity = -1,
        bool asMount = true, 
        bool asBlock = false)
    {
        var oneMb = CapacityDataGenerator.Megabytes(1);

        var request = new CreateVolumeRequest
        {
            Name = withName ?? Guid.NewGuid().ToString(),
            CapacityRange = withCapacity switch
            {
                null => null,
                -1 => new CapacityRange
                {
                    LimitBytes = oneMb,
                    RequiredBytes = oneMb
                },
                _ => new CapacityRange
                {
                    LimitBytes = withCapacity.Value,
                    RequiredBytes = withCapacity.Value
                }
            }
        };

        if (asMount)
        {
            request.VolumeCapabilities.Add(VolumeCapabilityDataGenerator.GenerateVolumeCapability());
        }

        if (asBlock)
        {
            request.VolumeCapabilities.Add(VolumeCapabilityDataGenerator.GenerateVolumeCapability(false));
        }

        return request;
    }

    public static DeleteVolumeRequest GenerateDeleteVolumeRequest(string volumeId)
    {
        return new DeleteVolumeRequest
        {
            VolumeId = volumeId
        };
    }
}