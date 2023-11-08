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
            request.VolumeCapabilities.Add(new VolumeCapability
            {
                Mount = new VolumeCapability.Types.MountVolume(),
                AccessMode = new VolumeCapability.Types.AccessMode
                {
                    Mode = VolumeCapability.Types.AccessMode.Types.Mode.SingleNodeWriter
                }
            });
        }

        if (asBlock)
        {
            request.VolumeCapabilities.Add(new VolumeCapability
            {
                Block = new VolumeCapability.Types.BlockVolume(),
                AccessMode = new VolumeCapability.Types.AccessMode
                {
                    Mode = VolumeCapability.Types.AccessMode.Types.Mode.SingleNodeWriter
                }
            });
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