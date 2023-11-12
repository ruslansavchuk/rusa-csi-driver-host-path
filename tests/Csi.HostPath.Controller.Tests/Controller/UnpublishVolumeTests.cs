using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using FluentAssertions;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class UnpublishVolumeTests : ControllerTestsBase
{
    [Fact]
    public void ShouldFailWhenNoVolumeIdSpecified()
    {
        var command = VolumePublishDataGenerator.GenerateUnpublishVolumeCommand(string.Empty);
        UnpublishVolume(command)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void ShouldFailWhenNoNodeIdSpecified()
    {
        var createVolumeCommand = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var createdVolume = CreateVolume(createVolumeCommand)();
        var publishVolumeCommand = VolumePublishDataGenerator
            .GeneratePublishVolumeCommand(createdVolume.Volume.VolumeId);
        PublishVolume(publishVolumeCommand)();
        
        var command = VolumePublishDataGenerator
            .GenerateUnpublishVolumeCommand(createdVolume.Volume.VolumeId, nodeId: string.Empty);
        
        UnpublishVolume(command)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void ShouldFailWhenCorrectParametersSpecified()
    {
        var createVolumeCommand = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var createdVolume = CreateVolume(createVolumeCommand)();
        var publishVolumeCommand = VolumePublishDataGenerator
            .GeneratePublishVolumeCommand(createdVolume.Volume.VolumeId);
        PublishVolume(publishVolumeCommand)();
        
        var command = VolumePublishDataGenerator
            .GenerateUnpublishVolumeCommand(createdVolume.Volume.VolumeId, nodeId: string.Empty);
        
        UnpublishVolume(command)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
}