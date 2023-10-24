using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using Csi.HostPath.Controller.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Csi.HostPath.Controller.Infrastructure.Repositories;

public class VolumeRepository : IVolumeRepository
{
    private readonly DataContext _dataContext;

    public VolumeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Add(Volume volume)
    {
        await _dataContext.Volumes.AddAsync(volume);
        await _dataContext.SaveChangesAsync();
    }

    public Task<Volume> Get(int id)
    {
        return GetInternal(id);
    }

    public Task<List<Volume>> Get(Expression<Func<Volume, bool>>? filter = null)
    {
        var volumes = _dataContext
            .Volumes
            .AsQueryable();

        if (filter != null)
        {
            volumes = volumes.Where(filter);
        }

        return volumes.ToListAsync();
    }

    public async Task Update(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);

        // todo: think what to do with updates
        // existingVolume.Attached = volume.Attached;
        
        _dataContext.Volumes.Update(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);
        _dataContext.Volumes.Remove(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    private Task<Volume> GetInternal(int id)
    {
        return _dataContext.Volumes.SingleAsync(i => i.Id == id);
    }
}
