using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class CommericalMasterRepository : ICommericalRepository
    {
        private IConfiguration _config;
        public CommericalMasterRepository(IConfiguration config) { _config = config; }

        public List<CommericalGetRes> Getcompanymaster(CommericalGetReq getReq)
        {
            List<CommericalGetRes> objresult = new List<CommericalGetRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CompanyNo = new SqlParameter("CompanyNo", getReq?.CompanyNo);
                    var _pageIndex = new SqlParameter("pageIndex", getReq?.pageIndex);
                    var _venueNo = new SqlParameter("venueNo", getReq?.venueNo);
                    objresult = context.Getcompany.FromSqlRaw(
                        "Execute dbo.pro_GetCompanyDetails @venueNo,@CompanyNo,@pageIndex",
                         _venueNo, _CompanyNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalRepository.Getcompanymaster" + getReq.CompanyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.venueNo,0, 0);
            }
            return objresult;
        }

        public CommericalInsRes Insertcompanymaster(CommericalInsReq insReq)
        {
            CommericalInsRes objresult = new CommericalInsRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insReq?.VenueNo);
                    var _CompanyNo = new SqlParameter("CompanyNo", insReq?.CompanyNo);
                    var _CompanyName = new SqlParameter("CompanyName", insReq?.CompanyName);
                    var _EmailID = new SqlParameter("EmailID", insReq?.EmailID);
                    var _MobileNo = new SqlParameter("MobileNo", insReq?.MobileNo);
                    var _SeqNo = new SqlParameter("SeqNo", insReq?.SeqNo);
                    var _Status = new SqlParameter("Status", insReq?.Status);
                    var _userNo = new SqlParameter("userNo", insReq?.userNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", insReq.venueBranchno);


                    var obj = context.Insertcompany.FromSqlRaw(
                        "Execute dbo.pro_InsertCompanyDetails @VenueNo,@CompanyNo,@CompanyName,@EmailID," +
                        "@MobileNo,@SeqNo,@Status,@userNo,@venueBranchno",
                         _VenueNo, _CompanyNo, _CompanyName, _EmailID, _MobileNo,
                         _SeqNo, _Status, _userNo, _venueBranchno).AsEnumerable().FirstOrDefault();
                    objresult.CompanyNo = obj?.CompanyNo ?? 0;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalRepository.Insertcompanymaster" + insReq.CompanyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, insReq.venueBranchno, 0);
            }
            return objresult;
        }
        public List<GSTGetRes> GetGSTMaster(GSTGetReq getReq)
        {
            List<GSTGetRes> objresult = new List<GSTGetRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TaxMastNo = new SqlParameter("TaxMastNo", getReq?.TaxMastNo);
                    var _VenueNo = new SqlParameter("VenueNo", getReq?.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", getReq?.pageIndex);
                    objresult = context.GetGST.FromSqlRaw(
                        "Execute dbo.Pro_GetGSTTaxmaster @TaxMastNo,@VenueNo,@pageIndex",
                         _TaxMastNo,_VenueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalRepository.GetGSTMaster" + getReq.TaxMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.VenueNo, 0, 0);
            }
            return objresult;
        }

        public GSTInsRes InsertGSTMaster(GSTInsReq insReq)
        {
            GSTInsRes objresult = new GSTInsRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _TaxMastNo = new SqlParameter("TaxMastNo", insReq?.TaxMastNo);
                    var _Description = new SqlParameter("Description", insReq?.Description);
                    var _Percentage = new SqlParameter("Percentage", insReq?.Percentage);
                    var _FromDate = new SqlParameter("FromDate", insReq?.FromDate);
                    var _ToDate = new SqlParameter("ToDate", insReq?.ToDate);
                    var _VenueNo = new SqlParameter("VenueNo", insReq?.VenueNo);
                    var _Status = new SqlParameter("Status", insReq?.Status);
                    var _userNo = new SqlParameter("userNo", insReq?.userNo);


                    var obj = context.InsertGST.FromSqlRaw(
                        "Execute dbo.Pro_InsertGSTTaxMaster @TaxMastNo,@Description,@Percentage,@FromDate,@ToDate,@VenueNo,@Status,@userNo",
                         _TaxMastNo, _Description, _Percentage, _FromDate, _ToDate, _VenueNo,_Status, _userNo).AsEnumerable().FirstOrDefault();
                    objresult.TaxMastNo = obj?.TaxMastNo ?? 0;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommericalRepository.Insertcompanymaster" + insReq.TaxMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, 0, 0);
            }
            return objresult;
        }

    }
}