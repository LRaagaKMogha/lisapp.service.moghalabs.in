using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DEV.Model
{
    public class CompanyMasterDTO
    {       
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int InsuranceCompanyNo { get; set; }
        public string TPAName { get; set; }
        public string ReceiverCode { get; set; }
        public string InsuranceCode { get; set; }
        public string ContactPerson { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string VSTRegNo { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public decimal EM { get; set; }
        public decimal CPT { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TopupInsurance { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
    }
    public class CompanyMasterRequest
    {
        public int InsuranceCompanyNo { get; set; }
        public string TPAName { get; set; }
        public string ReceiverCode { get; set; }
        public string InsuranceCode { get; set; }
        public string ContactPerson { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string VSTRegNo { get; set; }
        public string Address { get; set; }
        public int CountryNo { get; set; }
        public decimal EM { get; set; }
        public decimal CPT { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TopupInsurance { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }
    public class CompanyMasterDTOResponse
    {
        public int result { get; set; }
    }
}

