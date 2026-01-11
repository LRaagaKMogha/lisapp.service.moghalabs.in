using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.External.CommonReports
{
   
    public partial class LstPaidInformation
    {
        public string? visitId { get; set; }
        public string? visitDtTm { get; set; }
        public string? billReceiptNo { get; set; }
        public string? billDtTm { get; set; }
        public string? billedBy { get; set; }
        public string? patientName { get; set; }
        public int ageInYears { get; set; }
        public int ageInMonths { get; set; }
        public int ageInDays { get; set; }
        public string? gender { get; set; }
        public string? referralBy { get; set; }
        public decimal grossAmount { get; set; }
        public decimal discountAmount { get; set; }
        public decimal netAmount { get; set; }
        public decimal collectedAmount { get; set; }
        public string? payType { get; set; }
    }
}
