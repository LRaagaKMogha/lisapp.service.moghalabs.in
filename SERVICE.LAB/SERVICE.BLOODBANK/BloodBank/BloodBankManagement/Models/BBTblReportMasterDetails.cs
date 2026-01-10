using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using ErrorOr;

namespace BloodBankManagement.Models
{
    public class BBTblReportMasterDetails
    {
        public int ReportNo { get; set; }
        public string ReportKey { get; set; }
        public string ReportName { get; set; }        
        public string Description { get; set; }      
        public string ProcedureName { get; set; }
        public string ReportPath { get; set; }      
        public string ExportPath { get; set; }
        public string ExportURL { get; set; }
        public string Parameterstring { get; set; }
        public int? VenueNo { get; set; }
        public int? VenueBranchNo { get; set; }
        public bool? Status { get; set; }         
        public BBTblReportMasterDetails()
        {

        }

        public BBTblReportMasterDetails(
            int reportNo,
            string reportKey,
            string reportName,
            string description,
            string procedureName,
            string reportPath,
            string exportPath,
            string exportURL,
            string parameterstring,
            int venueNo,
            int venueBranchNo,
            bool status
        )
        {
            ReportNo = reportNo;
            ReportKey = reportKey;
            ReportName = reportName;
            Description = description;
            ProcedureName = procedureName;
            ReportPath = reportPath;
            ExportPath = exportPath;
            ExportURL = exportURL;
            Parameterstring = parameterstring;
            VenueNo = venueNo;
            VenueBranchNo = venueBranchNo;
            Status = status;
        }

        public static ErrorOr<BBTblReportMasterDetails> Create(
            int reportNo,
            string reportKey,
            string reportName,
            string description,
            string procedureName,
            string reportPath,
            string exportPath, 
            string exportURL,
            string parameterstring,
            int venueNo,
            int venueBranchNo,
            bool status
            )
        {
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }
            return new BBTblReportMasterDetails(reportNo, reportKey, reportName, description, procedureName, reportPath, exportPath, exportURL, parameterstring,venueNo,venueBranchNo,status);
        }

        public static ErrorOr<BBTblReportMasterDetails> From(UpsertBBTblReportMasterDetails request)
        {            
            return Create(request.ReportNo,request.ReportKey,request.ReportName,request.Description,request.ProcedureName,request.ReportPath,request.ExportPath,request.ExportURL,request.Parameterstring,request.VenueNo,request.VenueBranchNo,request.Status);
        }
    }

}
