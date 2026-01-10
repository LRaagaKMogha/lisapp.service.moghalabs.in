using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class CommonFilterRequestDTO
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public string Registertype { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public int CustomerNo { get; set; }
        public int PatientNo { get; set; }
        public int pageIndex { get; set; }
        public int userNo { get; set; }
        public string PageCode { get; set; }
        public int visitNo { get; set; }
        public int? refferalType { get; set; }
        public int filterCustomerNo { get; set; }
        public int physicianNo { get; set; }
        public int FranchiseNo { get; set; }
        public int departmentNo { get; set; }
        public Int16 maindeptNo { get; set; }
        public int serviceNo { get; set; }
        public string serviceType { get; set; }
        public int orderStatus { get; set; }
        public bool isSTATFilter { get; set; }
        public int loginType { get; set; }
        public int routeNo { get; set; }
        public int pageCount { get; set; }
        public int GridShowFilter { get; set; }
        public int AnalyzerNo { get; set; }
        public int isStat { get; set; }
        public int tatStatus { get; set; }
        public int reporttype { get; set; }
        public int searchType { get; set; }
        public int clinicno { get; set; }
        public int maindepartmentno { get; set; }
        public int vendorNo { get; set; }
        public int transferBranchNo { get; set; }
        public int transferModeNo { get; set; }
        public string MultiFieldsSearch { get; set; }
        public int AppointmentCategory { get; set; }
        public int AppointmentMode { get; set; }
        public int BilledBranchNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PatientName { get; set; }
        public string? AppointmentNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? SpecializationName { get; set; }
        public int BookingType { get; set; }
        public int ResourceNo { get; set; }
        public int BookingStatus { get; set; }
        public bool ViewType { get; set; }
        public int? machineNo {  get; set; }
        public string multiDeptNo { get; set; }
    }
    public class CommonSearchRequest
    {
        public string SearchKey { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class GetImpressionReportRequest
    {
        public int UserNo { get; set; }
        public string ReportKey { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string fileType { get; set; }
        public CommonFilterRequestDTO objData { get; set; }
        public List<ReportKeyParamDTO> ReportParamitem { get; set; }
    }
    public class SlidePrintingRequest : CommonFilterRequestDTO
    {
        public string outSourceType { get; set; }
        public string departmentType { get; set; }
        public string fromrp { get;set; }
        public string torp { get;set; }
    }
    public class BarcodePrintRequest
    {
        public string RequestType { get; set; } = string.Empty;
        public int PtVisitNo { get; set; }
        public int OrderListNo {  get; set; }
        public int SampleNo { get; set; } = 0;
        public string BarcodeNo { get; set; } = string.Empty;
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get;set; }
    }
    public class GetHcDocumentsDetailsRequest
    {
        public int PatientVisitNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
    }

    public class GetHcDocumentsDetailsResponse
    {
        public int HCPatientNo { get; set; }
        public string HCPatientID { get; set; }
    }
}




