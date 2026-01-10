using DEV.Common;
using DEV.Model.Integration;
using Microsoft.EntityFrameworkCore;
using Shared.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.EF.Common
{
    public partial class AuditContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public AuditContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public AuditContext(DbContextOptions<AuditContext> options)
            : base(options)
        {
        }
        public DbSet<AuditLogEntry> AuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(_connectionstring));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLogEntry>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.HasNoDiscriminator().HasKey(da => da.Id);

                entity.Property(x => x.OldValue).IsRequired(false);
                entity.Property(x => x.NewValue).IsRequired(false);
                entity.Property(x => x.MetadataJson).IsRequired(false);
                entity.Property(x => x.ParentMenuId).IsRequired(false);
                entity.Property(x => x.MenuId).IsRequired(false);
                entity.Property(x => x.SubMenuCode).IsRequired(false);
                entity.Property(x => x.ContextId).IsRequired(false);
                entity.Property(x => x.UserAction).IsRequired(false);
                entity.Property(x => x.VisitNo).IsRequired(false);
                entity.Property(x => x.LabAccessionNo).IsRequired(false);

            });
        }
    }
}
