using Csi.HostPath.Node.Api.Configuration;
using Csi.HostPath.Node.Api.Utils;
using Microsoft.Extensions.Options;

namespace Csi.HostPath.Node.Api.Services.Node;

public partial class NodeService : Csi.V1.Node.NodeBase
{
    private readonly Mounter _mounter;
    private readonly IOptions<ConfigurationOptions> _options;
    
    public NodeService(Mounter mounter, IOptions<ConfigurationOptions> options)
    {
        _mounter = mounter;
        _options = options;
    }
}