using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Volumes;
using Csi.HostPath.Controller.Infrastructure.Context;
using Csi.HostPath.Controller.Infrastructure.Context.Models;
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
        var newVolume = new VolumeDataModel
        {
            Name = volume.Name,
            Capacity = volume.Capacity,
            Ephemeral = volume.Ephemeral,
            AccessMode = volume.AccessMode,
            NodeId = volume.NodeId,
            ReadOnlyAttach = volume.ReadOnlyAttach,
            Attached = volume.Attached
        };
        await _dataContext.Volumes.AddAsync(newVolume);
        await _dataContext.SaveChangesAsync();

        volume.SetId(newVolume.Id);
    }

    public async Task<Volume> Get(int id)
    {
        var volume = await GetInternal(id);
        return Volume.Restore(
            volume.Id,
            volume.Name,
            volume.Capacity,
            volume.Attached,
            volume.Ephemeral,
            volume.AccessMode,
            volume.NodeId,
            volume.ReadOnlyAttach);
    }

    public async Task<List<Volume>> Get(string? name = null, int? getAfterId = null)
    {
        var query = _dataContext.Volumes.AsQueryable();

        if (getAfterId.HasValue)
        {
            query = query.Where(v => v.Id > getAfterId);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(v => v.Name == name);
        }

        var volumes = await query.ToListAsync();

        return volumes
            .Select(v => Volume.Restore(v.Id, v.Name, v.Capacity, v.Attached, v.Ephemeral, v.AccessMode, v.NodeId, v.ReadOnlyAttach))
            .ToList();
    }

    public async Task Update(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);

        existingVolume.Attached = volume.Attached;
        existingVolume.Capacity = volume.Capacity;
        existingVolume.Ephemeral = volume.Ephemeral;
        existingVolume.AccessMode = volume.AccessMode;
        existingVolume.ReadOnlyAttach = volume.ReadOnlyAttach;
        existingVolume.Name = volume.Name;
        existingVolume.NodeId = volume.NodeId;
        
        _dataContext.Volumes.Update(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);
        _dataContext.Volumes.Remove(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    private Task<VolumeDataModel> GetInternal(int id)
    {
        return _dataContext.Volumes.SingleAsync(i => i.Id == id);
    }
}
