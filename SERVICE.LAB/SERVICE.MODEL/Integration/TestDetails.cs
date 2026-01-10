using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DEV.Model.Integration
{
    public class TestMasterDetails
    {      
        public int TestNo { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        [MaxLength(100)]
        public string TestName { get; set; }
        [MaxLength(10)]
        public string TestShortName { get; set; }
        [MaxLength(100)]
        public string DepartmentName { get; set; }
        public int? SampleNo { get; set; }
        [MaxLength(40)]
        public string SampleName { get; set; }
        public int? ContainerNo { get; set; }
        [MaxLength(40)]
        public string ContainerName { get; set; }
        public int NoofTubes { get; set; }
        public string SampleVolume { get; set; }
        public string Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set;}
        public string ModifiedBy { get; set; }
        public bool isDelta { get; set; }
        public bool IsProfile { get; set; }
        public string testlist { get; set; }
        public bool? IsGroupConfigAsPackage { get; set; }
        public string testcodelist { get; set; }
    }
    public class TestDetailsRequest
    {
        public bool IsDelta { get; set; }
        public DateTime LastSynceddate { get; set; }

    }
    public class TestDetailsResponse
    {
        public List<TestMasterDetails> testdetails { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }
    public class Grams
    {
        [MaxLength(30)]
        public string GramStrain { get; set; }
        [MaxLength(5)]
        public string GramValue { get; set; }
    }
    public class Sensitivity
    {
        [MaxLength(40)]
        public string SensitivityDesc { get; set; }
        [MaxLength(1)]
        public string SensitivityValue { get; set; }
    }
    public class Cultures
    {
        [MaxLength(40)]
        public string CultureDesc { get; set; }
    }
    public class Micro_mbtest04
    {
        [MaxLength(254)]
        public string CultureRemark { get; set; }
        public Cultures Cultures { get; set; }
        [MaxLength(20)]
        public string NoGram { get; set; }
        [MaxLength(10)]
        public string ReportStatus { get; set; }
        public List<Sensitivity> Sensitivity { get; set; }
        [MaxLength(254)]
        public string SensitivityRemark { get; set; }
        [MaxLength(50)]
        public string Specimen { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        [MaxLength(64)]
        public string TestDesc { get; set; }
        [MaxLength(5)]

        public string Trichomonas { get; set; }
        [MaxLength(254)]
        public string WetRemark { get; set; }
        [MaxLength(5)]
        public string YeastCell { get; set; }

    }
    public class Micro_mbtest03
    {
        [MaxLength(254)]
        public string CultureRemark { get; set; }
        public Cultures Cultures { get; set; }
        [MaxLength(10)]
        public string ReportStatus { get; set; }
        public List<Sensitivity> Sensitivity { get; set; }
        [MaxLength(254)]
        public string SensitivityRemark { get; set; }
        [MaxLength(50)]
        public string Specimen { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        [MaxLength(64)]
        public string TestDesc { get; set; }

    }

    public class Micro_mbtest02
    {
        [MaxLength(254)]
        public string AfbRemark { get; set; }
        [MaxLength(20)]
        public string AfbNoSeen { get; set; }
        [MaxLength(20)]
        public string AfbSeen { get; set; }
        public List<Grams> Grams { get; set; }
        [MaxLength(254)]
        public string GramRemarks { get; set; }
        [MaxLength(50)]
        public string Specimen { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        [MaxLength(64)]
        public string TestDesc { get; set; }
        [MaxLength(10)]
        public string ReportStatus { get; set; }

    }

    public class Micro_mbtest01
    {
        [MaxLength(254)]
        public string CultureRemark { get; set; }
        public Cultures Cultures { get; set; }
        [MaxLength(15)]
        public string BacterialCount { get; set; }
        [MaxLength(10)]
        public string ReportStatus { get; set; }
        public List<Sensitivity> Sensitivity { get; set; }
        [MaxLength(254)]
        public string SensitivityRemark { get; set; }
        [MaxLength(50)]
        public string Specimen { get; set; }
        [MaxLength(10)]
        public string TestCode { get; set; }
        [MaxLength(64)]
        public string TestDesc { get; set; }
    }

    public class MicroResults
    {
        [MaxLength(10)]
        public string LabReqNo { get; set; }
        public int Micro_Cnt { get; set; }
        public Micro_mbtest01 micro_Mbtest01 { get; set; }
        public Micro_mbtest02 micro_Mbtest02 { get; set; }
        public Micro_mbtest03 micro_Mbtest03 { get; set; }
        public Micro_mbtest04 micro_Mbtest04 { get; set; }
        [MaxLength(80)]
        public string PatientName { get; set; }
        [MaxLength(20)]
        public string PatientNRIC { get; set; }
        [MaxLength(20)]
        public string VisitNumber { get; set; }
    }
    public class responseMicroResults
    {
        public MicroResults MicroResults { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }
}
