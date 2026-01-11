using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetSampleOutsourceRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public int DepartmentNo { get; set; }
        public int ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public int pageIndex { get; set; }
        public int VisitNo { get; set; }
        public int VendorNo { get; set; }
        public int PatientNo { get; set; }
        public int pageCount { get; set; }
        public int PhysicianNo { get; set; }
    }
    public class GetSampleOutSourceHistoryRequest
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int pageIndex { get; set; }
        public int VendorNo { get; set; }
    }
}


