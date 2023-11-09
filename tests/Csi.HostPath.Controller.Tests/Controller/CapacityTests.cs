using Csi.V1;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CapacityTests : ControllerTestsBase
{
    [Fact]
    public void GetControllerCapacity()
    {
        var request = new GetCapacityRequest();
        var response = GetCapacity(request);
    }
}