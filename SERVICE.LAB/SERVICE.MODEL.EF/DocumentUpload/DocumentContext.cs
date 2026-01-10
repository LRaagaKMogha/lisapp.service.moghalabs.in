using DEV.Common;
using Microsoft.EntityFrameworkCore;
using static DEV.Model.DocumentUploadDTO;

namespace DEV.Model.EF.DocumentUpload
{
    public partial class DocumentContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public DocumentContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DocumentContext(DbContextOptions<DocumentContext> options)
            : base(options)
        {
        }
        public DbSet<TestTemplateRequest> TemplateContent_Test { get; set; }
        public DbSet<TestTemplateApprovalRequest> TemplateContentApproval_Test { get; set; }
        public DbSet<TemplatePatientResultInsertRequest> InsertTemplatePatientResult { get; set; }
        //public DbSet<TemplatePatientResultResponse> GetTemplatePatientResult { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(_connectionstring));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestTemplateRequest>(entity =>
            {
                entity.ToTable("tbl_LB_TemplateContent_Test");
                entity.HasNoDiscriminator().HasKey(da => da.ContentId);

                entity.Property(x => x.TemplateNo).IsRequired();         
                entity.Property(x => x.TemplateName).IsRequired(false);  
                entity.Property(x => x.ContentBody).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired();
                entity.Property(x => x.CreatedOn).IsRequired();          
                entity.Property(x => x.CreatedBy).IsRequired();          
                entity.Property(x => x.Status).IsRequired();             
            });

            modelBuilder.Entity<TestTemplateApprovalRequest>(entity =>
            {
                entity.ToTable("tbl_LB_TemplateContentApproval_Test");
                entity.HasNoDiscriminator().HasKey(da => da.ContentApprovalId);

                entity.Property(x => x.TemplateApprovalNo).IsRequired();
                entity.Property(x => x.TemplateNo).IsRequired(false);
                entity.Property(x => x.TemplateName).IsRequired(false);
                entity.Property(x => x.ContentBody).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired();
                entity.Property(x => x.CreatedOn).IsRequired();
                entity.Property(x => x.CreatedBy).IsRequired();
                entity.Property(x => x.Status).IsRequired();
            });

            modelBuilder.Entity<TemplatePatientResultInsertRequest>(entity =>
            {
                entity.ToTable("tbl_LB_PatientResultsInTemplateContent");
                entity.HasNoDiscriminator().HasKey(da => da.PtRsltTmplContId);
                entity.Property(x => x.PatientVisitNo).IsRequired();
                entity.Property(x => x.OrderListNo).IsRequired();
                entity.Property(x => x.TestNo).IsRequired();
                entity.Property(x => x.TemplateNo).IsRequired();
                entity.Property(x => x.PageAction).IsRequired();
                entity.Property(x => x.PageCode).IsRequired();
                entity.Property(x => x.Result).IsRequired();
                entity.Property(x => x.VenueNo).IsRequired();
                entity.Property(x => x.CreatedOn).IsRequired();
                entity.Property(x => x.CreatedBy).IsRequired();
                entity.Property(x => x.Status).IsRequired();
            });

            //modelBuilder.Entity<TemplatePatientResultResponse>(entity =>
            //{
            //    entity.ToTable("tbl_LB_PatientResultsInTemplateContent"); 
            //    entity.HasNoKey();
            //});            
        }
    }
}
