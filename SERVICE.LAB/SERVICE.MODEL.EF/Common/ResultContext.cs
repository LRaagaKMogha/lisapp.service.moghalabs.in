using Service.Common;
using Microsoft.EntityFrameworkCore;

namespace Service.Model.EF
{
    public partial class ResultContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public ResultContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public ResultContext(DbContextOptions<ResultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lstsearchresultvisit> SearchResultVisit { get; set; }
        public virtual DbSet<Lstresultvisitdbl> GetResultVisit { get; set; }
        public virtual DbSet<Deltaresult> GetDeltaresult { get; set; }
        public virtual DbSet<Lsthistorydbl> GetVisitHistoy { get; set; }
        public virtual DbSet<Lstresultdbl> GetResult { get; set; }
        public virtual DbSet<Resultrtn> InsertResult { get; set; }
        public virtual DbSet<Objresultmbdbl> GetResultMB { get; set; }
        public virtual DbSet<Orgtypeantibiotic> GetOrgtypeantibiotic { get; set; }
        public virtual DbSet<Resultrtn> InsertResultMB { get; set; }
        public virtual DbSet<Objresulttemplatedbl> GetResultTemplate { get; set; }
        public virtual DbSet<Resultrtn> InsertResultTemplate { get; set; }
        public virtual DbSet<CustomerMsgDetails> GetCustomerMsgDetails { get; set; }
        public virtual DbSet<ExternalResultResponseDTO> InsertExternalResult { get; set; }
        public virtual DbSet<CreateTemplateResultDTO> GetExternalTemplateResult { get; set; }
        public virtual DbSet<Lstrecalldbl> GetRecall { get; set; }
        public virtual DbSet<RecallDataResponse> InsertRecall { get; set; }
        public virtual DbSet<Objbulkresultdbl> GetBulkResult { get; set; }
        public virtual DbSet<Covidresult> GetCovidWorkOrder { get; set; }
        public virtual DbSet<Resultrtn> InsertCovidWorkOrder { get; set; }
        public virtual DbSet<ApprovalDoctorResponse> GetApprovalDoctorList { get; set; }
        public virtual DbSet<PatientDataImpressionResponse> GetPatientImpressionList { get; set; }
        public virtual DbSet<PatientImpressionResponse> GetPatientImpressionoutput { get; set; }
        public virtual DbSet<Mergeresultresponse> GetMergedResults { get; set; }
        public virtual DbSet<Savemergeresultresponse> InsertMergedResults { get; set; }
        public virtual DbSet<Culturehistoryreponse> GetCultureHistory { get; set; }
        public virtual DbSet<Lstresultdbl> GetAnalyserResult { get; set; }
        public virtual DbSet<Resultrtn> InsertAnalyserResult { get; set; }
        public virtual DbSet<Lstbulkresultdbl> GetBulkEntryResult { get; set; }
        public virtual DbSet<BulkResultSaveResponse> InsertBulkResult { get; set; }
        public virtual DbSet<BulkCultureResultResponse> GetCultureBulkEntryResults { get; set; }
        public virtual DbSet<BulkCultureResultSaveResponse> InsertCultureBulkResult { get; set; }
        public virtual DbSet<ResultforVisitMergeResponse> GetVisitMergeRequest { get; set; }
        public virtual DbSet<InsertVisitMergeResponse> SaveVisitMergeResponse { get; set; }
        public virtual DbSet<Lstresultdbl> GetResultExceptUserMapped { get; set; }
        public virtual DbSet<InvestigationAvailResponse> CheckInvestigationReport { get; set; }
        public virtual DbSet<LogicCommentsRespose> GetLogicComment { get; set; }
        public virtual DbSet<ExternalCultureResultResponseDTO> InsertCultureInterfaceResult { get; set; }
        public virtual DbSet<Extrasubtestflagbasedformularesponse> GetExtrasubtestbasedformula { get; set; }
        public virtual DbSet<Saveinfectioncontroldetresponse> InsertInfectionControlAvailData { get; set; }
        public virtual DbSet<GetOldResultThroughDIResponse> GetOldResultThroughDI { get; set; }
        public virtual DbSet<LstExternalResultCalculation> GetExternalFormulaOrderDetails { get; set; }
        public virtual DbSet<ExternalApprovalResponseDTO> GetExternalApprovalResponseDTO { get; set; }       
        public virtual DbSet<CheckFormulaIsAvailable> CheckFormulaIsAvailable_ForCalculation { get; set; }
        public virtual DbSet<PBFTestResponse> GetPBFAutoComments { get; set; }
        public virtual DbSet<ObjUpdPartialEntryFlagResponse> InsertPartialResultFlag { get; set; }
        public virtual DbSet<PendingVisitdetailsRes> PendingVisitdetailsLst { get; set; }
        public virtual DbSet<OutSourceAPIDTOResponse> GetOutsourceDetailsAPI { get; set; }
        public virtual DbSet<AckOutSourceAPIDTOResponse> AckOutSourceAPIList { get; set; }
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

            modelBuilder.Entity<Lstsearchresultvisit>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_SearchResultVisit");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<Lstresultvisitdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResultVisit");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<Deltaresult>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetDeltaresult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<Lsthistorydbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetVisitHistoy");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<Lstresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<Resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertResult");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<Objresultmbdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResultMB");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<Orgtypeantibiotic>(entity =>
            {
                entity.HasKey(e => e.antibioticno);
                entity.ToTable("pro_GetOrgtypeantibiotic");
                entity.Property(e => e.antibioticno).HasColumnName("antibioticno");
            });

            modelBuilder.Entity<CustomerMsgDetails>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("Pro_GetCustomerNotification");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            
            modelBuilder.Entity<Resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertResultMB");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<Objresulttemplatedbl>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_GetResultTemplate");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<Resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertResultTemplate");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<ExternalResultResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_InsertExternalResults");
                entity.Property(e => e.Status).HasColumnName("Status");
            });

            modelBuilder.Entity<CreateTemplateResultDTO>(entity =>
            {
                entity.HasKey(e => e.orderlistno);
                entity.ToTable("pro_GetExternalTemplateResult");
                entity.Property(e => e.orderlistno).HasColumnName("orderlistno");
            });

            modelBuilder.Entity<Lstrecalldbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetRecallVisit");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<RecallDataResponse>(entity =>
            {
                entity.HasKey(r => r.patientvisitno);
                entity.ToTable("pro_InsertRecall");
                entity.Property(r => r.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<RecallTestDetailsResponse>(entity =>
            {
                entity.HasKey(r => r.orderListNo);
            });

            modelBuilder.Entity<Objbulkresultdbl>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_GetBulkResult_Bk");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<Covidresult>(entity =>
            {
                entity.HasKey(e => e.PatientVisitNo);
                entity.ToTable("pro_GetCovidWorkOrder");
                entity.Property(e => e.PatientVisitNo).HasColumnName("PatientVisitNo");
            });

            modelBuilder.Entity<Resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_GetInsertWorkOrder");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<ApprovalDoctorResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetApprovalDoctors");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<PatientDataImpressionResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetPatientSearchImpression");
                entity.Property(e => e.Row_Num).HasColumnName("Row_num");
            });
            modelBuilder.Entity<PatientImpressionResponse>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientImpression");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
                entity.Property(e => e.raceName).HasColumnName("raceName");
            });
            modelBuilder.Entity<Mergeresultresponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetMergeResult");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<Savemergeresultresponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_InsertMergedResult");
                entity.Property(e => e.status).HasColumnName("Status");
            });
            modelBuilder.Entity<Culturehistoryreponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetCultureHistory");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<Lstresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetAnalyserResult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            modelBuilder.Entity<Resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertAnalyserResult");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<Lstbulkresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetBulkResultEntry");
                entity.Property(e => e.rowno).HasColumnName("RowNo");
            });
            modelBuilder.Entity<BulkResultSaveResponse>(entity =>
            {
                entity.HasKey(e => e.outstatus);
                entity.ToTable("pro_InsertBulkResultEntry");
                entity.Property(e => e.outstatus).HasColumnName("outstatus");
            });
            modelBuilder.Entity<BulkCultureResultResponse>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetBulkResultEntryCulture");
                entity.Property(e => e.rowno).HasColumnName("RowNo");
            });
            modelBuilder.Entity<BulkCultureResultSaveResponse>(entity =>
            {
                entity.HasKey(e => e.outstatus);
                entity.ToTable("pro_InsertCultureBulkResultEntry");
                entity.Property(e => e.outstatus).HasColumnName("outstatus");
            });
            modelBuilder.Entity<ResultforVisitMergeResponse>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("pro_GetResultforVisitMerge");
                entity.Property(e => e.id).HasColumnName("id");
            });
            modelBuilder.Entity<InsertVisitMergeResponse>(entity =>
            {
                entity.HasKey(e => e.OStatus);
                entity.ToTable("pro_InsertResultforVisitMerge");
                entity.Property(e => e.OStatus).HasColumnName("OStatus");
            });
            modelBuilder.Entity<Lstresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResultExceptUserMapped");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            modelBuilder.Entity<InvestigationAvailResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_CheckInfectionControlAvail");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<LogicCommentsRespose>(entity =>
            {
                entity.HasKey(e => e.LogicCommentsId);
                entity.ToTable("pro_GetLogicComments");
                entity.Property(e => e.LogicCommentsId).HasColumnName("LogicCommentsId");
            });
            modelBuilder.Entity<ExternalCultureResultResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_InsertMBInterfaceResults");
                entity.Property(e => e.Status).HasColumnName("Status");
            });
            modelBuilder.Entity<Extrasubtestflagbasedformularesponse>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("pro_GetIndividualTestFormulajson");
                entity.Property(e => e.id).HasColumnName("id");
            });
            modelBuilder.Entity<Saveinfectioncontroldetresponse>(entity =>
            {
                entity.HasKey(e => e.OutStatus);
                entity.ToTable("Pro_InserInfectionControlAvailData");
                entity.Property(e => e.OutStatus).HasColumnName("OutStatus");
            });
            modelBuilder.Entity<GetOldResultThroughDIResponse>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetOldResultThroughDI");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            modelBuilder.Entity<LstExternalResultCalculation>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_Get_OrderDetails_FormulaCalculation");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            modelBuilder.Entity<ExternalApprovalResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_InsertResult_Approval");
                entity.Property(e => e.Status).HasColumnName("Status");
            });
            modelBuilder.Entity<CheckFormulaIsAvailable>(entity =>
            {
                entity.HasKey(e => e.ReturnValue);
                entity.ToTable("pro_check_visit_having_formula_for_calculation");
                entity.Property(e => e.ReturnValue).HasColumnName("ReturnValue");
            });
            modelBuilder.Entity<PBFTestResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_GetPBFAutoCommentResult");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<ObjUpdPartialEntryFlagResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_UpdatePartialResultFlag");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<PendingVisitdetailsRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPendingVisitdetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<OutSourceAPIDTOResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetOutsourceDetails_API");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<AckOutSourceAPIDTOResponse>(entity =>
            {
                entity.HasKey(e => e.OutStatus);
                entity.ToTable("pro_AckOutsourceDetails_API");
                entity.Property(e => e.OutStatus).HasColumnName("OutStatus");
            });
        }
    }
}
