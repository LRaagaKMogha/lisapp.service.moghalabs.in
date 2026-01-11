using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;
        public TaxController(ITaxRepository noteRepository )
        {
            _taxRepository = noteRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/Gettaxmaster")]
        public IEnumerable<TblTax> Gettaxmaster(TaxMasterRequest taxrequest)
        {
             List<TblTax> result = new List<TblTax>();
            try
            {               
                result= _taxRepository.Gettaxmaster(taxrequest);             
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "TaxController.Gettaxmaster" + taxrequest.taxNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, taxrequest.venueNo,0, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/Inserttaxmaster")]
        public TaxMasterResponse Inserttaxmaster(TblTax tbltax)
        {
            TaxMasterResponse objresult = new TaxMasterResponse();
            try
            {
                objresult = _taxRepository.Inserttaxmaster(tbltax);
                string _CacheKey = CacheKeys.CommonMaster + "TAX" + tbltax.venueNo + tbltax.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxController.Inserttaxmaster" + tbltax.taxNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tbltax.venueNo, tbltax.venueBranchno, tbltax.userNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/GetHSNMaster")]
        public List<TblHSN> GetHSNMaster(HSNMasterRequest HSNRequest)
        {
            List<TblHSN> result = new List<TblHSN>();
            try
            {
                result = _taxRepository.GetHSNMaster(HSNRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxController.GetHSNMaster" + HSNRequest.HSNNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, HSNRequest.venueNo, HSNRequest.venueBranchno, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/InsertHSNmaster")]
        public HSNMasterResponse InsertHSNmaster(TblHSN tblhsn)
        {
            HSNMasterResponse objresult = new HSNMasterResponse();
            try
            {
                objresult = _taxRepository.InsertHSNmaster(tblhsn);
                string _CacheKey = CacheKeys.CommonMaster + "HSNCODE" + tblhsn.venueNo + tblhsn.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxController.InsertHSNmaster" + tblhsn.HSNNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tblhsn.venueNo, tblhsn.venueBranchno, tblhsn.userNo);
            }
            return objresult;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/GetHSNRangeMaster")]
        public List<TblHSNRange> GetHSNRangeMaster(HSNRangeRequest HSNrangeRequest)
        {
            List<TblHSNRange> result = new List<TblHSNRange>();
            try
            {
                result = _taxRepository.GetHSNRangeMaster(HSNrangeRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxController.GetHSNRangeMaster" + HSNrangeRequest.HSNRangeNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, HSNrangeRequest.venueNo, HSNrangeRequest.venueBranchno, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Tax/InsertHSNRangeMaster")]
        public HSNInsertResponse InsertHSNRangeMaster(TblInsertHSNRange tblhsnrange)
        {
            HSNInsertResponse objresult = new HSNInsertResponse();
            try
            {
                objresult = _taxRepository.InsertHSNRangeMaster(tblhsnrange);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TaxController.InsertHSNRangeMaster" + tblhsnrange.HSNRangeNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tblhsnrange.venueNo, tblhsnrange.venueBranchno, tblhsnrange.userNo);
            }
            return objresult;
        }
    }
}