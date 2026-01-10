using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model.Sample
{
    public class CreateManageOptionalTestRequest
    {
        public string PackageCode { get; set; }
        public string TestCodes { get; set; }  
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool IsEditBill { get; set; }
    }
    public class CreateManageSampleRequest
    {
        public int visitNo { get; set; }
        public int collectedBy { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int userNo { get; set; }
        public int sampleNo { get; set; }
        public int oldSampleNo { get; set; }
        public int containerNo { get; set; }
        public int oldContainerNo { get; set; }
        public int ordersNo { get; set; }
        public int orderListNo { get; set; }
        public string collectedDateTime { get; set; }
        public string barcodeno { get; set; }
        public bool ispreprinted { get; set; }
        public string pagecode { get; set; }
        public int testNo { get; set; }
        public bool isnotgiven { get; set; }
        public bool ishigtemprature { get; set; }
        public bool isbarcodenotreq { get; set; }
        public string higTempValue { get; set; }
        public bool collectatsource { get; set; }
        public int specimenQty { get; set; }
        public string sampleSource { get; set; }
        public int fastingOrNonfasting { get; set; }
        public string sampleSourceDesc { get; set; }
        public bool isSampleChanged { get; set; }
        public int serviceNo { get; set; }
        public string rejectedReason { get; set; }
        public string? rejectedReasonDesc { get; set; }
    }
    public class SampleTestDetails
    {
        public int visitNo { get; set; }
        public int collectedBy { get; set; }
        public int venueBranchNo { get; set; }
        public int venueNo { get; set; }
        public int sampleNo { get; set; }
        public int containerNo { get; set; }
        public int ordersNo { get; set; }
        public int orderListNo { get; set; }
        public string collectedDateTime { get; set; }
        public int testNo { get; }

    }
}