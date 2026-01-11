using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Model.Integration
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public string StatusCode { get; set;  }
        public T Data { get; set; }
    }
        

    public class UpsertBloodBankRegistrationRequest
    {
        public string NRICNumber { get; set; }
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public string CaseOrVisitNumber { get; set; }
        public Int64 NationalityId { get; set; }
        public Int64 GenderId { get; set; }
        public Int64 RaceId { get; set; }
        public Int64 ResidenceStatusId { get; set; }
        public Int64? ClinicalDiagnosisId { get; set; }
        public Int64 IndicationOfTransfusionId { get; set; }
        public string ClinicalDiagnosisOthers { get; set; }
        public string IndicationOfTransfusionOthers { get; set; }
        public string DoctorOthers { get; set; }
        public string DoctorMCROthers { get; set; }
        public bool IsEmergency { get; set; }
        public Int64? WardId { get; set; }
        public Int64? ClinicId { get; set; }
        public Int64? DoctorId { get; set; }
        public List<PatientRegisteredProducts> Products { get; set; }
        public decimal ProductTotal { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public Int64? NurseId { get; set; }
        public string IssuingComments { get; set; }
        public string LabAccessionNumber { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public Int64? Identifier { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }



    public class PatientRegisteredProducts
    {
        public Int64 Identifier { get; set; }
        public Int64 ProductId { get; set; }
        public decimal MRP { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
    }
}
