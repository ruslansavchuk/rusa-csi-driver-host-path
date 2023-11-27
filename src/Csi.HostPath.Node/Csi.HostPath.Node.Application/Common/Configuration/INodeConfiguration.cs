namespace Csi.HostPath.Node.Application.Common.Configuration;

public interface INodeConfiguration
{
    public string CsiDataDir { get; }
    public string NodeId { get; }
    public int? MaxVolumesPerNode { get; }
    public string ControllerEndpoint { get; }
}