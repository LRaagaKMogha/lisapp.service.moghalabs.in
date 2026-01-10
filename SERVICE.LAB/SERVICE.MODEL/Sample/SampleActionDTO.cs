using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class SampleActionDTO
    {
        public Int64 Row_Num { get; set; }
        public string BarCodeNo { get; set; }
        public Int32 patientno { get; set; }
        public string PatientName { get; set; }
        public string AgeType { get; set; }
        public string Gender { get; set; }
        public string Physician { get; set; }
        public string PatientId { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int patientVisitNo { get; set; }
        public string VisitId { get; set; }
        public string SampleCollectedDTTM { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string DepartmentName { get; set; }
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public bool IsAccept { get; set; }
        public bool IsReject { get; set; }
        public string Remarks { get; set; }
        public int orderListNo { get; set; }
        public int TATFlag { get; set; }
        public bool IsSample { get; set; }

        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
        public int notifyCount { get; set; }
        public bool IsOutSource { get; set; }

    }
    public partial class SampleActionRequest
    {
        public string PageCode { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public string Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int DeptNo { get; set; }
        public string Searchkey { get; set; }
        public int UserNo { get; set; }
        public int VisitNo { get; set; }
        public int PatientNo { get; set; }
        public int NotifyCount { get; set; }
        public string Barcode { get; set; }
        public bool ISAck { get; set; }
    }  
    public class SearchBarcodeResponse
    {
        public int patientvisitno { get; set; }
        public string displaytext { get; set; }
        public string searchdisplaytext { get; set; }
        public int statusno { get; set; }
    }    
    public class SearchBranchSampleBarcodeResponse
    {
        public int patientvisitno { get; set; }
        public string displaytext { get; set; }
        public string searchdisplaytext { get; set; }
        public int statusno { get; set; }
    }

    public partial class BranchSampleActionDTO
    {
        public Int64 Row_Num { get; set; }
        public string? BarCodeNo { get; set; }
        public Int32 patientno { get; set; }
        public string? PatientName { get; set; }
        public string? AgeType { get; set; }
        public string? Gender { get; set; }
        public string? Physician { get; set; }
        public string? PatientId { get; set; }
        public int SampleNo { get; set; }
        public string? SampleName { get; set; }
        public int patientVisitNo { get; set; }
        public string? VisitId { get; set; }
        public string? SampleCollectedDTTM { get; set; }
        public int TestNo { get; set; }
        public string? TestName { get; set; }
        public string? DepartmentName { get; set; }
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public bool IsAccept { get; set; }
        public bool IsReject { get; set; }
        public string? Remarks { get; set; }
        public int orderListNo { get; set; }
        public int TATFlag { get; set; }
        public bool IsSample { get; set; }
        public string? PhysicianEmail { get; set; }
        public string? PhysicianMobileNumber { get; set; }
        public string? PhysicianWhatsAppNo { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMobileNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? testShortName { get; set; }
        public bool IsStat { get; set; }
        public string? VenueBranchName { get; set; }
        public Int32 OrderTransactionNo { get; set; }
    }

    public class PrePrintBarcodeOrderResponse
    {
        public Int64 Row_Num { get; set; }
        public string PatientName { get; set; }
        public string VisitID { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string BarcodeNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string SampleName { get; set; }
        public string ContainerName { get; set; }
        public string DepartmentName { get; set; }
        public string AccessionNo { get; set; }
        public string Urnid { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string CollectedDTTM { get; set; }
        public int TubeQty { get; set; }
        public string TubeNumber { get; set; }
        public string TestShortNames { get; set; }
        public string BarcodeShortNames { get; set; }
        public bool IsStat { get; set; }
        public bool IsVIP { get; set; }        
        public Int16 NoOfCopies { get; set; }
    }
}
