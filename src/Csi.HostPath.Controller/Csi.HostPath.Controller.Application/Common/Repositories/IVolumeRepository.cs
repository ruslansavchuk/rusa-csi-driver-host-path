using Csi.HostPath.Controller.Domain.Volumes;

namespace Csi.HostPath.Controller.Application.Common.Repositories;

public interface IVolumeRepository
{
    Task Add(Volume volume);
    Task<Volume> Get(int id);
    Task<List<Volume>> Get(string? name = null, int? getAfterId = null);
    Task Update(Volume volume);
    Task Delete(Volume volume);
}