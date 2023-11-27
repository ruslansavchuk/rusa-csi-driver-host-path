using Csi.HostPath.Node.Application.Common.Controller.Dtos;

namespace Csi.HostPath.Node.Application.Common.Controller;

public interface IVolumeController
{
    Task<List<Volume>> ListVolumes(CancellationToken cancellationToken);
    Task<Volume> Create(string volumeName, long capacity, CancellationToken cancellationToken);
    Task Delete(string volumeId, CancellationToken cancellationToken);
    Task Publish(string volumeId, string nodeId, CancellationToken cancellationToken);
    Task Unpublish(string volumeId, string nodeId, CancellationToken cancellationToken);
}