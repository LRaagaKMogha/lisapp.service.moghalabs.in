using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.FrontOffice.PatientDue
{
   public class CreatePatientDueRequest
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userID { get; set; }
        public int visitNo { get; set; }
        public List<PaymentDetails> payments { get; set; }
        public decimal discountAmount { get; set; }
        public decimal newDiscountAmount { get; set; }
        public string discountDescription { get; set; }        
        public decimal paidAmount { get; set; }
        public decimal balanceAmount { get; set; }
        public decimal discountNo { get; set; }
        public decimal dueAmount { get; set; }
        public Int16 isDiscountApprovalReq { get; set; }
    }
    public class PaymentDetails
    {
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ModeofType { get; set; }
        public int CurrencyNo { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? CurrencyAmount { get; set; }
    }
}

