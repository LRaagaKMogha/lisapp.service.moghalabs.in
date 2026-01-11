using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Service.Model
{
    public class CommericalGetReq
    {
        public int CompanyNo { get; set; }
        public Int16? venueNo { get; set; }
        public int? pageIndex { get; set; }
    }
    public partial class CommericalGetRes
    {
        public int CompanyNo { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailID { get; set; }
        public string? MobileNo { get; set; }
        public int seqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int CurrentseqNo { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }

    }
    public partial class CommericalInsReq
    {
        public int CompanyNo { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailID { get; set; }
        public string? MobileNo { get; set; }
        public int SeqNo { get; set; }
        public bool? Status { get; set; }
        public Int16 VenueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }

    }

    public class CommericalInsRes
    {
        public int CompanyNo { get; set; }
    }
    public class GSTGetReq
    {
        public int TaxMastNo { get; set; }
        public int VenueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public partial class GSTGetRes
    {
        public Int16 TaxMastNo { get; set; }
        public bool? Status { get; set; }
        public string Description { get; set; }
        public decimal Percentage { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromDateTime { get; set; }
        public string ToDateTime { get; set; }
        public string ExpiredDate { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }

    }
    public partial class GSTInsReq
    {
        public Int16 TaxMastNo { get; set; }
        public string Description { get; set; }
        public Decimal Percentage { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public byte VenueNo { get; set; }
        public bool? Status { get; set; }
        public int userNo { get; set; }

    }

    public class GSTInsRes
    {
        public Int16 TaxMastNo { get; set; }
    }


}
