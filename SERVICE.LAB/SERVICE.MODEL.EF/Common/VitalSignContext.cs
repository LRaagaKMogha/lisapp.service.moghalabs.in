using System;
using System.Data;
using DEV.Common;
using DEV.Model.FrontOffice.PatientDue;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DEV.Model.EF
{
    public partial class VitalSignContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public VitalSignContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public VitalSignContext(DbContextOptions<VitalSignContext> options)
            : base(options)
        {
        }
       
        public virtual DbSet<VitalSignDTO> GetVitalSignList { get; set; }
        public virtual DbSet<SaveVitalSignDTOResponse> SaveVitalSign { get; set; }
        public virtual DbSet<VitalSignMastersResponse> GetVitalSignMaster { get; set; }
        public virtual DbSet<SaveAllergyResponse> SaveAllergyDetails { get; set; }
        public virtual DbSet<GetAllergyResponse> GetAllergyDetails { get; set; }
        public virtual DbSet<SaveDiseasesResponse> SaveDiseasesDetails { get; set; }
        public virtual DbSet<GetDiseasesResponse> GetDiseasesDetails { get; set; }
        public virtual DbSet<lstVaccineSchedule> lstVaccineSchedule { get; set; }
        public virtual DbSet<lstPatientLatestVisit> lstPatientLatestVisit { get; set; }
        public virtual DbSet<GetVitalResultResponse> GetVitalResultDateTime { get; set; }
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
            
            modelBuilder.Entity<VitalSignDTO>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetVitalEntryDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<SaveVitalSignDTOResponse>(entity =>
            {
                entity.HasKey(e => e.VitalMasterNo);
                entity.ToTable("pro_InsertVitalEntryDetails");
                entity.Property(e => e.VitalMasterNo).HasColumnName("VitalMasterNo");
            });
            modelBuilder.Entity<VitalSignMastersResponse>(entity =>
            {
                entity.HasKey(e => e.mastercode);
                entity.ToTable("pro_GetVitalSignMasters");
                entity.Property(e => e.mastercode).HasColumnName("mastercode");
            });
            modelBuilder.Entity<SaveAllergyResponse>(entity =>
            {
                entity.HasKey(e => e.allergyRecordingno);
                entity.ToTable("pro_InsertAllergyDetails");
                entity.Property(e => e.allergyRecordingno).HasColumnName("allergyRecordingno");
            });
            modelBuilder.Entity<GetAllergyResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetAllergyDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<SaveDiseasesResponse>(entity =>
            {
                entity.HasKey(e => e.diseaserecordingno);
                entity.ToTable("pro_InsertDiseaseDetails");
                entity.Property(e => e.diseaserecordingno).HasColumnName("diseaserecordingno");
            });
            modelBuilder.Entity<GetDiseasesResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetDiseaseDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<lstVaccineSchedule>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);

                entity.Property(e => e.VaccineId).HasColumnName("VaccineId");
                entity.Property(e => e.VaccineName).HasColumnName("VaccineName");
                entity.Property(e => e.Description).HasColumnName("Description").IsRequired(false);
                entity.Property(e => e.Stage).HasColumnName("Stage");
                entity.Property(e => e.DueDate).HasColumnName("DueDate");
                entity.Property(e => e.DateOfVaccination).HasColumnName("DateOfVaccination");
            });
            modelBuilder.Entity<lstPatientLatestVisit>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
                entity.Property(e => e.PatientNo).HasColumnName("PatientNo");
                entity.Property(e => e.FullName).HasColumnName("FullName");
                entity.Property(e => e.DOB).HasColumnName("DOB");
                entity.Property(e => e.Gender).HasColumnName("Gender");
            });
            modelBuilder.Entity<GetVitalResultResponse>(entity =>
            {
                entity.HasKey(e => e.Vitaldatetime);
                entity.ToTable("pro_GetVitalResultDateTime");
                entity.Property(e => e.Vitaldatetime).HasColumnName("Vitaldatetime");
            });
        }
    }
}
