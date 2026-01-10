using BloodBankManagement.Services.Reports;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace BloodBankManagement.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]
    public class BBPatientReportController : ApiController
    {
        private readonly IBBPatientReport _bBPatientReport;
        public BBPatientReportController(IBBPatientReport StandardPatientReportService)
        {
            _bBPatientReport = StandardPatientReportService;
        }
        [HttpPost("printreport")]
        [ProducesResponseType(typeof(ServiceResponse<Models.BBReportOutputDetails>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PrintReport(Contracts.BBPatientReportRequestParam request)
        {
            ErrorOr<List<Models.BBReportOutputDetails>> patinetDetail = await _bBPatientReport.PrintReport(request);
            return patinetDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Models.BBReportOutputDetails>>("", "200", patinetDetail.Value)),
             errors => Problem(errors));
        }       
    }
}