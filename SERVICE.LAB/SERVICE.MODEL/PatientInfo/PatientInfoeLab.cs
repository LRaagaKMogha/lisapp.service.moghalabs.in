using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.PatientInfo
{
    public class PatientInfoeLab
    {
    }

    public class PatientInfoRequestDTO
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientNo { get; set; }
        public int pageIndex { get; set; }
        public int userNo { get; set; }
        public string PageCode { get; set; }
        public int visitNo { get; set; }
        public int physicianNo { get; set; }
        public int departmentNo { get; set; }
        public Int16 maindeptNo { get; set; }
        public int serviceNo { get; set; }
        public string serviceType { get; set; }
        public int orderStatus { get; set; }
        public bool isSTATFilter { get; set; }
        public string MultiFieldsSearch { get; set; }
        public int FilterBranch { get; set; }
    }
    public class PatientInfoeLabResponseDTO
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public int PatientNo { get; set; }
        public string RHNo { get; set; }
        public string patientID { get; set; }
        public string PrimaryId { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int VisitNo { get; set; }
        public string VisitId { get; set; }
        public string RegistrationDate { get; set; }
        public string PhysicianName { get; set; }
        public string TestName { get; set; }
        public string OrderStatus { get; set; }
        public string colorcode { get; set; }
        public int PatientEdit { get; set; }
        public bool IsVipIndication { get; set; }
        public bool IsVIP { get; set; }
        public string GenderCode { get; set; }
        public string BranchName { get; set; }
        public bool isStat { get; set; }
        public bool isPatientEdit { get; set; }
        public Int16 Quantity { get; set; }
        public string URNID { get; set; }
    }
}
