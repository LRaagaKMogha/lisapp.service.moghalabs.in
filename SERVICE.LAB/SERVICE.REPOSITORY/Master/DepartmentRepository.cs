using Service.Model;
using Service.Model.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using Serilog;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dev.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private IConfiguration _config;
        public DepartmentRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get Department Details
        /// </summary>
        /// <returns></returns>
        public List<TblDepartment> GetDepartmentDetails(GetCommonMasterRequest getCommonMaster)
        {
            List<TblDepartment> objresult = new List<TblDepartment>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    if (getCommonMaster?.masterNo > 0)
                    {
                        objresult = context.TblDepartment.Where(x => x.VenueNo == getCommonMaster.venueno && x.VenueBranchNo == getCommonMaster.venuebranchno && x.DepartmentNo == getCommonMaster.masterNo && x.Status == true).ToList();
                    }
                    else
                    {
                        objresult = context.TblDepartment.Where(x => x.VenueNo == getCommonMaster.venueno && x.VenueBranchNo == getCommonMaster.venuebranchno && x.Status == true).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDepartmentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, getCommonMaster?.venueno, getCommonMaster?.venuebranchno, 0);
            }
            return objresult;
        }
        /// <summary>
        /// Search Department
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        public List<TblDepartment> SearchDepartment(string DepartmentName)
        {
            List<TblDepartment> objresult = new List<TblDepartment>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    objresult = context.TblDepartment.Where(a => a.DepartmentName == DepartmentName).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SearchDepartment - " + DepartmentName, ExceptionPriority.Low, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;

        }
        /// <summary>
        /// Insert Department Details
        /// </summary>
        /// <param name="Departmentitem"></param>
        /// <returns></returns>
        public int InsertDepartmentDetails(TblDepartment Departmentitem)
        {
            int result = 0;
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    //if (Departmentitem.DepartmentNo > 0)
                    //{
                    //    Departmentitem.ModifiedOn = DateTime.Now;
                    //    Departmentitem.ModifiedBy = Departmentitem?.CreatedBy;
                    //    context.Entry(Departmentitem).State = EntityState.Modified;
                    //}
                    //else
                    //{
                    //    Departmentitem.CreatedOn = DateTime.Now;
                    //    Departmentitem.ModifiedOn = DateTime.Now;
                    //    context.TblDepartment.Add(Departmentitem);
                    //}
                    //context.SaveChanges();

                    var _DepartmentNo = new SqlParameter("DepartmentNo", Departmentitem.DepartmentNo);
                    var _DepartmentCode = new SqlParameter("DepartmentCode", Departmentitem.DepartmentCode);
                    var _DepartmentName = new SqlParameter("DepartmentName", Departmentitem.DepartmentName);
                    var _DepartmentDisplayText = new SqlParameter("DepartmentDisplayText", Departmentitem.DepartmentDisplayText);
                    var _DeptSequenceNo = new SqlParameter("DeptSequenceNo", Departmentitem.DeptSequenceNo);
                    var _IsSample = new SqlParameter("IsSample", Departmentitem.IsSample);
                    var _IsCytology = new SqlParameter("IsCytology", Departmentitem.IsCytology);
                    var _IsHisto = new SqlParameter("IsHisto", Departmentitem.IsHisto);
                    var _VenueNo = new SqlParameter("VenueNo", Departmentitem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", Departmentitem?.VenueBranchNo);
                    var _status = new SqlParameter("Status", Departmentitem.Status);
                    var _CreatedBy = new SqlParameter("CreatedBy", Departmentitem.CreatedBy);

                    var _MainDeptNo = new SqlParameter("MainDeptNo", Departmentitem.MainDeptNo);
                    var _DoctorName1 = new SqlParameter("DoctorName1", Departmentitem?.DoctorName1);
                    var _DoctorSign1 = new SqlParameter("DoctorSign1", Departmentitem?.DoctorSign1);
                    var _DoctorDescription1 = new SqlParameter("DoctorDescription1", Departmentitem?.DoctorDescription1);
                    var _DoctorName2 = new SqlParameter("DoctorName2", Departmentitem?.DoctorName2);
                    var _DoctorSign2 = new SqlParameter("DoctorSign2", Departmentitem?.DoctorSign2);
                    var _DoctorDescription2 = new SqlParameter("DoctorDescription2", Departmentitem?.DoctorDescription2);
                    var _DoctorName3 = new SqlParameter("DoctorName3", Departmentitem?.DoctorName3);
                    var _DoctorSign3 = new SqlParameter("DoctorSign3", Departmentitem?.DoctorSign3);
                    var _DoctorDescription3 = new SqlParameter("DoctorDescription3", Departmentitem?.DoctorDescription3);

                    var objResult = context.InsertDeptMaster.FromSqlRaw
                              ("Execute dbo.pro_InsertDeptMaster @DepartmentNo, @DepartmentCode, @DepartmentName, @DepartmentDisplayText, @DeptSequenceNo, @IsSample, @IsCytology, @IsHisto,@VenueNo,@VenueBranchNo,@Status," +
                              "@CreatedBy,@MainDeptNo,@DoctorName1,@DoctorSign1,@DoctorDescription1,@DoctorName2,@DoctorSign2,@DoctorDescription2,@DoctorName3,@DoctorSign3,@DoctorDescription3",
                              _DepartmentNo, _DepartmentCode, _DepartmentName, _DepartmentDisplayText, _DeptSequenceNo, _IsSample, _IsCytology, _IsHisto, _VenueNo, _VenueBranchNo, _status, _CreatedBy, _MainDeptNo,
                              _DoctorName1, _DoctorSign1, _DoctorDescription1, _DoctorName2, _DoctorSign2, _DoctorDescription2, _DoctorName3, _DoctorSign3, _DoctorDescription3).ToList();
                    result = objResult[0].DeptNo;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertDepartmentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, Departmentitem?.VenueNo, Departmentitem?.VenueBranchNo, 0);
            }
            return result;
        }

        public List<GetMaindepartment> GetMaindepartmentdetail(GetDeptMasterRequest req)
        {
            List<GetMaindepartment> objresult = new List<GetMaindepartment>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.venueno);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.venuebranchno);
                    var _masterNo = new SqlParameter("masterNo", req?.masterNo);
                    var _Departmentnumber = new SqlParameter("Departmentnumber", req?.Departmentnumber);
                    var _PageIndex = new SqlParameter("PageIndex", req?.pageIndex);
                    
                    objresult = context.GetDepartmentDetail.FromSqlRaw(
                        "Execute dbo.Pro_Getdept @VenueNo, @VenueBranchNo, @masterNo,@Departmentnumber, @PageIndex",
                     _VenueNo, _VenueBranchNo, _masterNo, _Departmentnumber, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DepartmentRepository.GetMaindepartmentdetail", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.venueno, req?.venuebranchno, 0);
            }
            return objresult;

        }
        public List<DepartMentLangCodeRes> InsertLangCodeDeptMaster(DepartMentLangCodeReq req)
        {
            List<DepartMentLangCodeRes> objResult = new List<DepartMentLangCodeRes>();
            try
            {
                using (var context  = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _deptNo = new SqlParameter("DeptNo", req.DeptNo);
                    var _deptType = new SqlParameter("DeptType", req.DeptType);
                    var _languageCode = new SqlParameter("LanguageCode", req.LanguageCode);
                    var _languageText = new SqlParameter("LanguageText", req.LanguageText);
                    var _venueNo = new SqlParameter("VenueNo", req.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    var _status = new SqlParameter("Status",req.Status);
                    var _userNo = new SqlParameter("UserNo", req.UserNo);
                    var _isEdit = new SqlParameter("IsEdit", req.IsEdit);


                    objResult = context.InsertLangCodeDeptMasters.FromSqlRaw
                        ("Execute dbo.Pro_InsertLanguageCodeDepartment @DeptNo, @DeptType, @LanguageCode, @LanguageText, @VenueNo, @VenueBranchNo, @Status, @UserNo,@IsEdit",
                       _deptNo, _deptType, _languageCode, _languageText, _venueNo, _venueBranchNo, _status, _userNo,_isEdit).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DepartmentRepository.InsertLangCodeDeptMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, 0);
            }
            return objResult;
        }

        public List<GetDeptLangCodeRes> GetLangCodeDeptMaster(GetDeptLangCodeReq req)
        {
            List<GetDeptLangCodeRes> objResult = new List<GetDeptLangCodeRes>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _deptNo = new SqlParameter("@DeptNo", req.DeptNo);
                    var _deptType = new SqlParameter("@DeptType", req.DeptType);
                    var _venueNo = new SqlParameter("@VenueNo", req.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", req.VenueBranchNo);
                    objResult = context.GetLangCodeDeptMasters.FromSqlRaw
                        ("EXEC dbo.Pro_GetLanguageCodeDepartment @DeptNo, @DeptType, @VenueNo, @VenueBranchNo",
                        _deptNo, _deptType, _venueNo,_venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DepartmentRepository.InsertLangCodeDeptMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, 0);
            }
            return objResult;
        }


    }
}

