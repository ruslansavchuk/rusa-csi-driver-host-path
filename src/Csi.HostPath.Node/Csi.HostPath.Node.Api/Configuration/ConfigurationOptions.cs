namespace Csi.HostPath.Node.Api.Configuration;

public class ConfigurationOptions
{
    public string UnixSocket { get; set; }
    public string CsiDataDir { get; set; }
    public string NodeId { get; set; }
}