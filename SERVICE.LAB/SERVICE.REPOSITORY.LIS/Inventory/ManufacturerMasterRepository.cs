using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using DEV.Model.Inventory;
using DEV.Model.Inventory.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class ManufacturerMasterRepository : IManufacturerMasterRepository
    {
        private IConfiguration _config;
        public ManufacturerMasterRepository(IConfiguration config) { _config = config; }

        ///// <summary>
        ///// Get Manufacturer Details
        ///// </summary>
        ///// <returns></returns>
        //public List<GetManufacturerMasterResponse> GetManufacturers(GetCommonMasterRequest masterRequest)
        //{
        //    List<GetManufacturerMasterResponse> objresult = new List<GetManufacturerMasterResponse>();
        //    try
        //    {
        //        using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
        //        {
        //            if (masterRequest.masterNo > 0)
        //            {
        //                objresult = context.Tbl_IV_Manufacturer.Where(x => x.venueNo == masterRequest.venueno && x.venueBranchNo == masterRequest.venuebranchno && x.manufacturerNo == masterRequest.masterNo && x.status == true).ToList();
        //            }
        //            else
        //            {
        //                objresult = context.Tbl_IV_Manufacturer.Where(x => x.venueNo == masterRequest.venueno && x.venueBranchNo == masterRequest.venuebranchno && x.status == true).ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MyDevException.Error(ex, "GetManufacturers", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueno, masterRequest.venuebranchno, 0);
        //    }
        //    return objresult;
        //}

        /// <summary>
        /// Insert Manufacturer Details
        /// </summary>
        /// <param name="Manufacturer"></param>
        /// <returns></returns>
        public int InsertManufacturerDetails(postManufacturerMasterDTO objManuDTO)
        {
            int result = 0;
            CommonAdminResponse response = new CommonAdminResponse();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", objManuDTO.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", objManuDTO.venueBranchno);
                    var _userNo = new SqlParameter("userNo", objManuDTO.userNo);
                    var _status = new SqlParameter("status", objManuDTO.status);
                    var _manufacturerNo = new SqlParameter("manufacturerNo", objManuDTO.manufacturerNo);
                    var _manufacturerName = new SqlParameter("manufacturerName", objManuDTO.manufacturerName);
                    var _contactName = new SqlParameter("contactName", objManuDTO.contactName);
                    var _mobileNo = new SqlParameter("mobileNo", objManuDTO.mobileNo);
                    var _phoneNo = new SqlParameter("phoneNo", objManuDTO.phoneNo);
                    var _cwhatsappNo = new SqlParameter("cwhatsappNo", objManuDTO.cwhatsappNo);
                    var _address = new SqlParameter("address", objManuDTO.address);
                    var _email = new SqlParameter("email", objManuDTO.email);
                    var _ccountryNo = new SqlParameter("ccountryNo", objManuDTO.ccountryNo);
                    var _cstateNo = new SqlParameter("cstateNo", objManuDTO.cstateNo);
                    var _ccityNo = new SqlParameter("ccityNo", objManuDTO.ccityNo);
                    var _cplace = new SqlParameter("cplace", objManuDTO.cplace);
                    var _cname = new SqlParameter("cname", objManuDTO.cname);
                    var _cemail = new SqlParameter("cemail", objManuDTO.cemail);
                    var _cphoneNo = new SqlParameter("cphoneNo", objManuDTO.cphoneNo);
                    var _cmobileNo = new SqlParameter("cmobileNo", objManuDTO.cmobileNo);                   
                    var _panNo = new SqlParameter("panNo", objManuDTO.panNo);
                    var _cstreet = new SqlParameter("cstreet", objManuDTO.cstreet);
                    var _website = new SqlParameter("website", objManuDTO.website);
                    var _remarks = new SqlParameter("remarks", objManuDTO.remarks);
                    var _fcountryNo = new SqlParameter("fcountryNo", objManuDTO.fcountryNo);
                    var _fstateNo = new SqlParameter("fstateNo", objManuDTO.fstateNo);
                    var _fcityNo = new SqlParameter("fcityNo", objManuDTO.fcityNo);
                    var _fplace = new SqlParameter("fplace", objManuDTO.fplace);
                    var _fstreet = new SqlParameter("fstreet", objManuDTO.fstreet);
                    var _fname = new SqlParameter("fname", objManuDTO.fname);
                    var _femail = new SqlParameter("femail", objManuDTO.femail);
                    var _fphoneNo = new SqlParameter("fphoneNo", objManuDTO.fphoneNo);
                    var _fmobileNo = new SqlParameter("fmobileNo", objManuDTO.fmobileNo);
                    var _fwhatsappNo = new SqlParameter("fwhatsappNo", objManuDTO.fwhatsappNo);

                    var objResult = context.CreateManufacturerMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_Iv_InsertManufacturerMaster @venueNo,@venueBranchno,@userNo,@status,@manufacturerNo,@manufacturerName,@contactName,@mobileNo," +
                    "@phoneNo,@cwhatsappNo,@address,@email,@ccountryNo,@cstateNo,@ccityNo,@cplace,@cname,@cemail,@cphoneNo," +
                    "@cmobileNo,@panNo,@cstreet,@website,@remarks,@fcountryNo,@fstateNo,@fcityNo,@fplace,@fstreet," +
                    "@fname,@femail,@fphoneNo,@fmobileNo,@fwhatsappNo",
                    _venueNo,_venueBranchno,_userNo,_status,_manufacturerNo,_manufacturerName, _contactName,_mobileNo,
                    _phoneNo,_cwhatsappNo,_address,_email,_ccountryNo,_cstateNo,_ccityNo,_cplace,_cname,_cemail,
                    _cphoneNo,_cmobileNo,_panNo,_cstreet, _website,_remarks, _fcountryNo, _fstateNo,_fcityNo,
                    _fplace,_fstreet,_fname,_femail,_fphoneNo,_fmobileNo,_fwhatsappNo).ToList();

                    response = objResult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex,"InsertManufacturerDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, objManuDTO.venueNo, 0, 0);
            }
            return result;
        }

        /// <summary>
        /// Get Manufacturer Master Details
        /// </summary>
        /// <returns></returns>
        public List<GetManufacturerMasterResponse> GetManufacturersDetail(ManufacturerMasterRequest masterRequest)
        {
            List<GetManufacturerMasterResponse> objResult = new List<GetManufacturerMasterResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("venueNo", masterRequest.venueNo);
                    var _manufacturerNo = new SqlParameter("manufacturerNo", masterRequest.manufacturerNo);
                    //var _status = new SqlParameter("Status", masterRequest.status);
                    //var _userNo = new SqlParameter("UserNo", masterRequest.userNo);
                    var _pageIndex = new SqlParameter("pageIndex", masterRequest.pageIndex);

                    objResult = context.GetManufacturersDetail.FromSqlRaw(
                    "Execute dbo.pro_Iv_FetchManufacturersDetail @venueNo,@manufacturerNo,@pageIndex",
                     _venueNo,_manufacturerNo,_pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetManufacturersDetail", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueNo, 0, 0);
            }
            return objResult;
        }
    }
}