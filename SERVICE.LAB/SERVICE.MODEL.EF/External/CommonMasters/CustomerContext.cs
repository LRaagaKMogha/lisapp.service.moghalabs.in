using DEV.Common;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.EF.External.CommonMasters
{
    public class CustomerContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public CustomerContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public virtual DbSet<LstCustomer> GetCustomer { get; set; }

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

            modelBuilder.Entity<LstCustomer>(entity =>
            {
                entity.HasKey(e => e.customerNo);
                entity.ToTable("pro_Ex_GetCustomer");
                entity.Property(e => e.customerName).HasColumnName("customerName");
            });
        }
    }
}
