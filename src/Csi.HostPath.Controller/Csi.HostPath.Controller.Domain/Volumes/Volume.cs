using Csi.HostPath.Controller.Domain.Common;
using Csi.HostPath.Controller.Domain.Common.Size;

namespace Csi.HostPath.Controller.Domain.Volumes;

public class Volume : EntityBase
{
    public string Name { get; private set; }
    public Size Capacity { get; private set; }
    public string? Path { get; private set; }
    public AccessType AccessType { get; private set; }
    public bool Ephemeral { get; private set; }
    public string? NodeId { get; private set; }
    public bool ReadOnlyAttach { get; private set; }
    public bool Attached { get; private set; }

    private Volume(string name, Size? capacity, bool attached, bool ephemeral, AccessType accessType,
        string? path, string? nodeId, bool readOnlyAttach)
    {
        Name = name ?? throw new ArgumentException("name is required", nameof(name));
        Capacity = capacity != null && capacity > 0
            ? capacity
            : throw new ArgumentException("size should be bigger than 0", nameof(capacity));
        AccessType = accessType == AccessType.Mount
            ? accessType
            : throw new Exceptions.ArgumentException("only access type mount supported", nameof(accessType));
        Path = path;
        NodeId = nodeId;
        Attached = attached;
        Ephemeral = ephemeral;
        ReadOnlyAttach = readOnlyAttach;
    }

    public void SetNode(string? nodeId)
    {
        NodeId = nodeId;
    }

    public static Volume Create(string name, Size? capacity, bool attached, bool ephemeral, AccessType accessType, 
        string? path, string? nodeId, bool readOnlyAttach)
    {
        return new Volume(name, capacity, attached, ephemeral, accessType, path, nodeId, readOnlyAttach);
    }

    public static Volume Restore(string name, Size? capacity, bool attached, bool ephemeral, AccessType accessType,
        string? path, string? nodeId, bool readOnlyAttach)
    {
        return new Volume(name, capacity, attached, ephemeral, accessType, path, nodeId, readOnlyAttach);
    }
}