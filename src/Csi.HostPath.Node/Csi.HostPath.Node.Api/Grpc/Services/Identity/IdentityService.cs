using Csi.HostPath.Node.Application.Identity;
using Csi.V1;
using Grpc.Core;
using MediatR;
using PluginCapability = Csi.HostPath.Node.Application.Identity.PluginCapability;

namespace Csi.HostPath.Node.Api.Grpc.Services.Identity;

public class IdentityService : Csi.V1.Identity.IdentityBase
{
    private readonly IMediator _mediator;

    public IdentityService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override Task<ProbeResponse> Probe(ProbeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ProbeResponse());
    }

    public override async Task<GetPluginInfoResponse> GetPluginInfo(GetPluginInfoRequest request, ServerCallContext context)
    {
        var pluginInfo = await _mediator.Send(new GetPluginInfoQuery());
        return ToResponse(pluginInfo);
    }

    private static GetPluginInfoResponse ToResponse(PluginInfo pluginInfo)
    {
        return new GetPluginInfoResponse
        {
            Name = pluginInfo.Name,
            VendorVersion = pluginInfo.Version
        };
    }

    public override async Task<GetPluginCapabilitiesResponse> GetPluginCapabilities(GetPluginCapabilitiesRequest request, ServerCallContext context)
    {
        var capabilities = await _mediator.Send(new GetPluginCapabilitiesQuery(), context.CancellationToken); 
        return ToResponse(capabilities);
    }

    private GetPluginCapabilitiesResponse ToResponse(List<PluginCapability> capabilities)
    {
        return new GetPluginCapabilitiesResponse
        {
            Capabilities = { new V1.PluginCapability() }
        };
    }
}