using DEV.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonMasters
{
    public class InternalPhysicianContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public InternalPhysicianContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public InternalPhysicianContext(DbContextOptions<InternalPhysicianContext> options) : base(options)
        {
        }
        public virtual DbSet<LstInternalPhysician> GetInternalPhysician { get; set; }

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

            modelBuilder.Entity<LstInternalPhysician>(entity =>
            {
                entity.HasKey(e => e.doctorNo);
                entity.ToTable("pro_Ex_GetInternalPhysician");
                entity.Property(e => e.doctorName).HasColumnName("doctorName");
            });
        }
    }
}
