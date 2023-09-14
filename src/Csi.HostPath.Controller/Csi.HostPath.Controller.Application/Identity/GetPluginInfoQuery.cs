using Csi.HostPath.Controller.Application.Common.Configuration;
using MediatR;
using Microsoft.Extensions.Options;

namespace Csi.HostPath.Controller.Application.Identity;

public record PluginInfo(string Name, string Version);

public record GetPluginInfoQuery : IRequest<PluginInfo>;

public class GetPluginInfoQueryHandler : IRequestHandler<GetPluginInfoQuery, PluginInfo>
{
    private readonly IOptions<Configuration> _options;

    public GetPluginInfoQueryHandler(IOptions<Configuration> options)
    {
        _options = options;
    }

    public Task<PluginInfo> Handle(GetPluginInfoQuery request, CancellationToken cancellationToken)
    {
        var config = _options.Value!;
        var buildVersion = typeof(GetPluginInfoQuery)
            .Assembly
            .GetName()
            .Version!
            .ToString();
        
        return Task.FromResult(new PluginInfo(config.PluginName, buildVersion));
    }
}