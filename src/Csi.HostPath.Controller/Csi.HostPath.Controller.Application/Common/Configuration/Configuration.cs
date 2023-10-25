using Csi.HostPath.Controller.Domain.Common.Size;

namespace Csi.HostPath.Controller.Application.Common.Configuration;

public class Configuration
{
    public long MaxVolumeSize { get; set; } = (long)FileSizeUnit.Gb;
    public long MinVolumeSize { get; set; } = 4 * (long) FileSizeUnit.Kb;
    public long Capacity { get; set; } = 100 * (long) FileSizeUnit.Gb;
}