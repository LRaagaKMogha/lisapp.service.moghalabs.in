using DEV.Common;
using Service.Model.External.CommonMasters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.CommonMasters
{
    public class GroupMastContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public GroupMastContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public GroupMastContext(DbContextOptions<GroupMastContext> options) : base(options)
        {
        }
        public virtual DbSet<LstGroupInfo> GetGroupList { get; set; }

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

            modelBuilder.Entity<LstGroupInfo>(entity =>
            {
                entity.HasKey(e => e.groupNo);
                entity.ToTable("pro_Ex_GetGroupListInfo");
                entity.Property(e => e.groupName).HasColumnName("groupName");
            });

            modelBuilder.Entity<LstGroupServiceList>(entity =>
            {
                entity.HasKey(e => e.serviceName);
                entity.ToTable("tbl_GroupMap");
                entity.Property(e => e.serviceName).HasColumnName("TestNo");
            });
        }
    }
}
