using System;
using System.Collections.Generic;
using DEV.Common;
using Service.Model.FrontOffice.PatientDue;
using Service.Model.PatientInfo;
using Service.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Service.Model.EF
{
    public partial class OPDContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public OPDContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public OPDContext(DbContextOptions<OPDContext> options)
            : base(options)
        {
        }
        public virtual DbSet<OPDPatientDTOResponse> OPDPatient { get; set; }
        public virtual DbSet<OPDPatientMachineResponse> OPDPatientMachineList { get; set; }
        public virtual DbSet<OPDPatientDTOList> OPDPatientDTOList { get; set; }
        public virtual DbSet<OPDPatientMachineDTOList> OPDPatientMachineDTOList { get; set; }
        public virtual DbSet<OPDPatientDoctorDTOList> OPDPatientDoctorDTOList { get; set; }
        public virtual DbSet<OPDPatientDoctorDTOList> OPDDoctorReferral { get; set; }
        public virtual DbSet<OPDPatientBookingList> OPDPatientBookingList { get; set; }
        public virtual DbSet<OPDPatientMachineBookingList> OPDPatientMachineBookingList { get; set; }
        public virtual DbSet<OPDDoctorList> OPDDoctorList { get; set; }
        public virtual DbSet<SearchOPDPatient> SearchOPDPatient { get; set; }
        public virtual DbSet<SearchOPDMachinePatient> SearchOPDMachinePatient { get; set; }
        public virtual DbSet<OPDPatientVitalList> OPDPatientVitalList { get; set; }
        public virtual DbSet<ServiceSearchDTO> ServiceSearchDTO { get; set; }
        public virtual DbSet<OPDPatientMedicineList> OPDPatientMedicineList { get; set; }
        public virtual DbSet<OPDDiagnosisDTOResponse> InsertOPDDiagnosis { get; set; }
        public virtual DbSet<OPDPatientOPDData> OPDPatientOPDDataList { get; set; }
        public virtual DbSet<OPDPatientOPDDrugData> OPDPatientOPDDrugDataList { get; set; }
        public virtual DbSet<OPDPatientOPDTestData> OPDPatientOPDTestDataList { get; set; }
        public virtual DbSet<OPDOPDPatientHistory> OPDOPDPatientHistory { get; set; }
        public virtual DbSet<lstAllergyType> GetAllergyTypeData { get; set; }
        public virtual DbSet<AllergyTypeResponse> InsertAllergyTypeData { get; set; }
        public virtual DbSet<lstAllergyMaster> GetAllergyMasterData { get; set; }
        public virtual DbSet<rtnAllergyMaster> InsertAllergyMasterData { get; set; }
        public virtual DbSet<lstDiseaseCategory> GetDiseaseCategoryData { get; set; }
        public virtual DbSet<rtnDiseaseCategory> InsertDiseaseCategoryData { get; set; }
        public virtual DbSet<lstDiseaseMaster> GetDiseaseMasterData { get; set; }
        public virtual DbSet<rtnDiseaseMaster> InsertDiseaseMasterData { get; set; }
        public virtual DbSet<OPDBulkFileUpload> GetPatientDocumentDetails { get; set; }
        public virtual DbSet<displaylist> displaylistEF { get; set; }
        public virtual DbSet<PhysicianAmount> GetOPDPhysicianAmount { get; set; }
        public virtual DbSet<AllergyTypeResponse> InsertAllergyTypes { get; set; }
        public virtual DbSet<drugresponse> GetDrugDetails { get; set; }
        public virtual DbSet<lstDiseaseTemplateList> GetTemplateList { get; set; }
        public virtual DbSet<OPDBeforeAfterImageList> OPDBeforeAfterImageList { get; set; }
        public virtual DbSet<DiseaseVsProductMapping> GetDiseaseVsDrugMaster { get; set; }
        public virtual DbSet<DiseaseVsTestMapping> GetDiseaseVsTestMaster { get; set; }
        public virtual DbSet<lstOPDReasonMaster> GetOPDResonMasterData { get; set; }
        public virtual DbSet<rtnOPDReasonMaster> InsertOPDResonMasterData { get; set; }
        public virtual DbSet<rtnDisVsDrugMaster> InsertDisVsDrugMaster { get; set; }
        public virtual DbSet<rtnDisVsInvMaster> InsertDisVsInvMaster { get; set; }
        public virtual DbSet<OPDPatientDisVsDrugDetails> GetOPDMasterDefinedDrugDetails { get; set; }
        public virtual DbSet<OPDPatientDisVsInvDetails> GetOPDMasterDefinedInvDetails { get; set; }
        public virtual DbSet<OPDApptDetails> GetOPDApptDetails { get; set; }
        public virtual DbSet<OPDBeforeAfterImageListResponse> InserOPDPatientImaging { get; set; }
        public virtual DbSet<OPDDiagnosisDTOFollowupResponse> InsertFollowUpAppointment { get; set; }
        public virtual DbSet<Humanbodyparts> Humanbodyparts { get; set; }
        public virtual DbSet<TreatmentPlanResponse> OPDInsertTreatmentplan { get; set; }
        public virtual DbSet<OPDTreatmentPlanProcedures> GetOPDTreatmentPlanPRO { get; set; }
        public virtual DbSet<OPDTreatmentPlanPharmacy> GetOPDTreatmentPlanPRM { get; set; }
        public virtual DbSet<OPDTreatmentPlanRes> GetOPDTreatmentPlan { get; set; }
        public virtual DbSet<ResOPDperformPhysicianAPPT> PerformingPhysicianAppointment { get; set; }
        public virtual DbSet<OPDTreatmentPlanProAppointmentlist> OPDTreatmentPlanAppointmentDetails { get; set; }
        public virtual DbSet<ProductInsRes> InsertProduct { get; set; }
        public virtual DbSet<PatientDrugList> GetPatientDrugDetails { get; set; }
        public virtual DbSet<PatientDrugDetailList> GetPatientDrugList { get; set; }
        public virtual DbSet<PatientDrugDetailList> GetAutoCompleteDruglist { get; set; }
        public virtual DbSet<PatientDrugDetailRes> InsetPatientPrescription { get; set; }
        public virtual DbSet<PrintPatientPrescription> GetPatientPrescriptionPrint { get; set; }
        public virtual DbSet<MachineMasterDTO> GetMachineResult { get; set; }
        public virtual DbSet<reqMachineMasterResponse> InsertMachineResult { get; set; }
        public virtual DbSet<ImageListResponse> OPDImagingIncludingreport { get; set; }
        public virtual DbSet<OPDDashBoardRes> GetOPDDashBoardDTO { get; set; }
        public virtual DbSet<OPDStatusLogListResponse> OPDStatusLogList { get; set; }
        public virtual DbSet<rtnAllergyReaction> InsertAllergyReaction { get; set; }
        public virtual DbSet<rtnAllergyReactionres> GetAllergyReactionl { get; set; }
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

            modelBuilder.Entity<OPDPatientDTOResponse>(entity =>
            {
                entity.HasKey(e => e.OPDPatientNo);
                entity.ToTable("pro_InsertOPDPatient");
                entity.Property(e => e.OPDPatientNo).HasColumnName("OPDPatientNo");

            });
            modelBuilder.Entity<OPDPatientMachineResponse>(entity =>
            {
                entity.HasKey(e => e.OPDPatientNo);
                entity.ToTable("pro_InsertOPDMachinePatient");
                entity.Property(e => e.OPDPatientNo).HasColumnName("OPDPatientNo");

            });
            modelBuilder.Entity<OPDPatientDTOList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetOPDTransaction");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<OPDPatientMachineDTOList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetOPDTransactionMachine");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<OPDPatientDoctorDTOList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetOPDDoctorTransaction");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<OPDPatientDoctorDTOList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetOPDDoctorReferral");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<OPDPatientBookingList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDBookingdata");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<OPDPatientMachineBookingList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDMachineBookingdata");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<OPDDoctorList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDPhysicianAppointment");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<SearchOPDPatient>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDPatientdata");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<SearchOPDMachinePatient>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDMachinePatientdata");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<OPDPatientVitalList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetVitalPatientData");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<ServiceSearchDTO>(entity =>
            {
                entity.HasKey(e => e.Rowno);
                entity.ToTable("pro_OPDSearchService");
                entity.Property(e => e.Rowno).HasColumnName("Rowno");

            });
            modelBuilder.Entity<OPDPatientMedicineList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetOPDMedicineData");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });
            modelBuilder.Entity<OPDDiagnosisDTOResponse>(entity =>
            {
                entity.HasKey(e => e.PhysicianDiagnosisNo);
                entity.ToTable("pro_InsertPhysicianDiagnosis");
                entity.Property(e => e.PhysicianDiagnosisNo).HasColumnName("PhysicianDiagnosisNo");

            });
            modelBuilder.Entity<OPDPatientOPDData>(entity =>
            {
                entity.HasKey(e => e.PhysicianDiagnosisNo);
                entity.ToTable("pro_GetOPDPatientSOAP");
                entity.Property(e => e.PhysicianDiagnosisNo).HasColumnName("PhysicianDiagnosisNo");

            });
            modelBuilder.Entity<OPDPatientOPDDrugData>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo);
                entity.ToTable("pro_GetOPDPatientDrugsData");
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");

            });
            modelBuilder.Entity<OPDPatientOPDTestData>(entity =>
            {
                entity.HasKey(e => e.TestNo);
                entity.ToTable("pro_GetOPDPatientTestData");
                entity.Property(e => e.TestNo).HasColumnName("TestNo");

            });
            modelBuilder.Entity<OPDOPDPatientHistory>(entity =>
            {
                entity.HasKey(e => e.OPDPatientAppointmentNo);
                entity.ToTable("pro_GetOPDPatientHistory");
                entity.Property(e => e.OPDPatientAppointmentNo).HasColumnName("OPDPatientAppointmentNo");

            });
            modelBuilder.Entity<lstAllergyType>(entity =>
            {
                entity.HasKey(e => e.AllergyTypeNo);
                entity.ToTable("pro_GetAllergyType");
                entity.Property(e => e.AllergyTypeNo).HasColumnName("AllergyTypeNo");
            });
            modelBuilder.Entity<AllergyTypeResponse>(entity =>
            {
                entity.HasKey(e => e.AllergyTypeNo);
                entity.ToTable("pro_InsertDiscountmaster");
                entity.Property(e => e.AllergyTypeNo).HasColumnName("AllergyTypeNo");
            });
            modelBuilder.Entity<lstAllergyMaster>(entity =>
            {
                entity.HasKey(e => e.AllergyMasterNo);
                entity.ToTable("pro_GetAllergyMaster");
                entity.Property(e => e.AllergyMasterNo).HasColumnName("AllergyMasterNo");
            });
            modelBuilder.Entity<rtnAllergyMaster>(entity =>
            {
                entity.HasKey(e => e.AllergyMasterNo);
                entity.ToTable("pro_InsertAllergyMaster");
                entity.Property(e => e.AllergyMasterNo).HasColumnName("AllergyMasterNo");
            });
            modelBuilder.Entity<lstDiseaseCategory>(entity =>
            {
                entity.HasKey(e => e.DiseaseCategoryNo);
                entity.ToTable("pro_GetDiseaseCategory");
                entity.Property(e => e.DiseaseCategoryNo).HasColumnName("DiseaseCategoryNo");
            });
            modelBuilder.Entity<rtnDiseaseCategory>(entity =>
            {
                entity.HasKey(e => e.DiseaseCategoryNo);
                entity.ToTable("pro_InsertDiseaseCategory");
                entity.Property(e => e.DiseaseCategoryNo).HasColumnName("DiseaseCategoryNo");
            });
            modelBuilder.Entity<lstDiseaseMaster>(entity =>
            {
                entity.HasKey(e => e.DiseaseMasterNo);
                entity.ToTable("pro_GetDiseaseMaster");
                entity.Property(e => e.DiseaseMasterNo).HasColumnName("DiseaseMasterNo");
            });
            modelBuilder.Entity<rtnDiseaseMaster>(entity =>
            {
                entity.HasKey(e => e.DiseaseMasterNo);
                entity.ToTable("pro_InsertDiseaseMaster");
                entity.Property(e => e.DiseaseMasterNo).HasColumnName("DiseaseMasterNo");
            });
            modelBuilder.Entity<OPDBulkFileUpload>(entity =>
            {
                entity.HasKey(e => e.patientNumber);
                entity.ToTable("pro_GetEntityDocument");
                entity.Property(e => e.patientNumber).HasColumnName("patientNumber");
            });
            modelBuilder.Entity<displaylist>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetOPD_Display");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<AllergyTypeResponse>(entity =>
            {
                entity.HasKey(e => e.AllergyTypeNo);
                entity.ToTable("pro_InsertAllergyType");
                entity.Property(e => e.AllergyTypeNo).HasColumnName("AllergyTypeNo");
            });
            modelBuilder.Entity<PhysicianAmount>(entity =>
            {
                entity.HasKey(e => e.PhysicianNo);
                entity.ToTable("Pro_GetOPDPhysicianAmount");
                entity.Property(e => e.PhysicianNo).HasColumnName("PhysicianNo");
            });
            modelBuilder.Entity<drugresponse>(entity =>
            {
                entity.HasKey(e => e.productNo);
                entity.ToTable("pro_GetDrugDetails");
                entity.Property(e => e.productNo).HasColumnName("productNo");
            });
            modelBuilder.Entity<lstDiseaseTemplateList>(entity =>
            {
                entity.HasKey(e => e.templateNo);
                entity.ToTable("pro_GetDiseaseTemplate");
                entity.Property(e => e.templateNo).HasColumnName("templateNo");
            });
            modelBuilder.Entity<OPDBeforeAfterImageList>(entity =>
            {
                entity.HasKey(e => e.physicianDiagnosisNo);
                entity.ToTable("pro_GetOPDBeforeAfterImage");
                entity.Property(e => e.physicianDiagnosisNo).HasColumnName("physicianDiagnosisNo");
            });
            modelBuilder.Entity<DiseaseVsProductMapping>(entity =>
            {
                entity.HasKey(e => e.DiseaseVsProductMappingNo);
                entity.ToTable("pro_GetDiseaseVsDrugMaster");
                entity.Property(e => e.DiseaseVsProductMappingNo).HasColumnName("DiseaseVsProductMappingNo");
            });
            modelBuilder.Entity<DiseaseVsTestMapping>(entity =>
            {
                entity.HasKey(e => e.DiseaseVsTestMappingNo);
                entity.ToTable("pro_GetDiseaseVsTestMaster");
                entity.Property(e => e.DiseaseVsTestMappingNo).HasColumnName("DiseaseVsTestMappingNo");
            });
            modelBuilder.Entity<lstOPDReasonMaster>(entity =>
            {
                entity.HasKey(e => e.OPDReasonMastNo);
                entity.ToTable("pro_GetOPDResonMaster");
                entity.Property(e => e.OPDReasonMastNo).HasColumnName("OPDReasonMastNo");
            });
            modelBuilder.Entity<rtnOPDReasonMaster>(entity =>
            {
                entity.HasKey(e => e.OPDReasonMastNo);
                entity.ToTable("Pro_InsertOPDResonMaster");
                entity.Property(e => e.OPDReasonMastNo).HasColumnName("OPDReasonMastNo");
            });
            modelBuilder.Entity<rtnDisVsDrugMaster>(entity =>
            {
                entity.HasKey(e => e.DiseaseVsProductMappingNo);
                entity.ToTable("pro_InsertDisVsDrugMaster");
                entity.Property(e => e.DiseaseVsProductMappingNo).HasColumnName("DiseaseVsProductMappingNo");
            });
            modelBuilder.Entity<rtnDisVsInvMaster>(entity =>
            {
                entity.HasKey(e => e.DiseaseVsTestMappingNo);
                entity.ToTable("pro_InsertDisVsInvMaster");
                entity.Property(e => e.DiseaseVsTestMappingNo).HasColumnName("DiseaseVsTestMappingNo");
            });
            modelBuilder.Entity<OPDPatientDisVsDrugDetails>(entity =>
            {
                entity.HasKey(e => e.diseaseVsProductMappingNo);
                entity.ToTable("pro_GetOPDPatientMasterDefinedDrugDetails");
                entity.Property(e => e.diseaseVsProductMappingNo).HasColumnName("diseaseVsProductMappingNo");
            });
            modelBuilder.Entity<OPDPatientDisVsInvDetails>(entity =>
            {
                entity.HasKey(e => e.diseaseVsTestMappingNo);
                entity.ToTable("pro_GetOPDPatientMasterDefinedInvDetails");
                entity.Property(e => e.diseaseVsTestMappingNo).HasColumnName("diseaseVsTestMappingNo");
            });
            modelBuilder.Entity<OPDApptDetails>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetApptDetails");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");
            });
            modelBuilder.Entity<OPDBeforeAfterImageListResponse>(entity =>
            {
                entity.HasKey(e => e.ResultNo);
                entity.ToTable("pro_InserOPDPatientImaging");
                entity.Property(e => e.ResultNo).HasColumnName("ResultNo");
            });
            modelBuilder.Entity<OPDDiagnosisDTOFollowupResponse>(entity =>
            {
                entity.HasKey(e => e.AppointmentNo);
                entity.ToTable("pro_InsertFollowUpAppointment");
                entity.Property(e => e.AppointmentNo).HasColumnName("AppointmentNo");

            });
            modelBuilder.Entity<Humanbodyparts>(entity =>
            {
                entity.HasKey(e => e.HumanBodyParts_Id);
                entity.ToTable("pro_humanbodyparts");
                entity.Property(e => e.HumanBodyParts_Id).HasColumnName("HumanBodyParts_Id");
            });
            modelBuilder.Entity<TreatmentPlanResponse>(entity =>
            {
                entity.HasKey(e => e.oPDTreatmentNo);
                entity.ToTable("Pro_OPDInsertTreatmentPlan");
                entity.Property(e => e.oPDTreatmentNo).HasColumnName("OPDTreatmentNo");
            });
            modelBuilder.Entity<OPDTreatmentPlanRes>(entity =>
            {
                entity.HasKey(e => e.oPDTreatmentNo);
                entity.ToTable("pro_GetOPDTreatmentPlan");
                entity.Property(e => e.oPDTreatmentNo).HasColumnName("OPDTreatmentNo");
            });
            modelBuilder.Entity<OPDTreatmentPlanProcedures>(entity =>
            {
                entity.HasKey(e => e.oPDTreatmentPlanProceduresNo);
                entity.ToTable("pro_GetOPDTreatmentPlanPRO");
                entity.Property(e => e.oPDTreatmentPlanProceduresNo).HasColumnName("oPDTreatmentPlanProceduresNo");
            });
            modelBuilder.Entity<OPDTreatmentPlanPharmacy>(entity =>
            {
                entity.HasKey(e => e.oPDTreatmentPlanPharmacyNo);
                entity.ToTable("pro_GetOPDTreatmentPlanPRM");
                entity.Property(e => e.oPDTreatmentPlanPharmacyNo).HasColumnName("OPDTreatmentPlanPharmacyNo");
            });
            modelBuilder.Entity<ResOPDperformPhysicianAPPT>(entity =>
            {
                entity.HasKey(e => e.AppointmentNo);
                entity.ToTable("pro_PerformingPhysicianMissingAppointment");
                entity.Property(e => e.AppointmentNo).HasColumnName("AppointmentNo");

            });
            modelBuilder.Entity<OPDTreatmentPlanProAppointmentlist>(entity =>
            {
                entity.HasKey(e => e.performingPhyAppointmentNo);
                entity.ToTable("Pro_OPDTreatmentPlanAppointmentDetails");
                entity.Property(e => e.performingPhyAppointmentNo).HasColumnName("performingPhyAppointmentNo");

            });
            modelBuilder.Entity<ProductInsRes>(entity =>
            {
                entity.HasKey(e => e.ProductNo);
                entity.ToTable("Pro_InsertProductOpd");
                entity.Property(e => e.ProductNo).HasColumnName("ProductNo");

            });
            modelBuilder.Entity<PatientDrugList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_GetPatientDrugsDetails");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<PatientDrugDetailList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("Pro_Get_PatientDrugList");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<PatientDrugDetailList>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("pro_GetAutoCompleteDruglist");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });
            modelBuilder.Entity<PatientDrugDetailRes>(entity =>
            {
                entity.HasKey(e => e.PatientPrescriptionBillNo);
                entity.ToTable("Pro_InsertPatientPrescription");
                entity.Property(e => e.PatientPrescriptionBillNo).HasColumnName("PatientPrescriptionBillNo");
            });
            modelBuilder.Entity<PrintPatientPrescription>(entity =>
            {
                entity.HasKey(e => e.Row_num);
                entity.ToTable("pro_GetPrintPatientPrescription");
                entity.Property(e => e.Row_num).HasColumnName("Row_num");

            });

            modelBuilder.Entity<MachineMasterDTO>(entity =>
            {
                entity.HasKey(e => e.machineNo);
                entity.ToTable("pro_GetMachineMaster");
                entity.Property(e => e.machineNo).HasColumnName("machineNo");
            });
            modelBuilder.Entity<reqMachineMasterResponse>(entity =>
            {
                entity.HasKey(e => e.machineNo);
                entity.ToTable("pro_InsertMachineMaster");
                entity.Property(e => e.machineNo).HasColumnName("machineNo");

            });
            modelBuilder.Entity<ImageListResponse>(entity =>
            {
                entity.HasKey(e => e.ResultNo);
                entity.ToTable("pro_UpdateOPDPatientImagingIncludingreport");
                entity.Property(e => e.ResultNo).HasColumnName("ResultNo");
            });
            modelBuilder.Entity<OPDDashBoardRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetOPDDashBoard");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
            modelBuilder.Entity<OPDStatusLogListResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetOPDStatusLog");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");

            });
            modelBuilder.Entity<rtnAllergyReaction>(entity =>
            {
                entity.HasKey(e => e.AllergyReactionNo);
                entity.ToTable("Pro_InsertAllergyReaction");
                entity.Property(e => e.AllergyReactionNo).HasColumnName("AllergyReactionNo");
            });

            modelBuilder.Entity<rtnAllergyReactionres>(entity =>
            {
                entity.HasKey(e => e.AllergyReactionNo);
                entity.ToTable("Pro_GetAllergyReaction");
                entity.Property(e => e.AllergyReactionNo).HasColumnName("AllergyReactionNo");
            });
        }
    }
}
