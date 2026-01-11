using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Sample
{
    public class GetSlidePrintingResponse
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public int VisitNo { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public string RchNo { get; set; }
        public bool IsPap { get; set; }
        public Int64 VisitRow { get; set; }
        public string IdNumber { get; set; }
        public string ApprovalDoctor { get; set; }

        public Int16 Quantity { get; set; }

        public bool isVipIndication { get; set; }


    }
    public class GetSlidePrintPatientDetailsResponse
    {

        public Int64 Sno { get; set; }
        public bool RCNo { get; set; }
        public bool RHNo { get; set; }
        public bool RPNo { get; set; }
        public string RCHNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string RegisterDate { get; set; }
        public string LabRequestNo { get; set; }
        public string ReportDate { get; set; }
        public string PatientNRIC { get; set; }
        public string DiscardDate { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Remarks { get; set; }
        public string Consultant { get; set; }
        public string FooterNote { get; set; }
        public string TissueAudit { get; set; }
        public string ReportStatus { get; set; }
        public string AmedReason { get; set; }
        public string AmedBy { get; set; }
        public string AmedDate { get; set; }
        public int SpecimenNo { get; set; }
        public int Block { get; set; }
        public int TrimmedBy { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string SpecimenDateTime { get; set; }
        public int SlideSpecimenNo { get; set; }
        public int SlideBlock { get; set; }
        public int Level { get; set; }
        public int StainType { get; set; }
        public bool Selected { get; set; }
        public bool SlideSelected { get; set; }
        public int SpecimenType { get; set; }
        public int SlideSpecimenType { get; set; }
        public int Centrifuge { get; set; }
        public int Cytospin { get; set; }
        public int Adequacy { get; set; }
        public int Brusing { get; set; }
        public int CellBlocking { get; set; }
        public int Pap { get; set; }
        public int HcStain { get; set; }
        public int HeStain { get; set; }
        public int LabelPrintingType { get; set; }
        public string Others { get; set; }
        public int TissueProcessor { get; set; }
        public bool SecondConsultation { get; set; }
        public string SlideDate { get; set; }
        public string photo { get; set; }
        public int ApprovalDoctor { get; set; }
        public string SpecimenOthers { get; set; }
        public bool IsShowSpecimenOthers { get; set; }
        public int SampleSource { get; set; }
        public string SampleSourceDesc { get; set; }
        public bool IsReject { get; set; }
        public string RejectionCode { get; set; }

    }
    public class SlidePrintPatientDetailsResponse
    {
        public Int64 Sno { get; set; }
        public bool RCNo { get; set; }
        public bool RHNo { get; set; }
        public bool RPNo { get; set; }
        public string RCHNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string RegisterDate { get; set; }
        public string LabRequestNo { get; set; }
        public string ReportDate { get; set; }
        public string PatientNRIC { get; set; }
        public string DiscardDate { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Remarks { get; set; }
        public string Consultant { get; set; }
        public string FooterNote { get; set; }
        public string TissueAudit { get; set; }
        public string ReportStatus { get; set; }
        public string AmedReason { get; set; }
        public string AmedBy { get; set; }
        public string AmedDate { get; set; }
        public List<Specimen> specimens { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool Status { get; set; }
        public int UserNo { get; set; }
        public int TissueProcessor { get; set; }
        public int LabelPrintingType { get; set; }
        public string Others { get; set; }
        public bool SecondConsultation { get; set; }
        public string photo { get; set; }
        public string DepartmentType { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceName { get; set; }
        public string IsRCHNo { get; set; }
        public int ApprovalDoctor { get; set; }
        public int SampleSource { get; set; }
        public string SampleSourceDesc { get; set; }
        public bool IsReject { get; set; }
        public string RejectionCode { get; set; }
    }
    public class Specimen
    {
        public bool Selected { get; set; }
        public int SpecimenType { get; set; }
        public int SpecimenNo { get; set; }
        public int Block { get; set; }
        public int TrimmedBy { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string SpecimenDateTime { get; set; }
        public List<Slide> slides { get; set; }
        public int Centrifuge { get; set; }
        public int Cytospin { get; set; }
        public int Adequacy { get; set; }
        public int Brusing { get; set; }
        public int CellBlocking { get; set; }
        public int Pap { get; set; }
        public int HcStain { get; set; }
        public int HeStain { get; set; }
        public int TissueProcessor { get; set; }
        public string SpecimenOthers { get; set; }
        public bool IsShowSpecimenOthers { get; set; }
    }
    public class Slide
    {
        public int SlideSpecimenType { get; set; }
        public bool SlideSelected { get; set; }
        public int SlideSpecimenNo { get; set; }
        public int SlideBlock { get; set; }
        public int Level { get; set; }
        public int StainType { get; set; }
        public string SlideDate { get; set; }
    }


    public class ExistingRCHNoResponse
    {
        public Int64 Sno { get; set; }
        public int VisitNo { get; set; }
        public string LabRequestNo { get; set; }
        public string PatientNRIC { get; set; }
        public string PatientName { get; set; }
        public string RCHNo { get; set; }
        public string CreatedOn { get; set; }
        public string DepartmentName { get; set; }
        public int ServiceNo { get;set; }
        public string ServiceName { get; set; }
        public bool IsPap { get; set; }

    }

    public class GetBulkSlidePrintingDetails
    {
        public Int64 Sno { get; set; }
        public string RCHNo { get; set; }
        public int VisitNo { get; set; }
        public string LabRequestNo { get; set; }
        public string PatientNRIC { get; set; }
        public string PatientName { get; set; }
        public string SpecimenDateTime { get; set; }
        public int SlideBlock { get; set; }
        public int Level { get; set; }
        public int StainTypeNo { get; set; }
        public string SpecimenType { get; set; }
        public string SpecimenName { get; set; }
        public string StainType { get; set; }
        public string DepartmentType { get; set; }

    }

    public class GetBulkSlidePrintingRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string FromRCHNo { get; set; }
        public int UserNo { get; set; }
        public string ToRCHNo { get; set; }

    }
}

