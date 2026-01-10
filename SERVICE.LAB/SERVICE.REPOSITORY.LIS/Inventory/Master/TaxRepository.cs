using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private IConfiguration _config;
        public TaxRepository(IConfiguration config) { _config = config; }

        public List<TblTax> Gettaxmaster(TaxMasterRequest taxrequest)
        {
            List<TblTax> objresult = new List<TblTax>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo", taxrequest?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", taxrequest?.venueNo);
                    var _pageIndex = new SqlParameter("pageIndex", taxrequest?.pageIndex);

                    objresult = context.Gettax.FromSqlRaw(
                    "Execute dbo.pro_GetTaxMaster @taxNo, @venueNo,@pageIndex",
                    _taxNo, _venueNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.Gettaxmaster" + taxrequest.taxNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, taxrequest.venueNo, 0, 0);
            }
            return objresult;
        }
        public TaxMasterResponse Inserttaxmaster(TblTax tbltax)
        {
            TaxMasterResponse objresult = new TaxMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo ", tbltax?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", tbltax?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tbltax?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tbltax?.userNo);
                    var _taxName = new SqlParameter("taxName", tbltax?.taxName);
                    var _taxPercentage = new SqlParameter("taxPercentage", tbltax?.taxPercentage);
                    var _sequenceNo = new SqlParameter("sequenceNo", tbltax?.sequenceNo);
                    var _status = new SqlParameter("status", tbltax?.status);
                    var _pageIndex = new SqlParameter("pageIndex", tbltax?.pageIndex);
                    var _totalRecords = new SqlParameter("totalRecords", tbltax?.totalRecords);

                    var obj = context.Inserttax.FromSqlRaw(
                    "Execute pro_InsertTaxMaster @taxNo,@venueNo,@venueBranchno,@userNo," +
                    "@taxName,@taxPercentage,@sequenceNo,@status,@pageIndex,@totalRecords",
                    _taxNo, _venueNo, _venueBranchno, _userNo, _taxName, _taxPercentage, _sequenceNo, _status, _pageIndex, _totalRecords).ToList();

                    objresult.taxNo = obj[0].taxNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.Inserttaxmaster" + tbltax.taxNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tbltax.venueNo, tbltax.venueBranchno, tbltax.userNo);
            }
            return objresult;
        }
        public List<TblHSN> GetHSNMaster(HSNMasterRequest HSNRequest)
        {
            List<TblHSN> objresult = new List<TblHSN>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo", HSNRequest ?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", HSNRequest ?.venueNo);
                    var _HSNNo = new SqlParameter("HSNNo", HSNRequest ?.HSNNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", HSNRequest ?.venueBranchno);
                    var _pageIndex = new SqlParameter("pageIndex", HSNRequest ?.pageIndex);

                    objresult = context.GetHSNMasters.FromSqlRaw(
                    "Execute dbo.pro_GetHSNMaster @venueNo,@venueBranchno,@HSNNo,@taxNo,@pageIndex",
                    _venueNo, _venueBranchno, _HSNNo, _taxNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.GetHSNMaster" + HSNRequest.HSNNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, HSNRequest.venueNo, HSNRequest.venueBranchno, 0);
            }
            return objresult;
        }
        public HSNMasterResponse InsertHSNmaster(TblHSN tblhsn)
        {
            HSNMasterResponse objresult = new HSNMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo ", tblhsn?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", tblhsn?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblhsn?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblhsn?.userNo);
                    var _HSNNo = new SqlParameter("HSNNo", tblhsn?.HSNNo);
                    var _HSNCode = new SqlParameter("HSNCode", tblhsn?.HSNCode);
                    var _Description = new SqlParameter("Description", tblhsn?.Description);
                    var _taxName = new SqlParameter("taxName", tblhsn?.taxName);
                    var _status = new SqlParameter("status", tblhsn?.status);
                    var _pageIndex = new SqlParameter("pageIndex", tblhsn?.pageIndex);
                    var _totalRecords = new SqlParameter("totalRecords", tblhsn?.totalRecords);

                    var obj = context.InsertHSNmaster.FromSqlRaw(
                    "Execute pro_InsertHSNmaster @venueNo,@venueBranchno,@HSNNo,@taxNo,@HSNCode,@taxName," +
                    "@Description,@status,@userNo,@pageIndex,@totalRecords",
                    _venueNo, _venueBranchno, _HSNNo, _taxNo, _HSNCode, _taxName,
                    _Description, _status, _userNo, _pageIndex, _totalRecords).ToList();
                    
                    objresult.HSNNo = obj[0].HSNNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.InsertHSNmaster" + tblhsn.HSNNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblhsn.venueNo, tblhsn.venueBranchno, tblhsn.userNo);
            }
            return objresult;
        }
        public List<TblHSNRange> GetHSNRangeMaster(HSNRangeRequest HSNrangeRequest)
        {
            List<TblHSNRange> objresult = new List<TblHSNRange>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo", HSNrangeRequest?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", HSNrangeRequest?.venueNo);
                    var _HSNNo = new SqlParameter("HSNNo", HSNrangeRequest?.HSNNo);
                    var _RangeFrom = new SqlParameter("RangeFrom", HSNrangeRequest?.RangeFrom);
                    var _RangeTo = new SqlParameter("RangeTo", HSNrangeRequest?.RangeTo);
                    var _HSNRangeNo = new SqlParameter("HSNRangeNo", HSNrangeRequest?.HSNRangeNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", HSNrangeRequest?.venueBranchno);
                    var _pageIndex = new SqlParameter("pageIndex", HSNrangeRequest?.pageIndex);

                    objresult = context.GetHSNRangeMaster.FromSqlRaw(
                    "Execute dbo.pro_GetHSNRangeWiseTax @venueNo,@venueBranchno,@HSNRangeNo,@HSNNo,@taxNo,@RangeFrom,@RangeTo,@pageIndex",
                    _venueNo, _venueBranchno, _HSNRangeNo, _HSNNo,  _taxNo, _RangeFrom, _RangeTo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.GetHSNRangeMaster" + HSNrangeRequest.HSNRangeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, HSNrangeRequest.venueNo, HSNrangeRequest.venueBranchno, 0);
            }
            return objresult;
        }
        public HSNInsertResponse InsertHSNRangeMaster(TblInsertHSNRange tblhsnrange)
        {
            HSNInsertResponse objresult = new HSNInsertResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _taxNo = new SqlParameter("taxNo ", tblhsnrange?.taxNo);
                    var _venueNo = new SqlParameter("venueNo", tblhsnrange?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tblhsnrange?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tblhsnrange?.userNo);
                    var _HSNNo = new SqlParameter("HSNNo", tblhsnrange?.HSNNo);
                    var _RangeFrom = new SqlParameter("RangeFrom", tblhsnrange?.RangeFrom);
                    var _RangeTo = new SqlParameter("RangeTo", tblhsnrange?.RangeTo);
                    var _HSNRangeNo = new SqlParameter("HSNRangeNo", tblhsnrange?.HSNRangeNo);
                    var _status = new SqlParameter("status", tblhsnrange?.status);
                    var obj = context.InsertHSNRangeMaster.FromSqlRaw(
                    "Execute pro_InsertHSNRangeWiseTax @venueNo,@venueBranchno,@HSNRangeNo,@HSNNo,@RangeFrom,@RangeTo,@taxNo,@status,@userNo",
                    _venueNo, _venueBranchno, _HSNRangeNo,_HSNNo, _RangeFrom, _RangeTo,_taxNo,_status, _userNo).ToList();
                    
                    objresult.HSNRangeNo = obj[0].HSNRangeNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxRepository.InsertHSNRangeMaster" + tblhsnrange.HSNRangeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblhsnrange.venueNo, tblhsnrange.venueBranchno, tblhsnrange.userNo);
            }
            return objresult;
        }
    }    
}