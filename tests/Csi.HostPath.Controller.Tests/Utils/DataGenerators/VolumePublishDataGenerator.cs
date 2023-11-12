using Csi.V1;

namespace Csi.HostPath.Controller.Tests.Utils.DataGenerators;

public static class VolumePublishDataGenerator
{
    public static ControllerPublishVolumeRequest GeneratePublishVolumeCommand(
        string? volumeId,
        string? nodeId = null,
        bool withCapability = true)
        => new()
        {
            NodeId = nodeId ?? Guid.NewGuid().ToString(),
            VolumeId = volumeId,
            VolumeCapability = withCapability 
                ? VolumeCapabilityDataGenerator.GenerateVolumeCapability() 
                : null
        };
    public static ControllerUnpublishVolumeRequest GenerateUnpublishVolumeCommand(
        string? volumeId,
        string? nodeId = null)
        => new()
        {
            NodeId = nodeId ?? Guid.NewGuid().ToString(),
            VolumeId = volumeId
        };
}