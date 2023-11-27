namespace Csi.HostPath.Node.Application.Common.Controller.Dtos;

public record Volume(
    string Id,
    string Name,
    Dictionary<string, string> Context);