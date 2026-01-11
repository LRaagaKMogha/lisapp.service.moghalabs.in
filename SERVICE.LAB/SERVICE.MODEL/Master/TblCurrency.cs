using System;
using System.Collections.Generic;

namespace Service.Model
{
    public partial class TblCurrency
    {
        public int CurrencyNo { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }
        public string font { get; set; }
        public int? VenueNo { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool isdefault { get; set; }
    }
}
