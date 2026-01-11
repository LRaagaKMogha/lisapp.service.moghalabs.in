using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class ReportParamDto
    {
        public List<Dictionary<string, object>>? datatable { get; set; }
        public Dictionary<string, string>? paramerter { get; set; }
        public string? ReportPath { get; set; }
        public string? ExportPath { get; set; }
        public string? ExportFormat { get; set; }
    }
}
