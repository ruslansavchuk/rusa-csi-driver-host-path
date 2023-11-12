using Csi.V1;
using FluentAssertions;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CapacityTests : ControllerTestsBase
{
    [Fact]
    public void GetControllerCapacity()
    {
        var request = new GetCapacityRequest();
        GetCapacity(request)
            .Should()
            .NotThrow();
    }
}