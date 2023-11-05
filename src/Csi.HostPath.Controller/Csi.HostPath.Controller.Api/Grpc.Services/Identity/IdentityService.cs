using Csi.HostPath.Controller.Application.Identity;
using Csi.V1;
using Grpc.Core;
using MediatR;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Identity;

public class IdentityService : V1.Identity.IdentityBase
{
    private readonly IMediator _mediator;

    public IdentityService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override Task<ProbeResponse> Probe(ProbeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ProbeResponse { Ready = true });
    }

    public override async Task<GetPluginInfoResponse> GetPluginInfo(GetPluginInfoRequest request, ServerCallContext context)
    {
        var pluginInfo = await _mediator.Send(new GetPluginInfoQuery());
        
        return new GetPluginInfoResponse
        {
            Name = pluginInfo.Name,
            VendorVersion = pluginInfo.Version
        };
    }

    public override async Task<GetPluginCapabilitiesResponse> GetPluginCapabilities(GetPluginCapabilitiesRequest request, ServerCallContext context)
    {
        var capabilities = await _mediator.Send(new GetPluginCapabilitiesQuery());
        return ToResponse(capabilities);
    }

    private GetPluginCapabilitiesResponse ToResponse(List<Application.Common.Dto.PluginCapability> capabilities)
    {
        var response = new GetPluginCapabilitiesResponse();

        foreach (var capability in capabilities)
        {
            response.Capabilities.Add(new PluginCapability
            {
                Service = new PluginCapability.Types.Service
                {
                    Type = (PluginCapability.Types.Service.Types.Type)capability
                }
            });
        }

        return response;
    }
}
