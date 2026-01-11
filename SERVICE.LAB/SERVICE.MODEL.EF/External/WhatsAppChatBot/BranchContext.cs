using DEV.Common;
using Service.Model.External.WhatsAppChatBot;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.WhatsAppChatBot
{
    public partial class BranchContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public BranchContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public BranchContext(DbContextOptions<BranchContext> options) : base(options)
        {
        }
        public virtual DbSet<lstBranch> GetBranchList { get; set; }

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

            modelBuilder.Entity<lstBranch>(entity =>
            {
                entity.HasKey(e => e.VenueBranchNo);
                entity.ToTable("pro_CB_GetBranchList");
                entity.Property(e => e.VenueBranchNo).HasColumnName("VenueBranchNo");
            });
        }
    }
}