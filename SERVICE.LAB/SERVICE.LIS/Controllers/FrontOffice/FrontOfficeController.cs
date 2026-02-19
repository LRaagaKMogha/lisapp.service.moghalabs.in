using Service.IRepository;
using Service.Common;
using Service.Model;
using Service.Model.Sample;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FrontOfficeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IFrontOfficeRepository _IFrontOfficeRepository;
        private readonly IMasterRepository _IMasterRepository;
        public FrontOfficeController(IFrontOfficeRepository noteRepository, IConfiguration config, IMasterRepository iMasterRepository)
        {
            _IFrontOfficeRepository = noteRepository;
            _config = config;
            _IMasterRepository = iMasterRepository;
        }

        /// <summary>
        /// Get Country
        /// /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/FrontOffice/GetCountry")]
        public List<TblCountryList> GetCountry(int VenueNo)
        {
            List<TblCountryList> Objresult = new List<TblCountryList>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCountry(VenueNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCountry", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }

        /// <summary>
        /// Get State
        /// </summary>
        /// <param name="countryNo"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/FrontOffice/GetState")]
        public List<TblState> GetState(int VenueNo)
        {
            List<TblState> Objresult = new List<TblState>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetState(VenueNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetState", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }
        /// <summary>
        /// Get City
        /// </summary>
        /// <param name="StateNo"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/FrontOffice/GetCity")]
        public List<TblCity> GetCity(int VenueNo)
        {
            List<TblCity> Objresult = new List<TblCity>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCity(VenueNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCity", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/FrontOffice/GetDetailsByPincode")]
        public GetDetailsByPincode GetDetailsByPincode(int VenueNo, int VenueBranchNo, string PinCode)
        {
            GetDetailsByPincode Objresult = new GetDetailsByPincode();
            try
            {
                Objresult = _IFrontOfficeRepository.GetDetailsByPincode(VenueNo, VenueBranchNo, PinCode);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetDetailsByPincode", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetCurrency")]
        public List<TblCurrency> GetCurrency(int VenueNo, int VenueBranchNo)
        {
            List<TblCurrency> Objresult = new List<TblCurrency>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCurrency(VenueNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCurrency", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetPhysicianDetails")]
        public List<TblPhysician> GetPhysicianDetails(int VenueNo, int VenueBranchNo)
        {
            List<TblPhysician> Objresult = new List<TblPhysician>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetPhysicianDetails(VenueNo, VenueBranchNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetPhysicianDetails", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/FrontOffice/GetPhysicianDetailsbyName")]
        public List<TblPhysicianSearch> GetPhysicianDetailsbyName(int VenueNo, int VenueBranchNo, string physicianName, int type = 0)
        {
            List<TblPhysicianSearch> Objresult = new List<TblPhysicianSearch>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetPhysicianDetailsbyName(VenueNo, VenueBranchNo, physicianName, type).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetPhysicianDetailsbyName", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/FrontOffice/GetDiscountMaster")]
        public List<TblDiscount> GetDiscountMaster(int VenueNo, int VenueBranchNo)
        {
            List<TblDiscount> Objresult = new List<TblDiscount>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetDiscountMaster(VenueNo, VenueBranchNo).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetDiscountMaster", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/Getservice")]
        public List<ServiceSearchDTO> Getservice(int VenueNo, int VenueBranchNo, int IsApproval)
        {
            List<ServiceSearchDTO> Objresult = new List<ServiceSearchDTO>();
            try
            {
                Objresult = _IFrontOfficeRepository.Getservice(VenueNo, VenueBranchNo, IsApproval).ToList();

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.Getservice", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetserviceDetails")]
        public ServiceRateList GetserviceDetails(int ServiceNo, string ServiceType, int ClientNo, int VenueNo, int VenueBranchNo, int physicianNo, int splratelisttype)
        {
            ServiceRateList Objresult = new ServiceRateList();
            try
            {
                Objresult = _IFrontOfficeRepository.GetserviceDetails(ServiceNo, ServiceType, ClientNo, VenueNo, VenueBranchNo, physicianNo, splratelisttype);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetserviceDetails/ServiceNo-" + ServiceNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/FrontOffice/GetGrouptest")]
        public List<GroupTestDTO> GetGrouptest(int ServiceNo, string ServiceType, int VenueNo, int VenueBranchNo)
        {
            List<GroupTestDTO> Objresult = new List<GroupTestDTO>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetGrouptest(ServiceNo, ServiceType, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetGrouptest/ServiceNo-" + ServiceNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpGet]
        [Route("api/FrontOffice/GetOptionalSelectedInPackages")]
        public List<OptionalTestDTO> GetOptionalSelectedInPackages(int ServiceNo, int VenueNo, int VenueBranchNo, int PatientVisitNo)
        {
            List<OptionalTestDTO> Objresult = new List<OptionalTestDTO>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetOptionalSelectedInPackages(ServiceNo, VenueNo, VenueBranchNo,PatientVisitNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetOptionalSelectedInPackages/ServiceNo-" + ServiceNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/Getcustomer")]
        public List<CustomerList> Getcustomers(int VenueNo, int VenueBranchNo, int UserNo, int IsFranchisee = 0, bool ExcludePostpaid = false, bool ExcludePrepaid = false, bool ExcludeCash = false, bool IsApproval = false, int IsClinical = -1 , int clientType = 0,bool IsMapping = false)
        {
            List<CustomerList> Objresult = new List<CustomerList>();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCustomers(VenueNo, VenueBranchNo, UserNo, IsFranchisee, ExcludePostpaid, ExcludePrepaid, ExcludeCash, IsApproval, IsClinical, clientType,IsMapping).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.Getcustomers", ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, UserNo);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetCustomerDetails")]
        public CustomerList GetCustomerDetails(long Customerno, int VenueNo, int VenueBranchNo)
        {
            CustomerList Objresult = new CustomerList();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCustomerDetails(Customerno, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCustomerDetails/Customerno:" + Customerno, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetCustomerCurrentBalance")]
        public CustomerCurrentBalance GetCustomerCurrentBalance(long Customerno, int VenueNo, int VenueBranchNo)
        {
            CustomerCurrentBalance Objresult = new CustomerCurrentBalance();
            try
            {
                Objresult = _IFrontOfficeRepository.GetCustomerCurrentBalance(Customerno, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCustomerCurrentBalance/Customerno:" + Customerno, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/InsertFrontOfficeMaster")]
        public ActionResult<FrontOffficeResponse> InsertFrontOfficeMaster([FromBody] FrontOffficeDTO objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            try
            {
                var _errormsg = RegistrationValidation.InsertFrontOfficeMaster(objDTO);
                if (!_errormsg.status)
                {
                    result = _IFrontOfficeRepository.InsertFrontOfficeMaster(objDTO);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.InsertFrontOfficeMaster", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return Ok(result);
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/InsertFrontOfficeRegistration")]
        public ActionResult<FrontOffficeResponse> InsertFrontOfficeRegistration([FromBody] FrontOffficeDTO objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            try
            {
                var _errormsg = RegistrationValidation.InsertFrontOfficeRegistration(objDTO);
                if (!_errormsg.status)
                {
                    result = _IFrontOfficeRepository.InsertFrontOfficeRegistration(objDTO);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.InsertFrontOfficeRegistration", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/FrontOffice/BulkFrontOfficeMaster")]
        public FrontOffficeResponse BulkFrontOfficeMaster([FromBody] List<FrontOffficeDTO> objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();

            try
            {
                foreach (var item in objDTO)
                {
                    result = _IFrontOfficeRepository.InsertFrontOfficeMaster(item);
                    //
                    objAppSettingResponse = new AppSettingResponse();
                    string AppBulkRegistrationTimeout = "BulkRegistrationTimeout";
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppBulkRegistrationTimeout);
                    string bulkregtimeout = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "0";

                    Thread.Sleep(int.Parse(bulkregtimeout));
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.BulkFrontOfficeMaster", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        [HttpPost]
        [Route("api/FrontOffice/Insertmassregistration")]
        public ActionResult<MassRegistrationResponse> Insertmassregistration([FromBody] ExternalBulkFile objDTO)
        {
            MassRegistrationResponse result = new MassRegistrationResponse();
            try
            {
                var _errormsg = massregistrationValidation.Insertmassregistration(objDTO);
                if (!_errormsg.status)
                {
                    result = _IFrontOfficeRepository.Insertmassregistration(objDTO);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.Insertmassregistration", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/FrontOffice/GetMassFileRegistration")]
        public List<MassFileDTO> GetMassFileRegistration(CommonFilterRequestDTO RequestItem)
        {
            List<MassFileDTO> lst = new List<MassFileDTO>();
            try
            {
                lst = _IFrontOfficeRepository.GetMassFileRegistration(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetMassFileRegistration", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lst;
        }
        [HttpGet]
        [Route("api/FrontOffice/DownloadMassFile")]
        public List<massPatientBarcode> DownloadMassFile(int MassFileNo, int VenueNo, int VenueBranchNo)
        {
            List<massPatientBarcode> lst = new List<massPatientBarcode>();
            try
            {
                lst = _IFrontOfficeRepository.DownloadMassFile(MassFileNo, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.DownloadMassFile", ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSFRONTOFFICE,LIMSDEFAULT")]
        [HttpPost]
        [Route("api/FrontOffice/PrintBill")]
        public async Task<ReportOutput> PrintBill(ReportRequestDTO req)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                result = await _IFrontOfficeRepository.PrintBill(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.PrintBill/visitNo - " + req.visitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpGet]
        [Route("api/FrontOffice/GetPatientdetails")]
        public GetPatientDetailsResponse GetPatientdetails(long visitNo, int VenueNo, int VenueBranchNo, string? searchType = null, int PatientNo = 0, int Isprocedure = 0)
        {
            GetPatientDetailsResponse Objresult = new GetPatientDetailsResponse();
            try
            {
                Objresult = _IFrontOfficeRepository.GetPatientDetails(visitNo, VenueNo, VenueBranchNo, searchType, PatientNo, Isprocedure);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetPatientdetails/visitNo-" + visitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/checkExists")]
        public List<rescheckExists> checkExists(reqcheckExists req)
        {
            List<rescheckExists> lst = new List<rescheckExists>();
            try
            {
                lst = _IFrontOfficeRepository.checkExists(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.checkExists/checkValue-" + req.checkValue, ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        //arun changes billing screen file upload
        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/ConvertToBase64")]
        public List<BulkFileUpload> ConvertToBase64([FromBody] FrontOffficeDTO objDTO)
        {
            List<BulkFileUpload> lstresult = new List<BulkFileUpload>();
            BulkFileUpload result = new BulkFileUpload();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();

            try
            {
                //
                objAppSettingResponse = new AppSettingResponse();
                string AppUploadPathInit = "UploadPathInit";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string Pathinit = uplodpathinit;
                var visitId = objDTO.ExternalVisitID;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                string newPath = Path.Combine(Pathinit, folderName);
                if (Directory.Exists(newPath))
                {
                    string[] filePaths = Directory.GetFiles(newPath);
                    if (filePaths != null && filePaths.Length > 0)
                    {
                        for (int f = 0; f < filePaths.Length; f++)
                        {
                            result = new BulkFileUpload();
                            string path = filePaths[f].ToString();
                            Byte[] bytes = System.IO.File.ReadAllBytes(path);
                            String base64String = Convert.ToBase64String(bytes);
                            result.FilePath = path;
                            result.ActualBinaryData = base64String;
                            var split = filePaths[f].ToString().Split('.');
                            int splitcount = split != null ? split.Count() - 1 : 0;
                            result.FileType = filePaths[f].ToString().Split('.')[splitcount];
                            result.ActualFileName = filePaths[f].ToString().Split("$$")[3];
                            result.ManualFileName = filePaths[f].ToString().Split("$$")[4];
                            lstresult.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.UploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return lstresult;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/UploadFile")]
        public FrontOffficeResponse UploadFile([FromBody] FrontOffficeDTO objDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();

            try
            {
                var base64data = objDTO.Base64Data;
                var visitId = objDTO.ExternalVisitID;
                var venueNo = objDTO.VenueNo;
                var venuebNo = objDTO.VenueBranchNo;
                var format = objDTO.FileFormat;
                string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                //
                objAppSettingResponse = new AppSettingResponse();
                string AppUploadPathInit = "UploadPathInit";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                string uploadpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                    ? objAppSettingResponse.ConfigValue : "";

                string webRootPath = uploadpath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (base64data != null && base64data.Length > 0)
                {
                    string fileName = venueNo + "_" + venuebNo + "_" + visitId + "." + format;
                    string fullPath = Path.Combine(newPath, fileName);
                    byte[] imageBytes = Convert.FromBase64String(base64data);
                    System.IO.File.WriteAllBytes(fullPath, imageBytes);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.UploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/BulkUploadFile")]
        public ActionResult<FrontOffficeResponse> BulkUploadFile([FromBody] List<BulkFileUpload> lstjDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            int venueno = 0;
            int venuebno = 0;

            try
            {
                var _errormsg = BulkFileUploadValidation.BulkUploadFile(lstjDTO);
                if (!_errormsg.status)
                {
                    foreach (var objDTO in lstjDTO)
                    {
                        venueno = objDTO.VenueNo;
                        venuebno = objDTO.VenueBranchNo;

                        var base64data = objDTO.ActualBinaryData;
                        var visitId = objDTO.ExternalVisitID;
                        var venueNo = objDTO.VenueNo;
                        var venuebNo = objDTO.VenueBranchNo;
                        var format = objDTO.FileType;
                        var actualfilename = objDTO.ActualFileName;
                        var manualfilename = objDTO.ManualFileName;
                        string folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
                        //
                        objAppSettingResponse = new AppSettingResponse();
                        string AppUploadPathInit = "UploadPathInit";
                        objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                        string uploadpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                            ? objAppSettingResponse.ConfigValue : "";

                        string webRootPath = uploadpath;
                        string newPath = Path.Combine(webRootPath, folderName);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        if (base64data != null && base64data.Length > 0)
                        {
                            string fileName = venueNo + "$$" + venuebNo + "$$" + visitId + "$$" + actualfilename + "$$" + manualfilename + "." + format;
                            string fullPath = Path.Combine(newPath, fileName);

                            byte[] imageBytes = Convert.FromBase64String(base64data);
                            System.IO.File.WriteAllBytes(fullPath, imageBytes);
                        }
                    }
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.BulkUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return Ok(result);
        }

        // scanner changes
        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpGet]
        [Route("api/FrontOffice/Getdevices")]
        public Dictionary<string, string> Getdevices()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                result.Add(key: "Scan Device 1", value: "DV1");
                result.Add(key: "Scan Device 2", value: "DV2");
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.Getdevices", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpGet]
        [Route("api/FrontOffice/ConnectScanner")]
        public string ConnectScanner(string DeviceID)
        {
            string result = string.Empty;
            try
            {
                string path = ApplicationConstants.ConnectScannerPath;
                if (OperatingSystem.IsWindows())
                {
                    using var image = Image.Load(path);
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            var extension = Path.GetExtension(path).ToLowerInvariant();
                            SixLabors.ImageSharp.Formats.IImageEncoder encoder = extension switch
                            {
                                ".jpg" or ".jpeg" => new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder(),
                                ".png" => new SixLabors.ImageSharp.Formats.Png.PngEncoder(),
                                ".bmp" => new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder(),
                                ".gif" => new SixLabors.ImageSharp.Formats.Gif.GifEncoder(),
                                ".tga" => new SixLabors.ImageSharp.Formats.Tga.TgaEncoder(),
                                ".webp" => new SixLabors.ImageSharp.Formats.Webp.WebpEncoder(),
                                _ => new SixLabors.ImageSharp.Formats.Png.PngEncoder(), // Default to PNG
                            };
                            image.Save(m, encoder);

                            byte[] imageBytes = m.ToArray();
                            result = Convert.ToBase64String(imageBytes);
                            result = result.ToString();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Image saving is only supported on Windows.");
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.Getdevices", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        //add doctor/physician in registration screen
        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/InsertDoctor")]
        public Doctordetails InsertDoctor([FromBody] Doctordetails objDTO)
        {
            Doctordetails result = new Doctordetails();
            try
            {
                result = _IFrontOfficeRepository.InsertDoctor(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.InsertDoctor", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/PushNotifyContent")]
        public int PushNotifyContent(NotificationDto req)
        {
            int result = 0;
            int patientVisitNo = req.Address != null ? Convert.ToInt32(req.Address) : 0;
            int venueNo = req.VenueNo;
            int venueBranchNo = req.VenueBranchNo;
            int userNo = req.UserNo;
            string messageType = (!string.IsNullOrEmpty(req.MessageType) ? req.MessageType : "");
            string message = (!string.IsNullOrEmpty(req.TemplateKey) ? req.TemplateKey : "");
            try
            {
                result = _IFrontOfficeRepository.PushNotifyMessage(patientVisitNo, venueNo, venueBranchNo, userNo, messageType, message);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.PushNotifyContent/visitNo-" + patientVisitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/InsertPatientNotifyLog")]
        public int InsertPatientNotifyLog(PatientNotifyLog objDTO)
        {
            int result = 0;
            try
            {
                result = _IFrontOfficeRepository.InsertPatientNotifyLog(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.InsertPatientNotifyLog", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/GetPatientNotifyLog")]
        public List<PatientNotifyLog> GetPatientNotifyLog(PatientNotifyLog req)
        {
            List<PatientNotifyLog> lst = new List<PatientNotifyLog>();
            try
            {
                lst = _IFrontOfficeRepository.GetPatientNotifyLog(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetPatientNotifyLog/patientvisitno-" + req.PatientVisitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/GetPatientClinicalSummary")]
        public ClinicalSummary GetPatientClinicalSummary(PatientNotifyLog objDTO)
        {
            ClinicalSummary clinicalSummary = new ClinicalSummary();
            try
            {
                clinicalSummary = _IFrontOfficeRepository.GetPatientClinicalSummary(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetPatientClinicalSummary/patientvisitno-" + objDTO.PatientVisitNo, ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return clinicalSummary;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/GetQueueOrderDetails")]
        public List<QueueOrderDTO> GetQueueOrderDetails(CommonFilterRequestDTO RequestItem)
        {
            List<QueueOrderDTO> lst = new List<QueueOrderDTO>();
            try
            {
                lst = _IFrontOfficeRepository.GetQueueOrderDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetQueueOrderDetails/patientvisitno-" + RequestItem.serviceNo, ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/UpdateQueueOrder")]
        public FrontOffficeQueueResponse UpdateQueueOrder(CommonFilterRequestDTO RequestItem)
        {
            FrontOffficeQueueResponse lst = new FrontOffficeQueueResponse();
            try
            {
                lst = _IFrontOfficeRepository.UpdateQueueOrder(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.UpdateQueueOrder/QueueNo-" + RequestItem.SearchKey, ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/CheckExternalVistIdExists")]
        public ExternalVisitdetailsResponse CheckExternalVistIdExists(ExternalVisitdetails req)
        {
            ExternalVisitdetailsResponse result = new ExternalVisitdetailsResponse();
            try
            {
                result = _IFrontOfficeRepository.CheckExternalVistIdExists(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.CheckExternalVistIdExists", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/GetTestPrePrintDetails")]
        public List<TestPrePrintDetailsResponse> GetTestPrePrintDetails(TestPrePrintDetailsRequest req)
        {
            List<TestPrePrintDetailsResponse> result = new List<TestPrePrintDetailsResponse>();
            try
            {
                result = _IFrontOfficeRepository.GetTestPrePrintDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetTestPrePrintDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/PrePrintManageSample")]
        public List<CreateManageSampleResponse> PrePrintManageSample(List<PrePrintBarcodeRequest> req)
        {
            List<CreateManageSampleResponse> result = new List<CreateManageSampleResponse>();
            try
            {
                result = _IFrontOfficeRepository.PrePrintManageSample(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.PrePrintManageSample", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/getvalidatetest")]
        public FrontOffficeValidatetest getvalidatetest(List<ServiceParamDTO> req)
        {
            FrontOffficeValidatetest result = new FrontOffficeValidatetest();
            try
            {
                result = _IFrontOfficeRepository.getvalidatetest(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.getvalidatetest", ExceptionPriority.High, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/GetDiscountApprovalDetails")]
        public List<GetDiscountApprovalResponse> GetDiscountApprovalDetails(GetDiscountApprovalDto objDTO)
        {
            List<GetDiscountApprovalResponse> lst = new List<GetDiscountApprovalResponse>();
            try
            {
                lst = _IFrontOfficeRepository.GetDiscountApprovalDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.GetDiscountApprovalDetails/", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return lst;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpPost]
        [Route("api/FrontOffice/InsertDiscountApprovalDetails")]
        public SaveDiscountApprovalResponse InsertDiscountApprovalDetails(SaveDiscountApprovalDto objDTO)
        {
            SaveDiscountApprovalResponse objOutput = new SaveDiscountApprovalResponse();
            try
            {
                objOutput = _IFrontOfficeRepository.InsertDiscountApprovalDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.InsertDiscountApprovalDetails/", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, objDTO.UserNo);
            }
            return objOutput;
        }

        [CustomAuthorize("LIMSFRONTOFFICE")]
        [HttpGet]
        [Route("api/FrontOffice/ValidateNricNo")]
        public dynamic ValidateCaseNo(int ServiceNo, string ServiceType, string NricNo, int VenueNo, int VenueBranchNo, bool IsNonConcurrent = false)
        {
            int Objresult = 0;
            try
            {
                Objresult = _IFrontOfficeRepository.ValidateNricNo(ServiceNo, ServiceType, NricNo, VenueNo, VenueBranchNo, IsNonConcurrent);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.ValidateNricNo/ServiceNo-" + ServiceNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/FrontOffice/OPDBulkUploadFile")]
        public FrontOffficeResponse OPDBulkUploadFile([FromBody] List<OPDBulkFileUpload> lstjDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();

            string AppUploadPathInit = "UploadPathInit";
            int venueno = 0;
            int venuebno = 0;
            try
            {
                foreach (var objDTO in lstjDTO)
                {
                    venueno = (int)objDTO.VenueNo;
                    venuebno = (int)objDTO.VenueBranchNo;

                    var base64data = objDTO.ActualBinaryData;
                    var visitId = objDTO.ExternalVisitID;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var visitno = objDTO.PatientVisitNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.actualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = venueNo + "\\" + venuebNo + "\\" + visitno + "\\" + visitId + "\\" + objDTO.docType;
                    
                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                    string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = uplodpathinit;
                    string newPath = Path.Combine(webRootPath, folderName);
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    if (base64data != null && base64data.Length > 0)
                    {
                        string fileName = venueNo + "$$" + venuebNo + "$$" + visitno + "$$" + visitId + "$$" + actualfilename + "$$" + manualfilename + "." + format;
                        string fullPath = Path.Combine(newPath, fileName);
                        if (!System.IO.File.Exists(fullPath))
                        {
                            byte[] imageBytes = Convert.FromBase64String(base64data);
                            System.IO.File.WriteAllBytes(fullPath, imageBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.OPDBulkUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/FrontOffice/RemoveUploadFile")]
        public FrontOffficeResponse RemoveUploadFile([FromBody] List<OPDBulkFileUpload> lstjDTO)
        {
            FrontOffficeResponse result = new FrontOffficeResponse();
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();

            string AppUploadPathInit = "UploadPathInit";
            int venueno = 0;
            int venuebno = 0;

            try
            {
                foreach (var objDTO in lstjDTO)
                {
                    venueno = (int)objDTO.VenueNo;
                    venuebno = (int)objDTO.VenueBranchNo;

                    var base64data = objDTO.ActualBinaryData;
                    var visitId = objDTO.ExternalVisitID;
                    var venueNo = objDTO.VenueNo;
                    var venuebNo = objDTO.VenueBranchNo;
                    var visitno = objDTO.PatientVisitNo;
                    var format = objDTO.FileType;
                    var actualfilename = objDTO.actualFileName;
                    var manualfilename = objDTO.ManualFileName;
                    string folderName = venueNo + "\\" + venuebNo + "\\" + visitno + "\\" + visitId + "\\" + objDTO.docType;

                    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
                    string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";

                    string webRootPath = uplodpathinit;
                    string newPath = Path.Combine(webRootPath, folderName);
                    string[] files = Directory.GetFiles(newPath);
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.RemoveUploadFile", ExceptionPriority.High, ApplicationType.APPSERVICE, venueno, venuebno, 0);
            }
            return result;
        }

        [HttpGet]
        [Route("api/FrontOffice/GetClinicalHistory")]
        public List<ClinicalHistory> GetClinicalHistory(int venueNo, int venueBranchNo, int patientVisitNo)
        {
            List<ClinicalHistory> patientAssessment = new List<ClinicalHistory>();
            try
            {
                patientAssessment = _IFrontOfficeRepository.GetClinicalHistory(venueNo, venueBranchNo, patientVisitNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetAllPatientAssessments", ExceptionPriority.High, ApplicationType.APPSERVICE, venueNo, venueBranchNo, 0);
            }
            return patientAssessment;
        }

        [HttpPost]
        [Route("api/FrontOffice/InsertClinicalHistory")]
        public CommonAdminResponse InsertClinicalHistory(InsertClinicalHistory insertClinicalHistory)
        {
            CommonAdminResponse adminResponse = new CommonAdminResponse();
            try
            {
                adminResponse = _IFrontOfficeRepository.InsertClinicalHistory(insertClinicalHistory);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.InsertClinicalHistory", ExceptionPriority.High, ApplicationType.APPSERVICE, insertClinicalHistory.VenueNo, insertClinicalHistory.VenueBranchNo, insertClinicalHistory.PatientVisitNo);
            }
            return adminResponse;
        }

        [HttpPost]
        [Route("api/FrontOffice/getloyalcard")]
        public List<Tblloyal> getloyalcard(TblloyalReq req)
        {
            List<Tblloyal> Objresult = new List<Tblloyal>();
            try
            {
                Objresult = _IFrontOfficeRepository.getloyalcard(req).ToList();
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetCustomerDetailsbyName", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.venueNo, req.venuebranchno, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/FrontOffice/GetVisitPatternID")]
        public PatientVisitPatternIDGenRes GetVisitPatternID(PatientVisitPatternIDGenReq req)
        {
            PatientVisitPatternIDGenRes Objresult = new PatientVisitPatternIDGenRes();
            try
            {
                Objresult = _IFrontOfficeRepository.GetVisitPatternID(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetVisitPatternID", ExceptionPriority.Medium, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return Objresult;
        }
        [HttpPost]
        [Route("api/FrontOffice/InsertPreBookingDetails")]
        public PreBookingtResponse InsertPreBookingDetails(PreBookingtRequest objDTO)
        {
            PreBookingtResponse resultPreBookingtResponse = new PreBookingtResponse();
            try
            {
                resultPreBookingtResponse = _IFrontOfficeRepository.InsertPreBookingDetails(objDTO);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientReportController.InsertPreBookingDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, objDTO.VenueNo, objDTO.VenueBranchNo, 0);
            }
            return resultPreBookingtResponse;
        }
        [HttpPost]
        [Route("api/FrontOffice/GetPreBookingDetails")]
        public List<PreBookingtDTO> GetPreBookingDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PreBookingtDTO> lstPreBookingtDTO = new List<PreBookingtDTO>();
            try
            {
                lstPreBookingtDTO = _IFrontOfficeRepository.GetPreBookingDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetPreBookingDetails", ExceptionPriority.High, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lstPreBookingtDTO;
        }

        [HttpPost]
        [Route("api/FrontOffice/GetLoyaltyCardPatternID")]
        public AutoLoyaltyIDGenResponse GetLoyaltyCardPatternID(AutoLoyaltyIDGenRequest req)
        {
            AutoLoyaltyIDGenResponse Objresult = new AutoLoyaltyIDGenResponse();
            try
            {
                Objresult = _IFrontOfficeRepository.GetLoyaltyCardPatternID(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FrontOfficeController.GetLoyaltyCardPatternID", ExceptionPriority.High, ApplicationType.APPSERVICE, req.VenueNo, req.VenueBranchNo, 0);
            }
            return Objresult;
        }
    }
}
