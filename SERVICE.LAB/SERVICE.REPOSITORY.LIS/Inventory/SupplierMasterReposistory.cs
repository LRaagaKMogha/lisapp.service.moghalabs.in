using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Inventory;
using DEV.Model.Inventory.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Dev.Repository
{
    public class SupplierMasterReposistory : ISupplierMasterRepository
    {
        private IConfiguration _config;
        public SupplierMasterReposistory(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get SupplierMaster Details
        /// </summary>
        /// <returns></returns>
        public List<GetSupplierMasterResponse> GetSupplierDetails(SupplierMasterRequest masterRequest)
        {
            List<GetSupplierMasterResponse> objResult = new List<GetSupplierMasterResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest?.venueNo);
                    var _SupplierNo = new SqlParameter("SupplierNo", masterRequest?.supplierNo);
                    var _Status = new SqlParameter("Status", masterRequest.status);
                    var _UserNo = new SqlParameter("UserNo", masterRequest?.userNo);
                    var _PageIndex = new SqlParameter("PageIndex", masterRequest?.pageIndex);

                    objResult = context.GetSupplierDetails.FromSqlRaw(
                    "Execute dbo.Pro_Iv_FetchSuppliersDetails @VenueNo, @SupplierNo, @Status,@UserNo, @PageIndex",
                    _VenueNo, _SupplierNo, _Status, _UserNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterReposistory.GetSupplierMasters" + masterRequest.supplierNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueNo, masterRequest.venueBranchNo, masterRequest.userNo);
            }
            return objResult;         
        }

        /// <summary>
        /// Insert SupplierMaster Details
        /// </summary>
        /// <param name="SupplierMaster"></param>
        /// <returns></returns>
        public int InsertSupplierMasterDetails(postSupplierMasterDTO supplierMasterDTO)
        {
            int result = 0;
            SupplierResponse response = new SupplierResponse();

            var supplierMaster = supplierMasterDTO?.updSupplierMaster;
            var supplierVsContact = supplierMasterDTO?.updSupplierVsContactLst;
            var supplierVsBank = supplierMasterDTO?.updSupplierVsBankLst;

            CommonHelper commonUtility = new CommonHelper();
            var supplierMasterXML =  supplierMaster.ToString() == string.Empty ? null : commonUtility.ToXML(supplierMaster);
            var supplierVsContactXML = supplierVsContact == null ? string.Empty : commonUtility.ToXML(supplierVsContact);
            var supplierVsBankXML = supplierVsBank == null ? string.Empty : commonUtility.ToXML(supplierVsBank);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", supplierMasterDTO?.venueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", supplierMasterDTO?.venueBranchNo);
                    var _supplierMasterXML = new SqlParameter("SupplierMasterXML", supplierMasterXML);
                    var _supplierVsContactXML = new SqlParameter("SupplierVsContactXML", supplierVsContactXML);
                    var _supplierVsBankXML = new SqlParameter("SupplierVsBankXML", supplierVsBankXML);
                    var _userNo = new SqlParameter("UserNo", supplierMasterDTO?.userNo);

                    var objResult = context.CreateSupplierMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_Iv_InsertSupplierMaster @VenueNo, @VenueBranchNo, @SupplierMasterXML, @SupplierVsContactXML, @SupplierVsBankXML, @UserNo",
                    _venueNo, _venueBranchNo, _supplierMasterXML, _supplierVsContactXML, _supplierVsBankXML, _userNo).AsEnumerable().FirstOrDefault();

                    result = objResult.status;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterReposistory.InsertSupplierMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, supplierMasterDTO.venueNo, supplierMasterDTO.venueBranchNo, supplierMasterDTO.userNo);
            }
            return result;
        }
        public EditSupplierresponse GetEditSuppiler(UpdateSupplierMaster req)
        {
            EditSupplierresponse obj = new EditSupplierresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _supplierMasterNo = new SqlParameter("SupplierMasterNo", req?.supplierMasterNo);
                    var _venueno = new SqlParameter("venueno", req?.venueNo);
                    //var _venuebranchno = new SqlParameter("venuebranchno", req.venueBranchNo);

                    var lst = context.GetEditSuppiler.FromSqlRaw(
                    "Execute dbo.pro_GetSuppilerMaster @SupplierMasterNo,@venueno",
                    _supplierMasterNo, _venueno).ToList();

                    obj.supplierMasterNo = lst[0].supplierMasterNo;
                    obj.supplierMasterName = lst[0].supplierMasterName;
                    obj.drugLicenceNo = lst[0].drugLicenceNo;
                    obj.tinNo = lst[0].tinNo;
                    obj.mobileNo = lst[0].mobileNo;
                    obj.phoneNo = lst[0].phoneNo;
                    obj.adress = lst[0].adress;
                    obj.email = lst[0].email;
                    obj.creditDays = lst[0].creditDays;
                    obj.isNonTaxable = lst[0].isNonTaxable;
                    obj.isConsignment = lst[0].isConsignment;
                    obj.currencyNo = lst[0].currencyNo;
                    obj.payTermsNo = lst[0].payTermsNo;
                    obj.cStreet = lst[0].cStreet;
                    obj.cPlaceName = lst[0].cPlaceName;
                    obj.cCityNo = lst[0].cCityNo;
                    obj.cStateNo = lst[0].cStateNo;
                    obj.cCountryNo = lst[0].cCountryNo;
                    obj.cPin = lst[0].cPin;
                    obj.cPhoneNo = lst[0].cPhoneNo;
                    obj.cMobileNo = lst[0].cMobileNo;
                    obj.cWhatsappNo = lst[0].cWhatsappNo;
                    obj.cFax = lst[0].cFax;
                    obj.cEmail = lst[0].cEmail;
                    obj.cWeb = lst[0].cWeb;
                    obj.bStreet = lst[0].bStreet;
                    obj.bPlaceName = lst[0].bPlaceName;
                    obj.bCityNo = lst[0].bCityNo;
                    obj.bStateNo = lst[0].bStateNo;
                    obj.bCountryNo = (short)lst[0].bCountryNo;
                    obj.bPin = lst[0].bPin;
                    obj.bPhoneNo = lst[0].bPhoneNo;
                    obj.bMobileNo = lst[0].bMobileNo;
                    obj.bWhatsappNo = lst[0].bWhatsappNo;
                    obj.bFax = lst[0].bFax;
                    obj.bEmail = lst[0].bEmail;
                    obj.bWeb = lst[0].bWeb;
                    obj.remarks = lst[0].remarks;
                    obj.status = lst[0].status;
                    obj.sCountryNo = lst[0].sCountryNo;
                    obj.venueNo = lst[0].venueNo;
                    obj.venueBranchNo = lst[0].venueBranchNo;
                    obj.userNo = lst[0].userNo;
                    obj.sIsNonTaxable = lst[0].sIsNonTaxable;
                    obj.sIsConsignment = lst[0].sIsConsignment;
                    obj.sPayTermsNo = lst[0].sPayTermsNo;
                    obj.sRegNo = lst[0].sRegNo;
                    obj.sCreditDays = lst[0].sCreditDays;
                    obj.sActive = lst[0].sActive;
                    obj.sNotes = lst[0].sNotes;
                    obj.SupplierContact = lst[0].SupplierContact;
                    obj.SupplierBank = lst[0].SupplierBank;
                    obj.manufacturerNo = lst[0].manufacturerNo;
                    obj.manufacturerName = lst[0].manufacturerName;
                    obj.DrugType = lst[0].DrugType;
                    obj.OtherDrug = lst[0].OtherDrug;
                    obj.updSupplierContactLst = JsonConvert.DeserializeObject<List<UpdateSupplierContact>>(lst[0].SupplierContact);
                    obj.updSupplierBankLst = JsonConvert.DeserializeObject<List<UpdateSupplierBank>>(lst[0].SupplierBank);                    
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SupplierMasterReposistory.SupplierMasterReposistory.GetEditSuppiler" + req.supplierMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.venueBranchNo, req.userNo);
            }
            return obj;
        }
    }
}

