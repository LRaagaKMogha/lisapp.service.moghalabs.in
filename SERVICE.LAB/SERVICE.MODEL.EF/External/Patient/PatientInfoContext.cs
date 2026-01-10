using DEV.Common;
using DEV.Model.External.Billing;
using DEV.Model.External.Patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.EF.External.Patient
{
    public class PatientInfoContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PatientInfoContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PatientInfoContext(DbContextOptions<PatientInfoContext> options) : base(options)
        {
        }

        public virtual DbSet<LstPatientInfo> FetchPatientInformation { get; set; }        

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

            modelBuilder.Entity<LstPatientInfo>(entity =>
            {
                entity.HasKey(e => e.visitId);
                entity.ToTable("Pro_Ex_GetPatientInfo");
                entity.Property(e => e.patientName).HasColumnName("patientName");
            });
        }
    }
}
