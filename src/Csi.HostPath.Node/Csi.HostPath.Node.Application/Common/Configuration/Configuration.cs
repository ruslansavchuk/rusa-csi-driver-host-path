namespace Csi.HostPath.Node.Application.Common.Configuration;

public class Configuration
{
    public string PluginName { get; set; } = "hostpath.csi.k8s.io";
    public string NodeId { get; set; } = Guid.NewGuid().ToString();
    public int MaxVolumesPerNode { get; set; } = 1000;
}