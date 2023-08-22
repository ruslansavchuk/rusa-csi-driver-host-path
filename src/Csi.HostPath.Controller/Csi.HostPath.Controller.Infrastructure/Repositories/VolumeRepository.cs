using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Csi.HostPath.Controller.Application.Common.Repositories;
using Csi.HostPath.Controller.Domain.Entities;
using Csi.HostPath.Controller.Infrastructure.Context;
using Csi.HostPath.Controller.Infrastructure.Context.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Csi.HostPath.Controller.Infrastructure.Repositories;

public class VolumeRepository : IVolumeRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public VolumeRepository(
        DataContext dataContext, 
        IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task Add(Volume volume)
    {
        var newVolume = _mapper.Map<VolumeDataModel>(volume);
        await _dataContext.Volumes.AddAsync(newVolume);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Volume> Get(string id)
    {
        var existingVolume = await GetInternal(id);
        return _mapper.Map<Volume>(existingVolume);
    }

    public Task<List<Volume>> Get(Expression<Func<Volume, bool>>? filter = null)
    {
        var volumes = _dataContext
            .Volumes
            .ProjectTo<Volume>(_mapper.ConfigurationProvider);

        if (filter != null)
        {
            volumes = volumes.Where(filter);
        }

        return volumes.ToListAsync();
    }

    public async Task Update(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);
        
        _mapper.Map(volume, existingVolume);
        _dataContext.Volumes.Update(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(Volume volume)
    {
        var existingVolume = await GetInternal(volume.Id);
        _dataContext.Volumes.Remove(existingVolume);
        await _dataContext.SaveChangesAsync();
    }

    private Task<VolumeDataModel> GetInternal(string id)
    {
        return _dataContext.Volumes.SingleAsync(i => i.VolumeId == id);
    }
}
