using Csi.HostPath.Controller.Domain.Volumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csi.HostPath.Controller.Infrastructure.Context.Models;

public class VolumeDataModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Capacity { get; set; }
    public string? Path { get; set; }
    public AccessMode AccessMode { get; set; }
    public bool Ephemeral { get; set; }
    public string? NodeId { get; set; }
    public bool ReadOnlyAttach { get; set; }
    public bool Attached { get; set; }
}

public class VolumeConfig : IEntityTypeConfiguration<VolumeDataModel>
{
    public void Configure(EntityTypeBuilder<VolumeDataModel> builder)
    {
        builder.ToTable("Volumes");

        builder.HasKey(v => v.Id);

        builder
            .Property(v => v.Name)
            .IsRequired();

        builder.HasIndex(v => v.Name).IsUnique();
    }
}