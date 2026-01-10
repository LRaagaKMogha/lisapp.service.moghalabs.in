using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using RtfPipe.Tokens;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class OpdPatientController : ControllerBase
    {
        private readonly IOPDPatientRepository _OPDPatientRepository;
        public OpdPatientController(IOPDPatientRepository noteRepository)
        {
            _OPDPatientRepository = noteRepository;
        }

        [HttpPost]
        [Route("api/OPDPatient/InsertOPDPatient")]
        public OPDPatientDTOResponse InsertOPDPatient(OPDPatientOfficeDTO req)
        {
            OPDPatientDTOResponse result = new OPDPatientDTOResponse();
            try
            {
                result = _OPDPatientRepository.InsertOPDPatient(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertOPDPatient", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetOPDPatientList")]
        public List<OPDPatientDTOList> GetOPDPatientList(CommonFilterRequestDTO req)
        {
            List<OPDPatientDTOList> result = new List<OPDPatientDTOList>();
            try
            {
                result = _OPDPatientRepository.GetOPDPatientList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDPatientList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetOPDDoctorPatientList")]
        public List<OPDPatientDoctorDTOList> GetOPDDoctorPatientList(CommonFilterRequestDTO req)
        {
            List<OPDPatientDoctorDTOList> result = new List<OPDPatientDoctorDTOList>();
            try
            {
                result = _OPDPatientRepository.GetOPDDoctorPatientList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDDoctorPatientList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientBookingList")]
        public List<OPDPatientBookingList> GetPatientBookingList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDPatientBookingList> result = new List<OPDPatientBookingList>();
            try
            {
                result = _OPDPatientRepository.GetPatientBookingList(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientBookingList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientData")]
        public List<SearchOPDPatient> GetPatientData(SearchOPDPatientRequest RequestItem)
        {
            List<SearchOPDPatient> result = new List<SearchOPDPatient>();
            try
            {
                result = _OPDPatientRepository.GetPatientData(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientData", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientVitalData")]
        public List<OPDPatientVitalList> GetPatientVitalData(SearchOPDPatientVitalRequest RequestItem)
        {
            List<OPDPatientVitalList> result = new List<OPDPatientVitalList>();
            try
            {
                result = _OPDPatientRepository.GetPatientVitalData(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientVitalData", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/OpDPatientHistory")]
        public List<OPDOPDPatientHistory> OpDPatientHistory(SearchOPDPatientRequest RequestItem)
        {
            List<OPDOPDPatientHistory> result = new List<OPDOPDPatientHistory>();
            try
            {
                result = _OPDPatientRepository.OpDPatientHistory(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.OpDPatientHistory", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientOPDData")]
        public OPDPatientoutputData GetPatientOPDData(SearchOPDPatientDataRequest RequestItem)
        {
            OPDPatientoutputData result = new OPDPatientoutputData();
            try
            {
                var patientdata = _OPDPatientRepository.GetPatientOPDData(RequestItem);
                if (patientdata != null)
                {
                    result.PatientNo = patientdata.PatientNo;
                    result.Subjective = patientdata.Subjective;
                    result.Objective = patientdata.Objective;
                    result.Assessment = patientdata.Assessment;
                    result.Plan = patientdata.Plan;
                    result.Remarks = patientdata.Remarks;
                    result.TemplateNo = patientdata.TemplateNo;
                    result.TempDiseaseNo = patientdata.TempDiseaseNo;
                    result.TemplateName = patientdata.TemplateName;
                    result.TemplateText = patientdata.TemplateText;
                    result.CheifComplaints = patientdata.CheifComplaints;
                    result.PresentingComplaints = patientdata.PresentingComplaints;
                    result.PastHistory = patientdata.PastHistory;
                    result.PhysicalExamination = patientdata.PhysicalExamination;
                    result.SystemicExamination = patientdata.SystemicExamination;
                    result.NutritionalSpec = patientdata.NutritionalSpec;
                    result.DiagnosisFollowup = patientdata.DiagnosisFollowup;
                    result.ProvisionalDiagnosis = patientdata.ProvisionalDiagnosis;
                    result.followUpCommands = patientdata.followUpCommands;
                    result.followUpDate = patientdata.followUpDate;
                    result.FollowOPDNo = patientdata.FollowOPDNo;
                    result.generalCommands = patientdata.GeneralCommands;
                    result.CheifComplaintslst = patientdata.CheifComplaintslst;
                    var TestData = _OPDPatientRepository.GetPatientOPDTestData(RequestItem);
                    var drugdata = _OPDPatientRepository.GetPatientOPDDrugData(RequestItem);
                    result.DrugsList = drugdata;
                    result.TestList = TestData;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientOPDData", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpGet]
        [Route("api/OPDPatient/GetOPDService")]
        public List<ServiceSearchDTO> GetOPDService(int VenueNo, int VenueBranchNo, int doctorNo, int type)
        {
            List<ServiceSearchDTO> objresult = new List<ServiceSearchDTO>();
            try
            {
                objresult = _OPDPatientRepository.GetOPDService(VenueNo, VenueBranchNo, doctorNo, type);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDService", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        [HttpGet]
        [Route("api/OPDPatient/GetOPDMedicineData")]
        public List<OPDPatientMedicineList> GetOPDMedicineData(int VenueNo, int VenueBranchNo, int doctorNo)
        {
            List<OPDPatientMedicineList> result = new List<OPDPatientMedicineList>();
            try
            {
                result = _OPDPatientRepository.GetOPDMedicineData(VenueNo, VenueBranchNo, doctorNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDMedicineData", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/InsertPhysicianDiagnosis")]
        public OPDDiagnosisDTOResponse InsertPhysicianDiagnosis(OPDDiagnosisDTORequest req)
        {
            OPDDiagnosisDTOResponse result = new OPDDiagnosisDTOResponse();
            try
            {
                result = _OPDPatientRepository.InsertPhysicianDiagnosis(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertPhysicianDiagnosis", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetOPDApptDetails")]
        public List<OPDApptDetails> GetOPDApptDetails(OPDApptDetailsreq req)
        {
            List<OPDApptDetails> result = new List<OPDApptDetails>();
            try
            {
                result = _OPDPatientRepository.GetOPDApptDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDApptDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetOPDDoctorList")]
        public List<OPDDoctorMainList> GetOPDDoctorList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDDoctorMainList> result = new List<OPDDoctorMainList>();
            try
            {
                result = _OPDPatientRepository.GetOPDDoctorList(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDDoctorList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetOPDPhysicianAmount")]
        public int GetOPDPhysicianAmount(OPDPatientOfficeDTO RequestItem)
        {
            int objresult = 0;
            try
            {
                objresult = _OPDPatientRepository.GetOPDPhysicianAmount(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDPhysicianAmount", ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.PhysicianNo, RequestItem.VenueNo, RequestItem.VenueBranchNo);
            }
            return objresult;
        }
        [HttpGet]
        [Route("api/OPDPatient/Gethumanbodyparts")]
        public List<Humanbodyparts> Gethumanbodyparts(int VenueNo, int VenueBranchNo, int type)
        {
            List<Humanbodyparts> objresult = new List<Humanbodyparts>();
            try
            {
                objresult = _OPDPatientRepository.Gethumanbodyparts(VenueNo, VenueBranchNo, type);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.Gethumanbodyparts", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpGet]
        [Route("api/OPDPatient/GetOPDPatientMasterDefinedInvDetails")]
        public List<OPDPatientDisVsInvDetails> GetOPDPatientMasterDefinedInvDetails(string type, int patientNo, int venueNo, int venueBranchNo)
        {
            List<OPDPatientDisVsInvDetails> result = null;
            try
            {
                result = _OPDPatientRepository.GetOPDPatientMasterDefinedInvDetails(type, patientNo, venueNo, venueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetOPDPatientDiseaseHistoryDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [HttpGet]
        [Route("api/OPDPatient/GetOPDPatientMasterDefinedDrugDetails")]
        public List<OPDPatientDisVsDrugDetails> GetOPDPatientDiseaseHistoryDrugDetails(string type, int patientNo, int venueNo, int venueBranchNo)
        {
            List<OPDPatientDisVsDrugDetails> result = null;
            try
            {
                result = _OPDPatientRepository.GetOPDPatientMasterDefinedDrugDetails(type, patientNo, venueNo, venueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetOPDPatientDiseaseHistoryDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [HttpGet]
        [Route("api/OPDPatient/GetOPDBeforeANDAfterImageList")]
        public List<OPDBeforeAfterImageList> GetOPDBeforeANDAfterImageList(int VenueNo, int VenueBranchNo, int OPDPatientNo, int PatientNo, int VisitNo)
        {
            List<OPDBeforeAfterImageList> result = new List<OPDBeforeAfterImageList>();
            try
            {
                result = _OPDPatientRepository.GetOPDBeforeANDAfterImageList(VenueNo, VenueBranchNo, OPDPatientNo, PatientNo, VisitNo);
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        if (item.imagingNo > 0 && item.b_Type == "Before")
                        {
                            if (item.b_PathName != "../assets/img/Default-img.png")
                            {
                                string base64 = "";
                                // Check if the image file exists
                                if (!System.IO.File.Exists(item.b_PathName))
                                {

                                }
                                else
                                {
                                    // Read the image file as a byte array
                                    byte[] imageBytes = System.IO.File.ReadAllBytes(item.b_PathName);

                                    // Convert the byte array to a base64 string
                                    string base64String = Convert.ToBase64String(imageBytes);

                                    // Construct the data URL with the appropriate MIME type (e.g., "image/png")
                                    string mimeType = "image/png"; // Update this with the actual MIME type of your image
                                    string dataUrl = $"data:{mimeType};base64,{base64String}";
                                    item.b_Src = dataUrl;
                                }
                            }
                        }
                        if (item.imagingNo > 0 && item.a_Type == "After")
                        {
                            if (item.a_PathName != "../assets/img/Default-img.png")
                            {
                                string base64 = "";
                                // Check if the image file exists
                                if (!System.IO.File.Exists(item.a_PathName))
                                {

                                }
                                else
                                {
                                    // Read the image file as a byte array
                                    byte[] imageBytes = System.IO.File.ReadAllBytes(item.a_PathName);

                                    // Convert the byte array to a base64 string
                                    string base64String = Convert.ToBase64String(imageBytes);

                                    // Construct the data URL with the appropriate MIME type (e.g., "image/png")
                                    string mimeType = "image/png"; // Update this with the actual MIME type of your image
                                    string dataUrl = $"data:{mimeType};base64,{base64String}";
                                    item.a_Src = dataUrl;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetOPDBeforeANDAfterImageList", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetOPDTreatmentPlanDetails")]
        public OPDTreatmentPlan GetOPDTreatmentPlanDetails(OPDTreatmentPlan req)
        {
            OPDTreatmentPlan result = new OPDTreatmentPlan();
            try
            {
                result = _OPDPatientRepository.GetOPDTreatmentPlanDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDTreatmentPlanDetails" + req.appointmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetPatientDocumentDetails")]
        public List<OPDBulkFileUpload> GetPatientDocumentDetails(PatientDocUploadReq Req)
        {
            List<OPDBulkFileUpload> objresult = new List<OPDBulkFileUpload>();
            try
            {
                objresult = _OPDPatientRepository.GetPatientDocumentDetails(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientDocumentDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, (int)Req.venueNo, (int)Req.venueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientDocumentAll")]
        public List<DocumentInfo> GetPatientDocumentAll(PatientDocUploadReq obj)
        {
            List<DocumentInfo> objresult = new List<DocumentInfo>();
            try
            {
                objresult = _OPDPatientRepository.GetPatientDocumentAll(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientDocumentAll", ExceptionPriority.Low, ApplicationType.APPSERVICE, (int)obj.venueNo, (int)obj.venueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetDrugDetails")]
        public List<drugresponse> GetDrugDetails(drugreq RequestItem)
        {
            List<drugresponse> patientAssessment = new List<drugresponse>();
            try
            {
                patientAssessment = _OPDPatientRepository.GetDrugDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetDrugDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.venueNo, 0, 0);
            }
            return patientAssessment;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetSkinHistory")]
        public List<ClinicalHistory> GetSkinHistory(SkinHistoryReq req)
        {
            List<ClinicalHistory> patientAssessment = new List<ClinicalHistory>();
            try
            {
                patientAssessment = _OPDPatientRepository.GetSkinHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetSkinHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return patientAssessment;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetopdclinicalHistory")]
        public List<ClinicalHistory> GetopdclinicalHistory(SkinHistoryReq req)
        {
            List<ClinicalHistory> patientAssessment = new List<ClinicalHistory>();
            try
            {
                patientAssessment = _OPDPatientRepository.GetopdclinicalHistory(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetopdclinicalHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return patientAssessment;
        }
        [HttpPost]
        [Route("api/OPDPatient/OPDImagingFile")]
        public OPDBeforeAfterImageList OPDImagingFile([FromBody] OPDBeforeAfterImageList objDTO)
        {
            OPDBeforeAfterImageList result = new OPDBeforeAfterImageList();
            try
            {
                result = _OPDPatientRepository.OPDImagingFile(objDTO);
                if (result != null)
                {
                    var res = _OPDPatientRepository.InserOPDImaging(result);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.OPDImagingFile", ExceptionPriority.High, ApplicationType.APPSERVICE, (int)objDTO.venueNo, (int)objDTO.venueBranchNo, (int)objDTO.userNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/InserOPDImaging")]
        public OPDBeforeAfterImageListResponse InserOPDImaging(OPDBeforeAfterImageList res)
        {
            OPDBeforeAfterImageListResponse objresult = new OPDBeforeAfterImageListResponse();
            try
            {
                objresult = _OPDPatientRepository.InserOPDImaging(res);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InserOPDImaging" + res.appointmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.venueNo, res.venueBranchNo, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/OPDPatient/InsertFollowUpAppointment")]
        public OPDDiagnosisDTOFollowupResponse InsertFollowUpAppointment(OPDDiagnosisDTORequest req)
        {
            OPDDiagnosisDTOFollowupResponse result = new OPDDiagnosisDTOFollowupResponse();
            try
            {
                result = _OPDPatientRepository.InsertFollowUpAppointment(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertFollowUpAppointment", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDPatient/InsertopdclinicHistory")]
        public CommonAdminResponse InsertopdclinicHistory(InsertSkinHistory insertSkinHistory)
        {
            CommonAdminResponse adminResponse = new CommonAdminResponse();
            try
            {
                adminResponse = _OPDPatientRepository.InsertopdclinicHistory(insertSkinHistory);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertopdclinicHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, insertSkinHistory.VenueNo, insertSkinHistory.VenueBranchNo, insertSkinHistory.PatientVisitNo);
            }
            return adminResponse;
        }

        [HttpPost]
        [Route("api/OPDPatient/InsertSkinHistory")]
        public CommonAdminResponse InsertSkinHistory(InsertSkinHistory insertSkinHistory)
        {
            CommonAdminResponse adminResponse = new CommonAdminResponse();
            try
            {
                adminResponse = _OPDPatientRepository.InsertSkinHistory(insertSkinHistory);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertSkinHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, insertSkinHistory.VenueNo, insertSkinHistory.VenueBranchNo, insertSkinHistory.PatientVisitNo);
            }
            return adminResponse;
        }

        [HttpPost]
        [Route("api/OPDPatient/OPDInsertTreatmentPlan")]
        public TreatmentPlanResponse OPDInsertTreatmentPlan(OPDTreatmentPlan req)
        {
            TreatmentPlanResponse result = new TreatmentPlanResponse();
            try
            {
                result = _OPDPatientRepository.InsertTreatmentPlan(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertTreatmentPlan" + req.oPDTreatmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/OPDImagingIncludingreport")]
        public ImageListResponse OPDImagingIncludingreport(OPDBeforeAfterImageList res)
        {
            ImageListResponse objresult = new ImageListResponse();
            try
            {
                objresult = _OPDPatientRepository.OPDImagingIncludingreport(res);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.OPDImaging" + res.appointmentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.venueNo, res.venueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/OPDPatient/GetOPDStatusLogList")]
        public List<OPDStatusLogListResponse> GetOPDStatusLogList(OPDStatusLogListRequest RequestItem)
        {
            List<OPDStatusLogListResponse> patientAssessment = new List<OPDStatusLogListResponse>();
            try
            {
                patientAssessment = _OPDPatientRepository.GetOPDStatusLogList(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDStatusLogList", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return patientAssessment;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("api/display/GetDisplayView")]
        public List<displaylist> GetDisplayView(int VenueNo, int VenueBranchNo, int type)
        {
            List<displaylist> objresult = new List<displaylist>();
            try
            {
                objresult = _OPDPatientRepository.GetDisplayView(VenueNo, VenueBranchNo, type);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetDisplayView", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return objresult;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetOPDPatientMachineList")]
        public List<OPDPatientMachineDTOList> GetOPDPatientMachineList(CommonFilterRequestDTO req)
        {
            List<OPDPatientMachineDTOList> result = new List<OPDPatientMachineDTOList>();
            try
            {
                result = _OPDPatientRepository.GetOPDPatientMachineList(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetOPDPatientMachineList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientMachineBookingList")]
        public List<OPDPatientMachineBookingList> GetPatientMachineBookingList(OPDPatientBookingRequest RequestItem)
        {
            List<OPDPatientMachineBookingList> result = new List<OPDPatientMachineBookingList>();
            try
            {
                result = _OPDPatientRepository.GetPatientMachineBookingList(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientMachineBookingList", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/GetPatientMachineData")]
        public List<SearchOPDMachinePatient> GetPatientMachineData(SearchOPDPatientRequest RequestItem)
        {
            List<SearchOPDMachinePatient> result = new List<SearchOPDMachinePatient>();
            try
            {
                result = _OPDPatientRepository.GetPatientMachineData(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.GetPatientMachineData", ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/OPDPatient/InsertOPDMachinePatient")]
        public OPDPatientMachineResponse InsertOPDMachinePatient(OPDPatientOfficeDTO req)
        {
            OPDPatientMachineResponse result = new OPDPatientMachineResponse();
            try
            {
                result = _OPDPatientRepository.InsertOPDMachinePatient(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OPDPatientController.InsertOPDMachinePatient", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }
    }
}