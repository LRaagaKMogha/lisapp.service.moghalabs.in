using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblPhysician
    {
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? Qualification { get; set; }
        public string? PhysicianEmail { get; set; }
        public string? PhysicianMobileNo { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string? Speciality { get; set; }
        public string? Signature { get; set; }
        public int? CustomerNo { get; set; }
        public bool? IsReportSms { get; set; }
        public bool? IsReportEmail { get; set; }
        public int? VenueBranchNo { get; set; }
        public int? VenueNo { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int? MarketingNo { get; set; }
        public int? RiderNo { get; set; }
        public int? routeNo { get; set; }
        public int? RCNo { get; set; }       
        public bool? IsBillEmail { get; set; }
        public bool? IsBillSMS { get; set; }
        public string? specification { get; set; }
        public string? registrationNo { get; set; }
        public string? address { get; set; }
        public string? chamberaddress { get; set; }
        public string? pincode { get; set; }
        public string? area { get; set; }
        public string? dob { get; set; }
        public string? dom { get; set; }
        public bool? IsReportWhatsApp { get; set; }
        public bool? IsBillWhatsApp { get; set; }
        public string? WhatsAppNo { get; set; }
        public string? physicianCode {  get; set; }
        public string? MappinglocationNo { get; set; }
        public bool? IsNoReportHeaderFooter { get; set; }
        public bool? IsConsultant { get; set; }
        public Int16? SpecializationNo { get; set; }
        public byte? apptDuration { get; set; }
        public byte? vipDuration { get; set; }
        public byte? followUpDuration { get; set; }
        public int? physicianUserNo { get; set; }
        public string? OPDTiming { get; set; }
        public int? PhysicianBranchNo { get; set; }
        public decimal? apptAmount { get; set; }
        public decimal? apptFollowUpAmount { get; set; }
        public decimal? apptVIPAmount { get; set; }
        public string? opdNotes { get; set; }
        public int? gender { get; set; }
        public byte? apptCount { get; set; }
        public string? physicianusername { get; set; }
        public string Password { get; set; }
        public decimal apptAmtPerc { get; set; }
        public decimal apptFollowUpAmtPerc { get; set; }
        public decimal apptVIPAmtPerc { get; set; }


    }
    public class PostPhysicianMaster
    {
        public TblPhysician? tblPhysician { get; set; }
        public List<TblPhysician>? physicianMapping { get; set; }
        public List<DocumentUploadlst>? documentUploadlst { get; set; }
        public List<OPDPhysicianDetail>? opdPhysiciandetail { get; set; }
    }
    public partial class DocumentUploadlst
    {
        public string? documentNo { get; set; }
        public string? typeOfEntity { get; set; }
        public Int16 documentTypeCode { get; set; }
    }
    public partial class TblPhysicianSearch
    {
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public int MarketingNo { get; set; }
        public int RiderNo { get; set; }
    }
    public class PhysicianNo
    {
        public int physicianNo { get; set; }
    }
    public partial class PhysicianDocUploadReq
    {
        public string? EntityType { get; set; }
        public int EntityNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
    public partial class PhysicianDocUploadRes
    {
        public string? documentType { get; set; }
        public string? documentNo { get; set; }
        public int documentTypeCode { get; set; }
        public int physicianNo { get; set; }
    }
    public partial class PhysicianDocUploadDetailRes
    {
        public string? documentType { get; set; }
        public string? documentNo { get; set; }
        public int documentTypeCode { get; set; }
        public int physicianNo { get; set; }
        public List<PhysicianFileUpload>? physicianfileUpload { get; set; }
    }
    public class PhysicianFileUpload
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
    public partial class OPDMachineRes
    {
        public Int64 RowNo { get; set; }
        public int? DayNo { get; set; }
        public string? DayNames { get; set; }
        public string? SessionCode { get; set; }
        public string? SessionDesc { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
    public partial class OPDMachineReq
    {
        public int? VenueNo { get; set; }
        public int? Venuebno { get; set; }
        public int? MachineNo { get; set; }
    }
    public partial class OPDPhysicianReq
    {
        public int? VenueNo { get; set; }
        public int? Venuebno { get; set; }
        public int? PhysicianNo { get; set; }
        public int? PhysicianBranchNo { get; set; }
    }
    public partial class OPDPhysicianDetail
    {
        public int dayNo { get; set; }
        public string? endTime { get; set; }
        public string? startTime { get; set; }
        public string? sessionCode { get; set; }
    }
    public partial class OPDPhysicianRes
    {
        public Int64 RowNo { get; set; }
        public int? DayNo { get; set; }
        public string? DayNames { get; set; }
        public string? SessionCode { get; set; }
        public string? SessionDesc { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public byte? apptDuration { get; set; }
        public decimal apptAmount { get; set; }
        public decimal apptFollowUpAmount { get; set; }
        public decimal apptVIPAmount { get; set; }
        public string? OPDTiming { get; set; }
        public byte apptCount { get; set; }
        public decimal apptAmtPerc { get; set; }
        public decimal apptFollowUpAmtPerc { get; set; }
        public decimal apptVIPAmtPerc { get; set; }
    }
    public partial class PhysicianOrClientCodeResponse
    {
        public string PhysicianORClientCode { get; set; }
    }
    public class getconsultant
    {
        public int venueNo { get; set; }
        public int venuebranchNo { get; set; }
        public int consultantNo { get; set; }
    }
    public class consultantdetails
    {
        public int consultantNo { get; set; }
        public string? consultantName { get; set; }
        public string? roomNo { get; set; }
    }
    public class saveConsultant
    {
        public int venueNo { get; set; }
        public int venuebranchNo { get; set; }
        public int userNo { get; set; }
        public List<consultantdetails> consultantdetails { get; set; }
    }
    public class saveConsultantlst
    {
        public int consultantNo { get; set; }
    }
}