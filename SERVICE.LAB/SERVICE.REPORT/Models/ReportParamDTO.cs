using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.ReportService
{
    public class ReportParamDTO
    {
        public List<Dictionary<string, object>> datatable { get; set; }
        public Dictionary<string, string> paramerter { get; set; }
        public string ReportPath { get; set; }
        public string ExportPath { get; set; }
        public string ExportFormat { get; set; }
    }
}
