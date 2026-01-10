using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Models;
using ErrorOr;

namespace BloodBankManagement.Services.Reports
{
    public interface IBBPatientReport
    {
        Task<ErrorOr<List<BBReportOutputDetails>>> PrintReport(Contracts.BBPatientReportRequestParam request);
    }
}