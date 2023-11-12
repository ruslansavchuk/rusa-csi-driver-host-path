using Csi.V1;

namespace Csi.HostPath.Controller.Tests.Utils.DataGenerators;

public static class VolumeCapabilityDataGenerator
{
    public static VolumeCapability GenerateVolumeCapability(
        bool asMount = true,
        VolumeCapability.Types.AccessMode.Types.Mode mode =
            VolumeCapability.Types.AccessMode.Types.Mode.SingleNodeWriter)
        => asMount
            ? new VolumeCapability
            {
                Mount = new VolumeCapability.Types.MountVolume(),
                AccessMode = new VolumeCapability.Types.AccessMode
                {
                    Mode = mode
                }
            }
            : new VolumeCapability
            {
                Block = new VolumeCapability.Types.BlockVolume(),
                AccessMode = new VolumeCapability.Types.AccessMode
                {
                    Mode = mode
                }
            };
}