using DEV.Common;
using DEV.Model.Admin;
using DEV.Model.FrontOffice.PatientDue;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;

namespace DEV.Model.EF
{
    public partial class LIMSContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public LIMSContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public LIMSContext(DbContextOptions<LIMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblDepartment> TblDepartment { get; set; }

        public virtual DbSet<GetMaindepartment> GetDepartmentDetail { get; set; }
        public virtual DbSet<TblIPSetting> TblIPSettings { get; set; }
        public virtual DbSet<TblRC> TblRCs { get; set; }
        public virtual DbSet<RCPriceList> RCPriceLists { get; set; }
        public virtual DbSet<GetIPSettingResponse> GetIPSettingMasterDTO { get; set; }
        public virtual DbSet<GetRCMasterResponse> GetRCMasterDTO { get; set; }
        public virtual DbSet<TblSample> TblSamples { get; set; }
        public virtual DbSet<GetManageSampleDTO> GetManageSampleDTO { get; set; }
        public virtual DbSet<GetSlidePrintingResponse> GetSlidePrintingDTO { get; set; }
        public virtual DbSet<CreateManageSampleResponse> CreateManageSamples { get; set; }
        public virtual DbSet<CreateOutSourceResponse> CreateSampleOutSource { get; set; }
        public virtual DbSet<CreateOutSourceResponse> CreateResultACK { get; set; }
        public virtual DbSet<PatientInfoResponse> GetPatientInfoDTO { get; set; }
        public virtual DbSet<SampleReportResponse> GetSampleReportResponse { get; set; }
        public virtual DbSet<PatientInfoListResponse> GetPatientListResponse { get; set; }
        public virtual DbSet<GetSampleOutsourceResponse> GetSampleOutSourceDTO { get; set; }
        public virtual DbSet<GetSampleOutSourceHistory> GetSampleOutSourceHistoryDTO { get; set; }
        public virtual DbSet<GetSampleTransferResponse> GetSampleTransferDTO { get; set; }
        public virtual DbSet<GetbranchSampleReceiveResponse> GetbranchSampleReceiveDTO { get; set; }
        public virtual DbSet<GetSampleOutsourceResponse> GetResultACKDTO { get; set; }
        public virtual DbSet<ExternalOrderDTO> GetInterfaceTest { get; set; }
        public virtual DbSet<WorkListResponse> GetWorkListDTO { get; set; }
        public virtual DbSet<HistoWorlkListRes> GetHistoWorkListDTO { get; set; }
        public virtual DbSet<TATResponse> GetTATReportDTO { get; set; }
        public virtual DbSet<TATResponseNew> GetTATReportNewDTO { get; set; }
        public virtual DbSet<TATReportDetailsResponse> GetTATReportDetails { get; set; }
        public virtual DbSet<CustomSearchResponse> GetCustomSearchDTO { get; set; }
        public virtual DbSet<TblReportMaster> TblReportMaster { get; set; }
        public virtual DbSet<PatientDueResponse> GetPatientDueInfoDTO { get; set; }
        public virtual DbSet<CreatePatientDueResponse> InsertPatientDueDTO { get; set; }
        public virtual DbSet<EditPatientResponse> UpdatePatientDetailsDTO { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<GetPatientDetailsResponse> GetPatientDetailsDTO { get; set; }
        public virtual DbSet<GetEditPatientDetailsResponse> GetEditPatientDetailsDTO { get; set; }
        public virtual DbSet<GetEditPatientDetailsResponse> GetEditBillingPatientDetailsDTO { get; set; }
        public virtual DbSet<GetEditBillPaymentDetails> GetEditBillPaymentDetailsDTO { get; set; }
        public virtual DbSet<GetBillInvoiceExists> GetBillInvoiceExists { get; set; }
        public virtual DbSet<SampleActionDTO> SampleActionDTO { get; set; }
        public virtual DbSet<CreateSampleActionResponse> InsertSamplesAcknowledgement { get; set; }
        public virtual DbSet<CommonAdminResponse> DeleteVisitIdDTO { get; set; }
        public virtual DbSet<DashBoardResponse> GetDashBoardsDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> UpdateCustomerDetailsDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> UpdateOrderDatesDTO { get; set; }
        public virtual DbSet<SearchVisitDetailsResponse> SearchVisitIdDTO { get; set; }
        public virtual DbSet<SearchUpdateDatesResponse> SearchUpdateDatesDTO { get; set; }
        public virtual DbSet<rescheckExists> checkExists { get; set; }
        public virtual DbSet<TblUserSession> TblUserSession { get; set; }
        public virtual DbSet<CreateSampleTransferResponse> CreateSampleTransfer { get; set; }
        public virtual DbSet<ExternalResultResponseDTO> AckInterface { get; set; }
        public virtual DbSet<dblCancelVisit> GetPatientCancelTestInfo { get; set; }
        public virtual DbSet<rtnCancelTest> InsertCancelTest { get; set; }
        public virtual DbSet<LstSearch> GetArchivePatientDTO { get; set; }
        public virtual DbSet<GetArchivePatientResponse> GetArchivePatientDetailsDTO { get; set; }
        public virtual DbSet<GetICMRResponse> GetICMRResponseDTO { get; set; }
        public virtual DbSet<AuditLogDTO> AuditLogDTO { get; set; }
        public virtual DbSet<AuditHistory> AuditHistory { get; set; }
        public virtual DbSet<UpdateMasterSyncData> UpdateBillMasterData { get; set; }
        public virtual DbSet<TblRoute> TblRoute { get; set; }
        public virtual DbSet<BarcodeResult> Barcoderesult { get; set; }
        public virtual DbSet<CreatePatientDueResponse> InsertBulkPatientDueDTO { get; set; }
        public virtual DbSet<PhysicianDetailsResponse> GetPhysicianDetailsDTO { get; set; }
        public virtual DbSet<GetMultiplsSampleResponse> GetMultiplsSampleByTestId { get; set; }
        public virtual DbSet<GetManageOptionalResponse> ManageOptionalTestPackage { get; set; }
        public virtual DbSet<UpdateRefRangeResponse> UpdateMultiSampleRefRange { get; set; }
        public virtual DbSet<ReasonDetailsResponse> GetServiceRejectReason { get; set; }
        public virtual DbSet<PatientsMasterResponse> GetPatientMaster { get; set; }
        public virtual DbSet<rtnpatient> SavePatientsMaster { get; set; }
        public virtual DbSet<Tblspecialization> Getspecialization { get; set; }
        public virtual DbSet<SpecializationMasterResponse> Insertspecialization { get; set; }
        public virtual DbSet<CheckMasterNameExistsResponse> Checkspecialization { get; set; }
        public virtual DbSet<TblContainer> Getcontainer { get; set; }
        public virtual DbSet<ContainerMasterResponse> Insertcontainer { get; set; }
        public virtual DbSet<TblMainDepartment> GetMainmaster { get; set; }
        public virtual DbSet<MainDepartmentMasterResponse> InsertMainDepartment { get; set; }
        public virtual DbSet<TblTerms> GetTermsmaster { get; set; }
        public virtual DbSet<Termsmasterresponse> InsertTermsmaster { get; set; }
        public virtual DbSet<SearchBarcodeResponse> SearchByBarcode { get; set; }
        public virtual DbSet<AdvancePaymentList> AdvancePaymentList { get; set; }
        public virtual DbSet<AdvancePaymentListResponse> AdvancePaymentListRequest { get; set; }
        public virtual DbSet<TblPack> Getpack { get; set; }
        public virtual DbSet<PackMasterResponse> Insertpack { get; set; }
        public virtual DbSet<TblTax> Gettax { get; set; }
        public virtual DbSet<TaxMasterResponse> Inserttax { get; set; }
        public virtual DbSet<TblSubtestheader> Getheader { get; set; }
        public virtual DbSet<SubtestheaderMasterResponse> Insertheader { get; set; }
        public virtual DbSet<TblTitle> GetTitle { get; set; }
        public virtual DbSet<Titlemasterresponse> InsertTitle { get; set; }
        public virtual DbSet<StoreVendorMaster> InsertVendorMaster { get; set; }
        public virtual DbSet<ExternalPatientEditResult> ExternalPatientEditResult { get; set; }
        public virtual DbSet<InsertTariffMasterResponse> InsertIPSetting { get; set; }
        public virtual DbSet<CashExpenseDTO> GetCashExpenses { get; set; }
        public virtual DbSet<InsertCashExpenseDTO> InsertCashExpenses { get; set; }
        public virtual DbSet<TblAnalyzer> TblAnalyzer { get; set; }
        public virtual DbSet<Tblfav> Tblfav { get; set; }
        public virtual DbSet<Tblgroup> Tblgroup { get; set; }
        public virtual DbSet<Tblpack> Tblpack { get; set; }
        public virtual DbSet<TblSample> GetSampleDetails { get; set; }
        public virtual DbSet<sampleMasterResponse> InsertSampleDetails { get; set; }
        public virtual DbSet<InsertTariffMasterResponse> InsertRcMaster { get; set; }
        public virtual DbSet<ResponseDataScrollText> SearchScrollTextDTO { get; set; }
        public virtual DbSet<PaymentMode> GetPaymentMode { get; set; }
        public virtual DbSet<SavePaymentModeResponse> UpdateVisitPaymentModes { get; set; }
        public virtual DbSet<TblAnalyzerdata> InsertAnalyzerDetails { get; set; }
        public virtual DbSet<WorkListHistoryRes> InsertWorkListHistory { get; set; }
        public virtual DbSet<GetWorkListHistoryRes> GetWorkListHistory { get; set; }
        public virtual DbSet<UserDeptmentDetails> GetDeptDetails { get; set; }
        public virtual DbSet<SingleTestCheckRes> getSubtestCheck { get; set; }
        public virtual DbSet<DenguTestRes> getDenguTestDetails { get; set; }
        public virtual DbSet<GetSlidePrintPatientDetailsResponse> GetSlidePrintingPatientDTO { get; set; }
        public virtual DbSet<CommonTokenResponse> GetGenerateRCHNoDTO { get; set; }
        public virtual DbSet<ExistingRCHNoResponse> GetExistngRCHNoDTO { get; set; }
        public virtual DbSet<GetBulkSlidePrintingDetails> GetBulkSlidePrintingDTO { get; set; }
        public virtual DbSet<PatientmergeResponseDTO> UpdateMergePatient { get; set; }
        public virtual DbSet<DepartMentLangCodeRes> InsertLangCodeDeptMasters { get; set; }
        public virtual DbSet<GetSampleResponse> GetPatientSampleInfo { get; set; }
        public virtual DbSet<GetDeptLangCodeRes> GetLangCodeDeptMasters { get; set; }
        public virtual DbSet<InsertDeptRes> InsertDeptMaster { get; set; }
        public virtual DbSet<SpecimenMappingoutput> SpecimenMappingoutput { get; set; }
        public virtual DbSet<SpecimenMappingResponse> SpecimenMappingResponse { get; set; }
        public virtual DbSet<EditPatientResponseNew> UpdatePatientDetailsDTONew { get; set; }
        public virtual DbSet<BarcodePrintResponse> BarcodePrintInfo { get; set; }
        public virtual DbSet<SearchBranchSampleBarcodeResponse> SearchBranchSampleByBarcode { get; set; }
        public virtual DbSet<GetPatientVisitActionHistoryResponse> GetPatientVisitActionHistory { get; set; }
        public virtual DbSet<BranchSampleActionDTO> BranchSampleActionDTO { get; set; }
        public virtual DbSet<GetReqExpensesResponse> GetReqExpenses { get; set; }
        public virtual DbSet<InsertCashExpenseDTO> ApproveExpenses { get; set; }
        public virtual DbSet<GetReqCancelResponse> GetRefundCancelRequest { get; set; }
        public virtual DbSet<UpdateReqCancelResponse> ApproveRefundCancel { get; set; }
        public virtual DbSet<PrePrintBarcodeOrderResponse> PrePrintBarcodeOrderrequest { get; set; }
        public virtual DbSet<InventoryDashBoardRes> GetInventoryDashBoardDTO { get; set; }
        public virtual DbSet<GetHcDocumentsDetailsResponse> GetHcDocumentsDetails { get; set; }
        public virtual DbSet<Routelst> GetrouteMaster { get; set; }
        public virtual DbSet<RouteMasterResponse> InsertRouteMaster { get; set; }
        public virtual DbSet<responsehistory> DeleteHistory { get; set; }

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

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerNo)
                    .HasName("PK__tbl_Cust__A4AFBF63B097A6A5");

                entity.ToTable("tbl_Customer");

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreditLimit).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerMobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Gstno)
                    .HasColumnName("GSTNO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idtype)
                    .HasColumnName("IDType")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsReportSms).HasColumnName("IsReportSMS");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentNo)
                    .HasName("PK__tbl_Depa__B207A396BDC21C2A");

                entity.ToTable("tbl_Department");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentDisplayText)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.MainDeptNo);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptSequenceNo);

                entity.Property(e => e.IsSample)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.IsHisto)
                  .HasDefaultValueSql("((1))");
                entity.Property(e => e.IsCytology)
                  .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DoctorName1);
                entity.Property(e => e.DoctorSign1);
                entity.Property(e => e.DoctorDescription1);
                entity.Property(e => e.DoctorName2);
                entity.Property(e => e.DoctorSign2);
                entity.Property(e => e.DoctorDescription2);
                entity.Property(e => e.DoctorName3);
                entity.Property(e => e.DoctorSign3);
                entity.Property(e => e.DoctorDescription3);
            });

            modelBuilder.Entity<TblIPSetting>(entity =>
            {
                entity.HasKey(e => e.IPSettingNo)
                    .HasName("PK__tbl_IPSet__B207A396BDC21C2A");

                entity.ToTable("tbl_IPSetting");
                entity.Property(e => e.PhysicianNo);
                entity.Property(e => e.DepartmentNo);
                entity.Property(e => e.ServiceNo);
                entity.Property(e => e.ServiceType);
                entity.Property(e => e.MRPPrice).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.IPPrice).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.IPPercentage).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.Status);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.VenueNo);
                entity.Property(e => e.VenueBranchNo);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.ModifiedBy);
            });

            modelBuilder.Entity<RCPriceList>(entity =>
            {
                entity.HasKey(e => e.RCPNo)
                    .HasName("PK__tbl_RCP__B207A396BDC21C2A");

                entity.ToTable("tbl_RCPriceList");
                entity.Property(e => e.RCNo);
                entity.Property(e => e.DepartmentNo);
                entity.Property(e => e.ServiceNo);
                entity.Property(e => e.ServiceType);
                entity.Property(e => e.MRPPrice).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.IPPrice).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.IPPercentage).HasColumnType("decimal(12, 2)");
                entity.Property(e => e.Status);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.VenueNo);
                entity.Property(e => e.VenueBranchNo);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.ModifiedBy);
            });

            modelBuilder.Entity<TblRC>(entity =>
            {
                entity.HasKey(e => e.RCNo)
                    .HasName("PK__tbl_RC__B207A396BDC21C2A");

                entity.ToTable("tbl_RC");
                entity.Property(e => e.RCNo);
                entity.Property(e => e.Status);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.VenueNo);
                entity.Property(e => e.VenueBranchNo);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.ModifiedBy);
            });

            modelBuilder.Entity<TblRoute>(entity =>
            {
                entity.HasKey(e => e.RouteNo)
                .HasName("PK__tbl_Rout__B207A396BDC21C2A");

                entity.ToTable("tbl_Route");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RouteCode)
                    .HasMaxLength(20);

                entity.Property(e => e.Description)
                    .HasMaxLength(100);

                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SequenceNo);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GetManageSampleDTO>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_Getmanagesample");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<GetSlidePrintingResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_GetSlidePrintDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CreateManageSampleResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_InsertSamples");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<PatientInfoResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientInfo");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });
            
            modelBuilder.Entity<SampleReportResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetSampleTransferReport");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<PatientInfoListResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientListInfo");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<CustomSearchResponse>(entity =>
            {
                entity.HasKey(e => e.VisitNo);
                entity.ToTable("Pro_GetCommonSearch");
                entity.Property(e => e.VisitNo).HasColumnName("VisitNo");
            });

            modelBuilder.Entity<PatientDueResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientDueInfo");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<dblCancelVisit>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetPatientCancelTestInfo");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<rtnCancelTest>(entity =>
            {
                entity.HasKey(e => e.cancelTestNo);
                entity.ToTable("pro_InsertCancelTest");
                entity.Property(e => e.cancelTestNo).HasColumnName("cancelTestNo");
            });

            modelBuilder.Entity<CreateSampleActionResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertSamplesAcknowledgement");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<TblReportMaster>(entity =>
            {
                entity.HasKey(e => e.ReportNo)
                    .HasName("PK__tbl_Repo__D5BE74AAC5D70E40");

                entity.ToTable("tbl_ReportMaster");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProcedureName)
                   .HasMaxLength(300)
                   .IsUnicode(false);

                entity.Property(e => e.ExportPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ExportURL)
               .HasMaxLength(1000)
               .IsUnicode(false);

                entity.Property(e => e.Parameterstring)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ReportKey)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreatePatientDueResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertDueClearence");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<CreatePatientDueResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_BulkInsertDueClearence");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<EditPatientResponse>(entity =>
            {
                entity.HasKey(e => e.statusCode);
                entity.ToTable("pro_UpdateEditPatientDetails");
                entity.Property(e => e.statusCode).HasColumnName("statusCode");
            });

            modelBuilder.Entity<TblSample>(entity =>
            {
                entity.HasKey(e => e.SampleNo)
                    .HasName("PK__tbl_Samp__8B99859117B34B22");

                entity.ToTable("tbl_Sample");

                entity.Property(e => e.SampleName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.SampleCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SampleDisplayText)
                   .IsRequired()
                   .HasMaxLength(100);

                entity.Property(e => e.SampleVolume)
                     .HasMaxLength(50);

                entity.Property(e => e.Status)
                   .IsRequired()
                   .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive)
               .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<GetPatientDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetPatientDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<SampleActionDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetSampleActionDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<WorkListResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetWorkListInfo");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<HistoWorlkListRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetHistoWorkListInfo");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_DeleteVisit");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_UpdateOrderDates");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<SearchVisitDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetVisitDetails");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<SearchUpdateDatesResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_SearchUpdateDates");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<ExternalOrderDTO>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetExternalOrder");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<GetSampleOutsourceResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetSampleOutSource");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<GetSampleOutSourceHistory>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("Pro_GetSampleOutSourceHistory");
                entity.Property(e => e.rowNo).HasColumnName("row_No");
            });

            modelBuilder.Entity<CreateOutSourceResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertSampleOutSource");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<GetSampleOutsourceResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetResultACK");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<CreateOutSourceResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertResultACK");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<rescheckExists>(entity =>
            {
                entity.ToTable("Pro_CheckExists");
                entity.HasKey(e => new { e.patientVisitNo, e.visitID });
                entity.Property(e => e.patientVisitNo).HasColumnName("patientVisitNo");
                entity.Property(e => e.visitID).HasColumnName("visitID");
            });

            modelBuilder.Entity<TATResponse>(entity =>
            {
                entity.HasKey(e => e.VisitNo);
                entity.ToTable("Pro_GetTATReport");
                entity.Property(e => e.VisitNo).HasColumnName("VisitNo");
            });

            modelBuilder.Entity<TblUserSession>(entity =>
            {
                entity.HasKey(e => e.UserSessionNo)
                    .HasName("PK__tbl_User__E7346D0F5745E60F");

                entity.ToTable("tbl_UserSession");

                entity.Property(e => e.ClientSysteminfo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogInDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogOutdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TATResponseNew>(entity =>
            {
                entity.HasKey(e => e.departmentNo);
                entity.ToTable("Pro_GetTATReportNew");
                entity.Property(e => e.departmentNo).HasColumnName("DepartmentNo");
            });

            modelBuilder.Entity<TATReportDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_TATReport_Details");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<GetSampleTransferResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetSampleTransfer");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<CreateSampleTransferResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertSampleTransfer");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_ClientChanges");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<ExternalResultResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("Pro_ACKInterfaceTest");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<GetbranchSampleReceiveResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetBranchReceive");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });

            modelBuilder.Entity<GetEditPatientDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetEditPatientDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
                entity.Property(e => e.SNetAmount).IsRequired(false);
            });

            modelBuilder.Entity<GetEditBillPaymentDetails>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetEditPatientPaymentDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<GetEditPatientDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetEditBillingPatientDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
                entity.Property(e => e.SNetAmount).IsRequired(false);
            });

            modelBuilder.Entity<DashBoardResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetDashBoard");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<LstSearch>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_SearchArchiveVisit");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<GetArchivePatientResponse>(entity =>
            {
                entity.HasKey(e => e.PatientVisitNo);
                entity.ToTable("pro_GetArchiveVisit");
                entity.Property(e => e.PatientVisitNo).HasColumnName("PatientVisitNo");
            });

            modelBuilder.Entity<GetICMRResponse>(entity =>
            {
                entity.HasKey(e => e.RowNum);
                entity.ToTable("Pro_GetICMRResultResponse");
                entity.Property(e => e.RowNum).HasColumnName("RowNum");
            });

            modelBuilder.Entity<AuditLogDTO>(entity =>
            {
                entity.HasKey(e => e.AuditLogNo);
                entity.ToTable("Pro_GetAuditLogReport");
                entity.Property(e => e.AuditLogNo).HasColumnName("AuditLogNo");
            });

            modelBuilder.Entity<AuditHistory>(entity =>
            {
                entity.HasKey(e => e.AuditLogNo);
                entity.ToTable("Pro_GetAuditHistory");
                entity.Property(e => e.AuditLogNo).HasColumnName("AuditLogNo");
            });

            modelBuilder.Entity<GetMultiplsSampleResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetMultiSampleByTestId");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<UpdateRefRangeResponse>(entity =>
            {
                entity.HasKey(e => e.TestNo);
                entity.ToTable("pro_UpdateMultiSampleRefRange");
                entity.Property(e => e.TestNo).HasColumnName("TestNo");
            });

            modelBuilder.Entity<ReasonDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_GetRejectedReasonByVisit");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<PhysicianDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.PhysicianNo);
                entity.ToTable("Pro_GetPhysicianDetails");
                entity.Property(e => e.PhysicianNo).HasColumnName("PhysicianNo");
            });

            modelBuilder.Entity<UpdateMasterSyncData>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_UpdateBillMasterdata");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<BarcodeResult>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_ValidateBarcodeNo");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<PatientsMasterResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPatientMaster");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<rtnpatient>(entity =>
            {
                entity.HasKey(e => e.PatientNo);
                entity.ToTable("pro_InsertPatientMaster");
                entity.Property(e => e.PatientNo).HasColumnName("PatientNo");
            });

            modelBuilder.Entity<GetMaindepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentNo);
                entity.ToTable("Pro_Getdept");
                entity.Property(e => e.DepartmentNo).HasColumnName("DepartmentNo");
            });

            modelBuilder.Entity<Tblspecialization>(entity =>
            {
                entity.HasKey(e => e.specializationNo);
                entity.ToTable("pro_GetSpecialization");
                entity.Property(e => e.specializationNo).HasColumnName("specializationNO");
            });

            modelBuilder.Entity<SpecializationMasterResponse>(entity =>
            {
                entity.HasKey(e => e.specializationNo);
                entity.ToTable("pro_InsertSpecialization");
                entity.Property(e => e.specializationNo).HasColumnName("specializationNO");
            });

            modelBuilder.Entity<CheckMasterNameExistsResponse>(entity =>
            {
                entity.HasKey(e => e.avail);
                entity.ToTable("pro_CheckMasterNameExists");
                entity.Property(e => e.avail).HasColumnName("avail");
            });

            modelBuilder.Entity<SearchBarcodeResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_SearchByBarcode");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<TblContainer>(entity =>
            {
                entity.HasKey(e => e.containerNo);
                entity.ToTable("pro_GetContainermaster");
                entity.Property(e => e.containerNo).HasColumnName("containerNo");
            });

            modelBuilder.Entity<ContainerMasterResponse>(entity =>
            {
                entity.HasKey(e => e.containerNo);
                entity.ToTable("pro_InsertContainer");
                entity.Property(e => e.containerNo).HasColumnName("containerNo");
            });

            modelBuilder.Entity<TblMainDepartment>(entity =>
            {
                entity.HasKey(e => e.maindeptno);
                entity.ToTable("pro_GetMainDepartment");
                entity.Property(e => e.maindeptno).HasColumnName("maindeptno");
            });

            modelBuilder.Entity<MainDepartmentMasterResponse>(entity =>
            {
                entity.HasKey(e => e.maindeptno);
                entity.ToTable("pro_InsertMainDepartment");
                entity.Property(e => e.maindeptno).HasColumnName("maindeptno");
            });

            modelBuilder.Entity<AdvancePaymentList>(entity =>
            {
                entity.HasKey(e => e.Rowno);
                entity.ToTable("pro_GetAdvancePayment");
                entity.Property(e => e.Rowno).HasColumnName("Rowno");
            });

            modelBuilder.Entity<AdvancePaymentListResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_InsertAdvancePayment");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<TblPack>(entity =>
            {
                entity.HasKey(e => e.packNo);
                entity.ToTable("pro_Getpackmaster");
                entity.Property(e => e.packNo).HasColumnName("packNo");
            });

            modelBuilder.Entity<PackMasterResponse>(entity =>
            {
                entity.HasKey(e => e.packNo);
                entity.ToTable("pro_InsertPackmaster");
                entity.Property(e => e.packNo).HasColumnName("packNo");
            });

            modelBuilder.Entity<TblTax>(entity =>
            {
                entity.HasKey(e => e.taxNo);
                entity.ToTable("pro_GetTaxMaster");
                entity.Property(e => e.taxNo).HasColumnName("taxNo");
            });

            modelBuilder.Entity<TaxMasterResponse>(entity =>
            {
                entity.HasKey(e => e.taxNo);
                entity.ToTable("pro_InsertTaxMaster");
                entity.Property(e => e.taxNo).HasColumnName("taxNo");
            });

            modelBuilder.Entity<TblTerms>(entity =>
            {
                entity.HasKey(e => e.termsNo);
                entity.ToTable("pro_GetTerms");
                entity.Property(e => e.termsNo).HasColumnName("termsNo");
            });

            modelBuilder.Entity<Termsmasterresponse>(entity =>
            {
                entity.HasKey(e => e.termsNo);
                entity.ToTable("pro_InsertTerms");
                entity.Property(e => e.termsNo).HasColumnName("termsNo");
            });

            modelBuilder.Entity<TblSubtestheader>(entity =>
            {
                entity.HasKey(e => e.headerNo);
                entity.ToTable("pro_GetSubtestheadermaster");
                entity.Property(e => e.headerNo).HasColumnName("headerNo");
            });

            modelBuilder.Entity<SubtestheaderMasterResponse>(entity =>
            {
                entity.HasKey(e => e.HeaderNo);
                entity.ToTable("pro_InsertSubtestheader");
                entity.Property(e => e.HeaderNo).HasColumnName("headerNo");
            });

            modelBuilder.Entity<TblTitle>(entity =>
            {
                entity.HasKey(e => e.commonBranchNo);
                entity.ToTable("pro_pro_GetCommanMaster");
                entity.Property(e => e.commonBranchNo).HasColumnName("commonBranchNo");
            });

            modelBuilder.Entity<Titlemasterresponse>(entity =>
            {
                entity.HasKey(e => e.commonBranchNo);
                entity.ToTable("pro_InsertCommanmaster");
                entity.Property(e => e.commonBranchNo).HasColumnName("commonBranchNo");
            });

            modelBuilder.Entity<StoreVendorMaster>(entity =>
            {
                entity.HasKey(e => e.vendorno);
                entity.ToTable("pro_InsertVendormaster");
                entity.Property(e => e.vendorno).HasColumnName("vendorno");
            });

            modelBuilder.Entity<ExternalPatientEditResult>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_EditPatientmaster");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<InsertTariffMasterResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("pro_InsertIPSettingDetails");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<CashExpenseDTO>(entity =>
            {
                entity.HasKey(e => e.ExpenseEntryNo);
                entity.ToTable("Pro_GetCashExpesnses");
                entity.Property(e => e.ExpenseEntryNo).HasColumnName("ExpenseEntryNo");
            });

            modelBuilder.Entity<InsertCashExpenseDTO>(entity =>
            {
                entity.HasKey(e => e.ExpenseEntryNo);
                entity.ToTable("Pro_InsertCashExpesnses");
                entity.Property(e => e.ExpenseEntryNo).HasColumnName("ExpenseEntryNo");
            });

            modelBuilder.Entity<TblAnalyzer>(entity =>
            {
                entity.HasKey(e => e.AnalyzerMasterNo)
                .HasName("PK_tbl_AnalyzerMaster_AzMNo");

                entity.ToTable("tbl_AnalyzerMaster");
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.AnalyzerMasterNo)
                    .HasMaxLength(20);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SerialNo);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Tblfav>(entity =>
            {
                entity.HasKey(e => e.ServiceNo)
                .HasName("PK_tbl_FavoriteServices_FSNo");

                entity.ToTable("tbl_FavoriteServices");
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SequenceNo);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasDefaultValueSql("(('T'))");
            });

            modelBuilder.Entity<Tblgroup>(entity =>
            {
                entity.HasKey(e => e.GroupNo)
                .HasName("PK_tbl_Group");

                entity.ToTable("tbl_Group");
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Tblpack>(entity =>
            {
                entity.HasKey(e => e.PackageNo)
                .HasName("PK_tbl_Package");

                entity.ToTable("tbl_Package");
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblSample>(entity =>
            {
                entity.HasKey(e => e.SampleNo);
                entity.ToTable("pro_GetSample");
                entity.Property(e => e.SampleNo).HasColumnName("SampleNo");
            });

            modelBuilder.Entity<sampleMasterResponse>(entity =>
            {
                entity.HasKey(e => e.SampleNo);
                entity.ToTable("pro_InsertSample");
                entity.Property(e => e.SampleNo).HasColumnName("SampleNo");
            });

            modelBuilder.Entity<InsertTariffMasterResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("pro_InsertRcMaster");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<ResponseDataScrollText>(entity =>
            {
                entity.HasKey(e => e.textInformation);
                entity.ToTable("Pro_GetScrollTextDetails");
                entity.Property(e => e.textInformation).HasColumnName("textInformation");
            });

            modelBuilder.Entity<TblAnalyzerdata>(entity =>
            {
                entity.HasKey(e => e.analyzerMasterNo);
                entity.ToTable("pro_InsertAnalyzerDetails");
                entity.Property(e => e.analyzerMasterNo).HasColumnName("AnalyzerMasterNo");
            });

            modelBuilder.Entity<WorkListHistoryRes>(entity =>
            {
                entity.HasKey(e => e.WorklistNo);
                entity.ToTable("pro_InsertWorklistHistory");
                entity.Property(e => e.WorklistNo).HasColumnName("WorklistNo");
            });

            modelBuilder.Entity<GetWorkListHistoryRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetWorklistHistory");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<UserDeptmentDetails>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("pro_GetWorklistUsetDept");
                entity.Property(e => e.id).HasColumnName("id");
            });

            modelBuilder.Entity<SingleTestCheckRes>(entity =>
            {
                entity.HasKey(e => e.subTestCheck);
                entity.ToTable("Pro_GetsubTestCheck");
                entity.Property(e => e.subTestCheck).HasColumnName("subTestCheck");
            });

            modelBuilder.Entity<DenguTestRes>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("Pro_GetDenguTestDetails");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<GetSlidePrintPatientDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_GetSlidePrintPatientDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonTokenResponse>(entity =>
            {
                entity.HasKey(e => e.responseValue);
                entity.ToTable("pro_GenerateSlidePrintNumber");
                entity.Property(e => e.responseValue).HasColumnName("responseValue");
            });

            modelBuilder.Entity<ExistingRCHNoResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_GetExistingRCHNoDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<GetBulkSlidePrintingDetails>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetBulkSlidePrintDetails");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<PatientmergeResponseDTO>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_UpdatePatientMerge");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<DepartMentLangCodeRes>(entity =>
            {
                entity.HasKey(e => e.LanguageId);
                entity.ToTable("Pro_InsertLanguageCodeDepartment");
                entity.Property(e => e.LanguageId).HasColumnName("LanguageId");
            });

            modelBuilder.Entity<GetDeptLangCodeRes>(entity =>
            {
                entity.HasKey(e => e.LanguageCode);
                entity.ToTable("Pro_GetLanguageCodeDepartment");
                entity.Property(e => e.LanguageCode).HasColumnName("LanguageCode");
            });

            modelBuilder.Entity<GetSampleResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetPatientSampleInfo");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<InsertDeptRes>(entity =>
            {
                entity.HasKey(e => e.DeptNo);
                entity.ToTable("pro_InsertDeptMaster");
                entity.Property(e => e.DeptNo).HasColumnName("DeptNo");
            });

            modelBuilder.Entity<SpecimenMappingoutput>(entity =>
            {
                entity.HasKey(e => e.mediastring);
                entity.ToTable("Pro_GetSpecimenMediaMapping");
                entity.Property(e => e.mediastring).HasColumnName("mediastring");
            });

            modelBuilder.Entity<SpecimenMappingResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertSpecimenMediaMapping");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<GetManageOptionalResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_ManageOptionalTest");
            });

            modelBuilder.Entity<GetBillInvoiceExists>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_CheckBillExistsInInvoice");
            });

            modelBuilder.Entity<EditPatientResponseNew>(entity =>
            {
                entity.HasKey(e => e.statusCode);
                entity.ToTable("pro_UpdateEditSampleDetailsNew");
                entity.Property(e => e.statusCode).HasColumnName("statusCode");
            });

            modelBuilder.Entity<BarcodePrintResponse>(entity =>
            {
                entity.HasKey(e => e.SampleNo);
                entity.ToTable("Pro_GetDetailsToPrintBarcode");
                entity.Property(e => e.AccessionNo).HasColumnName("AccessionNo");
            });

            modelBuilder.Entity<BranchSampleActionDTO>(entity =>
            {
                entity.HasKey(e => e.OrderTransactionNo);
                entity.ToTable("pro_GetBranchSampleActionDetails");
                entity.Property(e => e.OrderTransactionNo).HasColumnName("OrderTransactionNo");
            });

            modelBuilder.Entity<SearchBranchSampleBarcodeResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_SearchBranchSampleByBarcode");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<GetPatientVisitActionHistoryResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_GetPatientVisitActionHistory");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Pro_GetVisitPaymentModes");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<SavePaymentModeResponse>(entity =>
            {
                entity.HasKey(e => e.Response);
                entity.ToTable("Pro_UpdateVisitPaymentModes");
                entity.Property(e => e.Response).HasColumnName("Response");
            });

            modelBuilder.Entity<GetReqExpensesResponse>(entity =>
            {
                entity.HasKey(e => e.ExpenseEntryNo);
                entity.ToTable("Pro_GetReqExpenses");
                entity.Property(e => e.ExpenseEntryNo).HasColumnName("ExpenseEntryNo");
            });

            modelBuilder.Entity<InsertCashExpenseDTO>(entity =>
            {
                entity.HasKey(e => e.ExpenseEntryNo);
                entity.ToTable("Pro_ApproveExpenses");
                entity.Property(e => e.ExpenseEntryNo).HasColumnName("ExpenseEntryNo");
            });

            modelBuilder.Entity<GetReqCancelResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetRefundCancelApproval");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<UpdateReqCancelResponse>(entity =>
            {
                entity.HasKey(e => e.ApprovedStatus);
                entity.ToTable("pro_UpdateRefundCancelApproval");
                entity.Property(e => e.ApprovedStatus).HasColumnName("ApprovedStatus");
            });

            modelBuilder.Entity<PrePrintBarcodeOrderResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetPrePrintBarCodeOrder");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<InventoryDashBoardRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetInventoryDashBoard");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<GetHcDocumentsDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.HCPatientNo);
                entity.ToTable("GetHcDocumentsDetails");
                entity.Property(e => e.HCPatientNo).HasColumnName("HCPatientNo");
            });

            modelBuilder.Entity<Routelst>(entity =>
            {
                entity.HasKey(e => e.RouteNo);
                entity.ToTable("pro_GetRoutemaster");
                entity.Property(e => e.RouteNo).HasColumnName("RouteNo");
            });

            modelBuilder.Entity<RouteMasterResponse>(entity =>
            {
                entity.HasKey(e => e.RouteNo);
                entity.ToTable("pro_Insertroutemaster");
                entity.Property(e => e.RouteNo).HasColumnName("RouteNo");
            });
           
            modelBuilder.Entity<responsehistory>(entity =>
            {
                entity.HasKey(e => e.visitId);
                entity.ToTable("pro_DeleteHistory");
                entity.Property(e => e.visitId).HasColumnName("visitID");
            });
        }
    }

}




