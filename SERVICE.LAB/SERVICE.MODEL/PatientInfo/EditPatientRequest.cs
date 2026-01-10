using DEV.Model.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.PatientInfo
{
    public class EditPatientRequest
    {
        public int PatientNo { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int patientAge { get; set; }
        public string ageType { get; set; }
        public int ageYears { get; set; }
        public int ageMonths { get; set; }
        public int ageDays { get; set; }
        public int gender { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string urnType { get; set; }
        public string urnId { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
        public string externalvisitid { get; set; }
        public int nationality { get; set; }
        public string alternateId { get; set; }
        public string alternateIdType { get; set; }
        public int raceNo { get; set; }
        public string pincode { get; set; }
        public string patientOfficeNumber { get; set; }
        public string allergyInfo { get; set; }
        public int bedNo { get; set; }
        public int companyNo { get; set; }
        public int wardNo { get; set; }
        public string caseNumber{get;set;}
        public bool isVipIndication { get; set; }
        public int patientvisitno { get; set; }
        public string wardName { get; set; }
        public string nurnType { get; set; }
        public string nurnId { get; set; }
        public int PhysicianNo { get; set; }
    }

    public class EditPatientResponse
    {
        public string  statusCode { get; set; }
    }
    public class UpdateMasterSyncData
    {
        public int result { get; set; }
    }
    public class PatientmergeResponseDTO
    {
        public int result { get; set; }
    }
    public class PatientmergeDTO
    {
        public int FpatientvisitNo { get; set; }
        public int TpatientvisitNo { get; set; }
        public bool isfirstname { get; set; }
        public bool islastname { get; set; }
        public bool ismiddlename { get; set; }
        public bool isdob { get; set; }
        public bool isgender { get; set; }
        public bool isidnumber { get; set; }
        public bool ismobile { get; set; }
        public bool isemail { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int userno { get; set; }
    }
}