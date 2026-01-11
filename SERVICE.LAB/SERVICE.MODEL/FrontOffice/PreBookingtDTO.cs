using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public  class PreBookingtDTO
    {
        public int PageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 Row_Num { get; set; }
        public Int64 Sno { get; set; }
        public int PreBookingQueueNo { get; set; }
        public string PreBookingQueueID { get; set; }
        public int PatientNo { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public int patientAge { get; set; }
        public string AgeType { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmenStarttime { get; set; }
        public string AppointmenEndDateTime { get; set; }
        public string BookingStatusText { get; set; }
        public int BookingStatus { get; set; }
        public int BookingType { get; set; }
        public int ResourceTypeNo { get; set; }
        public string ResourceText { get; set; }
        public int Id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
    }

    public  class PreBookingtResponse
    {
        public int PreBookingQueueNo { get; set; }
    }

    public  class PreBookingtRequest
    {
        public int PreBookingQueueNo { get; set; }
        public Int64 PatientNo { get; set; }
        public string TitleCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string EmailID { get; set; }
        public string SecondaryEmailID { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public int StateNo { get; set; }
        public int CityNo { get; set; }
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public int BookingType { get; set; }
        public int ResourceTypeNo { get; set; }
        public string StartDatetime { get; set; }
        public string EndDateTime { get; set; }
        public int BookingStatus { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
  
    }
}