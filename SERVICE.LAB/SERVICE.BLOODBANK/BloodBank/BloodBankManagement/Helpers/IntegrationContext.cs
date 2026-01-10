using System;
using DEV.Common;

using DEV.Model.Integration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DEV.Model.EF
{
    public partial class IntegrationContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public IntegrationContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public IntegrationContext(DbContextOptions<IntegrationContext> options)
            : base(options)
        {
        }
        public virtual DbSet<labresponsedetails> GetPDFReportDetails { get; set; }
        public virtual DbSet<labtestdetails> GetPDFReportTestDetails { get; set; }
        public virtual DbSet<LabReportTestDetails> GetDiscreetLabData { get; set; }

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

            modelBuilder.Entity<labresponsedetails>(entity =>
            {
                entity.HasNoKey();
                entity.Ignore("reportdetails");
                entity.ToTable("Pro_GetPDFReportDetails");
            });
            modelBuilder.Entity<labtestdetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetPDFReportTestDetails");
            });

        }

    }


}
