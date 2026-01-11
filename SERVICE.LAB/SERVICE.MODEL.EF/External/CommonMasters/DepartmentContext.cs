using DEV.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonMasters
{
    public class DepartmentContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public DepartmentContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        {
        }
        public virtual DbSet<LstDepartment> GetDepartment { get; set; }
        public virtual DbSet<LstMainDepartment> GetMainDepartment { get; set; }

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

            modelBuilder.Entity<LstDepartment>(entity =>
            {
                entity.HasKey(e => e.deptNo);
                entity.ToTable("pro_Ex_GetDepartment");
                entity.Property(e => e.deptName).HasColumnName("deptName");
            });

            modelBuilder.Entity<LstMainDepartment>(entity =>
            {
                entity.HasKey(e => e.mainDeptNo);
                entity.ToTable("pro_Ex_GetMainDepartment");
                entity.Property(e => e.mainDeptName).HasColumnName("mainDeptName");
            });
        }
    }
}
