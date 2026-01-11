using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class ServiceSearchDTO
    {
        public int Rowno { get; set; }
        public int TestNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int DeptNo { get; set; }
        public string TestType { get; set; }
        public string ShortCode { get; set; }
        public string Gender { get; set; }
        public bool IsChoice { get; set; }
        public int ChoiceCount { get; set; }

    }
    public class ServiceParamDTO
    {    
        public int TestNo { get; set; }
        public string TestType { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }

    public class OptionalTestDTO
    {
        public int TestNo { get; set; }
        public string TestType { get; set; }
        public bool IsOptional { get; set; }
        public bool IsSelected { get; set; }
        public string ServiceCode { get; set; }
    }
    public class GroupTestDTO
    {
        public int TestNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public bool IsOptional { get; set; }
        public bool IsVisible { get; set; }
        public string ServiceCode { get; set; }
    }
    public class ServiceRateList
    {
        public int ServiceNo { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int RateListNo { get; set; }
        public decimal Amount { get; set; }
        public string ClientServiceCode { get; set; }
        public bool IsDiscountable { get; set; }
        public bool IsAmountEditable { get; set; }
        public int ProcessingMinutes { get; set; }
        public bool IsIncludeInstruction { get; set; }
        public string IncludeInstruction { get; set; }
        public bool IsChoice { get; set; }
        public int ChoiceCount { get; set; }
        public decimal baseamount { get; set; }
    }

    public class ClientRestrictionDay
    {
        public int ClientNumber { get; set; }
        public int RestrictionDays { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }

    }
    public class ClientRestrictionDayResponse
    {
        public int AvailVisitNo { get; set; }
    }
    public class TestPrePrintDetailsRequest
    {
        public int ServiceNo { get; set; }
        public char ServiceType { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class TestPrePrintDetailsResponse
    {
        public int ID { get; set; }
        public int TestNo { get; set; }
        public string TestName { get; set; }
        public int DeptNo { get; set; }
        public string DeptName { get; set; }
        public int MethodNo { get; set; }
        public string MethodName { get; set; }
        public int SampleNo { get; set; }
        public string SampleName { get; set; }
        public int ContainerNo { get; set; }
        public string ContainerName { get; set; }
        public int UnitsNo { get; set; }
        public string UnitsName { get; set; }
        public int SeqNo { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string ResultType { get; set; }  
        public bool IsMultiSampleSelect { get; set; }
        public List<MultiSampleList> lstMultiSampleList { get; set; }
    }    
    public class GetDetailsByPincode
    {

        public Int64 Row_Num { get; set; }
        public int CityNo { get; set; }
        public string CityName { get; set; }
        public int StateNo { get; set; }
        public string StateName { get; set; }
        public int CountryNo { get; set; }
        public string CountryName { get; set; }
        public string PlaceName { get; set; }
    }
    public class Humanbodyparts
    {
        public int HumanBodyParts_Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
    }
    public class displaylist
    {
        public Int64 Row_Num { get; set; }
        public Int64 Sno { get; set; }
        public string PatientName { get; set; }
        public string PhysicianName { get; set; }
        public string Appointmenttime { get; set; }
        public string RoomNo { get; set; }
        public string AppointmentNo { get; set; }
    }
}
