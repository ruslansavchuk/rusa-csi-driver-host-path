using Csi.HostPath.Controller.Domain.Volumes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csi.HostPath.Controller.Infrastructure.Context.Configs;

public class VolumeConfig : IEntityTypeConfiguration<Volume>
{
    public void Configure(EntityTypeBuilder<Volume> builder)
    {
        builder.ToTable("Volumes");

        builder.HasKey(v => v.Id);

        builder
            .Property(v => v.Name)
            .IsRequired();

        builder
            .HasIndex(v => v.Name)
            .IsUnique();
    }
}
