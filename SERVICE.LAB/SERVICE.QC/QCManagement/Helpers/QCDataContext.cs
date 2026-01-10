using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEV.Common;
using Microsoft.EntityFrameworkCore;
using QCManagement.Contracts;
using QCManagement.Entities;
using QCManagement.EntityConfigurations;
using QCManagement.Models;

namespace QC.Helpers
{
    public class QCDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public QCDataContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(Configuration.GetConnectionString("WebApiDatabase")), options =>
            {
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("QCBatchId").StartsAt(100).IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new ControlMasterEntityConfiguration());
            modelBuilder.ApplyConfiguration(new QCResultEntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SchedulerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TestControlSamplesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReagentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MediaInventoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StrainMasterEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StrainInventoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MicroQCMasterEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StrainMediaMappingEntityConfiguration());
        }


        public DbSet<ControlMaster> ControlMasters { get; set; }
        public DbSet<TestControlSamples> TestControlSamples { get; set; }
        public DbSet<QCResultEntry> QCResultEntries { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<MediaInventory> MediaInventories { get; set; }
        public DbSet<StrainMaster> StrainMasters { get; set; }
        public DbSet<StrainInventory> StrainInventories { get; set; }
        public DbSet<StrainMediaMapping> StrainMediaMappings { get; set; }
        public DbSet<MicroQCMaster> MicroQCMasters { get; set; }
    }
}
