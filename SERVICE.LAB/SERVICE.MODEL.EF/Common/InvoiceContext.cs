using System;
using DEV.Common;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DEV.Model.EF
{
    public partial class InvoiceContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public InvoiceContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public InvoiceContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<lstcustomerVisit> GetCustomerVisit { get; set; }
        public virtual DbSet<rtninvoice> InsertInvoiceCreate { get; set; }

        public virtual DbSet<rtninvoiceCredit> InsertInvoiceCreditNote { get; set; }
        
        public virtual DbSet<lstCustomerInvoice> GetCustomerInvoice { get; set; }
        public virtual DbSet<objInvoice> GetInvoiceInfo { get; set; }
        public virtual DbSet<rtninvoicePayment> InsertInvoicePayment { get; set; }
        public virtual DbSet<lstSearchInvoice> SearchInvoiceNo { get; set; }
        public virtual DbSet<lstInvoicePayment> GetInvoicePayment { get; set; }
        public virtual DbSet<lstCreditNoteVisit> GetCreditNoteResponse { get; set; }

        public virtual DbSet<CreditNoteReport> GetCreditNoteReport { get; set; }
        public virtual DbSet<VenueDetails> getInvoiceCreateVenueDetails { get; set; }
        public virtual DbSet<InvoiceTDSUpdateResponse> UpdateTDSFlag { get; set; }
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

            modelBuilder.Entity<lstcustomerVisit>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetCustomerVisit");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<lstCreditNoteVisit>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetCreditNoteDetails");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<rtninvoice>(entity =>
            {
                entity.HasKey(e => e.invoiceNo);
                entity.ToTable("pro_InsertInvoiceCreate");
                entity.Property(e => e.invoiceNo).HasColumnName("invoiceNo");
            });
            modelBuilder.Entity<rtninvoiceCredit>(entity =>
            {
                entity.HasKey(e => e.CreditNo);
                entity.ToTable("pro_InsertInvoiceCreditNote");
                entity.Property(e => e.CreditNo).HasColumnName("CreditNo");
            });


            modelBuilder.Entity<lstCustomerInvoice>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetCustomerInvoice");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<objInvoice>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetInvoiceInfo");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<rtninvoicePayment>(entity =>
            {
                entity.HasKey(e => e.invoicePaymentNo);
                entity.ToTable("pro_InsertInvoicePayment");
                entity.Property(e => e.invoicePaymentNo).HasColumnName("invoicePaymentNo");
            });

            modelBuilder.Entity<lstSearchInvoice>(entity =>
            {
                entity.HasKey(e => e.invoiceNo);
                entity.ToTable("pro_SearchInvoiceNo");
                entity.Property(e => e.invoiceNo).HasColumnName("invoiceNo");
            });

            modelBuilder.Entity<lstInvoicePayment>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetInvoicePayment");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<rtnCancelInvoice>(entity =>
            {
                entity.HasKey(e => e.invoiceNo);
                entity.ToTable("pro_InvoiceCancel");
                entity.Property(e => e.invoiceNo).HasColumnName("invoiceNo");
            });

            modelBuilder.Entity<CreditNoteReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetCreditNoteReport");
            });
            modelBuilder.Entity<VenueDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_getInvoiceGenerateVenueDetails");
            });
            modelBuilder.Entity<InvoiceTDSUpdateResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_UpdateTDSFlag");
                entity.Property(e => e.status).HasColumnName("status");
            });
        }

        public virtual DbSet<rtnCancelInvoice> InvoiceCancel { get; set; }
    }
}
