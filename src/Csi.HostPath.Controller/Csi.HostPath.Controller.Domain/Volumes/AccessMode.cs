namespace Csi.HostPath.Controller.Domain.Volumes;

public enum AccessMode
{
    Unknown = 0,
    SingleNodeWriter = 1,
    SingleNodeReaderOnly = 2,
    SingleNodeMultiWriter = 7
}