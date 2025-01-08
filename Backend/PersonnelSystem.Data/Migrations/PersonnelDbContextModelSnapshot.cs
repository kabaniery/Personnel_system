﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonnelSystem.Data;

#nullable disable

namespace PersonnelSystem.Data.Migrations
{
    [DbContext(typeof(PersonnelDbContext))]
    partial class PersonnelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonnelSystem.Data.Entities.DateWorkedEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("SubdivisionId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TimeFinished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TimeStarted")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("Dates");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SubdivisionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.SubdivisionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("HeaderId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HeaderId");

                    b.ToTable("Subdivisions");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.DateWorkedEntity", b =>
                {
                    b.HasOne("PersonnelSystem.Data.Entities.EmployeeEntity", "Employee")
                        .WithMany("Dates")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonnelSystem.Data.Entities.SubdivisionEntity", "Subdivision")
                        .WithMany("Dates")
                        .HasForeignKey("SubdivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("PersonnelSystem.Data.Entities.SubdivisionEntity", "Subdivision")
                        .WithMany("Employees")
                        .HasForeignKey("SubdivisionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.SubdivisionEntity", b =>
                {
                    b.HasOne("PersonnelSystem.Data.Entities.SubdivisionEntity", "Header")
                        .WithMany("Childs")
                        .HasForeignKey("HeaderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Header");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("Dates");
                });

            modelBuilder.Entity("PersonnelSystem.Data.Entities.SubdivisionEntity", b =>
                {
                    b.Navigation("Childs");

                    b.Navigation("Dates");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
