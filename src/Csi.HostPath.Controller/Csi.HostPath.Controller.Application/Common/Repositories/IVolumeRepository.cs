using System.Linq.Expressions;
using Csi.HostPath.Controller.Domain.Entities;

namespace Csi.HostPath.Controller.Application.Common.Repositories;

public interface IVolumeRepository
{
    Task Add(Volume volume);
    Task<Volume> Get(string id);
    Task<List<Volume>> Get(Expression<Func<Volume, bool>>? filter = null);
    Task Update(Volume volume);
    Task Delete(Volume volume);
}