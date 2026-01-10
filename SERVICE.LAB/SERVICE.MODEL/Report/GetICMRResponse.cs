using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetICMRResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 RowNum { get; set; }
        public string PatientId { get; set; }
        public int PatientVisitNo { get; set; }
        public string VisitID { get; set; }
        public string Name { get; set; }
        public string AgeGender { get; set; }
        public string Phone { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string Response { get; set; }
        public string ApprovalTime { get; set; }
        public string UploadedTime { get; set; }
    }
    public class BarcodeResult {
        public bool result { get; set; }
    }
}


