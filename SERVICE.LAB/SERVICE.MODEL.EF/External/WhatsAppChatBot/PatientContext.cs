using DEV.Model.External.WhatsAppChatBot;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using DEV.Common;

namespace DEV.Model.EF.External.WhatsAppChatBot
{
    public partial class PatientContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PatientContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }
        public virtual DbSet<ReturnPatientNo> SavePatientMaster { get; set; }
        public virtual DbSet<PatientInformationResponse> GetPatientList { get; set; }
        public virtual DbSet<PatientVisitResponseDtl> GetPatientVisitList { get; set; }

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

            modelBuilder.Entity<ReturnPatientNo>(entity =>
            {
                entity.HasKey(e => e.PatientNo);
                entity.ToTable("pro_CB_InsertPatientMaster");
                entity.Property(e => e.PatientNo).HasColumnName("PatientNo");
            });

            modelBuilder.Entity<PatientInformationResponse>(entity =>
            {
                entity.HasKey(e => e.PatientNo);
                entity.ToTable("pro_CB_GetPatientsList");
                entity.Property(e => e.PatientNo).HasColumnName("PatientNo");
            });

            //modelBuilder.Entity<PatientVisitResponse>(entity =>
            //{
            //    entity.HasKey(e => e.VisitId);
            //    entity.ToTable("pro_CB_GetPatientVisitsList");
            //    entity.Property(e => e.VisitId).HasColumnName("VisitId");
            //});

            modelBuilder.Entity<PatientVisitResponseDtl>(entity =>
            {
                entity.HasKey(e => e.VisitId);
                entity.ToTable("pro_CB_GetPatientVisitsList");
                entity.Property(e => e.VisitId).HasColumnName("VisitId");
            });

            modelBuilder.Entity<PayList>(entity =>
            {
                entity.HasKey(e => e.ModeOfPayment);
                //entity.ToTable("pro_CB_GetPatientVisitsList");
                entity.Property(e => e.ModeOfPayment).HasColumnName("ModeOfPayment");
            });
        }
    }
}
