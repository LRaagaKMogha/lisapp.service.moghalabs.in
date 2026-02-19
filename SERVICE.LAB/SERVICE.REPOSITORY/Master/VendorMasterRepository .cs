using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Service.Common;
using Microsoft.Data.SqlClient;

namespace Service.Repository
{
    public class VendorMasterRepository : IVendorMasterRepository
    {
        private IConfiguration _config;
        public VendorMasterRepository(IConfiguration config) { _config = config; }

        public List<Responsegetvendor> GetVendorMaster(Requestvendor req)
        {
            List<Responsegetvendor> lst = new List<Responsegetvendor>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venuebranchno);
                    var _vendorno = new SqlParameter("vendorno", req?.vendorno);
                    var _pageIndex = new SqlParameter("pageIndex", req?.pageIndex);

                    lst = context.GetVendorMaster.FromSqlRaw(
                    "Execute dbo.pro_GetVendormaster @vendorno,@venueno,@venuebranchno,@pageIndex",
                    _vendorno, _venueno, _venuebranchno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.GetVendorMaster" + req.vendorno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }
        public StoreVendorMaster InsertVendorMaster(Responsevendor obj1)
        {
            StoreVendorMaster Objresult = new StoreVendorMaster();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _vendorNo = new SqlParameter("vendorNo", obj1?.vendorno);
                    var _vendorName = new SqlParameter("vendorName", obj1?.vendorName);
                    var _vendorCode = new SqlParameter("vendorCode", obj1?.vendorCode);
                    var _mobileno = new SqlParameter("mobileNo", obj1?.mobileNo);
                    var _whatsAppNo = new SqlParameter("whatsAppNo", obj1?.whatsAppNo);
                    var _phone = new SqlParameter("phone", obj1?.phone);
                    var _status = new SqlParameter("status", obj1?.status);
                    var _address = new SqlParameter("address", obj1?.address);
                    var _place = new SqlParameter("place", obj1?.place);
                    var _cityNo = new SqlParameter("cityNo", obj1?.cityNo);
                    var _stateNo = new SqlParameter("stateNo", obj1?.stateNo);
                    var _countryNo = new SqlParameter("countryNo", obj1?.countryNo);
                    var _pinCode = new SqlParameter("pinCode", obj1?.pinCode);
                    var _email = new SqlParameter("email", obj1?.email);
                    var _venueNo = new SqlParameter("venueNo", obj1?.venueno);
                    var _venuebranchNo = new SqlParameter("venuebranchNo", obj1?.venuebranchno);
                    var _webSite = new SqlParameter("webSite", obj1?.webSite);
                    var _gstNo = new SqlParameter("gstNo", obj1?.gstNo);
                    var _userNo = new SqlParameter("userNo", obj1?.userNo);

                    var obj = context.InsertVendorMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertVendormaster @VendorNo,@VendorName,@MobileNo,@WhatsAppNo,@Phone,@status,@Address,@Place,@CityNo,@StateNo," +
                    "@CountryNo,@PinCode,@Email,@venueno,@venuebranchNo,@WebSite,@GSTNo,@userNo,@vendorCode",
                    _vendorNo, _vendorName, _mobileno, _whatsAppNo, _phone, _status, _address, _place, _cityNo, _stateNo, _countryNo,
                    _pinCode, _email, _venueNo, _venuebranchNo, _webSite, _gstNo, _userNo, _vendorCode).ToList();
                    
                    Objresult.vendorno = obj[0].vendorno;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.InsertVendorMaster" + obj1.vendorno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, obj1.venueno, obj1.venuebranchno, 0);
            }
            return Objresult;
        }
        public List<Getcontactlst> GetVendorvsContactmaster(Getcontact creq)
        {
            List<Getcontactlst> lst = new List<Getcontactlst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", creq?.venueno);
                    var _VendorMasterNo = new SqlParameter("VendorMasterNo", creq?.vendorMasterNo);

                    lst = context.GetVendorvsContactmaster.FromSqlRaw(
                    "Execute dbo.pro_GetVendorvsContactmaster @venueno,@VendorMasterNo",
                   _venueno, _VendorMasterNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.GetVendorvsContactmaster" + creq.vendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq.venueno, 0);
            }
            return lst;
        }
        public int InsertVendorContactmaster(Savecontact creq1)
        {

            CommonHelper commonUtility = new CommonHelper();
            string SavecontactXML = commonUtility.ToXML(creq1.Getcontactlst);
            int i = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", creq1?.venueno);
                    var _VendorMasterNo = new SqlParameter("VendorMasterNo", creq1?.vendorMasterNo);
                    var _userNo = new SqlParameter("userNo", creq1?.userNo);
                    var _SavecontactXML = new SqlParameter("SavecontactXML", SavecontactXML);

                    var lst = context.InsertVendorContactmaster.FromSqlRaw(
                    "Execute dbo.pro_InsertVendorVsContact @venueNo,@VendorMasterNo,@userNo,@SavecontactXML",
                    _venueno, _VendorMasterNo, _userNo, _SavecontactXML).ToList();

                    i = lst[0].VendorContactNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.InsertVendorContactmaster" + creq1.vendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, creq1.venueno, creq1.userNo, 0);
            }
            return i;
        }
        public List<Getservicelst> GetVendorvsservices(Getservice sobj)
        {
            List<Getservicelst> lst = new List<Getservicelst>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("VenueNo", sobj?.VenueNo);
                    var _VendorMasterNo = new SqlParameter("VendorMasterNo", sobj?.VendorMasterNo);
                    var _ServiceNo = new SqlParameter("ServiceNo", sobj?.ServiceNo);
                    var _pageindex = new SqlParameter("pageindex", sobj?.pageindex);

                    lst = context.GetVendorvsservices.FromSqlRaw(
                    "Execute dbo.pro_GetVendorVsServices @venueno,@VendorMasterNo,@ServiceNo,@pageindex",
                    _venueno, _VendorMasterNo, _ServiceNo, _pageindex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.GetVendorvsServices" + sobj.VendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, sobj.VenueNo, 0, 0);
            }
            return lst;
        }
        public int InsertVendorService(Saveservice serviceobj)
        {
            CommonHelper commonUtility = new CommonHelper();
            string ServicelstXML = commonUtility.ToXML(serviceobj.Getservicelst);
            int i = 0;
            
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", serviceobj?.venueno);
                    var _VendorMasterNo = new SqlParameter("VendorMasterNo", serviceobj?.VendorMasterNo);
                    var _userNo = new SqlParameter("userNo", serviceobj?.userNo);
                    var _servicelstXML = new SqlParameter("ServicelstXML", ServicelstXML);

                    var lst = context.InsertVendorService.FromSqlRaw(
                    "Execute dbo.pro_InsertVendorVsServices @venueNo,@VendorMasterNo,@userNo,@servicelstXML",
                    _venueno, _VendorMasterNo, _userNo, _servicelstXML).ToList();
                    
                    if(lst.Any())
                    {
                        i = lst[0].VendorServiceNo;
                    }
                    else
                    {
                        i = 0;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "VendorMasterRepository.InsertVendorService" + serviceobj.VendorMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, serviceobj.venueno, serviceobj.userNo, 0);
            }
            return i;
        }
    }
}