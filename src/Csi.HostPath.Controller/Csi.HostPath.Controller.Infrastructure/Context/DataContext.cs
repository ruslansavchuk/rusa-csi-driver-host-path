using Csi.HostPath.Controller.Infrastructure.Context.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Csi.HostPath.Controller.Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }
 
    // #region For Migration Scaffold
    //
    // public DataContext()
    // {
    //
    // }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite(
    //         "Filename=./data.db");
    // }
    //
    // #endregion
    
    public DbSet<VolumeDataModel> Volumes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}