namespace Csi.HostPath.Controller.Application.Common.Dto;

public enum AccessMode
{
    Unknown = 0,
    SingleNodeWriter = 1,
    SingleNodeReadOnly = 2,
    MultiNodeReaderOnly = 3,
    MultiNodeSingleWriter = 4,
    MultiNodeMultiWriter = 5,
    SingleNodeSingleWriter = 6,
    SingleNodeMultiWriter = 7
}