using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetEditPatientDetailsFinalResponse
    {
        public Int64 Row_Num { get; set; }
        public Int32 PatientNo { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public int ageYears { get; set; }
        public int ageMonths { get; set; }
        public int ageDays { get; set; }
        public string DOB { get; set; }
        public int Gender { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string EmailID { get; set; }
        public string SecondaryEmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string SecondaryAddress { get; set; }
        // public Int16 maritalStatus { get; set; }
        public string URNID { get; set; }
        public string URNType { get; set; }
        public bool IsStat { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public Int32 PatientVisitNo { get; set; }
        public Int32 OrderNo { get; set; }
        public Int32 OrderListNo { get; set; }
        public string ServiceType { get; set; }
        public Int32 ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public decimal Rate { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public List<EditBillServiceDetails> servicelist { get; set; }
        public List<GetEditBillPaymentDetails> payments { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string ReferralType { get; set; }
        public int RefferralTypeNo { get; set; }
        public int CustomerNo { get; set; }
        public int PhysicianNo { get; set; }
        public string CustomerName { get; set; }
        public string PhysicianName { get; set; }

        public string DueRemarks { get; set; }
        public string ExternalVisitIdentity { get; set; }
        public int WardNo { get; set; }
        public string WardName { get; set; }
        public string IPOPNumber { get; set; }
        public decimal GivenAmount { get; set; }
        public decimal ToBeReturn { get; set; }
        public Int64 HCPatientNo { get; set; }
        public Int16 IsDiscountApprovalReq { get; set; }
        public string NRICNumber { get; set; }
        public int NationalityNo { get; set; }
        public string AlternateIdType { get; set; }
        public string AlternateId { get; set; }
        public int RaceNo { get; set; }
        public bool IsAutoEmail { get; set; }
        public bool IsAutoSMS { get; set; }
        public bool IsAutoWhatsApp { get; set; }
        public string ExtenalVisitID { get; set; }
        public string AllergyInfo { get; set; }
        public string PatientBlock { get; set; }
        public string PatientUnitNo { get; set; }
        public string PatientFloor { get; set; }
        public string PatientBuilding { get; set; }
        public string PatientHomeNo { get; set; }
        public int PhysicianNo2 { get; set; }
        public bool IsVipIndication { get; set; }
        public int BedNo { get; set; }
        public int CompanyNo { get; set; }
        public string CaseNumber { get; set; }
        public string PatientOfficeNumber { get; set; }
        public bool IsPregnant { get; set; }
        public string Remarks { get; set; }
        public bool IsExistsInvoice { get; set; }
    }


    public class EditBillServiceDetails
    {
        public Int64 Row_Num { get; set; }
        public string servicetype { get; set; }
        public Int32 Serviceno { get; set; }
        public string servicecode { get; set; }
        public string servicename { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public string discounttype { get; set; }
        public decimal discount { get; set; }
        public decimal Netamount { get; set; }
        public decimal Amount { get; set; }
        public int rateListNo { get; set; }
        public Boolean isAmountEditable { get; set; }
        public string Status { get; set; }
        public int OrderListStatus { get; set; }
        public bool IsEdit { get; set; }
        public bool CancelledFlag { get; set; }
        public string serviceCodeNo { get; set; }
        public string includeInstruction { get; set; }
        public Boolean isIncludeInstruction { get; set; }
    }

    public class GetEditBillPaymentDetails
    {
        public Int64 Row_Num { get; set; }
        public Int32 PatientVisitNo { get; set; }
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public int currencyNo { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal CurrencyAmount { get; set; }
    }
    public class GetBillInvoiceExists
    {
        public bool IsExists { get; set; }
    }
}

