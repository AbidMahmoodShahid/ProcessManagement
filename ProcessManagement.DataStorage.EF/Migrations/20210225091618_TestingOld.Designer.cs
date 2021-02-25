﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessManagement.DataStorage.EF;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    [DbContext(typeof(PMDataContext))]
    [Migration("20210225091618_TestingOld")]
    partial class TestingOld
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Process.Editor.Elements.ProcessGroupModel", b =>
                {
                    b.Property<int>("ProcessGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessModelId")
                        .HasColumnType("int");

                    b.Property<int>("SortingNumber")
                        .HasColumnType("int");

                    b.HasKey("ProcessGroupId");

                    b.HasIndex("ProcessModelId");

                    b.ToTable("ProcessGroup");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessModel", b =>
                {
                    b.Property<int>("ProcessModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcessModelId");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPoint", b =>
                {
                    b.Property<int>("ProcessPointID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessGroupModelId")
                        .HasColumnType("int");

                    b.Property<string>("ProcessPointTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortingNumber")
                        .HasColumnType("int");

                    b.HasKey("ProcessPointID");

                    b.HasIndex("ProcessGroupModelId");

                    b.ToTable("ProcessPoint");

                    b.HasDiscriminator<string>("ProcessPointTypeName").HasValue("ProcessPoint");
                });

            modelBuilder.Entity("Process.Simulation.Elements.SimulationModel", b =>
                {
                    b.Property<int>("SimulationModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deactivated")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsUnderProcess")
                        .HasColumnType("bit");

                    b.Property<string>("ProcessPointId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessPointName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SimulationError")
                        .HasColumnType("bit");

                    b.Property<string>("SimulationStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortingNumber")
                        .HasColumnType("int");

                    b.Property<string>("SuccessPercentage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SimulationModelId");

                    b.ToTable("SimulationModel");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPointA", b =>
                {
                    b.HasBaseType("Process.Editor.Elements.ProcessPoint");

                    b.HasDiscriminator().HasValue("ProcessPointA");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPointB", b =>
                {
                    b.HasBaseType("Process.Editor.Elements.ProcessPoint");

                    b.HasDiscriminator().HasValue("ProcessPointB");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPointC", b =>
                {
                    b.HasBaseType("Process.Editor.Elements.ProcessPoint");

                    b.HasDiscriminator().HasValue("ProcessPointC");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPointD", b =>
                {
                    b.HasBaseType("Process.Editor.Elements.ProcessPoint");

                    b.HasDiscriminator().HasValue("ProcessPointD");
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessGroupModel", b =>
                {
                    b.HasOne("Process.Editor.Elements.ProcessModel", "ProcessModel")
                        .WithMany("ItemCollection")
                        .HasForeignKey("ProcessModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Process.Editor.Elements.ProcessPoint", b =>
                {
                    b.HasOne("Process.Editor.Elements.ProcessGroupModel", "ProcessGroupModel")
                        .WithMany("ItemCollection")
                        .HasForeignKey("ProcessGroupModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
