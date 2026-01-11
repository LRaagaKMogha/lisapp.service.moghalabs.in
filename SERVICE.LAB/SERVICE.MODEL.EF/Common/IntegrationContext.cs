using DEV.Common;
using Service.Model.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;

namespace Service.Model.EF
{
    public partial class IntegrationContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public IntegrationContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public IntegrationContext(DbContextOptions<IntegrationContext> options)
            : base(options)
        {
        }
        public virtual DbSet<orderresponse> SendOrderDetails { get; set; }
        public DbSet<TestInformation> TestInformation { get; set; }
        public DbSet<TestValidation> TestValidation { get; set; }
        public DbSet<TestAdditionalInformation> TestAdditionalInformation { get; set; }
        public DbSet<MassRegistrationList> MassRegistrationList { get; set; }
        public DbSet<IntegrationOrderDetails> IntegrationOrderDetails { get; set; }
        public DbSet<MassRegistration> MassRegistrations { get; set; }
        public DbSet<MassRegistrationSample> MassRegistrationSamples { get; set; }
        public DbSet<IntegrationOrderVisitDetails> IntegrationOrderVisitDetails { get; set; }
        public DbSet<IntegrationOrderPatientDetails> IntegrationOrderPatientDetails { get; set; }
        public DbSet<IntegrationOrderClientDetails> IntegrationOrderClientDetails { get; set; }
        public DbSet<IntegrationOrderDoctorDetails> IntegrationOrderDoctorDetails { get; set; }
        public DbSet<IntegrationOrderTestDetails> IntegrationOrderTestDetails { get; set; }
        public DbSet<IntegrationOrderWardDetails> IntegrationOrderWardDetails { get; set; }
        public DbSet<IntegrationOrderAllergyDetails> IntegrationOrderAllergyDetails { get; set; }

