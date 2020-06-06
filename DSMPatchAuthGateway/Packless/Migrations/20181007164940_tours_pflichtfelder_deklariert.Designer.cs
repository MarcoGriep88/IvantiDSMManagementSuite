﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Packless.Database;

namespace Packless.Migrations
{
    [DbContext(typeof(PacklessDbContext))]
    [Migration("20181007164940_tours_pflichtfelder_deklariert")]
    partial class tours_pflichtfelder_deklariert
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Packless.Models.GPXFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int?>("TourRouteId");

                    b.Property<string>("UploadedFilePath")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("GPXFiles");
                });

            modelBuilder.Entity("Packless.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("RandomAccessKey")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("TourDescription");

                    b.Property<int?>("TourRouteId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TourRouteId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Packless.Models.TourRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("GPXFileId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("RouteDescription");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TourId");

                    b.HasKey("Id");

                    b.HasIndex("GPXFileId");

                    b.HasIndex("TourId");

                    b.ToTable("TourRoutes");
                });

            modelBuilder.Entity("Packless.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Packless.Models.Tour", b =>
                {
                    b.HasOne("Packless.Models.TourRoute")
                        .WithMany("Tours")
                        .HasForeignKey("TourRouteId");
                });

            modelBuilder.Entity("Packless.Models.TourRoute", b =>
                {
                    b.HasOne("Packless.Models.GPXFile", "GPXFile")
                        .WithMany("TourRoutes")
                        .HasForeignKey("GPXFileId");

                    b.HasOne("Packless.Models.Tour")
                        .WithMany("TourRoutes")
                        .HasForeignKey("TourId");
                });
#pragma warning restore 612, 618
        }
    }
}
