﻿// <auto-generated />
using Csi.HostPath.Controller.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Csi.HostPath.Controller.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231126094523_RemovaPathProperty")]
    partial class RemovaPathProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Csi.HostPath.Controller.Infrastructure.Context.Models.VolumeDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessMode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Attached")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ephemeral")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NodeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ReadOnlyAttach")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Volumes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
