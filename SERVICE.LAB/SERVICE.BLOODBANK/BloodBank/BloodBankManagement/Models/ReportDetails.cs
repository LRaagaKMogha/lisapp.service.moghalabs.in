using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DEV.Model.Integration
{
    public enum nsourcesystem
    {
        RCMS = 1,
        SAP = 2,
        EMR = 3,
        BB = 4,
        LIS = 5,
        RC = 6,
        MA = 7
    }
    public class reportrequestdetails
    {
        [MaxLength(20)]
        public string visitnumber { get; set; }
        [MaxLength(20)]
        public string casenumber { get; set; }
        public nsourcesystem sourcesystem { get; set; }
        [MaxLength(36)]
        public string sourcerequestid { get; set; }
        [MaxLength(30)]
        public string patientnumber { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
    public class reportresponsedetails
    {
        public string referenceno { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
        public List<labresponsedetails> labresponsedetails { get; set; }
        [JsonIgnore]
        public List<labresponsedetails> labdetails { get; set; }
    }
    public class labresponsedetails
    {
        [JsonIgnore]
        public string accessionno { get; set; }
        [MaxLength(20)]
        public string visitnumber { get; set; }
        [JsonIgnore]
        public int lisvisitno { get; set; }
        [MaxLength(20)]
        public string casenumber { get; set; }
        [MaxLength(20)]
        public string SourceSystem { get; set; }
        [MaxLength(36)]
        public string SourceRequestID { get; set; }
        public DateTime labregistereddttm { get; set; }
        public List<labreportdetails> reportdetails { get; set; }

    }
    public class labreportdetails
    {
        public string accessionno { get; set; }

        public byte[]? reportdata { get; set; }
        public List<labtestdetails> testdetails { get; set; }
        [JsonIgnore]
        public string TestDescription { get; set; }
        [JsonIgnore]
        public string TestStatus { get; set; }
    }
    public class labtestdetails
    {
        public string TestDescription { get; set; }

        public string TestStatus { get; set; }

        [JsonIgnore]
        public int OrderListNo { get; set; }
        [JsonIgnore]
        public int ResultTypeNo { get; set; }

    }
    public class reportresponsediscreetdetails
    {
        public string referenceno { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
        public List<labreportdiscreetdetails> reportdetails { get; set; }
    }
    public class labreportdiscreetdetails
    {
        [MaxLength(10)]
        public string labrequestNo { get; set; }
        public int PAgeDay { get; set; }
        [MaxLength(64)]
        public string ProfileDesc { get; set; }
        [MaxLength(20)]
        public string VisitNumber { get; set; }
        public bool AbNormalStatus { get; set; }
        [MaxLength(100)]
        public string HSSComment { get; set; }

        public bool IsMicrobiology { get; set; }
        [MaxLength(50)]
        public string RangeTypeComment { get; set; }
        [MaxLength(5)]
        public string TestCodeReference { get; set; }
        [MaxLength(100)]
        public string ResultComment { get; set; }
        [MaxLength(2)]
        public string ResultStatus { get; set; }
        [MaxLength(50)]
        public string Section { get; set; }
        [MaxLength(2)]
        public string SectionSeq { get; set; }
        [MaxLength(100)]
        public string RangeRemark { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        public DateTime Testdate { get; set; }
        [MaxLength(64)]
        public string TestDesc { get; set; }
        [MaxLength(2)]
        public string TestPrefix { get; set; }

        public string TestResult { get; set; }
        [MaxLength(4)]
        public string TestSeq { get; set; }
        [MaxLength(1)]
        public string TestType { get; set; }
        [MaxLength(10)]
        public string TestUnit { get; set; }

    }

    public class LabReportTestDetails
    {
        public int PatientVisitNo { get; set; }
        public string  AgeGender { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDTTM { get; set; }
        public string ReferralType { get; set; }
        public int OrderListNo { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentSeqNo { get; set; }
        public string SampleName { get; set; }
        public string BarcodeNo { get; set; }
        public string ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public string GroupName { get; set; }
        public int GroupSeqNo { get; set; }
        public int ResultTypeNo { get; set; }
        public int OrderDetailsNo { get; set; }
        public string TestType { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int TSeqNo { get; set; }
        public int SubTestNo { get; set; }
        public string SubTestName { get; set; }
        public int SSeqNo { get; set; }
        public string ResultType { get; set; }
        public string MethodName { get; set; }
        public string UnitName { get; set; }
        public string DisplayRR { get; set; }
        public string Result { get; set; }
        public bool ResultFlag { get; set; }
        public string ResultComments { get; set; }
        public string Comments { get; set; }
        public string PackageName { get; set; }
        public DateTime SampleCollectedDTTM { get; set; }
    }
}
