using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class PatientDrugList
    {
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_num { get; set; }
        public Int64 Sno { get; set; }
        public string? PatientID { get; set; }
        public int PatientNo { get; set; }
        public string? Age { get; set; }
        public string? PatientName { get; set; }
        public int PatientAge { get; set; }
        public int DrugStatus { get; set; }
        public string? VenueBranchName { get; set; }
        public int DiagnosisNo { get; set; }
        public string? CreateDateTime { get; set; }
        public int PrescriptionBillNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? Specialization { get; set; }
        public string? MobileNumber { get; set; }
        public int PatientVisitNo { get; set; }
        public string? VisitID { get; set; }
    }
    public partial class PatientList
    {
        public Int64 Row_num { get; set; }
        public string? PatientID { get; set; }
        public int PatientNo { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public int Age { get; set; }
        public string? AgeType { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Address { get; set; }
        public string? AreaName { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string? Pincode { get; set; }
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? ProductMasterName { get; set; }
        public int ProductMasterNo { get; set; }
        public string? Instructions { get; set; }
        public string? BatchNo { get; set; }
        public string? ExpireDate { get; set; }
        public decimal? Rate { get; set; }
        public int Qty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NetAmount { get; set; }
        public int DrugStatus { get; set; }
        public string? VenueBranchName { get; set; }
        public int PhysicianDiagnosisNo { get; set; }
        public string? CreateDateTime { get; set; }
    }
    public partial class PatientDrugDetailList
    {
        public Int64 Row_num { get; set; }
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? ProductMasterName { get; set; }
        public int ProductMasterNo { get; set; }
        public string? Instructions { get; set; }
        public string? BatchNo { get; set; }
        public string? ExpireDate { get; set; }
        public decimal? Rate { get; set; }
        public int Qty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? CollectedAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public int DrugStatus { get; set; }
        public string? VenueBranchName { get; set; }
        public int PhysicianDiagnosisNo { get; set; }
        public string? CreateDateTime { get; set; }
        public int PrescriptionBillNo { get; set; }
        public string? Specialization { get; set; }
        public string? MobileNumber { get; set; }
        public int PatientVisitNo { get; set; }
        public string? VisitID { get; set; }
    }
    public partial class PatientDrugDetailListReq
    {
        public int PatientNo { get; set; }
        public int VisitNo { get; set; }
        public List<PatientDrugDetailList> lstDruglist { get; set; }
        public List<PatientPrescriptionPayment> lstPaymentlist { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public string? Type { get; set; }
    }
    public class PatientDrugDetailRes
    {
        public int PatientPrescriptionBillNo { get; set; }
    }
    public partial class PrintPatientPrescription
    {
        public Int64 Row_num { get; set; }
        public string? PatientID { get; set; }
        public int PatientNo { get; set; }
        public string? TitleCode { get; set; }
        public string? FirstName { get; set; }
        public int Age { get; set; }
        public string? AgeType { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Address { get; set; }
        public int PhysicianNo { get; set; }
        public string? PhysicianName { get; set; }
        public string? ProductMasterName { get; set; }
        public int ProductMasterNo { get; set; }
        public string? Instructions { get; set; }
        public string? BatchNo { get; set; }
        public string? ExpireDate { get; set; }
        public decimal? Rate { get; set; }
        public int Qty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NetAmount { get; set; }
        public int DrugStatus { get; set; }
        public string? VenueBranchName { get; set; }
        public int PhysicianDiagnosisNo { get; set; }
        public string? CreateDateTime { get; set; }
        public string? PrescriptionID { get; set; }
    }
    public class PatientPrescriptionPayment
    {
        public string? ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? ModeOfType { get; set; }
    }
}