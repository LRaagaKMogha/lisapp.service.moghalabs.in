using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.IRepository;
using Dev.Repository;
using DEV.Common;
using DEV.Model;
using DEV.Model.PatientInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using DEV.Model.Admin;
using Microsoft.Extensions.Configuration;

namespace DEV.API.SERVICE.Controllers.Admin
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _config;
        public AdminController(IAdminRepository adminRepository, IConfiguration config)
        {
            _adminRepository = adminRepository;
            _config = config;
        }
        
        [HttpPost]
        [Route("api/Admin/DeleteVisit")]
        public CommonAdminResponse DeleteVisitId(DeleteVisitRequest RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                response = _adminRepository.DeleteVisitId(RequestItem);               
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.DeleteVisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }
        
        [HttpPost]
        [Route("api/Admin/SearchVisit")]
        public List<SearchVisitDetailsResponse> SearchVisit(DeleteVisitRequest RequestItem)
        {
            List<SearchVisitDetailsResponse> response = new List<SearchVisitDetailsResponse>();
            try
            {
                response = _adminRepository.SearchVisitId(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.SearchVisit-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/UpdateCustomerDetails")]
        public CommonAdminResponse UpdateCustomerDetails(UpdateCustomerDetails RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                response = _adminRepository.UpdateCustomerDetails(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.UpdateCustomerDetails/refferalNo-" + RequestItem.refferalNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/SearchUpdateDates")]
        public List<SearchUpdateDatesResponse> SearchUpdateDates(DeleteVisitRequest RequestItem)
        {
            List<SearchUpdateDatesResponse> response = new List<SearchUpdateDatesResponse>();
            try
            {
                response = _adminRepository.SearchUpdateDates(RequestItem);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.SearchUpdateDates-" + RequestItem.VisitId, ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/UpdateOrderDates")]
        public CommonAdminResponse UpdateOrderDates(UpdateOrderDatesRequest RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                response = _adminRepository.UpdateOrderDates(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.UpdateOrderDates/visitId-" + RequestItem.VisitId, ExceptionPriority.Low, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/FetchScrollText")]
        public List<ResponseDataScrollText> GetScrollText(RequestDataScrollText reqItem)
        {
            List<ResponseDataScrollText> response = new List<ResponseDataScrollText>();
            MasterRepository _IMasterRepository = new MasterRepository(_config);            
            AppSettingResponse objAppSettingResponse = new AppSettingResponse();
            try
            {
                response = _adminRepository.SearchScrollText(reqItem);

                //string _CacheKey = CacheKeys.CommonMaster + "SCROLLTEXTINAPPFOOTER" + reqItem.venueNo + reqItem.venueBranchNo;
                //response = MemoryCacheRepository.GetCacheItem<List<ResponseDataScrollText>>(_CacheKey);
                //if (response == null)
                //{
                //    response = _adminRepository.SearchScrollText(reqItem);
                //    //
                //    objAppSettingResponse = new AppSettingResponse();
                //    string AppCacheMemoryTime = "CacheMemoryTime";
                //    objAppSettingResponse = _IMasterRepository.GetSingleAppSetting(AppCacheMemoryTime);                    
                //    int cachetime = objAppSettingResponse != null && objAppSettingResponse.ConfigValue != null && objAppSettingResponse.ConfigValue != ""
                //        ? Convert.ToInt32(objAppSettingResponse.ConfigValue) :0;

                //    MemoryCacheRepository.AddItem(_CacheKey, response, Convert.ToInt32(cachetime));
                //}
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.FetchScrollText-", ExceptionPriority.Medium, ApplicationType.APPSERVICE, reqItem.venueNo, reqItem.venueBranchNo, reqItem.userNo);
            }
            return response;
        }
        [HttpPost]
        [Route("api/Admin/DeleteHistory")]
        public List<responsehistory> DeleteHistory(visitRequest obj)
        {
            List<responsehistory> response = new List<responsehistory>();
            try
            {
                response = _adminRepository.DeleteHistory(obj);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.DeleteHistory-", ExceptionPriority.Medium, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return response;
        }

        #region Paymode changes
        [HttpPost]
        [Route("api/Admin/GetPaymentMode")]
        public List<PaymentMode> GetPaymentMode(GetPaymentModeRequest RequestItem)
        {
            List<PaymentMode> response = new List<PaymentMode>();
            try
            {
                response = _adminRepository.GetPaymentMode(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.GetPaymentMode/VisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/UpdateVisitPaymentModes")]
        public SavePaymentModeResponse UpdateVisitPaymentModes(SavePaymentModeRequest RequestItem)
        {
            SavePaymentModeResponse response = new SavePaymentModeResponse();
            try
            {
                response = _adminRepository.UpdateVisitPaymentModes(RequestItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.UpdateVisitPaymentModes/VisitId-" + RequestItem.PatientVisitNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, (int)RequestItem.UserID);
            }
            return response;
        }
        #endregion
    }
}