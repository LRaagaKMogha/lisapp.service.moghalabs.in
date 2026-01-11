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

namespace Dev.Repository
{
    public class MainDepartmentRepository : IMainDepartmentRepository
    {
        private IConfiguration _config;
        public MainDepartmentRepository(IConfiguration config) { _config = config; }
        public List<TblMainDepartment> GetMainDepartmentDetails(MainDepartmentmasterRequest maindeptmaster)
        {
            List<TblMainDepartment> maindeptresult = new List<TblMainDepartment>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _maindeptno = new SqlParameter("mainDeptno", maindeptmaster?.maindeptno);
                    var _venueno = new SqlParameter("venueNo", maindeptmaster?.venueno);
                    var _pageIndex = new SqlParameter("pageIndex", maindeptmaster?.pageIndex);

                    maindeptresult = context.GetMainmaster.FromSqlRaw(
                       "Execute dbo.pro_GetMainDepartment @maindeptno,@venueno,@pageIndex",
                        _maindeptno, _venueno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MainDepartmentRepository.GetMainDepartmentDetails" + maindeptmaster.maindeptno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, maindeptmaster.venueno, 0, 0);
            }
            return maindeptresult;
        }
    

        public MainDepartmentMasterResponse InsertMainDepartmentmaster(TblMainDepartment tblmaindepartment)        
        {
            MainDepartmentMasterResponse objresult = new MainDepartmentMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _maindeptno = new SqlParameter("maindeptno", tblmaindepartment?.maindeptno);
                    var _venueno = new SqlParameter("venueno", tblmaindepartment?.venueno);
                    var _userno = new SqlParameter("userno", tblmaindepartment?.userno);
                    var _departmentname = new SqlParameter("departmentname", tblmaindepartment?.departmentname);
                    var _displayname = new SqlParameter("displayName", tblmaindepartment?.displayname);
                    var _shortcode = new SqlParameter("shortcode", tblmaindepartment?.shortcode);
                    var _sequenceno = new SqlParameter("sequenceno", tblmaindepartment?.sequenceno);
                    var _status = new SqlParameter("status", tblmaindepartment?.status);

                    objresult = context.InsertMainDepartment.FromSqlRaw(
                    "Execute dbo.pro_InsertMainDepartment @maindeptno,@venueno,@userno,@departmentname," +
                    "@displayname,@shortcode, @sequenceno,@status",
                    _maindeptno, _venueno, _userno, _departmentname ,_displayname ,
                    _shortcode, _sequenceno, _status).AsEnumerable().FirstOrDefault();           
                }
            }
            catch (Exception ex)
            {           
               MyDevException.Error(ex, "MainDepartmentRepository.InsertMainDepartmentdetails - " + tblmaindepartment.maindeptno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tblmaindepartment.venueno,0, 0);
            }
            return objresult;
        }
    }
}

