using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class CustomerResponse
    {
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int? CustomerNo { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? UserName { get; set; }
        public string? CustomerMobileNo { get; set; }
        public decimal? CreditLimit { get; set; }
        public int CreditPeriod { get; set; }
        public int CustomerType { get; set; }
        public string? CustomerTypeValue { get; set; }
        public string? ContactPersonName { get; set; }
        public int? ClientPayType { get; set; }
        public string? ClientPayTypeValue { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public int? City { get; set; }
        public string? CityName { get; set; }
        public string? Pincode { get; set; }
        public string? ClientUsername { get; set; }
        public bool? CpBilling { get; set; }
        public bool? CpReportView { get; set; }
        public bool? CpBillView { get; set; }
        public bool? CpBlock { get; set; }
        public bool? BillingBlock { get; set; }
        public bool? ReportDispatchBlock { get; set; }
        public bool? ClientBlock { get; set; }
        public bool? Active { get; set; }
        public bool? status { get; set; }
        public string? password { get; set; }
        public int? state { get; set; }
        public int? country { get; set; }
        public bool? IsReportEmail { get; set; }
        public bool? IsReportSMS { get; set; }
        public bool IsShowAmount { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public string? secondaryemail { get; set; }
        public string? hcicode { get; set; }
       
        public int RouteNo { get; set; }
        public int RestrictionDays { get; set; }
        public bool IsBillEmail { get; set; }
        public bool IsBillSMS { get; set; }
        public string? CShortName { get; set; }
        public bool? fCBilling { get; set; }
        public bool? sampleScreen { get; set; }
        public bool isbillwhatsapp { get; set; }
        public bool isreportwhatsapp { get; set; }
        public bool isPatientEmail { get; set; }
        public bool collectatsource { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public int OldCustomerNo { get; set; }
        public string? ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedOn { get; set; } 
        public string RejectReason { get; set; }
        public bool IsClinic { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsInternal { get; set; }
        public bool IsSpeciality { get; set; }
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

    public class InsertCustomerResponse
    {
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? Url { get; set; }
        public string? email { get; set; }
        public int CustomerNo { get; set; }
    }
    public class InsertCustomersubuserResponse
    {
        public int status { get; set; }
    }
    public class ClientSubUserResponse
    {
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int? CustomerSubUserNo { get; set; }
        public string? userName { get; set; }
        public string? LoginName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string CustomerNo { get; set; }
        public bool status { get; set; }
    }
}
