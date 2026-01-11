using Service.Model.External.Billing;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Service.Model.External.Patient
{
    
    public partial class LstPatientInfo
    { 
        public string? patientId { get; set; }
        public string? patientName { get; set; }
        public int ageInYears { get; set; }
        public int ageInMonths { get; set; }
        public int ageInDays { get; set; }
        public string? gender { get; set; }
        public int visitNo { get; set; }
        public string? visitId { get; set; }
        public string? visitDtTm { get; set; }
        public string? refferedBy { get; set; }
        public bool IsDue { get; set; }
    }

    public partial class LstCancelPatientInfo
    {
        public string? patientId { get; set; }
        public string? patientName { get; set; }
        public int ageInYears { get; set; }
        public int ageInMonths { get; set; }
        public int ageInDays { get; set; }
        public string? gender { get; set; }
        public int visitNo { get; set; }
        public string? visitId { get; set; }
        public string? visitDtTm { get; set; }
        public string? referredBy { get; set; }
        public decimal gross { get; set; }
        public decimal discount { get; set; }
        public decimal net { get; set; }
        public decimal collected { get; set; }
        public decimal due { get; set; }
        public decimal cancelled { get; set; }
        public decimal refund { get; set; }
        [IgnoreDataMember]
        public string? listOfServices { get; set; }
        public List<LstCancelServiceList>? servicesList { get; set; }
    }

    public partial class LstCancelServiceList
    {
        public int serviceNo { get; set; }
        public string? serviceName { get; set; }
        public string? serviceType { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal net { get; set; }
        public string? reason { get; set; }
    }
}