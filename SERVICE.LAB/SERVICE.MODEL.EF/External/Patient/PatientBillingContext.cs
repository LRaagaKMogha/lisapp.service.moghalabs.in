using DEV.Common;
using Service.Model.External.Billing;
using Service.Model.External.Patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.Patient
{
    public class PatientBillingContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PatientBillingContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PatientBillingContext(DbContextOptions<PatientBillingContext> options) : base(options)
        {
        }

        public virtual DbSet<LstPatientBillingInfo> FetchPatientBillingInformation { get; set; }
        public virtual DbSet<BillPaymentDetails> FetchPatientBillingPayInformation { get; set; }
        public virtual DbSet<LstPatientCancelBillingInfo> FetchPatientCancelBillingInformation { get; set; }
        public virtual DbSet<CancelBillPaymentDetails> FetchPatientCancelBillingPayInformation { get; set; }
        public virtual DbSet<LstCancelPatientInfo> FetchCancelBillingInformation { get; set; }

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

            modelBuilder.Entity<LstPatientBillingInfo>(entity =>
            {
                entity.HasKey(e => e.patientVisitNo);
                entity.ToTable("Pro_Ex_GetPatientBillingInfo");
                entity.Property(e => e.patientName).HasColumnName("patientName");
            });

            modelBuilder.Entity<BillPaymentDetails>(entity =>
            {
                entity.HasKey(e => e.payType);
                entity.ToTable("Pro_Ex_GetPatientBillPaymentInfo");
                entity.Property(e => e.modeOfPay).HasColumnName("modeOfPay");
            });

            modelBuilder.Entity<BillServiceDetails>(entity =>
            {
                entity.HasKey(e => e.serviceNo);
                entity.Property(e => e.serviceName).HasColumnName("ServiceName");
            });

            modelBuilder.Entity<LstPatientCancelBillingInfo>(entity =>
            {
                entity.HasKey(e => e.patientVisitNo);
                entity.ToTable("Pro_Ex_GetPatientCancelBillingInfo");
                entity.Property(e => e.patientName).HasColumnName("PatientName");
            });

            modelBuilder.Entity<CancelBillPaymentDetails>(entity =>
            {
                entity.HasKey(e => e.payType);
                entity.ToTable("Pro_Ex_GetPatientCancelBillPaymentInfo");
                entity.Property(e => e.modeOfPay).HasColumnName("modeOfPay");
            });

            modelBuilder.Entity<CancelBillServiceDetails>(entity =>
            {
                entity.HasKey(e => e.serviceNo);
                entity.Property(e => e.serviceName).HasColumnName("ServiceName");
            });

            modelBuilder.Entity <LstCancelPatientInfo>(entity =>
            {
                entity.HasKey(e => e.visitNo);
                entity.ToTable("pro_Ex_GetCancelServiceInfo");
                entity.Property(e => e.visitId).HasColumnName("VisitId");
            });

            modelBuilder.Entity<LstCancelServiceList>(entity =>
            {
                entity.HasKey(e => e.serviceNo);
                entity.Property(e => e.serviceName).HasColumnName("ServiceName");
            });
            
        }
     }
}
