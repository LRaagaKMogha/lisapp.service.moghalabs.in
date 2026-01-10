using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class CreateManageOptionalResponse
    {
        public int result { get; set; }
    }
    public class CreateManageSampleResponse
    {
        public Int64 Sno { get; set; }
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public string AgeType { get; set; }
        public string Gender { get; set; }
        public string VisitID { get; set; }
        public int OrderListNo { get; set; } = 0;
        public string BarcodeNo { get; set; }
        public string SampleName { get; set; }
        public int SampleNo { get; set; } = 0;
        public string ContainerName { get; set; }
        public int ContainerNo { get; set; } = 0;
        public string CollectedDTTM { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string DepartmentName { get; set; }
        public bool IsAccept { get; set; }
        public bool IsReject { get; set; }
        public string Urnid { get; set; }
        public string DOB { get; set; }
        public string CollectionCenterCode { get; set; }
        public string AccessionNo { get; set; }
        public string Mobile { get; set; }
        //  public bool IsBarcodeNotReq { get; set; }
        public string TubeNumber { get; set; }
        public string TestShortNames { get; set; }
        public string BarcodeShortNames { get; set; }
        public bool IsStat { get; set; }
        public bool IsVIP { get; set; }
        public int TubeQty { get; set; }
        public Int16 NoOfCopies { get; set; }
    }  
}



