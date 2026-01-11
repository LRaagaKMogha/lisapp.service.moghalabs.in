using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Sample
{

    public class GetManageSampleDTO
    {
        public Int32 TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public Int64 Row_num { get; set; }
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
        public int SampleNo { get; set; }
        public int OldSampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public int OldContainerNo { get; set; }
        public string ContainerName { get; set; }
        public string SampleCollectedDate { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public string Rct { get; set; }
        public int OrdersNo { get; set; }
        public int OrderListNo { get; set; }
        public string OrderCode { get; set; }
        public string OrderType { get; set; }
        public bool IsStat { get; set; }
        public int TATFlag { get; set; }
        public bool IsSelectMultiSample { get; set; }
        public bool IsRejected { get; set; }
        public bool collectatsource { get; set; }
        public string IncludeInstruction { get; set; }
        public bool IsincludeInstruction { get; set; }
        public int specimenQty { get; set; }
        public int multiSampleTestno { get; set; }
        public string IDnumber { get; set; }
        public int fastingOrNonfasting { get; set; }
        public bool IsVipIndication { get; set; }
        public int ServiceNo { get; set; }
        public string BarcodeNo { get; set; }
    }
}


