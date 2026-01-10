using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using BloodBankManagement.Models;
using BloodBankManagement.Services.StandardPatinetReport;
using ErrorOr;
using MasterManagement.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Audit;

namespace BloodBankManagement.Controllers
{

    [CustomAuthorize("BloodBankMgmt")]
    public class StandardPatientReportController : ApiController
    {
        private readonly IStandardPatientReportService _standardPatientReportService;
        private readonly IAuditService _auditService;
        public StandardPatientReportController(IStandardPatientReportService StandardPatientReportService, IAuditService auditService)
        {
            _standardPatientReportService = StandardPatientReportService;
            _auditService = auditService;
        }
        [HttpPost("standardpatientreport")]
        [ProducesResponseType(typeof(ServiceResponse<Models.StandardPatinetReport>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStandardPatientReport(FetchPatientRequest request)
        {
            ErrorOr<List<Models.StandardPatinetReport>> patinetDetail = await _standardPatientReportService.GetStandardPatientReport(request);
            return patinetDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<List<Models.StandardPatinetReport>>("", "200", patinetDetail.Value)),
             errors => Problem(errors));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<Contracts.UpdateStandardPatientResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStandardPatientReport(UpdateStandardPatientRequest request)
        {
            var UpdateStandardPatientResult = Models.UpdateStandardPatinetReport.From(request);

            if (UpdateStandardPatientResult.IsError)
            {
                return Problem(UpdateStandardPatientResult.Errors);
            }
            var UpdateStandardPatient = UpdateStandardPatientResult.Value;
            ErrorOr<Contracts.UpdateStandardPatientResponse> patinetDetail;
            using (var auditScope = new AuditScope<UpdateStandardPatientRequest>(request, _auditService, "", new string[] { "Standard Patient Report Update" }))
            {
                patinetDetail = await _standardPatientReportService.UpdateStandardPatientReport(UpdateStandardPatient);
                auditScope.IsRollBack = patinetDetail.IsError;
                auditScope.VisitNo = request.RegistrationID.ToString();
            }
            return patinetDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<Contracts.UpdateStandardPatientResponse>("", "200", patinetDetail.Value)),
             errors => Problem(errors));
        }
        [HttpPost("patientreportprint")]
        [ProducesResponseType(typeof(ServiceResponse<GetPatientPrintReport>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStandardPatientReportPrint(FetchPatientReportRequest request)
        {
            ErrorOr<GetPatientPrintReport> patinetDetail = await _standardPatientReportService.GetStandardPatientReportPrint(request);
            return patinetDetail.Match(
             Tariffs => base.Ok(new ServiceResponse<GetPatientPrintReport>("", "200", patinetDetail.Value)),
             errors => Problem(errors));
        }
    }
}