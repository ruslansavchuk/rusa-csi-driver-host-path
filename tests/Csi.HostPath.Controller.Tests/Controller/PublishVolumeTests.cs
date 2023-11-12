using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using FluentAssertions;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class PublishVolumeTests : ControllerTestsBase
{
    [Fact]
    public void ShouldFailWhenNoVolumeIdProvided()
    {
        var publishVolumeCommand = VolumePublishDataGenerator.GeneratePublishVolumeCommand(string.Empty);
        PublishVolume(publishVolumeCommand)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldFailWhenNoNodeIdIsProvided()
    {
        var volume = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var createdVolume = CreateVolume(volume)();
        
        var publishVolumeCommand = VolumePublishDataGenerator.GeneratePublishVolumeCommand(createdVolume.Volume.VolumeId, nodeId: string.Empty);
        PublishVolume(publishVolumeCommand)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    // unknown status code
    public void ShouldFailWhenNoCapabilityIsProvided()
    {
        var volume = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var createdVolume = CreateVolume(volume)();
        
        var publishVolumeCommand = VolumePublishDataGenerator.GeneratePublishVolumeCommand(createdVolume.Volume.VolumeId, withCapability: false);
        PublishVolume(publishVolumeCommand)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    // status code not found
    public void ShouldFailWhenNotExistingVolumeIsProvided()
    {
        var publishVolumeCommand = VolumePublishDataGenerator.GeneratePublishVolumeCommand(Guid.NewGuid().ToString());
        PublishVolume(publishVolumeCommand)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldSuccessWhenConnectParametersProvided()
    {
        var volume = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var createdVolume = CreateVolume(volume)();

        var publishVolumeCommand = VolumePublishDataGenerator.GeneratePublishVolumeCommand(createdVolume.Volume.VolumeId);
        PublishVolume(publishVolumeCommand)
            .Should()
            .NotThrow();
    }
}