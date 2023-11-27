namespace Csi.HostPath.Node.Application.Common.Controller.Dtos;

public enum AccessMode
{
    Unknown = 0,
    SingleNodeWriter = 1,
    SingleNodeReaderOnly = 2,
    SingleNodeMultiWriter = 7
}