using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using System.Xml.Linq;
using Serilog;
using Microsoft.Identity.Client;
using RtfPipe.Tokens;
using Shared;
using DEV.Model.Sample;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private IConfiguration _config;
        public CommonRepository(IConfiguration config) { _config = config; }

        public List<LstSearch> CommonSearch(RequestCommonSearch req)
        {
            List<LstSearch> lst = new List<LstSearch>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", req.pagecode);
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchno", req.viewvenuebranchno);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    lst = context.CommonSearch.FromSqlRaw(
                    "Execute dbo.pro_CommonSearchTransaction @pagecode, @viewvenuebranchno, @searchby, @searchtext, @venueno, @venuebranchno, @userno",
                    _pagecode, _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.CommonSearch - (SearchBy, PageCode : " + req.searchby.ToString() + ", " + req.pagecode + ")", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }

        public NotificationResponse SendMessage(NotificationDto Notification)
        {
            NotificationResponse result = new NotificationResponse();
            try
            {
                XElement XMLMessageNode = new XElement("MessageQueue", Notification?.MessageItem?.Select(kv => new XElement("Content",
                new XAttribute("key", kv.Key), new XAttribute("value", kv.Value))));

                XElement XMLAttachmentNode = null;
                if (Notification != null)
                {
                    if (Notification.IsAttachment)
                    {
                        XMLAttachmentNode = new XElement("AttachmentXML", Notification?.AttachmentItem?.Select(kv => new XElement("AttachmentContent",
                        new XAttribute("key", kv.Key), new XAttribute("value", kv.Value))));
                    }
                    else
                    {
                        XMLAttachmentNode = new XElement("EmptyElement");
                    }
                }
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TemplateKey = new SqlParameter("TemplateKey", Notification?.TemplateKey);
                    var _MessageType = new SqlParameter("MessageType", Notification?.MessageType);
                    var _Address = new SqlParameter("Address", Notification?.Address.ValidateEmpty());
                    var _CCAddress = new SqlParameter("CCAddress", Notification?.CCAddress.ValidateEmpty());
                    var _BCCAddress = new SqlParameter("BCCAddress", Notification?.BCCAddress.ValidateEmpty());
                    var _MessageXML = new SqlParameter("MessageXML", XMLMessageNode?.ToString());
                    var _IsAttachment = new SqlParameter("IsAttachment", Notification?.IsAttachment);
                    var _MessageAttachmentXML = new SqlParameter("MessageAttachmentXML", XMLAttachmentNode?.ToString());
                    var _ScheduleTime = new SqlParameter("ScheduleTime", DateTime.Now);
                    var _venueno = new SqlParameter("VenueNo", Notification?.VenueNo);
                    var _venuebranchno = new SqlParameter("VenueBranchNo", Notification?.VenueBranchNo);
                    var _userno = new SqlParameter("UserNo", Notification?.UserNo);
                    var _patientVisitNo = new SqlParameter("PatientVisitNo", Notification?.PatientVisitNo);
                    var _clientNo = new SqlParameter("CustomerNo", Notification?.ClientNo);

                    result = context.Notification.FromSqlRaw(
                    "Execute dbo.Pro_PushMessage " +
                    "@TemplateKey, @MessageType, @Address, @CCAddress, @BCCAddress, @MessageXML," +
                    "@IsAttachment, @MessageAttachmentXML, @ScheduleTime, @VenueNo, @VenueBranchNo, @UserNo, @PatientVisitNo, @CustomerNo",
                    _TemplateKey, _MessageType, _Address, _CCAddress, _BCCAddress, _MessageXML, _IsAttachment, _MessageAttachmentXML,
                    _ScheduleTime, _venueno, _venuebranchno, _userno, _patientVisitNo, _clientNo).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.SendMessage-TemplateKey/MessageType/Address" + Notification?.TemplateKey + "/" + Notification?.MessageType + "/" + Notification?.Address, ExceptionPriority.Low, ApplicationType.REPOSITORY,
                   0, 0, 0);
            }
            return result;
        }

        public List<LstMasterSearch> CommonSearchMaster(RequestCommonMasterSearch req)
        {
            List<LstMasterSearch> lst = new List<LstMasterSearch>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _viewvenuebranchno = new SqlParameter("viewvenuebranchNo", req.viewvenuebranchno);
                    var _searchby = new SqlParameter("searchby", req.searchby);
                    var _searchtext = new SqlParameter("searchtext", req.searchtext);
                    var _venueno = new SqlParameter("venueno", req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req.venuebranchno);
                    var _userno = new SqlParameter("userno", req.userno);

                    lst = context.CommonMasterSearch.FromSqlRaw(
                    "Execute dbo.pro_CommonSearchMaster @viewvenuebranchno, @searchby, @searchtext, @venueno, @venuebranchno, @userno",
                    _viewvenuebranchno, _searchby, _searchtext, _venueno, _venuebranchno, _userno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.CommonSearchMaster - " + req.searchby, ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        public List<LstFilter> CommonFilter(string filterKey, int venueNo, int venueBranchNo)
        {
            List<LstFilter> lst = new List<LstFilter>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _filterKey = new SqlParameter("FilterKey", filterKey.ValidateEmpty());

                    lst = context.CommonFilter.FromSqlRaw(
                    "Execute dbo.pro_CommonFilter @FilterKey", _filterKey).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.CommonFilter - " + filterKey, ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        
        #region Critical Result Notification
        public List<GetCriticalResultsResponse> GetCriticalResultNotify(GetCriticalResultsReq req)
        {
            List<GetCriticalResultsResponse> lst = new List<GetCriticalResultsResponse>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", req.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo", req.userNo);
                    var _pageCode = new SqlParameter("PageCode", req.pageCode);

                    lst = context.GetCriticalResultNotifys.FromSqlRaw(
                    "Execute dbo.Pro_GetCriticalResultDetailsForUser @VenueNo, @venuebranchno, @UserNo, @pageCode",
                    _venueNo, _venueBranchNo, _userNo, _pageCode).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.GetCriticalResultNotify", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;
        }
        public List<ApprovalResponse> getApprovallist(ApprovalRequestDTO req)
        {

            List<ApprovalResponse> lst = new List<ApprovalResponse>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", req.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo", req.userNo);
                    var _pageIndex = new SqlParameter("PageIndex", req.pageIndex);
                    
                    lst = context.GetApprovalRequestData.FromSqlRaw(
                    "Execute dbo.Pro_GetApprovalList @VenueNo, @venuebranchno, @UserNo, @PageIndex",
                    _venueNo, _venueBranchNo, _userNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.getApprovallist", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return lst;

        }
        public SaveCriticalResultNotifyRes SaveCriticalResultNotify(SaveCriticalResultsReq req)
        {
            SaveCriticalResultNotifyRes outs = new SaveCriticalResultNotifyRes();
            try
            {
                CommonHelper commonUtility = new CommonHelper();
                string savedata = string.Empty;
                if (req.lstSaveData != null && req.lstSaveData.Count > 0)
                {
                    savedata = commonUtility.ToXML(req.lstSaveData);
                }
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", req.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo", req.userNo);
                    var _modifyUserNo = new SqlParameter("ModifyUserNo", req.modifyUserNo);
                    var _data = new SqlParameter("Data", savedata);

                    var result = context.SaveCriticalResultNotifys.FromSqlRaw(
                    "Execute dbo.Pro_InsertCriticalResultNotifyForUser @VenueNo,@venuebranchno,@UserNo,@ModifyUserNo,@Data",
                    _venueNo, _venueBranchNo, _userNo, _modifyUserNo, _data).ToList();
                    
                    outs.oStatus = result != null && result.Count > 0 && result[0].oStatus != null ? result[0].oStatus : 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.SaveCriticalResultNotify", ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return outs;
        }
        public List<ApprovalHistory> GetApprovalHistory(int oldno, int newno, int type, int VenueNo, int VenueBranchNo,int UserNo)
        {

            List<ApprovalHistory> lst = new List<ApprovalHistory>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _oldno = new SqlParameter("oldno", oldno);
                    var _newno = new SqlParameter("newno", newno);
                    var _type = new SqlParameter("type", type);
                    var _venueNo = new SqlParameter("VenueNo", VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo);
                    var _userNo = new SqlParameter("UserNo", UserNo);

                    lst = context.GetApprovalHistory.FromSqlRaw(
                    "Execute dbo.Pro_GetApprovalHistory @oldno, @newno, @type, @VenueNo, @venuebranchno, @UserNo",
                    _oldno, _newno, _type,  _venueNo, _venueBranchNo, _userNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex,"CommonRepository.GetApprovalHistory", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, UserNo);
            }
            return lst;
        }
        public List<ResTransactionSplit> GetTransactionSplitDetails(ReqTransactionSplit req)
        {
            List<ResTransactionSplit> lst = new List<ResTransactionSplit>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", req.type);
                    var _action = new SqlParameter("Action", req.action);
                    var _fromdate = new SqlParameter("FromDate", req.fromdate);
                    var _toDate = new SqlParameter("ToDate", req.todate);
                    var _refTypeNo = new SqlParameter("ReferralTypeNo", req.refferalTypeNo);
                    var _refferalNo = new SqlParameter("ReferralNo", req.refferalNo);
                    var _marketingNo = new SqlParameter("MarketingNo", req.marketingNo);
                    var _riderNo = new SqlParameter("RiderNo", req.riderNo);
                    var _billUserNo = new SqlParameter("BillUserNo", req.billUserNo);
                    var _ViewVenueBranchNo = new SqlParameter("ViewVenueBranchNo", req.viewVenueBranchNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.userNo);
                    var _DeptNo = new SqlParameter("DeptNo", req.deptNo);
                    var _MainDeptNo = new SqlParameter("MainDeptNo", req.mainDeptNo);
                    var _Filter = new SqlParameter("Filter", req.filter);
                    var _ServiceType = new SqlParameter("ServiceType", req.serviceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", req.serviceNo);
                    var _clientType = new SqlParameter("clientType", req.clientType);

                    lst = context.GetTransactionCustomerDetails.FromSqlRaw(
                    "Execute dbo.pro_GetCustomerDetTransactionSplitServiceMIS @Type,@Action,@FromDate,@ToDate,@ReferralTypeNo,@ReferralNo,@MarketingNo,@RiderNo," +
                    "@BillUserNo,@ViewVenueBranchNo,@VenueNo,@VenueBranchNo,@UserNo,@DeptNo,@MainDeptNo,@Filter,@ServiceType,@ServiceNo,@clientType",
                    _type, _action, _fromdate, _toDate, _refTypeNo, _refferalNo, _marketingNo, _riderNo, _billUserNo, _ViewVenueBranchNo,
                    _VenueNo, _VenueBranchNo, _UserNo, _DeptNo, _MainDeptNo, _Filter, _ServiceType, _ServiceNo, _clientType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.GetTransactionSplitDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return lst;
        }
        public List<ResTransactionSplitById> GetTransactionSplitDetailsById(ReqTransactionSplit req)
        {
            List<ResTransactionSplitById> lst = new List<ResTransactionSplitById>();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _type = new SqlParameter("Type", req.type);
                    var _action = new SqlParameter("Action", req.action);
                    var _fromdate = new SqlParameter("FromDate", req.fromdate);
                    var _toDate = new SqlParameter("ToDate", req.todate);
                    var _refTypeNo = new SqlParameter("ReferralTypeNo", req.refferalTypeNo);
                    var _refferalNo = new SqlParameter("ReferralNo", req.refferalNo);
                    var _marketingNo = new SqlParameter("MarketingNo", req.marketingNo);
                    var _riderNo = new SqlParameter("RiderNo", req.riderNo);
                    var _billUserNo = new SqlParameter("BillUserNo", req.billUserNo);
                    var _ViewVenueBranchNo = new SqlParameter("ViewVenueBranchNo", req.viewVenueBranchNo);
                    var _VenueNo = new SqlParameter("VenueNo", req.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req.venueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", req.userNo);
                    var _DeptNo = new SqlParameter("DeptNo", req.deptNo);
                    var _MainDeptNo = new SqlParameter("MainDeptNo", req.mainDeptNo);
                    var _Filter = new SqlParameter("Filter", req.filter);
                    var _ServiceType = new SqlParameter("ServiceType", req.serviceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", req.serviceNo);
                    var _clientType = new SqlParameter("clientType", req.clientType);

                    lst = context.GetTransactionCustomerDetailsById.FromSqlRaw(
                    "Execute dbo.pro_GetPatientDetTransactionSplitServiceMIS @Type,@Action,@FromDate,@ToDate,@ReferralTypeNo,@ReferralNo,@MarketingNo,@RiderNo," +
                    "@BillUserNo,@ViewVenueBranchNo,@VenueNo,@VenueBranchNo,@UserNo,@DeptNo,@MainDeptNo,@Filter,@ServiceType,@ServiceNo,@clientType",
                    _type, _action, _fromdate, _toDate, _refTypeNo, _refferalNo, _marketingNo, _riderNo, _billUserNo, _ViewVenueBranchNo,
                    _VenueNo, _VenueBranchNo, _UserNo, _DeptNo, _MainDeptNo, _Filter, _ServiceType, _ServiceNo, _clientType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.GetTransactionSplitDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return lst;
        }
        #endregion

        public FetchPortalResponse GetPortalUrl(Int16 VenueNo, string EntityType)
        {

            FetchPortalResponse response = new FetchPortalResponse();
            try
            {
                using (var context = new CommonContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _EntityType = new SqlParameter("EntityType", EntityType);

                    response = context.GetPortalUrl.FromSqlRaw(
                    "Execute dbo.Pro_GetPortalUrl @VenueNo, @EntityType", _VenueNo, _EntityType).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonRepository.GetPortalUrl", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, 0, 0);
            }
            return response;
        }
    }
}
