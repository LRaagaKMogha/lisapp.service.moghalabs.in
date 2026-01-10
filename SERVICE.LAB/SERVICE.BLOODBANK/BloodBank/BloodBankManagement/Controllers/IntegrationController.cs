using AutoMapper;
using BloodBankManagement.Helpers;
using BloodBankManagement.Services.Integration;
using BloodBankManagement.Services.Reports;
using DEV.Common;
using DEV.Model.Integration;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared;

//using RCMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DEV.API.SERVICE.Controllers
{
    [CustomAuthorize("BloodBankMgmt")]
    public class IntegrationController : ApiController
    {
        private readonly IBBPatientReport _bBPatientReport;
        private readonly IIntegrationService _integrationService;
        BloodBankDataContext _integrationContext;
        private IMapper _mapper;

        public IntegrationController(IMapper mapper, BloodBankDataContext integrationContext, IBBPatientReport StandardPatientReportService,IIntegrationService integrationService)
        {
            _bBPatientReport = StandardPatientReportService;
            _integrationContext = integrationContext;
            _integrationService = integrationService;
            _mapper = mapper;
        }        

        [HttpGet]
        [Route("/api/Integration/GetReportPDFDetails")]
        public async Task<reportresponsedetails> GetReportPDFDetails(reportrequestdetails reportrequestdetails)
        {
            reportresponsedetails response = new reportresponsedetails();
            response.labresponsedetails = new List<labresponsedetails>();

            try
            {
                var bloodbankregistrations =  await _integrationService.GetPDFReportDetails(reportrequestdetails, 1, 1);
                
                List<labtestdetails> labtestdetails = new List<labtestdetails>();

                if (bloodbankregistrations.Value.Count > 0)
                {
                    foreach (var bloodbankreg in bloodbankregistrations.Value)
                    {
                        labresponsedetails labdetail = new labresponsedetails();
                        labdetail.visitnumber = string.IsNullOrEmpty(reportrequestdetails.visitnumber) ? bloodbankreg.CaseOrVisitNumber : reportrequestdetails.visitnumber;
                        labdetail.casenumber = string.IsNullOrEmpty(reportrequestdetails.casenumber) ? bloodbankreg.CaseOrVisitNumber : reportrequestdetails.casenumber;
                        labdetail.SourceRequestID = reportrequestdetails.sourcerequestid;
                        labdetail.SourceSystem = reportrequestdetails.sourcesystem.ToString();
                        labdetail.accessionno = bloodbankreg.LabAccessionNumber;
                        labdetail.labregistereddttm = bloodbankreg.RegistrationDateTime;
                        BloodBankManagement.Contracts.BBPatientReportRequestParam request = new BloodBankManagement.Contracts.BBPatientReportRequestParam
                        (
                            RowNo: 0,
                            PatientNo: 0,
                            RegistrationNo: Convert.ToInt32(bloodbankreg.RegistrationId),
                            IdentityNo: 0,
                            VenueNo: 1,
                            VenueBranchNo: 1,
                            UserNo: 1,
                            TestNos: "",
                            TestNo: 0,
                            IsLogo: true,
                            PageCode: "",
                            ReportType: "",
                            ReportKey: "BBReportPrint"
                        );
                        ErrorOr<List<BloodBankManagement.Models.BBReportOutputDetails>> patinetDetail = await _bBPatientReport.PrintReport(request);

                        var bloodsampleresults = await _integrationService.GetTestDetails(bloodbankreg.RegistrationId);

                        List<labtestdetails> lstdata = new List<labtestdetails>();

                        foreach (var results in bloodsampleresults.Value)
                        {
                            lstdata.Add(new Model.Integration.labtestdetails()
                            {
                                TestDescription = results.TestName,
                                TestStatus = results.Status
                            });
                        }
                        labdetail.reportdetails = new List<labreportdetails>
                        {
                            new labreportdetails
                            {
                                reportdata = patinetDetail.Value.Count > 0 ? System.IO.File.ReadAllBytes(patinetDetail.Value[0].PatientExportFolderPath) : null,
                                accessionno = bloodbankreg.LabAccessionNumber,
                                testdetails = lstdata
                            }
                        };
                        response.labresponsedetails.Add(labdetail);
                    }
                    if (response.labresponsedetails != null)
                    {
                        response.referenceno = Guid.NewGuid().ToString();
                        response.responsecode = StatusCodes.Status200OK.ToString();
                        response.responsemsg = "Get report pdf details retrieved successfully";
                    }
                    else
                    {
                        response.responsecode = "201";
                        response.responsemsg = "report pdf details not retrieved successfully. Please try retrieving the details again after sometime";
                    }
                }
                else
                {
                    response.responsecode = "202";
                    response.responsemsg = "report pdf details not retrieved successfully as no records available for the given input criteria";
                }
            }
            catch(Exception ex)
            {
                response.responsecode = "201";
                response.responsemsg = "report pdf details not retrieved successfully. Please try retrieving the details again after sometime";
            }

            return Task.FromResult(response).Result;
        }

        [HttpPost]
        [Route("GetBBReportPDFDetails")]
        public async Task<reportresponsedetails> GetBBReportPDFDetails(reportrequestdetails reportrequestdetails)
        {
            reportresponsedetails response = new reportresponsedetails();
            response.labresponsedetails = new List<labresponsedetails>();

            try
            {
                var bloodbankregistrations = await _integrationService.GetPDFReportDetails(reportrequestdetails, 1, 1);

                List<labtestdetails> labtestdetails = new List<labtestdetails>();

                if (bloodbankregistrations.Value.Count > 0)
                {
                    foreach (var bloodbankreg in bloodbankregistrations.Value)
                    {
                        labresponsedetails labdetail = new labresponsedetails();
                        labdetail.visitnumber = string.IsNullOrEmpty(reportrequestdetails.visitnumber) ? string.Empty : reportrequestdetails.visitnumber;
                        labdetail.casenumber = string.IsNullOrEmpty(reportrequestdetails.casenumber) ? string.Empty : reportrequestdetails.casenumber;
                        labdetail.SourceRequestID = reportrequestdetails.sourcerequestid;
                        labdetail.SourceSystem = string.IsNullOrEmpty(reportrequestdetails.visitnumber) ? "EMR": "RCMS";
                        labdetail.accessionno = bloodbankreg.LabAccessionNumber;
                        labdetail.labregistereddttm = bloodbankreg.RegistrationDateTime;
                        BloodBankManagement.Contracts.BBPatientReportRequestParam request = new BloodBankManagement.Contracts.BBPatientReportRequestParam
                        (
                            RowNo: 0,
                            PatientNo: 0,
                            RegistrationNo: Convert.ToInt32(bloodbankreg.RegistrationId),
                            IdentityNo: 0,
                            VenueNo: 1,
                            VenueBranchNo: 1,
                            UserNo: 1,
                            TestNos: "",
                            TestNo: 0,
                            IsLogo: true,
                            PageCode: "",
                            ReportType: "",
                            ReportKey: "BBReportPrint"
                        );
                        ErrorOr<List<BloodBankManagement.Models.BBReportOutputDetails>> patinetDetail = await _bBPatientReport.PrintReport(request);

                        var bloodsampleresults = await _integrationService.GetTestDetails(bloodbankreg.RegistrationId);

                        List<labtestdetails> lstdata = new List<labtestdetails>();

                        foreach (var results in bloodsampleresults.Value)
                        {
                            lstdata.Add(new Model.Integration.labtestdetails()
                            {
                                TestDescription = results.TestName,
                                TestStatus = results.Status
                            });
                        }
                        labdetail.reportdetails = new List<labreportdetails>
                        {
                            new labreportdetails
                            {
                                reportdata = patinetDetail.Value.Count > 0 ? System.IO.File.ReadAllBytes(patinetDetail.Value[0].PatientExportFolderPath) : null,
                                accessionno = bloodbankreg.LabAccessionNumber,
                                testdetails = lstdata
                            }
                        };
                        response.labresponsedetails.Add(labdetail);
                    }
                    if (response.labresponsedetails != null)
                    {
                        response.referenceno = Guid.NewGuid().ToString();
                        response.responsecode = StatusCodes.Status200OK.ToString();
                        response.responsemsg = "Get report pdf details retrieved successfully";
                    }
                    else
                    {
                        response.responsecode = "201";
                        response.responsemsg = "report pdf details not retrieved successfully. Please try retrieving the details again after sometime";
                    }
                }
                else
                {
                    response.responsecode = "202";
                    response.responsemsg = "report pdf details not retrieved successfully as no records available for the given input criteria";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "201";
                response.responsemsg = "report pdf details not retrieved successfully. Please try retrieving the details again after sometime";
            }

            return Task.FromResult(response).Result;
        }

        [HttpGet]
        [Route("/api/Integration/GetLabResults")]
        public Task<reportresponsediscreetdetails> GetLabResults(reportrequestdetails reportrequestdetails)
        {
            reportresponsediscreetdetails response = new reportresponsediscreetdetails();              
           
            return Task.FromResult(response);
        }

        
    }
}
