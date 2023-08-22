using Csi.HostPath.Controller.Domain.Common;
using Csi.HostPath.Controller.Domain.Enums;

namespace Csi.HostPath.Controller.Domain.Entities;

public class Volume : EntityBase
{
    public string Name { get; init; }
    public long Size { get; init; }
    public string? Path { get; set; }
    public AccessType AccessType { get; init; }
    public bool Ephemeral { get; set; }
    public string? NodeId { get; set; }
    public bool ReadOnlyAttach { get; init; }
    public bool Attached { get; init; }
}