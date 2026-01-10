using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetConsumptionMappingResponse
    {
        public int Sno { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int MasterNo { get; set; }
        public int AnalyzerNo { get; set; }
        public string AnalyzerName { get; set; }
        public int ParameterNo { get; set; }
        public string ParameterName { get; set; }
        public int ProductNo { get; set; }
        public string ProductName { get; set; }
        public int UnitNo { get; set; }
        public string UnitName { get; set; }
        public bool Status { get; set; }
    }
    public class GetAllConsumptionListResponse
    {
        public int RowNo { get; set; }
        public Int16 PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int ConsumptionNo { get; set; }
        public string tranId { get; set; }
        public string tranDate { get; set; }
        public string Branch { get; set; }
        public string Store { get; set; }
        public string enteredDate { get; set; }
        public string enteredBy { get; set; }
        public string approvedDate { get; set; }
        public string approvedBy { get; set; }
        public bool Status { get; set; }
        public bool isConsEditable { get; set; }
        public string ConsStatus { get; set; }
        public string statusColorCode { get; set; }
    }
}

