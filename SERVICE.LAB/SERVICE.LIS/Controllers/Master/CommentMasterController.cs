using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEV.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;
using System.Xml;
using Shared.Audit;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAuditService _auditService;
        public CommentController(ICommentRepository noteRepository, IAuditService auditService)
        {
            _commentRepository = noteRepository;
            _auditService = auditService;
        }

        [HttpPost]
        [Route("api/Comment/Getcommentmaster")]
        public IEnumerable<CommentGetRes> Getcommentmaster(CommentGetReq getReq)
        {
            List<CommentGetRes> result = new List<CommentGetRes>();
            try
            {
                result = _commentRepository.Getcommentmaster(getReq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.Getcommentmaster" + getReq.CommentsMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.venueNo,0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Comment/Insertcommentmaster")]
        public ActionResult<CommentInsRes> Insertcommentmaster(CommentInsReq insReq)
        {
            CommentInsRes objresult = new CommentInsRes();
            try
            {
                using(var auditScoped = new AuditScope<CommentInsReq>(insReq, _auditService))
                {
                    var _errormsg = MiscellaneousMasterValidation.Insertcommentmaster(insReq);
                    if (!_errormsg.status)
                    {
                        objresult = _commentRepository.Insertcommentmaster(insReq);
                        string _CacheKey = CacheKeys.CommonMaster + "CONTAINER" + insReq.VenueNo + insReq.venueBranchno;
                        _commentRepository.Insertcommentmaster(insReq);
                        MemoryCacheRepository.RemoveItem(_CacheKey);
                        //comment master get 
                        string _CommentCacheKey = CacheKeys.CommonMaster + "ResultComments" + insReq.VenueNo + insReq.venueBranchno;
                        MemoryCacheRepository.RemoveItem(_CommentCacheKey);
                        //
                    }
                    else
                        return BadRequest(_errormsg);
                }                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.Insertcommentmaster - " + insReq.CommentsMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, insReq.venueBranchno, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/Comment/GetNationRace")]
        public IEnumerable<GetNationRaceRes> GetNationRace(GetNationRaceReq getReq)
        {
            List<GetNationRaceRes> result = new List<GetNationRaceRes>();
            try
            {
                result = _commentRepository.GetNationRace(getReq);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.GetNationRace" + getReq.CommonNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.CommonNo, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Comment/InsertNationRace")]
        public ActionResult<InsNationRaceRes> InsertNationRace(InsNationRaceReq insReq)
        {
            InsNationRaceRes objresult = new InsNationRaceRes();
            try
            {
                using (var auditScoped = new AuditScope<InsNationRaceReq>(insReq, _auditService))
                {
                    var _errormsg = MiscellaneousMasterValidation.InsertNationRace(insReq);
                    if (!_errormsg.status)
                    {
                        objresult = _commentRepository.InsertNationRace(insReq);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.InsertNationRace - " + insReq.CommonNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.CommonNo, 0, 0);
            }
            return Ok(objresult);
        }

        [HttpPost]
        [Route("api/Comment/TemplateInsertcomment")]
        public List<TemplateCommentRes> TemplateInsertcomment(TemplateComment Req)
        {
            List<TemplateCommentRes> objresult = new List<TemplateCommentRes>();
            try
            {
                objresult = _commentRepository.TemplateInsertcomment(Req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.TemplateInsertcomment" + Req.CH_QcNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req.VenueNo, Req.VenueBranchno, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Comment/InsertCommentSubCategory")]
        public ActionResult<CommentSubCatyInsResponse> InsertCommentSubCategory(InsertCommentSubCategoryReqest objRequest)
        {
            CommentSubCatyInsResponse objResponse = new CommentSubCatyInsResponse();
            try
            {
                using (var auditScoped = new AuditScope<InsertCommentSubCategoryReqest>(objRequest, _auditService))
                {
                    var _errormsg = MiscellaneousMasterValidation.InsertCommentSubCategory(objRequest);
                    if (!_errormsg.status)
                    {
                        objResponse = _commentRepository.InsertCommentSubCategory(objRequest);
                        string _CommentCacheKey = CacheKeys.CommonMaster + "COMMENTSSUBCATEGORY" + objRequest.VenueNo;
                        MemoryCacheRepository.RemoveItem(_CommentCacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.InsertCommentSubCategory - " + objRequest.CategoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, objRequest.userNo);
            }
            return Ok(objResponse);
        }

        [HttpPost]
        [Route("api/Comment/GetCommentSubCategory")]
        public IEnumerable<FetchCommentSubCategoryResponse> GetCommentSubCategory(FetchCommentSubCategoryReqest objRequest)
        {
            List<FetchCommentSubCategoryResponse> objResponse = new List<FetchCommentSubCategoryResponse>();
            try
            {
                objResponse = _commentRepository.GetCommentSubCategory(objRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.Insertcommentmaster" + objRequest.CategoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, objRequest.userNo);
            }
            return objResponse;
        }

        [HttpPost]
        [Route("api/Comment/InsertBankMaster")]
        public ActionResult<BankMasterResponse> InsertBankMaster(InsertBankMastereq objRequest)
        {
            BankMasterResponse objResponse = new BankMasterResponse();
            try
            {
                    var _errormsg = MiscellaneousMasterValidation.InsertBankMaster(objRequest);
                    if (!_errormsg.status)
                    {
                        objResponse = _commentRepository.InsertBankMaster(objRequest);
                        string _CommentCacheKey = CacheKeys.CommonMaster + "BANKTYPE" + objRequest.VenueNo;
                        MemoryCacheRepository.RemoveItem(_CommentCacheKey);
                    }
                    else
                        return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.InsertBankMaster - " + objRequest.BankID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return Ok(objResponse);
        }

        [HttpPost]
        [Route("api/Comment/GetBankMaster")]
        public IEnumerable<BankMasterResponse> GetBankMaster(InsertBankMasterr objRequest)
        {
            List<BankMasterResponse> objResponse = new List<BankMasterResponse>();
            try
            {
                objResponse = _commentRepository.GetBankMaster(objRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.GetBankMaster" + objRequest.BankID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return objResponse;
        }

        [HttpPost]
        [Route("api/Comment/InsertBankBranch")]
        public ActionResult<BankBranchResponse> InsertBankBranch(InsertBankbranchreq objRequest)
        {
            BankBranchResponse objResponse = new BankBranchResponse();
            try
            {
                var _errormsg = MiscellaneousMasterValidation.InsertBankBranch(objRequest);
                if (!_errormsg.status)
                {
                    objResponse = _commentRepository.InsertBankBranch(objRequest);
                    string _CommentCacheKey = CacheKeys.CommonMaster + "BRANCHTYPE" + objRequest.VenueNo;
                    MemoryCacheRepository.RemoveItem(_CommentCacheKey);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.InsertBankbranch - " + objRequest.BranchID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return Ok(objResponse);
        }

        [HttpPost]
        [Route("api/Comment/GetBankBranch")]
        public IEnumerable<BankBranchResponse> GetBankBranch(GetBankbranchreq objRequest)
        {
            List<BankBranchResponse> objResponse = new List<BankBranchResponse>();
            try
            {
                objResponse = _commentRepository.GetBankBranch(objRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentController.GetBankBranch" + objRequest.BranchID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return objResponse;
        }
    }
}