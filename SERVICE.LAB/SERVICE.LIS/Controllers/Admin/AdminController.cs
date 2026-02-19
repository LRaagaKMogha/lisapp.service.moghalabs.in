using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Model.Admin;
using Microsoft.Extensions.Configuration;

namespace Service.API.SERVICE.Controllers.Admin
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
        public List<SearchVisitdetailsResponse> SearchVisit(DeleteVisitRequest RequestItem)
        {
            List<SearchVisitdetailsResponse> response = new List<SearchVisitdetailsResponse>();
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
            
            try
            {
                response = _adminRepository.SearchScrollText(reqItem);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminController.FetchScrollText-", ExceptionPriority.Medium, ApplicationType.APPSERVICE, reqItem.venueNo, reqItem.venueBranchNo, reqItem.userNo);
            }
            return response;
        }

        [HttpPost]
        [Route("api/Admin/DeleteHistory")]
        public List<Responsehistory> DeleteHistory(visitRequest obj)
        {
            List<Responsehistory> response = new List<Responsehistory>();
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
                MyDevException.Error(ex, "AdminRepository.UpdateVisitPaymentModes/VisitId-" + RequestItem.PatientVisitNo, ExceptionPriority.Medium, ApplicationType.APPSERVICE, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.UserID);
            }
            return response;
        }
        #endregion
    }
}