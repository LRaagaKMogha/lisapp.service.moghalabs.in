using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DEV.Model
{
    public class DeductionMasterDTO
    {       
        public int DeductionMasterNo { get; set; }
        public string DeductionName { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public List<DeductionDetailsDTO> Deductionlist { get; set; }
        public string Deductiondata { get; set; }
        public int UserNo { get; set; }
    }
 
    public class DeductionDetailsDTO
    {
        public string headerName { get; set; }
        public int deductionHeaderNo { get; set; }
        public int deductionType { get; set; }
        public int deductionLimit { get; set; }
        public int deductionValue { get; set; }
        public string type { get; set; }

    }
    public class DeductionDTOResponse
    {
        public int result { get; set; }
    }

    public class Deductionresult
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int DeductionMasterNo { get; set; }
        public string DeductionName { get; set; }
        public int DeductionHeaderNo { get; set; }
        public string HeaderName { get; set; }
        public int DeductionType { get; set; }
        public decimal DeductionValue { get; set; }
        public decimal DeductionLimit { get; set; }
        public bool Status { get; set; }
    }
    public class DeductionResponse
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int DeductionMasterNo { get; set; }
        public string DeductionName { get; set; }
        public bool Status { get; set; }
        public List<DeductionDetail> Deductionlist { get; set; }
    }
    public class DeductionDetail
    {
        public int DeductionMasterNo { get; set; }
        public int DeductionHeaderNo { get; set; }
        public string DeductionHeaderName { get; set; }
        public int DeductionType { get; set; }
        public decimal DeductionValue { get; set; }
        public decimal DeductionLimit { get; set; }

    }

}
