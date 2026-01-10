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
    public class specializationRepository : IspecializationRepository
    {
        private IConfiguration _config;
        public specializationRepository(IConfiguration config) { _config = config; }

        public List<Tblspecialization> Getspecializationmaster(SpecializationMasterRequest specilazationMasterRequest)
        {
            List<Tblspecialization> objresult = new List<Tblspecialization>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _specializationNO = new SqlParameter("specializationNO", specilazationMasterRequest.specializationNo);
                    var _venueNo = new SqlParameter("venueNo", specilazationMasterRequest.venueNo);

                    objresult = context.Getspecialization.FromSqlRaw(
                        "Execute dbo.pro_GetSpecialization @specializationNO,@venueNo",
                         _specializationNO, _venueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "specializationRepository.GetspecializationDetails" + specilazationMasterRequest?.specializationNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, specilazationMasterRequest?.venueNo,0, 0);
            }
            return objresult;
        }
        public SpecializationMasterResponse Insertspecializatiomaster(Tblspecialization tblspecialization)
        {
            SpecializationMasterResponse objresult = new SpecializationMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _specializationNO = new SqlParameter("specializationNo", tblspecialization?.specializationNo);
                    var _venueno = new SqlParameter("venueno", tblspecialization?.venueNo);
                    var _specialization = new SqlParameter("specialization", tblspecialization?.specialization);
                    var _status = new SqlParameter("status", tblspecialization?.status);
                    var _userNo = new SqlParameter("userNo", tblspecialization?.userNo);

                    var obj = context.Insertspecialization.FromSqlRaw(
                        "Execute dbo.pro_InsertSpecialization @SpecializationNO,@Specialization,@venueno,@status,@userNo",
                         _specializationNO, _specialization, _venueno, _status, _userNo).AsEnumerable().FirstOrDefault();
                    objresult.specializationNo = obj?.specializationNo?? 0;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "specializationRepository.Insertspecializatiomaster" + tblspecialization?.specializationNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblspecialization?.venueNo, tblspecialization?.venueBranchno, tblspecialization?.userNo);
            }
            return objresult;
        }

        public int CheckMasterNameExists(CheckMasterNameExistsRequest checkMasterNameExistsRequest)
        {
            int iresult = 0;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _masterNo = new SqlParameter("masterno", checkMasterNameExistsRequest?.masterNo);
                    var _masterValueno = new SqlParameter("masterValueno", checkMasterNameExistsRequest?.masterValueno);
                    var _venueNo = new SqlParameter("venueno", checkMasterNameExistsRequest?.venueNo);
                    var _masterName = new SqlParameter("mastername", checkMasterNameExistsRequest?.masterName);
                    var _masterTypeNo = new SqlParameter("masterTypeNo", checkMasterNameExistsRequest?.masterTypeNo);
                    var _RangeFrom = new SqlParameter("RangeFrom", checkMasterNameExistsRequest?.RangeFrom);
                    var _RangeTo = new SqlParameter("RangeTo", checkMasterNameExistsRequest?.RangeTo);

                    var obj = context.Checkspecialization.FromSqlRaw(
                        "Execute dbo.pro_CheckMasterNameExists @venueNo,@masterNo,@masterName,@masterValueno,@masterTypeNo,@RangeFrom,@RangeTo",
                          _venueNo, _masterNo, _masterName, _masterValueno, _masterTypeNo,_RangeFrom, _RangeTo).ToList();
                    iresult = obj[0].avail;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "specializationRepository.CheckMasterNameExists", ExceptionPriority.Low, ApplicationType.REPOSITORY, checkMasterNameExistsRequest?.venueNo, checkMasterNameExistsRequest?.masterNo, 0);
            }
            return iresult;
        }

    }
}