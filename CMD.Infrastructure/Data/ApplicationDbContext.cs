using CMD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<HealthCondition> HealthConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<PatientAddress>(pa => pa.PatientAddressId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.HealthConditions)
                .WithOne(hc => hc.Patient)
                .HasForeignKey(hc => hc.PatientId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
           .EnableSensitiveDataLogging();
        }
    }
}
