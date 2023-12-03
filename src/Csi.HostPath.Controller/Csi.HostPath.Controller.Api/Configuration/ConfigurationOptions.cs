namespace Csi.HostPath.Controller.Api.Configuration;

public class ConfigurationOptions
{
    public int? ListeningPort { get; set; }
    public string UnixSocket { get; set; }
    public string DbPath { get; set; }
}