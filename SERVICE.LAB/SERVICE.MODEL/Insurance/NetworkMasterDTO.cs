using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DEV.Model
{
    public class NetworkMasterDTO
    {       
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int InsuranceNetworkNo { get; set; }
        public string PayerName { get; set; }
        public string PayerCode { get; set; }
        public int FollowupDays { get; set; }
        public string ContactPerson { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
    public class NetworkMasterRequest
    {
        public int NetworkNo { get; set; }
        public string PayerName { get; set; }
        public string PayerCode { get; set; }
        public int FollowupDays { get; set; }
        public string ContactPerson { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }
    public class NetworkMasterDTOResponse
    {
        public int result { get; set; }
    }
}

