using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.FrontOffice.PatientDue;
using DEV.Model.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Dev.Repository
{
   public class PatientDueRepository: IPatientDueRepository
    {
        private IConfiguration _config;
        public PatientDueRepository(IConfiguration config) { _config = config; }

        public List<PatientDueResponse> GetDuePatientInfoDetails(CommonFilterRequestDTO RequestItem)
        {
            List<PatientDueResponse> lstPatientInfoResponse = new List<PatientDueResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _FromDate = new SqlParameter("FromDate", RequestItem.FromDate);
                    var _ToDate = new SqlParameter("ToDate", RequestItem.ToDate);
                    var _Type = new SqlParameter("Type", RequestItem.Type);
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", RequestItem.visitNo);
                    var _PageIndex = new SqlParameter("PageIndex", RequestItem.pageIndex);
                    var _CustomerNo = new SqlParameter("ClientNo", RequestItem.CustomerNo);
                    var _PhysicianNo = new SqlParameter("PhysicianNo", RequestItem.physicianNo);
                    var _RefferalType = new SqlParameter("RefferalType", RequestItem.refferalType);
                    var _PageCount = new SqlParameter("PageCount", RequestItem.pageCount);
                    var _UserNo = new SqlParameter("UserNo", RequestItem.userNo);

                    lstPatientInfoResponse = context.GetPatientDueInfoDTO.FromSqlRaw(
                    "Execute dbo.Pro_GetPatientDueInfo @FromDate, @ToDate, @Type, @VenueNo, @VenueBranchNo, @VisitNo, @PageIndex, @ClientNo, @PhysicianNo, " +
                    "@RefferalType, @PageCount, @userNo",
                    _FromDate, _ToDate, _Type, _VenueNo, _VenueBranchNo, _VisitNo,_PageIndex, _CustomerNo, _PhysicianNo, _RefferalType, _PageCount, _UserNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.GetDuePatientInfoDetails/visitNo : " + RequestItem.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return lstPatientInfoResponse;
        }
        public CreatePatientDueResponse InsertPatientDue(CreatePatientDueRequest createPatientDueRequest)
        {
            CommonHelper commonUtility = new CommonHelper();
            CreatePatientDueResponse patientDueResponse = new CreatePatientDueResponse();

            try
            {
                string payments = commonUtility.ToXML(createPatientDueRequest.payments);

                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {                    
                    var _PaymentDetails = new SqlParameter("PaymentDetails", payments);
                    var _VenueNo = new SqlParameter("VenueNo", createPatientDueRequest.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", createPatientDueRequest.venueBranchNo);
                    var _VisitNo = new SqlParameter("VisitNo", createPatientDueRequest.visitNo);                    
                    var _DiscountAmount = new SqlParameter("DiscountAmount", createPatientDueRequest.discountAmount);
                    var _discountDescription = new SqlParameter("DiscountDescription", createPatientDueRequest.discountDescription.ValidateEmpty());                    
                    var _PaidAmount = new SqlParameter("PaidAmount", createPatientDueRequest.paidAmount);
                    var _BalanceAmount = new SqlParameter("BalanceAmount", createPatientDueRequest.balanceAmount);
                    var _UserID = new SqlParameter("UserID", createPatientDueRequest.userID);
                    var _DiscountType = new SqlParameter("DiscountType", createPatientDueRequest.discountNo);
                    var _IsDiscountApprovalReq = new SqlParameter("IsDiscountApprovalReq", createPatientDueRequest.isDiscountApprovalReq);

                    var dbResponse  = context.InsertPatientDueDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertDueClearence @VenueNo, @VenueBranchNo, @VisitNo, @PaymentDetails, @DiscountAmount, @DiscountDescription, @PaidAmount, @BalanceAmount, @UserID, @DiscountType, @IsDiscountApprovalReq",
                    _VenueNo, _VenueBranchNo, _VisitNo, _PaymentDetails,  _DiscountAmount, _discountDescription, _PaidAmount, _BalanceAmount, _UserID, _DiscountType,_IsDiscountApprovalReq).ToList();
                    
                    patientDueResponse = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.InsertPatientDue/visitNo-" + createPatientDueRequest.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, createPatientDueRequest.venueNo, createPatientDueRequest.venueBranchNo, createPatientDueRequest.userID);
            }
            return patientDueResponse;
        }        

        public CancelVisit GetPatientCancelTestInfo(getrequest Req)
        {
            CancelVisit obj = new CancelVisit();
            List<CancelVisitTest> lstCancelVisitTest  = new List<CancelVisitTest>();

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _pagecode = new SqlParameter("pagecode", Req.pagecode);
                    var _venueno = new SqlParameter("venueno", Req.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", Req.venuebranchno);
                    var _userno = new SqlParameter("userno", Req.userno);
                    var _visitNo = new SqlParameter("visitno", Req.patientvisitno);
                    var _patientvisitno = new SqlParameter("patientvisitno", Req.patientvisitno);

                    var exists = context.GetBillInvoiceExists.FromSqlRaw(
                    "Execute dbo.Pro_CheckBillExistsInInvoice @VenueNo, @VenueBranchNo, @VisitNo",
                    _venueno, _venuebranchno, _visitNo).ToList();

                    obj.IsExistsInvoice = exists.FirstOrDefault().IsExists;

                    if (obj.IsExistsInvoice) { return obj; }

                    var dblCancelVisit = context.GetPatientCancelTestInfo.FromSqlRaw(
                    "Execute dbo.pro_GetPatientCancelTestInfo @pagecode, @venueno, @venuebranchno, @userno, @patientvisitno",
                    _pagecode, _venueno, _venuebranchno, _userno,_patientvisitno).ToList();

                    int patientVisitNo = 0;
                    foreach (var v in dblCancelVisit)
                    {
                        if (patientVisitNo != v.patientVisitNo)
                        {
                            patientVisitNo = v.patientVisitNo;
                            obj.patientVisitNo = v.patientVisitNo;
                            obj.patientId = v.patientId;
                            obj.fullName = v.fullName;
                            obj.visitId = v.visitId;
                            obj.visitDTTM = v.visitDTTM;
                            obj.referralType = v.referralType;
                            obj.referredBy = v.referredBy;
                            obj.cancelled = v.cancelled;
                            obj.patientBillNo = v.patientBillNo;
                            obj.grossAmount = v.grossAmount;
                            obj.discountAmount = v.discountAmount;
                            obj.netAmount = v.netAmount;
                            obj.collectedAmount = v.collectedAmount;
                            obj.dueAmount = v.dueAmount;
                            obj.preCancelAmount = v.cancelAmount;
                            obj.preRefundAmount = v.refundAmount;
                            obj.venueBranchName = v.venueBranchName;
                            obj.currDiscountAmount = v.currDiscountAmount;
                        }

                        CancelVisitTest objs = new CancelVisitTest();
                        objs.patientVisitNo = v.patientVisitNo;
                        objs.patientBillDetailsNo = v.patientBillDetailsNo;
                        objs.orderNo = v.orderNo;
                        objs.serviceType = v.serviceType;
                        objs.serviceNo = v.serviceNo;
                        objs.serviceName = v.serviceName;
                        objs.serviceGrossAmount = v.serviceGrossAmount;
                        objs.serviceDiscountAmount = v.serviceDiscountAmount;
                        objs.blServiceDiscountAmount = v.blServiceDiscountAmount;
                        objs.isPreCancelled = v.isCancelled;
                        objs.isCancelled = false;
                        objs.cancelReason = v.cancelReason;
                        lstCancelVisitTest.Add(objs);
                    }
                    obj.lstCancelVisitTest = lstCancelVisitTest;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.GetPatientCancelTestInfo/patientVisitNo : " + Req.patientvisitno, ExceptionPriority.High, ApplicationType.REPOSITORY, Req.venueno, Req.venuebranchno, Req.userno);
            }
            return obj;
        }

        public rtnCancelTest InsertCancelTest(CancelVisit Req)
        {
            rtnCancelTest obj = new rtnCancelTest();
            CommonHelper commonUtility = new CommonHelper();
            string cancelServiceXML = "";
            if (Req.lstCancelVisitTest.Count > 0)
            {
                cancelServiceXML = commonUtility.ToXML(Req.lstCancelVisitTest);
            }
            Req.lstCancelVisitTest.Clear();

            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _patientVisitNo = new SqlParameter("patientVisitNo", Req.patientVisitNo);
                    var _patientBillNo = new SqlParameter("patientBillNo", Req.patientBillNo);
                    var _dueAmount = new SqlParameter("dueAmount", Req.dueAmount);
                    var _cancelAmount = new SqlParameter("cancelAmount", Req.cancelAmount);
                    var _refundAmount = new SqlParameter("refundAmount", Req.refundAmount);
                    var _currentDiscountAmount = new SqlParameter("CurrentDiscountAmount", Req.currDiscountAmount);
                    var _cancelServiceXML = new SqlParameter("cancelServiceXML", cancelServiceXML);
                    var _venueNo = new SqlParameter("venueNo", Req.venueNo);
                    var _venueBranchBo = new SqlParameter("venueBranchBo", Req.venueBranchBo);
                    var _userNo = new SqlParameter("userNo", Req.userNo);

                    var lst = context.InsertCancelTest.FromSqlRaw(
                    "Execute dbo.pro_InsertCancelTest @patientVisitNo, @patientBillNo, @dueAmount, @cancelAmount, @refundAmount, @CurrentDiscountAmount, " +
                    "@cancelServiceXML, @venueNo, @venueBranchBo, @userNo",
                    _patientVisitNo, _patientBillNo, _dueAmount, _cancelAmount, _refundAmount, _currentDiscountAmount,
                    _cancelServiceXML, _venueNo, _venueBranchBo, _userNo).ToList();

                    obj = lst[0]; 
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.InsertCancelTest : PatientVisitNo - " + Req.patientVisitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, Req.venueNo, Req.venueBranchBo, Req.userNo);
            }
            return obj;
        }
        public CreatePatientDueResponse Insertbulkpatientdue(List<CreatePatientDueRequest> createPatientDueRequests)
        {
            CommonHelper commonUtility = new CommonHelper();
            CreatePatientDueResponse patientDueResponse = new CreatePatientDueResponse();

            try
            {
                string payments = commonUtility.ToXML(createPatientDueRequests?.FirstOrDefault()?.payments);
                string patientDetails = commonUtility.ToXML(createPatientDueRequests);

                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _PaymentDetails = new SqlParameter("PaymentDetails", payments);
                    var _VenueNo = new SqlParameter("VenueNo", createPatientDueRequests?.FirstOrDefault()?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", createPatientDueRequests?.FirstOrDefault()?.venueBranchNo);
                    var _PatientDetails = new SqlParameter("PatientDetails", patientDetails);
                    var _UserID = new SqlParameter("UserID", createPatientDueRequests?.FirstOrDefault()?.userID);

                    var dbResponse = context.InsertBulkPatientDueDTO.FromSqlRaw(
                    "Execute dbo.Pro_BulkInsertDueClearence @VenueNo, @VenueBranchNo, @PatientDetails, @PaymentDetails, @UserID",
                    _VenueNo, _VenueBranchNo, _PatientDetails, _PaymentDetails, _UserID).ToList();
                    
                    patientDueResponse = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
               MyDevException.Error(ex, "PatientDueRepository.Insertbulkpatientdue " + createPatientDueRequests?.FirstOrDefault()?.visitNo, ExceptionPriority.High, ApplicationType.REPOSITORY, createPatientDueRequests?.FirstOrDefault()?.venueNo, createPatientDueRequests?.FirstOrDefault()?.venueBranchNo, createPatientDueRequests?.FirstOrDefault()?.userID);
            }
            return patientDueResponse;
        }

        #region refund/cancel approval 
        public List<GetReqCancelResponse> GetRefundCancelRequest(GetReqCancelParam RequestItem)
        {
            List<GetReqCancelResponse> objresult = new List<GetReqCancelResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("venueNo", RequestItem.venueno);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", RequestItem.venuebranchno);
                    var _DidByUser = new SqlParameter("didByUser", RequestItem.didByUser == null ? 0 : RequestItem.didByUser);
                    var _IsApproved = new SqlParameter("isApproved", RequestItem.isApproved == null ? 0 : RequestItem.isApproved);
                    var _PageIndex = new SqlParameter("pageIndex", RequestItem.pageIndex);
                    var _Type = new SqlParameter("type", RequestItem.type);
                    var _Fromdate = new SqlParameter("fromdate", RequestItem.fromdate);
                    var _Todate = new SqlParameter("todate", RequestItem.todate);

                    objresult = context.GetRefundCancelRequest.FromSqlRaw(
                    "Execute dbo.pro_GetRefundCancelApproval @venueno, @venuebranchno, @didByUser, @isApproved, @pageIndex, @type, @fromdate, @toDate",
                   _VenueNo, _VenueBranchNo, _DidByUser, _IsApproved, _PageIndex, _Type, _Fromdate, _Todate).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.GetRefundCancelRequest", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.venueno, RequestItem.venuebranchno, 0);
            }
            return objresult;
        }
        public UpdateReqCancelResponse ApproveRefundCancel(UpdateReqCancelParam RequestItem)
        {
            UpdateReqCancelResponse objresult = new UpdateReqCancelResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _refundCancelLogNo = new SqlParameter("refundCancelLogNo", RequestItem.refundCancelLogNo);
                    var _patientVisitNo = new SqlParameter("patientVisitNo", RequestItem.patientVisitNo);
                    var _patientBillNo = new SqlParameter("patientBillNo", RequestItem.patientBillNo);
                    var _patientBillDetailsNo = new SqlParameter("patientBillDetailsNo", RequestItem.patientBillDetailsNo);
                    var _orderNo = new SqlParameter("orderNo", RequestItem.orderNo);
                    var _orderListNo = new SqlParameter("orderListNo", RequestItem.orderListNo);
                    var _venueNo = new SqlParameter("venueNo", RequestItem.venueno);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", RequestItem.venuebranchno);
                    var _userno = new SqlParameter("userno", RequestItem.userno == null ? 0 : RequestItem.userno);
                    var _didByUser = new SqlParameter("didByUser", RequestItem.didByUser == null ? 0 : RequestItem.didByUser);
                    var _isApproved = new SqlParameter("isApproved", RequestItem.isApproved == null ? false : RequestItem.isApproved);
                    var _approvalReason = new SqlParameter("approvalReason", RequestItem.approvalReason.ValidateEmpty());
                    var _cancellogid = new SqlParameter("cancellogid", RequestItem.cancellogid.ValidateEmpty());
                    
                    objresult = context.ApproveRefundCancel.FromSqlRaw(
                    "Execute dbo.pro_UpdateRefundCancelApproval @refundCancelLogNo, @patientVisitNo, @patientBillNo, @patientBillDetailsNo, @orderNo, @orderListNo, @venueno, @venuebranchno, @userno, @didByUser, @isApproved, @approvalReason, @cancellogid",
                    _refundCancelLogNo, _patientVisitNo, _patientBillNo, _patientBillDetailsNo, _orderNo, _orderListNo, _venueNo, _venueBranchNo, _userno, _didByUser, _isApproved, _approvalReason, _cancellogid).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PatientDueRepository.ApproveRefundCancel", ExceptionPriority.High, ApplicationType.REPOSITORY, RequestItem.venueno, RequestItem.venuebranchno, 0);
            }
            return objresult;
        }
        #endregion
    }
}