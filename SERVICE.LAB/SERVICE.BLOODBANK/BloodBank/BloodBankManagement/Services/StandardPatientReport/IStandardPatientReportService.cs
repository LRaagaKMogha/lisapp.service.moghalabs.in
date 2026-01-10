using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.StandardPatinetReport
{
    public interface IStandardPatientReportService
    {
        Task<ErrorOr<List<Models.StandardPatinetReport>>> GetStandardPatientReport(Contracts.FetchPatientRequest request);    
        Task<ErrorOr<Contracts.UpdateStandardPatientResponse>> UpdateStandardPatientReport(UpdateStandardPatinetReport request);
        Task<ErrorOr<GetPatientPrintReport>> GetStandardPatientReportPrint(Contracts.FetchPatientReportRequest request);
    }
}