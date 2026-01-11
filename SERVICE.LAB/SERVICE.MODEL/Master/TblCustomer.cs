using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblCustomer
    {
        public int CustomerNo { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMobileNo { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? ActiveDate { get; set; }
        public decimal? CreditLimit { get; set; }
        public int? CreditPeriod { get; set; }
        public int CustomerType { get; set; }
        public string? Idtype { get; set; }
        public string? Id { get; set; }
        public string? Gstno { get; set; }
        public bool? AllowBilling { get; set; }
        public bool IsReportSms { get; set; }
        public bool IsReportEmail { get; set; }
        public bool? IsInterNotes { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string? ContactPersonName { get; set; }
        public int ClientPayType { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public int City { get; set; }
        public string? Pincode { get; set; }
        public string? ClientUsername { get; set; }
        public bool? CpBilling { get; set; }
        public bool? CpReportView { get; set; }
        public bool? fCBilling { get; set; }
        public bool? sampleScreen { get; set; }
        public bool? CpBillView { get; set; }
        public bool? CpBlock { get; set; }
        public bool? BillingBlock { get; set; }
        public bool? ReportDispatchBlock { get; set; }
        public bool? ClientBlock { get; set; }
        public bool? Active { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public bool IsShowAmount { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public string? secondaryemail { get; set; }
        public string? hcicode { get; set; }
        public int routeNo { get; set; }
        public int? IsFranchisee { get; set; } = 0;
        public int RestrictionDays { get; set; }
        public bool IsBillEmail { get; set; }
        public bool IsBillSMS { get; set; }
        public string? CShortName { get; set; }
        public bool isbillwhatsapp { get; set; }
        public bool isreportwhatsapp { get; set; }
        public bool IsPatientEmail { get; set; }
        public bool collectatsource { get; set; }
        public int IsApproval { get; set; }
        public bool IsReject { get; set; }
        public string RejectReason { get; set; }
        public int OldCustomerNo { get; set; }
        public bool IsClinic { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsInternal { get; set; }
        public bool IsSpeciality { get; set; }
        public string BlkHseLotNo { get; set; }
        public string FloorNo { get; set; }
        public string UnitNo { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string MappinglocationNo { get; set; }
        public bool isCreateinvoice { get; set; }
        public int invoceGenerateType { get; set; }
        public int invoiceGenerateDays { get; set; }
        public int creditDays { get; set; }
        public bool reportSMSCustomer { get; set; }
        public bool reportEmailCustomer { get; set; }
        public bool reportWhatsappCustomer { get; set; }
        public bool billSMSCustomer { get; set; }
        public bool billEmailCustomer { get; set; }
        public bool billWhatsappCustomer { get; set; }
        public bool reportSMSPatient { get; set; }
        public bool reportEmailPatient { get; set; }
        public bool reportWhatsappPatient { get; set; }
        public bool billSMSPatient { get; set; }
        public bool billEmailPatient { get; set; }
        public bool billWhatsappPatient { get; set; }
        public bool isNoReportHeaderFooter { get; set; }
        public bool IsPatientReportPortal { get; set; }
        public bool IsPatientInfoPortal { get; set; }
        public bool IsRegistrationPortal { get; set; }
    }

    public partial class PostCustomerMaster
    {
        public TblCustomer? tblcustomer { get; set; }
        public List<CustomerMappingDTO>? subclient { get; set; }
        public string? dashBoardDetailsJson { get; set; }
        public List<DocumentUploadlst>? documentUploadlst { get; set; }
        public bool isDocUpdModified { get; set; }
    }
    public class CustomerList
    {
        public long Rowno { get; set; }
        public int customerNo { get; set; }
        public string? customerName { get; set; }
        public string? customerEmail { get; set; }
        public string? customerMobile { get; set; }
        public decimal creditLimit { get; set; }
        public decimal balAmount { get; set; }
        public Boolean IsShowAmount { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public int RouteNo { get; set; }
        public bool IsClinic { get; set; }
        public bool IsCashCustomer { get; set; }
        public bool IsPrepaidCustomer { get; set; }
        public bool IsPostpaidCustomer { get; set; }
        public bool isBillEmail2Patient { get; set; }
        public bool isBillSms2Patient { get; set; }
        public bool isBillWhatsApp2Patient { get; set; }
        public bool isBillEmail2Customer { get; set; }
        public bool isBillSms2Customer { get; set; }
        public bool isBillWhatsApp2Customer { get; set; }
    }
    public class CustomerCurrentBalance
    {
        public int PayType { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal balAmount { get; set; }
        public Int16 CreditDays { get; set; }
        public Int16 CreditBalanceDays { get; set; }
    }

    public class AdvancePaymentList
    {
        public long Rowno { get; set; }
        public int CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public int Type { get; set; }
        public string? TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public string? Remarks { get; set; }
        public string? createdby { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public decimal CurrentAmt { get; set; } 
    }
    public class AdvancePaymentListRequest
    {
        public int CustomerNo { get; set; }
        public int Type { get; set; }
        public string TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public int userno { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public string ModeOfPayment { get; set; }
        public string Description { get; set; }
        public string? ModeOfType { get; set; }
        public List<AdvancePaymentTypes> advancePaymentTypes { get; set; }
    }
    public class AdvancePaymentListResponse
    {
        public int result { get; set; }
    }
    public partial class ClientDocUploadDetailRes
    {
        public string? documentType { get; set; }
        public string? documentNo { get; set; }
        public int documentTypeCode { get; set; }
        public int CustomerNo { get; set; }
        public List<ClientFileUpload>? clientfileUpload { get; set; }
    }
    public partial class ClientDocUploadRes
    {
        public string? documentType { get; set; }
        public string? documentNo { get; set; }
        public int documentTypeCode { get; set; }
        public int CustomerNo { get; set; }
    }
    public partial class ClientDocUploadReq
    {
        public string? EntityType { get; set; }
        public int EntityNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public class ClientFileUpload
    {
        public string? ActualFileName { get; set; }
        public string? ManualFileName { get; set; }
        public string? FileBinaryData { get; set; }
        public string? FileType { get; set; }
        public string? FilePath { get; set; }
        public string? ExternalVisitID { get; set; }
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? ActualBinaryData { get; set; }
    }
    public partial class PostCustomersubuserMaster
    {
        public int? CustomerSubUserNo { get; set; }
        public string? userName { get; set; }
        public string? LoginName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public bool status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int CreatedBy { get; set; }        
        public List<TblCustomersubuser> tblsubcustomer { get; set; }
    }
    public partial class TblCustomersubuser
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public bool isadd { get; set; }
    }
    public class AdvancePaymentTypes
    {
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ModeOfType { get; set; }

    }
}
    