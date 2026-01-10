using System;
using DEV.Common;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DEV.Model.EF
{
    public partial class CommonContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public CommonContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public CommonContext(DbContextOptions<CommonContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LstSearch> CommonSearch { get; set; }
        public virtual DbSet<LstMasterSearch> CommonMasterSearch { get; set; }
        public virtual DbSet<NotificationResponse> Notification { get; set; }
        public virtual DbSet<TblVenueBranches> TblVenueBranches { get; set; }
        public virtual DbSet<TblUserBranchMap> TblUserBranchMap { get; set; }
        public virtual DbSet<LstFilter>CommonFilter { get; set; }
        public virtual DbSet<GetCriticalResultsResponse> GetCriticalResultNotifys { get; set; }
        public virtual DbSet<ApprovalResponse> GetApprovalRequestData { get; set; }
        public virtual DbSet<ApprovalHistory> GetApprovalHistory { get; set; }
        public virtual DbSet<SaveCriticalResultNotifyRes> SaveCriticalResultNotifys { get; set; }
        public virtual DbSet<ResTransactionSplit> GetTransactionCustomerDetails { get; set; }
        public virtual DbSet<ResTransactionSplitById> GetTransactionCustomerDetailsById { get; set; }
        public virtual DbSet<FetchPortalResponse> GetPortalUrl { get; set; }
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

            modelBuilder.Entity<LstSearch>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_CommonSearchTransaction");
            });

            modelBuilder.Entity<LstMasterSearch>(entity =>
            {
                entity.HasKey(e => e.masterno);
                entity.ToTable("pro_CommonSearchMaster");
                entity.Property(e => e.masterno).HasColumnName("masterno");
            });

            modelBuilder.Entity<NotificationResponse>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("Pro_PushMessage");
                entity.Property(e => e.Status).HasColumnName("Status");

            });
            
            modelBuilder.Entity<TblUserBranchMap>(entity =>
            {
                entity.HasKey(e => e.UserBranchMapNo)
                    .HasName("PK__tbl_UserBranchMap__1A2949B8D01AE70E");

                entity.ToTable("tbl_UserBranchMap");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Isdefault)
                    .IsRequired()
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblVenueBranches>(entity =>
            {
                entity.HasKey(e => e.VenueBranchNo)
                    .HasName("PK__tbl_Venu__B39E4E1579655EA2");

                entity.ToTable("tbl_VenueBranches");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Domaincode)
                    .HasColumnName("domaincode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntegrationCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IntegrationId)
                    .HasColumnName("IntegrationID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.VenueBranchDisplayText).HasMaxLength(200);

                entity.Property(e => e.VenueBranchName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LstFilter>(entity =>
            {
                entity.HasKey(e => e.filterCode);
                entity.ToTable("pro_CommonFilter");
                entity.Property(e => e.filterCode).HasColumnName("FilterCode");
            });
            modelBuilder.Entity<GetCriticalResultsResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("Pro_GetCriticalResultDetailsForUser");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<SaveCriticalResultNotifyRes>(entity =>
            {
                entity.HasKey(e => e.oStatus);
                entity.ToTable("Pro_InsertCriticalResultNotifyForUser");
                entity.Property(e => e.oStatus).HasColumnName("oStatus");
            });
            modelBuilder.Entity<ApprovalResponse>(entity =>
            {
                entity.HasKey(e => e.NewNo);
                entity.ToTable("Pro_GetApprovalList");
                entity.Property(e => e.NewNo).HasColumnName("NewNo");
            });
            modelBuilder.Entity<ApprovalHistory>(entity =>
            {
                entity.HasKey(e => e.ApprovalHistoryNo);
                entity.ToTable("Pro_GetApprovalHistory");
                entity.Property(e => e.ApprovalHistoryNo).HasColumnName("ApprovalHistoryNo");
            });
            modelBuilder.Entity<ResTransactionSplit>(entity =>
            {
                entity.HasKey(e => e.sno);
                entity.ToTable("pro_GetCustomerDetTransactionSplitServiceMIS");
                entity.Property(e => e.sno).HasColumnName("sno");
            });
            modelBuilder.Entity<ResTransactionSplitById>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_GetPatientDetTransactionSplitServiceMI");
                entity.Property(e => e.sNo).HasColumnName("sno");
            });
            modelBuilder.Entity<FetchPortalResponse>(entity =>
            {
                entity.HasKey(e => e.returnUrl);
                entity.ToTable("Pro_GetPortalUrl");
                entity.Property(e => e.returnUrl).HasColumnName("returnUrl");
            });
        }
    }
}