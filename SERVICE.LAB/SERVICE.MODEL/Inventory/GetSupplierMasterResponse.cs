using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetSupplierMasterResponse
    {
        public int supplierMasterNo { get; set; }
        public string supplierMasterName { get; set; }
        public string drugLicenceNo { get; set; }
        public string tinNo { get; set; }
        public string mobileNo { get; set; }
        public string phoneNo { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public bool isConsignment { get; set; }
        public bool isNonTaxable { get; set; }
        public Int16 creditDays { get; set; }
        public Int16 currencyNo { get; set; }
        public Int16 payTermsNo { get; set; }
        public string remarks { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
        public int manufacturerNo { get; set; }
        public string manufacturerName { get; set; }
        public int CCountryNo { get; set; }
        public int CStateNo { get; set; }
        public int CCityNo { get; set; }
        public string CPlaceName { get; set; }
        public int BCountryNo { get; set; }
        public int BStateNo { get; set; }
        public int BCityNo { get; set; }
        public string BPlaceName { get; set; }
        public int sCountryNo { get; set; }
        public int sRegNo { get; set; }
        public int sPayTermsNo { get; set; }
        public int sCreditDays { get; set; }
        public bool sActive { get; set; }
        public string sNotes { get; set; }
        public string CStreet{ get; set; }
        public string CPin { get; set; }
        public string CPhoneNo { get; set; }
        public string CMobileNo { get; set; }
        public string CWhatsappNo { get; set; }
        public string CFax { get; set; }
        public string CWeb { get; set; }
        public string CEmail { get; set; }
        public string BStreet { get; set; }
        public string BPin { get; set; }
        public string BPhoneNo { get; set; }
        public string BMobileNo { get; set; }
        public string BWhatsappNo { get; set; }
        public string BFax { get; set; }
        public string BEmail { get; set; }
        public string BWeb { get; set; }
        public bool sIsConsignment { get;set;}
        //public bool sIsNonTaxable { get;set; }
        public string ISOcode { get; set; }
        public string IsdCode { get; set; }
        public string ISOcodee { get; set; }
        public string IsdCodee { get; set; }
        public string TermDescription { get; set; }
        public string DrugType { get; set; }
        public string OtherDrug { get; set; }




    }
}
