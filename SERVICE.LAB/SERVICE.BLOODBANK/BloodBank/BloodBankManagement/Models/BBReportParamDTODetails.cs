using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class BBReportParamDTODetails
    {
        public int IdentifyId { get; set; }
        public List<Dictionary<string, object>> Datatable { get; set; }
        public Dictionary<string, string> Paramerter { get; set; }
        public string ReportPath { get; set; }
        public string ExportPath { get; set; }
        public string ExportFormat { get; set; }
        public BBReportParamDTODetails()
        {

        }

        public BBReportParamDTODetails(
            int identifyId,
            List<Dictionary<string, object>> datatable,
            Dictionary<string, string> paramerter,
            string reportPath,
            string exportPath,
            string exportFormat
        )
        {
            IdentifyId = identifyId;
            Datatable = datatable;
            Paramerter = paramerter;
            ReportPath = reportPath;
            ExportPath = exportPath;
            ExportFormat = exportFormat;
        }

        public static ErrorOr<BBReportParamDTODetails> Create(
            int identifyId,
            List<Dictionary<string, object>> datatable,
            Dictionary<string, string> paramerter,
            string reportPath,
            string exportPath,
            string exportFormat
            )
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new BBReportParamDTODetails(identifyId,datatable,paramerter,reportPath,exportPath,exportFormat);
        }

        public static ErrorOr<BBReportParamDTODetails> From(UpsertBbReportParamDtoDetails request)
        {
            return Create(request.IdentifyId, request.Datatable,request.Paramerter,request.ReportPath,request.ExportPath,request.ExportFormat);
        }
    }

}
