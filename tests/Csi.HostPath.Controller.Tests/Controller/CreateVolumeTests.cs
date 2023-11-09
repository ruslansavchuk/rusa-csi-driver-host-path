﻿using Csi.HostPath.Controller.Tests.Utils.DataGenerators;
using FluentAssertions;
using FluentAssertions.Execution;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CreateVolumeTests : ControllerTestsBase
{
    private const int VolumeNameMaxLength = 128;
    
    // should return appropriate values SingleNodeWriter NoCapacity
    // should return appropriate values SingleNodeWriter WithCapacity 1Gi
    
    [Fact]
    public void ShouldFailWhenNoNameProvided()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(withName: string.Empty);

        var action = () => CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldNotFailWhenRequestingToCreateVolumeWithAlreadyExistingNameAndCapacity()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(
            withName: Guid.NewGuid().ToString(), 
            withCapacity: CapacityDataGenerator.Megabytes(2));
        
        CreateVolume(request);

        var action = () => CreateVolume(request);

        action.Should().NotThrow();
    }
    
    [Fact]
    public void ShouldFailWhenRequestingToCreateVolumeWithAlreadyExistingNameAndDifferentCapacity()
    {
        var volumeName = Guid.NewGuid().ToString();

        var request1 = VolumeDataGenerator.GenerateCreateVolumeCommand(
            withName: volumeName, 
            withCapacity: CapacityDataGenerator.Megabytes(2));
        
        CreateVolume(request1);

        var request2 = VolumeDataGenerator.GenerateCreateVolumeCommand(
            withName: volumeName, 
            withCapacity: CapacityDataGenerator.Megabytes(3));
        
        var action = () => CreateVolume(request2);

        action.Should().Throw<RpcException>().Where(e => e.StatusCode == StatusCode.AlreadyExists);
    }

    [Fact]
    public void ShouldFailWhenTryToCreateVolumeWithNameLongerThanMax()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(withName: new string('a', VolumeNameMaxLength + 1));

        var action = () => CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldSuccessfullyCreateVolumeWithNameMaxLength()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(withName: new string('a', VolumeNameMaxLength));

        var action = () => CreateVolume(request);

        action.Should().NotThrow();
    }

    [Fact]
    public void ShouldFailWhenNoVolumeCapabilitiesAreProvided()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(asMount:false);

        var action = () => CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void ShouldFailWhenBlockCapabilityProvided()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(asBlock: true, asMount:false);

        var action = () => CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void ShouldReturnCapacityWhenNoCapacitySpecified()
    {
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(withCapacity: null);

        var response = CreateVolume(request);

        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Volume.Should().NotBeNull();
            response.Volume.CapacityBytes.Should().BeGreaterThan(0);
        }
    }
    
    [Fact]
    public void ShouldReturnCorrectCapacityWhenCapacitySpecified()
    {
        var capacity = CapacityDataGenerator.Gigabytes(1);
        var request = VolumeDataGenerator.GenerateCreateVolumeCommand(withCapacity: capacity);

        var response = CreateVolume(request);

        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Volume.Should().NotBeNull();
            response.Volume.CapacityBytes.Should().Be(capacity);
        }
    }
}