        public DbSet<PatientTransactions> PatientTransactions {  get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
        public virtual DbSet<orderresponse> MassRegistration { get; set; }

        public virtual DbSet<TestMasterDetails> GetTestDetails { get; set; }
        public virtual DbSet<labresponsedetails> GetPDFReportDetails { get; set; }
        public virtual DbSet<labtestdetails> GetPDFReportTestDetails { get; set; }

        public virtual DbSet<IntegrationVisitDetails> GetIntegrationVisitDetails { get; set; }

        public virtual DbSet<IntegrationVisitTestDetails> GetIntegrationVisitTestDetails { get; set; }
        public virtual DbSet<LabReportTestDetails> GetDiscreetLabData { get; set; }

        public virtual DbSet<MassRegistrationResponse> UpdateLabAccessionNoMassRegistration { get; set; }

        public virtual DbSet<MassRegistrationResponse> UpdateLabAccessionNoDownTimeOrder { get; set; }

        public virtual DbSet<testdetailsTrendReport> GetTestTrendReportDetails { get; set; }
        public DbSet<TestResponse> TestDetails { get; set; }
        public DbSet<GroupResponse> GroupDetails { get; set; }

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
            modelBuilder.Ignore<TestResponse>();
            modelBuilder.Entity<TestResponse>().HasNoKey();
            modelBuilder.Ignore<GroupResponse>();
            modelBuilder.Entity<GroupResponse>().HasNoKey();

            modelBuilder.Entity<orderresponse>(entity =>
            {
                entity.HasKey(e => e.referenceno);
                entity.ToTable("Pro_InsertIntegrationOrderDetails");
                entity.Property(e => e.referenceno).HasColumnName("referenceno");
            });

            modelBuilder.Entity<testdetailsTrendReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetTestTrendReportDetails");
            });

            modelBuilder.Entity<TestInformation>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetTestInformation");
            });
            modelBuilder.Entity<TestValidation>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetTestValidation");
            });
            modelBuilder.Entity<TestAdditionalInformation>(entity =>
            {
                entity.HasKey(e => e.DataType);
                entity.ToTable("Pro_GetTestAdditionalInformation");
            });
            modelBuilder.Entity<MassRegistrationList>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_GetMassRegistration");
            });

            modelBuilder.Entity<MassRegistration>(entity =>
            {
                entity.ToTable("tbl_MassRegistration");
                entity.HasNoDiscriminator().HasKey(da => da.MassRegistrationNo);

                entity.Property(x => x.MassFileNo).IsRequired(false);
                entity.Property(x => x.PatientName).IsRequired(false);
                entity.Property(x => x.IdNumber).IsRequired(false);
                entity.Property(x => x.DOB).IsRequired(false);
                entity.Property(x => x.Gender).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.PatientVisitNo).IsRequired(false);
                entity.Property(x => x.LabAccessionNo).IsRequired(false);
            });

            modelBuilder.Entity<MassRegistrationSample>(entity =>
            {
                entity.ToTable("tbl_MassRegistrationSample");
                entity.HasNoDiscriminator().HasKey(da => da.MassRegistrationSampleNo);
                entity.Property(x => x.BarCodeNo).IsRequired(false);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("tbl_OrderDetails");
                entity.HasNoDiscriminator().HasKey(da => da.OrderDetailsNo);
                entity.Property(x => x.SubTestNo).IsRequired(false);
                entity.Property(x => x.SubTestName).IsRequired(false);
                entity.Property(x => x.SSeqNo).IsRequired(false);
                entity.Property(x => x.MethodNo).IsRequired(false);
                entity.Property(x => x.MethodName).IsRequired(false);
                entity.Property(x => x.UnitNo).IsRequired(false);
                entity.Property(x => x.UnitName).IsRequired(false);
                entity.Property(x => x.LLColumn).IsRequired(false);
                entity.Property(x => x.HLColumn).IsRequired(false);
                entity.Property(x => x.DisplayRR).IsRequired(false);
                entity.Property(x => x.CRLLColumn).IsRequired(false);
                entity.Property(x => x.CRHLColumn).IsRequired(false);
                entity.Property(x => x.DisplayCRRR).IsRequired(false);
                entity.Property(x => x.MinRange).IsRequired(false);
                entity.Property(x => x.MaxRange).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.IntegrationID).IsRequired(false);
                entity.Property(x => x.IntegrationCode).IsRequired(false);
                entity.Property(x => x.DecimalPoint).IsRequired(false);
                entity.Property(x => x.FormulaServiceNo).IsRequired(false);
                entity.Property(x => x.FormulaServiceType).IsRequired(false);
                entity.Property(x => x.IsIndiviual).IsRequired(false);
                entity.Property(x => x.IsNABL).IsRequired(false);
                entity.Property(x => x.HeaderName).IsRequired(false);
                entity.Property(x => x.IsInterfacePickedDTTM).IsRequired(false);
                entity.Property(x => x.IsUploadOption).IsRequired(false);
                entity.Property(x => x.UploadedFile).IsRequired(false);
                entity.Property(x => x.IsMultiEditor).IsRequired(false);
                entity.Property(x => x.DepCode).IsRequired(false);
                entity.Property(x => x.LanguageText).IsRequired(false);
                entity.Property(x => x.SubTestLanguageText).IsRequired(false);
                entity.Property(x => x.LesserValue).IsRequired(false);
                entity.Property(x => x.GreaterValue).IsRequired(false);
                entity.Property(x => x.IsSensitiveData).IsRequired(false);
                entity.Property(x => x.IsExtraSubTest).IsRequired(false);
                entity.Property(x => x.SnomedCode).IsRequired(false);
                entity.Property(x => x.SnomedCodeId).IsRequired(false);
                entity.Property(x => x.SubTestDepartmentNo).IsRequired(false);
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.ToTable("tbl_OrderList");
                entity.HasNoDiscriminator().HasKey(da => da.OrderListNo);
                entity.Property(x => x.SampleNo).IsRequired(false);
                entity.Property(x => x.ContainerNo).IsRequired(false);
                entity.Property(x => x.ProcessingMin).IsRequired(false);
                entity.Property(x => x.TATDTTM).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.IntegrationID).IsRequired(false);
                entity.Property(x => x.IntegrationCode).IsRequired(false);
                entity.Property(x => x.PackageNo).IsRequired(false);
                entity.Property(x => x.IsSecondReview).IsRequired(false);
                entity.Property(x => x.IsSampleAct).IsRequired(false);
            });

            modelBuilder.Entity<PatientTransactions>(entity =>
            {
                entity.ToTable("tbl_patientTransaction");
                entity.HasNoDiscriminator().HasKey(da => da.PatientTransactionNo);
                entity.Property(x => x.FullName).IsRequired(false);
                entity.Property(x => x.AgeType).IsRequired(false);
                entity.Property(x => x.DOB).IsRequired(false);
                entity.Property(x => x.Gender).IsRequired(false);
                entity.Property(x => x.MobileNumber).IsRequired(false);
                entity.Property(x => x.EmailID).IsRequired(false);
                entity.Property(x => x.Address).IsRequired(false);
                entity.Property(x => x.URNID).IsRequired(false);
                entity.Property(x => x.URNType).IsRequired(false);
                entity.Property(x => x.ExtenalVisitID).IsRequired(false);
                entity.Property(x => x.CustomerNo).IsRequired(false);
                entity.Property(x => x.CustomerName).IsRequired(false);
                entity.Property(x => x.CustomerEmail).IsRequired(false);
                entity.Property(x => x.CustomerMobileNumber).IsRequired(false);
                entity.Property(x => x.PhysicianNo).IsRequired(false);
                entity.Property(x => x.PhysicianName).IsRequired(false);
                entity.Property(x => x.PhysicianQualification).IsRequired(false);
                entity.Property(x => x.PhysicianEmail).IsRequired(false);
                entity.Property(x => x.PhysicianMobileNumber).IsRequired(false);
                entity.Property(x => x.RiderNo).IsRequired(false);
                entity.Property(x => x.RiderName).IsRequired(false);
                entity.Property(x => x.ExcutiveNo).IsRequired(false);
                entity.Property(x => x.ExcutiveName).IsRequired(false);
                entity.Property(x => x.RCTDTTM).IsRequired(false);
                entity.Property(x => x.ClinicalHistory).IsRequired(false);
                entity.Property(x => x.ID).IsRequired(false);
                entity.Property(x => x.IDType).IsRequired(false);
                entity.Property(x => x.ModeofDispatch).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ReferralType).IsRequired(false);
                entity.Property(x => x.MarketingNo).IsRequired(false);
                entity.Property(x => x.MarketingName).IsRequired(false);
                entity.Property(x => x.VaccinationType).IsRequired(false);
                entity.Property(x => x.VaccinationDate).IsRequired(false);
                entity.Property(x => x.RouteNo).IsRequired(false);
                entity.Property(x => x.IsSelf).IsRequired(false);
                entity.Property(x => x.NURNID).IsRequired(false);
                entity.Property(x => x.NURNType).IsRequired(false);
                entity.Property(x => x.Deliverymode).IsRequired(false);
                entity.Property(x => x.ExternalPatientId).IsRequired(false);
                entity.Property(x => x.IsOverAllTestApproved).IsRequired(false);
                entity.Property(x => x.WardNo).IsRequired(false);
                entity.Property(x => x.WardName).IsRequired(false);
                entity.Property(x => x.OPIPNumber).IsRequired(false);
                entity.Property(x => x.IsFranchise).IsRequired(false);
                entity.Property(x => x.PhysicianWhatsAppNo).IsRequired(false);
                entity.Property(x => x.NRICNumber).IsRequired(false);
                entity.Property(x => x.DietaryNo).IsRequired(false);
                entity.Property(x => x.IsFasting).IsRequired(false);
                entity.Property(x => x.IsDocuments).IsRequired(false);
            });

            modelBuilder.Entity<OrderTransaction>(entity =>
            {
                entity.ToTable("tbl_OrderTransaction");
                entity.HasNoDiscriminator().HasKey(da => da.OrderTransactionNo);
                entity.Property(x => x.IsHigTemprature).IsRequired(false);
                entity.Property(x => x.IsBarcodeNotReq).IsRequired(false);
                entity.Property(x => x.HigTempValue).IsRequired(false);
                entity.Property(x => x.Collectatsource).IsRequired(false);
                entity.Property(x => x.IsSecondReview).IsRequired(false);
                entity.Property(x => x.SecondReviewReqBy).IsRequired(false);
                entity.Property(x => x.IsPrint).IsRequired(false);
                entity.Property(x => x.WorklistNo).IsRequired(false);
                entity.Property(x => x.ProcessingMin).IsRequired(false);
                entity.Property(x => x.TATDTTM).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.PackageNo).IsRequired(false);
                entity.Property(x => x.PackageName).IsRequired(false);
            });

            modelBuilder.Entity<IntegrationOrderDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderDetails");
                entity.HasNoDiscriminator().HasKey(da => da.OrderId);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.PatientVisitNo).IsRequired(false);
                entity.Property(x => x.ReferenceNo).IsRequired(false);
                entity.Property(x => x.LabAccessionNo).IsRequired(false);
                entity.Property(x => x.VisitNo).IsRequired(false);
                entity.Property(x => x.CaseNumber).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.ReferenceNo).IsRequired(false);
                entity.Property(x => x.LabAccessionNo).IsRequired(false);
                entity.Property(x => x.VisitNo).IsRequired(false);
                entity.Property(x => x.CaseNumber).IsRequired(false);
                entity.Property(x => x.PatientVisitNo).IsRequired(false);
                entity.Property(x => x.HoldReason).IsRequired(false);
                entity.Property(x => x.DownTimeLabAccessionNo).IsRequired(false);
                entity.Property(x => x.BBLabAccessionNo).IsRequired(false);
                entity.Property(x => x.Messages).IsRequired(false);

                entity.HasOne(od => od.IntegrationOrderVisitDetails).WithOne(ov => ov.Order);
                entity.HasOne(od => od.IntegrationOrderPatientDetails).WithOne(ov => ov.Order);
                entity.HasOne(od => od.IntegrationOrderClientDetails).WithOne(ov => ov.Order);
                entity.HasOne(od => od.IntegrationOrderDoctorDetails).WithOne(ov => ov.Order);
                entity.HasMany(od => od.IntegrationOrderTestDetails).WithOne(ov => ov.Order);
                entity.HasOne(od => od.IntegrationOrderWardDetails).WithOne(ov => ov.Order);
                entity.HasMany(od => od.IntegrationOrderAllergyDetails).WithOne(ov => ov.Order);
            });
            modelBuilder.Entity<IntegrationOrderVisitDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderVisitDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.visitno).IsRequired(false);
                entity.Property(x => x.casenumber).IsRequired(false);
                entity.Property(x => x.idnumber).IsRequired(false);
                entity.Property(x => x.rcordernumber).IsRequired(false);
                entity.Property(x => x.contractcode).IsRequired(false);
            });
            modelBuilder.Entity<IntegrationOrderPatientDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderPatientDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.alternateIdtype).IsRequired(false);
                entity.Property(x => x.alternateIdnumber).IsRequired(false);
                entity.Property(x => x.diagnosiscode).IsRequired(false);
                entity.Property(x => x.diagnosisdescription).IsRequired(false);
                entity.Property(x => x.transfusionindication).IsRequired(false);
                entity.Property(x => x.height).IsRequired(false);
                entity.Property(x => x.weight).IsRequired(false);
            });
            modelBuilder.Entity<IntegrationOrderClientDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderClientDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.clientcode).IsRequired(false);
                entity.Property(x => x.clientname).IsRequired(false);
                entity.Property(x => x.cliniccode).IsRequired(false);
                entity.Property(x => x.clinicname).IsRequired(false);

            });
            modelBuilder.Entity<IntegrationOrderDoctorDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderDoctorDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.doctormcr).IsRequired(false);
                entity.Property(x => x.doctorname).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
            });
            modelBuilder.Entity<IntegrationOrderTestDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderTestDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.quantity).IsRequired(false);
                entity.Property(x => x.natureofrequest).IsRequired(false);
                entity.Property(x => x.natureofspecimen).IsRequired(false);
                entity.Property(x => x.IsRejected).IsRequired(false);
                entity.Property(x => x.RejectedReason).IsRequired(false);
                entity.Property(x => x.IsOnHold).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
                entity.Property(x => x.sourceofspecimen).IsRequired(false);
                entity.Property(x => x.remarks).IsRequired(false);
                entity.Property(x => x.createddttm).IsRequired(false);
                entity.Property(x => x.barcodenumber).IsRequired(false);
                entity.Property(x => x.TestCode).IsRequired(false);
                entity.Property(x => x.TestName).IsRequired(false);
                entity.Property(x => x.RejectedReasonDesc).IsRequired(false);
                entity.Property(x => x.LabAccessionNo).IsRequired(false);
                entity.Property(x => x.PatientVisitNo).IsRequired(false);
                entity.Property(x => x.IsNotGiven).IsRequired(false);

            });
            modelBuilder.Entity<IntegrationOrderWardDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderWardDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.nursingOU).IsRequired(false);
                entity.Property(x => x.room).IsRequired(false);
                entity.Property(x => x.bed).IsRequired(false);
                entity.Property(x => x.ward).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);
            });
            modelBuilder.Entity<IntegrationOrderAllergyDetails>(entity =>
            {
                entity.ToTable("tbl_IntegrationOrderAllergyDetails");
                entity.HasNoDiscriminator().HasKey(da => da.Id);
                entity.Property(x => x.Allergy).IsRequired(false);
                entity.Property(x => x.Status).IsRequired(false);
                entity.Property(x => x.CreatedOn).IsRequired(false);
                entity.Property(x => x.CreatedBy).IsRequired(false);
                entity.Property(x => x.ModifiedOn).IsRequired(false);
                entity.Property(x => x.ModifiedBy).IsRequired(false);
                entity.Property(x => x.VenueNo).IsRequired(false);
                entity.Property(x => x.VenueBranchNo).IsRequired(false);

            });

            modelBuilder.Entity<TestMasterDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetTestDetails");
            });

            modelBuilder.Entity<labresponsedetails>(entity =>
            {
                entity.HasNoKey();
                entity.Ignore("reportdetails");
                entity.ToTable("Pro_GetPDFReportDetails");
            });
            modelBuilder.Entity<labtestdetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetPDFReportTestDetails");
            });

            modelBuilder.Entity<IntegrationVisitDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetIntegrationVisitDetails");
            });
            modelBuilder.Entity<IntegrationVisitTestDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetIntegrationVisitTestDetails");
            });

            modelBuilder.Entity<MassRegistrationResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_UpdateLabAccessionNoMassRegistration");
            });


            modelBuilder.Entity<MassRegistrationResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_UpdateLabAccessionNoDownTimeOrder");
            });

            modelBuilder.Entity<LabReportTestDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_GetResultReport");
                entity.Ignore("RowNo");
                entity.Ignore("PatientNo");
                entity.Ignore("PatientID");
                entity.Ignore("FullName");
                entity.Ignore("ExtenalVisitID");
                entity.Ignore("PhysicianName");
                entity.Ignore("IsAttachment");
                entity.Ignore("LLColumn");
                entity.Ignore("HLColumn");
                entity.Ignore("TestInterNotes");
                entity.Ignore("GroupInterNotes");
                entity.Ignore("HeaderDisplayText");
                entity.Ignore("EnteredBy");
                entity.Ignore("ValidatedBy");
                entity.Ignore("ApprovedBy");
                entity.Ignore("ApprovedOn");
                entity.Ignore("PrintedOn");
                entity.Ignore("HeaderPath");
                entity.Ignore("FooterPath");
                entity.Ignore("Sign1Path");
                entity.Ignore("Sign1Name");
                entity.Ignore("Sign1Desc");
                entity.Ignore("Sign2Path");
                entity.Ignore("Sign2Name");
                entity.Ignore("Sign2Desc");
                entity.Ignore("Sign3Path");
                entity.Ignore("Sign3Name");
                entity.Ignore("Sign3Desc");
                entity.Ignore("IsLogo");
                entity.Ignore("IsDraft");
                entity.Ignore("GroupLanguageName");
                entity.Ignore("TestLanguageName");
                entity.Ignore("SubTestLanguageName");
                entity.Ignore("DepLanguageText");
                entity.Ignore("PackageCode");
                entity.Ignore("NRICNo"); 
                entity.Ignore("WardNo");
                entity.Ignore("ReportHeader");
                entity.Ignore("IncompleteTests");
                entity.Ignore("IsGrpTestInter");
                entity.Ignore("GroupInter");
                entity.Ignore("GenOn");
                entity.Ignore("RHNumber");
                entity.Ignore("AddendumWord");
            });
        }

    }


}
