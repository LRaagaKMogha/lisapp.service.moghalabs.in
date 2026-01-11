using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class PhysicianDetailsResponse
    {
        public Int32 TotalRecords { get; set; }
        public Int32 PageIndex { get; set; }
        public int PhysicianNo { get; set; }
        public string PhysicianName { get; set; }
        public string Qualification { get; set; }
        public string PhysicianEmail { get; set; }
        public string PhysicianMobileNo { get; set; }
        public bool Status { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string Speciality { get; set; }
        public string Signature { get; set; }
        public int? CustomerNo { get; set; }
        public bool IsReportSms { get; set; }
        public bool IsReportEmail { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
        public int? RCNo { get; set; }
        public int? routeNo { get; set; }
        public string routeName { get; set; }
        public bool IsBillEmail { get; set; }
        public bool IsBillSMS { get; set; }
        public string specification { get; set; }
        public string registrationNo { get; set; }
        public string address { get; set; }
        public string chamberaddress { get; set; }
        public string pincode { get; set; }
        public string area { get; set; }
        public string dob { get; set; }
        public string dom { get; set; }
        public bool IsReportWhatsApp { get; set; }
        public bool IsBillWhatsApp { get; set; }
        public string WhatsAppNo { get; set; }
        public string physicianCode { get; set; }
        public string MappinglocationNo { get; set; }
        public bool IsNoReportHeaderFooter { get; set; }
        public bool IsConsultant { get; set; }
        public Int16 SpecializationNo { get; set; }
        public int PhysicianBranchNo { get; set; }
        public int physicianUserNo { get; set; }
        public string? PortalURL { get; set; }
        public string? physicianusername { get; set; }

    }    
}