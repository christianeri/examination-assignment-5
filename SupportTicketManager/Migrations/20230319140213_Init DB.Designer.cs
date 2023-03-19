﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupportTicketManager.Contexts;

#nullable disable

namespace SupportTicketManager.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230319140213_Init DB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SupportTicketManager.Models.Entities.BuildingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuildingName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PropertyCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Buildings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BuildingName = "Blåsenhus",
                            PropertyCode = "5:1"
                        },
                        new
                        {
                            Id = 2,
                            BuildingName = "Carolina Rediviva",
                            PropertyCode = "1:68"
                        },
                        new
                        {
                            Id = 3,
                            BuildingName = "Ekonomikum",
                            PropertyCode = "62:8"
                        },
                        new
                        {
                            Id = 4,
                            BuildingName = "Rudbecklaboratoriet",
                            PropertyCode = "1:23"
                        },
                        new
                        {
                            Id = 5,
                            BuildingName = "Ångströmlaboratoriet",
                            PropertyCode = "7:1"
                        });
                });

            modelBuilder.Entity("SupportTicketManager.Models.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CustomerFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerLastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("char(13)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerEmail")
                        .IsUnique()
                        .HasFilter("[CustomerEmail] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SupportTicketManager.Models.Entities.StatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TicketStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TicketStatus = "Ej Påbörjad"
                        },
                        new
                        {
                            Id = 2,
                            TicketStatus = "Pågående"
                        },
                        new
                        {
                            Id = 3,
                            TicketStatus = "Avslutad"
                        });
                });

            modelBuilder.Entity("SupportTicketManager.Models.Entities.TicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("TicketComment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("TicketCommentUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TicketCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("TicketDescription")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketReference")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("TicketTitle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TicketReference")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("SupportTicketManager.Models.Entities.TicketEntity", b =>
                {
                    b.HasOne("SupportTicketManager.Models.Entities.BuildingEntity", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupportTicketManager.Models.Entities.CustomerEntity", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupportTicketManager.Models.Entities.StatusEntity", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Customer");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
