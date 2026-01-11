using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class QueueOrderDTO
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 RowNum { get; set; }
        public string QueueOrderID { get; set; }
        public string Title { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public string AgeType { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string UIDType { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string localbodies { get; set; }
        public string UIDNo { get; set; }
        public string ServiceNames { get; set; }
        public string ServiceNos { get; set; }
        public string ServiceTypes { get; set; }
        public string VisitID { get; set; }
        public int IsQueue { get; set; }
        public int PatientVisitNo { get; set; }
        public string OrderDTTM { get; set; }
    }
}


