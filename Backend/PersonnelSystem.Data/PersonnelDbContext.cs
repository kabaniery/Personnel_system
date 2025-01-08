using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonnelSystem.Data.Entities;

namespace PersonnelSystem.Data
{
    public class PersonnelDbContext : DbContext
    {
        public PersonnelDbContext(DbContextOptions<PersonnelDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubdivisionEntity>()
                .HasOne(s => s.Header)
                .WithMany(s => s.Childs)
                .HasForeignKey(s => s.HeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(e => e.Subdivision)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.SubdivisionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DateWorkedEntity>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.Dates)
                .HasForeignKey(d => d.EmployeeId);

            modelBuilder.Entity<DateWorkedEntity>()
                .HasOne(d => d.Subdivision)
                .WithMany(s => s.Dates)
                .HasForeignKey(d => d.SubdivisionId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<SubdivisionEntity> Subdivisions { get; set; }
        public DbSet<DateWorkedEntity> Dates { get; set; }
    }
}
