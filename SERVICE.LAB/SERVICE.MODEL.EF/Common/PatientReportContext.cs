using System;
using DEV.Common;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEV.Model.EF
{
    public partial class PatientReportContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PatientReportContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PatientReportContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<lstpatientreportdbl> GetPatientReport { get; set; }
        public virtual DbSet<TblCsatransaction> TblCsatransaction { get; set; }
        public virtual DbSet<CsaResponse> InsertCsatransaction { get; set; }
        public virtual DbSet<PatientReportLog> InsertReportLog { get; set; }
        public virtual DbSet<PatientReportLogRespose> GetReportLog { get; set; }
        public virtual DbSet<PatientDataImpressionResponse> GetPatientImpressionListReport { get; set; }
        public virtual DbSet<FetchAuditReportRes> GetAuditTrailReport { get; set; }
        public virtual DbSet<GetAuditTrailVisitHistory> GetAuditTrailVisitHistory { get; set; }
        public virtual DbSet<lstamendedpatientreportdbl> GetAmendedPatientReport { get; set; }
        public virtual DbSet<GetATSubCatyMasterSearchResponse> GetATSubCatyMastersData { get; set; }

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

            modelBuilder.Entity<lstpatientreportdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_PatientReport");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<TblCsatransaction>(entity =>
            {
                entity.HasKey(e => e.CsatransactionNo);
                entity.ToTable("Pro_CsaTransactionDetails");
                entity.Property(e => e.CsatransactionNo).HasColumnName("CsatransactionNo");
            });

            modelBuilder.Entity<CsaResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertCSAAcknowledgement");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<PatientReportLog>(entity =>
            {
                entity.HasKey(e => e.ReportLogNo);
                entity.ToTable("Pro_InsertPatientReportLog");
                entity.Property(e => e.ReportLogNo).HasColumnName("ReportLogNo");
            });

            modelBuilder.Entity<PatientReportLogRespose>(entity =>
            {
                entity.HasKey(e => e.LogNo);
                entity.ToTable("Pro_GetPatientReportLog");
                entity.Property(e => e.LogNo).HasColumnName("LogNo");
            });

            modelBuilder.Entity<PatientDataImpressionResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetPatientSearchImpressionReport");
                entity.Property(e => e.Row_Num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<FetchAuditReportRes>(entity =>
            {
                entity.HasKey(e => e.RowNum);
                entity.ToTable("Pro_GetAuditTrailLogs");
                entity.Property(e => e.RowNum).HasColumnName("RowNum");
            });

            modelBuilder.Entity<GetAuditTrailVisitHistory>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_getPatientVisitNoHistory");
                entity.Property(e => e.PatientVisitNo).HasColumnName("PatientVisitNo");
            });

            modelBuilder.Entity<lstamendedpatientreportdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_AmendedPatientReport");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<GetATSubCatyMasterSearchResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("Pro_GetATSubCatyMastersData");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
        }
    }
}
