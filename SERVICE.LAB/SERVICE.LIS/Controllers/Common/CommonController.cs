using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Dev.Repository;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonRepository _CommonRepository;
        public CommonController(ICommonRepository noteRepository)
        {
            _CommonRepository = noteRepository;
        }

        #region CommonSearch 
        [HttpPost]
        [Route("api/Common/CommonSearch")]
        public List<LstSearch> CommonSearch(RequestCommonSearch req)
        {
            List<LstSearch> lst = new List<LstSearch>();
            try
            {
                lst = _CommonRepository.CommonSearch(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.CommonSearch", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        #region Send Message 
        [HttpPost]
        [Route("api/Common/SendMessage")]
        public NotificationResponse SendMessage(NotificationDto Notification)
        {
            NotificationResponse result = new NotificationResponse();
            try
            {
                result = _CommonRepository.SendMessage(Notification);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.SendMessage", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }

        #endregion

        #region CommonSearchMaster 
        [HttpPost]
        [Route("api/Common/CommonSearchMaster")]
        public List<LstMasterSearch> CommonSearchMaster(RequestCommonMasterSearch req)
        {
            List<LstMasterSearch> lst = new List<LstMasterSearch>();
            try
            {
                lst = _CommonRepository.CommonSearchMaster(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.CommonSearchMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        #endregion

        #region Getcurrenttime 
        [HttpGet]
        [Route("api/Common/Getcurrenttime")]
        public TimeDto Getcurrenttime(int VenueNo, int VenueBranchNo, string Timezone)
        {
            TimeDto result = new TimeDto();
            try
            {
                result.currenttime = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.Getcurrenttime", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return result;
        }
        #endregion

        #region CommonFilter 
        [HttpGet]
        [Route("api/Common/GetCommonFilter")]
        public List<LstFilter> CommonFilter(string filterKey, int VenueNo, int VenueBranchNo)
        {
            List<LstFilter> lst = new List<LstFilter>();
            try
            {
                lst = _CommonRepository.CommonFilter(filterKey, VenueNo, VenueBranchNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.GetCommonFilter", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        #region gethelper 
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/gethelper")]
        public string gethelper(LstFilter plaintext)
        {
            string result = string.Empty;
            try
            {
                if (plaintext.filterCode == "E")
                    result = EncryptionHelper.Encrypt(plaintext.filterValue);
                else if (plaintext.filterCode == "D")
                    result = EncryptionHelper.Decrypt(plaintext.filterValue);
                else
                    result = "something went wrong";
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.gethelper", ExceptionPriority.Low, ApplicationType.APPSERVICE, 0, 0, 0);
            }
            return result;
        }
        #endregion

        #region Critical Result Notification        
        [HttpPost]
        [Route("api/Common/getCriticalResultNotify")]
        public List<GetCriticalResultsResponse> GetCriticalResultNotify(GetCriticalResultsReq req)
        {
            List<GetCriticalResultsResponse> lst = new List<GetCriticalResultsResponse>();
            try
            {
                lst = _CommonRepository.GetCriticalResultNotify(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.GetCriticalResultNotify", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        [HttpPost]
        [Route("api/Common/getApprovallist")]
        public List<ApprovalResponse> getApprovallist(ApprovalRequestDTO req)
        {
            List<ApprovalResponse> lst = new List<ApprovalResponse>();
            try
            {
                lst = _CommonRepository.getApprovallist(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.getApprovallist", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        [HttpPost]
        [Route("api/Common/saveCriticalResultNotify")]
        public ActionResult<SaveCriticalResultNotifyRes> SaveCriticalResultNotify(SaveCriticalResultsReq req)
        {
            SaveCriticalResultNotifyRes outs = new SaveCriticalResultNotifyRes();
            try
            {
                var _errormsg = UserValidation.SaveCriticalResultNotify(req);
                if (!_errormsg.status)
                {
                    outs = _CommonRepository.SaveCriticalResultNotify(req);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.SaveCriticalResultNotify", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return Ok(outs);
        }
        [HttpGet]
        [Route("api/commmon/GetApprovalHistory")]
        public List<ApprovalHistory> GetApprovalHistory(int oldno, int newno, int type, int VenueNo, int VenueBranchNo, int UserNo)
        {
            List<ApprovalHistory> lstAuditLogDTO = new List<ApprovalHistory>();
            try
            {
                lstAuditLogDTO = _CommonRepository.GetApprovalHistory(oldno, newno, type, VenueNo, VenueBranchNo, UserNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.GetApprovalHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, UserNo);
            }
            return lstAuditLogDTO;
        }
        #endregion
        
        #region transction split report servie
        [HttpPost]
        [Route("api/Common/GetTransactionSplitDetails")]
        public List<ResTransactionSplit> GetTransactionSplitDetails(ReqTransactionSplit req)
        {
            List<ResTransactionSplit> lst = new List<ResTransactionSplit>();
            try
            {
                lst = _CommonRepository.GetTransactionSplitDetails(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.CommonSearch", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Common/GetTransactionSplitDetailsById")]
        public List<ResTransactionSplitById> GetTransactionSplitDetailsByID(ReqTransactionSplit req)
        {
            List<ResTransactionSplitById> lst = new List<ResTransactionSplitById>();
            try
            {
                lst = _CommonRepository.GetTransactionSplitDetailsById(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.CommonSearch", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return lst;
        }
        #endregion

        [CustomAuthorize("LIMSDEFAULT")]
        [HttpGet]
        [Route("api/commmon/GetPortalUrl")]
        public FetchPortalResponse GetPortalUrl(Int16 VenueNo, string EntityType)
        {
            FetchPortalResponse response = new FetchPortalResponse();
            try
            {
                response = _CommonRepository.GetPortalUrl(VenueNo, EntityType);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonController.GetPortalUrl", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, 0, 0);
            }
            return response;
        }
    }
}