using DEV.Common;
using Service.Model.FrontOffice;
using Service.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Service.Model.EF
{
    public partial class FrontOfficeContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public FrontOfficeContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public FrontOfficeContext(DbContextOptions<FrontOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCountryList> TblCountry { get; set; }
        public virtual DbSet<TblState> TblState { get; set; }
        public virtual DbSet<TblCity> TblCity { get; set; }
        public virtual DbSet<TblCurrency> TblCurrency { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<tblCustomerSubUser> tblCustomerSubUser { get; set; }
        public virtual DbSet<TblDiscount> TblDiscount { get; set; }
        public virtual DbSet<TblPhysician> TblPhysician { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<UserResponseDTO> tblUserPassEF { get; set; }
        public virtual DbSet<FrontOffficeResponse> FrontOffficeTransaction { get; set; }
        public virtual DbSet<FrontOffficeResponse> EditBillingTransaction { get; set; }
        public virtual DbSet<FrontOffficePatientResponse> FrontOffficePatient { get; set; }
        public virtual DbSet<FrontOffficePatientResponse> FrontOffficeRegistration { get; set; }
        public virtual DbSet<FrontOffficePatientResponse> EditBillingPatientDTO { get; set; }
        public virtual DbSet<FrontOffficeResetResponse> FrontOffficeReset { get; set; }
        public virtual DbSet<ServiceSearchDTO> ServiceSearchDTO { get; set; }
        public virtual DbSet<GroupTestDTO> GroupServiceDTO { get; set; }
        public virtual DbSet<OptionalTestDTO> GetOptionalSelectedInPackages { get; set; }
        
        public virtual DbSet<ServiceRateList> ServiceRateList { get; set; }
        public virtual DbSet<CustomerList> CustomerList { get; set; }
        public virtual DbSet<GetDetailsByPincode> GetDetailsByPincodeDTO { get; set; }
        public virtual DbSet<TblPhysicianSearch> Physiciandetails { get; set; }
        public virtual DbSet<TblPatient> TblPatient { get; set; }
        public virtual DbSet<UserDashBoardMasterResponse> UserDashBoardMasterDTO { get; set; }
        public virtual DbSet<userbranchlist> userbranchlistDTO { get; set; }
        public virtual DbSet<DoctorDetails> DoctorDetails { get; set; }
        public virtual DbSet<PatientNotifyLogResponse> InsertPatientNotifyLogDetails { get; set; }
        public virtual DbSet<PatientNotifyLog> GetPatientNotifyLog { get; set; }
        public virtual DbSet<ClinicalSummary> GetPatientClinicalSummary { get; set; }
        public virtual DbSet<QueueOrderDTO> GetQueueOrder { get; set; }
        public virtual DbSet<FrontOffficeQueueResponse> QueueResponse { get; set; }
        public virtual DbSet<TestPrePrintDetailsResponse> TestPrePrintDetails { get; set; }
        public virtual DbSet<ExternalVisitDetailsResponse> CheckExternalVistIdExists { get; set; }
        public virtual DbSet<CreateManageSampleResponse> CreateManageSamples { get; set; }
        public virtual DbSet<FrontOffficeValidatetest> validatetestresult { get; set; }
        public virtual DbSet<CustomerCurrentBalance> CustomerCurrentBalance { get; set; }
        public virtual DbSet<ExternalPatientTempResponse> HCPatientTempResponse { get; set; }
        public virtual DbSet<ExternalResult> ExternalResult { get; set; }
        public virtual DbSet<ExternalDeleteTest> ExternalDeleteTest { get; set; }
        public virtual DbSet<ExternalBarcodeTest> ExternalBarcodeTest { get; set; }
        public virtual DbSet<ExternalHCResponse> ExternalHCResponse { get; set; }
        public virtual DbSet<ExternalSampleList> ExternalSampleList { get; set; }
        public virtual DbSet<ExternalPatientCommonResult> PatientSignUp { get; set; }
        public virtual DbSet<ExternalPatientLoginoutput> PM_PatientLogin { get; set; }
        public virtual DbSet<ExternalPatientOTPResponse> PM_OTPVerify { get; set; }

        public virtual DbSet<ExternalPatientUserDetail> PatientUserEF { get; set; }
        public virtual DbSet<ExternalPatientAddMemberResult> PatientAddmember { get; set; }


        public virtual DbSet<ExternalBookingResponse> ExternalBookingRider { get; set; }
        public virtual DbSet<ExternalHCRiderResponse> ExternalRiderStatus { get; set; }
        public virtual DbSet<ExternalHCPatientResponse> ExternalPatientStatus { get; set; }
        public virtual DbSet<ExternalServiceDto> ExternalServiceResponse { get; set; }
        public virtual DbSet<ExternalHCAppointment> ExternalHCAppointment { get; set; }
        public virtual DbSet<GetDiscountApprovalResponse> GetDiscountApprovalDetails { get; set; }
        public virtual DbSet<SaveDiscountApprovalResponse> InsertDiscountApprovalDetails { get; set; }
        public virtual DbSet<UserResponseDTO> tblUserResetPassEF { get; set; }
        public virtual DbSet<CommonAdminResponse> ValidateNricNo { get; set; }
        public virtual DbSet<MassRegistrationResponse> MassRegistrationResponse { get; set; }
        public virtual DbSet<MassFileDTO> GetMassFileResponse { get; set; }
        public virtual DbSet<massPatientBarcode> massPatientBarcodeResponse { get; set; }
        public virtual DbSet<CommonAdminResponse> ValidatePTTTestDTO { get; set; }
        public virtual DbSet<ClinicalHistoryResponse> GetClinicalHistories { get; set; }
        public virtual DbSet<CommonAdminResponse> InsertClinicalHistories { get; set; }
        public virtual DbSet<PatientVisitPatternIDGenRes> GetVisitPatternID { get; set; }
        public virtual DbSet<Tblloyal> getloyalcard { get; set; }
        public virtual DbSet<ExternalupdateCommonResponse> UpdateHCPatientDetails { get; set; }
        public virtual DbSet<UpdateStatusApptDateResponse> UpdateStatusApptDate { get; set; }
        public virtual DbSet<PreBookingtDTO> GetPreBookingDetails { get; set; }
        public virtual DbSet<PreBookingtResponse> PreBookingtResponse { get; set; }
        public virtual DbSet<AutoLoyaltyIDGenResponse> GetLoyaltyCardPatternID { get; set; }
        public virtual DbSet<TblDiscount> GetDiscountMaster { get; set; }
        public virtual DbSet<SlotBookingupdateCResponse> UpdateSlotBooking { get; set; }
        public virtual DbSet<TestSlotBookingDTO> TestSlotBookingDTO { get; set; }
        public virtual DbSet<TestSlotCommonResponse> InsertTestSlotBooking { get; set; }
        public virtual DbSet<ClientBranchSamplePickupResponse> GetClientBranchSamplePickup { get; set; }
        public virtual DbSet<ClientBranchSamplePickupInsertResponse> InsertClientBranchSamplePickup { get; set; }
        public virtual DbSet<ClientBranchSamplePickupRiderInsertResponse> InsertRiderClientBranchSamplePickup { get; set; }

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

            modelBuilder.Entity<TblCountryList>(entity =>
            {
                entity.HasKey(e => e.CountryNo)
                    .HasName("PK__tbl_Coun__10CE2595211B0BFD");

                entity.ToTable("tbl_Country");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.VenueNo).HasColumnName("venueNo");
            });

            modelBuilder.Entity<TblState>(entity =>
            {
                entity.HasKey(e => e.StateNo)
                    .HasName("PK__tbl_Coun__10CE2595211B0BFD1");

                entity.ToTable("Tbl_State");

                entity.Property(e => e.StateName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.VenueNo).HasColumnName("venueNo");
            });

            modelBuilder.Entity<TblCity>(entity =>
            {
                entity.HasKey(e => e.CityNo)
                    .HasName("PK__tbl_Coun__10CE2595211B0BFD1");

                entity.ToTable("Tbl_City");

                entity.Property(e => e.CityName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.VenueNo).HasColumnName("VenueNo");
            });

            modelBuilder.Entity<TblCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyNo)
                    .HasName("PK__tbl_Curr__14476323BE8DE3DE");

                entity.ToTable("tbl_Currency");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrencyName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Symbol).HasMaxLength(5);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerNo)
                    .HasName("PK__tbl_Cust__A4AFBF63D957DD44");

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

                entity.Property(e => e.Password)
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
            });

            modelBuilder.Entity<tblCustomerSubUser>(entity =>
            {
                entity.HasKey(e => e.CustomerSubUserNo)
                    .HasName("PK__tbl_Cust__A4AFBF63D957DD4434");

                entity.ToTable("tbl_Customer_SubUser");

                entity.Property(e => e.CustomerNo)
                 .HasMaxLength(500)
                 .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                 .HasMaxLength(10)
                .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            });


            modelBuilder.Entity<TblDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountNo)
                    .HasName("PK__tbl_Disc__E43F764550882659");

                entity.ToTable("tbl_Discount");

                entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DiscountName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountType)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PK__tbl_User__1788955F1E231A32");

                entity.ToTable("tbl_User");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPhysician>(entity =>
            {
                entity.HasKey(e => e.PhysicianNo)
                    .HasName("PK__tbl_Phys__DFF5F520A639BA46");

                entity.ToTable("tbl_Physician");

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsReportSms).HasColumnName("IsReportSMS");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhysicianEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhysicianMobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhysicianName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.Property(e => e.Speciality)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPatient>(entity =>
            {
                entity.HasKey(e => e.PatientNo)
                    .HasName("PK__tbl_Pati__970ED8BD9F7CB13D");

                entity.ToTable("tbl_Patient");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AgeType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AltMobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AreaName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdentifyId)
                    .HasColumnName("IdentifyID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdentifyType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IntegrationCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IntegrationId)
                    .HasColumnName("IntegrationID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PatientId)
                    .IsRequired()
                    .HasColumnName("PatientID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pincode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryEmailId)
                    .HasColumnName("SecondaryEmailID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TitleCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Urnid)
                    .HasColumnName("URNID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Urntype)
                    .HasColumnName("URNType")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServiceSearchDTO>(entity =>
            {
                entity.HasKey(e => e.Rowno);
                entity.ToTable("pro_SearchService");
                entity.Property(e => e.Rowno).HasColumnName("Rowno");
            });

            modelBuilder.Entity<ServiceRateList>(entity =>
            {
                entity.HasKey(e => e.ServiceNo);
                entity.ToTable("pro_ServiceDetails");
                entity.Property(e => e.ServiceNo).HasColumnName("ServiceNo");
            });
            modelBuilder.Entity<OptionalTestDTO>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetOptionalSelectedInPackages");
            });

            modelBuilder.Entity<FrontOffficeResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("Pro_InsertPatientOrders");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<FrontOffficePatientResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("Pro_InsertPatientRegistration");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<FrontOffficeResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("Pro_InsertEditBillingOrders");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });
            modelBuilder.Entity<FrontOffficePatientResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("Pro_InsertEditBilling");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });


            modelBuilder.Entity<FrontOffficeResetResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitno);
                entity.ToTable("pro_ResetRegistration");
                entity.Property(e => e.patientvisitno).HasColumnName("patientvisitno");
            });

            modelBuilder.Entity<GroupTestDTO>(entity =>
            {
                entity.HasKey(e => e.TestNo);
                entity.ToTable("pro_GrouptestDetails");
                entity.Property(e => e.TestNo).HasColumnName("TestNo");
            });

            modelBuilder.Entity<CustomerList>(entity =>
            {
                entity.HasKey(e => e.Rowno);
                entity.ToTable("Pro_GetSearchCustomer");
                entity.Property(e => e.Rowno).HasColumnName("Rowno");
            });
            modelBuilder.Entity<TblPhysicianSearch>(entity =>
            {
                entity.HasKey(e => e.PhysicianNo);
                entity.ToTable("Pro_SearchPhysicianDetail");
                entity.Property(e => e.PhysicianNo).HasColumnName("PhysicianNo");
            });
            modelBuilder.Entity<UserResponseDTO>(entity =>
            {
                entity.HasKey(e => e.UserNo);
                entity.ToTable("Pro_UpdatePassword");
                entity.Property(e => e.UserNo).HasColumnName("UserNo");
            });
            modelBuilder.Entity<UserResponseDTO>(entity =>
            {
                entity.HasKey(e => e.UserNo);
                entity.ToTable("Pro_ResetPassword");
                entity.Property(e => e.UserNo).HasColumnName("UserNo");
            });
            modelBuilder.Entity<UserDashBoardMasterResponse>(entity =>
            {
                entity.HasKey(e => e.DashBoardMasterNo);
                entity.ToTable("Pro_GetDashBoardMaster");
                entity.Property(e => e.DashBoardMasterNo).HasColumnName("DashBoardMasterNo");
            });

            modelBuilder.Entity<DoctorDetails>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
                entity.ToTable("Pro_InsertPhysician");
                entity.Property(e => e.DoctorId).HasColumnName("DoctorId");
            });

            modelBuilder.Entity<PatientNotifyLogResponse>(entity =>
            {
                entity.HasKey(e => e.PatientNotifyLogNo);
                entity.ToTable("Pro_InsertPatientNotifyLog");
                entity.Property(e => e.PatientNotifyLogNo).HasColumnName("PatientNotifyLogNo");
            });
            modelBuilder.Entity<PatientNotifyLog>(entity =>
            {
                entity.HasKey(e => e.LogNo);
                entity.ToTable("Pro_GetPatientNotifyLog");
                entity.Property(e => e.LogNo).HasColumnName("LogNo");
            });
            modelBuilder.Entity<ClinicalSummary>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetClinicalSummary");
            });
            modelBuilder.Entity<TestPrePrintDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("pro_GetTestPrePrintDetails");
                entity.Property(e => e.ID).HasColumnName("ID");
            });
            modelBuilder.Entity<ExternalVisitDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.Outpt);
                entity.ToTable("pro_CheckExternalVistIdExists");
                entity.Property(e => e.Outpt).HasColumnName("Outpt");
            });
            modelBuilder.Entity<CreateManageSampleResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_PrePrintBarcodeInsertSamples");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<QueueOrderDTO>(entity =>
            {
                entity.HasKey(e => e.RowNum);
                entity.ToTable("Pro_GetQueueOrder");
                entity.Property(e => e.RowNum).HasColumnName("RowNum");
            });
            modelBuilder.Entity<FrontOffficeQueueResponse>(entity =>
            {
                entity.HasKey(e => e.patientvisitNo);
                entity.ToTable("Pro_UpdateQueueOrder");
                entity.Property(e => e.patientvisitNo).HasColumnName("patientvisitNo");
            });
            modelBuilder.Entity<userbranchlist>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetUserBranchMapping");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<FrontOffficeValidatetest>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_ValidateService");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<CustomerCurrentBalance>(entity =>
            {
                entity.HasKey(e => e.PayType);
                entity.ToTable("Pro_GetCustomerBalance");
                entity.Property(e => e.PayType).HasColumnName("PayType");
            });
            modelBuilder.Entity<ExternalPatientTempResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetHCTransaction");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ExternalResult>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_GetHCInsertTest");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalDeleteTest>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_GetHCDeleteTest");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalBarcodeTest>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_GetHCInsertBarcode");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalHCResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetHCPatient");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ExternalSampleList>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_HCGetPatientDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ExternalBookingResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_InsertHCPatient");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalHCRiderResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_UpdateRiderStatus");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalHCPatientResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_UpdatePatientStatus");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<ExternalPatientCommonResult>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_PA_PatientSignUp");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalPatientLoginoutput>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_PA_PatientUser");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalPatientOTPResponse>(entity =>
            {
                entity.HasKey(e => e.username);
                entity.ToTable("Pro_PA_OTPVerify");
                entity.Property(e => e.username).HasColumnName("username");
            });
            modelBuilder.Entity<ExternalPatientUserDetail>(entity =>
            {
                entity.HasKey(e => e.PatientNo);
                entity.ToTable("Pro_PatientUserDetail");
                entity.Property(e => e.PatientNo).HasColumnName("PatientNo");
            });

            modelBuilder.Entity<ExternalPatientAddMemberResult>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_PA_Addmember");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<ExternalServiceDto>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_HCServiceDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ExternalHCAppointment>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetHCAppointment");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<GetDetailsByPincode>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetDetailsByPinCode");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });



            modelBuilder.Entity<GetDiscountApprovalResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetDiscountApprovalDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<SaveDiscountApprovalResponse>(entity =>
            {
                entity.HasKey(e => e.ApproveStatus);
                entity.ToTable("Pro_InsertDiscountApprovalDetails");
                entity.Property(e => e.ApproveStatus).HasColumnName("ApproveStatus");
            });

            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_ValidateNricNo");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<massPatientBarcode>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetMassTransactionDownload");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });


            modelBuilder.Entity<MassFileDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetMassFileRegistration");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<MassRegistrationResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("pro_InsertMassRegistration");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_ValidatePTTTest");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<ClinicalHistoryResponse>(entity =>
            {
                entity.HasKey(e => e.HistoryNo);
                entity.ToTable("Pro_GetClinicalHistory");
                entity.Property(e => e.HistoryNo).HasColumnName("HistoryNo");
            });
            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertClinicalHistory");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<Tblloyal>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("Pro_GetLoyaltyType");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<PatientVisitPatternIDGenRes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_GetPatternIDNew");
                entity.Property(e => e.Id).HasColumnName("Id");
            });
            modelBuilder.Entity<ExternalupdateCommonResponse>(entity =>
            {
                entity.HasKey(e => e.bioHCPatientNo);
                entity.ToTable("Pro_ UpdateHcPatientBIO");
                entity.Property(e => e.bioHCPatientNo).HasColumnName("bioHCPatientNo");
            });
            modelBuilder.Entity<UpdateStatusApptDateResponse>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("Pro_UpdateStatusApptDate");
                entity.Property(e => e.Status).HasColumnName("Status");
            });
            modelBuilder.Entity<PreBookingtResponse>(entity =>
            {
                entity.HasKey(e => e.PreBookingQueueNo);
                entity.ToTable("pro_InsertPreBooking");
                entity.Property(e => e.PreBookingQueueNo).HasColumnName("PreBookingQueueNo");
            });
            modelBuilder.Entity<PreBookingtDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("pro_GetPreBookingDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<AutoLoyaltyIDGenResponse>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_GetLoyaltyPatternID");
                entity.Property(e => e.Status).HasColumnName("Status");
            });
            modelBuilder.Entity<TblDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountNo);
                entity.ToTable("pro_GetDiscountMaster_All");
                entity.Property(e => e.DiscountNo).HasColumnName("DiscountNo");
            });
            modelBuilder.Entity<SlotBookingupdateCResponse>(entity =>
            {
                entity.HasKey(e => e.bioPatientNo);
                entity.ToTable("Pro_ Pro_UpdateSlotBooking");
                entity.Property(e => e.bioPatientNo).HasColumnName("bioPatientNo");
            });
            modelBuilder.Entity<TestSlotBookingDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetSlotBooking");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<TestSlotCommonResponse>(entity =>
            {
                entity.HasKey(e => e.BookingID);
                entity.ToTable("pro_InsertSlotBooking");
                entity.Property(e => e.BookingID).HasColumnName("BookingID");
            });
            modelBuilder.Entity<ClientBranchSamplePickupResponse>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetClientBranchSamplePickup");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ClientBranchSamplePickupInsertResponse>(entity =>
            {
                entity.HasKey(e => e.SamplePickupNo);
                entity.ToTable("Pro_InsertCilentBranchSamplePickup");
                entity.Property(e => e.SamplePickupNo).HasColumnName("SamplePickupNo");
            });
            modelBuilder.Entity<ClientBranchSamplePickupRiderInsertResponse>(entity =>
            {
                entity.HasKey(e => e.SamplePickupNo);
                entity.ToTable("Pro_InsertAssignRiderToSamplePickup");
                entity.Property(e => e.SamplePickupNo).HasColumnName("SamplePickupNo");
            });
        }
    }
}

