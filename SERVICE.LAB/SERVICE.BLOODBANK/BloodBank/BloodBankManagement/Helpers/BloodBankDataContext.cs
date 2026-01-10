using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Entities;
using BloodBankManagement.Models;
using DEV.Common;
using DEV.Model.Integration;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagement.Helpers
{
    public class BloodBankDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public BloodBankDataContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
            if (BloodBankPatients == null) BloodBankPatients = Set<BloodBankPatient>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(Configuration.GetConnectionString("WebApiDatabase")), options =>
            {
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("BatchId").StartsAt(100).IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new BloodBankPatientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodBankRegistrationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RegisteredProductsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodBankBillingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RegisteredSpecialRequirementsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationTransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodSampleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodBankInventoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodSampleResultEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodSampleInventoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StandardPatinetReportEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StandardPatinetReportPrintEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BloodBankInventoryTransactionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BBTblReportMasterDetailsEntityConfiguration());

            modelBuilder.Ignore<Contracts.TestResponse>();
            modelBuilder.Entity<Contracts.TestResponse>().HasNoKey();
            modelBuilder.Ignore<Contracts.GroupResponse>();
            modelBuilder.Entity<Contracts.GroupResponse>().HasNoKey();
            modelBuilder.Ignore<Contracts.SubTestResponse>();
            modelBuilder.Entity<Contracts.SubTestResponse>().HasNoKey();
            modelBuilder.Ignore<Contracts.TestPickListResponse>();
            modelBuilder.Entity<Contracts.TestPickListResponse>().HasNoKey();
            modelBuilder.Ignore<Contracts.StandardPatinetReport>();
            modelBuilder.Entity<Contracts.StandardPatinetReport>().HasNoKey();
            modelBuilder.Ignore<Contracts.UpdateStandardPatientResponse>();
            modelBuilder.Entity<Contracts.UpdateStandardPatientResponse>().HasNoKey();
            modelBuilder.Ignore<Contracts.GetPatientReport>();
            modelBuilder.Entity<Contracts.GetPatientReport>().HasNoKey();
            modelBuilder.Entity<LabReportTestDetails>(entity =>
            {
                entity.HasNoKey();                
            });
            modelBuilder.Entity<labreportdetails>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<labresponsedetails>(entity =>
            {
                entity.HasNoKey();
                entity.Ignore("reportdetails");
            });
            modelBuilder.Entity<labtestdetails>(entity =>
            {
                entity.HasNoKey();
            });
        }

        public DbSet<BloodBankPatient> BloodBankPatients { get; set; }
        public DbSet<BloodBankRegistration> BloodBankRegistrations { get; set; }
        public DbSet<BloodBankBilling> BloodBankBillings { get; set; }
        public DbSet<RegisteredProduct> RegisteredProducts { get; set; }
        public DbSet<RegisteredSpecialRequirement> RegisteredSpecialRequirements { get; set; }

        public DbSet<RegistrationTransaction> RegistrationTransactions { get; set; }
        public DbSet<BloodSample> BloodSamples { get; set; }
        public DbSet<BloodBankInventory> BloodBankInventories { get; set; }
        public DbSet<BloodSampleResult> BloodSampleResults { get; set; }
        public DbSet<BloodSampleInventory> BloodSampleInventories { get; set; }

        //BELOW ONES ARE CONTRACTS DBSET WHICH DOESN"T CREATE ANY TABLES SCHEMA 
        public DbSet<Contracts.TestResponse> TestDetails { get; set; }
        public DbSet<Contracts.GroupResponse> GroupDetails { get; set; }
        public DbSet<Contracts.SubTestResponse> SubTestDetails { get; set; }
        public DbSet<Contracts.TestPickListResponse> TestPickListResponses { get; set; }
        public DbSet<Contracts.StandardPatinetReport> StandardPatientReport { get; set; }
        public DbSet<StandardPatinetReport> StandardPatientReportNew { get; set; }
        public DbSet<Contracts.UpdateStandardPatientResponse> UpdateStandardPatientReport { get; set; }
        public DbSet<GetPatientPrintReport> PatientReportPrint { get; set; }
        public DbSet<BloodBankInventoryTransaction> bloodBankInventoryTransactions { get; set; }
        public DbSet<BBTblReportMasterDetails> TblReportMaster { get; set; }

        public  DbSet<labresponsedetails> GetPDFReportDetails { get; set; }
        public  DbSet<labtestdetails> GetPDFReportTestDetails { get; set; }
        public  DbSet<LabReportTestDetails> GetDiscreetLabData { get; set; }
    }
}