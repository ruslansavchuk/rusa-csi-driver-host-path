using Csi.HostPath.Controller.Tests.Utils;
using Csi.V1;
using FluentAssertions;
using Grpc.Net.Client;

namespace Csi.HostPath.Controller.Tests.Identity;

public class IdentityTests
{
    private readonly V1.Identity.IdentityClient _client;
    
    public IdentityTests()
    {
        var chanel = GrpcChannel.ForAddress(TestConfig.ConnectionString);
        _client = new V1.Identity.IdentityClient(chanel);
    }
    
    [Fact]
    public async Task TestProbe()
    {
        var request = new ProbeRequest();
        var response = await _client.ProbeAsync(request);

        response.Ready.Should().BeTrue();
    }
    
    [Fact]
    public async Task TestGetPluginInfo()
    {
        var request = new GetPluginInfoRequest();
        var response = await _client.GetPluginInfoAsync(request);

        response.Name.Should().BeEquivalentTo("hostpath.csi.k8s.io");
    }
    
    [Fact]
    public async Task TestGetPluginCapabilities()
    {
        var request = new GetPluginCapabilitiesRequest();
        var response = await _client.GetPluginCapabilitiesAsync(request);

        response.Capabilities.Select(c => c.Service.Type)
            .Should().Contain(i => i == PluginCapability.Types.Service.Types.Type.ControllerService);
    }
}