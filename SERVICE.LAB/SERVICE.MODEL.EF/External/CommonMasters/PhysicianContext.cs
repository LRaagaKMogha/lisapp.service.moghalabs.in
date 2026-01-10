using DEV.Common;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.EF.External.CommonMasters
{
    public class PhysicianContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public PhysicianContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public PhysicianContext(DbContextOptions<PhysicianContext> options) : base(options)
        {
        }
        public virtual DbSet<LstPhysician> GetPhysician { get; set; }

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

            modelBuilder.Entity<LstPhysician>(entity =>
            {
                entity.HasKey(e => e.doctorNo);
                entity.ToTable("pro_Ex_GetPhysician");
                entity.Property(e => e.doctorName).HasColumnName("doctorName");
            });
        }
    }
}
