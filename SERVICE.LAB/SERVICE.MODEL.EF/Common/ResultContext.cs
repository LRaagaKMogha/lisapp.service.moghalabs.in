using System;
using DEV.Common;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEV.Model.EF
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

        public virtual DbSet<lstsearchresultvisit> SearchResultVisit { get; set; }
        public virtual DbSet<lstresultvisitdbl> GetResultVisit { get; set; }
        public virtual DbSet<deltaresult> GetDeltaResult { get; set; }
        public virtual DbSet<lsthistorydbl> GetVisitHistoy { get; set; }
        public virtual DbSet<lstresultdbl> GetResult { get; set; }
        public virtual DbSet<resultrtn> InsertResult { get; set; }
        public virtual DbSet<objresultmbdbl> GetResultMB { get; set; }
        public virtual DbSet<orgtypeantibiotic> GetOrgTypeAntibiotic { get; set; }
        public virtual DbSet<resultrtn> InsertResultMB { get; set; }
        public virtual DbSet<objresulttemplatedbl> GetResultTemplate { get; set; }
        public virtual DbSet<resultrtn> InsertResultTemplate { get; set; }
        public virtual DbSet<CustomerMsgDetails> GetCustomerMsgDetails { get; set; }
        public virtual DbSet<ExternalResultResponseDTO> InsertExternalResult { get; set; }
        public virtual DbSet<CreateTemplateResultDTO> GetExternalTemplateResult { get; set; }
        public virtual DbSet<lstrecalldbl> GetRecall { get; set; }
        public virtual DbSet<recallDataResponse> InsertRecall { get; set; }

        public virtual DbSet<objbulkresultdbl> GetBulkResult { get; set; }

        public virtual DbSet<covidresult> GetCovidWorkOrder { get; set; }
        public virtual DbSet<resultrtn> InsertCovidWorkOrder { get; set; }
        public virtual DbSet<ApprovalDoctorResponse> GetApprovalDoctorList { get; set; }
        public virtual DbSet<PatientDataImpressionResponse> GetPatientImpressionList { get; set; }
        public virtual DbSet<PatientImpressionResponse> GetPatientImpressionoutput { get; set; }
        public virtual DbSet<mergeresultresponse> GetMergedResults { get; set; }
        public virtual DbSet<savemergeresultresponse> InsertMergedResults { get; set; }
        public virtual DbSet<culturehistoryreponse> GetCultureHistory { get; set; }
        public virtual DbSet<lstresultdbl> GetAnalyserResult { get; set; }
        public virtual DbSet<resultrtn> InsertAnalyserResult { get; set; }
        public virtual DbSet<lstbulkresultdbl> GetBulkEntryResult { get; set; }
        public virtual DbSet<BulkResultSaveResponse> InsertBulkResult { get; set; }
        public virtual DbSet<BulkCultureResultResponse> GetCultureBulkEntryResults { get; set; }
        public virtual DbSet<BulkCultureResultSaveResponse> InsertCultureBulkResult { get; set; }
        public virtual DbSet<ResultforVisitMergeResponse> GetVisitMergeRequest { get; set; }
        public virtual DbSet<InsertVisitMergeResponse> SaveVisitMergeResponse { get; set; }
        public virtual DbSet<lstresultdbl> GetResultExceptUserMapped { get; set; }
        public virtual DbSet<InvestigationAvailResponse> CheckInvestigationReport { get; set; }
        public virtual DbSet<logicCommentsRespose> GetLogicComment { get; set; }
        public virtual DbSet<ExternalCultureResultResponseDTO> InsertCultureInterfaceResult { get; set; }
        public virtual DbSet<extrasubtestflagbasedformularesponse> GetExtrasubtestbasedformula { get; set; }
        public virtual DbSet<saveinfectioncontroldetresponse> InsertInfectionControlAvailData { get; set; }
        public virtual DbSet<GetOldResultThroughDIResponse> GetOldResultThroughDI { get; set; }
        public virtual DbSet<LstExternalResultCalculation> GetExternalFormulaOrderDetails { get; set; }
        public virtual DbSet<ExternalApprovalResponseDTO> GetExternalApprovalResponseDTO { get; set; }
       
        public virtual DbSet<CheckFormulaIsAvailable> CheckFormulaIsAvailable_ForCalculation { get; set; }
        //public virtual DbSet<RecallTestDetailsResponse> RecallTestDetailsResponse { get; set; }
        public virtual DbSet<PBFTestResponse> GetPBFAutoComments { get; set; }
        public virtual DbSet<objUpdPartialEntryFlagResponse> InsertPartialResultFlag { get; set; }
        public virtual DbSet<PendingVisitDetailsRes> PendingVisitDetailsLst { get; set; }
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

            modelBuilder.Entity<lstsearchresultvisit>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_SearchResultVisit");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<lstresultvisitdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResultVisit");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<deltaresult>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetDeltaResult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<lsthistorydbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetVisitHistoy");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<lstresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertResult");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<objresultmbdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetResultMB");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<orgtypeantibiotic>(entity =>
            {
                entity.HasKey(e => e.antibioticno);
                entity.ToTable("pro_GetOrgTypeAntibiotic");
                entity.Property(e => e.antibioticno).HasColumnName("antibioticno");
            });

            modelBuilder.Entity<CustomerMsgDetails>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("Pro_GetCustomerNotification");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            
            modelBuilder.Entity<resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertResultMB");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<objresulttemplatedbl>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_GetResultTemplate");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<resultrtn>(entity =>
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

            modelBuilder.Entity<lstrecalldbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetRecallVisit");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });

            modelBuilder.Entity<recallDataResponse>(entity =>
            {
                entity.HasKey(r => r.patientvisitno);
                entity.ToTable("pro_InsertRecall");
                entity.Property(r => r.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<RecallTestDetailsResponse>(entity =>
            {
                entity.HasKey(r => r.orderListNo);
            });

            modelBuilder.Entity<objbulkresultdbl>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_GetBulkResult_Bk");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<covidresult>(entity =>
            {
                entity.HasKey(e => e.PatientVisitNo);
                entity.ToTable("pro_GetCovidWorkOrder");
                entity.Property(e => e.PatientVisitNo).HasColumnName("PatientVisitNo");
            });

            modelBuilder.Entity<resultrtn>(entity =>
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
            modelBuilder.Entity<mergeresultresponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetMergeResult");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<savemergeresultresponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_InsertMergedResult");
                entity.Property(e => e.status).HasColumnName("Status");
            });
            modelBuilder.Entity<culturehistoryreponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetCultureHistory");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<lstresultdbl>(entity =>
            {
                entity.HasKey(e => e.rowno);
                entity.ToTable("pro_GetAnalyserResult");
                entity.Property(e => e.rowno).HasColumnName("rowno");
            });
            modelBuilder.Entity<resultrtn>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_InsertAnalyserResult");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<lstbulkresultdbl>(entity =>
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
            modelBuilder.Entity<lstresultdbl>(entity =>
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
            modelBuilder.Entity<logicCommentsRespose>(entity =>
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
            modelBuilder.Entity<extrasubtestflagbasedformularesponse>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("pro_GetIndividualTestFormulaJson");
                entity.Property(e => e.id).HasColumnName("id");
            });
            modelBuilder.Entity<saveinfectioncontroldetresponse>(entity =>
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
            modelBuilder.Entity<objUpdPartialEntryFlagResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_UpdatePartialResultFlag");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<PendingVisitDetailsRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPendingVisitDetails");
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
