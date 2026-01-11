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
    public class CommentMasterRepository : ICommentRepository
    {
        private IConfiguration _config;
        public CommentMasterRepository(IConfiguration config) { _config = config; }

        public List<CommentGetRes> Getcommentmaster(CommentGetReq getReq)
        {
            List<CommentGetRes> objresult = new List<CommentGetRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CategoryNo = new SqlParameter("CategoryNo", getReq?.CategoryNo);
                    var _CommentsMastNo = new SqlParameter("CommentsMastNo", getReq?.CommentsMastNo);
                    var _pageIndex = new SqlParameter("pageIndex", getReq?.pageIndex);
                    var _venueNo = new SqlParameter("venueNo", getReq?.venueNo);
                    var _subCatyNo = new SqlParameter("subCatyNo", getReq?.SubCatyNo);

                    objresult = context.Getcomment.FromSqlRaw(
                    "Execute dbo.pro_GetCommentMaster @venueNo, @CommentsMastNo, @CategoryNo, @pageIndex, @SubCatyNo",
                    _venueNo, _CommentsMastNo, _CategoryNo, _pageIndex, _subCatyNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.Getcommentmaster" + getReq.CommentsMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.venueNo,0, 0);
            }
            return objresult;
        }

        public CommentInsRes Insertcommentmaster(CommentInsReq insReq)
        {
            CommentInsRes objresult = new CommentInsRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insReq?.VenueNo);
                    var _CommentsMastNo = new SqlParameter("CommentsMastNo", insReq?.CommentsMastNo);
                    var _CategoryNo = new SqlParameter("CategoryNo", insReq?.CategoryNo);
                    var _Description = new SqlParameter("Description", insReq?.Description);
                    var _ShortCode = new SqlParameter("ShortCode", insReq?.ShortCode);
                    var _SeqNo = new SqlParameter("SeqNo", insReq?.SeqNo);
                    var _Status = new SqlParameter("Status", insReq?.Status);
                    var _userNo = new SqlParameter("userNo", insReq?.userNo);
                    var _SubCatyNo = new SqlParameter("SubCatyNo", insReq?.SubCatyNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", insReq.venueBranchno);
                    var _Abnormal = new SqlParameter("IsAbnormal", insReq?.Abnormal);

                    var obj = context.Insertcomment.FromSqlRaw(
                        "Execute dbo.pro_InsertCommentMaster @VenueNo, @CommentsMastNo, @CategoryNo, @Description," +
                        "@ShortCode, @SeqNo, @Status, @userNo, @venueBranchno, @SubCatyNo, @IsAbnormal",
                         _VenueNo, _CommentsMastNo, _CategoryNo, _Description, _ShortCode,
                         _SeqNo, _Status, _userNo,_venueBranchno, _SubCatyNo, _Abnormal).AsEnumerable().FirstOrDefault();

                    objresult.CommentsMastNo = obj?.CommentsMastNo?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.Insertcommentmaster" + insReq.CommentsMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.VenueNo, insReq.venueBranchno, 0);
            }
            return objresult;
        }
        public List<GetNationRaceRes> GetNationRace(GetNationRaceReq getReq)
        {
            List<GetNationRaceRes> objresult = new List<GetNationRaceRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CommonNo = new SqlParameter("CommonNo", getReq?.CommonNo);
                    var _Type = new SqlParameter("Type", getReq?.Type);
                    var _pageIndex = new SqlParameter("pageIndex", getReq?.pageIndex);
                    objresult = context.GetNationRace.FromSqlRaw(
                        "Execute dbo.pro_GetNationalityRace @CommonNo,@Type,@pageIndex",
                         _CommonNo, _Type, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.GetNationaRace" + getReq.CommonNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, getReq.CommonNo, 0, 0);
            }
            return objresult;
        }
        public InsNationRaceRes InsertNationRace(InsNationRaceReq insReq)
        {
            InsNationRaceRes objresult = new InsNationRaceRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CommonNo = new SqlParameter("CommonNo", insReq?.CommonNo);
                    var _Description = new SqlParameter("Description", insReq?.Description);
                    var _Status = new SqlParameter("Status", insReq?.Status);
                    var _Type = new SqlParameter("Type", insReq?.Type);

                    var response = context.InsNationRace.FromSqlRaw(
                        "Execute dbo.pro_InsertNationalityRace @CommonNo,@Description,@Status,@Type",
                         _CommonNo, _Description, _Status, _Type).AsEnumerable().FirstOrDefault();

                    objresult.CommonNo = response?.CommonNo ?? 0;
                    objresult.LastPageIndex = response?.LastPageIndex ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.GetNationaRace - " + insReq.CommonNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, insReq.CommonNo, 0, 0);
            }
            return objresult;
        }

        public List<TemplateCommentRes> TemplateInsertcomment(TemplateComment Req)
        {
            List<TemplateCommentRes> objresult = new List<TemplateCommentRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _Type = new SqlParameter("type", Req?.Type);
                    var _CHQcNo = new SqlParameter("cHQcNo", Req?.CH_QcNo);
                    var _VenueNo = new SqlParameter("venueNo", Req?.VenueNo);
                    var _VenueBranchno = new SqlParameter("venueBranchno", Req.VenueBranchno);
                    var _TestNo = new SqlParameter("testNo", Req?.TestNo);
                    var _VisitNo = new SqlParameter("visitNo", Req?.PatientVisitNo);
                    var _CommentNo = new SqlParameter("commentNo", Req?.CommentNo);
                    var _ShortCode = new SqlParameter("shortCode", Req?.CommentShortCode);
                    var _Description = new SqlParameter("description", Req?.CommentDesc);
                    var _userNo = new SqlParameter("userNo", Req?.UserNo);
                    var _PageCode = new SqlParameter("pageCode", Req?.PageCode);

                    objresult = context.TemplateInsertcomment.FromSqlRaw(
                        "Execute dbo.pro_CRUDTemplateCommentbyPatientVisit @type,@cHQcNo,@venueNo,@venueBranchno,@testNo,@commentNo,@visitNo,@shortCode,@description," +
                        "@userNo,@pageCode",
                        _Type, _CHQcNo,_VenueNo, _VenueBranchno, _TestNo, _VisitNo, _CommentNo,_ShortCode, _Description, _userNo, _PageCode).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.Insertcommentmaster" + Req.CommentNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, Req.VenueNo, Req.VenueBranchno, 0);
            }
            return objresult;
        }

        public CommentSubCatyInsResponse InsertCommentSubCategory(InsertCommentSubCategoryReqest objRequest)
        {
            CommentSubCatyInsResponse objresult = new CommentSubCatyInsResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CategoryNo = new SqlParameter("CategoryNo", objRequest.CategoryNo);
                    var _SubCatyNo = new SqlParameter("SubCatyNo", objRequest?.SubCatyNo);
                    var _SubCatyDesc = new SqlParameter("SubCatyDesc", objRequest?.SubCatyDesc);
                    var _SeqNo = new SqlParameter("SeqNo", objRequest?.SeqNo);
                    var _Status = new SqlParameter("Status", objRequest?.Status);
                    var _DeptNo = new SqlParameter("DeptNo", objRequest?.DeptNo);
                    var _userNo = new SqlParameter("userNo", objRequest?.userNo);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);

                    var obj = context.InsertSubCatyComment.FromSqlRaw(
                        "Execute dbo.pro_InsertCommentSubCategory " +
                        "@CategoryNo, @SubCatyNo, @SubCatyDesc, @DeptNo, @SeqNo, @Status, @UserNo, @VenueNo",
                        _CategoryNo, _SubCatyNo, _SubCatyDesc, _DeptNo, _SeqNo, _Status, _userNo, _VenueNo).AsEnumerable().FirstOrDefault();
                    
                    objresult.SubCatyNo = obj?.SubCatyNo ?? 0;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.InsertCommentSubCategory" + objRequest.SubCatyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, objRequest.userNo);
            }
            return objresult;
        }

        public List<FetchCommentSubCategoryResponse> GetCommentSubCategory(FetchCommentSubCategoryReqest objRequest)
        {
            List<FetchCommentSubCategoryResponse> objResult = new List<FetchCommentSubCategoryResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CategoryNo = new SqlParameter("CategoryNo", objRequest?.CategoryNo);
                    var _SubCatyNo = new SqlParameter("SubCatyNo", objRequest?.SubCatyNo);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);
                    var _pageIndex = new SqlParameter("pageIndex", objRequest?.pageIndex);

                    objResult = context.GetSubCatyComment.FromSqlRaw(
                        "Execute dbo.pro_GetCommentSubCategory " +
                        "@CategoryNo, @SubCatyNo, @VenueNo, @PageIndex",
                        _CategoryNo, _SubCatyNo, _VenueNo, _pageIndex).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.GetCommentSubCategory" + objRequest.SubCatyNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, objRequest.userNo);
            }
            return objResult;
        }

        public BankMasterResponse InsertBankMaster(InsertBankMastereq objRequest)
        {
            BankMasterResponse objresult = new BankMasterResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BankID = new SqlParameter("BankID", objRequest.BankID);
                    var _BankName = new SqlParameter("BankName", objRequest?.BankName);
                    var _BankShortName = new SqlParameter("BankShortName", objRequest?.BankShortName);
                    var _BankType = new SqlParameter("BankType", objRequest?.BankType);
                    var _HeadOfficeAddresss = new SqlParameter("HeadOfficeAddress", objRequest?.HeadOfficeAddress);
                    var _CountryID = new SqlParameter("CountryID", objRequest?.CountryID);
                    var _StateID = new SqlParameter("StateID", objRequest?.StateID);
                    var _CityID = new SqlParameter("CityID", objRequest?.CityID);
                    var _PostalCode = new SqlParameter("PostalCode", objRequest?.PostalCode);
                    var _CreatedBy = new SqlParameter("CreatedBy", objRequest?.CreatedBy);
                    var _Status = new SqlParameter("Status", objRequest?.Status);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);
                    //var _VenueBranchNo = new SqlParameter("VenueBranchNo", objRequest?.VenueBranchNo);
                    

                    var obj = context.InsertBankMaster.FromSqlRaw(
                        "Execute dbo.Pro_InsertBank_Master " +
                        "@BankID, @BankName, @BankShortName, @BankType, @HeadOfficeAddress, @CountryID, @StateID, @CityID, @PostalCode, @CreatedBy, @Status,@VenueNo",
                        _BankID, _BankName, _BankShortName, _BankType, _HeadOfficeAddresss, _CountryID, _StateID, _CityID, _PostalCode, _CreatedBy, _Status, _VenueNo).AsEnumerable().FirstOrDefault();

                    objresult = obj;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.InsertBankMaster" + objRequest.BankID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return objresult;
        }

        public List<BankMasterResponse> GetBankMaster(InsertBankMasterr objRequest)
        {
            List<BankMasterResponse> objResult = new List<BankMasterResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BankID = new SqlParameter("BankID", objRequest?.BankID);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);
                    var _PageIndex = new SqlParameter("pageIndex", objRequest?.PageIndex);

                    objResult = context.GetBankMaster.FromSqlRaw(
                        "Execute dbo.Pro_GetBank_Master " +
                        "@BankID, @VenueNo, @PageIndex",
                        _BankID, _VenueNo, _PageIndex).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.GetBankMaster" + objRequest.BankID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0,0);
            }
            return objResult;
        }

        public BankBranchResponse InsertBankBranch(InsertBankbranchreq objRequest)
        {
            BankBranchResponse objresult = new BankBranchResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BranchID = new SqlParameter("BranchID", objRequest.BranchID);
                    var _BankID = new SqlParameter("BankID", objRequest?.BankID);
                    var _BranchName = new SqlParameter("BranchName", objRequest?.BranchName);
                    var _BranchCode = new SqlParameter("BranchCode", objRequest?.BranchCode);
                    var _IFSCcode = new SqlParameter("IFSCcode", objRequest?.IFSCcode);
                    var _BranchAddress = new SqlParameter("BranchAddress", objRequest?.BranchAddress);
                    var _ContactNumbers = new SqlParameter("ContactNumbers", objRequest?.ContactNumbers);
                    var _EmailID  = new SqlParameter("EmailID", objRequest?.EmailID);
                    var _CreatedBy = new SqlParameter("CreatedBy", objRequest?.CreatedBy);
                    var _Status = new SqlParameter("Status", objRequest?.Status);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);
                    //var _VenueBranchNo = new SqlParameter("VenueBranchNo", objRequest?.VenueBranchNo);


                    var obj = context.InsertBankBranch.FromSqlRaw(
                        "Execute dbo.Pro_InsertBank_Branch " +
                        "@BranchID, @BankID, @BranchName, @BranchCode, @IFSCcode, @BranchAddress, @ContactNumbers, @EmailID, @CreatedBy, @Status,@VenueNo",
                        _BranchID, _BankID, _BranchName, _BranchCode, _IFSCcode, _BranchAddress, _ContactNumbers, _EmailID, _CreatedBy, _Status, _VenueNo).AsEnumerable().FirstOrDefault();

                    objresult = obj;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommentRepository.InsertBankMaster" + objRequest.BankID.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, objRequest.VenueNo, 0, 0);
            }
            return objresult;
        }

        public List<BankBranchResponse> GetBankBranch(GetBankbranchreq objRequest)
        {
            List<BankBranchResponse> objResult = new List<BankBranchResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _BranchID = new SqlParameter("BranchID", objRequest?.BranchID);
                    var _VenueNo = new SqlParameter("VenueNo", objRequest?.VenueNo);
                    var _PageIndex = new SqlParameter("PageIndex", objRequest?.PageIndex);

                    objResult = context.GetBankBranch.FromSqlRaw(
                        "Execute dbo.Pro_GetBank_Branch @BranchID, @VenueNo, @PageIndex",
                        _BranchID, _VenueNo, _PageIndex).AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex,
                    "CommentRepository.GetBankBranch" + objRequest.BranchID.ToString(),
                    ExceptionPriority.Low, ApplicationType.REPOSITORY,
                    objRequest.VenueNo, 0, 0);
            }
            return objResult;
        }
    }
}