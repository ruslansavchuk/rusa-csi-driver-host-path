using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using FluentAssertions;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class DeleteVolumeTests : ControllerTestsBase
{
    [Fact]
    public void ShouldFailWhenNoVolumeIdIsProvided()
    {
        var request = VolumeDataGenerator.GenerateDeleteVolumeRequest(string.Empty);

        DeleteVolume(request)
            .Should()
            .Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    
    [Fact]
    public void ShouldSuccessfullyRemoveExistingVolume()
    {
        var volumeToCreate = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var result = CreateVolume(volumeToCreate)();

        var request = VolumeDataGenerator.GenerateDeleteVolumeRequest(result.Volume.VolumeId);

        DeleteVolume(request).Should().NotThrow();
    }
    
    [Fact]
    public void ShouldSuccessWhenTryToDeleteAlreadyDeletedVolume()
    {
        var volumeToCreate = VolumeDataGenerator.GenerateCreateVolumeCommand();
        var result = CreateVolume(volumeToCreate)();
        var request = VolumeDataGenerator.GenerateDeleteVolumeRequest(result.Volume.VolumeId);

        DeleteVolume(request);
        
        DeleteVolume(request).Should().NotThrow();
    }
    
    [Fact]
    public void ShouldSuccessWhenInvalidVolumeIdProvided()
    {
        var request = VolumeDataGenerator.GenerateDeleteVolumeRequest(Guid.NewGuid().ToString());
        DeleteVolume(request).Should().NotThrow();
    }
}