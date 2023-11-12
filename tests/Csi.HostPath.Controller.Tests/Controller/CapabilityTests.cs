using Csi.V1;
using FluentAssertions;

namespace Csi.HostPath.Controller.Tests.Controller;

public class CapabilityTests : ControllerTestsBase
{
    [Fact]
    public void GetControllerCapabilities()
    {
        var request = new ControllerGetCapabilitiesRequest();
        var response = GetCapabilities(request)();

        var capabilities = response.Capabilities.Select(c => c.Rpc.Type);

        capabilities
            .Should().Contain(c => c == ControllerServiceCapability.Types.RPC.Types.Type.CreateDeleteVolume)
            .And.Contain(c => c == ControllerServiceCapability.Types.RPC.Types.Type.PublishUnpublishVolume)
            .And.Contain(c => c == ControllerServiceCapability.Types.RPC.Types.Type.ListVolumes)
            .And.Contain(c => c == ControllerServiceCapability.Types.RPC.Types.Type.GetCapacity)
            .And.Contain(c => c == ControllerServiceCapability.Types.RPC.Types.Type.ExpandVolume);
    }
}