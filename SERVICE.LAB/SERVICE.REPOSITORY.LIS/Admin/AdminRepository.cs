using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.Admin;
using DEV.Model.EF;
using DEV.Model.PatientInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Xml.Linq;

namespace Dev.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private IConfiguration _config;
        public AdminRepository(IConfiguration config) { _config = config; }

        public CommonAdminResponse DeleteVisitId(DeleteVisitRequest RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitID", RequestItem.VisitId);
                    var _UserNo = new SqlParameter("UserNo", RequestItem.userNo);

                    var lstResponse = context.DeleteVisitIdDTO.FromSqlRaw("Execute dbo.pro_DeleteVisit @VisitID,@VenueNo,@VenueBranchNo,@UserNo ",
                    _VisitNo, _VenueNo, _VenueBranchNo,_UserNo).ToList();
                    response = lstResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.DeleteVisitId/VisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }

        public List<SearchVisitDetailsResponse> SearchVisitId(DeleteVisitRequest RequestItem)
        {
            List<SearchVisitDetailsResponse> lstPatientInfoResponse = new List<SearchVisitDetailsResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("@VisitId", RequestItem.VisitId);

                    lstPatientInfoResponse = context.SearchVisitIdDTO.FromSqlRaw("Execute dbo.Pro_GetVisitDetails @VenueNo,@VenueBranchNo, @VisitID",
                   _VenueNo, _VenueBranchNo, _VisitNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.SearchVisitId/VisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lstPatientInfoResponse;
        }


        public List<SearchUpdateDatesResponse> SearchUpdateDates(DeleteVisitRequest RequestItem)
        {
            List<SearchUpdateDatesResponse> lstPatientInfoResponse = new List<SearchUpdateDatesResponse>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("@VisitId", RequestItem.VisitId);

                    lstPatientInfoResponse = context.SearchUpdateDatesDTO.FromSqlRaw("Execute dbo.Pro_SearchUpdateDates @VenueNo,@VenueBranchNo, @VisitID",
                   _VenueNo, _VenueBranchNo, _VisitNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.SearchUpdateDates/VisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return lstPatientInfoResponse;
        }


        public CommonAdminResponse UpdateCustomerDetails(UpdateCustomerDetails RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitID", RequestItem.VisitId);
                    var _RefferralTypeNo = new SqlParameter("RefferralTypeNo", RequestItem.refferalTypeNo);
                    var _NewCustomerNo = new SqlParameter("NewCustomerNo", RequestItem.refferalNo);
                    var _NewPhysicianNo = new SqlParameter("NewPhysicianNo", RequestItem.physicianNo);
                    var _ModifiedBy = new SqlParameter("ModifiedBy", RequestItem.userNo);

                    var lstResponse = context.UpdateCustomerDetailsDTO.FromSqlRaw("Execute dbo.pro_ClientChanges @VisitID,@RefferralTypeNo,@NewCustomerNo,@VenueNo,@VenueBranchNo,@NewPhysicianNo,@ModifiedBy",
                    _VisitNo, _RefferralTypeNo, _NewCustomerNo, _VenueNo, _VenueBranchNo, _NewPhysicianNo, _ModifiedBy).ToList();
                    response = lstResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.UpdateCustomerDetails/VisitId-" + RequestItem.VisitId, ExceptionPriority.Medium, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, RequestItem.userNo);
            }
            return response;
        }

        public CommonAdminResponse UpdateOrderDates(UpdateOrderDatesRequest RequestItem)
        {
            CommonAdminResponse response = new CommonAdminResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserNo", RequestItem.userNo);
                    var _VisitNo = new SqlParameter("VisitID", RequestItem.VisitId);
                    var _RegistrationDT = new SqlParameter("RegistrationDT", RequestItem.RegistrationDT.Replace("T"," "));
                    var _CollectionDT = new SqlParameter("CollectionDT", RequestItem.CollectionDT.Replace("T", " "));
                    var _ApprovedDT = new SqlParameter("ApprovedDT", RequestItem.ApprovedDT.Replace("T", " "));

                    var lstResponse = context.UpdateOrderDatesDTO.FromSqlRaw("Execute dbo.Pro_UpdateOrderDates @VenueNo,@VenueBranchNo,@UserNo,@VisitID,@RegistrationDT,@CollectionDT,@ApprovedDT",
                     _VenueNo, _VenueBranchNo, _UserNo, _VisitNo, _RegistrationDT, _CollectionDT, _ApprovedDT).ToList();
                    response = lstResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.UpdateOrderDates/VisitId-" + RequestItem.VisitId, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }

        public List<ResponseDataScrollText> SearchScrollText(RequestDataScrollText reqItem)
        {
            List<ResponseDataScrollText> lstInfoResponse = new List<ResponseDataScrollText>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _venueNo = new SqlParameter("VenueNo", reqItem.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", reqItem.venueBranchNo);
                    var _userNo = new SqlParameter("UserNo", reqItem.userNo);

                    lstInfoResponse = context.SearchScrollTextDTO.FromSqlRaw("Execute dbo.Pro_GetScrollTextDetails @VenueNo,@VenueBranchNo, @UserNo",
                   _venueNo, _venueBranchNo, _userNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.SearchScrollText", ExceptionPriority.Medium, ApplicationType.REPOSITORY, reqItem.venueNo, reqItem.venueBranchNo, 0);
            }
            return lstInfoResponse;

        }
        public List<responsehistory> DeleteHistory(visitRequest obj)
        {
            List<responsehistory> lst = new List<responsehistory>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _visitID = new SqlParameter("visitID", obj.visitId.ValidateEmpty());
                    var _VenueNo = new SqlParameter("VenueNo", obj?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", obj?.venueBranchNo);
                    var _Status = new SqlParameter("@Status", obj.status == null ? false : obj.status);
                    var _patientID = new SqlParameter("@patientID", obj.patientID.ValidateEmpty());
                    var _MobileNumber = new SqlParameter("@MobileNumber", obj.MobileNumber.ValidateEmpty());
                    var _ReferalType = new SqlParameter("@RefferalType", obj.ReferalType == null ? 1 : obj.ReferalType);
                    var _CustomerNo = new SqlParameter("@CustomerNo", obj.CustomerNo == null ? 0 : obj.CustomerNo);
                    var _physicianNo = new SqlParameter("@PhysicianNo", obj.PhysicianNo == null ? 0 : obj.PhysicianNo);
                    var _type = new SqlParameter("@type", obj.type.ValidateEmpty());
                    var _fromdate = new SqlParameter("@fromdate", obj.fromdate.ValidateEmpty());
                    var _todate = new SqlParameter("@todate", obj.todate.ValidateEmpty());
                    var _userNo = new SqlParameter("@userNo", obj.userNo == null ? 0 : obj.userNo);
                    var _branchNo = new SqlParameter("@branchNo", obj.branchNo == null ? 0 : obj.branchNo);
                    var _visitNo = new SqlParameter("@visitNo", obj.visitNo == null ? 0 : obj.visitNo);
                    var _pageIndex = new SqlParameter("pageIndex", obj.pageIndex == null ? 1 : obj.pageIndex);

                    lst = context.DeleteHistory.FromSqlRaw("Execute dbo.pro_DeleteHistory @visitID,@VenueNo,@VenueBranchNo,@Status,@PatientID,@MobileNumber,@RefferalType,@CustomerNo,@PhysicianNo,@Type,@Fromdate,@ToDate,@userNo,@branchNo,@visitNo,@pageIndex",
                    _visitID, _VenueNo, _VenueBranchNo, _Status, _patientID, _MobileNumber, _ReferalType, _CustomerNo, _physicianNo, _type, _fromdate, _todate, _userNo, _branchNo, _visitNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.DeleteHistory", ExceptionPriority.Medium, ApplicationType.REPOSITORY, 0, obj.venueBranchNo, 0);
            }
            return lst;
        }
        #region Paymode changes
        public List<PaymentMode> GetPaymentMode(GetPaymentModeRequest RequestItem)
        {
            List<PaymentMode> response = new List<PaymentMode>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _VisitNo = new SqlParameter("VisitID", RequestItem.VisitId);

                    var lstResponse = context.GetPaymentMode.FromSqlRaw("Execute dbo.Pro_GetVisitPaymentModes @VenueNo,@VenueBranchNo,@VisitId",
                     _VenueNo, _VenueBranchNo, _VisitNo).ToList();
                    response = lstResponse;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.GetPaymentMode/VisitId-" + RequestItem.VisitId, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }
        public SavePaymentModeResponse UpdateVisitPaymentModes(SavePaymentModeRequest RequestItem)
        {
            SavePaymentModeResponse response = new SavePaymentModeResponse();
            try
            {
                XDocument PaymentXML = new XDocument(new XElement("PaymentXML", from Item in RequestItem.lstPaymentMode
                                                                                select
                new XElement("PaymentList",
                new XElement("ModeOfPayment", Item.ModeOfPayment),
                new XElement("Amount", Item.Amount),
                new XElement("Description", Item.Description),
                new XElement("ModeOfType", Item.ModeOfType)
                )));
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", RequestItem.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", RequestItem.VenueBranchNo);
                    var _UserNo = new SqlParameter("UserID", RequestItem.UserID);
                    var _PatientVisitNo = new SqlParameter("PatientVisitNo", RequestItem.PatientVisitNo);
                    var _Paymentxml = new SqlParameter("Paymentxml", PaymentXML.ToString());

                    var lstResponse = context.UpdateVisitPaymentModes.FromSqlRaw("Execute dbo.Pro_UpdateVisitPaymentModes @VenueNo,@VenueBranchNo,@PatientVisitNo,@Paymentxml,@UserID",
                     _VenueNo, _VenueBranchNo, _PatientVisitNo, _Paymentxml, _UserNo).ToList();
                    response = lstResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AdminRepository.UpdateVisitPaymentModes/VisitId-" + RequestItem.PatientVisitNo, ExceptionPriority.Low, ApplicationType.REPOSITORY, RequestItem.VenueNo, RequestItem.VenueBranchNo, 0);
            }
            return response;
        }
        #endregion
    }
}
