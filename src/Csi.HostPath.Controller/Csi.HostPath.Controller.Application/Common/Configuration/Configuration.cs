using Csi.HostPath.Controller.Domain.Common.Size;
using Csi.HostPath.Controller.Domain.Volumes;

namespace Csi.HostPath.Controller.Application.Common.Configuration;

public class Configuration
{
    public string PluginName { get; set; } = "hostpath.csi.k8s.io";
    public long MaxVolumeSize { get; set; } = (long)FileSizeUnit.Gb;
    public long MinVolumeSize { get; set; } = 4 * (long) FileSizeUnit.Kb;
    public long Capacity { get; set; } = 100 * (long) FileSizeUnit.Gb;
}