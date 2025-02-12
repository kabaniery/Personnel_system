﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonnelSystem.Data;

#nullable disable

namespace PersonnelSystem.Data.Migrations
{
    [DbContext(typeof(PersonnelDbContext))]
    [Migration("20250105190219_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<int?>("HeaderId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HeaderId");

                    b.ToTable("Subdivisions");
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

            modelBuilder.Entity("PersonnelSystem.Data.Entities.SubdivisionEntity", b =>
                {
                    b.Navigation("Childs");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
