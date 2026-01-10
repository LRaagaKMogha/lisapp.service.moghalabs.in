using System;
using System.IO;
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
    public class AllergyRepository : IAllergyRepository
    {
        private IConfiguration _config;
        public AllergyRepository(IConfiguration config) { _config = config; }

        public List<lstAllergyType> GetAllergyTypes(reqAllergyType allType)
        {
            List<lstAllergyType> objresult = new List<lstAllergyType>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyTypeNo = new SqlParameter("AllergyTypeNo", allType?.AllergyTypeNo);
                    var _VenueNo = new SqlParameter("VenueNo", allType?.VenueNo);
                    var _PageIndex = new SqlParameter("PageIndex", allType?.PageIndex);

                    objresult = context.GetAllergyTypeData.FromSqlRaw(
                    "Execute dbo.pro_GetAllergyType @AllergyTypeNo, @VenueNo, @PageIndex",
                    _AllergyTypeNo, _VenueNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.GetAllergyTypes", ExceptionPriority.Low, ApplicationType.REPOSITORY, allType.VenueNo, 0, 0);
            }
            return objresult;
        }
        public AllergyTypeResponse InsertAllergyTypes(TblAllergyType req)
        {
            AllergyTypeResponse result = new AllergyTypeResponse();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyTypeNo = new SqlParameter("AllergyTypeNo", req?.AllergyTypeNo);
                    var _AllergyDescription = new SqlParameter("AllergyDescription", req?.AllergyDescription);
                    var _SequenceNo = new SqlParameter("SequenceNo", req?.SequenceNo);
                    var _Status = new SqlParameter("Status", req?.Status);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);

                    var obj = context.InsertAllergyTypeData.FromSqlRaw(
                    "Execute dbo.pro_InsertAllergyType @AllergyTypeNo, @AllergyDescription, @SequenceNo, @Status, @UserNo,@VenueNo, @VenueBranchNo",
                    _AllergyTypeNo, _AllergyDescription, _SequenceNo, _Status, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.AllergyTypeNo = obj[0].AllergyTypeNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.InsertAllergyTypes", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }

        public List<lstAllergyMaster> GetAllergyMasters(reqAllergyMaster allyName)
        {
            List<lstAllergyMaster> objresult = new List<lstAllergyMaster>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyMasterNo = new SqlParameter("AllergyMasterNo", allyName?.AllergyMasterNo);
                    var _AllergyTypeNo = new SqlParameter("AllergyTypeNo", allyName?.AllergyTypeNo);
                    var _VenueNo = new SqlParameter("VenueNo", allyName?.VenueNo);
                    var _PageIndex = new SqlParameter("PageIndex", allyName?.PageIndex);

                    objresult = context.GetAllergyMasterData.FromSqlRaw(
                    "Execute dbo.pro_GetAllergyMaster @AllergyMasterNo, @AllergyTypeNo, @VenueNo, @PageIndex",
                    _AllergyMasterNo,_AllergyTypeNo, _VenueNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.GetAllergyMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, allyName.VenueNo, 0, 0);
            }
            return objresult;
        }
        public rtnAllergyMaster InsertAllergyMasters(TblAllergyMaster res)
        {
            rtnAllergyMaster result = new rtnAllergyMaster();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyMasterNo = new SqlParameter("AllergyMasterNo", res?.AllergyMasterNo);
                    var _AllergyTypeNo = new SqlParameter("AllergyTypeNo", res?.AllergyTypeNo);
                    var _Description = new SqlParameter("Description", res?.Description);
                    var _AlgySequenceNo = new SqlParameter("AlgySequenceNo", res?.AlgySequenceNo);
                    var _AlgyStatus = new SqlParameter("AlgyStatus", res?.AlgyStatus);
                    var _UserNo = new SqlParameter("UserNo", res?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", res?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", res?.VenueBranchNo);

                    var obj = context.InsertAllergyMasterData.FromSqlRaw(
                    "Execute dbo.pro_InsertAllergyMaster @AllergyMasterNo, @AllergyTypeNo, @Description, @AlgySequenceNo, @AlgyStatus, @UserNo, @VenueNo, @VenueBranchNo",
                    _AllergyMasterNo, _AllergyTypeNo, _Description, _AlgySequenceNo, _AlgyStatus,_UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    result.AllergyMasterNo = obj[0].AllergyMasterNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.InsertAllergyMasters", ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return result;
        }

        public List<lstOPDReasonMaster> GetOPDReasonMaster(reqOPDReasonMaster resMas)
        {
            List<lstOPDReasonMaster> objresult = new List<lstOPDReasonMaster>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDReasonMastNo = new SqlParameter("OPDReasonMastNo ", resMas?.OPDReasonMastNo);
                    var _TypeNo = new SqlParameter("TypeNo", resMas?.TypeNo);
                    var _VenueNo = new SqlParameter("VenueNo", resMas?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", resMas?.VenueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", resMas?.PageIndex);

                    objresult = context.GetOPDResonMasterData.FromSqlRaw(
                    "Execute dbo.pro_GetOPDResonMaster @OPDReasonMastNo, @TypeNo, @VenueNo, @VenueBranchNo, @PageIndex",
                    _OPDReasonMastNo, _TypeNo, _VenueNo, _VenueBranchNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.GetOPDReasonMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, resMas.VenueNo, resMas.VenueBranchNo, 0);
            }
            return objresult;
        }
        public rtnOPDReasonMaster InsertOPDReasonMaster(TblOPDReasonMaster reasonMas)
        {
            rtnOPDReasonMaster result = new rtnOPDReasonMaster();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _OPDReasonMastNo = new SqlParameter("OPDReasonMastNo", reasonMas?.OPDReasonMastNo);
                    var _TypeNo = new SqlParameter("TypeNo", reasonMas?.TypeNo);
                    var _Description = new SqlParameter("Description", reasonMas?.Description);
                    var _ShortDesc = new SqlParameter("ShortDesc", reasonMas?.ShortDesc);
                    var _SeqNo = new SqlParameter("SeqNo", reasonMas?.SeqNo);
                    var _Status = new SqlParameter("Status", reasonMas?.Status);
                    var _UserNo = new SqlParameter("UserNo", reasonMas?.UserNo);
                    var _VenueNo = new SqlParameter("VenueNo", reasonMas?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", reasonMas?.VenueBranchNo);

                    var obj = context.InsertOPDResonMasterData.FromSqlRaw(
                    "Execute dbo.Pro_InsertOPDResonMaster @OPDReasonMastNo, @TypeNo, @Description, @ShortDesc, @SeqNo, @Status, @UserNo, @VenueNo, @VenueBranchNo",
                    _OPDReasonMastNo, _TypeNo, _Description, _ShortDesc, _SeqNo, _Status, _UserNo, _VenueNo, _VenueBranchNo).ToList();
                    
                    foreach (var lst in obj)
                        result.OPDReasonMastNo = lst.OPDReasonMastNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.InsertOPDReasonMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, reasonMas.VenueNo, reasonMas.VenueBranchNo, 0);
            }
            return result;
        }

        public rtnAllergyReaction InsertAllergyReaction(TblAllergyReaction res)
        {
            rtnAllergyReaction objresult = new rtnAllergyReaction();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyReactionNo = new SqlParameter("AllergyReactionNo", res?.AllergyReactionNo);
                    var _VenueNo = new SqlParameter("VenueNo", res?.VenueNo);
                    var _Description = new SqlParameter("Description", res?.Description);
                    var _SequenceNo = new SqlParameter("SequenceNo", res?.SequenceNo);
                    var _Status = new SqlParameter("Status", res?.Status);
                    var _UserId = new SqlParameter("UserId", res?.CreatedBy);

                    var obj = context.InsertAllergyReaction.FromSqlRaw(
                    "Execute dbo.Pro_InsertAllergyReaction @AllergyReactionNo, @VenueNo, @Description, @SequenceNo, @Status, @UserId",
                    _AllergyReactionNo, _VenueNo, _Description, _SequenceNo, _Status, _UserId).ToList();

                    res.AllergyReactionNo = obj[0].AllergyReactionNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyRepository.InsertAllergyReaction", ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, 0, 0);
            }
            return objresult;
        }
        public List<rtnAllergyReactionres> GetAllergyReactionl(rtnAllergyReactionreq masterRequest)
        {
            List<rtnAllergyReactionres> objResult = new List<rtnAllergyReactionres>();
            try
            {
                using (var context = new OPDContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _AllergyReactionNo = new SqlParameter("AllergyReactionNo", masterRequest.AllergyReactionNo);
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest.VenueNo);
                   // var _Status = new SqlParameter("Status", masterRequest.Status);
                    var _PageIndex = new SqlParameter("PageIndex", masterRequest.PageIndex);
                 

                    objResult = context.GetAllergyReactionl.FromSqlRaw(
                    "Execute dbo.Pro_GetAllergyReaction @AllergyReactionNo, @VenueNo, @PageIndex",
                    _AllergyReactionNo, _VenueNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAllergyReactionl", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.VenueNo, 0, 0);
            }
            return objResult;
        }

    }
}