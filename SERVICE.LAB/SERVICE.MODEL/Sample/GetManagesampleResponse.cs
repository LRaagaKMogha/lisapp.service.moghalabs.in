using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class GetManagesampleResponse
    {
        public Int32 TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int PatientNo { get; set; }
        public string PatientName { get; set; }
        public string PrimaryId { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string Age { get; set; }
        public string RefferedBy { get; set; }
        public string RegistrationDate { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool SelectPatient { get; set; }
        public bool IsStat { get; set; }
        public List<SampleDetails> sampleDetails { get; set; }
        public int TATFlag { get; set; }
        public bool IsRejected { get; set; }    
        public string IncludeInstruction { get; set; }
        public bool IsIncludeInstruction { get; set; }
        public string IDnumber { get; set; }
        public bool IsVipIndication { get; set; }
    }
    public class SampleDetails
    {
        public int SampleNo { get; set; }
        public int OldSampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public int OldContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string SampleCollectedDate { get; set; }
        public string BarcodeNo { get; set; }
        public bool SelectSample { get; set; }
        public List<TestDetails> testDetails { get; set; }
        public bool ishigtemprature { get; set; }
        public string higTempValue { get; set; }
        public bool collectatsource { get; set; }
        public int specimenQty { get; set; }

        public int fastingOrNonfasting { get; set; }
    }
    public class TestDetails
    {
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string Rct { get; set; }
        public int OrdersNo { get; set; }
        public int OrderListNo { get; set; }
        public string OrderCode { get; set; }
        public string OrderType { get; set; }
        public int ChangedSample { get; set; }
        public List<MultiSampleList> lstMultiSamples{get;set;}
        public bool isnotgiven { get; set; }
        public bool isbarcodenotreq { get; set; }
        public int ServiceNo { get; set; }
    }
    public class BarcodePrintResponse
    {
        public string PatientName { get; set; }
        public string PatientID { get; set; }
        public string VisitId { get; set; }
        public string Age { get; set; }
        public string AgeType { get; set; }
        public string UrnId { get; set; }
        public string DOB { get; set; }
        public string AccessionNo { get; set; }
        public bool IsStat { get; set; }
        public bool IsVIP { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public int SourceOfSpecimen { get; set; }
        public string SourceOfSpecimenDesc { get; set; }
        public string BarcodeNo { get; set; }
        public string BarcodeShortNames { get; set; }
        public string SampleCollectedDTTM { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string DepartmentName { get; set; }
        public string Mobile {  get; set; }
        public string TestShortNames { get; set; }
        public Int16 NoOfCopies { get; set; }
        public int TubeQty { get; set; }
    }
}
