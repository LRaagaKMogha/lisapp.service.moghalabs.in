using DEV.Common;
using Service.Model.External.CommonReports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonReports
{
    public class MISReportContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public MISReportContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public MISReportContext(DbContextOptions<MISReportContext> options) : base(options)
        {
        }

        public virtual DbSet<LstPaidInformation> FetchPaidInformation { get; set; }

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

            modelBuilder.Entity<LstPaidInformation>(entity =>
            {
                entity.HasKey(e => e.visitId);
                entity.ToTable("pro_Ex_GetCollectionMIS");
                entity.Property(e => e.patientName).HasColumnName("patientName");
            });
        }
    }
}
