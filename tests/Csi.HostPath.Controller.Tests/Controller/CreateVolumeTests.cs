using Csi.V1;
using FluentAssertions;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CreateVolumeTests : ControllerTestsBase
{
    private const int VolumeNameMaxLength = 128;
    
    [Fact]
    public void ShouldFailWhenNoNameProvided()
    {
        var request = BuildVolumeRequest(withName: string.Empty);

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldFailWhenTryToCreateVolumeWithNameLongerThanMax()
    {
        var request = BuildVolumeRequest(withName: new string('a', VolumeNameMaxLength + 1));

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    [Fact]
    public void ShouldSuccessfullyCreateVolumeWithNameMaxLength()
    {
        var request = BuildVolumeRequest(withName: new string('a', VolumeNameMaxLength));

        var action = () => Client.CreateVolume(request);

        action.Should().NotThrow();
    }

    [Fact]
    public void ShouldFailWhenNoVolumeCapabilitiesAreProvided()
    {
        var request = BuildVolumeRequest(asMount:false);

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void ShouldFailWhenBlockCapabilityProvided()
    {
        var request = BuildVolumeRequest(asBlock: true, asMount:false);

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    private CreateVolumeRequest BuildVolumeRequest(
        string? withName = null, 
        long? withCapacity = null,
        bool asMount = true, 
        bool asBlock = false)
    {
        const long oneMb = 1024 * 1024;
        
        var request = new CreateVolumeRequest
        {
            Name = withName ?? Guid.NewGuid().ToString(),
            CapacityRange = new CapacityRange
            {
                LimitBytes = withCapacity ?? oneMb,
                RequiredBytes = withCapacity ?? oneMb
            }
        };

        if (asMount)
        {
            request.VolumeCapabilities.Add(new VolumeCapability
            {
                Mount = new VolumeCapability.Types.MountVolume()
            });
        }

        if (asBlock)
        {
            request.VolumeCapabilities.Add(new VolumeCapability
            {
                Block = new VolumeCapability.Types.BlockVolume()
            });
        }

        return request;
    }
}