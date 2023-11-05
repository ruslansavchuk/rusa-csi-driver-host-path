using Csi.V1;
using FluentAssertions;
using Grpc.Core;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CreateVolumeTests : ControllerTestsBase
{
    [Fact]
    public void CreateShouldFailWhenNoNameProvided()
    {
        var request = BuildVolumeRequest(withName: string.Empty);

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }
    
    [Fact]
    public void CreateShouldFailWhenNoVolumeCapabilitiesAreProvided()
    {
        var request = BuildVolumeRequest(withName: string.Empty);

        var action = () => Client.CreateVolume(request);

        action.Should().Throw<RpcException>()
            .Where(e => e.StatusCode == StatusCode.InvalidArgument);
    }

    private CreateVolumeRequest BuildVolumeRequest(string? withName = null, long? withCapacity = null)
    {
        const long oneMb = 1024 * 1024;
        
        var request = new CreateVolumeRequest
        {
            Name = withName ?? Guid.NewGuid().ToString(),
            CapacityRange = new CapacityRange
            {
                LimitBytes = oneMb,
                RequiredBytes = oneMb
            }
        };
        
        return request;
    }
}