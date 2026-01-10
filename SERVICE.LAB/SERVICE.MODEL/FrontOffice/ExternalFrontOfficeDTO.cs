using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class ExternalFrontOfficeDTO
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string ageType { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string mobileNumber { get; set; }
        public string emailID { get; set; }
        public string urnid { get; set; }
        public string urnType { get; set; }
        public string refferralType { get; set; }
        public int customerNo { get; set; }
        public int physicianNo { get; set; }
        public string clinicalHistory { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public string externalpatientID { get; set; }
        public string externalOrderID { get; set; }
        public List<externalOrder> orders { get; set; }
        public List<externalPayment> payments { get; set; }
    }
    public class externalOrder
    {
        public int testNo { get; set; }
        public int quantity { get; set; }
    }
    public class externalPayment
    {
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ModeOfType { get; set; }
    }
    public class externalFrontOfficeResponse
    {
        public bool success { get; set; }
        public externalResponse response { get; set; }
        public List<externalError> errors { get; set; }
    }
    public class externalResponse
    {
        public object data { get; set; }
        public string message { get; set; }
    }
    public class externalError
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
