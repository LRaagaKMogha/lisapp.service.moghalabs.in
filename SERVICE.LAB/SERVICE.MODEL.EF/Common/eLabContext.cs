using DEV.Common;
using Service.Model.PatientInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.EF.Common
{
    public partial class eLabContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public eLabContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public eLabContext(DbContextOptions<eLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PatientInfoeLabResponseDTO> GeteLabPatientInfoDTO { get; set; }
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
            modelBuilder.Entity<PatientInfoeLabResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientInfo_eLab");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });
        }
    }
}
