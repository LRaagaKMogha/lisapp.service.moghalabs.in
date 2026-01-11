using DEV.Common;
using Service.Model.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Service.Model.EF
{
    public partial class FinanceIntegrationContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public FinanceIntegrationContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public FinanceIntegrationContext(DbContextOptions<FinanceIntegrationContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FinanceIntegrationMaster> GetFinanceMasterDetails { get; set; }
        public virtual DbSet<SaleExportResponse> GetFinanceSalesDetails { get; set; }

        public virtual DbSet<InvoiceExportResponse> GetFinanceInvoiceDetails { get; set; }
        public virtual DbSet<InvoiceExportResponse> GetFinanceCreditNoteDetails { get; set; }
        public virtual DbSet<FinanceCustomer> GetFinanceCustomers { get; set; }
        public DbSet<FinanceSales> FinanceSales { get; set; }

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

            modelBuilder.Entity<FinanceIntegrationMaster>(entity =>
            {
                entity.ToTable("tbl_FinanceIntegrationMaster");
                entity.HasNoDiscriminator().HasKey(da => da.FinanceNo);
            });
            modelBuilder.Entity<SaleExportResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetFinanceSalesDetails");
            });
            modelBuilder.Entity<InvoiceExportResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetFinanceInvoiceDetails");
            });
            modelBuilder.Entity<InvoiceExportResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetFinanceCreditNoteDetails");
            });
            modelBuilder.Entity<FinanceCustomer>(entity =>
            {
                entity.ToTable("tbl_FinanceCustomer");
                entity.HasKey(da => da.Id);
            });
            modelBuilder.Entity<FinanceSales>(entity =>
            {
                entity.ToTable("tbl_FinanceSales");
                entity.HasKey(da => da.Id);
            });
        }


    }


}
