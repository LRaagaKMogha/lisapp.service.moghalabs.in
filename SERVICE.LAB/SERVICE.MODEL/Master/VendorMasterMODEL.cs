using System;
using System.Collections.Generic;

namespace Service.Model
{    
    public partial class requestvendor
    {
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public int vendorno { get; set; }
        public int pageIndex { get; set; }
    }

    public partial class responsevendor
    {
        public int vendorno { get; set; }
        public string vendorName { get; set; }
        public string vendorCode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public string mobileNo { get; set; }
        public string whatsAppNo { get; set; }
        public string place { get; set; }
        public int cityNo { get; set; }
        public int stateNo { get; set; }
        public int countryNo { get; set; }
        public string pinCode { get; set; }
        public string webSite { get; set; }
        public string gstNo { get; set; }
        public int userNo { get; set; }
    }
    public partial class responsegetvendor
    {
        public int vendorno { get; set; }
        public string vendorName { get; set; }
        public string vendorCode { get; set; }
        public int venueno { get; set; }
        public int venuebranchno { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public string mobileNo { get; set; }
        public string whatsAppNo { get; set; }
        public string place { get; set; }
        public int cityNo { get; set; }
        public int stateNo { get; set; }
        public int countryNo { get; set; }
        public string pinCode { get; set; }
        public string webSite { get; set; }
        public string gstNo { get; set; }
        public int userNo { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }
    }
    public class StoreVendorMaster
    {
        public int vendorno { get; set; }
    }
    //-----tab 2 CONTACT-------//
    public class getcontact
    {
        public int vendorContactNo { get; set; }
        public Int16 venueno { get; set; }
        public int vendorMasterNo { get; set; }
    }
    public class savecontact
    {
        public Int16 venueno { get; set; }
        public int vendorMasterNo { get; set; }
        public int userNo { get; set; }
        public List<getcontactlst> getcontactlst { get; set; }

    }
    public class getcontactlst
    {
        public int vendorContactNo { get; set; }
        public int vendorMasterNo { get; set; }
        public string cname { get; set; }
        public string cdesignation { get; set; }
        public string cmobileNo { get; set; }
        public string cwhatsAppNo { get; set; }
        public string cemailId { get; set; }
    }
    public class StorecontactMaster
    {
        public int VendorContactNo { get; set; }
    }
    //tab 3 SERVICE//
    public class getservice
    {
        //public int VendorServiceNo { get; set; }
        public Int16 VenueNo { get; set; }
        public int VendorMasterNo { get; set; }
        public int ServiceNo { get; set; }        
        public int pageindex { get; set; }
    }
    public class saveservice
    {
        public Int16 venueno { get; set; }
        public int VendorMasterNo { get; set; }
        public int userNo { get; set; }
        public List<getservicelst> getservicelst { get; set; }
    }
    public class getservicelst
    {
        public int vendorServiceNo { get; set; }
        public int vendorMasterNo { get; set; }
        public int serviceType { get; set; }
        public int serviceNo { get; set; }
        public Decimal amount { get; set; }
        public string serviceCode { get; set; }
        public string processingDays { get; set; }
        public Int16 cutoffhour { get; set; }
        public Int16 cutoffmin { get; set; }
        public Byte reportDays { get; set; }
        public string serviceName { get; set; }
        public bool status { get; set; }
        public int totalRecords { get; set; }
        public int pageSize { get; set; }
    }
    public class storeservice
    {
        public int VendorServiceNo { get; set; }

    }





}
