using AutoMapper;
using Azure;
using Dev.IRepository;
using Dev.Repository;
using Dev.Repository.Integration;
using DEV.Common;
using DEV.Model;
using DEV.Model.Integration;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PdfSharp.Pdf;
using RCMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationRepository _IngtegrationRepository;
        private readonly IPatientReportRepository _PatientReportRepository;
        private readonly IFrontOfficeRepository _IFrontOfficeRepository;
        private readonly ITestRepository _ITestRepository;
        private IMapper _mapper;

        public IntegrationController(IMapper mapper, IFrontOfficeRepository frontOfficeRepository, IIntegrationRepository ingtegrationRepository, IPatientReportRepository noteRepository,
            ITestRepository testRepository)
        {
            _IngtegrationRepository = ingtegrationRepository;
            _PatientReportRepository = noteRepository;
            _IFrontOfficeRepository = frontOfficeRepository;
            _ITestRepository = testRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/Integration/GetPatientDetailsForEditing")]
        public async Task<GetPatientDetailsResponse> GetPatientDetailsForEditing(EditPatientDetailsRequest request)
        {
            GetPatientDetailsResponse response = new GetPatientDetailsResponse();
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            response = await _IngtegrationRepository.GetPatientDetailsForEditing(request, user);
            return response;
        }

        [HttpPost]
        [Route("api/Integration/GetWaitingList")]
        public async Task<waitinglistresponse> GetWaitingList(waitinglistrequest waitinglistrequest)
        {
            waitinglistresponse response = new waitinglistresponse();
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            if (!waitinglistrequest.IsMassRegistration)
                response = await _IngtegrationRepository.GetWaitingList(waitinglistrequest, user);
            else
                response = await _IngtegrationRepository.GetMassRegistrationResponse(waitinglistrequest, user);
            return response;
        }

        [HttpPost]
        [Route("api/Integration/GetMassRegistrationResponse")]
        public async Task<waitinglistresponse> GetMassRegistrationResponse(waitinglistrequest waitinglistrequest)
        {
            waitinglistresponse response = new waitinglistresponse();
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            response = await _IngtegrationRepository.GetMassRegistrationResponse(waitinglistrequest, user);
            return response;
        }




        [HttpPost]
        [Route("api/Integration/saveWaitingListMessages")]
        public async Task<List<WaitingListMessage>> saveWaitingListMessages(List<WaitingListMessage> messages)
        {
            List<WaitingListMessage> response = new List<WaitingListMessage>();

            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                for(var i = 0; i < messages.Count; i++)
                {
                    var message = messages[i];
                    message.NotifyContent = message.VisitTestNo != 0 ? message.NotifyContent + "~" + "" : message.NotifyContent;
                    if(message.PatientVisitNo != 0)
                    {
                        var objDTO = _mapper.Map<PatientNotifyLog>(message);
                        var result = _IFrontOfficeRepository.InsertPatientNotifyLog(objDTO);
                        message.PatientNotifyLogNo = result;
                    }
                    else
                    {
                        var responseData = await _IngtegrationRepository.updateMessages(message, user);
                    }

                    response.Add(message);
                };
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "saveWaitingListMessages", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Integration/AddTest")]
        public async Task<IntegrationOrderTestDetailsResponse> AddTest(AddTestRequest request)
        {
            IntegrationOrderTestDetailsResponse response = new IntegrationOrderTestDetailsResponse();

            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                var serviceResponse = await _IngtegrationRepository.AddTest(request, user, 0);
                return serviceResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AddTest", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Integration/CreateManageSample")]
        public async Task<WaitingListSaveRequest> CreateManageSample(WaitingListSaveRequest request)
        {
            WaitingListSaveRequest response = new WaitingListSaveRequest();

            try
            {
                var validateInput = await _IngtegrationRepository.GetTestValidation(request);
                if (validateInput != null && validateInput.Count > 0) return null;
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                var serviceResponse = await _IngtegrationRepository.CreateManageSample(request, user);
                response = serviceResponse;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CreateManageSample", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }


        [HttpPost]
        [Route("api/Integration/updateRegistrations")]
        public async Task<bool> updateRegistrations(OnHoldRegistrationRequest request)
        {
            var response = true;
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                var serviceResponse = await _IngtegrationRepository.updateRegistrations(request, user);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "updateRegistrations", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Integration/updatemassregistration")]
        public async Task<bool> UpdateMassRegistration(UpdateMassRegistrationRequest request)
        {
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            var response = await _IngtegrationRepository.UpdateMassRegistration(request);
            return response;
        }

        [HttpGet]
        [Route("api/Integration/PatientDetails/{patientVisitId}/{system}")]
        public async Task<ExternalPatientDetailsResponse> GetPatientInformation(string patientVisitId, string system)
        {
            var user = HttpContext.Items["User"] as UserClaimsIdentity;
            var response = await _IngtegrationRepository.GetPatientInformation(patientVisitId, system, user);
            return response;
        }


        [HttpPost]
        [Route("api/Integration/SendOrderDetails")]
        public Task<orderrespondetails> SendOrderDetails(orderrequestdetails orderrequestdetails)
        {
            orderrespondetails response = new orderrespondetails();
            try
            {
                if (orderrequestdetails == null)
                {
                    response.responsemsg = "Input validation failed. Order details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.patientdetails == null)
                {
                    response.responsemsg = "Input validation failed. Patient details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.visitdetails == null)
                {
                    response.responsemsg = "Input validation failed. Visit details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.clientdetails == null)
                {
                    response.responsemsg = "Input validation failed. Client/Clinic details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.doctordetails == null)
                {
                    response.responsemsg = "Input validation failed. Doctor details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.labdetails == null)
                {
                    response.responsemsg = "Input validation failed. Lab details is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.sourcesystem == nsourcesystem.EMR
                    && orderrequestdetails.clientdetails != null && string.IsNullOrEmpty(orderrequestdetails.clientdetails.institutioncode))
                {
                    response.responsemsg = "Input validation failed. InstitutionCode details is mandatory for EMR Source System";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.sourcesystem == nsourcesystem.RCMS &&
                    orderrequestdetails.visitdetails != null && string.IsNullOrEmpty(orderrequestdetails.visitdetails.visitno))
                {
                    response.responsemsg = "Input validation failed. VisitNo is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.sourcesystem == nsourcesystem.RCMS &&
                    orderrequestdetails.visitdetails != null && orderrequestdetails.visitdetails.registrationdttm == DateTime.MinValue)
                {
                    response.responsemsg = "Input validation failed. RegisteredDttm is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }
                if (orderrequestdetails != null && orderrequestdetails.sourcesystem == nsourcesystem.EMR &&
                    orderrequestdetails.visitdetails != null && string.IsNullOrEmpty(orderrequestdetails.visitdetails.casenumber))
                {
                    response.responsemsg = "Input validation failed. CaseNumber is mandatory";
                    response.responsecode = StatusCodes.Status400BadRequest.ToString();
                    return Task.FromResult(response);
                }


                var user = HttpContext.Items["User"] as UserClaimsIdentity;

                var request = new TestDetailsRequest
                {
                    IsDelta = false
                };

                var packreq = new IntegrationPackageReq
                {
                    VenueBranchNo = user.VenueBranchNo,
                    VenueNo = user.VenueNo
                };
                var TestList  = _IngtegrationRepository.GetTestDetails(request, user);
                var PackageList = _ITestRepository.GetIntegrationPackage(packreq);

                if (orderrequestdetails.labdetails != null && orderrequestdetails.labdetails.orderitems != null)
                {
                    foreach(var order in orderrequestdetails.labdetails.orderitems)
                    {
                        if (order.itemtype == nitemtype.Test || order.itemtype == nitemtype.OptionalPackage || order.itemtype == nitemtype.OptedOut)
                        {
                            if(!TestList.Exists(x => x.TestCode == order.code))
                            {
                                response.responsemsg = "Test ordered is not available in the LIS System: "+ order.code;
                                response.responsecode = "203";
                                return Task.FromResult(response);
                            }
                        }
                        if (order.itemtype == nitemtype.Package)
                        {
                            if (!PackageList.Exists(x => x.SourcePkgCode == order.code))
                            {
                                response.responsemsg = "Package mapping not available in the LIS System for the ordered Package: " + order.code;
                                response.responsecode = "203";
                                return Task.FromResult(response);
                            }
                        }
                    }
                }
                response.referenceno = _IngtegrationRepository.SendOrderDetails(orderrequestdetails, user).referenceno;

                if (!string.IsNullOrEmpty(response.referenceno))
                {
                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "Order details received successfully";
                }
                else
                {
                    response.responsecode = "201";
                    response.responsemsg = "Order details not saved successfully. Please try resending the order details again after sometime";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"Order details not saved successfully due to following exception {ex.Message} in {ex.StackTrace}";

                MyDevException.Error(ex, $"IntegrationController.SendOrderDetails from source {orderrequestdetails.sourcesystem}", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("api/Integration/GetTestDetails")]
        public Task<TestDetailsResponse> GetTestDetails(TestDetailsRequest testDetailsRequest)
        {
            TestDetailsResponse response = new TestDetailsResponse();
            try
            {

                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response.testdetails = _IngtegrationRepository.GetTestDetails(testDetailsRequest, user);

                if (response.testdetails != null)
                {
                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "Get test details retrieved successfully";
                }
                else
                {
                    response.responsecode = "201";
                    response.responsemsg = "test details not retrieved successfully. Please try retrieving the details again after sometime";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"Test details not retrieved successfully due to following exception {ex.Message}";

                MyDevException.Error(ex, $"IntegrationController.GetTestDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("api/Integration/GetReportPDFDetails")]
        public async Task<reportresponsedetails> GetReportPDFDetails(reportrequestdetails reportrequestdetails)
        {
            reportresponsedetails response = new reportresponsedetails();

            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response.labresponsedetails = new List<labresponsedetails>();
                response.labresponsedetails = _IngtegrationRepository.GetPDFReportDetails(reportrequestdetails, user);
                foreach (var labdetail in response.labresponsedetails)
                {
                    var labtestdetails = _IngtegrationRepository.GetPDFReportTestDetails(labdetail.lisvisitno, user);
                    try
                    {
                        PatientReportDTO PatientItem = new PatientReportDTO
                        {
                            pagecode = "PCPR",
                            venueno = user.VenueNo,
                            venuebranchno = user.VenueBranchNo,
                            patientvisitno = labdetail.lisvisitno.ToString(),
                            isheaderfooter = true,
                            process = 1,
                            reportstatus = 0,
                            isNABLlogo = true,
                            userno = user.UserNo,
                            isdefault = false,
                            pritlanguagetype = 1                            
                        };

                        foreach (var test in labtestdetails)
                        {
                            PatientItem.resulttypenos = string.IsNullOrEmpty(PatientItem.resulttypenos) ? test.ResultTypeNo.ToString() : string.Concat(PatientItem.resulttypenos, ",", test.ResultTypeNo.ToString());
                            PatientItem.orderlistnos = string.IsNullOrEmpty(PatientItem.orderlistnos) ? test.OrderListNo.ToString() : string.Concat(PatientItem.orderlistnos, ",", test.OrderListNo.ToString());
                        }

                        var result = await _PatientReportRepository.PrintPatientReport(PatientItem);
                        labdetail.reportdetails = new List<labreportdetails>();
                        labdetail.reportdetails.Add(new labreportdetails
                        {
                            reportdata = System.IO.File.ReadAllBytes(result[0].PatientExportFolderPath),
                            accessionno = labdetail.accessionno,
                            testdetails = new List<labtestdetails>()
                        });
                        labdetail.reportdetails.FirstOrDefault(x => x.accessionno == labdetail.accessionno).testdetails = labtestdetails;
                    }
                    catch (Exception ex)
                    {
                        MyDevException.Error(ex, "PatientReportController.PrintPatientReport/patientvisitno-" + labdetail.lisvisitno, ExceptionPriority.High, ApplicationType.APPSERVICE, user.VenueNo, user.VenueBranchNo, user.UserNo);
                    }
                }
                if (response.labresponsedetails != null && response.labresponsedetails.Count > 0)
                {
                    response.referenceno = Guid.NewGuid().ToString();
                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "Get report pdf details retrieved successfully";
                }
                else
                {
                    response.responsecode = "202";
                    response.responsemsg = "report pdf details not retrieved successfully. No records for the given search criteria";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"report pdf details not retrieved successfully due to following exception {ex.Message}";

                MyDevException.Error(ex, $"IntegrationController.GetReportPDF", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return await Task.FromResult(response);
        }

        [HttpGet]
        [Route("api/Integration/GetLabResults")]
        public Task<reportresponsediscreetdetails> GetLabResults(reportrequestdetails reportrequestdetails)
        {
            reportresponsediscreetdetails response = new reportresponsediscreetdetails();
            response.reportdetails = new List<labreportdiscreetdetails>();

            
            List<string> abnormallist = new List<string> { "L", "H", "CH", "CL" };
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;

                TestDetailsRequest request = new TestDetailsRequest();
                request.IsDelta = false;
                var testMasters = _IngtegrationRepository.GetTestDetails(request, user);

                List<labresponsedetails> labresponsedetails = new List<labresponsedetails>();
                labresponsedetails = _IngtegrationRepository.GetPDFReportDetails(reportrequestdetails, user);
                foreach (var labdetail in labresponsedetails)
                {
                    var results = _IngtegrationRepository.GetDiscreetLabData(labdetail.lisvisitno, user);
                    if (results != null)
                    {
                        foreach (var result in results)
                        {

                            response.reportdetails.Add(new labreportdiscreetdetails()
                            {
                                labrequestNo = result.VisitID,
                                labregistereddttm = string.IsNullOrEmpty(result.VisitDTTM) ? string.Empty : DateTime.ParseExact(result.VisitDTTM, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).ToString("s"),
                                PAgeYear = DateTime.Now.Year - result.DOB.Year,
                                PAgeDay = Convert.ToInt32((DateTime.Now.Date - result.DOB.Date).TotalDays),
                                ProfileDesc = result.PackageName,
                                VisitNumber = labdetail.visitnumber,
                                AbNormalStatus = abnormallist.Exists(x=> x == result.ResultFlag) ? "Yes" : "No",
                                HSSComment = string.Empty,
                                GroupName = result.GroupName,
                                GroupSeqNo = result.GroupSeqNo,
                                IsMicrobiology = false,
                                RangeRemark = string.Empty,
                                TestCodeReference = string.Empty,
                                ResultComment = result.ResultComments,
                                ResultStatus = string.Empty,
                                Section = result.DepartmentName,
                                SectionSeq = result.DepartmentSeqNo > 0 ? result.DepartmentSeqNo.ToString() : string.Empty,
                                RangeTypeComment = result.DisplayRR,
                                TestCode = testMasters.FirstOrDefault( x=> x.TestNo == result.TestNo) != null ? testMasters.FirstOrDefault(x => x.TestNo == result.TestNo).TestCode : string.Empty,
                                Testdate = string.IsNullOrEmpty(result.SampleCollectedDTTM) ? string.Empty : DateTime.ParseExact(result.SampleCollectedDTTM,"dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture).ToString("s"),
                                TestDesc = string.IsNullOrEmpty(result.TestName) ? result.SubTestName : result.TestName,
                                TestPrefix = string.Empty,
                                TestResult = result.Result,
                                TestSeq = result.TSeqNo > 0 ? result.TSeqNo.ToString() : string.Empty,
                                TestType = HelperMethods.getResulType(result.ResultType),
                                TestUnit = result.UnitName,
                                InterpNotes = result.TestComments
                            });
                        }
                    }
                }

                if (response.reportdetails != null && response.reportdetails.Count > 0)
                {
                    response.referenceno = Guid.NewGuid().ToString();
                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "Get report lab discreet result details retrieved successfully";
                }
                else
                {
                    response.responsecode = "201";
                    response.responsemsg = "report lab discreet result details not retrieved successfully. No records for the given search criteria";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"report lab discreet result details not retrieved successfully due to following exception {ex.Message}";

                MyDevException.Error(ex, $"IntegrationController.GetLabResults", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }

            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("api/Integration/GetTrendReport")]
        public Task<responseTrendReport> GetTrendReport(requestTrendReport requestTrendReport)
        {
            responseTrendReport response = new responseTrendReport();
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;

                if (requestTrendReport.TestCode != null && requestTrendReport.TestCode.Count > 0)
                {
                    response.testcode = new List<testcode>();
                    foreach (string sTestCode in requestTrendReport.TestCode)
                    {
                        var output = _IngtegrationRepository.GetTestTrendReportDetails(requestTrendReport, sTestCode, user);
                        if (output != null)
                        {
                            response.testcode.Add(new testcode
                            {
                                TestCode = sTestCode,
                                trendReport = output
                            });
                        }
                            
                    }
                } 


                if (response.testcode != null && response.testcode.Count > 0)
                {
                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "trend report details retrieved successfully";
                }
                else
                {
                    response.responsecode = "201";
                    response.responsemsg = "trend report not retrieved successfully. No records for the given criteria";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"trend report details not retrieved successfully due to following exception {ex.Message}";

                MyDevException.Error(ex, $"IntegrationController.GetTrendReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("api/Integration/GetMicroBiologyReport")]
        public Task<responseMicroResults> GetMicroBiologyReport(reportrequestdetails reportrequestdetails)
        {
            responseMicroResults response = new responseMicroResults();
            try
            {
                var user = HttpContext.Items["User"] as UserClaimsIdentity;
                response.MicroResults = new MicroResults();


                if (response.MicroResults != null)
                {
                    response.MicroResults.LabReqNo = "LB001";
                    response.MicroResults.Micro_Cnt = 1;
                    response.MicroResults.PatientName = "Prabhu";
                    response.MicroResults.PatientNRIC = "XXXX";
                    response.MicroResults.VisitNumber = "V001";

                    response.MicroResults.micro_Mbtest01 = new Micro_mbtest01();
                    response.MicroResults.micro_Mbtest01.CultureRemark = "Growth Seen";
                    response.MicroResults.micro_Mbtest01.BacterialCount = "10000 CFU/ml";
                    response.MicroResults.micro_Mbtest01.ReportStatus = "Preliminary";
                    response.MicroResults.micro_Mbtest01.Specimen = "SEMEN";
                    response.MicroResults.micro_Mbtest01.TestCode = "SEMENCULTURE";
                    response.MicroResults.micro_Mbtest01.TestDesc = "SEMEN CULTURE";
                    response.MicroResults.micro_Mbtest01.Cultures = new Cultures();
                    response.MicroResults.micro_Mbtest01.Cultures.CultureDesc = "";
                    response.MicroResults.micro_Mbtest01.Sensitivity = new List<Sensitivity>();
                    response.MicroResults.micro_Mbtest02 = new Micro_mbtest02();
                    response.MicroResults.micro_Mbtest02.Grams = new List<Grams>();
                    response.MicroResults.micro_Mbtest02.Grams.Add(new Grams()
                    {
                        GramStrain = "Pus Cells",
                        GramValue = "Rare"
                    });
                    response.MicroResults.micro_Mbtest02.Grams.Add(new Grams()
                    {
                        GramStrain = "Clue Cells",
                        GramValue = "Rare"
                    });
                    response.MicroResults.micro_Mbtest01.Sensitivity.Add(new Sensitivity()
                    {
                        SensitivityValue = "Resistant",
                        SensitivityDesc = "Azithromycin"
                    });
                    response.MicroResults.micro_Mbtest03 = new Micro_mbtest03();
                    response.MicroResults.micro_Mbtest03.Cultures = new Cultures();
                    response.MicroResults.micro_Mbtest03.Sensitivity = new List<Sensitivity>();
                    response.MicroResults.micro_Mbtest04 = new Micro_mbtest04();
                    response.MicroResults.micro_Mbtest04.Cultures = new Cultures();
                    response.MicroResults.micro_Mbtest04.Sensitivity = new List<Sensitivity>();

                    response.responsecode = StatusCodes.Status200OK.ToString();
                    response.responsemsg = "micro results report details retrieved successfully";
                }
                else
                {
                    response.responsecode = "201";
                    response.responsemsg = "micro results report not retrieved successfully. Please try retrieving the details again after sometime";
                }
            }
            catch (Exception ex)
            {
                response.responsecode = "202";
                response.responsemsg = $"micro results report details not retrieved successfully due to following exception {ex.Message}";

                MyDevException.Error(ex, $"IntegrationController.GetMicroBiologyReport", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Task.FromResult(response);
        }
    }
}
