using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class DeleteVisitRequest
    {
        public string? VisitId { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }      
        public int userNo { get; set; }
    }

    public class UpdateCustomerDetails
    {
        public string? VisitId { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int refferalTypeNo { get; set; }
        public int refferalNo { get; set; }
        public int physicianNo { get; set; }
        public int userNo { get; set; }
    }
    public class UpdateOrderDatesRequest
    {
        public string? VisitId { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int userNo { get; set; }
        public string? RegistrationDT { get; set; }
        public string? CollectionDT { get; set; }
        public string? ApprovedDT { get; set; }
    }
    public class PaymentMode
    {
        public int? Id { get; set; }
        public string? ModeOfPayment { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? ModeOfType { get; set; }
        public int? VisitNo { get; set; }

    }
    public class SavePaymentModeRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PatientVisitNo { get; set; }
        public int? UserID { get; set; }
        public List<PaymentMode>? lstPaymentMode { get; set; }
    }
    public class SavePaymentModeResponse
    {
        public int Response { get; set; }
    }
    public class GetPaymentModeRequest
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string? VisitId { get; set; }
    }
}


