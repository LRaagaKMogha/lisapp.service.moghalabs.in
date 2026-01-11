using DEV.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonMasters
{
    public class PackageMastContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PackageMastContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PackageMastContext(DbContextOptions<PackageMastContext> options) : base(options)
        {
        }
        public virtual DbSet<LstPackageInfo> GetPackageList { get; set; }
        public virtual DbSet<LstPackageBreakUpInfo> GetPackageBreakUp { get; set; }

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

            modelBuilder.Entity<LstPackageInfo>(entity =>
            {
                entity.HasKey(e => e.packageNo);
                entity.ToTable("pro_Ex_GetPackageListInfo");
                entity.Property(e => e.packageName).HasColumnName("packageName");
            });

            modelBuilder.Entity<LstPackageBreakUpInfo>(entity =>
            {
                entity.HasKey(e => e.packageNo);
                entity.ToTable("pro_Ex_GetPackageBreakUp");
                entity.Property(e => e.packageName).HasColumnName("packageName");
            });

            modelBuilder.Entity<LstPackageServiceList>(entity =>
            {
                entity.HasKey(e => e.serviceName);
                entity.ToTable("tbl_PackageMap");
                entity.Property(e => e.serviceName).HasColumnName("PackageNo");
            });
        }
    }
}
