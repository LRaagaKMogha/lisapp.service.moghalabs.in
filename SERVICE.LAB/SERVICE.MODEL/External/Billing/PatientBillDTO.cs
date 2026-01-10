using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DEV.Model.External.Billing
{
  
    public partial class LstPatientBillingInfo
    {
        public Int32 patientNo { get; set; }
        public string? patientName { get; set; }
        public string? dob { get; set; }
        public int ageInYears { get; set; }
        public int ageInMonths { get; set; }
        public int ageInDays { get; set; }
        public string? gender { get; set; }
        public string? mobileNo { get; set; }
        public string? emailId { get; set; }
        public Int32 patientVisitNo { get; set; }
        public string? referralType { get; set; }
        public Int32? customerNo { get; set; }
        public string? customerName { get; set; }
        public Int32? physicianNo { get; set; }
        public string? physicianName { get; set; }
        [IgnoreDataMember]
        public string? serviceType { get; set; }
        [IgnoreDataMember]
        public Int32 serviceNo { get; set; }
        [IgnoreDataMember]
        public string? serviceName { get; set; }
        [IgnoreDataMember]
        public decimal rate { get; set; }
        public List<BillServiceDetails>? servicelist { get; set; }
        public List<BillPaymentDetails>? payments { get; set; }
        public decimal net { get; set; }
        public decimal discount { get; set; }
        public decimal gross { get; set; }
        public decimal collected { get; set; }
        public decimal due { get; set; }
    }
    public class BillServiceDetails
    {
        [IgnoreDataMember]
        public int patientVisitNo { get; set; }
        public string? serviceType { get; set; }
        public Int32 serviceNo { get; set; }
        public string? serviceName { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal net { get; set; }
        public int OrderListStatus { get; set; }
        public bool CancelledFlag { get; set; }
    }

    public class BillPaymentDetails
    {
        [IgnoreDataMember]
        public int patientVisitNo { get; set; }
        public string? modeOfPay { get; set; }
        public decimal amount { get; set; }
        public string? payDescription { get; set; }
        public string? payType { get; set; }
    }

    public partial class LstPatientCancelBillingInfo
    {
        public Int32 patientNo { get; set; }
        public string? patientName { get; set; }
        public string? dob { get; set; }
        public int ageInYears { get; set; }
        public int ageInMonths { get; set; }
        public int ageInDays { get; set; }
        public string? gender { get; set; }
        public string? mobileNo { get; set; }
        public string? emailId { get; set; }
        public Int32 patientVisitNo { get; set; }
        public string? referralType { get; set; }
        public string? customerName { get; set; }
        public string? physicianName { get; set; }
        [IgnoreDataMember]
        public string? serviceType { get; set; }
        [IgnoreDataMember]
        public string? serviceName { get; set; }
        [IgnoreDataMember]
        public decimal rate { get; set; }
        public List<CancelBillServiceDetails>? servicelist { get; set; }
        public List<CancelBillPaymentDetails>? payments { get; set; }
        public decimal net { get; set; }
        public decimal discount { get; set; }
        public decimal gross { get; set; }
        public decimal collected { get; set; }
        public decimal due { get; set; }
    }
    public class CancelBillServiceDetails
    {
        [IgnoreDataMember]
        public int patientVisitNo { get; set; }
        public string? serviceType { get; set; }
        public Int32 serviceNo { get; set; }
        public string? serviceName { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal net { get; set; }
        public int OrderListStatus { get; set; }
        public bool CancelledFlag { get; set; }
    }

    public class CancelBillPaymentDetails
    {
        [IgnoreDataMember]
        public int patientVisitNo { get; set; }
        public string? modeOfPay { get; set; }
        public decimal amount { get; set; }
        public string? payDescription { get; set; }
        public string? payType { get; set; }
    }
}
