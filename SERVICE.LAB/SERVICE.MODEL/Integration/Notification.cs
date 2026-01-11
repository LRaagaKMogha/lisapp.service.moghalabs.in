using System.ComponentModel.DataAnnotations;

namespace Service.Model.Integration
{
    public class IntegrationVisitDetails
    {
        public string visitno { get; set; }
        public string clientname { get; set; }
        public string LabOrderId { get; set; }
        public string sourcerequestId { get; set; }
        public string SourceSystem { get; set; }
    }
    public class IntegrationVisitTestDetails
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string OrderListStatusText { get; set; }
        public bool IsOutSource { get; set; }
        public int? PackageNo { get; set; }
        public char ServiceType { get; set; }
    }

}

