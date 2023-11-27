using Csi.HostPath.Node.Application.Common.Configuration;

namespace Csi.HostPath.Node.Api.Configuration;

public class ConfigurationOptions : INodeConfiguration
{
    public string UnixSocket { get; set; }
    public string CsiDataDir { get; set; }
    public string NodeId { get; set; }
    public int? MaxVolumesPerNode { get; set; }
    public string ControllerEndpoint { get; set; }
}