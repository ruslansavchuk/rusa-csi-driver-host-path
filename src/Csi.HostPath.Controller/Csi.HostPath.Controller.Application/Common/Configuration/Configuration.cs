using Csi.HostPath.Controller.Domain.Enums;

namespace Csi.HostPath.Controller.Application.Common.Configuration;

public class Configuration
{
    public string PluginName { get; set; } = "rusa-csi-driver-host-path";
    public long MaxVolumeSize { get; set; } = (long)FileSizeUnit.Gb;
    public long MinVolumeSize { get; set; } = 4 * (long) FileSizeUnit.Kb;
    public long Capacity { get; set; } = 100 * (long) FileSizeUnit.Gb;
}