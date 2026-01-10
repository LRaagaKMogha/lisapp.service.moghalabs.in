using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class TblTax
    {
        public int taxNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string taxName{ get; set; }       
        public byte  taxPercentage{ get; set; }
        public Int16 sequenceNo { get; set; }
        public bool status { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int CurrentseqNo { get; set; }
    }

    public class TaxMasterRequest
    {
        public int taxNo { get; set; }
        public Int16 venueNo { get; set; }
        public int pageIndex { get; set; }
    }
    public class TaxMasterResponse
    {
        public int taxNo { get; set; }      
    }
    public partial class TblHSN
    {
        public int taxNo { get; set; }
        public int HSNNo { get; set; }
        public string HSNCode { get; set; }
        public string Description { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string taxName { get; set; }
        public bool status { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }
    public class HSNMasterRequest
    {
        public int taxNo { get; set; }
        public Int16 venueNo { get; set; }
        public int HSNNo { get; set; }
        public int venueBranchno { get; set; }
        public int pageIndex { get; set; }
    }
    public class HSNMasterResponse
    {
        public int HSNNo { get; set; }
    }
    public partial class TblHSNRange
    {
        public int taxNo { get; set; }
        public int HSNNo { get; set; }
        public int HSNRangeNo { get; set; }
        public string HSNCode { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public string Description { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string taxName { get; set; }
        public bool status { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }
    public partial class TblInsertHSNRange
    {
        public int taxNo { get; set; }
        public int HSNNo { get; set; }
        public int HSNRangeNo { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public bool status { get; set; }
    }
    public class HSNRangeRequest
    {
        public int taxNo { get; set; }
        public Int16 venueNo { get; set; }
        public int HSNRangeNo { get; set; }
        public int HSNNo { get; set; }
        public int venueBranchno { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public int pageIndex { get; set; }
    }
    public class HSNInsertResponse
    {
        public int HSNRangeNo { get; set; }
    }
}
