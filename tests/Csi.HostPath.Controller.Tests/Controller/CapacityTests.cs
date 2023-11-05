using Csi.V1;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CapacityTests : ControllerTestsBase
{
    [Fact]
    public async Task GetControllerCapacity()
    {
        var request = new GetCapacityRequest();
        var response = Client.GetCapacityAsync(request);
    }
}