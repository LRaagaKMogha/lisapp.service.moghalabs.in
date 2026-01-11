using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class OutSourceAPIDTORequest
    {        
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }    
    public class OutSourceAPIDTOResponse
    {
        public Int64 RowNo { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmirateID { get; set; }
        public string Nationality { get; set; }
        public string AdmissionType { get; set; }
        public string PointOfCare { get; set; }
        public string RequestNo { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string Prefix { get; set; }
        public string Diagnosis { get; set; }        
        public string VisitID { get; set; }
        public string InsuranceCode { get; set; }
        public string InsuranceStatus { get; set; }
        public string InsuranceName { get; set; }
        public string PayerName { get; set; }
        public string PayerCode { get; set; }
        public string PlanEffectiveDate { get; set; }
        public string PlanExpiryDate { get; set; }
        public string CardNo { get; set; }
        public string Network { get; set; }
        public string OrderControl { get; set; }
        public string BarcodeNo { get; set; }
        public DateTime CollectionDateandtime { get; set; }
        public string Priority { get; set; }
        public DateTime TransactionDateandtime { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderCreatedDateandtime { get; set; }
        public string SetID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string CPTCode { get; set; }
        public decimal TestPrice { get; set; }
        public string SpecimenDetails { get; set; }
        public int OutsourceNo { get; set; }
        public int OutsourceDetailsNo { get; set; }
        public Int64 APIOutsourceSendNo { get; set; }                
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }

    public class AckOutSourceAPIDTORequest
    {
        public Int64 APIOutsourceSendNo { get; set; }
        public Int16 Ackstatus { get; set; }
    }
    public class AckOutSourceAPIDTOResponse
    {
        public int OutStatus { get; set; }        
    }
}

