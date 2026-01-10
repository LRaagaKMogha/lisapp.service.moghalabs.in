using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
   public class InsertClientMasterRequest
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobileNo { get; set; }
        public string Password { get; set; }
        public DateTime? ActiveDate { get; set; }
        public decimal? CreditLimit { get; set; }
        public int? CreditPeriod { get; set; }
        public string CustomerType { get; set; }
        public string Idtype { get; set; }
        public string Id { get; set; }
        public string Gstno { get; set; }
        public bool? AllowBilling { get; set; }
        public bool IsReportSms { get; set; }
        public bool IsReportEmail { get; set; }
        public bool? IsInterNotes { get; set; }
        public int VenueBranchNo { get; set; }
        public int VenueNo { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ContactPersonName { get; set; }
        public int ClientPayType { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public int City { get; set; }
        public string Pincode { get; set; }
        public string ClientUsername { get; set; }
        public bool? CpBilling { get; set; }
        public bool? CpReportView { get; set; }
        public bool? CpBillView { get; set; }
        public bool? CpBlock { get; set; }
        public bool? BillingBlock { get; set; }
        public bool? ReportDispatchBlock { get; set; }
        public bool? ClientBlock { get; set; }
        public bool? Active { get; set; }


        //public int CustomerNo { get; set; }
        //public string CustomerName { get; set; }
        //public string CustomerEmail { get; set; }
        //public string CustomerMobileNo { get; set; }
        //public decimal? CreditLimit { get; set; }
        //public string CustomerType { get; set; }
        //public string CustomerTypeValue { get; set; }
        //public string ContactPersonName { get; set; }
        //public int ClientPayType { get; set; }
        //public string ClientPayTypeValue { get; set; }
        //public string Address { get; set; }
        //public string Area { get; set; }
        //public int City { get; set; }
        //public string CityName { get; set; }
        //public string Pincode { get; set; }
        //public string ClientUsername { get; set; }
        //public bool? CpBilling { get; set; }
        //public bool? CpReportView { get; set; }
        //public bool? CpBillView { get; set; }
        //public bool? CpBlock { get; set; }
        //public bool? BillingBlock { get; set; }
        //public bool? ReportDispatchBlock { get; set; }
        //public bool? ClientBlock { get; set; }
        //public bool? Active { get; set; }

        ////public string CpBilling { get; set; }
        //public string CpReportView { get; set; }
        //public string CpBillView { get; set; }
        //public string CpBlock { get; set; }
        //public string BillingBlock { get; set; }
        //public string ReportDispatchBlock { get; set; }
        //public string ClientBlock { get; set; }
        //public string Active { get; set; }
    }
}
