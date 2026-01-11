using System;
using DEV.Common;
using Service.Model.FrontOffice.PatientDue;
using Service.Model.PatientInfo;
using Service.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Service.Model.EF
{
    public partial class InsuranceContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public InsuranceContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public InsuranceContext(DbContextOptions<InsuranceContext> options)
            : base(options)
        {
        }
        public virtual DbSet<NetworkMasterDTO> GetNetworkMasters { get; set; }
        public virtual DbSet<NetworkMasterDTOResponse> InsertNetworkMaster { get; set; }

        public virtual DbSet<CompanyMasterDTO> GetCompanyMaster { get; set; }
        public virtual DbSet<CompanyMasterDTOResponse> InsertCompanyMaster { get; set; }
        public virtual DbSet<DeductionDTOResponse> InsertDeductionMaster { get; set; }
        public virtual DbSet<Deductionresult> GetDeductionMaster { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(_connectionstring));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<NetworkMasterDTO>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetInsuranceNetwork");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<NetworkMasterDTOResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertInsuranceNetwork");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<CompanyMasterDTO>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetInsuranceCompany");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CompanyMasterDTOResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertInsuranceCompany");
                entity.Property(e => e.result).HasColumnName("result");
            });


            modelBuilder.Entity<DeductionDTOResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertInsuranceDeduction");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<Deductionresult>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetInsuranceDeductionDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
            
        }

    }


}
