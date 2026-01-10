using Dev.IRepository.PatientInfo;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.EF.Common;
using DEV.Model.PatientInfo;
using DEV.Model.Sample;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Repository.PatientInfo
{
    public class PatientInfoRepository : IPatientInfoRepository
    {

        private IConfiguration _config;
        public PatientInfoRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Patient Info Details
        /// </summary>
        /// <returns></returns>
        public List<PatientInfoResponse> GetPatientInfoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoResponse> lstPatientInfoResponse = new List<PatientInfoResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _FilterCustomerNo = new SqlParameter("FilterCustomerNo", RequestItem.filterCustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _ServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem.orderStatus);
                    var _isSTATFilter = new SqlParameter("isSTATFilter", RequestItem.isSTATFilter);
                    var _loginType = new SqlParameter("loginType", RequestItem.loginType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _routeNo = new SqlParameter("RouteNo", RequestItem.routeNo);
                    var _maindeptNo = new SqlParameter("maindeptNo", RequestItem.maindeptNo);
                    var _MultiFieldsSearch = new SqlParameter("MultiFieldsSearch", RequestItem.MultiFieldsSearch);
                    var _MultiDeptNo = new SqlParameter("MultiDeptNo", RequestItem.multiDeptNo);

                    lstPatientInfoResponse = context.GetPatientInfoDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientInfo @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@CustomerNo,@PatientNo,@VisitNo,@RefferalType,@FilterCustomerNo,@PhysicianNo," +
                    "@DepartmentNo,@ServiceNo,@ServiceType,@OrderStatus,@isSTATFilter,@PageIndex,@loginType,@UserNo,@RouteNo,@maindeptNo, @MultiFieldsSearch,@MultiDeptNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _CustomerNo, _PatientNo, _VisitNo, _RefferalType, _FilterCustomerNo, _PhysicianNo, _DepartmentNo,
                    _ServiceNo, _ServiceType, _OrderStatus, _isSTATFilter, _pageIndex, _loginType, _userNo, _routeNo, _maindeptNo, _MultiFieldsSearch,_MultiDeptNo).ToList();                   

                    foreach(var patientinfo in lstPatientInfoResponse)
                    {
                        SetDocumentShowDetails(RequestItem, patientinfo, "UploadPathInit", "Document");
                        SetDocumentShowDetails(RequestItem, patientinfo, "ResultAckUpload", "SendOutDocument");
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetPatientInfoDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }
        public List<PatientInfoListResponse> GetPatientListDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoListResponse> lstPatientListInfoResponse = new List<PatientInfoListResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FROMDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _CustomerNo = new SqlParameter("CustomerNo", RequestItem.CustomerNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _FilterCustomerNo = new SqlParameter("FilterCustomerNo", RequestItem.filterCustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _ServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem.orderStatus);
                    var _isSTATFilter = new SqlParameter("isSTATFilter", RequestItem.isSTATFilter);
                    var _loginType = new SqlParameter("loginType", RequestItem.loginType);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _routeNo = new SqlParameter("RouteNo", RequestItem.routeNo);
                    var _maindeptNo = new SqlParameter("maindeptNo", RequestItem.maindeptNo);
                    var _MultiFieldsSearch = new SqlParameter("MultiFieldsSearch", RequestItem.MultiFieldsSearch);
                    var _pageCount = new SqlParameter("pageCount", RequestItem.pageCount);

                    lstPatientListInfoResponse = context.GetPatientListResponse.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientListInfo @FROMDate,@ToDate,@Type,@VenueNo,@VenueBranchNo,@CustomerNo,@PatientNo,@VisitNo,@RefferalType,@FilterCustomerNo,@PhysicianNo," +
                    "@DepartmentNo,@ServiceNo,@ServiceType,@OrderStatus,@isSTATFilter,@PageIndex,@loginType,@UserNo,@RouteNo,@maindeptNo, @MultiFieldsSearch,@pageCount",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _CustomerNo, _PatientNo, _VisitNo, _RefferalType, _FilterCustomerNo, _PhysicianNo, _DepartmentNo,
                    _ServiceNo, _ServiceType, _OrderStatus, _isSTATFilter, _pageIndex, _loginType, _userNo, _routeNo, _maindeptNo, _MultiFieldsSearch, _pageCount).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetPatientInfoDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientListInfoResponse;
        }

        private void SetDocumentShowDetails(CommonFilterRequestDTO RequestItem, PatientInfoResponse patientinfo,string UploadPath,string DocumentName)
        {
            MasterRepository _IMasterRepository = new MasterRepository(_config);
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            objAppSettingResponse = new AppSettingResponse();
            string AppUploadPathInit = UploadPath;//"UploadPathInit";
            objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppUploadPathInit);
            string uplodpathinit = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                ? objAppSettingResponse.ConfigValue : "";

            string Pathinit = uplodpathinit;
            var visitId = patientinfo?.VisitId;
            var venueNo = RequestItem.VenueNo;
            var venuebNo = RequestItem.VenueBranchNo;
            var serviceno = patientinfo.TestNo;
            var visitnumber = patientinfo?.VisitNo.ToString();

            string folderName = string.Empty;
            if (DocumentName == "Document")
            {
                folderName = venueNo + "\\" + venuebNo + "\\" + visitId;
            }
            else
            {
                if (UploadPath == "ResultAckUpload")
                {
                    folderName = venueNo  + "\\" + visitnumber + "\\" + serviceno;
                }
                else
                {
                    folderName = venueNo + "\\" + venuebNo + "\\" + visitId + "\\" + serviceno;
                }
            }
            string newPath = Path.Combine(Pathinit, folderName);
            if (Directory.Exists(newPath))
            {
                string[] filePaths = Directory.GetFiles(newPath);
                if (filePaths != null && filePaths.Length > 0)
                {                    
                    if (DocumentName == "Document")
                    {
                        patientinfo.IsShowDocument = true;
                    }
                    else
                    {
                        patientinfo.IsShowSendOutDocument = true;
                    }
                }
            }
        }

        public List<CustomSearchResponse> GetCustomSearch(CommonSearchRequest searchRequest)
        {
            List<CustomSearchResponse> lstCustomSearch = new List<CustomSearchResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", searchRequest.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", searchRequest.VenueBranchNo);
                    var _SearchKey = new SqlParameter("SearchKey", searchRequest.SearchKey);

                    lstCustomSearch = context.GetCustomSearchDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetCommonSearch @VenueNo,@VenueBranchNo,@SearchKey",
                    _SearchKey, _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetCustomSearch", ExceptionPriority.Low, ApplicationType.REPOSITORY, searchRequest.VenueNo, searchRequest.VenueBranchNo, 0);
            }
            return lstCustomSearch;
        }

        /// <summary>
        /// Patient Report 
        /// </summary>
        /// <param name="VisitNo"></param>
        /// <returns></returns>
        public async Task<ReportOutput> PrintPatientReport(ReportRequestDTO requestDTO)
        {
            ReportOutput result = new ReportOutput();
            try
            {
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();
                objdictionary.Add("PatientVisitNo", requestDTO.visitNo.ToString());
                objdictionary.Add("UserNo", requestDTO.userNo.ToString());
                objdictionary.Add("VenueNo", requestDTO.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", requestDTO.VenueBranchNo.ToString());
                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();
                
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == ReportKey.PATIENTREPORT && x.VenueNo == requestDTO.VenueNo
                    && x.VenueBranchNo == requestDTO.VenueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster?.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster?.ExportPath);
                    }
                }
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster?.ProcedureName);
                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster?.ReportPath;
                objitem.ExportPath = tblReportMaster?.ExportPath + Guid.NewGuid().ToString("N").Substring(0, 6) + ".pdf";
                objitem.ExportFormat = FileFormat.PDF;
                string ReportParam = JsonConvert.SerializeObject(objitem);
                //
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppReportServiceURL = "ReportServiceURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                string dpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                string filename = await ExportReportService.ExportPrint(ReportParam, dpath);
                result.PatientExportFile = tblReportMaster?.ExportURL + filename;
                result.PatientExportFolderPath = objitem.ExportPath;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.PrintPatientReport", ExceptionPriority.High, ApplicationType.REPOSITORY, requestDTO.VenueNo, requestDTO.VenueBranchNo, requestDTO.userNo);
            }
            return result;
        }

        public EditPatientResponse UpdatePatientDetails(EditPatientRequest editPatientRequest)
        {
            CommonHelper commonUtility = new CommonHelper();
            EditPatientResponse editPatientResponse = new EditPatientResponse();
            try
            {
                string editDetails = commonUtility.ToXML(editPatientRequest);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _PaymentDetails = new SqlParameter("PatientDetails", editDetails);
                    var _VenueNo = new SqlParameter("VenueNo", editPatientRequest.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", editPatientRequest.venueBranchNo);
                    var _UserID = new SqlParameter("UserNo", editPatientRequest.userNo);
                    var dbResponse = context.UpdatePatientDetailsDTO.FromSqlRaw(
                    
                    "Execute dbo.pro_UpdateEditPatientDetails @VenueNo,@VenueBranchNo,@UserNo,@PatientDetails",
                    _VenueNo, _VenueBranchNo, _UserID, _PaymentDetails).ToList();
                    editPatientResponse = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.UpdatePatientDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, editPatientRequest.venueNo, editPatientRequest.venueBranchNo, editPatientRequest.userNo);
            }
            return editPatientResponse;
        }

        //Patient Visit History for the specific patient no
        public List<PatientInfoResponse> GetPatientVisitHistory(CommonFilterRequestDTO RequestItem)
        {
            List<PatientInfoResponse> lstPatientInfoResponse = new List<PatientInfoResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venuebranchno", RequestItem.VenueBranchNo);
                    var _PatientNo = new SqlParameter("patientno", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("patientvisitno", RequestItem.visitNo);
                    var _userNo = new SqlParameter("userno", RequestItem.userNo);
                    var _customerNo = new SqlParameter("customerno", RequestItem.CustomerNo);

                    lstPatientInfoResponse = context.GetPatientInfoDTO.FromSqlRaw(
                    "Execute dbo.pro_GetPatientVisitHistory @venueno,@venuebranchno,@userno,@patientvisitno,@patientno,@customerno",
                    _VenueNo, _VenueBranchNo, _userNo, _VisitNo, _PatientNo, _customerNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetPatientVisitHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }

        public List<ReasonDetailsResponse> GetServiceRejectReason(ReasonDetailsRequest RequestItem)
        {
            List<ReasonDetailsResponse> lstPatientReason = new List<ReasonDetailsResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _ViewVenueBranchNo = new SqlParameter("ViewVenueBranchNo", RequestItem.ViewVenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", RequestItem.UserNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem.ServiceNo);
                    var _ServieType = new SqlParameter("ServiceType", RequestItem.ServieType);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", RequestItem.VisitNo);
                    var _OrderDetailsNo = new SqlParameter("OrderDetailsNo", RequestItem.OrderDetailsNo);
                    var _OrderListNo = new SqlParameter("OrderListNo", RequestItem.OrderListNo);
                    var _PageCode = new SqlParameter("PageCode", RequestItem.PageCode);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _SearchStatus = new SqlParameter("SearchStatus", RequestItem.SearchStatus);

                    lstPatientReason = context.GetServiceRejectReason.FromSqlRaw(
                    "Execute dbo.pro_GetRejectedReasonByVisit @PageCode,@ViewVenueBranchNo,@VenueNo,@VenueBranchNo,@UserNo,@ServiceNo,@ServiceType,@PatientVisitNo,@OrderDetailsNo,@OrderListNo,@PatientNo,@SearchStatus",
                    _PageCode, _ViewVenueBranchNo, _VenueNo, _VenueBranchNo, _UserNo, _ServiceNo, _ServieType, _PatientVisitNo, _OrderDetailsNo, _OrderListNo, _PatientNo, _SearchStatus).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetServiceRejectReason", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.UserNo);
            }
            return lstPatientReason;
        }
        public int UpdateMasterData(SyncMasterRequestDTO requestDTO)
        {
            int result = 0;
            CommonHelper commonUtility = new CommonHelper();
            string serviceXML = commonUtility.ToXML(requestDTO.serviceList);

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", requestDTO.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", requestDTO.venueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", requestDTO.userNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", requestDTO.visitNo);
                    var _SyncType = new SqlParameter("SyncType", requestDTO.syncType);
                    var _ServiceXML = new SqlParameter("ServiceXML", serviceXML);

                    var data = context.UpdateBillMasterData.FromSqlRaw(
                    "Execute dbo.pro_UpdateBillMasterdata @VenueNo, @VenueBranchNo, @UserNo, @PatientVisitNo, @SyncType, @ServiceXML",
                    _VenueNo, _VenueBranchNo, _UserNo, _PatientVisitNo, _SyncType, _ServiceXML).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.UpdateMasterData", ExceptionPriority.High, ApplicationType.REPOSITORY, requestDTO.venueNo, requestDTO.venueBranchNo, requestDTO.userNo);
            }
            return result;
        }

        public List<PatientsMasterResponse> GetPatientsMaster(PatientsMasterRequest RequestItem)
        {
            List<PatientsMasterResponse> lstPatientMaster = new List<PatientsMasterResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.PageIndex);
                    var _IsPatientMaster = new SqlParameter("IsPatientMaster", RequestItem.IsPatientMaster);

                    lstPatientMaster = context.GetPatientMaster.FromSqlRaw(
                    "Execute dbo.pro_GetPatientMaster @VenueNo,@VenueBranchNo,@PatientNo,@PageIndex,@IsPatientMaster",
                    _VenueNo, _VenueBranchNo, _PatientNo, _PageIndex, _IsPatientMaster).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetPatientsMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lstPatientMaster;
        }

        public int SavePatientsMaster(PatientsMasterSave RequestItem)
        {
            int patientno = 0;

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string Password = Guid.NewGuid().ToString("N").Substring(0, 7);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _IsPatientMaster = new SqlParameter("IsPatientMaster", RequestItem.IsPatientMaster);
                    var _TitleCode = new SqlParameter("TitleCode", RequestItem.TitleCode);
                    var _FirstName = new SqlParameter("FirstName", RequestItem.FirstName);
                    var _MiddleName = new SqlParameter("MiddleName", RequestItem.MiddleName);
                    var _LastName = new SqlParameter("LastName", RequestItem.LastName);
                    var _DOB = new SqlParameter("DOB", RequestItem.DOB);
                    var _Gender = new SqlParameter("Gender", RequestItem.Gender);
                    var _Age = new SqlParameter("Age", RequestItem.Age);
                    var _AgeMonths = new SqlParameter("AgeMonths", RequestItem.AgeMonths);
                    var _AgeDays = new SqlParameter("AgeDays", RequestItem.AgeDays);
                    var _MobileNumber = new SqlParameter("MobileNumber", RequestItem.MobileNumber);
                    var _WhatsappNumber = new SqlParameter("WhatsappNumber", RequestItem.WhatsappNumber);
                    var _LandlineNumber = new SqlParameter("LandlineNumber", RequestItem.LandlineNumber);
                    var _EmailID = new SqlParameter("EmailID", RequestItem.EmailID);
                    var _Address = new SqlParameter("Address", RequestItem.Address);
                    var _CountryNo = new SqlParameter("CountryNo", RequestItem.CountryNo);
                    var _StateNo = new SqlParameter("StateNo", RequestItem.StateNo);
                    var _CityNo = new SqlParameter("CityNo", RequestItem.CityNo);
                    var _Place = new SqlParameter("Place", RequestItem.Place);
                    var _Pincode = new SqlParameter("Pincode", RequestItem.Pincode);
                    var _RemarksHistory = new SqlParameter("RemarksHistory", RequestItem.RemarksHistory);
                    var _MaritalStatus = new SqlParameter("MaritalStatus", RequestItem.MaritalStatus);
                    var _Nationality = new SqlParameter("Nationality", RequestItem.NationalityNo);
                    var _BloodGroup = new SqlParameter("BloodGroup", RequestItem.BloodGroup);
                    var _venueno = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _UserID = new SqlParameter("UserID", RequestItem.UserID);
                    var _SaveType = new SqlParameter("SaveType", RequestItem.SaveType);
                    var _IsActive = new SqlParameter("IsActive", RequestItem.IsActive);
                    var _LoyalCardNo = new SqlParameter("LoyalCardNo", RequestItem.LoyalCardNo ?? (object)DBNull.Value);
                    var _Password = new SqlParameter("Pass", CommonSecurity.EncodePassword(Password, CommonSecurity.GeneratePassword(1)));

                    var lst = context.SavePatientsMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertPatientMaster @PatientNo,@IsPatientMaster,@TitleCode,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@Age,@AgeMonths,@AgeDays,@MobileNumber,@WhatsappNumber,@LandlineNumber,@EmailID,@Address,@CountryNo,@StateNo,@CityNo,@Place,@Pincode,@RemarksHistory,@MaritalStatus,@Nationality,@BloodGroup,@VenueNo,@VenueBranchNo,@UserID,@SaveType,@IsActive,@Pass,@LoyalCardNo",
                    _PatientNo, _IsPatientMaster, _TitleCode, _FirstName, _MiddleName, _LastName, _DOB, _Gender, _Age, _AgeMonths, _AgeDays, _MobileNumber, _WhatsappNumber, _LandlineNumber, _EmailID, _Address, _CountryNo, _StateNo, _CityNo, _Place, _Pincode, _RemarksHistory, _MaritalStatus, _Nationality, _BloodGroup, _venueno, _venuebranchno, _UserID, _SaveType, _IsActive, _Password, _LoyalCardNo).ToList();
                    
                    patientno = lst[0].PatientNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.SavePatientsMaster" + RequestItem.PatientNo.ToString(), ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.UserID);
            }
            return patientno;
        }
        public PatientmergeResponseDTO SavePatientMerge(PatientmergeDTO RequestItem)
        {
            PatientmergeResponseDTO objresult = new PatientmergeResponseDTO();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FpatientvisitNo = new SqlParameter("FpatientvisitNo", RequestItem.FpatientvisitNo);
                    var _TpatientvisitNo = new SqlParameter("TpatientvisitNo", RequestItem.TpatientvisitNo);
                    var _isfirstname = new SqlParameter("isfirstname", RequestItem.isfirstname);
                    var _islastname = new SqlParameter("islastname", RequestItem.islastname);
                    var _ismiddlename = new SqlParameter("ismiddlename", RequestItem.ismiddlename);
                    var _isdob = new SqlParameter("isdob", RequestItem.isdob);
                    var _isgender = new SqlParameter("isgender", RequestItem.isgender);
                    var _isidnumber = new SqlParameter("isidnumber", RequestItem.isidnumber);
                    var _ismobile = new SqlParameter("ismobile", RequestItem.ismobile);
                    var _isemail = new SqlParameter("isemail", RequestItem.isemail);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.venueno.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.venuebranchno.ToString());
                    var _UserNo = new SqlParameter("UserNo", RequestItem.userno);

                    context.UpdateMergePatient.FromSqlRaw(
                    "Execute dbo.Pro_UpdatePatientMerge @FpatientvisitNo,@TpatientvisitNo,@isfirstname,@islastname,@ismiddlename,@isdob,@isgender,@isidnumber,@ismobile,@isemail," +
                    "@VenueNo,@VenueBranchNo,@UserNo",
                    _FpatientvisitNo, _TpatientvisitNo, _isfirstname, _islastname, _ismiddlename, _isdob, _isgender, _isidnumber, _ismobile, _isemail, 
                    _VenueNo, _VenueBranchNo, _UserNo).AsEnumerable().FirstOrDefault();
                    
                    objresult.result = 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SavePatientMerge", ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.venueno, RequestItem.venuebranchno, RequestItem.userno);
            }
            return objresult;
        }

        public EditPatientResponse UpdateSampleDetails(List<EditSampleRequest> editSampleRequest)
        {
            CommonHelper commonUtility = new CommonHelper();
            EditPatientResponse editPatientResponse = new EditPatientResponse();
            try
            {
                string editDetails = commonUtility.ToXML(editSampleRequest);
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PaymentDetails = new SqlParameter("PatientDetails", editDetails);
                    var dbResponse = context.UpdatePatientDetailsDTO.FromSqlRaw(
                    
                    "Execute dbo.pro_UpdateEditSampleDetails @PatientDetails",
                    _PaymentDetails).ToList();
                    editPatientResponse = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.UpdateSampleDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, editSampleRequest[0].PatientVisitNo, editSampleRequest[0].VenueNo, editSampleRequest[0].UserNo);
            }
            return editPatientResponse;
        }
        public List<GetSampleResponse> GetPatientSampleInfo(GetSampleRequest RequestItem)
        {
            List<GetSampleResponse> lstPatientInfoResponse = new List<GetSampleResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", RequestItem.PatientVisitNo);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    
                    lstPatientInfoResponse = context.GetPatientSampleInfo.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientSampleInfo @PatientVisitNo,@VenueNo",
                    _PatientVisitNo, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.UpdateSampleDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.PatientVisitNo, RequestItem.VenueNo, 0);
            }
            return lstPatientInfoResponse;
        }

        public EditPatientResponseNew UpdateSampleDetailsNew(EditSampleRequestNew editSampleRequest)
        {
            CommonHelper commonUtility = new CommonHelper();
            EditPatientResponseNew editPatientResponse = new EditPatientResponseNew();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _specimenQty = new SqlParameter("specimenQty", editSampleRequest.specimenQty);
                    var _PatientSamplesNo = new SqlParameter("PatientSamplesNo", editSampleRequest.PatientSamplesNo);
                    var _UserNo = new SqlParameter("UserNo", editSampleRequest.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", editSampleRequest.VenueNo);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", editSampleRequest.PatientVisitNo);
                    var _SampleNo = new SqlParameter("SampleNo", editSampleRequest.SampleNo);
                    var _ContainerNo = new SqlParameter("ContainerNo", editSampleRequest.ContainerNo);
                    var _SampleSourceNo = new SqlParameter("SampleSourceNo", editSampleRequest.SampleSourceNo);
                    var _SampleSource = new SqlParameter("SampleSource", editSampleRequest.SampleSource);
                    var _ServiceNo = new SqlParameter("ServiceNo", editSampleRequest.ServiceNo);
                    var _BarcodeNo = new SqlParameter("BarcodeNo", editSampleRequest.BarcodeNo);
                    var _VisitID = new SqlParameter("VisitID", editSampleRequest.VisitID);
                    
                    var dbResponse = context.UpdatePatientDetailsDTONew.FromSqlRaw(
                    "Execute dbo.pro_UpdateEditSampleDetailsNew @specimenQty,@PatientSamplesNo,@UserNo,@VenueNo,@PatientVisitNo,@SampleNo,@ContainerNo,@SampleSourceNo,@SampleSource,@ServiceNo,@BarcodeNo,@VisitID",
                    _specimenQty, _PatientSamplesNo, _UserNo, _VenueNo, _PatientVisitNo, _SampleNo, _ContainerNo,_SampleSourceNo, _SampleSource,_ServiceNo,_BarcodeNo, _VisitID).ToList();
                    
                    editPatientResponse = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.UpdateSampleDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, editSampleRequest.PatientVisitNo, editSampleRequest.VenueNo, editSampleRequest.UserNo);
            }
            return editPatientResponse;
        }

        public List<GetPatientVisitActionHistoryResponse> GetPatientVisitActionHistory(CommonFilterRequestDTO RequestItem)
        {
            List<GetPatientVisitActionHistoryResponse> lstPatientInfoResponse = new List<GetPatientVisitActionHistoryResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueno", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venuebranchno", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("patientvisitno", RequestItem.visitNo);

                    lstPatientInfoResponse = context.GetPatientVisitActionHistory.FromSqlRaw(
                    "Execute dbo.pro_GetPatientVisitActionHistory @venueno,@venuebranchno,@patientvisitno",
                    _VenueNo, _VenueBranchNo, _VisitNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetPatientVisitActionHistory", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }
        public async Task<ReportOutputhc> PrintHcBill(ReportRequestDTO req)
        {
            ReportOutputhc result = new ReportOutputhc();
            try
            {
                Dictionary<string, string> objdictionary = new Dictionary<string, string>();

                objdictionary.Add("PatientVisitNo", req.visitNo.ToString());
                objdictionary.Add("UserNo", req.userNo.ToString());
                objdictionary.Add("VenueNo", req.VenueNo.ToString());
                objdictionary.Add("VenueBranchNo", req.VenueBranchNo.ToString());
                if (!string.IsNullOrEmpty(req.hcAppNo))
                {
                    objdictionary.Add("hcAppNo", req.hcAppNo.ToString());
                }

                ReportContext objReportContext = new ReportContext(_config.GetConnectionString(ConfigKeys.DefaultConnection));
                TblReportMaster tblReportMaster = new TblReportMaster();

                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (req.print == "" || req.print == null)
                    {
                        req.print = ReportKey.PATIENTHCBILL;
                    }
                    tblReportMaster = context.TblReportMaster.Where(x => x.ReportKey == req.print && x.VenueNo == req.VenueNo
                    && x.VenueBranchNo == req.VenueBranchNo).FirstOrDefault();
                    if (!Directory.Exists(tblReportMaster.ExportPath))
                    {
                        Directory.CreateDirectory(tblReportMaster.ExportPath);
                    }
                }
                DataTable datable = objReportContext.getdatatable(objdictionary, tblReportMaster.ProcedureName);

                ReportParamDto objitem = new ReportParamDto();
                objitem.datatable = CommonExtension.DatableToDicionary(datable);
                objitem.paramerter = objdictionary;
                objitem.ReportPath = tblReportMaster.ReportPath;
                objitem.ExportPath = tblReportMaster.ExportPath + req.visitNo + "_" + Guid.NewGuid().ToString("N").Substring(0, 4) + ".pdf";
                objitem.ExportFormat = FileFormat.PDF;
                string ReportParam = JsonConvert.SerializeObject(objitem);
                MasterRepository _IMasterRepository = new MasterRepository(_config);
                AppSettingResponse objAppSettingResponse = new AppSettingResponse();
                objAppSettingResponse = new AppSettingResponse();
                string AppReportServiceURL = "ReportServiceURL";
                objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppReportServiceURL);
                string dpath = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                        ? objAppSettingResponse.ConfigValue : "";
                string filename = await ExportReportService.ExportPrint(ReportParam, dpath);
                result.PatientExportFile = await CommonHelper.URLShorten(tblReportMaster.ExportURL + filename, _config.GetValue<string>(ConfigKeys.FireBaseAPIkey));
                result.ExportURL = tblReportMaster.ExportURL + filename;
                result.PatientExportFolderPath = objitem.ExportPath;
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.PatientinfoRepository.PrintHcBill/visitNo-" + req.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, req.userNo);
            }
            return result;
        }
        public List<GetHcDocumentsDetailsResponse> GetHcDocumentsDetails(GetHcDocumentsDetailsRequest requestItem)
        {
            List<GetHcDocumentsDetailsResponse> lstResponse = new List<GetHcDocumentsDetailsResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PatientVisitNo = new SqlParameter("@PatientVisitNo", requestItem.PatientVisitNo);
                    var _VenueBranchNo = new SqlParameter("@VenueBranchNo", requestItem.VenueBranchNo);
                    var _VenueNo = new SqlParameter("@VenueNo", requestItem.VenueNo);

                    lstResponse = context.GetHcDocumentsDetails.FromSqlRaw("EXEC dbo.pro_GetHcDocumentsDetails @PatientVisitNo, @VenueBranchNo, @VenueNo",
                    _PatientVisitNo, _VenueBranchNo, _VenueNo)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GetHcDocumentsDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, requestItem.VenueNo, requestItem.VenueBranchNo, 0);
            }
            return lstResponse;
        }

        public List<PatientInfoeLabResponseDTO> GeteLabPatientInfoList(PatientInfoRequestDTO RequestItem)
        {
            List<PatientInfoeLabResponseDTO> lstPatientInfoResponse = new List<PatientInfoeLabResponseDTO>();
            try
            {
                using (var context = new eLabContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _PatientNo = new SqlParameter("PatientNo", RequestItem.PatientNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _maindeptNo = new SqlParameter("MainDeptNo", RequestItem.maindeptNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", RequestItem.departmentNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", RequestItem.serviceNo);
                    var _ServiceType = new SqlParameter("ServiceType", RequestItem.serviceType);
                    var _OrderStatus = new SqlParameter("OrderStatus", RequestItem.orderStatus);
                    var _isSTATFilter = new SqlParameter("isSTATFilter", RequestItem.isSTATFilter);
                    var _pageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _userNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _MultiFieldsSearch = new SqlParameter("MultiFieldsSearch", RequestItem.MultiFieldsSearch);
                    var _FilterBranch = new SqlParameter("FilterBranch", RequestItem.FilterBranch);

                    lstPatientInfoResponse = context.GeteLabPatientInfoDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientInfo_eLab " +
                    "@FromDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @PatientNo, @VisitNo, @PhysicianNo, @MainDeptNo, @DepartmentNo, " +
                    "@ServiceNo, @ServiceType, @OrderStatus, @isSTATFilter, @PageIndex, @UserNo, @MultiFieldsSearch, @FilterBranch",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _PatientNo, _VisitNo, _PhysicianNo, _maindeptNo, _DepartmentNo,
                    _ServiceNo, _ServiceType, _OrderStatus, _isSTATFilter, _pageIndex, _userNo, _MultiFieldsSearch, _FilterBranch).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientInfoRepository.GeteLabPatientInfoList", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }
    }
}
