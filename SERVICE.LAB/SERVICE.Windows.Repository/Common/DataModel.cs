using System.Collections.Generic;

namespace Service.Win.Repository
{
    public class ArchiveTransactionDTO
    {
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string PatientTransaction { get; set; }
        public string PatientTransactionStatus { get; set; }
        public string PatientBill { get; set; }
        public string PatientBillCancel { get; set; }
        public string PatientBillDetails { get; set; }
        public string PatientBillDues { get; set; }
        public string PatientBillPayments { get; set; }
        public string PatientBillTransaction { get; set; }
        public string PatientDiscount { get; set; }
        public string Orders { get; set; }
        public string OrderDetails { get; set; }
        public string OrderTransaction { get; set; }
        public string OrderTransactionStatus { get; set; }
        public string PatientResult { get; set; }
        public string PatientFinalResult { get; set; }
        public string PatientResultMB { get; set; }
        public string PatientResultMBDrug { get; set; }
        public string PatientResultTemplate { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool IsTemplate { get; set; }
        public string TemplateTextResult { get; set; }
    }
    public class ArchiveTemplateDTO
    {
        public string OrderListNo { get; set; }
        public string TestNo { get; set; }        
        public string Results { get; set; }
    }
    public class PostGoogleURL
    {
        public string longDynamicLink { get; set; }
        public Suffix suffix { get; set; }
    }
    public class Suffix
    {
        public string option { get; set; }
    }
    public class GoogleResponse
    {
        public string shortLink { get; set; }
        public string previewLink { get; set; }
    }
    public class WMSPOSTRequest
    {
        public string countryCode { get; set; }
        public string phoneNumber { get; set; }
        public string callbackData { get; set; }
        public string type { get; set; }
        public WMSPOSTTemplate template { get; set; }
    }
    public class WMSPOSTTemplate
    {
        public string name { get; set; }
        public string languageCode { get; set; }
        public List<string> headerValues { get; set; }
        public string fileName { get; set; }
        public List<string> bodyValues { get; set; }
    }
    public class WMSPOSTResponse
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }
    public class WMSPostRequst_14
    {
        public string apiKey { get; set; }
        public string campaignName { get; set; }
        public string destination { get; set; }
        public string userName { get; set; }
        public string source { get; set; }
        public Media media { get; set; }
        public List<string> templateParams { get; set; }
        public List<string> tags { get; set; }
        public Attributes attributes { get; set; }
    }
    public class Attributes
    {
        public string customerType { get; set; }
        public string location { get; set; }
    }
    public class Media
    {
        public string url { get; set; }
        public string filename { get; set; }
    }
}