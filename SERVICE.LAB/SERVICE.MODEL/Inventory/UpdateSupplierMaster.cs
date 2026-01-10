using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Inventory
{
    public class UpdateSupplierMaster
    {
        public int supplierMasterNo { get; set; }
        public string supplierMasterName { get; set; }
        public string drugLicenceNo { get; set; }
        public string tinNo { get; set; }
        public string mobileNo { get; set; }
        public string phoneNo { get; set; }
        public string adress { get; set; }
        public string email { get; set; }
        public Int16 creditDays { get; set; }
        public bool isNonTaxable { get; set; }
        public bool isConsignment { get; set; }
        public Int16 currencyNo { get; set; }
        public Int16 payTermsNo { get; set; }
        public string cStreet { get; set; }
        public string cPlaceName { get; set; }
        public int cCityNo { get; set; }
        public int cStateNo { get; set; }
        public Int16 cCountryNo { get; set; }
        public string cPin { get; set; }
        public string cPhoneNo { get; set; }
        public string cMobileNo { get; set; }
        public string cWhatsappNo { get; set; }
        public string cFax { get; set; }
        public string cEmail { get; set; }
        public string cWeb { get; set; }
        public string bStreet { get; set; }
        public string bPlaceName { get; set; }
        public int bCityNo { get; set; }
        public int bStateNo { get; set; }
        public Int16 bCountryNo { get; set; }
        public string bPin { get; set; }
        public string bPhoneNo { get; set; }
        public string bMobileNo { get; set; }
        public string bWhatsappNo { get; set; }
        public string bFax { get; set; }
        public string bEmail { get; set; }
        public string bWeb { get; set; }
        public string remarks { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
        public int sCountryNo { get; set; }
        public bool sIsNonTaxable { get; set; }
        public bool sIsConsignment { get; set; }
        public int sPayTermsNo { get; set; }
        public int sRegNo { get; set; }
        public int sCreditDays { get; set; }
        public bool sActive { get; set; }
        public string sNotes { get; set; }
        public int manufacturerNo { get; set; }
        public string DrugType { get; set; }
        public string OtherDrug { get; set; }
    }
    public class EditSupplierresponse
    {
        public int supplierMasterNo { get; set; }
        public string supplierMasterName { get; set; }
        public string drugLicenceNo { get; set; }
        public string tinNo { get; set; }
        public string mobileNo { get; set; }
        public string phoneNo { get; set; }
        public string adress { get; set; }
        public string email { get; set; }
        public Int16 creditDays { get; set; }
        public bool isNonTaxable { get; set; }
        public bool isConsignment { get; set; }
        public Int16 currencyNo { get; set; }
        public Int16 payTermsNo { get; set; }
        public string cStreet { get; set; }
        public string cPlaceName { get; set; }
        public int cCityNo { get; set; }
        public int cStateNo { get; set; }
        public Int16 cCountryNo { get; set; }
        public string cPin { get; set; }
        public string cPhoneNo { get; set; }
        public string cMobileNo { get; set; }
        public string cWhatsappNo { get; set; }
        public string cFax { get; set; }
        public string cEmail { get; set; }
        public string cWeb { get; set; }
        public string bStreet { get; set; }
        public string bPlaceName { get; set; }
        public int bCityNo { get; set; }
        public int bStateNo { get; set; }
        public Int16 bCountryNo { get; set; }
        public string bPin { get; set; }
        public string bPhoneNo { get; set; }
        public string bMobileNo { get; set; }
        public string bWhatsappNo { get; set; }
        public string bFax { get; set; }
        public string bEmail { get; set; }
        public string bWeb { get; set; }
        public string remarks { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
        public int sCountryNo { get; set; }
        public bool sIsNonTaxable { get; set; }
        public bool sIsConsignment { get; set; }
        public int sPayTermsNo { get; set; }
        public int sRegNo { get; set; }
        public int sCreditDays { get; set; }
        public bool sActive { get; set; }
        public string sNotes { get; set; }
        public string SupplierContact { get; set; }
        public string SupplierBank { get; set; }
        public List<UpdateSupplierContact> updSupplierContactLst { get; set; }
        public List<UpdateSupplierBank> updSupplierBankLst { get; set; }
        public int manufacturerNo { get; set; }
        public string manufacturerName { get; set; }
        public string DrugType { get; set; }
        public string OtherDrug { get; set; }
    }
    public class EditSupplier
    {
        public int supplierMasterNo { get; set; }
        public string supplierMasterName { get; set; }
        public string drugLicenceNo { get; set; }
        public string tinNo { get; set; }
        public string mobileNo { get; set; }
        public string phoneNo { get; set; }
        public string adress { get; set; }
        public string email { get; set; }
        public Int16 creditDays { get; set; }
        public bool isNonTaxable { get; set; }
        public bool isConsignment { get; set; }
        public Int16 currencyNo { get; set; }
        public Int16 payTermsNo { get; set; }
        public string cStreet { get; set; }
        public string cPlaceName { get; set; }
        public int cCityNo { get; set; }
        public int cStateNo { get; set; }
        public Int16 cCountryNo { get; set; }
        public string cPin { get; set; }
        public string cPhoneNo { get; set; }
        public string cMobileNo { get; set; }
        public string cWhatsappNo { get; set; }
        public string cFax { get; set; }
        public string cEmail { get; set; }
        public string cWeb { get; set; }
        public string bStreet { get; set; }
        public string bPlaceName { get; set; }
        public int bCityNo { get; set; }
        public int bStateNo { get; set; }
        public Int16 bCountryNo { get; set; }
        public string bPin { get; set; }
        public string bPhoneNo { get; set; }
        public string bMobileNo { get; set; }
        public string bWhatsappNo { get; set; }
        public string bFax { get; set; }
        public string bEmail { get; set; }
        public string bWeb { get; set; }
        public string remarks { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
        public int sCountryNo { get; set; }
        public bool sIsNonTaxable { get; set; }
        public bool sIsConsignment { get; set; }
        public int sPayTermsNo { get; set; }
        public int sRegNo { get; set; }
        public int sCreditDays { get; set; }
        public bool sActive { get; set; }
        public string sNotes { get; set; }
        public string SupplierContact { get; set; }
        public string SupplierBank { get; set; }
        public int manufacturerNo { get; set; }
        public string manufacturerName { get; set; }
        public string DrugType { get; set; }
        public string OtherDrug { get; set; }
    }
    public class UpdateSupplierVsBank
    {
        public int supplierMasterNo { get; set; }
        public int supplierBankMasterNo { get; set; }
        public int bankNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accountNo { get; set; }
        public string ifscCode { get; set; }
        public Int16 venueNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
    }
    public class UpdateSupplierVsContact
    {
        public int supplierMasterNo { get; set; }
        public int supplierContactMasterNo { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string mobileNo { get; set; }
        public string whatsAppNo { get; set; }
        public string emailId { get; set; }
        public Int16 venueNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
    }
    public class UpdateSupplierContact
    {
        public int SupplierContactNo { get; set; }
        public int supplierMasterNo { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string mobileNo { get; set; }
        public string whatsAppNo { get; set; }
        public string emailId { get; set; }
        public Int16 venueNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
    }
    public class UpdateSupplierBank
    {
        public int supplierBankMasterNo { get; set; }
        public int supplierMasterNo { get; set; }       
        public int bankNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accountNo { get; set; }
        public string ifscCode { get; set; }
        public Int16 venueNo { get; set; }
        public bool status { get; set; }
        public int userNo { get; set; }
    }

    public class postSupplierMasterDTO
    {
        public UpdateSupplierMaster updSupplierMaster { get; set; }
        public List<UpdateSupplierVsContact> updSupplierVsContactLst { get; set; }
        public List<UpdateSupplierVsBank> updSupplierVsBankLst { get; set; }
        public int userNo { get; set; }
        public Int16 venueNo { get; set; }
        public int venueBranchNo { get; set; }
    }
}
