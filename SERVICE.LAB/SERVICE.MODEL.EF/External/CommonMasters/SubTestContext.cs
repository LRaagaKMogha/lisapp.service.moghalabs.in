using DEV.Common;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.EF.External.CommonMasters
{
    public class SubTestContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public SubTestContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public SubTestContext(DbContextOptions<SubTestContext> options) : base(options)
        {
        }
        public virtual DbSet<LstSubTestInfo> GetSubTestList { get; set; }

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

            modelBuilder.Entity<LstSubTestInfo>(entity =>
            {
                entity.HasKey(e => e.subtestNo);
                entity.ToTable("pro_Ex_GetSubTestListInfo");
                entity.Property(e => e.subTest).HasColumnName("subTest");
            });
        }
    }
}
