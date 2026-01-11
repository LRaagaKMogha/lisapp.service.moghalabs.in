using DEV.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonMasters
{
    public class TestMastContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public TestMastContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public TestMastContext(DbContextOptions<TestMastContext> options) : base(options)
        {
        }
        public virtual DbSet<LstTestInfo> GetTestList { get; set; }

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

            modelBuilder.Entity<LstTestInfo>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_Ex_GetTestListInfo");
                entity.Property(e => e.testName).HasColumnName("testName");
            });
        }
    }
}
