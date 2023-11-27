namespace Csi.HostPath.Controller.Domain.Volumes;

public enum AccessMode
{
    Unknown = 0,
    SingleNodeWriter = 1,
    SingleNodeReaderOnly = 2,
    SingleNodeSingleWriter = 6,
    SingleNodeMultiWriter = 7
}