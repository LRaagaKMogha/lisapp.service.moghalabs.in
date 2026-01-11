using DEV.Common;
using Service.Model.External.WhatsAppChatBot;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.EF.External.WhatsAppChatBot
{
    public partial class LogContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public LogContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }
        public virtual DbSet<returnLogRefNo> InsertLogInformation { get; set; }
        public virtual DbSet<FetchLogResponse> FetLogInformation { get; set; }

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

            modelBuilder.Entity<returnLogRefNo>(entity =>
            {
                entity.HasKey(e => e.LogRefNo);
                entity.ToTable("pro_CB_InsertLogInformation");
                entity.Property(e => e.LogRefNo).HasColumnName("LogRefNo");
            });

            modelBuilder.Entity<FetchLogResponse>(entity =>
            {
                entity.HasKey(e => e.LogRefNo);
                entity.ToTable("pro_CB_GetLogInformation");
                entity.Property(e => e.LogRefNo).HasColumnName("LogRefNo");
            });
        }

        }
}
