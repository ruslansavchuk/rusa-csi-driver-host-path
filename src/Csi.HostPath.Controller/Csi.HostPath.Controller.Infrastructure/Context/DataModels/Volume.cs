using AutoMapper;
using Csi.HostPath.Controller.Domain.Entities;
using Csi.HostPath.Controller.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csi.HostPath.Controller.Infrastructure.Context.DataModels;

public class VolumeDataModel
{
    public string VolumeId { get; init; }
    public string Name { get; init; }
    public long Size { get; init; }
    public string? Path { get; set; }
    public AccessType AccessType { get; init; }
    public string? ParentVolumeId { get; set; }
    public string? ParentSnapshotId { get; set; }
    public bool Ephemeral { get; init; }
    public string? NodeId { get; set; }
    public bool ReadOnlyAttach { get; init; }
    public bool Attached { get; init; }
}

public class VolumeConfig : IEntityTypeConfiguration<VolumeDataModel>
{
    public void Configure(EntityTypeBuilder<VolumeDataModel> builder)
    {
        builder.ToTable("Volumes");

        builder.HasKey(v => v.VolumeId);

        builder
            .Property(v => v.Name)
            .IsRequired();

        builder
            .HasIndex(v => v.Name)
            .IsUnique();
    }
}

public class VolumeMappingProfile : Profile
{
    public VolumeMappingProfile()
    {
        CreateMap<Volume, VolumeDataModel>().ForMember(v => v.VolumeId, opt => opt.MapFrom(i => i.Id));
        CreateMap<VolumeDataModel, Volume>().ForMember(v => v.Id, opt => opt.MapFrom(i => i.VolumeId));
    }
}