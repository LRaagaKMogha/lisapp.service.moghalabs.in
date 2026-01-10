using DEV.Common;
using DEV.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.EF.External.CommonMasters
{
    public class UserContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public UserContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public virtual DbSet<LstUser> FetchUser { get; set; }

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

            modelBuilder.Entity<LstUser>(entity =>
            {
                entity.HasKey(e => e.userNo);
                entity.ToTable("pro_Ex_GetUserList");
                entity.Property(e => e.userName).HasColumnName("userName");
            });
        }
    }
}
